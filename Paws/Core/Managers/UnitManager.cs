﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using Paws.Core.Abilities;
using Paws.Core.Abilities.Feral;
using Paws.Core.Abilities.Guardian;
using Paws.Core.Abilities.Shared;
using Paws.Core.Conditions;
using Paws.Core.Utilities;
using Styx;
using Styx.CommonBot.Coroutines;
using Styx.CommonBot.POI;
using Styx.WoWInternals;
using Styx.WoWInternals.WoWObjects;

namespace Paws.Core.Managers
{
    /// <summary>
    ///     Provides the managment of surrounding units and methods.
    /// </summary>
    public sealed class UnitManager
    {
        private static UnitManager _singletonInstance;

        private static readonly Func<WoWPartyMember, bool> AllyNeedsHealingWhereClause = o =>
            Settings.HealMyAlliesMyHealthCheckEnabled
                ? Me.HealthPercent >= Settings.HealMyAlliesMyMinHealth
                : !o.Dead &&
                  o.ToPlayer().HealthPercent > 1.0 &&
                  o.ToPlayer().Distance < 30.0 &&
                  !o.ToPlayer().IsMe &&
                  o.ToPlayer().GetAllAuras().Any(p => p.SpellId == SpellBook.Rejuvenation && p.CreatorGuid == Me.Guid);

        private static readonly Func<WoWPartyMember, double> AllyNeedsHealingOrderByClause = o =>
            o.ToPlayer().HealthPercent;

        /// <summary>
        ///     Timer used to monitor the surrounding enemies.
        /// </summary>
        private readonly Stopwatch _enemyScanner = new Stopwatch();

        /// <summary>
        ///     The number of milliseconds to elapse before considering the enemy timer "tick"
        /// </summary>
        private readonly int _enemyScannerIntervalMs = 500;

        /// <summary>
        ///     Timer used to monitor the group information.
        /// </summary>
        private readonly Stopwatch _groupScanner = new Stopwatch();

        /// <summary>
        ///     The number of milliseconds to elapse before considering the group timer "tick"
        /// </summary>
        private readonly int _groupScannerIntervalMs = 5000;

        /// <summary>
        ///     Timer used to monitor the player's minions.
        /// </summary>
        private readonly Stopwatch _minionTimer = new Stopwatch();

        /// <summary>
        ///     The number of milliseconds to elapse before considering the minion timer "tick"
        /// </summary>
        private readonly int _minionTimerIntervalMs = 1000;

        /// <summary>
        ///     Timer used to soothe an enraged enemy.
        /// </summary>
        private readonly Stopwatch _sootheTimer = new Stopwatch();

        public UnitManager()
        {
            LastKnownSurroundingEnemies = new List<WoWUnit>();
            LastKnownRejuvenatedAllies = new List<WoWUnit>();

            _enemyScanner.Start();
            _groupScanner.Start();
            _minionTimer.Start();
        }

        /// <summary>
        ///     Gets the list of the last known surrounding enemies (Cached - ensure to check for null and valid units).
        /// </summary>
        public List<WoWUnit> LastKnownSurroundingEnemies { get; private set; }

        /// <summary>
        ///     Gets the list of the last known allies that have had Rejuvenation applied.
        /// </summary>
        public List<WoWUnit> LastKnownRejuvenatedAllies { get; private set; }

        /// <summary>
        ///     Gets the number of the last known group size.
        /// </summary>
        public int LastKnownGroupMemberSize { get; set; }

        /// <summary>
        ///     Updates the Unit manager. Should only be performed during Main.Pulse().
        /// </summary>
        public void Update()
        {
            EnemyUpdate();
            GroupUpdate();
        }

        /// <summary>
        ///     Updates and caches the last known surrrounding enemies list. Try to make as few calls to the object manager as
        ///     neccessary for performance considerations.
        /// </summary>
        private void EnemyUpdate()
        {
            if (!_enemyScanner.IsRunning) _enemyScanner.Restart();
            if (_enemyScanner.ElapsedMilliseconds >= _enemyScannerIntervalMs)
            {
                LastKnownSurroundingEnemies = ObjectManager.GetObjectsOfTypeFast<WoWUnit>().Where(o =>
                    o.IsValid &&
                    o.Distance <= SettingsManager.Instance.AoeRange &&
                    o.Attackable &&
                    !o.IsDead &&
                    !o.IsFriendly &&
                    !o.IsNonCombatPet &&
                    !o.IsCritter
                    )
                    .OrderBy(o => o.Distance)
                    .ToList();

                _enemyScanner.Restart();
            }
        }

        /// <summary>
        ///     Updates the group information.  Uncomment to help diagnose group detection issues.
        /// </summary>
        private void GroupUpdate()
        {
            if (!_groupScanner.IsRunning) _groupScanner.Restart();
            if (_groupScanner.ElapsedMilliseconds >= _groupScannerIntervalMs)
            {
                /* diagnostics and debugging only
                 * We keep this commented out because we don't want player names to show up in our logs
                 * This should only be uncommented if something is not acting right with the group information
                */
                if (LastKnownGroupMemberSize != Me.GroupInfo.NumRaidMembers)
                {
                    var sb =
                        new StringBuilder(string.Format("Group size changed from {0} to {1}", LastKnownGroupMemberSize,
                            Me.GroupInfo.NumRaidMembers));
                    LastKnownGroupMemberSize = Me.GroupInfo.NumRaidMembers;

                    /* // BEGIN
                    this.LastKnownGroupMemberSize = Me.GroupInfo.NumRaidMembers;
                    foreach (var partyMemeber in Me.GroupInfo.RaidMembers)
                    {
                        WoWPlayer player = null;
                        try
                        {
                            player = partyMemeber.ToPlayer();
                        }
                        catch { } // can't convert unit to player - maybe you're in proving grounds? // }

                        sb.Append(string.Format("\t[{0}, {1}]", player == null ? "Unknown" : player.Name, partyMemeber.Role));
                    } */

                    Log.Gui(sb.ToString());
                }
                // END */

                _groupScanner.Restart();
            }
        }

        /// <summary>
        ///     Targets the nearest enemey.
        /// </summary>
        public void TargetNearestEnemey()
        {
            if (Settings.AllowTargeting)
            {
                if (Me.CurrentTarget == null || Me.CurrentTarget.IsDead)
                {
                    if (LastKnownSurroundingEnemies.Count > 0)
                    {
                        var newTarget = LastKnownSurroundingEnemies.FirstOrDefault();

                        if (newTarget != null && newTarget.IsValid && newTarget.IsAlive)
                        {
                            newTarget.Target();
                        }
                    }
                }
            }
        }

        /// <summary>
        ///     Force combat with your current target.
        /// </summary>
        public async Task<bool> ForceCombat()
        {
            if (Settings.ForceCombat)
            {
                if (!Me.Combat && Me.HasAttackableTarget())
                {
                    if (Me.HasAura(SpellBook.Prowl))
                    {
                        if (await Abilities.Cast<ProwlOpenerAbility>(Me.CurrentTarget)) return true;
                    }
                    else
                    {
                        if (await Abilities.Cast<RakeAbility>(Me.CurrentTarget)) return true;
                    }
                }
            }

            return false;
        }

        /// <summary>
        ///     Taunts the target away from other players/ncps.
        /// </summary>
        public async Task<bool> TauntTarget(WoWUnit target)
        {
            if (target != null && target.IsValid && target.IsAlive && target.Attackable && !target.IsPlayer)
            {
                if (target.CurrentTarget != null && target.CurrentTarget.IsValid && target.CurrentTarget.IsAlive &&
                    !target.CurrentTarget.IsMe)
                {
                    var canTaunt = false;

                    if (Me.GroupInfo.NumRaidMembers > 1)
                    {
                        var playerInGroup =
                            Me.GroupInfo.RaidMembers.SingleOrDefault(o => o.Guid == target.CurrentTarget.Guid);

                        if (playerInGroup != null)
                        {
                            if (playerInGroup.Role == WoWPartyMember.GroupRole.Tank && Settings.GrowlGroupTank)
                                canTaunt = true;
                            if (((int) playerInGroup.Role == 50 || (int) playerInGroup.Role == 51) &&
                                Settings.GrowlGroupTank) canTaunt = true;
                            if (playerInGroup.Role == WoWPartyMember.GroupRole.Healer && Settings.GrowlGroupHealer)
                                canTaunt = true;
                            if (playerInGroup.Role == WoWPartyMember.GroupRole.Damage && Settings.GrowlGroupDps)
                                canTaunt = true;
                        }

                        // Check for player pet...
                        if (target.CurrentTarget.IsPet && Settings.GrowlGroupPlayerPet)
                        {
                            if (Me.GroupInfo.RaidMembers.Any(o => o.Guid == target.CurrentTarget.OwnedByUnit.Guid))
                                canTaunt = true;
                        }
                    }

                    if (Settings.GrowlAnythingNotMe) canTaunt = true;

                    if (canTaunt)
                    {
                        if (await Abilities.Cast<GrowlAbility>(target)) return true;
                    }
                }
            }

            return false;
        }

        /// <summary>
        ///     Determines if the player's minions are being attacked and will attempt to target the enemy.
        /// </summary>
        public async Task<bool> CheckForMyMinionsBeingAttacked()
        {
            if (Me.Combat)
            {
                if (!_minionTimer.IsRunning) _minionTimer.Restart();
                if (_minionTimer.ElapsedMilliseconds >= _minionTimerIntervalMs)
                {
                    if (Me.Minions != null && Me.Minions.Count > 0)
                    {
                        if (Me.CurrentTarget == null || Me.Minions.Any(o => o == Me.CurrentTarget))
                        {
                            foreach (var minion in Me.Minions)
                            {
                                if (!minion.IsDead && minion.CurrentTarget != null && minion.CurrentTarget.Attackable)
                                {
                                    _minionTimer.Restart();

                                    Log.AppendLine(
                                        string.Format("Switched to target attacking my minion: {0} [{1}]",
                                            minion.CurrentTarget.SafeName, GuidToUnitId(minion.CurrentTarget.Guid)),
                                        Colors.HotPink);

                                    minion.CurrentTarget.Target();
                                    BotPoi.Current = new BotPoi(minion.CurrentTarget, PoiType.Kill);

                                    await CommonCoroutines.SleepForLagDuration();

                                    return true;
                                }
                            }
                        }
                    }

                    _minionTimer.Restart();
                }
            }
            else
            {
                _minionTimer.Reset();
            }

            return false;
        }

        /// <summary>
        ///     TODO: Experimental, and does not work yet.
        /// </summary>
        public async Task<bool> CheckAndResurrectDeadAllies()
        {
            if (Me.GroupInfo.RaidMembers.Any(o => o.Dead))
            {
                if (SettingsManager.Instance.RebirthTank)
                {
                    if (Settings.RebirthTank || Settings.RebirthAnyAlly)
                        if (await ResurrectDeadAlly(WoWPartyMember.GroupRole.Tank)) return true;

                    if (Settings.RebirthHealer || Settings.RebirthAnyAlly)
                        if (await ResurrectDeadAlly(WoWPartyMember.GroupRole.Healer)) return true;

                    if (Settings.RebirthDps || Settings.RebirthAnyAlly)
                        if (await ResurrectDeadAlly(WoWPartyMember.GroupRole.Damage)) return true;

                    if (Settings.RebirthAnyAlly)
                    {
                        if (await ResurrectDeadAlly(WoWPartyMember.GroupRole.None)) return true;
                        if (await ResurrectDeadAlly(WoWPartyMember.GroupRole.Leader)) return true;
                    }
                }
            }

            return false;
        }

        /// <summary>
        ///     Helper function to resurrenct a dead ally.
        /// </summary>
        private static async Task<bool> ResurrectDeadAlly(WoWPartyMember.GroupRole role)
        {
            var theDead = StyxWoW.Me.GroupInfo.RaidMembers
                .Where(o =>
                    o.Role == role &&
                    o.Dead &&
                    o.ToPlayer().Distance <= 30.0
                )
                .OrderBy(o => o.ToPlayer().Distance);

            if (role == WoWPartyMember.GroupRole.Tank && (!theDead.Any()))
            {
                // check on main/assist tanks just in case.
                theDead = StyxWoW.Me.GroupInfo.RaidMembers
                    .Where(o =>
                        (o.IsMainTank || o.IsMainAssist || (int) o.Role == 50 || (int) o.Role == 51) &&
                        o.Dead &&
                        o.ToPlayer().Distance <= 30.0
                    )
                    .OrderBy(o => o.ToPlayer().Distance);
            }

            if (!theDead.Any()) return false;
            var first = theDead.FirstOrDefault();
            if (first == null) return false;
            var deadPlayer = first.ToPlayer();

            deadPlayer.Target();

            if (!await Abilities.Cast<RebirthAbility>(deadPlayer)) return false;
            Log.Gui(string.Format("Resurrected dead ally: {0} [{1}] (Role: {2}) [G: {3}]]",
                deadPlayer.SafeName,
                GuidToUnitId(deadPlayer.Guid),
                (int) role == 50 ? "LFR Tank" : role.ToString(),
                Me.GroupInfo.NumRaidMembers));

            return true;
        }

        /// <summary>
        ///     (Non-Blocking) Checks for nearby allies that need healing, and heals them.
        /// </summary>
        /// <returns></returns>
        public async Task<bool> CheckForAlliesNeedHealing()
        {
            if (Me.GroupInfo.NumRaidMembers <= 1) return false;
            try
            {
                var possibleCandidates = Me.GroupInfo.RaidMembers
                    .Where(AllyNeedsHealingWhereClause)
                    .OrderBy(AllyNeedsHealingOrderByClause);

                if (possibleCandidates.Any())
                {
                    var possibilities = new List<WoWPartyMember>();

                    var possibleTank = possibleCandidates.FirstOrDefault(o =>
                        (Settings.HealMyAlliesAnyAlly || Settings.HealMyAlliesTank) &&
                        o.Role == WoWPartyMember.GroupRole.Tank);

                    var possibleLfrTank =
                        possibleCandidates.FirstOrDefault(
                            o =>
                                (Settings.HealMyAlliesAnyAlly || Settings.HealMyAlliesTank) && ((int) o.Role == 50) ||
                                (int) o.Role == 51);
                    var possibleHealer =
                        possibleCandidates.FirstOrDefault(
                            o =>
                                (Settings.HealMyAlliesAnyAlly || Settings.HealMyAlliesHealer) &&
                                o.Role == WoWPartyMember.GroupRole.Healer);
                    var possibleDps =
                        possibleCandidates.FirstOrDefault(
                            o =>
                                (Settings.HealMyAlliesAnyAlly || Settings.HealMyAlliesDps) &&
                                o.Role == WoWPartyMember.GroupRole.Damage);
                    var possibleNoRole =
                        possibleCandidates.FirstOrDefault(
                            o => Settings.HealMyAlliesAnyAlly && o.Role == WoWPartyMember.GroupRole.None);

                    if (possibleTank != null) possibilities.Add(possibleTank);
                    if (possibleLfrTank != null) possibilities.Add(possibleLfrTank);
                    if (possibleHealer != null) possibilities.Add(possibleHealer);
                    if (possibleDps != null) possibilities.Add(possibleDps);
                    if (possibleNoRole != null) possibilities.Add(possibleNoRole);

                    if (possibilities.Count > 0)
                    {
                        // After raw health percents are checked, apply a sort order based on weights for each role (provided that health weights are enabled)
                        var theMostHurtPartyMember = possibilities.OrderBy(o =>
                        {
                            var player = o.ToPlayer();

                            if (!Settings.HealMyAlliesApplyWeightsEnabled) return player.HealthPercent;
                            try
                            {
                                if (o.Role == WoWPartyMember.GroupRole.Tank)
                                    return player.HealthPercent/Settings.HealMyAlliesTankWeight;
                                if ((int) o.Role == 50 || (int) o.Role == 51)
                                    return player.HealthPercent/Settings.HealMyAlliesTankWeight;
                                switch (o.Role)
                                {
                                    case WoWPartyMember.GroupRole.Healer:
                                        return player.HealthPercent/Settings.HealMyAlliesHealerWeight;
                                    case WoWPartyMember.GroupRole.Damage:
                                        return player.HealthPercent/Settings.HealMyAlliesDpsWeight;
                                    case WoWPartyMember.GroupRole.None:
                                        break;
                                    case WoWPartyMember.GroupRole.Leader:
                                        break;
                                    case WoWPartyMember.GroupRole.Tank:
                                        break;
                                    default:
                                        throw new ArgumentOutOfRangeException();
                                }
                            }
                            catch (Exception ex)
                            {
                                Log.Diagnostics(string.Format("Error applying weight to [{0}]. Applying raw health percent instead. {1}", o.Role, ex));
                            }

                            return player.HealthPercent;
                        }).FirstOrDefault();

                        if (theMostHurtPartyMember != null)
                        {
                            WoWUnit bestCandidate = theMostHurtPartyMember.ToPlayer();

                            // Healing Touch //
                            if (await HealMyAlly<HealingTouchMyAllyAbility>(bestCandidate, theMostHurtPartyMember.Role, Settings.HealMyAlliesWithHealingTouchMinHealth)) return true;

                            // Rejuvenation //
                            LastKnownRejuvenatedAllies.RemoveAll(o => o == null || !o.IsValid || !o.HasAura(SpellBook.Rejuvenation));

                            if (LastKnownRejuvenatedAllies.Count < Settings.HealMyAlliesWithRejuvenationMaxAllies)
                            {
                                if (!bestCandidate.HasAura(SpellBook.Rejuvenation))
                                {
                                    if (await HealMyAlly<RejuvenateMyAllyAbility>(bestCandidate, theMostHurtPartyMember.Role, Settings.HealMyAlliesWithRejuvenationMinHealth)) return true;
                                }
                            }
                        }
                    }
                }
            }
            catch
            {
                // ignored
            }

            return false;
        }

        /// <summary>
        ///     Helper function to heal a nearby ally.
        /// </summary>
        private async Task<bool> HealMyAlly<T>(WoWUnit ally, WoWPartyMember.GroupRole role, double minHealth) where T : IAbility
        {
            if (ally.HealthPercent < minHealth && ally.HealthPercent > 1.0)
            {
                var cachedHealth = ally.HealthPercent;

                // Will need to work on this a little, since coroutine does not happen immediately, 
                // we may try and heal someone who's already dead by the time the coroutine kicks off
                if (await Abilities.Cast<T>(ally))
                {
                    Log.Diagnostics(string.Format("Healed the most hurt ally: {0} (Role: {1}) @ {2:0.##}% health [G: {3} R: {4}]", ally.SafeName, (int) role == 50 || (int) role == 51 ? "LFR Tank" : role.ToString(), cachedHealth, Me.GroupInfo.NumRaidMembers, LastKnownRejuvenatedAllies.Count));

                    return true;
                }
            }

            return false;
        }

        /// <summary>
        ///     Soothes an enraged target.
        /// </summary>
        public async Task<bool> SootheEnragedTarget(WoWUnit target)
        {
            if (target == null || !target.HasCancelableEnragedEffect()) return false;
            if (!_sootheTimer.IsRunning) _sootheTimer.Start();
            if (_sootheTimer.ElapsedMilliseconds < Settings.SootheReactionTimeInMs) return false;
            if (!await Abilities.Cast<SootheAbility>(target)) return false;
            _sootheTimer.Reset();
            return true;
        }

        public static string GuidToUnitId(WoWGuid wowGuid)
        {
            return string.Format("{0:X4}", wowGuid.Lowest).Right(4);
        }

        public static string GuidToUnitId(string wowGuid)
        {
            return string.IsNullOrEmpty(wowGuid) ? string.Empty : wowGuid.Substring(wowGuid.Length - 4, 4);
        }

        /// <summary>
        ///     Convert the TargetType enum to a WoWUnit.
        /// </summary>
        public static WoWUnit TargetTypeConverter(TargetType targetType)
        {
            switch (targetType)
            {
                case TargetType.Me:
                {
                    return Me;
                }
                case TargetType.MyCurrentTarget:
                {
                    return Me.CurrentTarget;
                }
                case TargetType.MyCurrentFocus:
                {
                    return Me.FocusedUnit;
                }
                default:
                    return null;
            }
        }

        #region Singleton Stuff

        private static LocalPlayer Me
        {
            get { return StyxWoW.Me; }
        }

        private static AbilityManager Abilities
        {
            get { return AbilityManager.Instance; }
        }

        private static SettingsManager Settings
        {
            get { return SettingsManager.Instance; }
        }

        public static UnitManager Instance
        {
            get { return _singletonInstance ?? (_singletonInstance = new UnitManager()); }
        }

        #endregion
    }
}