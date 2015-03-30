using Paws.Core.Conditions.Attributes;
using Styx;

namespace Paws.Core.Conditions
{
    /// <summary>
    /// Condition based on the player's current combo points.
    /// </summary>
    [ItemCondition(FriendlyName = "My Combo Points")]
    public class MyComboPointsCondition : ICondition
    {
        /// <summary>
        /// The maximum number of combo points allowed.
        /// </summary>
        public const int MAX = 5;

        /// <summary>
        /// The minimum number of combo points allowed.
        /// </summary>
        public const int MIN = 0;

        /// <summary>
        /// The minimum number of combat points to satisfy the condition.
        /// </summary>
        [ItemConditionParameter]
        public int Min { get; set; }

        /// <summary>
        /// The maximum number of combat points to satisfy the condition.
        /// </summary>
        [ItemConditionParameter]
        public int Max { get; set; }

        public MyComboPointsCondition()
            : this(MIN, MAX)
        { }

        public MyComboPointsCondition(int min = MIN, int max = MAX)
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

            //Paws.Core.Utilities.Log.GUI("Combo Points: " + StyxWoW.Me.ComboPoints.ToString() + ", Min=" + this.Min.ToString() + ", Max=" + this.Max.ToString());

            return StyxWoW.Me.ComboPoints >= this.Min && StyxWoW.Me.ComboPoints <= this.Max;
        }
    }
}
