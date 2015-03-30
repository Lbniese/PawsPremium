using Paws.Core.Conditions;
using Styx.WoWInternals;

namespace Paws.Core.Abilities.Shared
{
    /// <summary>
    /// <para>Skull Bash</para>
    /// <para>13 yd range</para>
    /// <para>Instant, 15 sec cooldown</para>
    /// <para>Requires Druid (Feral, Guardian)</para>
    /// <para>Requires level 64</para>
    /// <para>Requires Cat Form, Bear Form</para>
    /// <para>You charge and skull bash the target, interrupting spellcasting and preventing any spell</para>
    /// <para>in that school from being cast for 4 sec.</para>
    /// <para>http://www.wowhead.com/spell=106839/skull-bash</para>
    /// </summary>
    public class SkullBashAbility : AbilityBase
    {
        public SkullBashAbility()
            : base(WoWSpell.FromId(SpellBook.SkullBash), false, true)
        {
            base.Conditions.Add(new ConditionOrList(
                new ConditionTestSwitchCondition(
                    new MyExpectedSpecializationCondition(Styx.WoWSpec.DruidFeral),
                    new BooleanCondition(Settings.SkullBashEnabled),
                    false
                ),
                new ConditionTestSwitchCondition(
                    new MyExpectedSpecializationCondition(Styx.WoWSpec.DruidGuardian),
                    new BooleanCondition(Settings.GuardianSkullBashEnabled),
                    false
                )
            ));
            base.Conditions.Add(new MeHasAttackableTargetCondition());
            base.Conditions.Add(new TargetDoesNotHaveAuraCondition(TargetType.Me, SpellBook.Prowl));
            base.Conditions.Add(new MyTargetDistanceCondition(0, 13));
        }
    }
}
