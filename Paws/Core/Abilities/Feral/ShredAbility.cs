using Paws.Core.Conditions;
using Paws.Core.Abilities.Attributes;
using Styx.WoWInternals;
using System;

namespace Paws.Core.Abilities.Feral
{
    /// <summary>
    /// <para>Shred</para>
    /// <para>40 Energy, Melee Range</para>
    /// <para>Instant</para>
    /// <para>Requires Druid</para>
    /// <para>Requires level 6</para>
    /// <para>Requires Cat Form</para>
    /// <para>Shred the target, causing 400% Physical damage to the target.</para>
    /// <para>For Feral and Guardian (level 40): reduces the target's movement speed by</para>
    /// <para>50% for 12 seconds.</para>
    /// <para>Awards 1 combo point.</para>
    /// <para>Being stealthed increases damage by 35% and doubles critical strike chance. Damage</para>
    /// <para>by 35% and doubles critical strike chance. Damage increased by 20% against bleeding</para>
    /// <para>targets.</para>
    /// <para>http://www.wowhead.com/spell=5221/shred</para>
    /// </summary>
    [AbilityChain(FriendlyName = "Shred")]
    public class ShredAbility : MeleeFeralAbilityBase
    {
        public ShredAbility()
            : base(WoWSpell.FromId(SpellBook.Shred), true)
        {
            base.RequiredConditions.Add(new ConditionTestSwitchCondition(
                new TargetHasAuraCondition(TargetType.Me, SpellBook.BerserkDruid),
                new MyEnergyRangeCondition(40.0 / 2.0),
                new MyEnergyRangeCondition(40.0)
            ));
            base.RequiredConditions.Add(new TargetDoesNotHaveAuraCondition(TargetType.Me, SpellBook.Prowl));
        }

        public override void ApplyDefaultSettings()
        {
            base.ApplyDefaultSettings();

            base.Conditions.Add(new BooleanCondition(Settings.ShredEnabled));
            
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
            if (Settings.SwipeEnabled)
            {
                // This will effectively replace swipe as the filler spell if swipe is enabled
                // and clamp the max amount of surrounding enemies for shred to the minimum number of enemies
                // required by swipe subtracted by 1.
                base.Conditions.Add(new ConditionTestSwitchCondition(
                    new MeKnowsSpellCondition(SpellBook.FeralSwipe),
                    new AttackableTargetsMaxCountCondition(Settings.SwipeMinEnemies - 1)
                ));
            }
        }
    }

    public class ShredAtFiveComboPointsAbility : MeleeFeralAbilityBase
    {
        public ShredAtFiveComboPointsAbility()
            : base(WoWSpell.FromId(SpellBook.Shred), true)
        {
            base.RequiredConditions.Add(new ConditionTestSwitchCondition(
                new TargetHasAuraCondition(TargetType.Me, SpellBook.BerserkDruid),
                new MyEnergyRangeCondition(40.0 / 2.0),
                new MyEnergyRangeCondition(40.0)
            ));
            base.RequiredConditions.Add(new TargetDoesNotHaveAuraCondition(TargetType.Me, SpellBook.Prowl));
        }

        public override void ApplyDefaultSettings()
        {
            // This ability pools more energy at 5 combo points

            base.ApplyDefaultSettings();

            base.Conditions.Add(new BooleanCondition(Settings.ShredEnabled));
            // base.Conditions.Add(new TargetDoesNotHaveAuraCondition(TargetType.Me, SpellBook.BloodtalonsProc));
            base.Conditions.Add(new MyComboPointsCondition(5));
        }
    }
}
