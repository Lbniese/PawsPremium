namespace Paws.Core.Conditions
{
    /// <summary>
    ///     Condition based on a static boolean value, such as a setting.
    /// </summary>
    public class BooleanCondition : ICondition
    {
        public BooleanCondition(bool value)
        {
            Value = value;
        }

        /// <summary>
        ///     The value to determine if the condition has been satisfied.
        /// </summary>
        public bool Value { get; set; }

        public bool Satisfied()
        {
            return Value;
        }
    }
}