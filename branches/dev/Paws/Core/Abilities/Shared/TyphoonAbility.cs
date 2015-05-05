using Paws.Core.Conditions;
using Paws.Core.Abilities.Attributes;
using Styx.WoWInternals;

namespace Paws.Core.Abilities.Shared
{
    /// <summary>
    /// <para>Typhoon</para>
    /// <para>15 yd range</para>
    /// <para>Instant, 30 sec cooldowm</para>
    /// <para>Requires Druid</para>
    /// <para>Requires level 45</para>
    /// <para>Summons a violent Typhoon that strikes targets in front of the caster within 15 yards,</para>
    /// <para>knowcking them back and dazing them for 6 sec. Usable in all shapeshift forms.</para>
    /// <para>http://www.wowhead.com/spell=132469/typhoon</para>
    /// </summary>
    [AbilityChain(FriendlyName = "Typhoon")]
    public class TyphoonAbility : AbilityBase
    {
        public TyphoonAbility()
            : base(WoWSpell.FromId(SpellBook.Typhoon), true, true)
        {
            
        }

        public override void ApplyDefaultSettings()
        {
            base.ApplyDefaultSettings();

            base.Conditions.Add(new ConditionOrList(
                new ConditionTestSwitchCondition(
                    new MyExpectedSpecializationCondition(Styx.WoWSpec.DruidFeral),
                    new BooleanCondition(Settings.TyphoonEnabled),
                    false
                ),
                new ConditionTestSwitchCondition(
                    new MyExpectedSpecializationCondition(Styx.WoWSpec.DruidGuardian),
                    new BooleanCondition(Settings.GuardianTyphoonEnabled),
                    false
                )
            ));
            base.Conditions.Add(new MeHasAttackableTargetCondition());
            base.Conditions.Add(new TargetDoesNotHaveAuraCondition(TargetType.Me, SpellBook.Prowl));
            base.Conditions.Add(new MyTargetDistanceCondition(0, 15));
        }
    }
}
