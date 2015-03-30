using Paws.Core.Conditions;
using Styx.WoWInternals;

namespace Paws.Core.Abilities.Shared
{
    /// <summary>
    /// <para>Nature's Vigil</para>
    /// <para>Instant, 1.5 min cooldown</para>
    /// <para>Requires Druid</para>
    /// <para>Requires level 90</para>
    /// <para>While active, all single-target healing and damage spells and abilities also heal a nearby</para>
    /// <para>friendly target based on the amount done, 30% for heals, 40% for damage spells.</para>
    /// <para>http://www.wowhead.com/spell=124974/natures-vigil</para>
    /// </summary>
    public class NaturesVigilAbility : AbilityBase
    {
        public NaturesVigilAbility()
            : base(WoWSpell.FromId(SpellBook.NaturesVigil), true, true)
        {
            base.Category = AbilityCategory.Buff;

            base.Conditions.Add(new MeHasAttackableTargetCondition());
            base.Conditions.Add(new ConditionOrList(
                new ConditionTestSwitchCondition(
                    new MyExpectedSpecializationCondition(Styx.WoWSpec.DruidFeral),
                    new ConditionDependencyList(
                        new BooleanCondition(Settings.NaturesVigilEnabled),
                        new TargetHealthRangeCondition(TargetType.Me, 0, Settings.NaturesVigilMinHealth)
                    ),
                    false
                ),
                new ConditionTestSwitchCondition(
                    new MyExpectedSpecializationCondition(Styx.WoWSpec.DruidGuardian),
                    new ConditionDependencyList(
                        new BooleanCondition(Settings.GuardianNaturesVigilEnabled),
                        new TargetHealthRangeCondition(TargetType.Me, 0, Settings.GuardianNaturesVigilMinHealth)
                    ),
                    false
                )
            ));
            base.Conditions.Add(new TargetDoesNotHaveAuraCondition(TargetType.Me, this.Spell.Id));
            base.Conditions.Add(new TargetDoesNotHaveAuraCondition(TargetType.Me, SpellBook.Prowl));
        }
    }
}
