using Paws.Core.Abilities.Feral;
using Paws.Core.Managers;
using Styx;
using Styx.WoWInternals.WoWObjects;
using System.Threading.Tasks;
using System.Linq;
using Guardian = Paws.Core.Abilities.Guardian;
using Shared = Paws.Core.Abilities.Shared;

namespace Paws.Core.Routines
{
    public static class Heal
    {
        private static LocalPlayer Me { get { return StyxWoW.Me; } }
        private static WoWUnit MyCurrentTarget { get { return Me.CurrentTarget; } }
        private static AbilityManager Abilities { get { return AbilityManager.Instance; } }

        public static async Task<bool> Rotation()
        {
            if (Main.DeathTimer.IsRunning) Main.DeathTimer.Reset();

            if (!StyxWoW.IsInGame || !StyxWoW.IsInWorld)
                return false;

            if (Me.IsDead || Me.IsGhost || Me.IsCasting || Me.IsChanneling || Me.IsFlying || Me.OnTaxi || Me.Mounted || Me.IsInTravelForm())
                return false;

            if (Me.HasTotalLossOfControl()) 
                return false;

            if (Me.Specialization == WoWSpec.DruidGuardian) return await GuardianHealRotation();
            else return await FeralHealRotation();
        }

        private static async Task<bool> GuardianHealRotation()
        {
            if (await ItemManager.UseHealthstone()) return true;
            if (await ItemManager.UseEligibleItems(MyState.CombatHealing)) return true;
            if (await Abilities.Cast<Shared.HealingTouchAbility>(Me)) return true;
            if (await Abilities.Cast<Shared.NaturesVigilAbility>(Me)) return true;
            if (await Abilities.Cast<Shared.RenewalAbility>(Me)) return true;

            return false;
        }

        private static async Task<bool> FeralHealRotation()
        {
            if (await ItemManager.UseHealthstone()) return true;
            if (await ItemManager.UseEligibleItems(MyState.CombatHealing)) return true;
            if (await Abilities.Cast<Shared.NaturesVigilAbility>(Me)) return true;
            if (await Abilities.Cast<Shared.RenewalAbility>(Me)) return true;
            if (await Abilities.Cast<Shared.HealingTouchAbility>(Me)) return true;
            if (await Abilities.Cast<Shared.RejuvenationAbility>(Me)) return true;
            if (await Abilities.Cast<CatFormAbility>(Me)) return true;

            return false;
        }
    }
}
