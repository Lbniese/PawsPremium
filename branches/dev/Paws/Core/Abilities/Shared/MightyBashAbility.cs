using Paws.Core.Conditions;
using Styx.WoWInternals;

namespace Paws.Core.Abilities.Shared
{
    /// <summary>
    /// <para>Mighty Bash</para>
    /// <para>Melee Range</para>
    /// <para>Instant, 50 sec cooldown</para>
    /// <para>Requires Druid</para>
    /// <para>Requires level 75</para>
    /// <para>Invokes the spirit of Ursoc to stun the target for 5 sec. Usable in all shapeshift forms.</para>
    /// <para>http://www.wowhead.com/spell=5211/mighty-bash</para>
    /// </summary>
    public class MightyBashAbility : AbilityBase
    {
        public MightyBashAbility()
            : base(WoWSpell.FromId(SpellBook.MightyBash), false)
        {
            base.Conditions.Add(new ConditionOrList(
                new ConditionTestSwitchCondition(
                    new MyExpectedSpecializationCondition(Styx.WoWSpec.DruidFeral),
                    new BooleanCondition(Settings.MightyBashEnabled),
                    false
                ),
                new ConditionTestSwitchCondition(
                    new MyExpectedSpecializationCondition(Styx.WoWSpec.DruidGuardian),
                    new BooleanCondition(Settings.GuardianMightyBashEnabled),
                    false
                )
            ));
            base.Conditions.Add(new MeHasAttackableTargetCondition());
            base.Conditions.Add(new MyTargetIsWithinMeleeRangeCondition());
            base.Conditions.Add(new SpellIsNotOnCooldownCondition(this.Spell));
            base.Conditions.Add(new TargetDoesNotHaveAuraCondition(TargetType.Me, SpellBook.Prowl));
            base.Conditions.Add(new TargetDoesNotHaveAuraCondition(TargetType.MyCurrentTarget, this.Spell.Id));
        }
    }
}
