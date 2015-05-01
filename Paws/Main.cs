using CommonBehaviors.Actions;
using Paws.Core;
using Paws.Core.Managers;
using Paws.Core.Utilities;
using Paws.Interface;
using Styx;
using Styx.CommonBot;
using Styx.CommonBot.Routines;
using Styx.TreeSharp;
using Styx.WoWInternals;
using Styx.WoWInternals.WoWObjects;
using System;
using System.Diagnostics;
using System.Windows.Forms;
using System.Linq;
using R = Paws.Core.Routines;

namespace Paws
{
    /// <summary>
    /// Main entry point into the custom combat routine.
    /// </summary>
    public class Main : CombatRoutine
    {
        public static Product Product { get { return Paws.Product.Premium; } }

        private static Version _version = new Version(1, 8, 0);
        private static string _environment = "Development";

        public static Version Version { get { return _version; } }
        public static Stopwatch DeathTimer = new Stopwatch();

        private static LocalPlayer Me { get { return StyxWoW.Me; } }
        private static WoWUnit MyCurrentTarget { get { return Me.CurrentTarget; } }
        public override WoWClass Class { get { return WoWClass.Druid; } }
        public override string Name { get { return string.Format("Paws {0} ({1}) (v{2})", Product, _environment, Version); } }
        public override bool WantButton { get { return true; } }
        public override bool NeedDeath { get { return !BotManager.Current.IsRoutineBased() && Me.IsDead && !Me.IsGhost; } }

        public WoWSpec MyCurrentSpec { get; set; }
        public Events Events { get; set; }

        #region Routines

        public override Composite PreCombatBuffBehavior { get { return new ActionRunCoroutine(o => R.PreCombat.Rotation()); } }
        public override Composite PullBehavior { get { return new ActionRunCoroutine(o => R.Pull.Rotation()); } }
        public override Composite CombatBehavior { get { return new ActionRunCoroutine(o => R.Combat.Rotation()); } }
        public override Composite HealBehavior { get { return new ActionRunCoroutine(o => R.Heal.Rotation()); } }
        public override Composite RestBehavior { get { return new ActionRunCoroutine(o => R.Rest.Rotation()); } }

        #endregion


        #region Implementation

        public override void Initialize()
        {
            try
            {
                this.MyCurrentSpec = Me.Specialization;

                GlobalSettingsManager.Instance.Init();
                AbilityManager.ReloadAbilities();
                ItemManager.LoadDataSet();
                AbilityChainsManager.Init();

                this.Events = new Events();

                Log.Combat("--------------------------------------------------");
                Log.Combat(Name);
                Log.Combat(string.Format("You are a Level {0} {1} {2}", Me.Level, Me.Race, Me.Class));
                Log.Combat(string.Format("Current Specialization: {0}", this.MyCurrentSpec.ToString().Replace("Druid", string.Empty)));
                Log.Combat(string.Format("Current Profile: {0}", GlobalSettingsManager.Instance.LastUsedProfile));
                Log.Combat(string.Format("{0} abilities loaded", AbilityManager.Instance.Abilities.Count));
                Log.Combat(string.Format("{0} conditional use items loaded ({1} enabled)", ItemManager.Items.Count, ItemManager.Items.Count(o => o.Enabled)));
                Log.Combat("--------------------------------------------------");

                SettingsManager.Instance.LogDump();
            }
            catch (Exception ex)
            {
                Log.GUI(string.Format("Error Initializing Paws Combat Routine: {0}", ex));
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

                SettingsManager.Instance.LogDump();

                Log.GUI(string.Format("Profile [{0}] saved and loaded.", GlobalSettingsManager.Instance.LastUsedProfile));
            }
        }

        public override void Pulse()
        {
            if (MyCurrentSpec != Me.Specialization)
            {
                Log.Combat(string.Format("Specialization changed from {0} to {1}", MyCurrentSpec.ToString().Replace("Druid", string.Empty), Me.Specialization.ToString().Replace("Druid", string.Empty)));
                this.MyCurrentSpec = Me.Specialization;
            }

            AbilityManager.Instance.Update();
            UnitManager.Instance.Update();
            SnapshotManager.Instance.Update();
            HotKeyManager.Instance.Update();
            AbilityChainsManager.Instance.Update();
            UnitManager.Instance.TargetNearestEnemey();
            
            base.Pulse();
        }

        public override void Death()
        {
            if (this.NeedDeath)
            {
                if (SettingsManager.Instance.ReleaseSpiritOnDeathEnabled)
                {
                    if (!DeathTimer.IsRunning) DeathTimer.Start();
                    if (DeathTimer.ElapsedMilliseconds >= SettingsManager.Instance.ReleaseSpiritOnDeathIntervalInMs)
                    {
                        Log.GUI(string.Format("I have died. Corpse released after {0} ms", DeathTimer.ElapsedMilliseconds));

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
        Community = 0x00, Premium
    }
}
