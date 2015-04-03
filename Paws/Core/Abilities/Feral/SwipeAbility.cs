using Paws.Core.Conditions;
using Styx.WoWInternals;
using System;

namespace Paws.Core.Abilities.Feral
{
    /// <summary>
    /// <para>Swipe</para>
    /// <para>45 Energy, 8 yd range</para>
    /// <para>Instant</para>
    /// <para>Requires Druid (Feral)</para>
    /// <para>Requires level 22</para>
    /// <para>Requires Cat Form</para>
    /// <para>Swipe nearby enemies, inflicting 175% Physical damage. Awards 1 combo point.</para>
    /// <para>Does 20% additional damage against bleeding targets.</para>
    /// <para>http://www.wowhead.com/spell=106785/swipe</para>
    /// </summary>
    public class SwipeAbility : AbilityBase
    {
        public SwipeAbility()
            : base(WoWSpell.FromId(SpellBook.FeralSwipe), true, true)
        { }

        public override void ApplyDefaultSettings()
        {
            base.ApplyDefaultSettings();

            base.Conditions.Add(new BooleanCondition(Settings.SwipeEnabled));
            base.Conditions.Add(new AttackableTargetsMinCountCondition(Settings.SwipeMinEnemies));
            base.Conditions.Add(new MeHasAttackableTargetCondition());
            base.Conditions.Add(new TargetDoesNotHaveAuraCondition(TargetType.Me, SpellBook.Prowl));
            base.Conditions.Add(new MeIsFacingTargetCondition());
            base.Conditions.Add(new MeIsInCatFormCondition());
            base.Conditions.Add(new MyTargetDistanceCondition(0, Settings.AOERange));
            base.Conditions.Add(new ConditionTestSwitchCondition(
                new TargetHasAuraCondition(TargetType.Me, SpellBook.BerserkDruid),
                new MyEnergyRangeCondition(45.0 / 2.0),
                new MyEnergyRangeCondition(45.0)
            ));
            if (Settings.SavageRoarEnabled)
            {
                base.Conditions.Add(new ConditionTestSwitchCondition(
                    new MeKnowsSpellCondition(SpellBook.SavageRoar),
                    new MySavageRoarAuraCondition()
                ));
            }
        }
    }
}
