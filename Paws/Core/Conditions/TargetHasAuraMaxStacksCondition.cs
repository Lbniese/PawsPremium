using Paws.Core.Conditions.Attributes;
using Paws.Core.Managers;
using Styx;
using Styx.WoWInternals;
using Styx.WoWInternals.WoWObjects;
using System.Linq;

namespace Paws.Core.Conditions
{
    /// <summary>
    /// Condition based on if the specified target has the provided aura.
    /// </summary>
    public class TargetHasAuraMaxStacksCondition : ICondition
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
        public int MaxStackCount { get; set; }

        /// <summary>
        /// The creator Guid used to determine if who created the aura.
        /// </summary>
        public WoWGuid CreatorGuid { get; set; }

        public TargetHasAuraMaxStacksCondition()
            : this(TargetType.Me, 0, 0)
        { }

        public TargetHasAuraMaxStacksCondition(TargetType target, int auraId, int maxStackCount)
        {
            this.Target = target;
            this.AuraId = auraId;
            this.MaxStackCount = maxStackCount;
            this.CreatorGuid = StyxWoW.Me.Guid;
        }

        public TargetHasAuraMaxStacksCondition(TargetType target, int auraId, int maxStackCount, WoWGuid creatorGuid)
        {
            this.Target = target;
            this.AuraId = auraId;
            this.MaxStackCount = maxStackCount;
            this.CreatorGuid = creatorGuid;
        }

        public bool Satisfied()
        {
            var target = UnitManager.TargetTypeConverter(this.Target);

            if (target == null || !target.IsValid)
                return false;

            return target.GetAllAuras().SingleOrDefault(o => 
                o.SpellId == this.AuraId && 
                o.StackCount < this.MaxStackCount && 
                o.CreatorGuid == this.CreatorGuid) != null;
        }
    }
}
