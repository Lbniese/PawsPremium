using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;
using Paws.Core.Abilities.Attributes;
using Paws.Core.Abilities.Feral;
using Paws.Core.Abilities.Shared;
using Paws.Core.Conditions;
using Paws.Core.Utilities;
using Styx;
using Styx.Common;

namespace Paws.Core.Managers
{
    /// <summary>
    ///     Provides the management of ability chains.
    /// </summary>
    public sealed class AbilityChainsManager
    {
        private Queue<ChainedAbility> _abilityQueue;

        /// <summary>
        ///     The trigger timer used to evaluate the time between casted abilities.
        /// </summary>
        private readonly Stopwatch _triggerTimer = new Stopwatch();

        /// <summary>
        ///     Builds the list of chainable abilities on creation.
        /// </summary>
        static AbilityChainsManager()
        {
            AllowedAbilityTypes = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(o => o.GetTypes())
                .Where(o => Attribute.IsDefined(o, typeof (AbilityChainAttribute)))
                .OrderBy(o => o.Name)
                .ToList();
        }

        public AbilityChainsManager()
        {
            AbilityChains = new List<AbilityChain>();
        }

        public static List<Type> AllowedAbilityTypes { get; private set; }

        /// <summary>
        ///     The time between each ability before deciding to kill the ability chain.
        /// </summary>
        public int TriggerTimerElapsedMs { get; set; }

        /// <summary>
        ///     The list of ability chains loaded into the system.
        /// </summary>
        public List<AbilityChain> AbilityChains { get; set; }

        /// <summary>
        ///     The current ability chain that has been triggered, otherwise null.
        /// </summary>
        public AbilityChain TriggeredAbilityChain { get; set; }

        /// <summary>
        ///     If a trigger is in action, this will return true.
        /// </summary>
        public bool TriggerInAction { get; set; }

        public void RegisterAbilityChain(AbilityChain abilityChain)
        {
            if (Main.Product != Product.Premium) return;
            if (HotkeysManager.Hotkeys.Any(o => o.Name == "Paws_" + abilityChain.Name))
                return;

            AbilityChains.Add(abilityChain);

            var hotKey = HotkeysManager.Hotkeys.FirstOrDefault(o => o.Name == abilityChain.Name);
            if (hotKey != null)
            {
                HotkeysManager.Unregister(hotKey);
            }

            var registeredHotKey = HotkeysManager.Register("Paws_" + abilityChain.Name, abilityChain.HotKey,
                abilityChain.ModiferKey, KeyIsPressed);

            Log.AbilityChain(string.Format("Ability chain successfully registered ({0}: {1} + {2}).",
                registeredHotKey.Name, registeredHotKey.ModifierKeys, registeredHotKey.Key));
        }

        public async Task<bool> TriggeredRotation()
        {
            try
            {
                if (TriggeredAbilityChain != null)
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
                        if (_triggerTimer.ElapsedMilliseconds > TriggerTimerElapsedMs)
                        {
                            Log.Diagnostics(
                                string.Format(
                                    "Ability cast failed: {0}, skipping to the next link in the ability chain.",
                                    ability.FriendlyName));

                            _abilityQueue.Dequeue();
                            _triggerTimer.Restart();

                            return await TriggeredRotation();
                        }
                    }
                    else
                    {
                        Log.AbilityChain("The " + TriggeredAbilityChain.Name + " ability chain finished.");

                        TriggerInAction = false;
                        TriggeredAbilityChain = null;

                        _triggerTimer.Reset();
                    }
                }
            }
            catch (Exception ex)
            {
                if (TriggeredAbilityChain != null)
                {
                    Log.AbilityChain(string.Format("{0} ability chain failed. Exception raised: {1}",
                        TriggeredAbilityChain.Name, ex));

                    TriggerInAction = false;
                }
                TriggeredAbilityChain = null;

                _triggerTimer.Reset();
            }

            return false;
        }

        public void Trigger(AbilityChain abilityChain)
        {
            if (TriggerInAction) return;
            foreach (var link in abilityChain.ChainedAbilities.Where(link => link.MustBeReady).Where(link => link.Instance.Spell.CooldownTimeLeft.TotalMilliseconds > 2000))
            {
                Log.AbilityChain(
                    string.Format(
                        "NOTICE: The {0} ability chain has been canceled. {1} is still on cooldown (Time left: {2})",
                        abilityChain.Name, link.Instance.Spell.Name, link.Instance.Spell.CooldownTimeLeft));
                return;
            }

            Log.AbilityChain("The " + abilityChain.Name + " ability chain has been triggered.");

            TriggerInAction = true;
            TriggeredAbilityChain = abilityChain;
            _abilityQueue = new Queue<ChainedAbility>(TriggeredAbilityChain.ChainedAbilities);

            TriggerTimerElapsedMs = 2000;
            // give each ability 2 seconds to cast before moving on to the next ability.

            _triggerTimer.Restart();
        }

        /// <summary>
        ///     The internal method used to handle hotkeys that are pressed. This will subsequently call the Trigger method
        ///     provided the various checks and balances pass.
        /// </summary>
        private void KeyIsPressed(Hotkey hotKey)
        {
            // Ability Chain Check... placing a "Paws_" prefix ensures that no other registered hotkeys are messed with in the system.
            var abilityChain = AbilityChains.SingleOrDefault(o => "Paws_" + o.Name == hotKey.Name);
            if (abilityChain == null) return;
            if (StyxWoW.Me.Specialization == abilityChain.Specialization)
            {
                // We have a triggered Ability Chain
                Trigger(abilityChain);
            }
            else
            {
                Log.AbilityChain(
                    string.Format(
                        "Hotkey detected, but your specialization must be {0} to trigger the {1} ability chain.",
                        abilityChain.Specialization.ToString().Replace("Druid", string.Empty), hotKey.Name));
            }
        }

        /// <summary>
        ///     Retrieves a new list of allowed abilities based on the list of allowed types.
        /// </summary>
        public static List<ChainedAbility> GetAllowedAbilities()
        {
            return AllowedAbilityTypes.Select(abilityType => new ChainedAbility(abilityType)).ToList();
        }

        /// <summary>
        ///     Saves the dataset of abilities to file.
        /// </summary>
        public static void SaveDataSet(List<AbilityChain> abilityChains)
        {
            var pathToFile = Path.Combine(Styx.Helpers.Settings.CharacterSettingsDirectory, "Paws-AbilityChains.xml");

            var serializer = new XmlSerializer(typeof (List<AbilityChain>));

            using (var fileStream = new FileStream(pathToFile, FileMode.Create))
            {
                serializer.Serialize(fileStream, abilityChains);
                fileStream.Close();
            }
        }

        public static void LoadDataSet()
        {
            List<AbilityChain> listOfAbilityChains;

            var pathToFile = Path.Combine(Styx.Helpers.Settings.CharacterSettingsDirectory, "Paws-AbilityChains.xml");

            if (!File.Exists(pathToFile))
            {
                // Create a default abilities list...
                var defaultChains = new List<AbilityChain>();

                // Ability chain sample: Burst Damage
                var sampleBurstChain = new AbilityChain("Burst Damage")
                {
                    Specialization = WoWSpec.DruidFeral,
                    HotKey = Keys.F,
                    ModiferKey = ModifierKeys.Control
                };

                sampleBurstChain.ChainedAbilities.Add(new ChainedAbility(new IncarnationAbility(), TargetType.Me, true));
                sampleBurstChain.ChainedAbilities.Add(new ChainedAbility(new BerserkAbility(), TargetType.Me, true));

                defaultChains.Add(sampleBurstChain);

                // Ability chain sample: Burst Damage
                var sampleDefenseChain = new AbilityChain("HotW Defense")
                {
                    Specialization = WoWSpec.DruidFeral,
                    HotKey = Keys.D,
                    ModiferKey = ModifierKeys.Control
                };

                sampleDefenseChain.ChainedAbilities.Add(new ChainedAbility(new BearFormPowerShiftAbility(),
                    TargetType.Me, false));
                sampleDefenseChain.ChainedAbilities.Add(new ChainedAbility(new HeartOfTheWildAbility(), TargetType.Me,
                    true));
                sampleDefenseChain.ChainedAbilities.Add(new ChainedAbility(new CenarionWardAbility(), TargetType.Me,
                    false));
                sampleDefenseChain.ChainedAbilities.Add(new ChainedAbility(new SurvivalInstinctsAbility(), TargetType.Me,
                    false));

                defaultChains.Add(sampleDefenseChain);

                // Ability chain sample: Cyclone
                var sampleCycloneChain = new AbilityChain("Cyclone")
                {
                    Specialization = WoWSpec.DruidFeral,
                    HotKey = Keys.C,
                    ModiferKey = ModifierKeys.Control
                };

                sampleCycloneChain.ChainedAbilities.Add(new ChainedAbility(new CycloneAbility(),
                    TargetType.MyCurrentTarget, false));

                defaultChains.Add(sampleCycloneChain);

                // Ability chain sample: Entangling Roots
                var sampleEntanglingRootsChain = new AbilityChain("Entangling Roots")
                {
                    Specialization = WoWSpec.DruidFeral,
                    HotKey = Keys.R,
                    ModiferKey = ModifierKeys.Shift
                };

                sampleEntanglingRootsChain.ChainedAbilities.Add(new ChainedAbility(new EntanglingRootsAbility(),
                    TargetType.MyCurrentTarget, false));

                defaultChains.Add(sampleEntanglingRootsChain);

                SaveDataSet(defaultChains);
                LoadDataSet();
            }

            var serializer = new XmlSerializer(typeof (List<AbilityChain>));

            using (var reader = new StreamReader(pathToFile))
            {
                listOfAbilityChains = (List<AbilityChain>) serializer.Deserialize(reader);
                reader.Close();
            }

            foreach (var chain in listOfAbilityChains)
            {
                Instance.RegisterAbilityChain(chain);
            }
        }

        #region Singleton Stuff

        private static AbilityChainsManager _singletonInstance;

        /// <summary>
        ///     Singleton instance.
        /// </summary>
        public static AbilityChainsManager Instance
        {
            get { return _singletonInstance ?? (_singletonInstance = new AbilityChainsManager()); }
        }

        /// <summary>
        ///     Rebuilds and reloads all of the abilities. Useful after changing settings.
        /// </summary>
        public static void Init()
        {
            _singletonInstance = new AbilityChainsManager();

            var pawsHotKeys = HotkeysManager.Hotkeys.Where(o => o.Name.StartsWith("Paws_"));

            var hotKeys = pawsHotKeys as Hotkey[] ?? pawsHotKeys.ToArray();
            for (var i = 0; i < hotKeys.Count(); i++)
            {
                var hotKey = hotKeys.ElementAt(i);
                HotkeysManager.Unregister(hotKey);

                Log.Diagnostics(string.Format("Unregistered Hotkey {0}", hotKey.Name));
            }
        }

        #endregion
    }
}