using Paws.Core.Conditions;
using Paws.Core.Managers;
using Paws.Core.Utilities;
using Styx.WoWInternals;
using Styx.WoWInternals.WoWObjects;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Paws.Core.Abilities.Feral
{
    /// <summary>
    /// <para>Rake</para>
    /// <para>35 Energy, Melee Range</para>
    /// <para>Instant</para>
    /// <para>Requires Druid (Feral)</para>
    /// <para>Requires level 6</para>
    /// <para>Requires Cat Form</para>
    /// <para>Rake the target for (40% of Attack Power) Bleed damage and an addtional (%200 of</para>
    /// <para>Attack power) Bleed damage over 15 seconds. Awards 1 combo point.</para>
    /// <para>If used while stealthed, the taret will be stunned for 4 seconds.</para>
    /// <para>http://www.wowhead.com/spell=1822/rake</para>
    /// </summary>
    public class RakeAbility : MeleeFeralPandemicAbilityBase
    {
        public RakeAbility()
            : base(WoWSpell.FromId(SpellBook.Rake), false)
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

        public override void Update()
        {
            if (MyCurrentTarget != null && MyCurrentTarget.IsValid)
            {
                int rakeUnitIndex = -1;

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
                                rakeUnitIndex = r;

                                base.PandemicConditions.Remove(minTimeCondition);
                                Log.AppendLine(string.Format("Queuing Rake with a better multiplier (From {0:0.##}x to {1:0.##}x)", rakeTarget.AppliedMultiplier, SnapshotManager.CurrentMultiplier), Colors.Tan);

                                break;
                            }
                        }
                        else
                        {
                            // the multiplier is already the highest it can currently be, we need to make sure the pandemic time condition is applied.
                            var minTimeCondition = GetMinTimeLeftCondition();

                            if (minTimeCondition == null)
                            {
                                rakeUnitIndex = r;

                                base.PandemicConditions.Add(new TargetAuraMinTimeLeftCondition(TargetType.MyCurrentTarget, SpellBook.RakeBleedDebuff, TimeSpan.FromSeconds(4.5)));

                                break;
                            }
                        }
                    }
                }

                if (rakeUnitIndex != -1) SnapshotManager.Instance.RakedTargets.RemoveAt(rakeUnitIndex);
            }
        }

        public override void ApplyDefaultSettings()
        {
            base.ApplyDefaultSettings();

            // Shared //
            var rakeIsEnabled = new BooleanCondition(Settings.RakeEnabled);
            var energy = new ConditionTestSwitchCondition(
                new TargetHasAuraCondition(TargetType.Me, SpellBook.BerserkDruid),
                new MyEnergyRangeCondition(35.0 / 2.0),
                new MyEnergyRangeCondition(35.0)
            );

            // Normal //
            base.Conditions.Add(rakeIsEnabled);
            base.Conditions.Add(energy);
            base.Conditions.Add(new TargetDoesNotHaveAuraCondition(TargetType.MyCurrentTarget, SpellBook.RakeBleedDebuff));

            // Pandemic //
            base.PandemicConditions.Add(rakeIsEnabled);
            base.PandemicConditions.Add(energy);
            base.PandemicConditions.Add(new BooleanCondition(Settings.RakeAllowClipping));
            base.PandemicConditions.Add(new TargetHasAuraCondition(TargetType.MyCurrentTarget, SpellBook.RakeBleedDebuff));
            base.PandemicConditions.Add(new TargetAuraMinTimeLeftCondition(TargetType.MyCurrentTarget, SpellBook.RakeBleedDebuff, TimeSpan.FromSeconds(4.5)));
        }
    }
}
