using Paws.Core.Conditions.Attributes;
using Styx;

namespace Paws.Core.Conditions
{
    /// <summary>
    /// Condition based on if the player is in Combat.
    /// </summary>
    public class MeIsInCombatCondition : ICondition
    {
        public bool Satisfied()
        {
            return StyxWoW.Me.Combat;
        }
    }
}
