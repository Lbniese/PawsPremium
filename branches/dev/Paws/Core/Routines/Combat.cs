using Paws.Core.Managers;
using Styx;
using Styx.CommonBot;
using Styx.WoWInternals.WoWObjects;
using System.Threading.Tasks;
using System.Linq;
using Shared = Paws.Core.Abilities.Shared;
using Guardian = Paws.Core.Abilities.Guardian;
using Feral = Paws.Core.Abilities.Feral;

namespace Paws.Core.Routines
{
    public static class Combat
    {
        private static LocalPlayer Me { get { return StyxWoW.Me; } }
        private static WoWUnit MyCurrentTarget { get { return Me.CurrentTarget; } }
        private static SettingsManager Settings { get { return SettingsManager.Instance; } }
        private static AbilityManager Abilities { get { return AbilityManager.Instance; } }
        private static UnitManager Units { get { return UnitManager.Instance; } }
        private static SnapshotManager Snapshots { get { return SnapshotManager.Instance; } }
        private static AbilityChainsManager Chains { get { return AbilityChainsManager.Instance; } }

        public static async Task<bool> Rotation()
        {
            if (Me.IsCasting || Me.IsChanneling || Me.IsFlying || Me.OnTaxi) return false;

            if (BotManager.Current.IsRoutineBased() && Me.Mounted) return false;

            if (BotManager.Current.IsRoutineBased() && !Me.Combat) return false;

            if (!Chains.TriggerInAction)
            {
                if (Me.Specialization == WoWSpec.DruidFeral && SnareManager.BearFormBlockedTimer.IsRunning && Me.Shapeshift == ShapeshiftForm.Bear)
                {
                    if (await Abilities.Cast<Feral.CatFormPowerShiftAbility>(Me))
                    {
                        Paws.Core.Utilities.Log.GUI(string.Format("Switching to Cat Form after Bear Form Powershift."));
                        return true;
                    }

                    return false;
                }

                // Clear loss of control if we can //
                if (await ItemManager.ClearLossOfControlWithTrinkets()) return true;

                // Don't go any further if we have total loss of control //
                if (Me.HasTotalLossOfControl()) return false;

                // Clear Roots and Snares if we can //
                if (Me.Specialization != WoWSpec.DruidGuardian) // Will add Guardian Clears at a later time.
                    if (await SnareManager.CheckAndClear()) return true;

                // Use eligible items //
                if (await ItemManager.UseEligibleItems(MyState.InCombat)) return true;

                // Check on my minions //
                if (!BotManager.Current.IsRoutineBased())
                    if (await Units.CheckForMyMinionsBeingAttacked()) return false;

                // Check for dead party members (ignore if in a BG or Arena) //
                if (Me.Specialization != WoWSpec.DruidGuardian) // Will add Guardian Battle Rez at a later time.
                    if (!Me.GroupInfo.IsInBattlegroundParty && !Me.IsInArena)
                        if (await Units.CheckAndResurrectDeadAllies()) return true;

                // Check for allies that need to be healed //
                if (Me.Specialization != WoWSpec.DruidGuardian)
                    if (await Units.CheckForAlliesNeedHealing()) return true;

                // Movement //
                if (SettingsManager.Instance.AllowMovement) await MovementHelper.MoveToMyCurrentTarget();
                if (SettingsManager.Instance.AllowTargetFacing) await MovementHelper.FaceMyCurrentTarget();

                // Clear Dead Target - useful in some situations for the questing bot //
                if (!BotManager.Current.IsRoutineBased())
                    if (await MovementHelper.ClearMyDeadTarget()) return true;

                // Soothe an Enraged Target //
                if (await Units.SootheEnragedTarget(MyCurrentTarget)) return true;

                if (Me.Specialization == WoWSpec.DruidGuardian) return await GuardianCombatRotation();
                else return await FeralCombatRotation();
            }
            else
            {
                // The normal rotation is paused because an ability chain has been triggered.
                return await Chains.TriggeredRotation();
            }
        }

        private static async Task<bool> GuardianCombatRotation()
        {
            if (await Units.TauntTarget(MyCurrentTarget)) return true;
            if (await Abilities.Cast<Guardian.FaerieFireAbility>(MyCurrentTarget)) return true;
            if (await Abilities.Cast<Shared.DashAbility>(Me)) return true;
            if (await Abilities.Cast<Shared.StampedingRoarAbility>(Me)) return true;
            if (await Abilities.Cast<Guardian.BearFormAbility>(Me)) return true;
            if (await Abilities.Cast<Guardian.WildChargeAbility>(MyCurrentTarget)) return true;
            if (await Abilities.Cast<Shared.CenarionWardAbility>(Me)) return true;
            if (await Abilities.Cast<Guardian.FrenziedRegenerationAbility>(Me)) return true;
            if (await Abilities.Cast<Guardian.BarkskinAbility>(Me)) return true;
            if (await Abilities.Cast<Guardian.SurvivalInstinctsAbility>(Me)) return true;
            if (await Abilities.Cast<Guardian.BristlingFurAbility>(Me)) return true;
            if (await Abilities.Cast<Guardian.SavageDefenseAbility>(Me)) return true;
            if (await InterruptManager.CheckMyTarget()) return true;
            if (await ItemManager.UseTrinket1()) return true;
            if (await ItemManager.UseTrinket2()) return true;
            if (await Abilities.Cast<Shared.IncapacitatingRoarAbility>(MyCurrentTarget)) return true;
            if (await Abilities.Cast<Shared.MassEntanglementAbility>(MyCurrentTarget)) return true;
            if (await Abilities.Cast<Guardian.IncarnationAbility>(Me)) return true;
            if (await Abilities.Cast<Guardian.BerserkAbility>(Me)) return true;
            if (await Abilities.Cast<Guardian.PulverizeAbility>(MyCurrentTarget)) return true;
            if (await Abilities.Cast<Guardian.MangleAbility>(MyCurrentTarget)) return true;
            if (await Abilities.Cast<Guardian.ThrashAbility>(MyCurrentTarget)) return true;
            if (await Abilities.Cast<Guardian.MaulAbility>(MyCurrentTarget)) return true;
            if (await Abilities.Cast<Guardian.LacerateAbility>(MyCurrentTarget)) return true;
            
            return false;
        }

        private static async Task<bool> FeralCombatRotation()
        {
            if (await Snapshots.CheckAndApplyBloodtalons()) return true;
            if (await Abilities.Cast<Feral.MoonfireHeightIssueAbility>(MyCurrentTarget)) return true;
            if (await Abilities.Cast<Shared.DashAbility>(Me)) return true;
            if (await Abilities.Cast<Shared.StampedingRoarAbility>(Me)) return true;
            if (await Abilities.Cast<Feral.ProwlAbility>(Me)) return true;
            if (await Abilities.Cast<Feral.CatFormAbility>(Me)) return true;
            if (await Abilities.Cast<Feral.SurvivalInstinctsAbility>(Me)) return true;
            if (await Abilities.Cast<Shared.CenarionWardAbility>(Me)) return true;
            if (await Abilities.Cast<Feral.HeartOfTheWildAbility>(Me)) return true;
            if (await Abilities.Cast<Feral.SavageRoarAbility>(Me)) return true;
            if (await Abilities.Cast<Feral.WildChargeAbility>(MyCurrentTarget)) return true;
            if (await Abilities.Cast<Feral.ProwlOpenerAbility>(MyCurrentTarget)) return true;
            if (await Abilities.Cast<Shared.DisplacerBeastAbility>(MyCurrentTarget)) return true;
            if (await Abilities.Cast<Feral.FaerieFireAbility>(MyCurrentTarget)) return true;
            if (await InterruptManager.CheckMyTarget()) return true;
            if (await ItemManager.UseTrinket1()) return true;
            if (await ItemManager.UseTrinket2()) return true;
            if (await Abilities.Cast<Feral.ForceOfNatureAbility>(MyCurrentTarget)) return true;
            if (await Abilities.Cast<Feral.WarStompAbility>(MyCurrentTarget)) return true;
            if (await Abilities.Cast<Shared.IncapacitatingRoarAbility>(MyCurrentTarget)) return true;
            if (await Abilities.Cast<Shared.MassEntanglementAbility>(MyCurrentTarget)) return true;
            if (await Abilities.Cast<Feral.IncarnationAbility>(Me)) return true;
            if (await Abilities.Cast<Feral.BerserkingAbility>(Me)) return true;
            if (await Abilities.Cast<Feral.TigersFuryAbility>(Me)) return true;
            if (await Abilities.Cast<Feral.BerserkAbility>(Me)) return true;
            if (await Abilities.Cast<Feral.ThrashAbility>(MyCurrentTarget)) return true;
            if (await Abilities.Cast<Feral.RipAbility>(MyCurrentTarget)) return true;
            if (await Abilities.Cast<Feral.FerociousBiteAbility>(MyCurrentTarget)) return true;
            if (await Abilities.Cast<Feral.RakeAbility>(MyCurrentTarget)) return true;
            if (await Abilities.Cast<Feral.SwipeAbility>(MyCurrentTarget)) return true;
            if (await Abilities.Cast<Feral.MoonfireAbility>(MyCurrentTarget)) return true;
            if (await Abilities.Cast<Feral.WrathAbility>(MyCurrentTarget)) return true;
            if (await Abilities.Cast<Feral.ShredAbility>(MyCurrentTarget)) return true;
            if (await Abilities.Cast<Feral.ShredAtFiveComboPointsAbility>(MyCurrentTarget)) return true;

            return false;
        }
    }
}
