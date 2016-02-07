using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Media;
using Paws.Core.Abilities.Feral;
using Paws.Core.Abilities.Shared;
using Paws.Core.Utilities;
using Styx;
using Styx.CommonBot.Coroutines;
using Styx.WoWInternals.WoWObjects;

namespace Paws.Core.Managers
{
    /// <summary>
    ///     Provides the management of interrupt timers, targets, and methods.
    /// </summary>
    public static class InterruptManager
    {
        private static readonly Stopwatch InterruptTimer = new Stopwatch();
        private static WoWUnit _lastInterruptableTarget;
        private static int _interruptRandomIntervalInMilliseconds = 1000;
        private static bool _interruptRollIsSuccessful;

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
        ///     <para>(Non-Blocking) Checks the target to determine if it is a candidate for interrupting the current spell.</para>
        ///     <para>
        ///         If the target is a candidate, it will attempt to interrupt based on the condition of the settings and
        ///         available interrupt abilities.
        ///     </para>
        /// </summary>
        /// <returns>Returns true on a successful interrupt</returns>
        public static async Task<bool> CheckMyTarget()
        {
            if (!Me.CanActuallyInterruptCurrentTargetSpellCast(SettingsManager.Instance.InterruptMinMilliseconds))
                return false;
            if (MyCurrentTarget == _lastInterruptableTarget)
            {
                if (InterruptTimer.ElapsedMilliseconds < _interruptRandomIntervalInMilliseconds) return false;
                if (_interruptRollIsSuccessful)
                {
                    if (await Abilities.Cast<SkullBashAbility>(MyCurrentTarget))
                        return ReturnSuccessWithMessage(InterruptTimer.ElapsedMilliseconds);
                    if (await Abilities.Cast<MightyBashAbility>(MyCurrentTarget))
                        return ReturnSuccessWithMessage(InterruptTimer.ElapsedMilliseconds);
                    if (await Abilities.Cast<TyphoonAbility>(MyCurrentTarget))
                        return ReturnSuccessWithMessage(InterruptTimer.ElapsedMilliseconds);
                    if (await Abilities.Cast<MaimAbility>(MyCurrentTarget))
                        return ReturnSuccessWithMessage(InterruptTimer.ElapsedMilliseconds);
                }
                else
                {
                    Log.AppendLine(
                        string.Format("Roll failed to complete the Interrupt on {0} [{1}] after {2} milliseconds.",
                            MyCurrentTarget.SafeName,
                            UnitManager.GuidToUnitId(MyCurrentTarget.Guid),
                            InterruptTimer.ElapsedMilliseconds),
                        Colors.Gold);

                    InterruptTimer.Reset();
                    _interruptRollIsSuccessful = !_interruptRollIsSuccessful;
                    _lastInterruptableTarget = null;

                    return false;
                }
            }
            else
            {
                await SetupRandomInterruptTimer();
            }

            return false;
        }

        /// <summary>
        ///     (Non-Blocking) Sets up a random interrupt timer based on the interrupt settings and success rate requirements.
        /// </summary>
        private static async Task SetupRandomInterruptTimer()
        {
            var rand = new Random();
            _interruptRollIsSuccessful = rand.Next(1, 101) <= SettingsManager.Instance.InterruptSuccessRate;

            _interruptRandomIntervalInMilliseconds = rand.Next(SettingsManager.Instance.InterruptMinMilliseconds,
                SettingsManager.Instance.InterruptMaxMilliseconds);
            _lastInterruptableTarget = MyCurrentTarget;
            InterruptTimer.Restart();

            Log.Diagnostics(string.Format("(Interrupt Roll {2}) Setting Interrupt Timer on {0} [{1}] for {3} ms",
                MyCurrentTarget.SafeName,
                UnitManager.GuidToUnitId(MyCurrentTarget.Guid),
                _interruptRollIsSuccessful ? "is successful" : "failed", _interruptRandomIntervalInMilliseconds));

            await CommonCoroutines.SleepForLagDuration();
        }

        /// <summary>
        ///     Helper method to return a success message back to the caller.
        /// </summary>
        private static bool ReturnSuccessWithMessage(double elapsedMs)
        {
            Log.AppendLine(
                string.Format("Interrupted {0} [{1}] after {2} milliseconds.", MyCurrentTarget.SafeName,
                    UnitManager.GuidToUnitId(MyCurrentTarget.Guid), elapsedMs), Colors.Gold);

            InterruptTimer.Reset();
            _interruptRollIsSuccessful = !_interruptRollIsSuccessful;
            _lastInterruptableTarget = null;

            return true;
        }
    }
}