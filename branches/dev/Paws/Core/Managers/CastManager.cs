using Paws.Core.Abilities;
using Paws.Core.Conditions;
using Paws.Core.Managers;
using Paws.Core.Utilities;
using Styx;
using Styx.CommonBot;
using Styx.CommonBot.Coroutines;
using Styx.WoWInternals.WoWObjects;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Paws.Core
{
    /// <summary>
    /// Provides the management of spell casting.
    /// </summary>
    public static class CastManager
    {
        /// <summary>
        /// Determine if the spell is on cooldown.
        /// </summary>
        /// <returns></returns>
        public static bool SpellIsOnCooldown(int spellId)
        {
            SpellFindResults spellFindResults;
            if (SpellManager.FindSpell(spellId, out spellFindResults))
            {
                return spellFindResults.Override != null
                    ? spellFindResults.Override.Cooldown
                    : spellFindResults.Original.Cooldown;
            }

            return false;
        }

        /// <summary>
        /// (Non-Blocking) Casts the provided spell on the provided target requiring all conditions to be satisfied prior to casting.
        /// </summary>
        /// <returns>Returns true if the cast is successful</returns>
        public static async Task<bool> CastOnTarget(WoWUnit target, IAbility ability, List<ICondition> conditions)
        {
            foreach (var condition in conditions)
                if (!condition.Satisfied()) return false;

            if (!SpellManager.HasSpell(ability.Spell)) return false;
            if (!SpellManager.CanCast(ability.Spell)) return false;
            if (!SpellManager.Cast(ability.Spell, target)) return false;

            var logColor = Colors.CornflowerBlue;

            switch (ability.Category)
            {
                case AbilityCategory.Heal: logColor = Colors.Yellow; break;
                case AbilityCategory.Defensive: logColor = Colors.LightGreen; break;
                case AbilityCategory.Bloodtalons: logColor = Colors.YellowGreen; break;
                case AbilityCategory.Pandemic: logColor = Colors.Blue; break;
                case AbilityCategory.Buff: logColor = Colors.Plum; break;
            }

            if (StyxWoW.Me.Specialization == WoWSpec.DruidGuardian)
            {
                Log.AppendLine(string.Format("[{0}] Casted {1} on {2} {3}(HP: {4:0.##}%, Rage: {5:0.##}%, SE = {6}) {7}",
                    ability.Category,
                    ability.Spell.Name,
                    target == null ? "Nothing" : (target.IsMe ? "Me" : target.SafeName),
                    target == null ? "No Guid" : (target.IsMe ? string.Empty : "[" + UnitManager.GuidToUnitID(target.Guid) + "] "),
                    StyxWoW.Me.HealthPercent,
                    StyxWoW.Me.RagePercent,
                    UnitManager.Instance.LastKnownSurroundingEnemies.Count,
                    target == null ? string.Empty : (target.IsMe ? string.Empty : string.Format("(Target HP = {0:0.##}%, D = {1:0.##} yd, L = {2})", target.HealthPercent, target.Distance, target.HasAura(SpellBook.Lacerate) ? target.GetAuraById(SpellBook.Lacerate).StackCount.ToString() : "0"))
                ), logColor);
            }
            else
            {
                Log.AppendLine(string.Format("[{0}] Casted {1} on {2} {3}({4}CP: {5}, HP: {6:0.##}%, E: {7:0.##}%, SE = {8}) {9}",
                    ability.Category,
                    ability.Spell.Name,
                    target == null ? "Nothing" : (target.IsMe ? "Me" : target.SafeName),
                    target == null ? "No Guid" : (target.IsMe ? string.Empty : "[" + UnitManager.GuidToUnitID(target.Guid) + "] "),
                    (ability.Spell.Id == SpellBook.Rake || ability.Spell.Id == SpellBook.Rip) ? string.Format("M: {0:0.##}x, ", SnapshotManager.CurrentMultiplier) : string.Empty,
                    StyxWoW.Me.ComboPoints,
                    StyxWoW.Me.HealthPercent,
                    StyxWoW.Me.EnergyPercent,
                    UnitManager.Instance.LastKnownSurroundingEnemies.Count,
                    target == null ? string.Empty : (target.IsMe ? string.Empty : string.Format("(Target HP = {0:0.##}%, D = {1:0.##} ({2:0.##}) yd)", target.HealthPercent, Math.Abs(target.Distance - target.CombatReach), target.Distance))
                ), logColor);
            }
            

            await CommonCoroutines.SleepForLagDuration();

            return true;
        }
    }
}
