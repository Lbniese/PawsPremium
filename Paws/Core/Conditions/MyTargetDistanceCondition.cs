using Paws.Core.Conditions.Attributes;
using Paws.Core.Managers;
using Styx;
using System;

namespace Paws.Core.Conditions
{
    /// <summary>
    /// Condition based on if the player's current target is within the specified distance range.
    /// </summary>
    [ItemCondition(FriendlyName = "My Target's Distance From Me")]
    public class MyTargetDistanceCondition : ICondition
    {
        /// <summary>
        /// The minimum distance required to satisfy the condition.
        /// </summary>
        [ItemConditionParameter(Descriptor = "yd")]
        public double Min { get; set; }

        /// <summary>
        /// The maxiumum distance required to satisfy the condition.
        /// </summary>
        [ItemConditionParameter(Descriptor = "yd")]
        public double Max { get; set; }

        public MyTargetDistanceCondition()
            : this(0, 100)
        { }

        public MyTargetDistanceCondition(double min, double max)
        {
            this.Min = min;
            this.Max = max;
        }

        public bool Satisfied()
        {
            if (StyxWoW.Me.CurrentTarget == null || !StyxWoW.Me.CurrentTarget.IsValid)
                throw new ConditionException("The current target cannot be null or invalid");

            if (this.Min > this.Max)
                throw new ConditionException("The Min cannot be greater than the Max");

            return
                GlobalSettingsManager.Instance.UseCombatReachDistanceCalculations
                    ? Math.Abs((StyxWoW.Me.CurrentTarget.Distance - StyxWoW.Me.CurrentTarget.CombatReach)) >= this.Min && Math.Abs((StyxWoW.Me.CurrentTarget.Distance - StyxWoW.Me.CurrentTarget.CombatReach)) <= this.Max        
                    : StyxWoW.Me.CurrentTarget.Distance >= this.Min && StyxWoW.Me.CurrentTarget.Distance <= this.Max;
        }
    }
}
