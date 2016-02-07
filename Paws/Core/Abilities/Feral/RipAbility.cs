using System;
using Paws.Core.Abilities.Attributes;
using Paws.Core.Conditions;
using Styx.WoWInternals;

namespace Paws.Core.Abilities.Feral
{
    /// <summary>
    ///     <para>Rip</para>
    ///     <para>30 Energy, Melee Range</para>
    ///     <para>Instant</para>
    ///     <para>Requires Druid (Feral)</para>
    ///     <para>Requires level 20</para>
    ///     <para>Requires Cat Form</para>
    ///     <para>Finishing move that causes Bleed damage over 24 seconds. Damage increases per combo point:</para>
    ///     <para>1 point:  [floor(1 * (0.086 * Attack power * 1)) * 8] damage</para>
    ///     <para>2 points: [floor(2 * (0.086 * Attack power * 1)) * 8] damage</para>
    ///     <para>3 points: [floor(3 * (0.086 * Attack power * 1)) * 8] damage</para>
    ///     <para>4 points: [floor(4 * (0.086 * Attack power * 1)) * 8] damage</para>
    ///     <para>5 points: [floor(5 * (0.086 * Attack power * 1)) * 8] damage</para>
    ///     <para>http://www.wowhead.com/spell=1079/rip</para>
    /// </summary>
    [AbilityChain(FriendlyName = "Rip")]
    public class RipAbility : MeleeFeralPandemicAbilityBase
    {
        public RipAbility()
            : base(WoWSpell.FromId(SpellBook.Rip), true)
        {
            RequiredConditions.Add(new TargetDoesNotHaveAuraCondition(TargetType.Me, SpellBook.Prowl));
        }

        private TargetAuraMinTimeLeftCondition GetMinTimeLeftCondition()
        {
            for (var i = 0; i < PandemicConditions.Count; i++)
            {
                if (PandemicConditions[i] is TargetAuraMinTimeLeftCondition)
                {
                    return PandemicConditions[i] as TargetAuraMinTimeLeftCondition;
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
                new MyEnergyRangeCondition(30.0/2.0),
                new MyEnergyRangeCondition(30.0)
                );

            // Normal //
            Conditions.Add(ripIsEnabled);
            Conditions.Add(minComboPoints);
            Conditions.Add(healthCheck);
            Conditions.Add(energy);
            Conditions.Add(new TargetDoesNotHaveAuraCondition(TargetType.MyCurrentTarget, SpellBook.Rip));

            // Pandemic //
            PandemicConditions.Add(ripIsEnabled);
            PandemicConditions.Add(minComboPoints);
            PandemicConditions.Add(healthCheck);
            PandemicConditions.Add(energy);
            PandemicConditions.Add(new BooleanCondition(Settings.RipAllowClipping));
            PandemicConditions.Add(new TargetHasAuraCondition(TargetType.MyCurrentTarget, SpellBook.Rip));
            PandemicConditions.Add(new TargetHealthRangeCondition(TargetType.MyCurrentTarget, 25, 100));
            PandemicConditions.Add(new TargetAuraMinTimeLeftCondition(TargetType.MyCurrentTarget, SpellBook.Rip,
                TimeSpan.FromSeconds(7)));
        }
    }
}