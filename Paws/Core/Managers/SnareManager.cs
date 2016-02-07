using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Media;
using Paws.Core.Abilities.Feral;
using Paws.Core.Utilities;
using Styx;
using Styx.WoWInternals.WoWObjects;

namespace Paws.Core.Managers
{
    /// <summary>
    ///     Provides the management of root and snare timers and methods.
    /// </summary>
    public static class SnareManager
    {
        public const int BearFormBlockedMs = 8000;
        private static readonly Stopwatch SnareTimer = new Stopwatch();

        public static Stopwatch BearFormBlockedTimer = new Stopwatch();

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

        /// <summary>
        ///     Checks for roots or snares. Attempts to clear them if they exist.
        /// </summary>
        /// <returns></returns>
        public static async Task<bool> CheckAndClear()
        {
            if (SettingsManager.Instance.BearFormPowerShiftEnabled)
            {
                // ublock bear form after the specified period of time
                if (BearFormBlockedTimer.ElapsedMilliseconds >= BearFormBlockedMs)
                {
                    Log.Gui(string.Format("Bear Form unblocked after {0} ms.", BearFormBlockedTimer.ElapsedMilliseconds));
                    BearFormBlockedTimer.Reset();
                }
            }

            if (Me.HasRootOrSnare())
            {
                if (!SnareTimer.IsRunning)
                {
                    SnareTimer.Start();
                    return false;
                }
                if (SnareTimer.ElapsedMilliseconds >= SettingsManager.Instance.SnareReactionTimeInMs)
                {
                    if (!BearFormBlockedTimer.IsRunning)
                    {
                        if (await Abilities.Cast<BearFormPowerShiftAbility>(Me))
                        {
                            BearFormBlockedTimer.Start();
                            Log.Gui(string.Format("Blocking Bear Form for {0} ms due to snare powershift.",
                                BearFormBlockedMs));
                            return ReturnSuccessWithMessage(SnareTimer.ElapsedMilliseconds);
                        }
                    }
                    if (await Abilities.Cast<RemoveSnareWithStampedingRoarAbility>(Me))
                        return ReturnSuccessWithMessage(SnareTimer.ElapsedMilliseconds);
                    if (await Abilities.Cast<RemoveSnareWithDashAbility>(Me))
                        return ReturnSuccessWithMessage(SnareTimer.ElapsedMilliseconds);
                }
            }

            return false;
        }

        /// <summary>
        ///     Helper method to return a success message back to the caller.
        /// </summary>
        private static bool ReturnSuccessWithMessage(double elapsedMs)
        {
            Log.AppendLine(string.Format("Removed root or snare after {0} milliseconds.", elapsedMs), Colors.Gold);

            SnareTimer.Reset();
            return true;
        }
    }
}