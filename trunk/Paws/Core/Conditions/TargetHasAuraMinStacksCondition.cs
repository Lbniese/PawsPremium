using Paws.Core.Conditions.Attributes;
using Styx;
using Styx.WoWInternals;
using Styx.WoWInternals.WoWObjects;
using System.Linq;

namespace Paws.Core.Conditions
{
    /// <summary>
    /// Condition based on if the specified target has the provided aura.
    /// </summary>
    public class TargetHasAuraMinStacksCondition : ICondition
    {
        /// <summary>
        /// The target used to satisfy the condition against.
        /// </summary>
        public TargetType Target { get; set; }

        /// <summary>
        /// The aura id used to determine if the target has active.
        /// </summary>
        public int AuraId { get; set; }

        /// <summary>
        /// The number of stacks the target must have to satisfy this condition.
        /// </summary>
        public int MinStackCount { get; set; }

        /// <summary>
        /// The creator Guid used to determine if who created the aura.
        /// </summary>
        public WoWGuid CreatorGuid { get; set; }

        public TargetHasAuraMinStacksCondition()
            : this(TargetType.Me, 0, 0)
        { }

        public TargetHasAuraMinStacksCondition(TargetType target, int auraId, int minStackCount)
        {
            this.Target = target;
            this.AuraId = auraId;
            this.MinStackCount = minStackCount;
            this.CreatorGuid = StyxWoW.Me.Guid;
        }

        public TargetHasAuraMinStacksCondition(TargetType target, int auraId, int minStackCount, WoWGuid creatorGuid)
        {
            this.Target = target;
            this.AuraId = auraId;
            this.MinStackCount = minStackCount;
            this.CreatorGuid = creatorGuid;
        }

        public bool Satisfied()
        {
            var target = this.Target == TargetType.Me ? StyxWoW.Me : StyxWoW.Me.CurrentTarget;

            if (target == null || !target.IsValid)
                throw new ConditionException("Target cannot be null or invalid.");

            return target.GetAllAuras().SingleOrDefault(o => 
                o.SpellId == this.AuraId && 
                o.StackCount >= this.MinStackCount && 
                o.CreatorGuid == this.CreatorGuid) != null;
        }
    }
}
