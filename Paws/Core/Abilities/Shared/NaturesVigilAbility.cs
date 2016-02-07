using Paws.Core.Abilities.Attributes;
using Paws.Core.Conditions;
using Styx;
using Styx.WoWInternals;

namespace Paws.Core.Abilities.Shared
{
    /// <summary>
    ///     <para>Nature's Vigil</para>
    ///     <para>Instant, 1.5 min cooldown</para>
    ///     <para>Requires Druid</para>
    ///     <para>Requires level 90</para>
    ///     <para>While active, all single-target healing and damage spells and abilities also heal a nearby</para>
    ///     <para>friendly target based on the amount done, 30% for heals, 40% for damage spells.</para>
    ///     <para>http://www.wowhead.com/spell=124974/natures-vigil</para>
    /// </summary>
    [AbilityChain(FriendlyName = "Nature's Vigil")]
    public class NaturesVigilAbility : AbilityBase
    {
        public NaturesVigilAbility()
            : base(WoWSpell.FromId(SpellBook.NaturesVigil), true, true)
        {
            Category = AbilityCategory.Buff;
        }

        public override void ApplyDefaultSettings()
        {
            base.ApplyDefaultSettings();

            Conditions.Add(new MeHasAttackableTargetCondition());
            Conditions.Add(new ConditionOrList(
                new ConditionTestSwitchCondition(
                    new MyExpectedSpecializationCondition(WoWSpec.DruidFeral),
                    new ConditionDependencyList(
                        new BooleanCondition(Settings.NaturesVigilEnabled),
                        new TargetHealthRangeCondition(TargetType.Me, 0, Settings.NaturesVigilMinHealth)
                        ),
                    false
                    ),
                new ConditionTestSwitchCondition(
                    new MyExpectedSpecializationCondition(WoWSpec.DruidGuardian),
                    new ConditionDependencyList(
                        new BooleanCondition(Settings.GuardianNaturesVigilEnabled),
                        new TargetHealthRangeCondition(TargetType.Me, 0, Settings.GuardianNaturesVigilMinHealth)
                        ),
                    false
                    )
                ));
            Conditions.Add(new TargetDoesNotHaveAuraCondition(TargetType.Me, Spell.Id));
            Conditions.Add(new TargetDoesNotHaveAuraCondition(TargetType.Me, SpellBook.Prowl));
        }
    }
}