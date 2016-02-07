using System.Diagnostics;
using System.Threading.Tasks;
using Paws.Core.Abilities.Feral;
using Paws.Core.Abilities.Guardian;
using Paws.Core.Managers;
using Styx;
using Styx.WoWInternals.WoWObjects;

namespace Paws.Core.Routines
{
    public static class PreCombat
    {
        /// <summary>
        ///     Buff after a 5 second pulse timer so we don't appear like an automated program that zones in and immediately buffs.
        /// </summary>
        public const int BuffTimerIntervalMs = 5000;

        public const int InitTimerMs = 3000;
        private static readonly Stopwatch BuffTimer = new Stopwatch();
        public static Stopwatch InitTimer = new Stopwatch();

        private static LocalPlayer Me
        {
            get { return StyxWoW.Me; }
        }

        private static AbilityManager Abilities
        {
            get { return AbilityManager.Instance; }
        }

        public static async Task<bool> Rotation()
        {
            if (Main.DeathTimer.IsRunning) Main.DeathTimer.Reset();

            if (Main.Me.IsDead || Main.Me.IsGhost || Main.Me.IsCasting || Main.Me.IsChanneling || Main.Me.IsFlying ||
                Main.Me.OnTaxi || Main.Me.Mounted || Main.Me.IsInTravelForm())
                return false;

            if (!BuffTimer.IsRunning) BuffTimer.Start();

            if (Main.Chains.TriggerInAction) return await Main.Chains.TriggeredRotation();
            // Checking if auras are greater than 0 helps with the bot to stop rebuffing immediately after zoning in
            // because the bot has a very small window after loading the character when it's loaded but does not know about
            // the character auras yet (aura count is 0). // Even if we don't have any visible buffs up, the character likely has over 10 "invisible" auras
            if ((BuffTimer.ElapsedMilliseconds >= BuffTimerIntervalMs) && Main.Me.Auras.Count > 0)
            {
                BuffTimer.Restart();

                if (Main.Me.Specialization == WoWSpec.DruidGuardian) return await GuardianPreCombatRotation();
                return await FeralPreCombatRotation();
            }

            if (!InitTimer.IsRunning) InitTimer.Start();
            if (InitTimer.ElapsedMilliseconds < InitTimerMs) return await Main.Units.ForceCombat();
            if (Main.Me.Specialization != WoWSpec.DruidFeral) return await Main.Units.ForceCombat();
            if (await Main.Abilities.Cast<ProwlAbility>(Main.Me)) return true;

            return await Main.Units.ForceCombat();
        }

        private static async Task<bool> GuardianPreCombatRotation()
        {
            if (await ItemManager.UseEligibleItems(MyState.NotInCombat)) return true;
            if (await Abilities.Cast<MarkOfTheWildAbility>(Me)) return true;
            return await Abilities.Cast<BearFormAbility>(Me);
        }

        private static async Task<bool> FeralPreCombatRotation()
        {
            if (await ItemManager.UseEligibleItems(MyState.NotInCombat)) return true;
            if (await Abilities.Cast<MarkOfTheWildAbility>(Me)) return true;
            return await Abilities.Cast<CatFormAbility>(Me);
        }
    }
}