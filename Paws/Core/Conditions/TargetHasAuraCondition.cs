using System.Linq;
using Styx;
using Styx.WoWInternals;

namespace Paws.Core.Conditions
{
    /// <summary>
    ///     Condition based on if the specified target has the provided aura.
    /// </summary>
    public class TargetHasAuraCondition : ICondition
    {
        public TargetHasAuraCondition()
            : this(TargetType.Me, 0)
        {
        }

        public TargetHasAuraCondition(TargetType target, int auraId)
        {
            Target = target;
            AuraId = auraId;
            CreatorGuid = StyxWoW.Me.Guid;
        }

        public TargetHasAuraCondition(TargetType target, int auraId, WoWGuid creatorGuid)
        {
            Target = target;
            AuraId = auraId;
            CreatorGuid = creatorGuid;
        }

        /// <summary>
        ///     The target used to satisfy the condition against.
        /// </summary>
        public TargetType Target { get; set; }

        /// <summary>
        ///     The aura id used to determine if the target has active.
        /// </summary>
        public int AuraId { get; set; }

        /// <summary>
        ///     The creator Guid used to determine if who created the aura.
        /// </summary>
        public WoWGuid CreatorGuid { get; set; }

        public bool Satisfied()
        {
            var target = Target == TargetType.Me ? StyxWoW.Me : StyxWoW.Me.CurrentTarget;

            if (target == null || !target.IsValid)
                throw new ConditionException("Target cannot be null or invalid.");

            return target.GetAllAuras().SingleOrDefault(o => o.SpellId == AuraId && o.CreatorGuid == CreatorGuid) !=
                   null;
        }
    }
}