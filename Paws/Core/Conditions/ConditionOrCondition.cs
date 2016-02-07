using System.Collections.Generic;

namespace Paws.Core.Conditions
{
    /// <summary>
    ///     Condition list that returns true if any condition is satisfied.
    /// </summary>
    public class ConditionOrList : List<ICondition>, ICondition
    {
        public ConditionOrList()
        {
        }

        public ConditionOrList(params ICondition[] conditions)
        {
            AddRange(conditions);
        }

        public bool Satisfied()
        {
            foreach (var condition in this)
            {
                if (condition.Satisfied()) return true;
            }

            return false;
        }
    }
}