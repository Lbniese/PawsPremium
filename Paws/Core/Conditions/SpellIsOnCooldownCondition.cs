using Paws.Core.Managers;
using Styx.WoWInternals;

namespace Paws.Core.Conditions
{
    /// <summary>
    ///     Condition based on if the player's spell is on cooldown.
    /// </summary>
    public class SpellIsOnCooldownCondition : ICondition
    {
        public SpellIsOnCooldownCondition(WoWSpell spell)
        {
            Spell = spell;
        }

        public SpellIsOnCooldownCondition(int spellId)
            : this(WoWSpell.FromId(spellId))
        {
        }

        /// <summary>
        ///     The spell used to determine if the cooldown is present to satisfy the condition.
        /// </summary>
        public WoWSpell Spell { get; set; }

        public bool Satisfied()
        {
            return CastManager.SpellIsOnCooldown(Spell.Id);
        }
    }
}