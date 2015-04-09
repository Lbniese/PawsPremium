using Paws.Core.Abilities.Feral;
using Paws.Core.Utilities;
using Styx;
using Styx.WoWInternals.WoWObjects;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Paws.Core.Managers
{
    /// <summary>
    /// Provides the management of snapshotting abilities.
    /// </summary>
    public sealed class SnapshotManager
    {
        #region Singleton Stuff

        private static SnapshotManager _singletonInstance;

        /// <summary>
        /// Singleton instance.
        /// </summary>
        public static SnapshotManager Instance
        {
            get
            {
                return _singletonInstance ?? (_singletonInstance = new SnapshotManager());
            }
        }

        /// <summary>
        /// Rebuilds and reloads all of the abilities. Useful after changing settings.
        /// </summary>
        public static void Reload()
        {
            _singletonInstance = new SnapshotManager();
        }

        private static SettingsManager Settings { get { return SettingsManager.Instance; } }

        #endregion

        private Stopwatch _snapshotTimer = new Stopwatch();
        private int _snapshotIntervalInMs = 250;

        private static LocalPlayer Me { get { return StyxWoW.Me; } }
        private static WoWUnit MyCurrentTarget { get { return Me.CurrentTarget; } }
        private AbilityManager Abilities { get { return AbilityManager.Instance; } }

        public List<BleedingUnit> RakedTargets { get; private set; }

        public static float CurrentMultiplier
        {
            get
            {
                var multiplier = 1.0f;

                if (Me.HasSavageRoarAura()) multiplier *= 1.4f;
                if (Me.HasAura(SpellBook.BloodtalonsProc)) multiplier *= 1.3f;
                if (Me.HasAura(SpellBook.TigersFury)) multiplier *= 1.15f;
                if (Me.KnowsSpell(SpellBook.ImprovedRake) && (Me.HasAura(SpellBook.FeralIncarnationForm) || AbilityManager.Instance.WasJustProwling)) multiplier *= 2.0f;

                // Log.GUI("Was Just Prowling? " + AbilityManager.Instance.WasJustProwling);
                // Log.GUI("Last Cast Ability Type: " + AbilityManager.Instance.LastCastAbility.GetType().Name);

                return multiplier;
            }
        }

        public SnapshotManager()
        {
            this.RakedTargets = new List<BleedingUnit>();
        }

        public async Task<bool> CheckAndApplyBloodtalons()
        {
            // Do I have Blood Talons Talent?
            var hasBloodTalonsTalent = Me.KnowsSpell(SpellBook.Bloodtalons);

            // Do I have Predatory Swiftness Proc?
            var hasPredatorySwiftnessProc = Me.HasAura(SpellBook.PredatorySwiftnessProc);

            var accessGranted = false;

            if (Settings.BloodtalonsApplyToFinishers)
            {
                // Do I have 4 combo points?
                accessGranted = Me.ComboPoints >= 4;
            }

            if (Settings.BloodtalonsApplyImmediately)
            {
                accessGranted = hasPredatorySwiftnessProc;
            }

            if (hasBloodTalonsTalent && hasPredatorySwiftnessProc)
            {
                if (accessGranted || Me.GetAuraById(SpellBook.PredatorySwiftnessProc).TimeLeft <= TimeSpan.FromSeconds(3))
                {
                    if (await Abilities.Cast<HealingTouchSnapshotAbility>(Me)) return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Updates the SnapShot manager. Should be called during Main.Pulse().
        /// </summary>
        public void Update()
        {
            if (!_snapshotTimer.IsRunning)
            {
                _snapshotTimer.Start();
                return;
            }

            if (_snapshotTimer.ElapsedMilliseconds >= _snapshotIntervalInMs)
            {
                SnapShotTimerElapsed();

                _snapshotTimer.Restart();
            }
        }

        // This is to monitor the snapshotted targets.
        private void SnapShotTimerElapsed()
        {
            // Remove targets that should not be here anymore
            this.RakedTargets.RemoveAll(o => o == null || o.Unit == null || !o.Unit.IsValid || o.Unit.IsDead || !o.Unit.HasAura(SpellBook.RakeBleedDebuff));
        }

        public void AddRakedTarget(WoWUnit target, bool fromCombatEvent = false)
        {
            foreach (var rakedTarget in this.RakedTargets)
            {
                if (target == rakedTarget.Unit)
                {
                    // target already exists, update the multiplier
                    if (!fromCombatEvent) rakedTarget.AppliedMultiplier = CurrentMultiplier;
                    return;
                }
            }

            // target does not exist...
            BleedingUnit unit = new BleedingUnit(target, CurrentMultiplier);

            this.RakedTargets.Add(unit);

            Log.Diagnostics(string.Format("Added Raked unit: {0} [{1}] @ {2:0.##}x ({3} total tracked units)", unit.Unit.SafeName, unit.Unit.GetUnitId(), unit.AppliedMultiplier, this.RakedTargets.Count));
        }
    }
}
