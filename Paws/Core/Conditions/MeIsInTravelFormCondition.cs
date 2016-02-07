using Styx;

namespace Paws.Core.Conditions
{
    /// <summary>
    ///     Condition based on if the player is in travel form.
    /// </summary>
    public class MeIsInTravelFormCondition : ICondition
    {
        public bool Satisfied()
        {
            return StyxWoW.Me.IsInTravelForm();
        }
    }
}