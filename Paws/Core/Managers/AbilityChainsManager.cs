using Paws.Core.Abilities;
using Paws.Core.Utilities;
using Styx;
using Styx.WoWInternals.WoWObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shared = Paws.Core.Abilities.Shared;
using Feral = Paws.Core.Abilities.Feral;
using Guardian = Paws.Core.Abilities.Guardian;
using Paws.Core.Abilities.Attributes;
using Paws.Core.Conditions;
using System.Diagnostics;
using Buddy.Coroutines;
using Styx.Common;
using Styx.CommonBot;
using System.IO;
using System.Xml.Serialization;

namespace Paws.Core.Managers
{
    /// <summary>
    /// Provides the management of ability chains.
    /// </summary>
    public sealed class AbilityChainsManager
    {
        public static List<Type> AllowedAbilityTypes { get; private set; }

        /// <summary>
        /// The time between each ability before deciding to kill the ability chain.
        /// </summary>
        public int TriggerTimerElapsedMs { get; set; }

        #region Singleton Stuff

        private static AbilityChainsManager _singletonInstance;

        /// <summary>
        /// Singleton instance.
        /// </summary>
        public static AbilityChainsManager Instance
        {
            get
            {
                return _singletonInstance ?? (_singletonInstance = new AbilityChainsManager());
            }
        }

        /// <summary>
        /// Rebuilds and reloads all of the abilities. Useful after changing settings.
        /// </summary>
        public static void Init()
        {
            _singletonInstance = new AbilityChainsManager();

            var pawsHotKeys = HotkeysManager.Hotkeys.Where(o => o.Name.StartsWith("Paws_"));

            for (int i = 0; i < pawsHotKeys.Count(); i++)
            {
                var hotKey = pawsHotKeys.ElementAt(i);
                HotkeysManager.Unregister(hotKey);

                Log.Diagnostics(string.Format("Unregistered Hotkey {0}", hotKey.Name));
            }
        }

        private static SettingsManager Settings { get { return SettingsManager.Instance; } }

        #endregion

        /// <summary>
        /// The list of ability chains loaded into the system.
        /// </summary>
        public List<AbilityChain> AbilityChains { get; set; }

        /// <summary>
        /// The current ability chain that has been triggered, otherwise null.
        /// </summary>
        public AbilityChain TriggeredAbilityChain { get; set; }

        /// <summary>
        /// If a trigger is in action, this will return true.
        /// </summary>
        public bool TriggerInAction { get; set; }

        /// <summary>
        /// The trigger timer used to evaluate the time between casted abilities.
        /// </summary>
        private Stopwatch _triggerTimer = new Stopwatch();

        public AbilityChainsManager()
        {
            this.AbilityChains = new List<AbilityChain>();
        }

        public void RegisterAbilityChain(AbilityChain abilityChain)
        {
            if (Main.Product == Product.Premium)
            {
                if (HotkeysManager.Hotkeys.Any(o => o.Name == "Paws_" + abilityChain.Name))
                    return;

                this.AbilityChains.Add(abilityChain);

                var hotKey = HotkeysManager.Hotkeys.FirstOrDefault(o => o.Name == abilityChain.Name);
                if (hotKey != null)
                {
                    HotkeysManager.Unregister(hotKey);
                }

                var registeredHotKey = HotkeysManager.Register("Paws_" + abilityChain.Name, abilityChain.HotKey, abilityChain.ModiferKey, KeyIsPressed);

                Log.AbilityChain(string.Format("Ability chain successfully registered ({0}: {1} + {2}).", registeredHotKey.Name, registeredHotKey.ModifierKeys, registeredHotKey.Key));
            }
        }

        private Queue<ChainedAbility> _abilityQueue;

        public async Task<bool> TriggeredRotation()
        {
            try
            {
                if (this.TriggeredAbilityChain != null)
                {
                    if (!_triggerTimer.IsRunning) _triggerTimer.Start();
                    if (_abilityQueue.Count > 0)
                    {
                        var ability = _abilityQueue.Peek();
                        Log.Diagnostics(string.Format("Peeking ability: {0}", ability.FriendlyName));

                        if (await ability.Instance.CastOnTarget(UnitManager.TargetTypeConverter(ability.TargetType)))
                        {
                            Log.Diagnostics(string.Format("ability cast successful: {0}", ability.FriendlyName));

                            _abilityQueue.Dequeue();
                            _triggerTimer.Restart();

                            return await TriggeredRotation();
                        }
                        else
                        {
                            if (_triggerTimer.ElapsedMilliseconds > this.TriggerTimerElapsedMs)
                            {
                                Log.Diagnostics(string.Format("Ability cast failed: {0}, skipping to the next link in the ability chain.", ability.FriendlyName));

                                _abilityQueue.Dequeue();
                                _triggerTimer.Restart();

                                return await TriggeredRotation();
                            }
                        }
                    }
                    else
                    {
                        Log.AbilityChain("The " + this.TriggeredAbilityChain.Name + " ability chain finished.");

                        this.TriggerInAction = false;
                        this.TriggeredAbilityChain = null;

                        _triggerTimer.Reset();
                    }
                }
            }
            catch (Exception ex)
            {
                Log.AbilityChain(string.Format("{0} ability chain failed. Exception raised: {1}", this.TriggeredAbilityChain.Name, ex.ToString()));

                this.TriggerInAction = false;
                this.TriggeredAbilityChain = null;

                _triggerTimer.Reset();
            }

            return false;
        }

        public void Trigger(AbilityChain abilityChain)
        {
            if (!this.TriggerInAction)
            {
                foreach (var link in abilityChain.ChainedAbilities)
                {
                    if (link.MustBeReady)
                    {
                        if (link.Instance.Spell.CooldownTimeLeft.TotalMilliseconds > 2000) // allow the chain to que up if less than 2 seconds on the cooldown clock
                        {
                            Log.AbilityChain(string.Format("NOTICE: The {0} ability chain has been canceled. {1} is still on cooldown (Time left: {2})", abilityChain.Name, link.Instance.Spell.Name, link.Instance.Spell.CooldownTimeLeft));
                            return;
                        }
                    }
                }

                Log.AbilityChain("The " + abilityChain.Name + " ability chain has been triggered.");

                this.TriggerInAction = true;
                this.TriggeredAbilityChain = abilityChain;
                _abilityQueue = new Queue<ChainedAbility>(this.TriggeredAbilityChain.ChainedAbilities);

                this.TriggerTimerElapsedMs = 2000; // give each ability 2 seconds to cast before moving on to the next ability.

                _triggerTimer.Restart();
            }
        }

        public void KeyIsPressed(Hotkey hotKey)
        {
            if (!StyxWoW.Me.Combat)
            {
                Log.AbilityChain(string.Format("Hotkey detected, but you must be in combat to trigger the {0} ability chain.", hotKey.Name));
            }
            else
            {
                // Ability Chain Check...
                var abilityChain = this.AbilityChains.SingleOrDefault(o => "Paws_" + o.Name == hotKey.Name);
                if (abilityChain != null)
                {
                    if (StyxWoW.Me.Specialization == abilityChain.Specialization)
                    {
                        // We have a triggered Ability Chain
                        Trigger(abilityChain);
                    }
                    else
                    {
                        Log.AbilityChain(string.Format("Hotkey detected, but your specialization must be {0} to trigger the {1} ability chain.", 
                            abilityChain.Specialization.ToString().Replace("Druid", string.Empty), hotKey.Name));
                    }
                }
            }
        }

        /// <summary>
        /// Retrieves a new list of allowed abilities based on the list of allowed types.
        /// </summary>
        public static List<ChainedAbility> GetAllowedAbilities()
        {
            List<ChainedAbility> allowedAbilities = new List<ChainedAbility>();

            foreach (Type abilityType in AllowedAbilityTypes)
            {
                allowedAbilities.Add(new ChainedAbility(abilityType));
            }

            return allowedAbilities;
        }

        /// <summary>
        /// Saves the dataset of abilities to file.
        /// </summary>
        public static void SaveDataSet(List<AbilityChain> abilityChains)
        {
            var pathToFile = Path.Combine(Styx.Helpers.Settings.CharacterSettingsDirectory, "Paws-AbilityChains.xml");

            XmlSerializer serializer = new XmlSerializer(typeof(List<AbilityChain>));

            using (var fileStream = new FileStream(pathToFile, FileMode.Create))
            {
                serializer.Serialize(fileStream, abilityChains);
                fileStream.Close();
            }
        }

        public static void LoadDataSet()
        {
            var listOfAbilityChains = new List<AbilityChain>();

            var pathToFile = Path.Combine(Styx.Helpers.Settings.CharacterSettingsDirectory, "Paws-AbilityChains.xml");

            if (!File.Exists(pathToFile))
            {
                // Create a default abilities list...
                List<AbilityChain> defaultChains = new List<AbilityChain>();

                // Ability chain sample: Burst Damage
                AbilityChain sampleBurstChain = new AbilityChain("Burst Damage");

                sampleBurstChain.Specialization = WoWSpec.DruidFeral;
                sampleBurstChain.HotKey = System.Windows.Forms.Keys.F;
                sampleBurstChain.ModiferKey = ModifierKeys.Control;
                sampleBurstChain.ChainedAbilities.Add(new ChainedAbility(new Feral.IncarnationAbility(), TargetType.Me, true));
                sampleBurstChain.ChainedAbilities.Add(new ChainedAbility(new Feral.BerserkAbility(), TargetType.Me, true));

                defaultChains.Add(sampleBurstChain);

                // Ability chain sample: Burst Damage
                AbilityChain sampleDefenseChain = new AbilityChain("HotW Defense");

                sampleDefenseChain.Specialization = WoWSpec.DruidFeral;
                sampleDefenseChain.HotKey = System.Windows.Forms.Keys.D;
                sampleDefenseChain.ModiferKey = ModifierKeys.Control;
                sampleDefenseChain.ChainedAbilities.Add(new ChainedAbility(new Feral.BearFormPowerShiftAbility(), TargetType.Me, false));
                sampleDefenseChain.ChainedAbilities.Add(new ChainedAbility(new Feral.HeartOfTheWildAbility(), TargetType.Me, true));
                sampleDefenseChain.ChainedAbilities.Add(new ChainedAbility(new Shared.CenarionWardAbility(), TargetType.Me, false));
                sampleDefenseChain.ChainedAbilities.Add(new ChainedAbility(new Feral.SurvivalInstinctsAbility(), TargetType.Me, false));

                defaultChains.Add(sampleDefenseChain);

                // Ability chain sample: Cyclone
                AbilityChain sampleCycloneChain = new AbilityChain("Cyclone");

                sampleCycloneChain.Specialization = WoWSpec.DruidFeral;
                sampleCycloneChain.HotKey = System.Windows.Forms.Keys.C;
                sampleCycloneChain.ModiferKey = ModifierKeys.Control;
                sampleCycloneChain.ChainedAbilities.Add(new ChainedAbility(new Shared.CycloneAbility(), TargetType.MyCurrentTarget, false));

                defaultChains.Add(sampleCycloneChain);

                // Ability chain sample: Entangling Roots
                AbilityChain sampleEntanglingRootsChain = new AbilityChain("Entangling Roots");

                sampleEntanglingRootsChain.Specialization = WoWSpec.DruidFeral;
                sampleEntanglingRootsChain.HotKey = System.Windows.Forms.Keys.R;
                sampleEntanglingRootsChain.ModiferKey = ModifierKeys.Shift;
                sampleEntanglingRootsChain.ChainedAbilities.Add(new ChainedAbility(new Shared.EntanglingRootsAbility(), TargetType.MyCurrentTarget, false));

                defaultChains.Add(sampleEntanglingRootsChain);

                SaveDataSet(defaultChains);
                LoadDataSet();
            }

            XmlSerializer serializer = new XmlSerializer(typeof(List<AbilityChain>));

            using (StreamReader reader = new StreamReader(pathToFile))
            {
                listOfAbilityChains = (List<AbilityChain>)serializer.Deserialize(reader);
                reader.Close();
            }

            foreach(var chain in listOfAbilityChains)
            {
                AbilityChainsManager.Instance.RegisterAbilityChain(chain);
            }
        }

        /// <summary>
        /// Builds the list of chainable abilities on creation.
        /// </summary>
        static AbilityChainsManager()
        {
            AllowedAbilityTypes = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(o => o.GetTypes())
                .Where(o => Attribute.IsDefined(o, typeof(AbilityChainAttribute)))
                .OrderBy(o => o.Name)
                .ToList();
        }
    }
}
