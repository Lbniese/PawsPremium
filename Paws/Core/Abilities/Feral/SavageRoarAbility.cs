using System;
using Paws.Core.Conditions;
using Styx.WoWInternals;

namespace Paws.Core.Abilities.Feral
{
    /// <summary>
    ///     <para>Savage Roar</para>
    ///     <para>25 Energy, 100 yd range</para>
    ///     <para>Requires Druid (Feral)</para>
    ///     <para>Requires level 18</para>
    ///     <para>Finishing move that increases physical damage done by 40% while in Cat Form. Lasts longer per</para>
    ///     <para>combo point (Notice: does not break stealth):</para>
    ///     <para>1 point:  18 seconds</para>
    ///     <para>2 points: 24 seconds</para>
    ///     <para>3 points: 30 seconds</para>
    ///     <para>4 points: 36 seconds</para>
    ///     <para>5 points: 42 seconds</para>
    ///     <para>http://www.wowhead.com/spell=52610/savage-roar</para>
    /// </summary>
    public class SavageRoarAbility : PandemicAbilityBase
    {
        public SavageRoarAbility()
            : base(WoWSpell.FromId(SpellBook.SavageRoar))
        {
            Category = AbilityCategory.Buff;

            RequiredConditions.Add(new MeIsInCatFormCondition());
        }

        public override void ApplyDefaultSettings()
        {
            base.ApplyDefaultSettings();

            // Shared //
            var savageRoarIsEnabled = new BooleanCondition(Settings.SavageRoarEnabled);
            var minComboPoints = new MyComboPointsCondition(Settings.SavageRoarMinComboPoints, 5);
            var energy = new ConditionTestSwitchCondition(
                new TargetHasAuraCondition(TargetType.Me, SpellBook.BerserkDruid),
                new MyEnergyRangeCondition(25.0/2.0),
                new MyEnergyRangeCondition(25.0)
                );

            // Normal //
            Conditions.Add(savageRoarIsEnabled);
            Conditions.Add(minComboPoints);
            Conditions.Add(energy);
            Conditions.Add(new MySavageRoarAuraCondition(false));

            // Pandemic //
            PandemicConditions.Add(savageRoarIsEnabled);
            PandemicConditions.Add(minComboPoints);
            PandemicConditions.Add(energy);
            PandemicConditions.Add(new BooleanCondition(Settings.SavageRoarAllowClipping));
            PandemicConditions.Add(new TargetDoesNotHaveAuraCondition(TargetType.Me, SpellBook.GlyphOfSavagery));
            PandemicConditions.Add(new ConditionOrList(
                new TargetHasAuraCondition(TargetType.Me, SpellBook.SavageRoar),
                new TargetHasAuraCondition(TargetType.Me, SpellBook.GlyphOfSavageRoar)
                ));
            PandemicConditions.Add(new ConditionTestSwitchCondition(
                new TargetHasAuraCondition(TargetType.Me, SpellBook.SavageRoar),
                new TargetAuraMinTimeLeftCondition(TargetType.Me, SpellBook.SavageRoar, TimeSpan.FromSeconds(3.5))
                ));
            PandemicConditions.Add(new ConditionTestSwitchCondition(
                new TargetHasAuraCondition(TargetType.Me, SpellBook.GlyphOfSavageRoar),
                new TargetAuraMinTimeLeftCondition(TargetType.Me, SpellBook.GlyphOfSavageRoar, TimeSpan.FromSeconds(3.5))
                ));
        }
    }
}