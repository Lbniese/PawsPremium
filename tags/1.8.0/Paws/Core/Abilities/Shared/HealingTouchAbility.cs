using Paws.Core.Conditions;
using Styx.WoWInternals;

namespace Paws.Core.Abilities.Shared
{
    /// <summary>
    /// <para>Healing Touch</para>
    /// <para>10.35% of base mana, 40 yd range</para>
    /// <para>2.5 second cast</para>
    /// <para>Requires Druid</para>
    /// <para>Requires level 26</para>
    /// <para>Heals a friendsly target for (360% of Spell power).</para>
    /// <para>http://www.wowhead.com/spell=5185/healing-touch</para>
    /// </summary>
    public class HealingTouchAbility : AbilityBase
    {
        public HealingTouchAbility()
            : base(WoWSpell.FromId(SpellBook.HealingTouch))
        {
            base.Category = AbilityCategory.Heal;
        }

        public override void ApplyDefaultSettings()
        {
            base.ApplyDefaultSettings();

            base.Conditions.Add(new ConditionOrList(
                new ConditionTestSwitchCondition(
                    new MyExpectedSpecializationCondition(Styx.WoWSpec.DruidFeral),
                    new ConditionDependencyList(
                        new BooleanCondition(Settings.HealingTouchEnabled),
                        new TargetHealthRangeCondition(TargetType.Me, 0.0, Settings.HealingTouchMinHealth),
                        new ConditionTestSwitchCondition(
                            new BooleanCondition(Settings.HealingTouchOnlyDuringPredatorySwiftness),
                            new TargetHasAuraCondition(TargetType.Me, SpellBook.PredatorySwiftnessProc)
                        )
                    ),
                    false
                ),
                new ConditionTestSwitchCondition(
                    new MyExpectedSpecializationCondition(Styx.WoWSpec.DruidGuardian),
                    new ConditionDependencyList(
                        new BooleanCondition(Settings.GuardianHealingTouchEnabled),
                        new TargetHealthRangeCondition(TargetType.Me, 0.0, Settings.GuardianHealingTouchMinHealth),
                        new ConditionTestSwitchCondition(
                            new BooleanCondition(Settings.GuardianHealingTouchOnlyDuringDreamOfCenarius),
                            new TargetHasAuraCondition(TargetType.Me, SpellBook.DreamOfCenariusProc)
                        )
                    ),
                    false
                )
            ));

            base.Conditions.Add(new TargetDoesNotHaveAuraCondition(TargetType.Me, SpellBook.Prowl));
        }
    }
}