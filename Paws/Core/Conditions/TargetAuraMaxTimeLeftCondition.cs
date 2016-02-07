using System;
using System.Linq;
using Styx;
using Styx.WoWInternals;

namespace Paws.Core.Conditions
{
    /// <summary>
    ///     Condition based on if the specified target has an aura that is greater than or equal to the provided time.
    /// </summary>
    public class TargetAuraMaxTimeLeftCondition : ICondition
    {
        public TargetAuraMaxTimeLeftCondition(TargetType target, int auraId, TimeSpan maxTimeLeft)
        {
            Target = target;
            AuraId = auraId;
            CreatorGuid = StyxWoW.Me.Guid;
            MaxTimeLeft = maxTimeLeft;
        }

        public TargetAuraMaxTimeLeftCondition(TargetType target, int auraId, WoWGuid creatorGuid, TimeSpan maxTimeLeft)
        {
            Target = target;
            AuraId = auraId;
            CreatorGuid = creatorGuid;
            MaxTimeLeft = maxTimeLeft;
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
        ///     The maximum amount of time left to satisfy the condition.
        /// </summary>
        public TimeSpan MaxTimeLeft { get; set; }

        public bool Satisfied()
        {
            var target = Target == TargetType.Me ? StyxWoW.Me : StyxWoW.Me.CurrentTarget;

            if (target == null || !target.IsValid)
                throw new ConditionException("Target cannot be null or invalid.");

            return target.GetAllAuras().SingleOrDefault(o =>
                o.SpellId == AuraId &&
                o.CreatorGuid == CreatorGuid &&
                o.TimeLeft >= MaxTimeLeft) != null;
        }
    }
}