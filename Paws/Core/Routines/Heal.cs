using System.Threading.Tasks;
using Paws.Core.Abilities.Feral;
using Paws.Core.Abilities.Shared;
using Paws.Core.Managers;
using Styx;
using Styx.WoWInternals.WoWObjects;

namespace Paws.Core.Routines
{
    public static class Heal
    {
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

            if (!StyxWoW.IsInGame || !StyxWoW.IsInWorld)
                return false;

            if (Me.IsDead || Me.IsGhost || Me.IsCasting || Me.IsChanneling || Me.IsFlying || Me.OnTaxi || Me.Mounted ||
                Me.IsInTravelForm())
                return false;

            if (Me.HasTotalLossOfControl())
                return false;

            if (Me.Specialization == WoWSpec.DruidGuardian) return await GuardianHealRotation();
            return await FeralHealRotation();
        }

        private static async Task<bool> GuardianHealRotation()
        {
            if (await ItemManager.UseHealthstone()) return true;
            if (await ItemManager.UseEligibleItems(MyState.CombatHealing)) return true;
            if (await Abilities.Cast<HealingTouchAbility>(Me)) return true;
            if (await Abilities.Cast<NaturesVigilAbility>(Me)) return true;
            return await Abilities.Cast<RenewalAbility>(Me);
        }

        private static async Task<bool> FeralHealRotation()
        {
            if (await ItemManager.UseHealthstone()) return true;
            if (await ItemManager.UseEligibleItems(MyState.CombatHealing)) return true;
            if (await Abilities.Cast<NaturesVigilAbility>(Me)) return true;
            if (await Abilities.Cast<RenewalAbility>(Me)) return true;
            if (await Abilities.Cast<HealingTouchAbility>(Me)) return true;
            if (await Abilities.Cast<RejuvenationAbility>(Me)) return true;
            return await Abilities.Cast<CatFormAbility>(Me);
        }
    }
}