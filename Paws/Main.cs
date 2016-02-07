using System;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;
using CommonBehaviors.Actions;
using Paws.Core;
using Paws.Core.Managers;
using Paws.Core.Utilities;
using Paws.Interface.Forms;
using Styx;
using Styx.CommonBot;
using Styx.CommonBot.Routines;
using Styx.TreeSharp;
using Styx.WoWInternals;
using Styx.WoWInternals.WoWObjects;
using R = Paws.Core.Routines;

namespace Paws
{
    /// <summary>
    ///     Main entry point into the custom combat routine.
    /// </summary>
    public class Main : CombatRoutine
    {
        private const string Environment = "Release";

        // ReSharper disable once InconsistentNaming
        private static readonly Version _version = new Version(1, 8, 4);
        public static Stopwatch DeathTimer = new Stopwatch();

        public static Product Product
        {
            get { return Product.Premium; }
        }

        public static Version Version
        {
            get { return _version; }
        }

        public static LocalPlayer Me
        {
            get { return StyxWoW.Me; }
        }

        public static WoWUnit MyCurrentTarget
        {
            get { return Me.CurrentTarget; }
        }

        public static SettingsManager Settings
        {
            get { return SettingsManager.Instance; }
        }

        public static AbilityManager Abilities
        {
            get { return AbilityManager.Instance; }
        }

        public static UnitManager Units
        {
            get { return UnitManager.Instance; }
        }

        public static SnapshotManager Snapshots
        {
            get { return SnapshotManager.Instance; }
        }

        public static AbilityChainsManager Chains
        {
            get { return AbilityChainsManager.Instance; }
        }

        public override WoWClass Class
        {
            get { return WoWClass.Druid; }
        }

        public override string Name
        {
            get { return string.Format("Paws {0} ({1}) (v{2})", Product, Environment, Version); }
        }

        public override bool WantButton
        {
            get { return true; }
        }

        public override bool NeedDeath
        {
            get { return !BotManager.Current.IsRoutineBased() && Me.IsDead && !Me.IsGhost; }
        }

        public WoWSpec MyCurrentSpec { get; set; }
        public Events Events { get; set; }

        #region Routines

        public override Composite PreCombatBuffBehavior
        {
            get { return new ActionRunCoroutine(o => R.PreCombat.Rotation()); }
        }

        public override Composite PullBehavior
        {
            get { return new ActionRunCoroutine(o => R.Pull.Rotation()); }
        }

        public override Composite CombatBehavior
        {
            get { return new ActionRunCoroutine(o => R.Combat.Rotation()); }
        }

        public override Composite HealBehavior
        {
            get { return new ActionRunCoroutine(o => R.Heal.Rotation()); }
        }

        public override Composite RestBehavior
        {
            get { return new ActionRunCoroutine(o => R.Rest.Rotation()); }
        }

        #endregion

        #region Implementation

        public override void Initialize()
        {
            try
            {
                MyCurrentSpec = Me.Specialization;

                GlobalSettingsManager.Instance.Init();
                AbilityManager.ReloadAbilities();
                AbilityChainsManager.Init();
                ItemManager.LoadDataSet();

                Events = new Events();

                Log.Combat("--------------------------------------------------");
                Log.Combat(Name);
                Log.Combat(string.Format("You are a Level {0} {1} {2}", Me.Level, Me.Race, Me.Class));
                Log.Combat(string.Format("Current Specialization: {0}",
                    MyCurrentSpec.ToString().Replace("Druid", string.Empty)));
                Log.Combat(string.Format("Current Profile: {0}", GlobalSettingsManager.Instance.LastUsedProfile));
                Log.Combat(string.Format("{0} abilities loaded", AbilityManager.Instance.Abilities.Count));
                Log.Combat(string.Format("{0} conditional use items loaded ({1} enabled)", ItemManager.Items.Count,
                    ItemManager.Items.Count(o => o.Enabled)));
                Log.Combat("--------------------------------------------------");

                AbilityChainsManager.LoadDataSet();

                SettingsManager.Instance.LogDump();
            }
            catch (Exception ex)
            {
                Log.Gui(string.Format("Error Initializing Paws Combat Routine: {0}", ex));
            }
        }

        public override void OnButtonPress()
        {
            var settingsForm = new SettingsForm();

            if (settingsForm.ShowDialog() == DialogResult.OK)
            {
                SettingsManager.InitWithLastUsedProfile();
                AbilityManager.ReloadAbilities();
                ItemManager.LoadDataSet();

                AbilityChainsManager.Init();
                AbilityChainsManager.LoadDataSet();

                SettingsManager.Instance.LogDump();

                Log.Gui(string.Format("Profile [{0}] saved and loaded.", GlobalSettingsManager.Instance.LastUsedProfile));
            }
        }

        public override void Pulse()
        {
            if (MyCurrentSpec != Me.Specialization)
            {
                Log.Combat(string.Format("Specialization changed from {0} to {1}",
                    MyCurrentSpec.ToString().Replace("Druid", string.Empty),
                    Me.Specialization.ToString().Replace("Druid", string.Empty)));
                MyCurrentSpec = Me.Specialization;
            }

            AbilityManager.Instance.Update();
            UnitManager.Instance.Update();
            SnapshotManager.Instance.Update();
            UnitManager.Instance.TargetNearestEnemey();

            base.Pulse();
        }

        public override void Death()
        {
            if (NeedDeath)
            {
                if (SettingsManager.Instance.ReleaseSpiritOnDeathEnabled)
                {
                    if (!DeathTimer.IsRunning) DeathTimer.Start();
                    if (DeathTimer.ElapsedMilliseconds >= SettingsManager.Instance.ReleaseSpiritOnDeathIntervalInMs)
                    {
                        Log.Gui(string.Format("I have died. Corpse released after {0} ms",
                            DeathTimer.ElapsedMilliseconds));

                        DeathTimer.Reset();
                        Lua.DoString("RunMacroText(\"/script RepopMe()\")");
                    }
                }
            }

            base.Death();
        }

        #endregion
    }

    public enum Product
    {
        Community = 0x00,
        Premium
    }
}