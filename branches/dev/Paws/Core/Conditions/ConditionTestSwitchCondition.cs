namespace Paws.Core.Conditions
{
    /// <summary>
    /// Condition that tests a condition result first and returns a different condition based on the result of the test condition.
    /// </summary>
    public class ConditionTestSwitchCondition : ICondition
    {
        /// <summary>
        /// The condition to test.
        /// </summary>
        public ICondition TestCondition { get; set; }
        
        /// <summary>
        /// The condition to return if the test condition is satisfied.
        /// </summary>
        public ICondition ConditionIfTestIsTrue { get; set; }

        /// <summary>
        /// The condition to return if the test condition is not satisfied.
        /// </summary>
        public ICondition ConditionIfTestIsFalse { get; set; }

        public ConditionTestSwitchCondition(ICondition testCondition, ICondition conditionIfTestIsTrue, bool returnValueIfTestIsFalse = true)
            : this(testCondition, conditionIfTestIsTrue, new BooleanCondition(returnValueIfTestIsFalse))
        { }

        public ConditionTestSwitchCondition(ICondition testCondition, ICondition conditionIfTestIsTrue, ICondition conditionIfTestIsFalse)
        {
            this.TestCondition = testCondition;
            this.ConditionIfTestIsTrue = conditionIfTestIsTrue;
            this.ConditionIfTestIsFalse = conditionIfTestIsFalse;
        }

        public bool Satisfied()
        {
            return this.TestCondition.Satisfied()
                ? this.ConditionIfTestIsTrue.Satisfied()
                : this.ConditionIfTestIsFalse.Satisfied();
        }
    }
}