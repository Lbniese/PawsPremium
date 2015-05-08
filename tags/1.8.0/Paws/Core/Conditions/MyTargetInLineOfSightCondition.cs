using Paws.Core.Conditions.Attributes;
using Styx;

namespace Paws.Core.Conditions
{
    /// <summary>
    /// Condition based on if the player's current target is in the line of sight.
    /// </summary>
    [ItemCondition(FriendlyName = "My Target is in Line of Sight")]
    public class MyTargetInLineOfSightCondition : ICondition
    {
        public bool Satisfied()
        {
            if (StyxWoW.Me.CurrentTarget == null || !StyxWoW.Me.CurrentTarget.IsValid)
                throw new ConditionException("Current target cannot be null or invalid.");

            return StyxWoW.Me.CurrentTarget.InLineOfSight;
        }
    }
}
