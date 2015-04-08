using Paws.Core.Conditions;
using Styx.WoWInternals;
using System;

namespace Paws.Core.Abilities.Feral
{
    /// <summary>
    /// <para>Moonfire, Lunar</para>
    /// <para>3.3% of base mana, 40 yd range</para>
    /// <para>Instant</para>
    /// <para>Requires Druid</para>
    /// <para>Requires level 3</para>
    /// <para>A Lunar spell that burns the enemy for (40.5% of Spell power) Arcane damage and then an additional</para>
    /// <para>(292.5% of Spell power) Arcane damage over 20 sec.</para>
    /// <para>http://www.wowhead.com/spell=8921/moonfire#comments</para>
    /// </summary>
    public class MoonfireAbility : PandemicAbilityBase
    {
        public MoonfireAbility()
            : base(WoWSpell.FromId(SpellBook.Moonfire))
        {
            base.RequiredConditions.Add(new MeHasAttackableTargetCondition());
            base.RequiredConditions.Add(new MeIsFacingTargetCondition());
            base.RequiredConditions.Add(new MyTargetDistanceCondition(0, 38));
            base.RequiredConditions.Add(new TargetDoesNotHaveAuraCondition(TargetType.Me, SpellBook.Prowl));
        }

        public override void ApplyDefaultSettings()
        {
            base.ApplyDefaultSettings();

            // Shared //
            var isEnabled = new BooleanCondition(Settings.MoonfireEnabled);
            var isLowLevelCheck = new ConditionTestSwitchCondition(
                new MeKnowsSpellCondition(SpellBook.CatForm),
                new ConditionDependencyList(
                    new BooleanCondition(Settings.MoonfireEnabled),
                    new MeIsInCatFormCondition()
                )
            );
            var energy = new ConditionTestSwitchCondition(
                new TargetHasAuraCondition(TargetType.Me, SpellBook.BerserkDruid),
                new MyEnergyRangeCondition(35.0 / 2.0),
                new MyEnergyRangeCondition(35.0)
            );

            // Normal //
            base.Conditions.Add(isEnabled);
            base.Conditions.Add(isLowLevelCheck);
            base.Conditions.Add(energy);
            base.Conditions.Add(new TargetDoesNotHaveAuraCondition(TargetType.MyCurrentTarget, SpellBook.MoonfireDotDebuffLowLevel));
            base.Conditions.Add(new TargetDoesNotHaveAuraCondition(TargetType.MyCurrentTarget, SpellBook.MoonfireDotDebuffHighLevel));
            if (Settings.MoonfireOnlyWithLunarInspiration)
            {
                base.Conditions.Add(new MeKnowsSpellCondition(SpellBook.LunarInspiration));
            }
            if (Settings.FerociousBiteEnabled)
            {
                base.Conditions.Add(new ConditionTestSwitchCondition(
                    new MeKnowsSpellCondition(SpellBook.FerociousBite),
                    new MyComboPointsCondition(0, 4)
                ));
            }
            if (Settings.RipEnabled)
            {
                base.Conditions.Add(new ConditionTestSwitchCondition(
                    new MeKnowsSpellCondition(SpellBook.Rip),
                    new MyComboPointsCondition(0, 4)
                ));
            }

            // Pandemic //
            base.PandemicConditions.Add(isEnabled);
            base.PandemicConditions.Add(isLowLevelCheck);
            base.PandemicConditions.Add(energy);
            base.PandemicConditions.Add(new BooleanCondition(Settings.MoonfireAllowClipping));
            base.PandemicConditions.Add(new TargetHasAuraCondition(TargetType.MyCurrentTarget, SpellBook.MoonfireDotDebuffHighLevel));
            base.PandemicConditions.Add(new TargetAuraMinTimeLeftCondition(TargetType.MyCurrentTarget, SpellBook.MoonfireDotDebuffHighLevel, TimeSpan.FromSeconds(4)));
            if (Settings.FerociousBiteEnabled)
            {
                base.PandemicConditions.Add(new ConditionTestSwitchCondition(
                    new MeKnowsSpellCondition(SpellBook.FerociousBite),
                    new MyComboPointsCondition(0, 4)
                ));
            }
            if (Settings.RipEnabled)
            {
                base.PandemicConditions.Add(new ConditionTestSwitchCondition(
                    new MeKnowsSpellCondition(SpellBook.Rip),
                    new MyComboPointsCondition(0, 4)
                ));
            }
        }
    }
}
