using Paws.Core.Managers;
using Paws.Core.Utilities;
using Styx;
using Styx.CommonBot;
using Styx.Patchables;
using Styx.WoWInternals;
using Styx.WoWInternals.WoWObjects;
using System.Linq;

namespace Paws.Core
{
    /// <summary>
    /// Extensions added to the LocalPlayer class.
    /// </summary>
    public static class PlayerExtensions
    {
        /// <summary>
        /// Determines if the player's current target is attackable.
        /// </summary>
        public static bool HasAttackableTarget(this LocalPlayer thisPlayer)
        {
            try
            {
                return
                    thisPlayer.CurrentTarget != null &&
                    (thisPlayer.CurrentTarget.Name.ToUpper().Contains("DUMMY")) ||
                    (thisPlayer.CurrentTarget.IsValid &&
                    thisPlayer.CurrentTarget.Attackable &&
                    thisPlayer.CurrentTarget.CanSelect &&
                    !thisPlayer.CurrentTarget.IsDead);
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Determines if the player is within melee distance of the current target.
        /// </summary>
        public static bool IsWithinMeleeDistanceOfTarget(this LocalPlayer thisPlayer)
        {
            return
                thisPlayer.CurrentTarget != null &&
                thisPlayer.CurrentTarget.IsWithinMeleeRange;
        }

        /// <summary>
        /// Determines if the player is in cat form (Includes Incarnation and Claws of Shirvallah).
        /// </summary>
        public static bool IsInCatForm(this LocalPlayer thisPlayer)
        {
            return
                thisPlayer.HasAura(SpellBook.CatForm) ||
                thisPlayer.HasAura(SpellBook.ClawsOfShirvallah) ||
                thisPlayer.HasAura(SpellBook.FeralIncarnationForm);
        }

        /// <summary>
        /// Determines if the player is in travel form: Travel, Flight, or Water
        /// </summary>
        public static bool IsInTravelForm(this LocalPlayer thisPlayer)
        {
            return
                thisPlayer.Shapeshift == Styx.ShapeshiftForm.Travel ||
                thisPlayer.Shapeshift == Styx.ShapeshiftForm.EpicFlightForm ||
                thisPlayer.Shapeshift == Styx.ShapeshiftForm.FlightForm ||
                thisPlayer.Shapeshift == Styx.ShapeshiftForm.Aqua;
        }

        /// <summary>
        /// Determines if the player has Savage Roar aura. Includes normal SR, Glyph of Savage Roar, and Glyph of Savagery
        /// </summary>
        public static bool HasSavageRoarAura(this LocalPlayer thisPlayer)
        {
            return
                thisPlayer.HasAura(SpellBook.SavageRoar) ||
                thisPlayer.HasAura(SpellBook.GlyphOfSavagery) ||
                thisPlayer.HasAura(SpellBook.GlyphOfSavageRoar);
        }

        /// <summary>
        /// Determines if the player currently has stats buff: Mark of the Wild, Blessing of Kings, Legacy of Emperor, or Legacy of White Tiger
        /// </summary>
        public static bool HasStatsBuff(this LocalPlayer thisPlayer)
        {
            return
                thisPlayer.HasAura(SpellBook.MarkOfTheWild) ||
                thisPlayer.HasAura(SpellBook.BlessingOfTheKings) ||
                thisPlayer.HasAura(SpellBook.LegacyOfTheEmperor) ||
                thisPlayer.HasAura(SpellBook.LegacyOfTheWhiteTiger);
        }

        /// <summary>
        /// Determines if the cast on the player's target can be interrupted.
        /// </summary>
        public static bool CanActuallyInterruptCurrentTargetSpellCast(this LocalPlayer thisPlayer, int milliseconds)
        {
            if (!thisPlayer.HasAttackableTarget()) return false;
            if (thisPlayer.CurrentTarget.IsChanneling && thisPlayer.CurrentTarget.ChanneledSpell != null)
            {
                return (thisPlayer.CurrentTarget.CurrentChannelTimeLeft.TotalMilliseconds > milliseconds) &&
                    (thisPlayer.CurrentTarget.CanInterruptCurrentSpellCast);
            }
            if (thisPlayer.CurrentTarget.IsCasting && thisPlayer.CurrentTarget.CastingSpell != null)
            {
                return (thisPlayer.CurrentTarget.CurrentCastTimeLeft.TotalMilliseconds > milliseconds) &&
                    (thisPlayer.CurrentTarget.CanInterruptCurrentSpellCast);
            }

            return false;
        }

        /// <summary>
        /// Determines if the player currently has lost control.
        public static bool HasLossOfControl(this LocalPlayer thisPlayer)
        {
            //foreach (var aura in thisPlayer.GetAllAuras())
            //{
            //    if (!aura.IsHarmful)
            //        continue;

            //    if (aura.Spell == null)
            //        continue;

            //    if (aura.Spell.Mechanic.HasFlag(WoWSpellMechanic.Asleep) ||
            //        aura.Spell.Mechanic.HasFlag(WoWSpellMechanic.Charmed) ||
            //        aura.Spell.Mechanic.HasFlag(WoWSpellMechanic.Disoriented) ||
            //        aura.Spell.Mechanic.HasFlag(WoWSpellMechanic.Fleeing) ||
            //        aura.Spell.Mechanic.HasFlag(WoWSpellMechanic.Horrified) ||
            //        aura.Spell.Mechanic.HasFlag(WoWSpellMechanic.Incapacitated) ||
            //        aura.Spell.Mechanic.HasFlag(WoWSpellMechanic.Sapped) ||
            //        aura.Spell.Mechanic.HasFlag(WoWSpellMechanic.Stunned) ||
            //        aura.Spell.Mechanic.HasFlag(WoWSpellMechanic.Polymorphed))
            //    {
            //        Log.Equipment(string.Format("Loss of control detected on me: {0} ({1})", aura.Spell.Name, aura.Spell.Mechanic));
            //        return true;
            //    }
            //}

            return false;
        }

        /// <summary>
        /// Determines if the player currently has total loss of control (cannot clear).
        public static bool HasTotalLossOfControl(this LocalPlayer thisPlayer)
        {
            //if (thisPlayer.HasLossOfControl()) return true;

            //foreach (var aura in thisPlayer.GetAllAuras())
            //{
            //    if (!aura.IsHarmful)
            //        continue;

            //    if (aura.Spell == null)
            //        continue;

            //    if (aura.Spell.Mechanic.HasFlag(WoWSpellMechanic.Banished) ||
            //        aura.Spell.Mechanic.HasFlag(WoWSpellMechanic.Frozen))
            //    {
            //        Log.Equipment(string.Format("Total Loss of control detected on me: {0} ({1})", aura.Spell.Name, aura.Spell.Mechanic));
            //        return true;
            //    }
            //}

            return false;
        }

        /// <summary>
        /// <para>Determines if the player currently has a root or snare effect</para>
        /// <para>Release 1.1.0</para>
        /// </summary>
        public static bool HasRootOrSnare(this LocalPlayer thisPlayer)
        {
            foreach (var aura in thisPlayer.GetAllAuras())
            {
                if (aura.Spell.Mechanic.HasFlag(WoWSpellMechanic.Rooted) ||
                    aura.Spell.Mechanic.HasFlag(WoWSpellMechanic.Snared))
                {
                    // Log.Equipment(string.Format("Snare or Root detected on me: {0} ({1})", aura.Spell.Name, aura.Spell.Mechanic));
                    return true;
                }
            }

            return false;
        }
    }

    /// <summary>
    /// Extensions added to WoWSpell spells.
    /// </summary>
    public static class SpellExtensions
    {
        /// <summary>
        /// Determines if the spell is on cooldown or not.
        /// </summary>
        public static bool IsOnCooldown(this WoWSpell thisSpell)
        {
            SpellFindResults spellFindResults;
            if (SpellManager.FindSpell(thisSpell.Id, out spellFindResults))
            {
                WoWSpell theSpell = spellFindResults.Override != null
                    ? spellFindResults.Override
                    : spellFindResults.Original;

                var timeLeft = SpellManager.GlobalCooldown 
                    ? (theSpell.CooldownTimeLeft.TotalMilliseconds - 1500) 
                    : theSpell.CooldownTimeLeft.TotalMilliseconds;

                // Log.GUI(string.Format("Spell {0} Base CD: {1}, CD: {2}, GCD: {3}", theSpell.Name, theSpell.BaseCooldown, theSpell.CooldownTimeLeft.TotalMilliseconds, SpellManager.GlobalCooldownLeft.TotalMilliseconds));

                return timeLeft <= 0;
            }

            return false;
        }
    }

    /// <summary>
    /// Extensions added to the BotBase class.
    /// </summary>
    public static class BotBaseExtensions
    {
        /// <summary>
        /// Determines if the current bot base is Enyo or Raid Bot.
        /// </summary>
        public static bool IsRoutineBased(this BotBase thisBotBase)
        {
            return
                thisBotBase.Name.ToUpper().Contains("ENYO") ||
                thisBotBase.Name.ToUpper().Contains("RAID BOT");
        }
    }

    /// <summary>
    /// Extensions added to the WoWUnit class.
    /// </summary>
    public static class UnitExtensions
    {
        /// <summary>
        /// Retrieves the last 4 characters of the Guid.
        /// </summary>
        public static string GetUnitId(this WoWUnit thisUnit)
        {
            return UnitManager.GuidToUnitID(thisUnit.Guid);
        }

        public static bool HasCancelableEnragedEffect(this WoWUnit thisUnit)
        {
            return thisUnit.GetAllAuras().Any(o => o.Name == "Enraged" && o.Flags.HasFlag(AuraFlags.Cancelable));
        }
    }
}