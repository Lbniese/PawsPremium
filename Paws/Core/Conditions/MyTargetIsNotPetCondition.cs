using Styx;

namespace Paws.Core.Conditions
{
    /// <summary>
    ///     Condition based on if the player's current target is not a pet.
    /// </summary>
    public class MyTargetIsNotPetCondition : ICondition
    {
        public bool Satisfied()
        {
            if (StyxWoW.Me.CurrentTarget == null || !StyxWoW.Me.CurrentTarget.IsValid)
                throw new ConditionException("Current target cannot be null or invalid.");

            return !StyxWoW.Me.CurrentTarget.IsPet;
        }
    }
}