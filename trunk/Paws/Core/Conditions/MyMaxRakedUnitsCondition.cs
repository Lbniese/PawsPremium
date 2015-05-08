using Paws.Core.Conditions.Attributes;
using Paws.Core.Managers;
using Styx;

namespace Paws.Core.Conditions
{
    /// <summary>
    /// Condition based on the number of raked targets.
    /// </summary>
    [ItemCondition(FriendlyName = "Maximum Number of Raked Enemies")]
    public class MyMaxRakedUnitsCondition : ICondition
    {
        /// <summary>
        /// The maxiumum number of targets to satisfy the condition.
        /// </summary>
        [ItemConditionParameter]
        public int Count { get; set; }

        public MyMaxRakedUnitsCondition()
            : this(1)
        { }

        public MyMaxRakedUnitsCondition(int count)
        {
            this.Count = count;
        }

        public bool Satisfied()
        {
            return SnapshotManager.Instance.RakedTargets.Count < this.Count;
        }
    }
}
