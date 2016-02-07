using Paws.Core.Managers;
using Styx.WoWInternals;

namespace Paws.Core.Conditions
{
    /// <summary>
    ///     Condition based on if the player's specified spell is not on cooldown.
    /// </summary>
    public class SpellIsNotOnCooldownCondition : ICondition
    {
        public SpellIsNotOnCooldownCondition(WoWSpell spell)
        {
            Spell = spell;
        }

        /// <summary>
        ///     The spell used to determine if the cooldown is not present to satisfy the condition.
        /// </summary>
        public WoWSpell Spell { get; set; }

        public bool Satisfied()
        {
            return !CastManager.SpellIsOnCooldown(Spell.Id);
        }
    }
}