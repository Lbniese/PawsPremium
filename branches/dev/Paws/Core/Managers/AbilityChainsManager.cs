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

namespace Paws.Core.Managers
{
    /// <summary>
    /// Provides the management of ability chains.
    /// </summary>
    public sealed class AbilityChainsManager
    {
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

        public List<Type> AllowedAbilityTypes { get; set; }
        public List<AbilityChain> AbilityChains { get; set; }

        public AbilityChain TriggeredAbilityChain { get; set; }

        public bool TriggerInAction { get; set; }

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

            foreach (var type in AllowedAbilityTypes)
            {
                Log.GUI(type.Name);
            }
            Log.GUI("Count: " + AllowedAbilityTypes.Count.ToString());


            LoadAbilityChains();
        }

        public void LoadAbilityChains()
        {
            this.AbilityChains = new List<AbilityChain>();

            // Here we will load the ability chains from file. For now, we just use test fixtures.

            AbilityChain testChain = new AbilityChain();
            testChain.Trigger = TriggerType.HotKeyButton;

            testChain.RegisteredHotKeyName = "Burst";

            // Our test chain will make sure we have an attackable target that has 85% health or less.
            testChain.Conditions.Add(new MeHasAttackableTargetCondition());
            testChain.Conditions.Add(new TargetHealthRangeCondition(TargetType.MyCurrentTarget, 0, 85));

            testChain.Abilities.Add(new Shared.MightyBashAbility());
            testChain.Abilities.Add(new Feral.BerserkAbility());
            testChain.Abilities.Add(new Feral.IncarnationAbility());

            this.AbilityChains.Add(testChain);
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
                foreach (var ability in this.TriggeredAbilityChain.Abilities)
                {
                    if (await ability.CastOnTarget(StyxWoW.Me.CurrentTarget))
                    {
                        // Hack. Should build a queue.
                        if (this.TriggeredAbilityChain.Abilities.Last() == ability)
                        {
                            this.TriggerInAction = false;
                            Log.GUI("Ability Chain Finished. Resuming normal operations.");
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
                Log.GUI("Ability Chain Triggered.");

                this.TriggerInAction = true;
                this.TriggeredAbilityChain = abilityChain;
            }
        }
    }
}
