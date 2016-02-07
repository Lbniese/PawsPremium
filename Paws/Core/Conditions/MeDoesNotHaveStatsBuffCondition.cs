using Styx;

namespace Paws.Core.Conditions
{
    /// <summary>
    ///     Condition based on if the player does not have stats buff.
    /// </summary>
    public class MeDoesNotHaveStatsBuffCondition : ICondition
    {
        public bool Satisfied()
        {
            return !StyxWoW.Me.HasStatsBuff();
        }
    }
}