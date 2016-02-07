using System;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media;
using Paws.Core.Managers;
using Paws.Core.Utilities;
using Styx;
using Styx.CommonBot.Coroutines;
using Styx.WoWInternals.WoWObjects;

namespace Paws.Core
{
    /// <summary>
    ///     Provides movement helper methods.
    /// </summary>
    public static class MovementHelper
    {
        private static LocalPlayer Me
        {
            get { return StyxWoW.Me; }
        }

        private static WoWUnit MyCurrentTarget
        {
            get { return Me.CurrentTarget; }
        }

        [DllImport("user32.dll")]
        private static extern short GetAsyncKeyState(Keys vKey);

        /// <summary>
        ///     (Non-Blocking) Attempts to move to the player's current target.
        /// </summary>
        /// <returns>Returns true if we are able to move towards the target.</returns>
        public static async Task MoveToMyCurrentTarget()
        {
            if (MyCurrentTarget == null)
                return;

            try
            {
                // Movement key detection routine courtesy of pasterke
                if (GetAsyncKeyState(Keys.W) != 0 ||
                    GetAsyncKeyState(Keys.S) != 0 ||
                    GetAsyncKeyState(Keys.D) != 0 ||
                    GetAsyncKeyState(Keys.A) != 0) return;

                await MoveToTarget(MyCurrentTarget,
                    () =>
                        MyCurrentTarget != null &&
                        SettingsManager.Instance.AllowMovement &&
                        Me.HasAttackableTarget() &&
                        Me.KnowsSpell(SpellBook.CatForm)
                            ? !Me.IsWithinMeleeDistanceOfTarget()
                            : MyCurrentTarget.Distance > 38 && MyCurrentTarget.InLineOfSpellSight);

                await MoveStop(
                    () =>
                        MyCurrentTarget != null &&
                        SettingsManager.Instance.AllowMovement &&
                        Me.KnowsSpell(SpellBook.CatForm)
                            ? Me.IsWithinMeleeDistanceOfTarget()
                            : MyCurrentTarget.Distance <= 38 && MyCurrentTarget.InLineOfSpellSight);
            }
            catch (Exception ex)
            {
                Log.Diagnostics(string.Format("Exception caught while trying to move: {0}", ex.Message));
            }
        }

        /// <summary>
        ///     (Non-Blocking) Attempts to move to the specified target.
        /// </summary>
        /// <returns>Returns true if we are able to move towards the target.</returns>
        public static async Task MoveToTarget(WoWUnit target, Func<bool> conditionCheck = null)
        {
            if (conditionCheck != null && !conditionCheck())
                return;

            if (target == null)
                return;

            if (target.IsDead)
                return;

            await CommonCoroutines.MoveTo(target.Location);
        }

        /// <summary>
        ///     (Non-Blocking) Stop the player from moving.
        /// </summary>
        /// <returns>Returns true if we are able to stop moving.</returns>
        public static async Task MoveStop(Func<bool> conditionCheck = null)
        {
            if (conditionCheck != null && !conditionCheck())
                return;

            await CommonCoroutines.StopMoving();
        }

        /// <summary>
        ///     (Non-Blocking) Attempts to face the player's current target.
        /// </summary>
        /// <returns>Returns true if we are able to safely face the target</returns>
        public static async Task FaceMyCurrentTarget()
        {
            if (MyCurrentTarget == null)
                return;

            // Movement key detection routine courtesy of pasterke
            if (GetAsyncKeyState(Keys.LButton) != 0
                && GetAsyncKeyState(Keys.RButton) != 0) return;

            await FaceTarget(MyCurrentTarget,
                () =>
                    MyCurrentTarget != null &&
                    SettingsManager.Instance.AllowTargetFacing &&
                    !Me.IsMoving &&
                    !Me.IsSafelyFacing(MyCurrentTarget));
        }

        /// <summary>
        ///     (Non-Blocking) Attempts to face the specified target.
        /// </summary>
        /// <returns>Returns true if we are able to safely face the target</returns>
        public static async Task FaceTarget(WoWUnit target, Func<bool> conditionCheck = null)
        {
            if (conditionCheck != null && !conditionCheck())
                return;

            target.Face();
            await CommonCoroutines.SleepForLagDuration();
        }

        /// <summary>
        ///     (Non-Blocking) Attempts to clear the player's current target.
        /// </summary>
        /// <returns>Returns true if we are able to clear the target</returns>
        public static async Task<bool> ClearMyDeadTarget()
        {
            if (MyCurrentTarget == null || !MyCurrentTarget.IsDead) return false;
            Me.ClearTarget();
            Log.AppendLine(
                string.Format("Clearing dead target {0} [{1}]", MyCurrentTarget.SafeName, MyCurrentTarget.Guid),
                Colors.Bisque);

            await CommonCoroutines.SleepForLagDuration();

            return false;
        }
    }
}