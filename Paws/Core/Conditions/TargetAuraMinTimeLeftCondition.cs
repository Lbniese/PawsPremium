using System;
using System.Linq;
using Styx;
using Styx.WoWInternals;

namespace Paws.Core.Conditions
{
    /// <summary>
    ///     Condition based on if the specified target has an aura that is less than or equal to the provided time.
    /// </summary>
    public class TargetAuraMinTimeLeftCondition : ICondition
    {
        public TargetAuraMinTimeLeftCondition(TargetType target, int auraId, TimeSpan minTimeLeft)
        {
            Target = target;
            AuraId = auraId;
            CreatorGuid = StyxWoW.Me.Guid;
            MinTimeLeft = minTimeLeft;
        }

        public TargetAuraMinTimeLeftCondition(TargetType target, int auraId, WoWGuid creatorGuid, TimeSpan minTimeLeft)
        {
            Target = target;
            AuraId = auraId;
            CreatorGuid = creatorGuid;
            MinTimeLeft = minTimeLeft;
        }

        /// <summary>
        ///     The target used to satisfy the condition against.
        /// </summary>
        public TargetType Target { get; set; }

        /// <summary>
        ///     The aura id used to determine the time left to satisfy the condition.
        /// </summary>
        public int AuraId { get; set; }

        /// <summary>
        ///     The creator Guid used to determine if who created the aura.
        /// </summary>
        public WoWGuid CreatorGuid { get; set; }

        /// <summary>
        ///     The minimum amount of time left to satisfy the condition.
        /// </summary>
        public TimeSpan MinTimeLeft { get; set; }

        public bool Satisfied()
        {
            var target = Target == TargetType.Me ? StyxWoW.Me : StyxWoW.Me.CurrentTarget;

            if (target == null || !target.IsValid)
                throw new ConditionException("Target cannot be null or invalid.");

            return target.GetAllAuras().SingleOrDefault(o =>
                o.SpellId == AuraId &&
                o.CreatorGuid == CreatorGuid &&
                o.TimeLeft <= MinTimeLeft) != null;
        }
    }
}