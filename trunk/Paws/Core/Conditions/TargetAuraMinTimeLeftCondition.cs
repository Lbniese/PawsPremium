using Styx;
using Styx.WoWInternals;
using Styx.WoWInternals.WoWObjects;
using System;
using System.Linq;

namespace Paws.Core.Conditions
{
    /// <summary>
    /// Condition based on if the specified target has an aura that is less than or equal to the provided time.
    /// </summary>
    public class TargetAuraMinTimeLeftCondition : ICondition
    {
        /// <summary>
        /// The target used to satisfy the condition against.
        /// </summary>
        public TargetType Target { get; set; }

        /// <summary>
        /// The aura id used to determine the time left to satisfy the condition.
        /// </summary>
        public int AuraId { get; set; }

        /// <summary>
        /// The creator Guid used to determine if who created the aura.
        /// </summary>
        public WoWGuid CreatorGuid { get; set; }

        /// <summary>
        /// The minimum amount of time left to satisfy the condition.
        /// </summary>
        public TimeSpan MinTimeLeft { get; set; }

        public TargetAuraMinTimeLeftCondition(TargetType target, int auraId, TimeSpan minTimeLeft)
        {
            this.Target = target;
            this.AuraId = auraId;
            this.CreatorGuid = StyxWoW.Me.Guid;
            this.MinTimeLeft = minTimeLeft;
        }

        public TargetAuraMinTimeLeftCondition(TargetType target, int auraId, WoWGuid creatorGuid, TimeSpan minTimeLeft)
        {
            this.Target = target;
            this.AuraId = auraId;
            this.CreatorGuid = creatorGuid;
            this.MinTimeLeft = minTimeLeft;
        }

        public bool Satisfied()
        {
            var target = this.Target == TargetType.Me ? StyxWoW.Me : StyxWoW.Me.CurrentTarget;

            if (target == null || !target.IsValid)
                throw new ConditionException("Target cannot be null or invalid.");

            return target.GetAllAuras().SingleOrDefault(o =>
                o.SpellId == this.AuraId &&
                o.CreatorGuid == this.CreatorGuid &&
                o.TimeLeft <= this.MinTimeLeft) != null;
        }
    }
}
