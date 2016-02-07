using Paws.Core.Abilities.Attributes;
using Paws.Core.Conditions;
using Styx;
using Styx.WoWInternals;

namespace Paws.Core.Abilities.Shared
{
    /// <summary>
    ///     <para>Displacer Beast</para>
    ///     <para>Instant, 30 second cooldown</para>
    ///     <para>Requires Druid</para>
    ///     <para>Requires level 15</para>
    ///     <para>Teleports the Druid up to 20 yards forward, activates Cat Form, and increases movement</para>
    ///     <para>speed by 50% for 4 seconds.</para>
    ///     <para>http://www.wowhead.com/spell=102280/displacer-beast</para>
    /// </summary>
    [AbilityChain(FriendlyName = "Displacer Beast")]
    public class DisplacerBeastAbility : AbilityBase
    {
        public DisplacerBeastAbility()
            : base(WoWSpell.FromId(SpellBook.DisplacerBeast), true, true)
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
                        new BooleanCondition(Settings.DisplacerBeastEnabled),
                        new MyTargetDistanceCondition(Settings.DisplacerBeastMinDistance,
                            Settings.DisplacerBeastMaxDistance)
                        ),
                    false
                    ),
                new ConditionTestSwitchCondition(
                    new MyExpectedSpecializationCondition(WoWSpec.DruidGuardian),
                    new ConditionDependencyList(
                        new BooleanCondition(Settings.GuardianDisplacerBeastEnabled),
                        new MyTargetDistanceCondition(Settings.GuardianDisplacerBeastMinDistance,
                            Settings.GuardianDisplacerBeastMaxDistance)
                        ),
                    false
                    )
                ));
            Conditions.Add(new MeIsFacingTargetCondition());
            Conditions.Add(new MyTargetInLineOfSightCondition());
        }
    }
}