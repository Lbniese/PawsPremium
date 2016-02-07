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
    [AbilityChain(FriendlyName = "Mass Entanglement")]
    public class MassEntanglementAbility : AbilityBase
    {
        public MassEntanglementAbility()
            : base(WoWSpell.FromId(SpellBook.MassEntanglement), true, true)
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
                        new BooleanCondition(Settings.MassEntanglementEnabled),
                        new AttackableTargetsMinCountCondition(Settings.MassEntanglementMinEnemies)
                        ),
                    false
                    ),
                new ConditionTestSwitchCondition(
                    new MyExpectedSpecializationCondition(WoWSpec.DruidGuardian),
                    new ConditionDependencyList(
                        new BooleanCondition(Settings.GuardianMassEntanglementEnabled),
                        new AttackableTargetsMinCountCondition(Settings.GuardianMassEntanglementMinEnemies)
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