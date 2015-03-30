using Styx;

namespace Paws.Core.Conditions
{
    /// <summary>
    /// Condition based on if the player is not in combat.
    /// </summary>
    public class MeNotInCombatCondition : ICondition
    {
        public bool Satisfied()
        {
            return !StyxWoW.Me.Combat;
        }
    }
}
