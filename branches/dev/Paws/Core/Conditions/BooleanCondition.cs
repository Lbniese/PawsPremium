namespace Paws.Core.Conditions
{
    /// <summary>
    /// Condition based on a static boolean value, such as a setting.
    /// </summary>
    public class BooleanCondition : ICondition
    {
        /// <summary>
        /// The value to determine if the condition has been satisfied.
        /// </summary>
        public bool Value { get; set; }

        public BooleanCondition(bool value)
        {
            this.Value = value;
        }

        public bool Satisfied()
        {
            return this.Value;
        }
    }
}
