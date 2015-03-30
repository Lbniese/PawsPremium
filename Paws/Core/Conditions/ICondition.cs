namespace Paws.Core.Conditions
{
    /// <summary>
    /// Condition interface that all conditions adhere to.
    /// </summary>
    public interface ICondition
    {
        /// <summary>
        /// Checks the condition to determine if the specified parameteters are satisfied.
        /// </summary>
        /// <returns>Returns true if the condition is satisfied.</returns>
        bool Satisfied();
    }
}
