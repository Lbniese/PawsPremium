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
    public class SurvivalInstinctsAbility : AbilityBase
    {
        public SurvivalInstinctsAbility()
            : base(WoWSpell.FromId(SpellBook.SurvivalInstincts), true, true)
        {
            base.Category = AbilityCategory.Defensive;

            base.Conditions.Add(new BooleanCondition(Settings.GuardianSurvivalInstinctsEnabled));
            base.Conditions.Add(new MeIsInBearFormCondition());
            base.Conditions.Add(new MeIsInCombatCondition());
            base.Conditions.Add(new TargetHealthRangeCondition(TargetType.Me, 0, Settings.GuardianSurvivalInstinctsMinHealth));
            base.Conditions.Add(new TargetDoesNotHaveAuraCondition(TargetType.Me, SpellBook.SurvivalInstincts));
        }
    }
}
