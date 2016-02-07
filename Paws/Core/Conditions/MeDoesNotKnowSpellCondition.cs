using Styx;
using Styx.WoWInternals;

namespace Paws.Core.Conditions
{
    /// <summary>
    ///     Condition based on if the player does not know the specified spell.
    /// </summary>
    public class MeDoesNotKnowSpellCondition : ICondition
    {
        public MeDoesNotKnowSpellCondition(int spellId)
            : this(WoWSpell.FromId(spellId))
        {
        }

        public MeDoesNotKnowSpellCondition(WoWSpell spell)
        {
            Spell = spell;
        }

        /// <summary>
        ///     The spell to check to satisfy the condition.
        /// </summary>
        public WoWSpell Spell { get; set; }

        public bool Satisfied()
        {
            if (Spell == null)
                throw new ConditionException("Spell cannot be null.");

            return !StyxWoW.Me.KnowsSpell(Spell.Id);
        }
    }
}