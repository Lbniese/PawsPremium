using Paws.Core.Conditions;
using Styx.WoWInternals;

namespace Paws.Core.Abilities.Shared
{
    /// <summary>
    /// <para>Stampeding Roar</para>
    /// <para>Instant, 2 min cooldown</para>
    /// <para>Requires Druid</para>
    /// <para>Requires level 84</para>
    /// <para>The Druid roars, increasing the movement speed of all friendly players within 10 yards by</para>
    /// <para>60% for 8 sec and removing all roots and snares on those targets.</para>
    /// <para>Using this ability outside of Bear Form or Cat Form activates Bear Form.</para>
    /// <para>http://www.wowhead.com/spell=106898/stampeding-roar</para>
    /// </summary>
    public class StampedingRoarAbility : AbilityBase
    {
        public StampedingRoarAbility()
            : base(WoWSpell.FromId(SpellBook.StampedingRoar), true, true)
        {
            base.Conditions.Add(new MeHasAttackableTargetCondition());
            base.Conditions.Add(new ConditionOrList(
                new ConditionTestSwitchCondition(
                    new MyExpectedSpecializationCondition(Styx.WoWSpec.DruidFeral),
                    new ConditionDependencyList(
                        new BooleanCondition(Settings.StampedingRoarEnabled),
                        new MeIsInCatFormCondition(),
                        new MyTargetDistanceCondition(Settings.StampedingRoarMinDistance, Settings.StampedingRoarMaxDistance)
                    ),
                    false
                ),
                new ConditionTestSwitchCondition(
                    new MyExpectedSpecializationCondition(Styx.WoWSpec.DruidGuardian),
                    new ConditionDependencyList(
                        new BooleanCondition(Settings.GuardianStampedingRoarEnabled),
                        new MyTargetDistanceCondition(Settings.GuardianStampedingRoarMinDistance, Settings.GuardianStampedingRoarMaxDistance)
                    ),
                    false
                )
            ));
            base.Conditions.Add(new TargetDoesNotHaveAuraCondition(TargetType.Me, SpellBook.StampedingRoar));
            base.Conditions.Add(new TargetDoesNotHaveAuraCondition(TargetType.Me, SpellBook.Dash));
        }
    }
}
