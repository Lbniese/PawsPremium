using Paws.Core.Conditions.Attributes;
using Styx;

namespace Paws.Core.Conditions
{
    /// <summary>
    ///     Condition based on if the specified target is within the provided health range percentage.
    /// </summary>
    [ItemCondition(FriendlyName = "Health Percent Range")]
    public class TargetHealthRangeCondition : ICondition
    {
        /// <summary>
        ///     The minimum health percentage allowed.
        /// </summary>
        private static double MIN;

        /// <summary>
        ///     The maximum health percentage allowed.
        /// </summary>
        public const double MAX = 100;

        public TargetHealthRangeCondition()
            : this(TargetType.Me, MIN, MAX)
        {
        }

        public TargetHealthRangeCondition(TargetType target, double min, double max)
        {
            Target = target;
            MIN = min;
            Max = max;
        }

        /// <summary>
        ///     The target used to satisfy the condition against.
        /// </summary>
        [ItemConditionParameter("Me", TargetType.Me, "My Target", TargetType.MyCurrentTarget)]
        public TargetType Target { get; set; }

        /// <summary>
        ///     The minimum health percentage required to satisfy the condition.
        /// </summary>
        [ItemConditionParameter(Descriptor = "%")]
        public double Min { get; set; }

        /// <summary>
        ///     The maximum health percentage required to satisfy the condition.
        /// </summary>
        [ItemConditionParameter(Descriptor = "%")]
        public double Max { get; set; }

        public bool Satisfied()
        {
            var target = Target == TargetType.Me ? StyxWoW.Me : StyxWoW.Me.CurrentTarget;

            if (target == null || !target.IsValid)
                throw new ConditionException("Target cannot be null or invalid.");

            if (MIN < 0 || Max > 100)
                throw new ConditionException("Min and Max must be clamped at 0 and 100 respectively.");

            if (MIN > Max)
                throw new ConditionException("Min cannot be greater than Max.");

            return target.HealthPercent >= MIN && target.HealthPercent <= Max;
        }
    }
}