using Paws.Core.Conditions.Attributes;
using Styx;

namespace Paws.Core.Conditions
{
    /// <summary>
    /// Condition based on the player's current combo points.
    /// </summary>
    public class MyRageCondition : ICondition
    {
        /// <summary>
        /// The maximum number of combo points allowed.
        /// </summary>
        public const double MAX = 100;

        /// <summary>
        /// The minimum number of combo points allowed.
        /// </summary>
        public const double MIN = 0;

        /// <summary>
        /// The minimum number of combat points to satisfy the condition.
        /// </summary>
        public double Min { get; set; }

        /// <summary>
        /// The maximum number of combat points to satisfy the condition.
        /// </summary>
        public double Max { get; set; }

        public MyRageCondition()
            : this(MIN, MAX)
        { }

        public MyRageCondition(double min = MIN, double max = MAX)
        {
            this.Min = min;
            this.Max = max;
        }

        public bool Satisfied()
        {
            if (this.Min < MIN)
                throw new ConditionException(string.Format("The Min cannot be less than {0}.", MIN));

            if (this.Max > MAX)
                throw new ConditionException(string.Format("The Max cannot be greater than {0}.", MAX));

            if (this.Min > this.Max)
                throw new ConditionException("The Min cannot be greater than the Max.");

            return StyxWoW.Me.RagePercent >= this.Min && StyxWoW.Me.RagePercent <= this.Max;
        }
    }
}
