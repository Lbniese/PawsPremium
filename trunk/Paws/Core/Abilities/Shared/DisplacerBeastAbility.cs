using Paws.Core.Conditions;
using Styx.WoWInternals;

namespace Paws.Core.Abilities.Shared
{
    /// <summary>
    /// <para>Displacer Beast</para>
    /// <para>Instant, 30 second cooldown</para>
    /// <para>Requires Druid</para>
    /// <para>Requires level 15</para>
    /// <para>Teleports the Druid up to 20 yards forward, activates Cat Form, and increases movement</para>
    /// <para>speed by 50% for 4 seconds.</para>
    /// <para>http://www.wowhead.com/spell=102280/displacer-beast</para>
    /// </summary>
    public class DisplacerBeastAbility : AbilityBase
    {
        public DisplacerBeastAbility()
            : base(WoWSpell.FromId(SpellBook.DisplacerBeast), true, true)
        {
            base.Conditions.Add(new MeHasAttackableTargetCondition());
            base.Conditions.Add(new ConditionOrList(
                new ConditionTestSwitchCondition(
                    new MyExpectedSpecializationCondition(Styx.WoWSpec.DruidFeral),
                    new ConditionDependencyList(
                        new BooleanCondition(Settings.DisplacerBeastEnabled),
                        new MyTargetDistanceCondition(Settings.DisplacerBeastMinDistance, Settings.DisplacerBeastMaxDistance)
                    ),
                    false
                ),
                new ConditionTestSwitchCondition(
                    new MyExpectedSpecializationCondition(Styx.WoWSpec.DruidGuardian),
                    new ConditionDependencyList(
                        new BooleanCondition(Settings.GuardianDisplacerBeastEnabled),
                        new MyTargetDistanceCondition(Settings.GuardianDisplacerBeastMinDistance, Settings.GuardianDisplacerBeastMaxDistance)
                    ),
                    false
                )
            ));
            base.Conditions.Add(new MeIsFacingTargetCondition());
            base.Conditions.Add(new MyTargetInLineOfSightCondition());
        }
    }
}
