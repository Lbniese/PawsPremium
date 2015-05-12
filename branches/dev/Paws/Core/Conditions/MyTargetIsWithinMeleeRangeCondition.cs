using Paws.Core.Conditions.Attributes;
using Styx;

namespace Paws.Core.Conditions
{
    /// <summary>
    /// Condition based on if the player's current target is within melee range.
    /// </summary>
    [ItemCondition(FriendlyName = "My Target is Within Melee Range")]
    public class MyTargetIsWithinMeleeRangeCondition : ICondition
    {
        public bool Satisfied()
        {
            if (Main.MyCurrentTarget == null || !Main.MyCurrentTarget.IsValid)
                return false;

            return StyxWoW.Me.CurrentTarget.IsWithinMeleeRange;
        }
    }
}
