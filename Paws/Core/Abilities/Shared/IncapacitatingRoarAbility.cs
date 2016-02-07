using Paws.Core.Abilities.Attributes;
using Paws.Core.Conditions;
using Styx;
using Styx.WoWInternals;

namespace Paws.Core.Abilities.Shared
{
    /// <summary>
    ///     <para>Incapacitating Roar</para>
    ///     <para>Instant, 10 yd range</para>
    ///     <para>30 sec cooldown</para>
    ///     <para>Requires Druid (Feral)</para>
    ///     <para>Requires level 75</para>
    ///     <para>Invokes the spirit of Ursol to roar, incapacitating all enemeis within 10 yards for 3 sec.</para>
    ///     <para>Any damage caused will remove the effect. Usable in all shapeshift forms.</para>
    /// </summary>
    [AbilityChain(FriendlyName = "Incapacitating Roar")]
    public class IncapacitatingRoarAbility : AbilityBase
    {
        public IncapacitatingRoarAbility()
            : base(WoWSpell.FromId(SpellBook.IncapacitatingRoar), true, true)
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
                        new BooleanCondition(Settings.IncapacitatingRoarEnabled),
                        new AttackableTargetsMinCountCondition(Settings.IncapacitatingRoarMinEnemies)
                        ),
                    false
                    ),
                new ConditionTestSwitchCondition(
                    new MyExpectedSpecializationCondition(WoWSpec.DruidGuardian),
                    new ConditionDependencyList(
                        new BooleanCondition(Settings.GuardianIncapacitatingRoarEnabled),
                        new AttackableTargetsMinCountCondition(Settings.GuardianIncapacitatingRoarMinEnemies)
                        ),
                    false
                    )
                ));
            Conditions.Add(new TargetDoesNotHaveAuraCondition(TargetType.Me, SpellBook.Prowl));
            Conditions.Add(new TargetDoesNotHaveAuraCondition(TargetType.MyCurrentTarget, Spell.Id));
            Conditions.Add(new MyTargetDistanceCondition(0, Settings.AoeRange));
        }
    }
}