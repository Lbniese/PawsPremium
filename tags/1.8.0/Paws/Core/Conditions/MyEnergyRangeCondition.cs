using Paws.Core.Conditions.Attributes;
using Styx;

namespace Paws.Core.Conditions
{
    /// <summary>
    /// Condition based on the player's current energy percentage range.
    /// </summary>
    [ItemCondition(FriendlyName = "My Energy Range")]
    public class MyEnergyRangeCondition : ICondition
    {
        /// <summary>
        /// The minimum energy percentage allowed.
        /// </summary>
        public const double MIN = 0.0;

        /// <summary>
        /// The maximum energy percentage allowed.
        /// </summary>
        public const double MAX = 100.0;

        /// <summary>
        /// The minumum energy percentage to satisfy the condition.
        /// </summary>
        [ItemConditionParameter(Descriptor = "%")]
        public double Min { get; set; }

        /// <summary>
        /// The maximum energy percentage to satisfy the condition.
        /// </summary>
        [ItemConditionParameter(Descriptor = "%")]
        public double Max { get; set; }

        public MyEnergyRangeCondition()
            : this(MIN, MAX)
        { }

        public MyEnergyRangeCondition(double min = MIN, double max = MAX)
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

            return StyxWoW.Me.EnergyPercent >= this.Min && StyxWoW.Me.EnergyPercent <= this.Max;
        }
    }
}
