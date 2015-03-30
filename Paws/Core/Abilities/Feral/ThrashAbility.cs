using Paws.Core.Conditions;
using Styx.WoWInternals;
using System;

namespace Paws.Core.Abilities.Feral
{
    /// <summary>
    /// <para>Thrash</para>
    /// <para>50 Energy, 8 yd range</para>
    /// <para>Instant</para>
    /// <para>Requires Druid</para>
    /// <para>Requires level 28</para>
    /// <para>Requires Cat Form</para>
    /// <para>Strikes all enemy targets within 8 yards, dealing (31.5% of Attack power) bleed damage and an</para>
    /// <para>additional (112.5% of Attack power) damage over 15 seconds.</para>
    /// <para>http://www.wowhead.com/spell=106830/thrash</para>
    /// </summary>
    public class ThrashAbility : PandemicAbilityBase
    {
        public ThrashAbility()
            : base(WoWSpell.FromId(SpellBook.FeralThrash), true, true)
        {
            var enabled = new BooleanCondition(Settings.ThrashEnabled);
            var minTargetCount = new ConditionTestSwitchCondition(
                new BooleanCondition(Settings.ThrashClearcastingProcEnabled),
                new TargetHasAuraCondition(TargetType.Me, SpellBook.ClearcastingProc),
                new AttackableTargetsMinCountCondition(Settings.ThrashMinEnemies)
            );
            var attackableTarget = new MeHasAttackableTargetCondition();
            var noProwl = new TargetDoesNotHaveAuraCondition(TargetType.Me, SpellBook.Prowl);
            var facingTarget = new MeIsFacingTargetCondition();
            var inCatForm = new MeIsInCatFormCondition();
            var distance = new MyTargetDistanceCondition(0, Settings.AOERange);
            var energy = new ConditionTestSwitchCondition(
                new TargetDoesNotHaveAuraCondition(TargetType.Me, SpellBook.ClearcastingProc),
                new ConditionTestSwitchCondition(
                    new TargetHasAuraCondition(TargetType.Me, SpellBook.BerserkDruid),
                    new MyEnergyRangeCondition(50.0 / 2.0f),
                    new MyEnergyRangeCondition(50.0)
                )
            );

            base.Conditions.Add(enabled);
            base.Conditions.Add(minTargetCount);
            base.Conditions.Add(attackableTarget);
            base.Conditions.Add(noProwl);
            base.Conditions.Add(facingTarget);
            base.Conditions.Add(inCatForm);
            base.Conditions.Add(distance);
            base.Conditions.Add(energy);
            base.Conditions.Add(new TargetDoesNotHaveAuraCondition(TargetType.MyCurrentTarget, this.Spell.Id));
            if (Settings.SavageRoarEnabled)
            {
                base.Conditions.Add(new ConditionTestSwitchCondition(
                    new MeKnowsSpellCondition(SpellBook.SavageRoar),
                    new MySavageRoarAuraCondition()
                ));
            }

            base.PandemicConditions.Add(enabled);
            base.PandemicConditions.Add(minTargetCount);
            base.PandemicConditions.Add(attackableTarget);
            base.PandemicConditions.Add(noProwl);
            base.PandemicConditions.Add(facingTarget);
            base.PandemicConditions.Add(inCatForm);
            base.PandemicConditions.Add(distance);
            base.PandemicConditions.Add(energy);
            base.PandemicConditions.Add(new TargetHasAuraCondition(TargetType.MyCurrentTarget, this.Spell.Id));
            base.PandemicConditions.Add(new TargetAuraMinTimeLeftCondition(TargetType.MyCurrentTarget, this.Spell.Id, TimeSpan.FromSeconds(4)));
            if (Settings.SavageRoarEnabled)
            {
                base.PandemicConditions.Add(new ConditionTestSwitchCondition(
                    new MeKnowsSpellCondition(SpellBook.SavageRoar),
                    new MySavageRoarAuraCondition()
                ));
            }
        }
    }
}
