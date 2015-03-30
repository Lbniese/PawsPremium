using Paws.Core.Abilities.Feral;
using Paws.Core.Utilities;
using Styx;
using Styx.WoWInternals.WoWObjects;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Paws.Core.Managers
{
    /// <summary>
    /// Provides the management of root and snare timers and methods.
    /// </summary>
    public static class SnareManager
    {
        private static Stopwatch _snareTimer = new Stopwatch();

        private static LocalPlayer Me { get { return StyxWoW.Me; } }
        private static WoWUnit MyCurrentTarget { get { return Me.CurrentTarget; } }
        private static AbilityManager Abilities { get { return AbilityManager.Instance; } }

        public static Stopwatch BearFormBlockedTimer = new Stopwatch();
        public const int BEAR_FORM_BLOCKED_MS = 8000;

        /// <summary>
        /// Checks for roots or snares. Attempts to clear them if they exist.
        /// </summary>
        /// <returns></returns>
        public static async Task<bool> CheckAndClear()
        {
            if (SettingsManager.Instance.BearFormPowerShiftEnabled)
            {
                // ublock bear form after the specified period of time
                if (BearFormBlockedTimer.ElapsedMilliseconds >= BEAR_FORM_BLOCKED_MS)
                {
                    Log.GUI(string.Format("Bear Form unblocked after {0} ms.", BearFormBlockedTimer.ElapsedMilliseconds));
                    BearFormBlockedTimer.Reset();
                }
            }

            if (Me.HasRootOrSnare())
            {
                if (!_snareTimer.IsRunning)
                {
                    _snareTimer.Start();
                    return false;
                }
                if (_snareTimer.ElapsedMilliseconds >= SettingsManager.Instance.SnareReactionTimeInMs)
                {
                    if (!BearFormBlockedTimer.IsRunning)
                    {
                        if (await Abilities.Cast<BearFormPowerShiftAbility>(Me))
                        {
                            BearFormBlockedTimer.Start();
                            Log.GUI(string.Format("Blocking Bear Form for {0} ms due to snare powershift.", BEAR_FORM_BLOCKED_MS));
                            return ReturnSuccessWithMessage(_snareTimer.ElapsedMilliseconds);
                        }
                    }
                    if (await Abilities.Cast<RemoveSnareWithStampedingRoarAbility>(Me)) return ReturnSuccessWithMessage(_snareTimer.ElapsedMilliseconds);
                    if (await Abilities.Cast<RemoveSnareWithDashAbility>(Me)) return ReturnSuccessWithMessage(_snareTimer.ElapsedMilliseconds);
                }
            }

            return false;
        }

        /// <summary>
        /// Helper method to return a success message back to the caller.
        /// </summary>
        private static bool ReturnSuccessWithMessage(double elapsedMs)
        {
            Log.AppendLine(string.Format("Removed root or snare after {0} milliseconds.", elapsedMs), Colors.Gold);

            _snareTimer.Reset();
            return true;
        }
    }
}
