using Styx.CommonBot;
using System;

namespace Paws.Core.Conditions
{
    /// <summary>
    /// Condition based on if the specified spell cooldown meets the minimum time requirement provided.
    /// </summary>
    public class MySpellCooldownTimeLeft : ICondition
    {
        /// <summary>
        /// The spell id used to satisfy the condition.
        /// </summary>
        public int SpellId { get; set; }

        /// <summary>
        /// The minimum amount of time left for the spell cooldown to satisfy the condition.
        /// </summary>
        public TimeSpan MinTimeLeft { get; set; }

        public MySpellCooldownTimeLeft(int spellId, TimeSpan minTimeLeft)
        {
            this.SpellId = spellId;
            this.MinTimeLeft = minTimeLeft;
        }

        public bool Satisfied()
        {
            SpellFindResults spellFindResults;

            if (SpellManager.FindSpell(this.SpellId, out spellFindResults))
            {
                if (spellFindResults.Override != null)
                {
                    return spellFindResults.Override.CooldownTimeLeft >= this.MinTimeLeft;
                }
                else
                {
                    return spellFindResults.Original.CooldownTimeLeft >= this.MinTimeLeft;
                }
            }
            else
            {
                throw new ConditionException("Unable to find the specified spell.");
            }
        }
    }
}
