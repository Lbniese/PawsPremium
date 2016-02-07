using System.Threading.Tasks;
using Paws.Core.Abilities.Feral;
using Paws.Core.Abilities.Guardian;
using Paws.Core.Abilities.Shared;
using Paws.Core.Managers;
using Paws.Core.Utilities;
using Styx;
using Styx.CommonBot;
using Styx.WoWInternals.WoWObjects;
using BerserkAbility = Paws.Core.Abilities.Guardian.BerserkAbility;
using FaerieFireAbility = Paws.Core.Abilities.Guardian.FaerieFireAbility;
using IncarnationAbility = Paws.Core.Abilities.Guardian.IncarnationAbility;
using SurvivalInstinctsAbility = Paws.Core.Abilities.Guardian.SurvivalInstinctsAbility;
using ThrashAbility = Paws.Core.Abilities.Guardian.ThrashAbility;
using WildChargeAbility = Paws.Core.Abilities.Guardian.WildChargeAbility;

namespace Paws.Core.Routines
{
    public static class Combat
    {
        private static LocalPlayer Me
        {
            get { return StyxWoW.Me; }
        }

        private static WoWUnit MyCurrentTarget
        {
            get { return Me.CurrentTarget; }
        }

        private static AbilityManager Abilities
        {
            get { return AbilityManager.Instance; }
        }

        private static UnitManager Units
        {
            get { return UnitManager.Instance; }
        }

        private static SnapshotManager Snapshots
        {
            get { return SnapshotManager.Instance; }
        }

        private static AbilityChainsManager Chains
        {
            get { return AbilityChainsManager.Instance; }
        }

        public static async Task<bool> Rotation()
        {
            if (Me.IsCasting || Me.IsChanneling || Me.IsFlying || Me.OnTaxi) return false;

            if (BotManager.Current.IsRoutineBased() && Me.Mounted) return false;

            if (BotManager.Current.IsRoutineBased() && !Me.Combat) return false;

            if (!Chains.TriggerInAction)
            {
                //Chains.Check();

                if (Me.Specialization == WoWSpec.DruidFeral && SnareManager.BearFormBlockedTimer.IsRunning &&
                    Me.Shapeshift == ShapeshiftForm.Bear)
                {
                    if (!await Abilities.Cast<CatFormPowerShiftAbility>(Me)) return false;
                    Log.Gui("Switching to Cat Form after Bear Form Powershift.");
                    return true;
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
                return await FeralCombatRotation();
            }
            // The normal rotation is paused because an ability chain has been triggered.
            return await Chains.TriggeredRotation();
        }

        private static async Task<bool> GuardianCombatRotation()
        {
            if (await Units.TauntTarget(MyCurrentTarget)) return true;
            if (await Abilities.Cast<FaerieFireAbility>(MyCurrentTarget)) return true;
            if (await Abilities.Cast<DashAbility>(Me)) return true;
            if (await Abilities.Cast<StampedingRoarAbility>(Me)) return true;
            if (await Abilities.Cast<BearFormAbility>(Me)) return true;
            if (await Abilities.Cast<WildChargeAbility>(MyCurrentTarget)) return true;
            if (await Abilities.Cast<CenarionWardAbility>(Me)) return true;
            if (await Abilities.Cast<FrenziedRegenerationAbility>(Me)) return true;
            if (await Abilities.Cast<BarkskinAbility>(Me)) return true;
            if (await Abilities.Cast<SurvivalInstinctsAbility>(Me)) return true;
            if (await Abilities.Cast<BristlingFurAbility>(Me)) return true;
            if (await Abilities.Cast<SavageDefenseAbility>(Me)) return true;
            if (await InterruptManager.CheckMyTarget()) return true;
            if (await ItemManager.UseTrinket1()) return true;
            if (await ItemManager.UseTrinket2()) return true;
            if (await Abilities.Cast<IncapacitatingRoarAbility>(MyCurrentTarget)) return true;
            if (await Abilities.Cast<MassEntanglementAbility>(MyCurrentTarget)) return true;
            if (await Abilities.Cast<IncarnationAbility>(Me)) return true;
            if (await Abilities.Cast<BerserkAbility>(Me)) return true;
            if (await Abilities.Cast<PulverizeAbility>(MyCurrentTarget)) return true;
            if (await Abilities.Cast<MangleAbility>(MyCurrentTarget)) return true;
            if (await Abilities.Cast<ThrashAbility>(MyCurrentTarget)) return true;
            if (await Abilities.Cast<MaulAbility>(MyCurrentTarget)) return true;
            return await Abilities.Cast<LacerateAbility>(MyCurrentTarget);
        }

        private static async Task<bool> FeralCombatRotation()
        {
            if (await Snapshots.CheckAndApplyBloodtalons()) return true;
            if (await Abilities.Cast<MoonfireHeightIssueAbility>(MyCurrentTarget)) return true;
            if (await Abilities.Cast<DashAbility>(Me)) return true;
            if (await Abilities.Cast<StampedingRoarAbility>(Me)) return true;
            if (await Abilities.Cast<ProwlAbility>(Me)) return true;
            if (await Abilities.Cast<CatFormAbility>(Me)) return true;
            if (await Abilities.Cast<Abilities.Feral.SurvivalInstinctsAbility>(Me)) return true;
            if (await Abilities.Cast<CenarionWardAbility>(Me)) return true;
            if (await Abilities.Cast<HeartOfTheWildAbility>(Me)) return true;
            if (await Abilities.Cast<SavageRoarAbility>(Me)) return true;
            if (await Abilities.Cast<Abilities.Feral.WildChargeAbility>(MyCurrentTarget)) return true;
            if (await Abilities.Cast<ProwlOpenerAbility>(MyCurrentTarget)) return true;
            if (await Abilities.Cast<DisplacerBeastAbility>(MyCurrentTarget)) return true;
            if (await Abilities.Cast<Abilities.Feral.FaerieFireAbility>(MyCurrentTarget)) return true;
            if (await InterruptManager.CheckMyTarget()) return true;
            if (await ItemManager.UseTrinket1()) return true;
            if (await ItemManager.UseTrinket2()) return true;
            if (await Abilities.Cast<ForceOfNatureAbility>(MyCurrentTarget)) return true;
            if (await Abilities.Cast<WarStompAbility>(MyCurrentTarget)) return true;
            if (await Abilities.Cast<IncapacitatingRoarAbility>(MyCurrentTarget)) return true;
            if (await Abilities.Cast<MassEntanglementAbility>(MyCurrentTarget)) return true;
            if (await Abilities.Cast<Abilities.Feral.IncarnationAbility>(Me)) return true;
            if (await Abilities.Cast<BerserkingAbility>(Me)) return true;
            if (await Abilities.Cast<TigersFuryAbility>(Me)) return true;
            if (await Abilities.Cast<Abilities.Feral.BerserkAbility>(Me)) return true;
            if (await Abilities.Cast<Abilities.Feral.ThrashAbility>(MyCurrentTarget)) return true;
            if (await Abilities.Cast<RipAbility>(MyCurrentTarget)) return true;
            if (await Abilities.Cast<FerociousBiteAbility>(MyCurrentTarget)) return true;
            if (await Abilities.Cast<RakeAbility>(MyCurrentTarget)) return true;
            if (await Abilities.Cast<SwipeAbility>(MyCurrentTarget)) return true;
            if (await Abilities.Cast<MoonfireAbility>(MyCurrentTarget)) return true;
            if (await Abilities.Cast<WrathAbility>(MyCurrentTarget)) return true;
            if (await Abilities.Cast<ShredAbility>(MyCurrentTarget)) return true;
            return await Abilities.Cast<ShredAtFiveComboPointsAbility>(MyCurrentTarget);
        }
    }
}