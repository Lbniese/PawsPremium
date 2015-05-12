using Paws.Core.Conditions.Attributes;
using Styx;

namespace Paws.Core.Conditions
{
    /// <summary>
    /// Condition based on the health multiplier of player compared to the player's current target
    /// </summary>
    [ItemCondition(FriendlyName = "My Target's Health Multiplier")]
    public class MyTargetHealthMultiplierCondition : ICondition
    {
        /// <summary>
        /// The minimum health multiplier used on the player against the current target to satisfy the condition.
        /// </summary>
        [ItemConditionParameter(Descriptor = "x")]
        public float Multiplier { get; set; }

        public MyTargetHealthMultiplierCondition()
            : this(1.0f)
        { }

        public MyTargetHealthMultiplierCondition(float multiplier)
        {
            this.Multiplier = multiplier;
        }

        public bool Satisfied()
        {
            if (Main.MyCurrentTarget == null || !Main.MyCurrentTarget.IsValid)
                return false;

            return (StyxWoW.Me.CurrentTarget.MaxHealth >= (StyxWoW.Me.MaxHealth * this.Multiplier));
        }
    }
}
