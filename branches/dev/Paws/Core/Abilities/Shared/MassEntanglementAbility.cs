using Paws.Core.Conditions;
using Paws.Core.Abilities.Attributes;
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

            base.Conditions.Add(new MeHasAttackableTargetCondition());
            base.Conditions.Add(new ConditionOrList(
                new ConditionTestSwitchCondition(
                    new MyExpectedSpecializationCondition(Styx.WoWSpec.DruidFeral),
                    new ConditionDependencyList(
                        new BooleanCondition(Settings.MassEntanglementEnabled),
                        new AttackableTargetsMinCountCondition(Settings.MassEntanglementMinEnemies)
                    ),
                    false
                ),
                new ConditionTestSwitchCondition(
                    new MyExpectedSpecializationCondition(Styx.WoWSpec.DruidGuardian),
                    new ConditionDependencyList(
                        new BooleanCondition(Settings.GuardianMassEntanglementEnabled),
                        new AttackableTargetsMinCountCondition(Settings.GuardianMassEntanglementMinEnemies)
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
