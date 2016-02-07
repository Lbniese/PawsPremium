using Paws.Core.Conditions.Attributes;
using Styx;

namespace Paws.Core.Conditions
{
    /// <summary>
    ///     Condition based on if the player has an attackable target.
    /// </summary>
    [ItemCondition(FriendlyName = "I Have an Attackable Target")]
    public class MeHasAttackableTargetCondition : ICondition
    {
        public bool Satisfied()
        {
            return StyxWoW.Me.HasAttackableTarget();
        }
    }
}