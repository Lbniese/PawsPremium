using Styx.CommonBot;

namespace Paws.Core.Conditions
{
    /// <summary>
    ///     Condition based on if the GCD is not on cooldown.
    /// </summary>
    public class IsOffGlobalCooldownCondition : ICondition
    {
        public bool Satisfied()
        {
            return !SpellManager.GlobalCooldown;
        }
    }
}