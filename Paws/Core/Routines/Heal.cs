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
        public static async Task<bool> Rotation()
        {
            if (Main.DeathTimer.IsRunning) Main.DeathTimer.Reset();

            if (!StyxWoW.IsInGame || !StyxWoW.IsInWorld)
                return false;

            if (Main.Me.IsDead || Main.Me.IsGhost || Main.Me.IsCasting || Main.Me.IsChanneling || Main.Me.IsFlying || Main.Me.OnTaxi || Main.Me.Mounted || Main.Me.IsInTravelForm())
                return false;

            if (Main.Me.HasTotalLossOfControl()) 
                return false;

            if (!Main.Chains.TriggerInAction)
            {
                if (Main.Me.Specialization == WoWSpec.DruidGuardian) 
                    return await GuardianHealRotation();
                
                else return await FeralHealRotation();
            }
            else
            {
                return false;
            }
        }

        private static async Task<bool> GuardianHealRotation()
        {
            if (await ItemManager.UseHealthstone()) return true;
            if (await ItemManager.UseEligibleItems(MyState.CombatHealing)) return true;
            if (await Main.Abilities.Cast<Shared.HealingTouchAbility>(Main.Me)) return true;
            if (await Main.Abilities.Cast<Shared.NaturesVigilAbility>(Main.Me)) return true;
            if (await Main.Abilities.Cast<Shared.RenewalAbility>(Main.Me)) return true;

            return false;
        }

        private static async Task<bool> FeralHealRotation()
        {
            if (await ItemManager.UseHealthstone()) return true;
            if (await ItemManager.UseEligibleItems(MyState.CombatHealing)) return true;
            if (await Main.Abilities.Cast<Shared.NaturesVigilAbility>(Main.Me)) return true;
            if (await Main.Abilities.Cast<Shared.RenewalAbility>(Main.Me)) return true;
            if (await Main.Abilities.Cast<Shared.HealingTouchAbility>(Main.Me)) return true;
            if (await Main.Abilities.Cast<Shared.RejuvenationAbility>(Main.Me)) return true;
            if (await Main.Abilities.Cast<CatFormAbility>(Main.Me)) return true;

            return false;
        }
    }
}
