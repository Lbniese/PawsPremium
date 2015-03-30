using Paws.Core.Conditions.Attributes;
using Styx;

namespace Paws.Core.Conditions
{
    /// <summary>
    /// Condition based on if the player is not moving.
    /// </summary>
    [ItemCondition(FriendlyName = "I am Not Moving")]
    public class MeIsNotMovingCondition : ICondition
    {
        public bool Satisfied()
        {
            return !StyxWoW.Me.IsMoving;
        }
    }
}
