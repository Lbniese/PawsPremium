using Paws.Core.Conditions.Attributes;
using Styx;

namespace Paws.Core.Conditions
{
    /// <summary>
    /// Condition based on if the player is not in Cat Form.
    /// </summary>
    [ItemCondition(FriendlyName = "I am Not in Cat Form")]
    public class MeIsNotInCatFormCondition : ICondition
    {
        public bool Satisfied()
        {
            return !StyxWoW.Me.IsInCatForm();
        }
    }
}
