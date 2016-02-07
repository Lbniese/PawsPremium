using Paws.Core.Abilities.Attributes;
using Paws.Core.Conditions;
using Styx;
using Styx.WoWInternals;

namespace Paws.Core.Abilities.Shared
{
    /// <summary>
    ///     <para>Renewal</para>
    ///     <para>Instant, 2 min cooldown</para>
    ///     <para>Requires Druid</para>
    ///     <para>Requires level 30</para>
    ///     <para>Instantly heals the Druid for 22% of maximum health. Usable in all shapeshift forms.</para>
    ///     <para>http://www.wowhead.com/spell=108238/renewal</para>
    /// </summary>
    [AbilityChain(FriendlyName = "Renewal")]
    public class RenewalAbility : AbilityBase
    {
        public RenewalAbility()
            : base(WoWSpell.FromId(SpellBook.Renewal), true, true)
        {
            Category = AbilityCategory.Heal;
        }

        public override void ApplyDefaultSettings()
        {
            base.ApplyDefaultSettings();

            Conditions.Add(new ConditionOrList(
                new ConditionTestSwitchCondition(
                    new MyExpectedSpecializationCondition(WoWSpec.DruidFeral),
                    new ConditionDependencyList(
                        new BooleanCondition(Settings.RenewalEnabled),
                        new TargetHealthRangeCondition(TargetType.Me, 0, Settings.RenewalMinHealth)
                        ),
                    false
                    ),
                new ConditionTestSwitchCondition(
                    new MyExpectedSpecializationCondition(WoWSpec.DruidGuardian),
                    new ConditionDependencyList(
                        new BooleanCondition(Settings.GuardianRenewalEnabled),
                        new TargetHealthRangeCondition(TargetType.Me, 0, Settings.GuardianRenewalMinHealth)
                        ),
                    false
                    )
                ));
            Conditions.Add(new TargetDoesNotHaveAuraCondition(TargetType.Me, SpellBook.Prowl));
        }
    }
}