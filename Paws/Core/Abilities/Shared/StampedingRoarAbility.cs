using Paws.Core.Abilities.Attributes;
using Paws.Core.Conditions;
using Styx;
using Styx.WoWInternals;

namespace Paws.Core.Abilities.Shared
{
    /// <summary>
    ///     <para>Stampeding Roar</para>
    ///     <para>Instant, 2 min cooldown</para>
    ///     <para>Requires Druid</para>
    ///     <para>Requires level 84</para>
    ///     <para>The Druid roars, increasing the movement speed of all friendly players within 10 yards by</para>
    ///     <para>60% for 8 sec and removing all roots and snares on those targets.</para>
    ///     <para>Using this ability outside of Bear Form or Cat Form activates Bear Form.</para>
    ///     <para>http://www.wowhead.com/spell=106898/stampeding-roar</para>
    /// </summary>
    [AbilityChain(FriendlyName = "Stampeding Roar")]
    public class StampedingRoarAbility : AbilityBase
    {
        public StampedingRoarAbility()
            : base(WoWSpell.FromId(SpellBook.StampedingRoar), true, true)
        {
        }

        public override void ApplyDefaultSettings()
        {
            base.ApplyDefaultSettings();

            Conditions.Add(new MeHasAttackableTargetCondition());
            Conditions.Add(new ConditionOrList(
                new ConditionTestSwitchCondition(
                    new MyExpectedSpecializationCondition(WoWSpec.DruidFeral),
                    new ConditionDependencyList(
                        new BooleanCondition(Settings.StampedingRoarEnabled),
                        new MeIsInCatFormCondition(),
                        new MyTargetDistanceCondition(Settings.StampedingRoarMinDistance,
                            Settings.StampedingRoarMaxDistance)
                        ),
                    false
                    ),
                new ConditionTestSwitchCondition(
                    new MyExpectedSpecializationCondition(WoWSpec.DruidGuardian),
                    new ConditionDependencyList(
                        new BooleanCondition(Settings.GuardianStampedingRoarEnabled),
                        new MyTargetDistanceCondition(Settings.GuardianStampedingRoarMinDistance,
                            Settings.GuardianStampedingRoarMaxDistance)
                        ),
                    false
                    )
                ));
            Conditions.Add(new TargetDoesNotHaveAuraCondition(TargetType.Me, SpellBook.StampedingRoar));
            Conditions.Add(new TargetDoesNotHaveAuraCondition(TargetType.Me, SpellBook.Dash));
        }
    }
}