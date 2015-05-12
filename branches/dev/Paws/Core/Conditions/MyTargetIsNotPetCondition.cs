using Styx;

namespace Paws.Core.Conditions
{
    /// <summary>
    /// Condition based on if the player's current target is not a pet.
    /// </summary>
    public class MyTargetIsNotPetCondition : ICondition
    {
        public bool Satisfied()
        {
            if (Main.MyCurrentTarget == null || !Main.MyCurrentTarget.IsValid)
                return false;

            return !StyxWoW.Me.CurrentTarget.IsPet;
        }
    }
}
