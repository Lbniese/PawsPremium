using Paws.Core.Abilities.Feral;
using Paws.Core.Managers;
using Styx;
using Styx.WoWInternals.WoWObjects;
using System.Diagnostics;
using System.Threading.Tasks;
using Guardian = Paws.Core.Abilities.Guardian;

namespace Paws.Core.Routines
{
    public static class PreCombat
    {
        private static LocalPlayer Me { get { return StyxWoW.Me; } }
        private static WoWUnit MyCurrentTarget { get { return Me.CurrentTarget; } }
        private static AbilityManager Abilities { get { return AbilityManager.Instance; } }

        /// <summary>
        /// Buff after a 5 second pulse timer so we don't appear like an automated program that zones in and immediately buffs.
        /// </summary>
        public const int BUFF_TIMER_INTERVAL_MS = 5000;
        private static Stopwatch BuffTimer = new Stopwatch();

        public static async Task<bool> Rotation()
        {
            if (Main.DeathTimer.IsRunning) Main.DeathTimer.Reset();

            if (!BuffTimer.IsRunning) BuffTimer.Start();

            // Checking if auras are greater than 0 helps with the bot to stop rebuffing immediately after zoning in
            // because the bot has a very small window after loading the character when it's loaded but does not know about
            // the character auras yet (aura count is 0). // Even if we don't have any visible buffs up, the character likely has over 10 "invisible" auras
            if ((BuffTimer.ElapsedMilliseconds >= BUFF_TIMER_INTERVAL_MS) && Me.Auras.Count > 0)
            {
                BuffTimer.Restart();

                if (Me.IsDead || Me.IsGhost || Me.IsCasting || Me.IsChanneling || Me.IsFlying || Me.OnTaxi || Me.Mounted || Me.IsInTravelForm())
                    return false;

                if (Me.Specialization == WoWSpec.DruidGuardian) return await GuardianPreCombatRotation();
                else return await FeralPreCombatRotation();
            }

            return false;
        }

        private static async Task<bool> GuardianPreCombatRotation()
        {
            if (await ItemManager.UseEligibleItems(MyState.NotInCombat)) return true;
            if (await Abilities.Cast<MarkOfTheWildAbility>(Me)) return true;
            if (await Abilities.Cast<Guardian.BearFormAbility>(Me)) return true;

            return false;
        }

        private static async Task<bool> FeralPreCombatRotation()
        {
            if (await ItemManager.UseEligibleItems(MyState.NotInCombat)) return true;
            if (await Abilities.Cast<MarkOfTheWildAbility>(Me)) return true;
            if (await Abilities.Cast<CatFormAbility>(Me)) return true;

            return false;
        }
    }
}