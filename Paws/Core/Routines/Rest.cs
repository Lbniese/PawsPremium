using System.Threading.Tasks;
using Paws.Core.Abilities.Shared;
using Paws.Core.Managers;
using Styx;
using Styx.CommonBot.Coroutines;
using Styx.WoWInternals.WoWObjects;

namespace Paws.Core.Routines
{
    public static class Rest
    {
        private static LocalPlayer Me
        {
            get { return StyxWoW.Me; }
        }

        public static async Task<bool> Rotation()
        {
            if (Main.DeathTimer.IsRunning) Main.DeathTimer.Reset();

            if (!StyxWoW.IsInGame || !StyxWoW.IsInWorld)
                return false;

            if (Me.IsDead || Me.IsGhost || Me.IsCasting || Me.IsChanneling || Me.IsFlying || Me.OnTaxi || Me.Mounted ||
                Me.IsInTravelForm())
                return false;

            if (Me.HasTotalLossOfControl())
                return false;

            if (Me.Specialization == WoWSpec.DruidGuardian) return await GuardianRestRotation();
            return await FeralRestRoatation();
        }

        private static async Task<bool> GuardianRestRotation()
        {
            return await FeralRestRoatation();
        }

        private static async Task<bool> FeralRestRoatation()
        {
            if (await ItemManager.UseEligibleItems(MyState.Resting)) return true;

            if (Me.HealthPercent <= 50 && !Me.HasAura("Food"))
            {
                Styx.CommonBot.Rest.Feed();
                await CommonCoroutines.SleepForLagDuration();
            }

            if (!(Me.HealthPercent <= 75) || Me.HasAura("Food")) return false;
            return await AbilityManager.Instance.Cast<RejuvenationAbility>(Me);
        }
    }
}