using System.Collections.Generic;
using System.Threading.Tasks;
using Paws.Core.Conditions;
using Paws.Core.Managers;
using Paws.Core.Utilities;
using Styx;
using Styx.WoWInternals;
using Styx.WoWInternals.WoWObjects;

namespace Paws.Core.Abilities
{
    /// <summary>
    ///     The base Ability class that all abilities should inherit from. This class cannot be directly instantiated.
    /// </summary>
    public abstract class AbilityBase : IAbility
    {
        protected bool MustWaitForGlobalCooldown;
        protected bool MustWaitForSpellCooldown;

        /// <summary>
        ///     <para>The default declaration defines an instant use ability that is on the GCD</para>
        ///     <para>mustWaitForGlobalCooldown = true</para>
        ///     <para>mustWaitForSpellCooldown = false</para>
        /// </summary>
        protected AbilityBase(WoWSpell spell, bool mustWaitForGlobalCooldown = true, bool mustWaitForSpellCooldown = false)
        {
            Category = AbilityCategory.Normal;

            if (spell == null)
                throw new AbilityException("Spell cannot be null");

            Spell = spell;

            MustWaitForGlobalCooldown = mustWaitForGlobalCooldown;
            MustWaitForSpellCooldown = mustWaitForSpellCooldown;

            Conditions = new List<ICondition>();
            RequiredConditions = new List<ICondition>();
        }

        protected static LocalPlayer Me
        {
            get { return StyxWoW.Me; }
        }

        protected static WoWUnit MyCurrentTarget
        {
            get { return Me.CurrentTarget; }
        }

        protected static SettingsManager Settings
        {
            get { return SettingsManager.Instance; }
        }

        /// <summary>
        ///     The category of the ability. Displayed during logging.
        /// </summary>
        public AbilityCategory Category { get; set; }

        /// <summary>
        ///     The spell that the ability directly relates to.
        /// </summary>
        public WoWSpell Spell { get; set; }

        /// <summary>
        ///     The list of conditions that must be satisfied prior to a casting attempt.
        /// </summary>
        public List<ICondition> Conditions { get; set; }

        /// <summary>
        ///     A list of conditions that are required for the Ability to function properly.
        /// </summary>
        public List<ICondition> RequiredConditions { get; set; }

        /// <summary>
        ///     (Non-Blocking) Casts the ability's spell on the specified target. The cast will only be attempted if the conditions
        ///     list is completely satisfied first.
        /// </summary>
        /// <returns>Returns true on a successful cast.</returns>
        public virtual async Task<bool> CastOnTarget(WoWUnit target)
        {
            return await CastManager.CastOnTarget(target, this, Conditions);
        }

        /// <summary>
        ///     Provides an opportunity to update the ability with any dynamic changes as necessary. This should be done during
        ///     Main.Pulse().
        /// </summary>
        public virtual void Update()
        {
            // Filler so that every implementing class does not have to update and those that need to can just override.
        }

        /// <summary>
        ///     Diagnostics for dumping the condition information useful for debugging issues.
        /// </summary>
        public virtual void LogConditionsDump()
        {
            var type = GetType();

            Log.Gui("------------------------");
            Log.Gui(string.Format("Dump of Ability: {0}", type.Name));
            foreach (var condition in Conditions)
            {
                Log.Gui(string.Format("\t{0}", condition));
                if (condition is CombatSwitchCondition)
                {
                    var combatCondition = condition as CombatSwitchCondition;
                    Log.Gui(string.Format("\t[In Combat]: {0}", combatCondition.ConditionIfInCombat.GetType().Name));
                    Log.Gui(string.Format("\t[Not In Combat]: {0}", combatCondition.ConditionIfNotInCobat.GetType().Name));
                }
            }
            Log.Gui("------------------------");
        }

        /// <summary>
        ///     Provides an implementation of how default settings should be applied for each ability.
        /// </summary>
        public virtual void ApplyDefaultSettings()
        {
            Conditions.Clear();

            Conditions.AddRange(RequiredConditions);

            if (MustWaitForGlobalCooldown) Conditions.Add(new IsOffGlobalCooldownCondition());
            if (MustWaitForSpellCooldown) Conditions.Add(new SpellIsNotOnCooldownCondition(Spell));
        }
    }
}