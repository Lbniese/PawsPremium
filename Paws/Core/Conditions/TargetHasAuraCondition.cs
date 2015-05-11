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
    public class TargetHasAuraCondition : ICondition
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
        /// The creator Guid used to determine if who created the aura.
        /// </summary>
        public WoWGuid CreatorGuid { get; set; }

        public TargetHasAuraCondition()
            : this(TargetType.Me, 0)
        { }

        public TargetHasAuraCondition(TargetType target, int auraId)
        {
            this.Target = target;
            this.AuraId = auraId;
            this.CreatorGuid = StyxWoW.Me.Guid;
        }

        public TargetHasAuraCondition(TargetType target, int auraId, WoWGuid creatorGuid)
        {
            this.Target = target;
            this.AuraId = auraId;
            this.CreatorGuid = creatorGuid;
        }

        public bool Satisfied()
        {
            var target = UnitManager.TargetTypeConverter(this.Target);

            if (target == null || !target.IsValid)
                return false;

            return target.GetAllAuras().SingleOrDefault(o => o.SpellId == this.AuraId && o.CreatorGuid == this.CreatorGuid) != null;
        }
    }
}
