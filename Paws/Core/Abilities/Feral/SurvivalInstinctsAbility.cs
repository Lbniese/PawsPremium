using Paws.Core.Abilities.Attributes;
using Paws.Core.Conditions;
using Styx.WoWInternals;

namespace Paws.Core.Abilities.Feral
{
    /// <summary>
    ///     <para>Survival Instincts</para>
    ///     <para>Instant, 2 min charge</para>
    ///     <para>2 Charges</para>
    ///     <para>Requires Druid (Feral, Guardian)</para>
    ///     <para>Requires level 56</para>
    ///     <para>Reduces all damage taken by 50% for 6 sec. Max 2 charges.</para>
    ///     <para>http://www.wowhead.com/spell=61336/survival-instincts</para>
    /// </summary>
    [AbilityChain(FriendlyName = "Survival Instincts")]
    public class SurvivalInstinctsAbility : AbilityBase
    {
        public SurvivalInstinctsAbility()
            : base(WoWSpell.FromId(SpellBook.SurvivalInstincts), true, true)
        {
            Category = AbilityCategory.Defensive;

            RequiredConditions.Add(new MeIsInCatFormCondition());
            RequiredConditions.Add(new TargetDoesNotHaveAuraCondition(TargetType.Me, SpellBook.Prowl));
        }

        public override void ApplyDefaultSettings()
        {
            base.ApplyDefaultSettings();

            Conditions.Add(new BooleanCondition(Settings.SurvivalInstinctsEnabled));
            Conditions.Add(new MeIsInCombatCondition());
            Conditions.Add(new TargetHealthRangeCondition(TargetType.Me, 0, Settings.SurvivalInstinctsMinHealth));
            Conditions.Add(new TargetDoesNotHaveAuraCondition(TargetType.Me, SpellBook.SurvivalInstincts));
        }
    }
}