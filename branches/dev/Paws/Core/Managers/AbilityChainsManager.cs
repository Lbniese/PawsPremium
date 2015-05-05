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
        }

        private static SettingsManager Settings { get { return SettingsManager.Instance; } }

        #endregion

        public List<AbilityChain> AbilityChains { get; set; }

        public AbilityChain TriggeredAbilityChain { get; set; }

        // public Queue<IAbility> AbilityQueue { get; set; }

        public bool TriggerInAction { get; set; }

        private Stopwatch _triggerTimer = new Stopwatch();

        /// <summary>
        /// Builds the list of abilities on creation.
        /// </summary>
        public AbilityChainsManager()
        {
            // Lets get a list of all chainable abilities...
            AllowedAbilityTypes = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(o => o.GetTypes())
                .Where(o => Attribute.IsDefined(o, typeof(AbilityChainAttribute)))
                .OrderBy(o => o.Name)
                .ToList();

            LoadAbilityChains();
        }

        public void LoadAbilityChains()
        {
            this.AbilityChains = new List<AbilityChain>();

            // Here we will load the ability chains from file. For now, we just use test fixtures.

            //AbilityChain testChain = new AbilityChain("Burst");
            //testChain.Trigger = TriggerType.HotKeyButton;

            //// Our test chain will make sure we have an attackable target that has 85% health or less.
            //testChain.Conditions.Add(new MeHasAttackableTargetCondition());
            //testChain.Conditions.Add(new TargetHealthRangeCondition(TargetType.MyCurrentTarget, 0, 85));

            //var berserk = new Feral.BerserkAbility();
            //berserk.Conditions.Add(new TargetHasAuraCondition(TargetType.Me, SpellBook.FeralIncarnationForm));

            //testChain.ChainedAbilities.Add(new ChainedAbility(new Shared.MightyBashAbility(), TargetType.MyCurrentTarget, false));
            //testChain.ChainedAbilities.Add(new ChainedAbility(new Feral.IncarnationAbility(), TargetType.Me, true));
            //testChain.ChainedAbilities.Add(new ChainedAbility(berserk, TargetType.Me, true));

            //this.AbilityChains.Add(testChain);
        }

        public void RegisterAbilityChain(AbilityChain abilityChain)
        {
            if (Main.Product == Product.Premium)
            {
                this.AbilityChains.Add(abilityChain);

                var hotKey = HotkeysManager.Hotkeys.FirstOrDefault(o => o.Name == abilityChain.Name);
                if (hotKey != null)
                {
                    HotkeysManager.Unregister(hotKey);
                }

                var registeredHotKey = HotkeysManager.Register(abilityChain.Name, abilityChain.HotKey, abilityChain.ModiferKey, KeyIsPressed);

                Log.GUI(string.Format("Ability chain successfully registered ({0}: {1} + {2}).", registeredHotKey.Name, registeredHotKey.ModifierKeys, registeredHotKey.Key));
            }
        }

        public void Update()
        {
            if (this.TriggeredAbilityChain != null)
            {
                if (!_triggerTimer.IsRunning) _triggerTimer.Start();
                if (_triggerTimer.ElapsedMilliseconds > this.TriggerTimerElapsedMs)
                {
                    _triggerTimer.Reset();

                    Log.GUI(string.Format("NOTICE: The {0} ability chain was canceled due to exeeding the alotted chain timer of {1} ms.", this.TriggeredAbilityChain.Name, this.TriggerTimerElapsedMs));

                    this.TriggerInAction = false;
                    this.TriggeredAbilityChain = null;
                }
            }
        }

        public void Check()
        {
            if (this.TriggeredAbilityChain == null)
            {
                foreach (var abilityChain in this.AbilityChains)
                {
                    // Short circuit to look for conditions...
                    if (abilityChain.Satisfied())
                    {
                        Log.GUI("Ability Chain Triggered - Normal Rotation Should now be Paused!");

                        // We have a triggered chain... now we put the manager in a state of ensuring the chain gets fired.
                        this.TriggeredAbilityChain = abilityChain;
                        return;
                    }
                }
            }
        }

        public async Task<bool> TriggeredRotation()
        {
            if (this.TriggeredAbilityChain != null)
            {
                foreach (var link in this.TriggeredAbilityChain.ChainedAbilities)
                {
                    if (await link.Instance.CastOnTarget(UnitManager.TargetTypeConverter(link.TargetType)))
                    {
                        if (this.TriggeredAbilityChain != null)
                        {
                            if (link == this.TriggeredAbilityChain.ChainedAbilities.Last())
                            {
                                Log.GUI("The " + this.TriggeredAbilityChain.Name + " ability chain finished.");

                                this.TriggerInAction = false;
                                this.TriggeredAbilityChain = null;

                                _triggerTimer.Reset();
                            }
                        }

                        return true;
                    }
                }
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
                            Log.GUI(string.Format("NOTICE: The {0} ability chain has been canceled. {1} is still on cooldown (Time left: {2})", abilityChain.Name, link.Instance.Spell.Name, link.Instance.Spell.CooldownTimeLeft));
                            return;
                        }
                    }
                }

                Log.GUI("The " + abilityChain.Name + " ability chain has been triggered.");

                this.TriggerInAction = true;
                this.TriggeredAbilityChain = abilityChain;

                this.TriggerTimerElapsedMs = abilityChain.ChainedAbilities.Count * 2000;

                _triggerTimer.Restart();
            }
        }

        public void KeyIsPressed(Hotkey hotKey)
        {
            //Log.GUI(string.Format("Key pressed: {0}, {1}, {2}, {3}", hotKey.Id, hotKey.Name, hotKey.ModifierKeys, hotKey.Key));

            // Ability Chain Check...
            var abilityChain = this.AbilityChains.SingleOrDefault(o => o.Trigger == TriggerType.HotKeyButton && o.Name == hotKey.Name);
            if (abilityChain != null)
            {
                // We have a triggered Ability Chain
                Trigger(abilityChain);
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
    }
}
