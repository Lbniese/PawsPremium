﻿using System;
using System.Windows.Media;
using Paws.Core.Abilities.Attributes;
using Paws.Core.Conditions;
using Paws.Core.Managers;
using Paws.Core.Utilities;
using Styx.WoWInternals;

namespace Paws.Core.Abilities.Feral
{
    /// <summary>
    ///     <para>Rake</para>
    ///     <para>35 Energy, Melee Range</para>
    ///     <para>Instant</para>
    ///     <para>Requires Druid (Feral)</para>
    ///     <para>Requires level 6</para>
    ///     <para>Requires Cat Form</para>
    ///     <para>Rake the target for (40% of Attack Power) Bleed damage and an addtional (%200 of</para>
    ///     <para>Attack power) Bleed damage over 15 seconds. Awards 1 combo point.</para>
    ///     <para>If used while stealthed, the taret will be stunned for 4 seconds.</para>
    ///     <para>http://www.wowhead.com/spell=1822/rake</para>
    /// </summary>
    [AbilityChain(FriendlyName = "Rake")]
    public class RakeAbility : MeleeFeralPandemicAbilityBase
    {
        public RakeAbility()
            : base(WoWSpell.FromId(SpellBook.Rake), false)
        {
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

        public override void Update()
        {
            if (Settings.RakeAllowMultiplierClipping && MyCurrentTarget != null && MyCurrentTarget.IsValid)
            {
                for (var r = 0; r < SnapshotManager.Instance.RakedTargets.Count; r++)
                {
                    var rakeTarget = SnapshotManager.Instance.RakedTargets[r];

                    if (MyCurrentTarget == rakeTarget.Unit)
                    {
                        //Log.GUI("Rake.Update() Applied Multiplier: " + rakeTarget.AppliedMultiplier.ToString() + " | Current Multiplier: " + SnapshotManager.CurrentMultiplier.ToString());
                        if (SnapshotManager.CurrentMultiplier > rakeTarget.AppliedMultiplier)
                        {
                            // We need to reapply rake on the unit, this will happen by removing the time restriction on the pandemic conditions list
                            var minTimeCondition = GetMinTimeLeftCondition();

                            if (minTimeCondition != null)
                            {
                                // remove the target from the unit list (it will be readded when rake is successfully applied again but with a better multiplier)
                                //rakeUnitIndex = r;
                                rakeTarget.Requeue = true;

                                PandemicConditions.Remove(minTimeCondition);
                                Log.AppendLine(
                                    string.Format(
                                        "Queuing Rake with a better multiplier (From {0:0.##}x to {1:0.##}x)",
                                        rakeTarget.AppliedMultiplier, SnapshotManager.CurrentMultiplier), Colors.Tan);

                                break;
                            }
                        }
                        else
                        {
                            // the multiplier is already the highest it can currently be, we need to make sure the pandemic time condition is applied.
                            var minTimeCondition = GetMinTimeLeftCondition();

                            if (minTimeCondition == null)
                            {
                                PandemicConditions.Add(new TargetAuraMinTimeLeftCondition(TargetType.MyCurrentTarget,
                                    SpellBook.RakeBleedDebuff, TimeSpan.FromSeconds(4.5)));

                                break;
                            }
                        }
                    }
                }
            }
        }

        public override void ApplyDefaultSettings()
        {
            base.ApplyDefaultSettings();

            // Shared //
            var rakeIsEnabled = new BooleanCondition(Settings.RakeEnabled);
            var meIsNotProwling = new TargetDoesNotHaveAuraCondition(TargetType.Me, SpellBook.Prowl);
            var energy = new ConditionTestSwitchCondition(
                new TargetHasAuraCondition(TargetType.Me, SpellBook.BerserkDruid),
                new MyEnergyRangeCondition(35.0/2.0),
                new MyEnergyRangeCondition(35.0)
                );

            // Normal //
            Conditions.Add(rakeIsEnabled);
            Conditions.Add(meIsNotProwling);
            Conditions.Add(energy);
            Conditions.Add(new TargetDoesNotHaveAuraCondition(TargetType.MyCurrentTarget, SpellBook.RakeBleedDebuff));
            Conditions.Add(new MyMaxRakedUnitsCondition(Settings.RakeMaxEnemies));

            // Pandemic //
            PandemicConditions.Add(rakeIsEnabled);
            PandemicConditions.Add(meIsNotProwling);
            PandemicConditions.Add(energy);
            PandemicConditions.Add(new BooleanCondition(Settings.RakeAllowClipping));
            PandemicConditions.Add(new TargetHasAuraCondition(TargetType.MyCurrentTarget, SpellBook.RakeBleedDebuff));
            PandemicConditions.Add(new TargetAuraMinTimeLeftCondition(TargetType.MyCurrentTarget,
                SpellBook.RakeBleedDebuff, TimeSpan.FromSeconds(4.5)));
        }
    }
}