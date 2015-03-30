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

            // Feral //
            this.Abilities.Add(new Feral.MarkOfTheWildAbility());
            this.Abilities.Add(new Feral.CatFormAbility());
            this.Abilities.Add(new Feral.CatFormPowerShiftAbility());
            this.Abilities.Add(new Feral.ProwlAbility());
            this.Abilities.Add(new Feral.MoonfireHeightIssueAbility());
            this.Abilities.Add(new Feral.SurvivalInstinctsAbility());
            this.Abilities.Add(new Feral.SavageRoarAbility());
            this.Abilities.Add(new Feral.WildChargeAbility());
            this.Abilities.Add(new Feral.ProwlOpenerAbility());
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
            this.Abilities.Add(new Feral.ForceOfNatureAbility());
            this.Abilities.Add(new Feral.BerserkAbility());
            this.Abilities.Add(new Feral.TigersFuryAbility());
            this.Abilities.Add(new Feral.IncarnationAbility());
            this.Abilities.Add(new Feral.FerociousBiteAbility());
            this.Abilities.Add(new Feral.RipAbility());
            this.Abilities.Add(new Feral.RakeAbility());
            this.Abilities.Add(new Feral.WrathAbility());
            this.Abilities.Add(new Feral.MoonfireAbility());
            this.Abilities.Add(new Feral.ShredAbility());
            this.Abilities.Add(new Feral.MaimAbility());
            this.Abilities.Add(new Feral.RebirthAbility());
            this.Abilities.Add(new Feral.WarStompAbility());
            this.Abilities.Add(new Feral.BerserkingAbility());
            this.Abilities.Add(new Feral.ThrashAbility());
            this.Abilities.Add(new Feral.SwipeAbility());
            this.Abilities.Add(new Feral.HeartOfTheWildAbility());
            this.Abilities.Add(new Feral.RemoveSnareWithStampedingRoarAbility());
            this.Abilities.Add(new Feral.RemoveSnareWithDashAbility());
            this.Abilities.Add(new Feral.HealingTouchSnapshotAbility());
            this.Abilities.Add(new Feral.ShredAtFiveComboPointsAbility());
            this.Abilities.Add(new Feral.HealingTouchMyAllyAbility());
            this.Abilities.Add(new Feral.RejuvenateMyAllyAbility());
            this.Abilities.Add(new Feral.BearFormPowerShiftAbility());

            // Guardian //
            this.Abilities.Add(new Guardian.BearFormAbility());
            this.Abilities.Add(new Guardian.MangleAbility());
            this.Abilities.Add(new Guardian.LacerateAbility());
            this.Abilities.Add(new Guardian.PulverizeAbility());
            this.Abilities.Add(new Guardian.ThrashAbility());
            this.Abilities.Add(new Guardian.MaulAbility());
            this.Abilities.Add(new Guardian.SurvivalInstinctsAbility());
            this.Abilities.Add(new Guardian.BarkskinAbility());
            this.Abilities.Add(new Guardian.FrenziedRegenerationAbility());
            this.Abilities.Add(new Guardian.SavageDefenseAbility());
            this.Abilities.Add(new Guardian.BerserkAbility());
            this.Abilities.Add(new Guardian.WildChargeAbility());
            this.Abilities.Add(new Guardian.FaerieFireAbility());
            this.Abilities.Add(new Guardian.GrowlAbility());
            this.Abilities.Add(new Guardian.IncarnationAbility());
            this.Abilities.Add(new Guardian.BristlingFurAbility());

            // Shared //
            this.Abilities.Add(new Shared.SkullBashAbility());
            this.Abilities.Add(new Shared.CenarionWardAbility());
            this.Abilities.Add(new Shared.MightyBashAbility());
            this.Abilities.Add(new Shared.TyphoonAbility());
            this.Abilities.Add(new Shared.IncapacitatingRoarAbility());
            this.Abilities.Add(new Shared.MassEntanglementAbility());
            this.Abilities.Add(new Shared.RenewalAbility());
            this.Abilities.Add(new Shared.RejuvenationAbility());
            this.Abilities.Add(new Shared.HealingTouchAbility());
            this.Abilities.Add(new Shared.NaturesVigilAbility());
            this.Abilities.Add(new Shared.DashAbility());
            this.Abilities.Add(new Shared.StampedingRoarAbility());
            this.Abilities.Add(new Shared.DisplacerBeastAbility());
            this.Abilities.Add(new Shared.SootheAbility());
        }
    }
}
