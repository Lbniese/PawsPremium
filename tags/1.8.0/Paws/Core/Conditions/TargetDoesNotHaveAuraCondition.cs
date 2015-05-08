using Paws.Core.Conditions.Attributes;
using Styx;
using Styx.WoWInternals;
using Styx.WoWInternals.WoWObjects;
using System.Linq;

namespace Paws.Core.Conditions
{
    /// <summary>
    /// Condition based on if the specified target does not have the provided aura.
    /// </summary>
    public class TargetDoesNotHaveAuraCondition : ICondition
    {
        /// <summary>
        /// The target used to satisfy the condition against.
        /// </summary>
        public TargetType Target { get; set; }

        /// <summary>
        /// The aura id used to determine if the target does not have active.
        /// </summary>
        public int AuraId { get; set; }

        /// <summary>
        /// The creator Guid used to determine if who created the aura.
        /// </summary>
        public WoWGuid CreatorGuid { get; set; }

        public TargetDoesNotHaveAuraCondition()
            : this(TargetType.Me, 0)
        { }

        public TargetDoesNotHaveAuraCondition(TargetType target, int auraId)
        {
            this.Target = target;
            this.AuraId = auraId;
            this.CreatorGuid = StyxWoW.Me.Guid;
        }

        public TargetDoesNotHaveAuraCondition(TargetType target, int auraId, WoWGuid creatorGuid)
        {
            this.Target = target;
            this.AuraId = auraId;
            this.CreatorGuid = creatorGuid;
        }

        public bool Satisfied()
        {
            var target = this.Target == TargetType.Me ? StyxWoW.Me : StyxWoW.Me.CurrentTarget;

            if (target == null || !target.IsValid)
                throw new ConditionException("Target cannot be null or invalid.");

            return target.GetAllAuras().SingleOrDefault(o => o.SpellId == this.AuraId && o.CreatorGuid == this.CreatorGuid) == null;
        }
    }
}
