using Paws.Core.Conditions;
using Paws.Core.Abilities.Attributes;
using Styx.WoWInternals;

namespace Paws.Core.Abilities.Shared
{
    /// <summary>
    /// <para>Dash</para>
    /// <para>Instant, 3 min cooldown</para>
    /// <para>Requires Druid</para>
    /// <para>Requires level 24</para>
    /// <para>Activates Cat Form, removes all roots and snares, and increases movement speed by 70% while in Cat Form</para>
    /// <para>for 15 seconds.</para>
    /// <para>http://www.wowhead.com/spell=1850/dash</para>
    /// </summary>
    [AbilityChain(FriendlyName = "Dash")]
    public class DashAbility : AbilityBase
    {
        public DashAbility()
            : base(WoWSpell.FromId(SpellBook.Dash), true, true)
        {
            base.Category = AbilityCategory.Buff;
        }

        public override void ApplyDefaultSettings()
        {
            base.ApplyDefaultSettings();

            base.Conditions.Add(new MeHasAttackableTargetCondition());
            base.Conditions.Add(new ConditionOrList(
                new ConditionTestSwitchCondition(
                    new MyExpectedSpecializationCondition(Styx.WoWSpec.DruidFeral),
                    new ConditionDependencyList(
                        new BooleanCondition(Settings.DashEnabled),
                        new MyTargetDistanceCondition(Settings.DashMinDistance, Settings.DashMaxDistance)
                    ),
                    false
                ),
                new ConditionTestSwitchCondition(
                    new MyExpectedSpecializationCondition(Styx.WoWSpec.DruidGuardian),
                    new ConditionDependencyList(
                        new BooleanCondition(Settings.GuardianDashEnabled),
                        new MyTargetDistanceCondition(Settings.GuardianDashMinDistance, Settings.GuardianDashMaxDistance)
                    ),
                    false
                )
            ));
            base.Conditions.Add(new TargetDoesNotHaveAuraCondition(TargetType.Me, SpellBook.Dash));
            base.Conditions.Add(new TargetDoesNotHaveAuraCondition(TargetType.Me, SpellBook.StampedingRoar));
        }
    }
}
