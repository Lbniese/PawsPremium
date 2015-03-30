using Paws.Core.Conditions.Attributes;
using Styx;

namespace Paws.Core.Conditions
{
    /// <summary>
    /// Condition based on if the player is not in Bear Form.
    /// </summary>
    [ItemCondition(FriendlyName = "I am Not in Bear Form")]
    public class MeIsNotInBearFormCondition : ICondition
    {
        public bool Satisfied()
        {
            return
                StyxWoW.Me.Shapeshift != ShapeshiftForm.Bear &&
                StyxWoW.Me.Shapeshift != ShapeshiftForm.CreatureBear &&
                StyxWoW.Me.Shapeshift != ShapeshiftForm.DireBear;
        }
    }
}
