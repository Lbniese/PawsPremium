using System;
using Paws.Core.Abilities.Attributes;
using Paws.Core.Conditions;
using Styx.WoWInternals;

namespace Paws.Core.Abilities.Feral
{
    /// <summary>
    ///     <para>Thrash</para>
    ///     <para>50 Energy, 8 yd range</para>
    ///     <para>Instant</para>
    ///     <para>Requires Druid</para>
    ///     <para>Requires level 28</para>
    ///     <para>Requires Cat Form</para>
    ///     <para>Strikes all enemy targets within 8 yards, dealing (31.5% of Attack power) bleed damage and an</para>
    ///     <para>additional (112.5% of Attack power) damage over 15 seconds.</para>
    ///     <para>http://www.wowhead.com/spell=106830/thrash</para>
    /// </summary>
    [AbilityChain(FriendlyName = "Thrash")]
    public class ThrashAbility : PandemicAbilityBase
    {
        public ThrashAbility()
            : base(WoWSpell.FromId(SpellBook.FeralThrash), true, true)
        {
            RequiredConditions.Add(new MeHasAttackableTargetCondition());
            RequiredConditions.Add(new TargetDoesNotHaveAuraCondition(TargetType.Me, SpellBook.Prowl));
            RequiredConditions.Add(new MeIsFacingTargetCondition());
            RequiredConditions.Add(new MeIsInCatFormCondition());
            RequiredConditions.Add(new ConditionTestSwitchCondition(
                new TargetDoesNotHaveAuraCondition(TargetType.Me, SpellBook.ClearcastingProc),
                new ConditionTestSwitchCondition(
                    new TargetHasAuraCondition(TargetType.Me, SpellBook.BerserkDruid),
                    new MyEnergyRangeCondition(50.0/2.0f),
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
            var distance = new MyTargetDistanceCondition(0, Settings.AoeRange);

            Conditions.Add(enabled);
            Conditions.Add(minTargetCount);
            Conditions.Add(distance);
            Conditions.Add(new TargetDoesNotHaveAuraCondition(TargetType.MyCurrentTarget, Spell.Id));
            if (Settings.SavageRoarEnabled)
            {
                Conditions.Add(new ConditionTestSwitchCondition(
                    new MeKnowsSpellCondition(SpellBook.SavageRoar),
                    new MySavageRoarAuraCondition()
                    ));
            }

            PandemicConditions.Add(enabled);
            PandemicConditions.Add(minTargetCount);
            PandemicConditions.Add(distance);
            PandemicConditions.Add(new TargetHasAuraCondition(TargetType.MyCurrentTarget, Spell.Id));
            PandemicConditions.Add(new TargetAuraMinTimeLeftCondition(TargetType.MyCurrentTarget, Spell.Id,
                TimeSpan.FromSeconds(4)));
            if (Settings.SavageRoarEnabled)
            {
                PandemicConditions.Add(new ConditionTestSwitchCondition(
                    new MeKnowsSpellCondition(SpellBook.SavageRoar),
                    new MySavageRoarAuraCondition()
                    ));
            }
        }
    }
}