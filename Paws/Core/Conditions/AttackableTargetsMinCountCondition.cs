using Paws.Core.Conditions.Attributes;
using Paws.Core.Managers;

namespace Paws.Core.Conditions
{
    /// <summary>
    ///     Condition based on the number of surrounding attackable targets.
    /// </summary>
    [ItemCondition(FriendlyName = "Minimum Number of Surrounding Enemies")]
    public class AttackableTargetsMinCountCondition : ICondition
    {
        public AttackableTargetsMinCountCondition()
            : this(1)
        {
        }

        public AttackableTargetsMinCountCondition(int count)
        {
            Count = count;
        }

        /// <summary>
        ///     The minimum number of targets to satisfy the condition.
        /// </summary>
        [ItemConditionParameter]
        public int Count { get; set; }

        public bool Satisfied()
        {
            return UnitManager.Instance.LastKnownSurroundingEnemies.Count >= Count;
        }
    }
}