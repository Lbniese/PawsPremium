﻿using System;
using Paws.Core.Conditions;
using Styx.WoWInternals;

namespace Paws.Core.Abilities.Feral
{
    /// <summary>
    ///     <para>Ferocious Bite</para>
    ///     <para>25 Energy, Melee Range</para>
    ///     <para>Instant</para>
    ///     <para>Requires Druid</para>
    ///     <para>Requires level 6</para>
    ///     <para>Requires Cat Form</para>
    ///     <para>Finishing move that causes damage per combo point and consumes up to 25 additional</para>
    ///     <para>Energy to increase damage by up to 100%</para>
    ///     <para>For Feral (Level 20): When used on targets below 25% health, Ferocious Bite will also</para>
    ///     <para>refresh the duration of your Rip on your target</para>
    ///     <para>Critical strike chance doubled against bleeding targets.</para>
    ///     <para>http://www.wowhead.com/spell=22568/ferocious-bite</para>
    /// </summary>
    public class FerociousBiteAbility : MeleeFeralAbilityBase
    {
        public FerociousBiteAbility()
            : base(WoWSpell.FromId(SpellBook.FerociousBite), true)
        {
            RequiredConditions.Add(new TargetDoesNotHaveAuraCondition(TargetType.Me, SpellBook.Prowl));
        }

        public override void ApplyDefaultSettings()
        {
            base.ApplyDefaultSettings();

            Conditions.Add(new BooleanCondition(Settings.FerociousBiteEnabled));
            Conditions.Add(new MyComboPointsCondition(5));
            Conditions.Add(new ConditionTestSwitchCondition(
                new TargetHasAuraCondition(TargetType.MyCurrentTarget, SpellBook.Rip),
                new ConditionTestSwitchCondition(
                    new TargetHealthRangeCondition(TargetType.MyCurrentTarget, 26.0, 100.0),
                    new TargetAuraMaxTimeLeftCondition(TargetType.MyCurrentTarget, SpellBook.Rip,
                        TimeSpan.FromSeconds(12))
                    )
                ));
            Conditions.Add(new ConditionTestSwitchCondition(
                new TargetHasAuraCondition(TargetType.Me, SpellBook.BerserkDruid),
                new MyEnergyRangeCondition(Settings.FerociousBiteMinEnergy/2.0),
                new MyEnergyRangeCondition(Settings.FerociousBiteMinEnergy)
                ));
        }
    }
}