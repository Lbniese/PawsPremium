using Paws.Core.Conditions;
using Styx.WoWInternals;

namespace Paws.Core.Abilities.Feral
{
    /// <summary>
    ///     <para>Rejuvenation</para>
    ///     <para>9.45% of base mana, 40 yd range</para>
    ///     <para>Instant</para>
    ///     <para>Requires Druid</para>
    ///     <para>Requires level 4</para>
    ///     <para>Heals the target for (228% of Spell power) over 12 seconds.</para>
    ///     <para>http://www.wowhead.com/spell=774/rejuvenation</para>
    /// </summary>
    public class RejuvenateMyAllyAbility : AbilityBase
    {
        public RejuvenateMyAllyAbility()
            : base(WoWSpell.FromId(SpellBook.Rejuvenation))
        {
            Category = AbilityCategory.Heal;

            RequiredConditions.Add(new TargetDoesNotHaveAuraCondition(TargetType.Me, SpellBook.Prowl));
        }

        public override void ApplyDefaultSettings()
        {
            base.ApplyDefaultSettings();

            Conditions.Add(new BooleanCondition(Settings.HealMyAlliesEnabled));
            Conditions.Add(new BooleanCondition(Settings.HealMyAlliesWithRejuvenationEnabled));
            Conditions.Add(new MeIsNotInTravelFormCondition());
            Conditions.Add(new TargetHealthRangeCondition(TargetType.MyCurrentTarget, 5.0,
                Settings.HealMyAlliesWithRejuvenationMinHealth));
            if (Settings.HealMyAlliesMyHealthCheckEnabled)
            {
                Conditions.Add(new TargetHealthRangeCondition(TargetType.Me, Settings.HealMyAlliesMyMinHealth, 100.0));
            }
        }
    }
}