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
            base.RequiredConditions.Add(new MeHasAttackableTargetCondition());
            base.RequiredConditions.Add(new TargetDoesNotHaveAuraCondition(TargetType.Me, SpellBook.Prowl));
            base.RequiredConditions.Add(new MeIsFacingTargetCondition());
            base.RequiredConditions.Add(new MeIsInCatFormCondition());
            base.RequiredConditions.Add(new ConditionTestSwitchCondition(
                new TargetDoesNotHaveAuraCondition(TargetType.Me, SpellBook.ClearcastingProc),
                new ConditionTestSwitchCondition(
                    new TargetHasAuraCondition(TargetType.Me, SpellBook.BerserkDruid),
                    new MyEnergyRangeCondition(50.0 / 2.0f),
                    new MyEnergyRangeCondition(50.0)
                )
            ));
        }

        public override void ApplyDefaultSettings()
        {
            base.ApplyDefaultSettings();

            var enabled = new BooleanCondition(Settings.ThrashEnabled);
            var minTargetCount = new ConditionTestSwitchCondition(
                new BooleanCondition(Settings.ThrashClearcastingProcEnabled),
                new TargetHasAuraCondition(TargetType.Me, SpellBook.ClearcastingProc),
                new AttackableTargetsMinCountCondition(Settings.ThrashMinEnemies)
            );
            var distance = new MyTargetDistanceCondition(0, Settings.AOERange);

            base.Conditions.Add(enabled);
            base.Conditions.Add(minTargetCount);
            base.Conditions.Add(distance);
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
            base.PandemicConditions.Add(distance);
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
