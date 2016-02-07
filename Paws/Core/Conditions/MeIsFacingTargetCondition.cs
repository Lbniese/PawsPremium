using Paws.Core.Conditions.Attributes;
using Styx;

namespace Paws.Core.Conditions
{
    /// <summary>
    ///     Condition based on if the player is safely facing the current target.
    /// </summary>
    [ItemCondition(FriendlyName = "I am Facing My Target")]
    public class MeIsFacingTargetCondition : ICondition
    {
        public bool Satisfied()
        {
            if (StyxWoW.Me.CurrentTarget == null || !StyxWoW.Me.CurrentTarget.IsValid)
                throw new ConditionException("Target cannot be null or invalid.");

            return StyxWoW.Me.IsFacing(StyxWoW.Me.CurrentTarget);
        }
    }
}