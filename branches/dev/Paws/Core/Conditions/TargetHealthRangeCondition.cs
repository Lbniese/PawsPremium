using Paws.Core.Conditions.Attributes;
using Paws.Core.Managers;
using Styx;
using Styx.WoWInternals.WoWObjects;
using System;

namespace Paws.Core.Conditions
{
    /// <summary>
    /// Condition based on if the specified target is within the provided health range percentage.
    /// </summary>
    [ItemCondition(FriendlyName = "Health Percent Range")]
    public class TargetHealthRangeCondition : ICondition
    {
        /// <summary>
        /// The minimum health percentage allowed.
        /// </summary>
        public const double MIN = 0;

        /// <summary>
        /// The maximum health percentage allowed.
        /// </summary>
        public const double MAX = 100;

        /// <summary>
        /// The target used to satisfy the condition against.
        /// </summary>
        [ItemConditionParameter("Me", TargetType.Me, "My Target", TargetType.MyCurrentTarget)]
        public TargetType Target { get; set; }

        /// <summary>
        /// The minimum health percentage required to satisfy the condition.
        /// </summary>
        [ItemConditionParameter(Descriptor = "%")]
        public double Min { get; set; }

        /// <summary>
        /// The maximum health percentage required to satisfy the condition.
        /// </summary>
        [ItemConditionParameter(Descriptor = "%")]
        public double Max { get; set; }

        public TargetHealthRangeCondition()
            : this(TargetType.Me, MIN, MAX)
        { }

        public TargetHealthRangeCondition(TargetType target, double min, double max)
        {
            this.Target = target;
            this.Min = min;
            this.Max = max;
        }

        public bool Satisfied()
        {
            var target = UnitManager.TargetTypeConverter(this.Target);

            if (target == null || !target.IsValid)
                return false;

            if (this.Min < 0 || this.Max > 100)
                throw new ConditionException("Min and Max must be clamped at 0 and 100 respectively.");

            if (this.Min > this.Max)
                throw new ConditionException("Min cannot be greater than Max.");

            return target.HealthPercent >= this.Min && target.HealthPercent <= this.Max;
        }
    }
}
