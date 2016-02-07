using Paws.Core.Abilities.Attributes;
using Paws.Core.Conditions;
using Styx.WoWInternals;

namespace Paws.Core.Abilities.Feral
{
    /// <summary>
    ///     <para>Heart of the Wild, Talent</para>
    ///     <para>Instant, 6 min cooldown</para>
    ///     <para>Requires level 90</para>
    ///     <para>When activated, dramatically imporves the Druid's ability to tank, cast spells, and heal for 45 seconds.</para>
    ///     <para>http://www.wowhead.com/spell=108292/heart-of-the-wild</para>
    /// </summary>
    [AbilityChain(FriendlyName = "Heart of the Wild")]
    public class HeartOfTheWildAbility : AbilityBase
    {
        public HeartOfTheWildAbility()
            : base(WoWSpell.FromId(SpellBook.HeartOfTheWild), true, true)
        {
            Category = AbilityCategory.Defensive;

            RequiredConditions.Add(new MeHasAttackableTargetCondition());
            RequiredConditions.Add(new TargetDoesNotHaveAuraCondition(TargetType.Me, Spell.Id));
            RequiredConditions.Add(new TargetDoesNotHaveAuraCondition(TargetType.Me, SpellBook.Prowl));
        }

        public override void ApplyDefaultSettings()
        {
            base.ApplyDefaultSettings();

            Conditions.Add(new BooleanCondition(Settings.HeartOfTheWildEnabled));
            Conditions.Add(new TargetHealthRangeCondition(TargetType.Me, 0, Settings.HeartOfTheWildMinHealth));
        }
    }
}