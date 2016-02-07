using System;
using Styx.CommonBot;

namespace Paws.Core.Conditions
{
    /// <summary>
    ///     Condition based on if the specified spell cooldown meets the minimum time requirement provided.
    /// </summary>
    public class MySpellCooldownTimeLeft : ICondition
    {
        public MySpellCooldownTimeLeft(int spellId, TimeSpan minTimeLeft)
        {
            SpellId = spellId;
            MinTimeLeft = minTimeLeft;
        }

        /// <summary>
        ///     The spell id used to satisfy the condition.
        /// </summary>
        public int SpellId { get; set; }

        /// <summary>
        ///     The minimum amount of time left for the spell cooldown to satisfy the condition.
        /// </summary>
        public TimeSpan MinTimeLeft { get; set; }

        public bool Satisfied()
        {
            SpellFindResults spellFindResults;

            if (SpellManager.FindSpell(SpellId, out spellFindResults))
            {
                if (spellFindResults.Override != null)
                {
                    return spellFindResults.Override.CooldownTimeLeft >= MinTimeLeft;
                }
                return spellFindResults.Original.CooldownTimeLeft >= MinTimeLeft;
            }
            throw new ConditionException("Unable to find the specified spell.");
        }
    }
}