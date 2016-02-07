using Styx;

namespace Paws.Core.Conditions
{
    /// <summary>
    ///     Condition based on if the player has Savage Roar aura active or not.
    /// </summary>
    public class MySavageRoarAuraCondition : ICondition
    {
        public MySavageRoarAuraCondition(bool isActive = true)
        {
            IsActive = isActive;
        }

        /// <summary>
        ///     The value used to determine if Savage Roar should be active or not to satisfy the condition.
        /// </summary>
        public bool IsActive { get; set; }

        public bool Satisfied()
        {
            return IsActive ? StyxWoW.Me.HasSavageRoarAura() : !StyxWoW.Me.HasSavageRoarAura();
        }
    }
}