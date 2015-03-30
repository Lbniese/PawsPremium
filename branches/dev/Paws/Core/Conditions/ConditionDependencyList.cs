using System.Collections.Generic;

namespace Paws.Core.Conditions
{
    /// <summary>
    /// Condition dependency list where each condition must be satisfied to return true.
    /// </summary>
    public class ConditionDependencyList : List<ICondition>, ICondition
    {
        public ConditionDependencyList()
            : base()
        {
        }

        public ConditionDependencyList(params ICondition[] conditions)
        {
            this.AddRange(conditions);
        }

        public bool Satisfied()
        {
            foreach (var item in this)
            {
                if (!item.Satisfied()) return false;
            }

            return true;
        }
    }
}