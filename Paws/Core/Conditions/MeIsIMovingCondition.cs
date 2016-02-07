using Paws.Core.Conditions.Attributes;
using Styx;

namespace Paws.Core.Conditions
{
    /// <summary>
    ///     Condition based on if the player is moving.
    /// </summary>
    [ItemCondition(FriendlyName = "I am Moving")]
    public class MeIsMovingCondition : ICondition
    {
        public bool Satisfied()
        {
            return StyxWoW.Me.IsMoving;
        }
    }
}