using Styx.WoWInternals;

namespace Paws.Core.Conditions
{
    /// <summary>
    /// Condition based on if the player's spell is on cooldown.
    /// </summary>
    public class SpellIsOnCooldownCondition : ICondition
    {
        /// <summary>
        /// The spell used to determine if the cooldown is present to satisfy the condition.
        /// </summary>
        public WoWSpell Spell { get; set; }

        public SpellIsOnCooldownCondition(WoWSpell spell)
        {
            this.Spell = spell;
        }

        public SpellIsOnCooldownCondition(int spellId)
            : this(WoWSpell.FromId(spellId))
        { }

        public bool Satisfied()
        {
            return CastManager.SpellIsOnCooldown(Spell.Id);
        }
    }
}
