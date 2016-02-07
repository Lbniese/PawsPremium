using System;
using Paws.Core.Conditions.Attributes;
using Paws.Core.Managers;
using Styx;

namespace Paws.Core.Conditions
{
    /// <summary>
    ///     Condition based on if the player's current target is within the specified distance range.
    /// </summary>
    [ItemCondition(FriendlyName = "My Target's Distance From Me")]
    public class MyTargetDistanceCondition : ICondition
    {
        public MyTargetDistanceCondition()
            : this(0, 100)
        {
        }

        public MyTargetDistanceCondition(double min, double max)
        {
            Min = min;
            Max = max;
        }

        /// <summary>
        ///     The minimum distance required to satisfy the condition.
        /// </summary>
        [ItemConditionParameter(Descriptor = "yd")]
        public double Min { get; set; }

        /// <summary>
        ///     The maxiumum distance required to satisfy the condition.
        /// </summary>
        [ItemConditionParameter(Descriptor = "yd")]
        public double Max { get; set; }

        public bool Satisfied()
        {
            if (StyxWoW.Me.CurrentTarget == null || !StyxWoW.Me.CurrentTarget.IsValid)
                throw new ConditionException("The current target cannot be null or invalid");

            if (Min > Max)
                throw new ConditionException("The Min cannot be greater than the Max");

            return
                GlobalSettingsManager.Instance.UseCombatReachDistanceCalculations
                    ? Math.Abs(StyxWoW.Me.CurrentTarget.Distance - StyxWoW.Me.CurrentTarget.CombatReach) >= Min &&
                      Math.Abs(StyxWoW.Me.CurrentTarget.Distance - StyxWoW.Me.CurrentTarget.CombatReach) <= Max
                    : StyxWoW.Me.CurrentTarget.Distance >= Min && StyxWoW.Me.CurrentTarget.Distance <= Max;
        }
    }
}