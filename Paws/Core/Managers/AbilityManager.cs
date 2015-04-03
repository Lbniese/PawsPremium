using Paws.Core.Abilities;
using Paws.Core.Utilities;
using Styx;
using Styx.WoWInternals.WoWObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shared = Paws.Core.Abilities.Shared;
using Feral = Paws.Core.Abilities.Feral;
using Guardian = Paws.Core.Abilities.Guardian;

namespace Paws.Core.Managers
{
    /// <summary>
    /// Provides the management of loaded abilities.
    /// </summary>
    public sealed class AbilityManager
    {
        #region Singleton Stuff

        private static AbilityManager _singletonInstance;

        /// <summary>
        /// Singleton instance.
        /// </summary>
        public static AbilityManager Instance
        {
            get
            {
                return _singletonInstance ?? (_singletonInstance = new AbilityManager());
            }
        }

        /// <summary>
        /// Rebuilds and reloads all of the abilities. Useful after changing settings.
        /// </summary>
        public static void ReloadAbilities()
        {
            _singletonInstance = new AbilityManager();
        }

        private static SettingsManager Settings { get { return SettingsManager.Instance; } }

        #endregion

        /// <summary>
        /// The amount of time to elapse in order to presume an ability has been casted too quickly after it has been already previously casted.
        /// </summary>
        public const int CAST_TRY_ELAPSED_TIME_MS = 500;

        /// <summary>
        /// The number of times allowed to cast an ability before it has been blocked after a consecutive attempt.
        /// </summary>
        public const int CAST_TRY_THRESHOLD = 1;

        /// <summary>
        /// The length of time in Milliseconds to block an ability before it is allowed to be cast again.
        /// </summary>
        public const int BLOCK_TIME_MS = 2000;

        /// <summary>
        /// Gets the last casted ability.
        /// </summary>
        public IAbility LastCastAbility { get; private set; }

        /// <summary>
        /// Gets the time of the last casted ability.
        /// </summary>
        public DateTime LastCastDateTime { get; private set; }

        /// <summary>
        /// Gets the number of successful cast attempts for the last casted ability.
        /// </summary>
        public int LastCastTries { get; private set; }

        /// <summary>
        /// Gets the list of loaded abilities.
        /// </summary>
        public List<IAbility> Abilities { get; private set; }

        /// <summary>
        /// Gets the list of abilities that are currently blocked.
        /// </summary>
        public BlockedAbilityList BlockedAbilities { get; private set; }

        /// <summary>
        /// Updates each loaded ability. This should only be done during the Main.Pulse().
        /// </summary>
        public void Update()
        {
            foreach (IAbility ability in Abilities)
            {
                ability.Update();
            }
        }

        /// <summary>
        /// <para>(Non-Blocking) Casts the specified ability on the provided target. Also generates logging and audit information.</para>
        /// <para>This is the perferred entry point to casting an ability's spell, as it manages the logic behind blocked abilities and snapshotting.</para>
        /// </summary>
        /// <returns>Will return true if the cast was successful.</returns>
        public async Task<bool> Cast<T>(WoWUnit target) where T: IAbility
        {
            var abilities = Get<T>();

            if (abilities == null || abilities.Count == 0)
                throw new AbilityException("Ability does not exist.");

            foreach (var ability in abilities)
            {
                var blockedAbility = this.BlockedAbilities.GetBlockedAbilityByType(ability.GetType());
                if (blockedAbility != null)
                {
                    var blockedTimeInMs = (DateTime.Now - blockedAbility.BlockedDateAndTime).TotalMilliseconds;
                    if (blockedTimeInMs >= BLOCK_TIME_MS)
                    {
                        this.BlockedAbilities.Remove(blockedAbility);
                        Log.Diagnostics(string.Format("Blocked ability {0} removed after {1} ms.", ability.Spell.Name, blockedTimeInMs));
                    }
                    else
                    {
                        // The ability is blocked, do not cast.
                        return false;
                    }
                }

                var castResult = await ability.CastOnTarget(target);

                if (castResult)
                {
                    if (ability == this.LastCastAbility) // This ability was already casted before, has it been less than the threshold minimum?
                    {
                        var lastCastTimeInMs = (DateTime.Now - this.LastCastDateTime).TotalMilliseconds;
                        if (lastCastTimeInMs < CAST_TRY_ELAPSED_TIME_MS)
                        {
                            if (this.LastCastTries >= CAST_TRY_THRESHOLD)
                            {
                                this.BlockedAbilities.Add(new BlockedAbility(ability, DateTime.Now));
                                Log.Diagnostics(string.Format("{0} has been blocked after {1} cast atttempts. Total of {2} blocked abilities.", ability.GetType().Name, this.LastCastTries+1, this.BlockedAbilities.Count));

                                return false;
                            }
                            else
                            {
                                this.LastCastTries++;
                            }
                        }
                    }
                    else
                    {
                        this.LastCastTries = 1;
                    }

                    // Track Bleeds
                    if (ability is Feral.RakeAbility) SnapshotManager.Instance.AddRakedTarget(target);
                    if (ability is Feral.RipAbility) SnapshotManager.Instance.AddRippedTarget(target);

                    // Track Rejuvenation Targets
                    if (ability is Feral.RejuvenateMyAllyAbility) UnitManager.Instance.LastKnownRejuvenatedAllies.Add(target);

                    this.LastCastAbility = ability;
                    this.LastCastDateTime = DateTime.Now;
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Gets the current instance of the specified ability class.
        /// </summary>
        public List<IAbility> Get<T>() where T: IAbility
        {
            return this.Abilities
                .Where(o => o is T)
                .ToList();
        }

        /// <summary>
        /// Builds the list of abilities on creation.
        /// </summary>
        public AbilityManager()
        {
            this.Abilities = new List<IAbility>();
            this.BlockedAbilities = new BlockedAbilityList();

            // New Feral //
            this.Abilities.Add(Create<Feral.MarkOfTheWildAbility>());
            this.Abilities.Add(Create<Feral.CatFormAbility>());
            this.Abilities.Add(Create<Feral.CatFormPowerShiftAbility>());
            this.Abilities.Add(Create<Feral.ProwlAbility>());
            this.Abilities.Add(Create<Feral.MoonfireHeightIssueAbility>());
            this.Abilities.Add(Create<Feral.SurvivalInstinctsAbility>());
            this.Abilities.Add(Create<Feral.SavageRoarAbility>());
            this.Abilities.Add(Create<Feral.WildChargeAbility>());
            this.Abilities.Add(Create<Feral.ProwlOpenerAbility>());
            this.Abilities.Add(Create<Feral.ForceOfNatureAbility>());
            this.Abilities.Add(Create<Feral.BerserkAbility>());
            this.Abilities.Add(Create<Feral.TigersFuryAbility>());
            this.Abilities.Add(Create<Feral.IncarnationAbility>());
            this.Abilities.Add(Create<Feral.FerociousBiteAbility>());
            this.Abilities.Add(Create<Feral.RipAbility>());
            this.Abilities.Add(Create<Feral.RakeAbility>());
            this.Abilities.Add(Create<Feral.WrathAbility>());
            this.Abilities.Add(Create<Feral.MoonfireAbility>());
            this.Abilities.Add(Create<Feral.ShredAbility>());
            this.Abilities.Add(Create<Feral.MaimAbility>());
            this.Abilities.Add(Create<Feral.RebirthAbility>());
            this.Abilities.Add(Create<Feral.WarStompAbility>());
            this.Abilities.Add(Create<Feral.BerserkingAbility>());
            this.Abilities.Add(Create<Feral.ThrashAbility>());
            this.Abilities.Add(Create<Feral.SwipeAbility>());
            this.Abilities.Add(Create<Feral.HeartOfTheWildAbility>());
            this.Abilities.Add(Create<Feral.RemoveSnareWithStampedingRoarAbility>());
            this.Abilities.Add(Create<Feral.RemoveSnareWithDashAbility>());
            this.Abilities.Add(Create<Feral.HealingTouchSnapshotAbility>());
            this.Abilities.Add(Create<Feral.ShredAtFiveComboPointsAbility>());
            this.Abilities.Add(Create<Feral.HealingTouchMyAllyAbility>());
            this.Abilities.Add(Create<Feral.RejuvenateMyAllyAbility>());
            this.Abilities.Add(Create<Feral.BearFormPowerShiftAbility>());

            this.Abilities.Add(new Feral.FaerieFireAbility(WoWClass.Rogue, Settings.FaerieFireRogueEnabled));
            this.Abilities.Add(new Feral.FaerieFireAbility(WoWClass.Druid, Settings.FaerieFireDruidEnabled));
            this.Abilities.Add(new Feral.FaerieFireAbility(WoWClass.Warrior, Settings.FaerieFireWarriorEnabled));
            this.Abilities.Add(new Feral.FaerieFireAbility(WoWClass.Paladin, Settings.FaerieFirePaladinEnabled));
            this.Abilities.Add(new Feral.FaerieFireAbility(WoWClass.Mage, Settings.FaerieFireMageEnabled));
            this.Abilities.Add(new Feral.FaerieFireAbility(WoWClass.Monk, Settings.FaerieFireMonkEnabled));
            this.Abilities.Add(new Feral.FaerieFireAbility(WoWClass.Hunter, Settings.FaerieFireHunterEnabled));
            this.Abilities.Add(new Feral.FaerieFireAbility(WoWClass.Priest, Settings.FaerieFirePriestEnabled));
            this.Abilities.Add(new Feral.FaerieFireAbility(WoWClass.DeathKnight, Settings.FaerieFireDeathKnightEnabled));
            this.Abilities.Add(new Feral.FaerieFireAbility(WoWClass.Shaman, Settings.FaerieFireShamanEnabled));
            this.Abilities.Add(new Feral.FaerieFireAbility(WoWClass.Warlock, Settings.FaerieFireWarlockEnabled));

            // Guardian //
            this.Abilities.Add(Create<Guardian.BearFormAbility>());
            this.Abilities.Add(Create<Guardian.MangleAbility>());
            this.Abilities.Add(Create<Guardian.LacerateAbility>());
            this.Abilities.Add(Create<Guardian.PulverizeAbility>());
            this.Abilities.Add(Create<Guardian.ThrashAbility>());
            this.Abilities.Add(Create<Guardian.MaulAbility>());
            this.Abilities.Add(Create<Guardian.SurvivalInstinctsAbility>());
            this.Abilities.Add(Create<Guardian.BarkskinAbility>());
            this.Abilities.Add(Create<Guardian.FrenziedRegenerationAbility>());
            this.Abilities.Add(Create<Guardian.SavageDefenseAbility>());
            this.Abilities.Add(Create<Guardian.BerserkAbility>());
            this.Abilities.Add(Create<Guardian.WildChargeAbility>());
            this.Abilities.Add(Create<Guardian.FaerieFireAbility>());
            this.Abilities.Add(Create<Guardian.GrowlAbility>());
            this.Abilities.Add(Create<Guardian.IncarnationAbility>());
            this.Abilities.Add(Create<Guardian.BristlingFurAbility>());

            // Shared //
            this.Abilities.Add(Create<Shared.SkullBashAbility>());
            this.Abilities.Add(Create<Shared.CenarionWardAbility>());
            this.Abilities.Add(Create<Shared.MightyBashAbility>());
            this.Abilities.Add(Create<Shared.TyphoonAbility>());
            this.Abilities.Add(Create<Shared.IncapacitatingRoarAbility>());
            this.Abilities.Add(Create<Shared.MassEntanglementAbility>());
            this.Abilities.Add(Create<Shared.RenewalAbility>());
            this.Abilities.Add(Create<Shared.RejuvenationAbility>());
            this.Abilities.Add(Create<Shared.HealingTouchAbility>());
            this.Abilities.Add(Create<Shared.NaturesVigilAbility>());
            this.Abilities.Add(Create<Shared.DashAbility>());
            this.Abilities.Add(Create<Shared.StampedingRoarAbility>());
            this.Abilities.Add(Create<Shared.DisplacerBeastAbility>());
            this.Abilities.Add(Create<Shared.SootheAbility>());
        }

        /// <summary>
        /// Create an instance of an ability using default settings.
        /// </summary>
        public static IAbility Create<T>() where T : AbilityBase
        {
            var instance = Activator.CreateInstance<T>();
            instance.ApplyDefaultSettings();

            return instance;
        }
    }
}
