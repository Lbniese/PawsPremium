using Paws.Core.Conditions;
using Styx.WoWInternals;

namespace Paws.Core.Abilities.Guardian
{
    /// <summary>
    /// <para>Survival Instincts</para>
    /// <para>Instant, 2 min charge</para>
    /// <para>2 Charges</para>
    /// <para>Requires Druid (Feral, Guardian)</para>
    /// <para>Requires level 56</para>
    /// <para>Reduces all damage taken by 50% for 6 sec. Max 2 charges.</para>
    /// <para>http://www.wowhead.com/spell=61336/survival-instincts</para>
    /// </summary>
    public class BarkskinAbility : AbilityBase
    {
        public BarkskinAbility()
            : base(WoWSpell.FromId(SpellBook.Barkskin), false, true)
        {
            base.Category = AbilityCategory.Defensive;

            base.Conditions.Add(new BooleanCondition(Settings.BarkskinEnabled));
            base.Conditions.Add(new MeIsInBearFormCondition());
            base.Conditions.Add(new MeIsInCombatCondition());
            base.Conditions.Add(new TargetHealthRangeCondition(TargetType.Me, 0, Settings.BarkskinMinHealth));
            base.Conditions.Add(new TargetDoesNotHaveAuraCondition(TargetType.Me, SpellBook.Barkskin));
        }
    }
}
