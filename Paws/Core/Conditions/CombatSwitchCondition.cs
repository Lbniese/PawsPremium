using Styx;

namespace Paws.Core.Conditions
{
    /// <summary>
    ///     Condition based on the state of combat.
    /// </summary>
    public class CombatSwitchCondition : ICondition
    {
        public CombatSwitchCondition(ICondition conditionIfInCombat, ICondition conditionIfNotInCombat)
        {
            ConditionIfInCombat = conditionIfInCombat;
            ConditionIfNotInCobat = conditionIfNotInCombat;
        }

        /// <summary>
        ///     The condition to check if the player is in combat.
        /// </summary>
        public ICondition ConditionIfInCombat { get; set; }

        /// <summary>
        ///     The condition to check if the player is not in combat.
        /// </summary>
        public ICondition ConditionIfNotInCobat { get; set; }

        public bool Satisfied()
        {
            return StyxWoW.Me.Combat
                ? ConditionIfInCombat.Satisfied()
                : ConditionIfNotInCobat.Satisfied();
        }
    }
}