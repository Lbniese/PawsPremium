using Paws.Core.Conditions.Attributes;
using Paws.Core.Managers;

namespace Paws.Core.Conditions
{
    /// <summary>
    ///     Condition based on the number of raked targets.
    /// </summary>
    [ItemCondition(FriendlyName = "Maximum Number of Raked Enemies")]
    public class MyMaxRakedUnitsCondition : ICondition
    {
        public MyMaxRakedUnitsCondition()
            : this(1)
        {
        }

        public MyMaxRakedUnitsCondition(int count)
        {
            Count = count;
        }

        /// <summary>
        ///     The maxiumum number of targets to satisfy the condition.
        /// </summary>
        [ItemConditionParameter]
        public int Count { get; set; }

        public bool Satisfied()
        {
            return SnapshotManager.Instance.RakedTargets.Count < Count;
        }
    }
}