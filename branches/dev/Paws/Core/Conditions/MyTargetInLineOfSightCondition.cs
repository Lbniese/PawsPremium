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
            if (Main.MyCurrentTarget == null || !Main.MyCurrentTarget.IsValid)
                return false;

            return StyxWoW.Me.CurrentTarget.InLineOfSight;
        }
    }
}
