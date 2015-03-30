using Styx;

namespace Paws.Core.Conditions
{
    /// <summary>
    /// Condition based on if the player is not in travel form.
    /// </summary>
    public class MeIsNotInTravelFormCondition : ICondition
    {
        public bool Satisfied()
        {
            return !StyxWoW.Me.IsInTravelForm();
        }
    }
}
