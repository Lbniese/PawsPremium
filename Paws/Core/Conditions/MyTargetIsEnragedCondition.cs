using Paws.Core.Conditions.Attributes;
using Styx;

namespace Paws.Core.Conditions
{
    /// <summary>
    ///     Condition based on if the player's current target is enraged.
    /// </summary>
    [ItemCondition(FriendlyName = "My Target is Enraged")]
    public class MyTargetIsEnragedCondition : ICondition
    {
        public bool Satisfied()
        {
            if (StyxWoW.Me.CurrentTarget == null || !StyxWoW.Me.CurrentTarget.IsValid)
                throw new ConditionException("Current target cannot be null or invalid.");

            return StyxWoW.Me.CurrentTarget.HasCancelableEnragedEffect();
        }
    }
}