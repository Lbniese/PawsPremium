using Paws.Core.Conditions.Attributes;
using Styx;

namespace Paws.Core.Conditions
{
    /// <summary>
    /// Condition based on if the player is in Cat Form.
    /// </summary>
    [ItemCondition(FriendlyName = "I am in Cat Form")]
    public class MeIsInCatFormCondition : ICondition
    {
        public bool Satisfied()
        {
            return StyxWoW.Me.IsInCatForm();
        }
    }
}
