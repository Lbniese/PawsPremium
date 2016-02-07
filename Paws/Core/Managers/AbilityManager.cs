using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Paws.Core.Abilities;
using Paws.Core.Abilities.Feral;
using Paws.Core.Abilities.Guardian;
using Paws.Core.Abilities.Shared;
using Paws.Core.Utilities;
using Styx;
using Styx.WoWInternals.WoWObjects;
using BerserkAbility = Paws.Core.Abilities.Feral.BerserkAbility;
using FaerieFireAbility = Paws.Core.Abilities.Feral.FaerieFireAbility;
using IncarnationAbility = Paws.Core.Abilities.Feral.IncarnationAbility;
using SurvivalInstinctsAbility = Paws.Core.Abilities.Feral.SurvivalInstinctsAbility;
using ThrashAbility = Paws.Core.Abilities.Feral.ThrashAbility;
using WildChargeAbility = Paws.Core.Abilities.Feral.WildChargeAbility;

namespace Paws.Core.Managers
{
    /// <summary>
    ///     Provides the management of loaded abilities.
    /// </summary>
    public sealed class AbilityManager
    {
        /// <summary>
        ///     The amount of time to elapse in order to presume an ability has been casted too quickly after it has been already
        ///     previously casted.
        /// </summary>
        public const int CastTryElapsedTimeMs = 500;

        /// <summary>
        ///     The number of times allowed to cast an ability before it has been blocked after a consecutive attempt.
        /// </summary>
        public const int CastTryThreshold = 1;

        /// <summary>
        ///     The length of time in Milliseconds to block an ability before it is allowed to be cast again.
        /// </summary>
        public const int BlockTimeMs = 2000;

        /// <summary>
        ///     Builds the list of abilities on creation.
        /// </summary>
        public AbilityManager()
        {
            Abilities = new List<IAbility>();
            BlockedAbilities = new BlockedAbilityList();

            // New Feral //
            Abilities.Add(Create<MarkOfTheWildAbility>());
            Abilities.Add(Create<CatFormAbility>());
            Abilities.Add(Create<CatFormPowerShiftAbility>());
            Abilities.Add(Create<ProwlAbility>());
            Abilities.Add(Create<MoonfireHeightIssueAbility>());
            Abilities.Add(Create<SurvivalInstinctsAbility>());
            Abilities.Add(Create<SavageRoarAbility>());
            Abilities.Add(Create<WildChargeAbility>());
            Abilities.Add(Create<ProwlOpenerAbility>());
            Abilities.Add(Create<ForceOfNatureAbility>());
            Abilities.Add(Create<BerserkAbility>());
            Abilities.Add(Create<TigersFuryAbility>());
            Abilities.Add(Create<IncarnationAbility>());
            Abilities.Add(Create<FerociousBiteAbility>());
            Abilities.Add(Create<RipAbility>());
            Abilities.Add(Create<RakeAbility>());
            Abilities.Add(Create<WrathAbility>());
            Abilities.Add(Create<MoonfireAbility>());
            Abilities.Add(Create<ShredAbility>());
            Abilities.Add(Create<MaimAbility>());
            Abilities.Add(Create<RebirthAbility>());
            Abilities.Add(Create<WarStompAbility>());
            Abilities.Add(Create<BerserkingAbility>());
            Abilities.Add(Create<ThrashAbility>());
            Abilities.Add(Create<SwipeAbility>());
            Abilities.Add(Create<HeartOfTheWildAbility>());
            Abilities.Add(Create<RemoveSnareWithStampedingRoarAbility>());
            Abilities.Add(Create<RemoveSnareWithDashAbility>());
            Abilities.Add(Create<HealingTouchSnapshotAbility>());
            Abilities.Add(Create<ShredAtFiveComboPointsAbility>());
            Abilities.Add(Create<HealingTouchMyAllyAbility>());
            Abilities.Add(Create<RejuvenateMyAllyAbility>());
            Abilities.Add(Create<BearFormPowerShiftAbility>());

            Abilities.Add(new FaerieFireAbility(WoWClass.Rogue, Settings.FaerieFireRogueEnabled));
            Abilities.Add(new FaerieFireAbility(WoWClass.Druid, Settings.FaerieFireDruidEnabled));
            Abilities.Add(new FaerieFireAbility(WoWClass.Warrior, Settings.FaerieFireWarriorEnabled));
            Abilities.Add(new FaerieFireAbility(WoWClass.Paladin, Settings.FaerieFirePaladinEnabled));
            Abilities.Add(new FaerieFireAbility(WoWClass.Mage, Settings.FaerieFireMageEnabled));
            Abilities.Add(new FaerieFireAbility(WoWClass.Monk, Settings.FaerieFireMonkEnabled));
            Abilities.Add(new FaerieFireAbility(WoWClass.Hunter, Settings.FaerieFireHunterEnabled));
            Abilities.Add(new FaerieFireAbility(WoWClass.Priest, Settings.FaerieFirePriestEnabled));
            Abilities.Add(new FaerieFireAbility(WoWClass.DeathKnight, Settings.FaerieFireDeathKnightEnabled));
            Abilities.Add(new FaerieFireAbility(WoWClass.Shaman, Settings.FaerieFireShamanEnabled));
            Abilities.Add(new FaerieFireAbility(WoWClass.Warlock, Settings.FaerieFireWarlockEnabled));

            // Guardian //
            Abilities.Add(Create<BearFormAbility>());
            Abilities.Add(Create<MangleAbility>());
            Abilities.Add(Create<LacerateAbility>());
            Abilities.Add(Create<PulverizeAbility>());
            Abilities.Add(Create<Abilities.Guardian.ThrashAbility>());
            Abilities.Add(Create<MaulAbility>());
            Abilities.Add(Create<Abilities.Guardian.SurvivalInstinctsAbility>());
            Abilities.Add(Create<BarkskinAbility>());
            Abilities.Add(Create<FrenziedRegenerationAbility>());
            Abilities.Add(Create<SavageDefenseAbility>());
            Abilities.Add(Create<Abilities.Guardian.BerserkAbility>());
            Abilities.Add(Create<Abilities.Guardian.WildChargeAbility>());
            Abilities.Add(Create<Abilities.Guardian.FaerieFireAbility>());
            Abilities.Add(Create<GrowlAbility>());
            Abilities.Add(Create<Abilities.Guardian.IncarnationAbility>());
            Abilities.Add(Create<BristlingFurAbility>());

            // Shared //
            Abilities.Add(Create<SkullBashAbility>());
            Abilities.Add(Create<CenarionWardAbility>());
            Abilities.Add(Create<MightyBashAbility>());
            Abilities.Add(Create<TyphoonAbility>());
            Abilities.Add(Create<IncapacitatingRoarAbility>());
            Abilities.Add(Create<MassEntanglementAbility>());
            Abilities.Add(Create<RenewalAbility>());
            Abilities.Add(Create<RejuvenationAbility>());
            Abilities.Add(Create<HealingTouchAbility>());
            Abilities.Add(Create<NaturesVigilAbility>());
            Abilities.Add(Create<DashAbility>());
            Abilities.Add(Create<StampedingRoarAbility>());
            Abilities.Add(Create<DisplacerBeastAbility>());
            Abilities.Add(Create<SootheAbility>());
            Abilities.Add(Create<CycloneAbility>());
            Abilities.Add(Create<EntanglingRootsAbility>());
        }

        /// <summary>
        ///     Gets the last casted ability.
        /// </summary>
        public IAbility LastCastAbility { get; private set; }

        /// <summary>
        ///     Gets the time of the last casted ability.
        /// </summary>
        public DateTime LastCastDateTime { get; private set; }

        /// <summary>
        ///     Gets the number of successful cast attempts for the last casted ability.
        /// </summary>
        public int LastCastTries { get; private set; }

        /// <summary>
        ///     Gets the list of loaded abilities.
        /// </summary>
        public List<IAbility> Abilities { get; private set; }

        /// <summary>
        ///     Gets the list of abilities that are currently blocked.
        /// </summary>
        public BlockedAbilityList BlockedAbilities { get; private set; }


        /// <summary>
        ///     Gets a flag that tells if the character was just prowling within the last 0.5 seconds.
        /// </summary>
        public bool WasJustProwling { get; set; }

        /// <summary>
        ///     Updates each loaded ability. This should only be done during the Main.Pulse().
        /// </summary>
        public void Update()
        {
            foreach (var ability in Abilities)
            {
                ability.Update();
            }

            WasJustProwling = StyxWoW.Me.HasAura(SpellBook.Prowl);
        }

        /// <summary>
        ///     <para>
        ///         (Non-Blocking) Casts the specified ability on the provided target. Also generates logging and audit
        ///         information.
        ///     </para>
        ///     <para>
        ///         This is the perferred entry point to casting an ability's spell, as it manages the logic behind blocked
        ///         abilities and snapshotting.
        ///     </para>
        /// </summary>
        /// <returns>Will return true if the cast was successful.</returns>
        public async Task<bool> Cast<T>(WoWUnit target) where T : IAbility
        {
            var abilities = Get<T>();

            if (abilities == null || abilities.Count == 0)
                throw new AbilityException("Ability does not exist.");

            foreach (var ability in abilities)
            {
                var blockedAbility = BlockedAbilities.GetBlockedAbilityByType(ability.GetType());
                if (blockedAbility != null)
                {
                    var blockedTimeInMs = (DateTime.Now - blockedAbility.BlockedDateAndTime).TotalMilliseconds;
                    if (blockedTimeInMs >= BlockTimeMs)
                    {
                        BlockedAbilities.Remove(blockedAbility);
                        Log.Diagnostics(string.Format("Blocked ability {0} removed after {1} ms.", ability.Spell.Name,
                            blockedTimeInMs));
                    }
                    else
                    {
                        // The ability is blocked, do not cast.
                        return false;
                    }
                }

                var castResult = await ability.CastOnTarget(target);

                if (!castResult) continue;
                if (ability == LastCastAbility)
                    // This ability was already casted before, has it been less than the threshold minimum?
                {
                    var lastCastTimeInMs = (DateTime.Now - LastCastDateTime).TotalMilliseconds;
                    if (lastCastTimeInMs < CastTryElapsedTimeMs)
                    {
                        if (LastCastTries >= CastTryThreshold)
                        {
                            BlockedAbilities.Add(new BlockedAbility(ability, DateTime.Now));
                            Log.Diagnostics(
                                string.Format(
                                    "{0} has been blocked after {1} cast atttempts. Total of {2} blocked abilities.",
                                    ability.GetType().Name, LastCastTries + 1, BlockedAbilities.Count));

                            return false;
                        }
                        LastCastTries++;
                    }
                }
                else
                {
                    LastCastTries = 1;
                }

                LastCastAbility = ability;
                LastCastDateTime = DateTime.Now;

                // Track Bleeds
                if (ability is ProwlOpenerAbility && Settings.RakeStealthOpener)
                    SnapshotManager.Instance.AddRakedTarget(target);
                if (ability is RakeAbility) SnapshotManager.Instance.AddRakedTarget(target);
                // if (ability is Feral.RipAbility) SnapshotManager.Instance.AddRippedTarget(target);

                // Track Rejuvenation Targets
                if (ability is RejuvenateMyAllyAbility) UnitManager.Instance.LastKnownRejuvenatedAllies.Add(target);

                return true;
            }

            return false;
        }

        /// <summary>
        ///     Gets the current instance of the specified ability class.
        /// </summary>
        public List<IAbility> Get<T>() where T : IAbility
        {
            return Abilities
                .Where(o => o is T)
                .ToList();
        }

        /// <summary>
        ///     Create an instance of an ability using default settings.
        /// </summary>
        public static IAbility Create<T>() where T : AbilityBase
        {
            var instance = Activator.CreateInstance<T>();
            instance.ApplyDefaultSettings();

            return instance;
        }

        #region Singleton Stuff

        private static AbilityManager _singletonInstance;

        /// <summary>
        ///     Singleton instance.
        /// </summary>
        public static AbilityManager Instance
        {
            get { return _singletonInstance ?? (_singletonInstance = new AbilityManager()); }
        }

        /// <summary>
        ///     Rebuilds and reloads all of the abilities. Useful after changing settings.
        /// </summary>
        public static void ReloadAbilities()
        {
            _singletonInstance = new AbilityManager();
        }

        private static SettingsManager Settings
        {
            get { return SettingsManager.Instance; }
        }

        #endregion
    }
}