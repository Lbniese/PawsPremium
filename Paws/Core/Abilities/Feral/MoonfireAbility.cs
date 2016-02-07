using System;
using Paws.Core.Abilities.Attributes;
using Paws.Core.Conditions;
using Styx.WoWInternals;

namespace Paws.Core.Abilities.Feral
{
    /// <summary>
    ///     <para>Moonfire, Lunar</para>
    ///     <para>3.3% of base mana, 40 yd range</para>
    ///     <para>Instant</para>
    ///     <para>Requires Druid</para>
    ///     <para>Requires level 3</para>
    ///     <para>A Lunar spell that burns the enemy for (40.5% of Spell power) Arcane damage and then an additional</para>
    ///     <para>(292.5% of Spell power) Arcane damage over 20 sec.</para>
    ///     <para>http://www.wowhead.com/spell=8921/moonfire#comments</para>
    /// </summary>
    [AbilityChain(FriendlyName = "Moonfire")]
    public class MoonfireAbility : PandemicAbilityBase
    {
        public MoonfireAbility()
            : base(WoWSpell.FromId(SpellBook.Moonfire))
        {
            RequiredConditions.Add(new MeHasAttackableTargetCondition());
            RequiredConditions.Add(new MeIsFacingTargetCondition());
            RequiredConditions.Add(new MyTargetDistanceCondition(0, 38));
            RequiredConditions.Add(new TargetDoesNotHaveAuraCondition(TargetType.Me, SpellBook.Prowl));
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
                new MyEnergyRangeCondition(35.0/2.0),
                new MyEnergyRangeCondition(35.0)
                );

            // Normal //
            Conditions.Add(isEnabled);
            Conditions.Add(isLowLevelCheck);
            Conditions.Add(energy);
            Conditions.Add(new TargetDoesNotHaveAuraCondition(TargetType.MyCurrentTarget,
                SpellBook.MoonfireDotDebuffLowLevel));
            Conditions.Add(new TargetDoesNotHaveAuraCondition(TargetType.MyCurrentTarget,
                SpellBook.MoonfireDotDebuffHighLevel));
            if (Settings.MoonfireOnlyWithLunarInspiration)
            {
                Conditions.Add(new MeKnowsSpellCondition(SpellBook.LunarInspiration));
            }
            if (Settings.FerociousBiteEnabled)
            {
                Conditions.Add(new ConditionTestSwitchCondition(
                    new MeKnowsSpellCondition(SpellBook.FerociousBite),
                    new MyComboPointsCondition(0, 4)
                    ));
            }
            if (Settings.RipEnabled)
            {
                Conditions.Add(new ConditionTestSwitchCondition(
                    new MeKnowsSpellCondition(SpellBook.Rip),
                    new MyComboPointsCondition(0, 4)
                    ));
            }

            // Pandemic //
            PandemicConditions.Add(isEnabled);
            PandemicConditions.Add(isLowLevelCheck);
            PandemicConditions.Add(energy);
            PandemicConditions.Add(new BooleanCondition(Settings.MoonfireAllowClipping));
            PandemicConditions.Add(new TargetHasAuraCondition(TargetType.MyCurrentTarget,
                SpellBook.MoonfireDotDebuffHighLevel));
            PandemicConditions.Add(new TargetAuraMinTimeLeftCondition(TargetType.MyCurrentTarget,
                SpellBook.MoonfireDotDebuffHighLevel, TimeSpan.FromSeconds(4)));
            if (Settings.FerociousBiteEnabled)
            {
                PandemicConditions.Add(new ConditionTestSwitchCondition(
                    new MeKnowsSpellCondition(SpellBook.FerociousBite),
                    new MyComboPointsCondition(0, 4)
                    ));
            }
            if (Settings.RipEnabled)
            {
                PandemicConditions.Add(new ConditionTestSwitchCondition(
                    new MeKnowsSpellCondition(SpellBook.Rip),
                    new MyComboPointsCondition(0, 4)
                    ));
            }
        }
    }
}