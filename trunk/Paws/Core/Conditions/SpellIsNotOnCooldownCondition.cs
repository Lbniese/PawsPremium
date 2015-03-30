using Styx.WoWInternals;

namespace Paws.Core.Conditions
{
    /// <summary>
    /// Condition based on if the player's specified spell is not on cooldown.
    /// </summary>
    public class SpellIsNotOnCooldownCondition : ICondition
    {
        /// <summary>
        /// The spell used to determine if the cooldown is not present to satisfy the condition.
        /// </summary>
        public WoWSpell Spell { get; set; }

        public SpellIsNotOnCooldownCondition(WoWSpell spell)
        {
            this.Spell = spell;
        }

        public bool Satisfied()
        {
            return !CastManager.SpellIsOnCooldown(Spell.Id);
        }
    }
}
