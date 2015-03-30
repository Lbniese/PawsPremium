using Paws.Core.Conditions;
using Paws.Core.Managers;
using Paws.Core.Utilities;
using Styx;
using Styx.WoWInternals;
using Styx.WoWInternals.WoWObjects;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Paws.Core.Abilities
{
    /// <summary>
    /// The base Ability class that all abilities should inherit from. This class cannot be directly instantiated. 
    /// </summary>
    public abstract class AbilityBase : IAbility
    {
        /// <summary>
        /// The category of the ability. Displayed during logging.
        /// </summary>
        public AbilityCategory Category { get; set; }

        /// <summary>
        /// The spell that the ability directly relates to.
        /// </summary>
        public WoWSpell Spell { get; set; }

        /// <summary>
        /// The list of conditions that must be satisfied prior to a casting attempt.
        /// </summary>
        public List<ICondition> Conditions { get; protected set; }

        protected static LocalPlayer Me { get { return StyxWoW.Me; } }
        protected static WoWUnit MyCurrentTarget { get { return Me.CurrentTarget; } }
        protected static SettingsManager Settings { get { return SettingsManager.Instance; } }

        protected bool _mustWaitForGlobalCooldown;
        protected bool _mustWaitForSpellCooldown;

        /// <summary>
        /// <para>The default declaration defines an instant use ability that is on the GCD</para>
        /// <para>mustWaitForGlobalCooldown = true</para>
        /// <para>mustWaitForSpellCooldown = false</para>
        /// </summary>
        public AbilityBase(WoWSpell spell, bool mustWaitForGlobalCooldown = true, bool mustWaitForSpellCooldown = false)
        {
            this.Category = AbilityCategory.Normal;

            if (spell == null)
                throw new AbilityException("Spell cannot be null");

            this.Spell = spell;

            _mustWaitForGlobalCooldown = mustWaitForGlobalCooldown;
            _mustWaitForSpellCooldown = mustWaitForSpellCooldown;

            this.Conditions = new List<ICondition>();

            if (_mustWaitForGlobalCooldown) this.Conditions.Add(new IsOffGlobalCooldownCondition());
            if (_mustWaitForSpellCooldown) this.Conditions.Add(new SpellIsNotOnCooldownCondition(this.Spell));
        }

        /// <summary>
        /// (Non-Blocking) Casts the ability's spell on the specified target. The cast will only be attempted if the conditions list is completely satisfied first.
        /// </summary>
        /// <returns>Returns true on a successful cast.</returns>
        public virtual async Task<bool> CastOnTarget(WoWUnit target)
        {
            return await CastManager.CastOnTarget(target, this, this.Conditions);
        }

        /// <summary>
        /// Provides an opportunity to update the ability with any dynamic changes as necessary. This should be done during Main.Pulse().
        /// </summary>
        public virtual void Update()
        {
            // Filler so that every implementing class does not have to update and those that need to can just override.
        }

        /// <summary>
        /// Diagnostics for dumping the condition information useful for debugging issues.
        /// </summary>
        public virtual void LogConditionsDump()
        {
            Type type = this.GetType();

            Log.GUI("------------------------");
            Log.GUI(string.Format("Dump of Ability: {0}", type.Name));
            foreach (var condition in this.Conditions)
            {
                Log.GUI(string.Format("\t{0}", condition));
                if (condition is CombatSwitchCondition)
                {
                    var combatCondition = condition as CombatSwitchCondition;
                    Log.GUI(string.Format("\t[In Combat]: {0}", combatCondition.ConditionIfInCombat.GetType().Name));
                    Log.GUI(string.Format("\t[Not In Combat]: {0}", combatCondition.ConditionIfNotInCobat.GetType().Name));
                }
            }
            Log.GUI("------------------------");
        }
    }
}
