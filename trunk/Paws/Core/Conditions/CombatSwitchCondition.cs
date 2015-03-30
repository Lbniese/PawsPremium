using Paws.Core.Utilities;
using System;
namespace Paws.Core.Conditions
{
    /// <summary>
    /// Condition based on the state of combat.
    /// </summary>
    public class CombatSwitchCondition : ICondition
    {
        /// <summary>
        /// The condition to check if the player is in combat.
        /// </summary>
        public ICondition ConditionIfInCombat { get; set; }

        /// <summary>
        /// The condition to check if the player is not in combat.
        /// </summary>
        public ICondition ConditionIfNotInCobat { get; set; }

        public CombatSwitchCondition(ICondition conditionIfInCombat, ICondition conditionIfNotInCombat)
        {
            this.ConditionIfInCombat = conditionIfInCombat;
            this.ConditionIfNotInCobat = conditionIfNotInCombat;
        }

        public bool Satisfied()
        {
            return (Styx.StyxWoW.Me.Combat 
                ? ConditionIfInCombat.Satisfied() 
                : ConditionIfNotInCobat.Satisfied());
        }
    }
}
