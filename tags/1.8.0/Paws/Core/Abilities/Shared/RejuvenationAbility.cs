using Paws.Core.Conditions;
using Styx.WoWInternals;

namespace Paws.Core.Abilities.Shared
{
    /// <summary>
    /// <para>Rejuvenation</para>
    /// <para>9.45% of base mana, 40 yd range</para>
    /// <para>Instant</para>
    /// <para>Requires Druid</para>
    /// <para>Requires level 4</para>
    /// <para>Heals the target for (228% of Spell power) over 12 seconds.</para>
    /// <para>http://www.wowhead.com/spell=774/rejuvenation</para>
    /// </summary>
    public class RejuvenationAbility : AbilityBase
    {
        public RejuvenationAbility()
            : base(WoWSpell.FromId(SpellBook.Rejuvenation))
        {
            base.Category = AbilityCategory.Heal;
        }

        public override void ApplyDefaultSettings()
        {
            base.ApplyDefaultSettings();

            var feralOrNoneSpecConditions = new ConditionDependencyList(
                        new BooleanCondition(Settings.RejuvenationEnabled),
                        new TargetHealthRangeCondition(TargetType.Me, 0.0, Settings.RejuvenationMinHealth)
                    );

            base.Conditions.Add(new ConditionOrList(
                new ConditionTestSwitchCondition(
                    new MyExpectedSpecializationCondition(Styx.WoWSpec.None),
                    feralOrNoneSpecConditions,
                    false
                ),
                new ConditionTestSwitchCondition(
                    new MyExpectedSpecializationCondition(Styx.WoWSpec.DruidFeral),
                    feralOrNoneSpecConditions,
                    false
                ),
                new ConditionTestSwitchCondition(
                    new MyExpectedSpecializationCondition(Styx.WoWSpec.DruidGuardian),
                    new ConditionDependencyList(
                        new BooleanCondition(Settings.GuardianRejuvenationEnabled),
                        new TargetHealthRangeCondition(TargetType.Me, 0.0, Settings.GuardianRejuvenationMinHealth)
                    ),
                    false
                )
            ));

            base.Conditions.Add(new TargetDoesNotHaveAuraCondition(TargetType.Me, SpellBook.Rejuvenation));
            base.Conditions.Add(new TargetDoesNotHaveAuraCondition(TargetType.Me, SpellBook.Prowl));
        }
    }
}