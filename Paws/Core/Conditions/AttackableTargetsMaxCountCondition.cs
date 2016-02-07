using Paws.Core.Conditions.Attributes;
using Paws.Core.Managers;

namespace Paws.Core.Conditions
{
    /// <summary>
    ///     Condition based on the number of surrounding attackable targets.
    /// </summary>
    [ItemCondition(FriendlyName = "Maximum Number of Surrounding Enemies")]
    public class AttackableTargetsMaxCountCondition : ICondition
    {
        public AttackableTargetsMaxCountCondition()
            : this(1)
        {
        }

        public AttackableTargetsMaxCountCondition(int count)
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
            return UnitManager.Instance.LastKnownSurroundingEnemies.Count <= Count;
        }
    }
}