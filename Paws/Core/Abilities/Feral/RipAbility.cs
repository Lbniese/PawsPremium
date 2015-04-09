using Paws.Core.Conditions;
using Paws.Core.Managers;
using Paws.Core.Utilities;
using Styx.WoWInternals;
using System;
using System.Windows.Media;

namespace Paws.Core.Abilities.Feral
{
    /// <summary>
    /// <para>Rip</para>
    /// <para>30 Energy, Melee Range</para>
    /// <para>Instant</para>
    /// <para>Requires Druid (Feral)</para>
    /// <para>Requires level 20</para>
    /// <para>Requires Cat Form</para>
    /// <para>Finishing move that causes Bleed damage over 24 seconds. Damage increases per combo point:</para>
    /// <para>1 point:  [floor(1 * (0.086 * Attack power * 1)) * 8] damage</para>
    /// <para>2 points: [floor(2 * (0.086 * Attack power * 1)) * 8] damage</para>
    /// <para>3 points: [floor(3 * (0.086 * Attack power * 1)) * 8] damage</para>
    /// <para>4 points: [floor(4 * (0.086 * Attack power * 1)) * 8] damage</para>
    /// <para>5 points: [floor(5 * (0.086 * Attack power * 1)) * 8] damage</para>
    /// <para>http://www.wowhead.com/spell=1079/rip</para>
    /// </summary>
    public class RipAbility : MeleeFeralPandemicAbilityBase
    {
        public RipAbility()
            : base(WoWSpell.FromId(SpellBook.Rip), true)
        {
            base.RequiredConditions.Add(new TargetDoesNotHaveAuraCondition(TargetType.Me, SpellBook.Prowl));
        }

        private TargetAuraMinTimeLeftCondition GetMinTimeLeftCondition()
        {
            for (var i = 0; i < base.PandemicConditions.Count; i++)
            {
                if (base.PandemicConditions[i] is TargetAuraMinTimeLeftCondition)
                {
                    return base.PandemicConditions[i] as TargetAuraMinTimeLeftCondition;
                }
            }

            return null;
        }

        public override void ApplyDefaultSettings()
        {
            base.ApplyDefaultSettings();

            // Shared //
            var ripIsEnabled = new BooleanCondition(Settings.RipEnabled);
            var minComboPoints = new MyComboPointsCondition(5, 5);
            var healthCheck = new ConditionTestSwitchCondition(
                new BooleanCondition(Settings.RipEnemyHealthCheck),
                new MyTargetHealthMultiplierCondition(Settings.RipEnemyHealthMultiplier)
            );
            var energy = new ConditionTestSwitchCondition(
                new TargetHasAuraCondition(TargetType.Me, SpellBook.BerserkDruid),
                new MyEnergyRangeCondition(30.0 / 2.0),
                new MyEnergyRangeCondition(30.0)
            );

            // Normal //
            base.Conditions.Add(ripIsEnabled);
            base.Conditions.Add(minComboPoints);
            base.Conditions.Add(healthCheck);
            base.Conditions.Add(energy);
            base.Conditions.Add(new TargetDoesNotHaveAuraCondition(TargetType.MyCurrentTarget, SpellBook.Rip));

            // Pandemic //
            base.PandemicConditions.Add(ripIsEnabled);
            base.PandemicConditions.Add(minComboPoints);
            base.PandemicConditions.Add(healthCheck);
            base.PandemicConditions.Add(energy);
            base.PandemicConditions.Add(new BooleanCondition(Settings.RipAllowClipping));
            base.PandemicConditions.Add(new TargetHasAuraCondition(TargetType.MyCurrentTarget, SpellBook.Rip));
            base.PandemicConditions.Add(new TargetHealthRangeCondition(TargetType.MyCurrentTarget, 25, 100));
            base.PandemicConditions.Add(new TargetAuraMinTimeLeftCondition(TargetType.MyCurrentTarget, SpellBook.Rip, TimeSpan.FromSeconds(7)));
        }
    }
}
