using Paws.Core.Conditions;
using Styx.WoWInternals;
using System;

namespace Paws.Core.Abilities.Shared
{
    /// <summary>
    /// <para>Incapacitating Roar</para>
    /// <para>Instant, 10 yd range</para>
    /// <para>30 sec cooldown</para>
    /// <para>Requires Druid (Feral)</para>
    /// <para>Requires level 75</para>
    /// <para>Invokes the spirit of Ursol to roar, incapacitating all enemeis within 10 yards for 3 sec.</para>
    /// <para>Any damage caused will remove the effect. Usable in all shapeshift forms.</para>
    /// </summary>
    public class IncapacitatingRoarAbility : AbilityBase
    {
        public IncapacitatingRoarAbility()
            : base(WoWSpell.FromId(SpellBook.IncapacitatingRoar), true, true)
        {
            base.Conditions.Add(new MeHasAttackableTargetCondition());
            base.Conditions.Add(new ConditionOrList(
                new ConditionTestSwitchCondition(
                    new MyExpectedSpecializationCondition(Styx.WoWSpec.DruidFeral),
                    new ConditionDependencyList(
                        new BooleanCondition(Settings.IncapacitatingRoarEnabled),
                        new AttackableTargetsMinCountCondition(Settings.IncapacitatingRoarMinEnemies)
                    ),
                    false
                ),
                new ConditionTestSwitchCondition(
                    new MyExpectedSpecializationCondition(Styx.WoWSpec.DruidGuardian),
                    new ConditionDependencyList(
                        new BooleanCondition(Settings.GuardianIncapacitatingRoarEnabled),
                        new AttackableTargetsMinCountCondition(Settings.GuardianIncapacitatingRoarMinEnemies)
                    ),
                    false
                )
            ));
            base.Conditions.Add(new TargetDoesNotHaveAuraCondition(TargetType.Me, SpellBook.Prowl));
            base.Conditions.Add(new TargetDoesNotHaveAuraCondition(TargetType.MyCurrentTarget, this.Spell.Id));
            base.Conditions.Add(new MyTargetDistanceCondition(0, Settings.AOERange));
        }
    }
}
