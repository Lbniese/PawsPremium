using Styx;
using Styx.WoWInternals;

namespace Paws.Core.Conditions
{
    /// <summary>
    /// Condition based on if the player does not know the specified spell.
    /// </summary>
    public class MeDoesNotKnowSpellCondition : ICondition
    {
        /// <summary>
        /// The spell to check to satisfy the condition.
        /// </summary>
        public WoWSpell Spell { get; set; }

        public MeDoesNotKnowSpellCondition(int spellId)
            : this(WoWSpell.FromId(spellId))
        { }

        public MeDoesNotKnowSpellCondition(WoWSpell spell)
        {
            this.Spell = spell;
        }

        public bool Satisfied()
        {
            if (this.Spell == null)
                throw new ConditionException("Spell cannot be null.");

            return !StyxWoW.Me.KnowsSpell(this.Spell.Id);
        }
    }
}
