using Styx;

namespace Paws.Core.Conditions
{
    /// <summary>
    ///     Condition based on if the player is not flying.
    /// </summary>
    public class MeIsNotFlyingCondition : ICondition
    {
        public bool Satisfied()
        {
            return !StyxWoW.Me.IsFlying;
        }
    }
}