using System.Collections.Generic;
using System.Linq;

namespace Paws.Core.Conditions
{
    /// <summary>
    ///     Condition dependency list where each condition must be satisfied to return true.
    /// </summary>
    public class ConditionDependencyList : List<ICondition>, ICondition
    {
        public ConditionDependencyList()
        {
        }

        public ConditionDependencyList(params ICondition[] conditions)
        {
            AddRange(conditions);
        }

        public bool Satisfied()
        {
            return this.All(item => item.Satisfied());
        }
    }
}