using Styx;
using Styx.WoWInternals;

namespace Paws.Core.Conditions
{
    /// <summary>
    /// Condition based on if the player knows the specified spell.
    /// </summary>
    public class MeKnowsSpellCondition : ICondition
    {
        /// <summary>
        /// The spell used to satisfy the condition.
        /// </summary>
        public WoWSpell Spell { get; set; }

        public MeKnowsSpellCondition(int spellId)
            : this(WoWSpell.FromId(spellId))
        { }

        public MeKnowsSpellCondition(WoWSpell spell)
        {
            this.Spell = spell;
        }

        public bool Satisfied()
        {
            if (this.Spell == null)
                throw new ConditionException("Spell cannot be null.");

            return StyxWoW.Me.KnowsSpell(this.Spell.Id);
        }
    }
}
