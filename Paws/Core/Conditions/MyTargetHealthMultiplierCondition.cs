using Paws.Core.Conditions.Attributes;
using Styx;

namespace Paws.Core.Conditions
{
    /// <summary>
    ///     Condition based on the health multiplier of player compared to the player's current target
    /// </summary>
    [ItemCondition(FriendlyName = "My Target's Health Multiplier")]
    public class MyTargetHealthMultiplierCondition : ICondition
    {
        public MyTargetHealthMultiplierCondition()
            : this(1.0f)
        {
        }

        public MyTargetHealthMultiplierCondition(float multiplier)
        {
            Multiplier = multiplier;
        }

        /// <summary>
        ///     The minimum health multiplier used on the player against the current target to satisfy the condition.
        /// </summary>
        [ItemConditionParameter(Descriptor = "x")]
        public float Multiplier { get; set; }

        public bool Satisfied()
        {
            if (StyxWoW.Me.CurrentTarget == null || !StyxWoW.Me.CurrentTarget.IsValid)
                throw new ConditionException("Target cannot be null or invalid.");

            return StyxWoW.Me.CurrentTarget.MaxHealth >= StyxWoW.Me.MaxHealth*Multiplier;
        }
    }
}