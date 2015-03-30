using Styx;
using Paws.Core.Conditions.Attributes;

namespace Paws.Core.Conditions
{
    /// <summary>
    /// Condition based on if the player is in Bear form.
    /// </summary>
    [ItemCondition(FriendlyName = "I am in Bear Form")]
    public class MeIsInBearFormCondition : ICondition
    {
        public bool Satisfied()
        {
            return
                StyxWoW.Me.Shapeshift == ShapeshiftForm.Bear ||
                StyxWoW.Me.Shapeshift == ShapeshiftForm.CreatureBear ||
                StyxWoW.Me.Shapeshift == ShapeshiftForm.DireBear;
        }
    }
}
