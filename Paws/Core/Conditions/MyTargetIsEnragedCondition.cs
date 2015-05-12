using Styx;
using Paws.Core.Conditions.Attributes;

namespace Paws.Core.Conditions
{
    /// <summary>
    /// Condition based on if the player's current target is enraged.
    /// </summary>
    [ItemCondition(FriendlyName = "My Target is Enraged")]
    public class MyTargetIsEnragedCondition : ICondition
    {
        public bool Satisfied()
        {
            if (Main.MyCurrentTarget == null || !Main.MyCurrentTarget.IsValid)
                return false;

            return StyxWoW.Me.CurrentTarget.HasCancelableEnragedEffect();
        }
    }
}
