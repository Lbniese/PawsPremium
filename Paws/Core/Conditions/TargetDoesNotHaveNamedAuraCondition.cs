using System.Linq;
using Paws.Core.Conditions.Attributes;
using Styx;

namespace Paws.Core.Conditions
{
    /// <summary>
    ///     Condition based on if the specified target does not have the provided aura.
    /// </summary>
    [ItemCondition(FriendlyName = "Target Does Not Have Aura")]
    public class TargetDoesNotHaveNamedAuraCondition : ICondition
    {
        public TargetDoesNotHaveNamedAuraCondition()
            : this(TargetType.Me, string.Empty)
        {
        }

        public TargetDoesNotHaveNamedAuraCondition(TargetType target, string auraName)
        {
            Target = target;
            AuraName = auraName;
        }

        /// <summary>
        ///     The target used to satisfy the condition against.
        /// </summary>
        [ItemConditionParameter("Me", TargetType.Me, "My Target", TargetType.MyCurrentTarget)]
        public TargetType Target { get; set; }

        /// <summary>
        ///     The aura id used to determine if the target does not have active.
        /// </summary>
        [ItemConditionParameter(Name = "Aura Name")]
        public string AuraName { get; set; }

        public bool Satisfied()
        {
            var target = Target == TargetType.Me ? StyxWoW.Me : StyxWoW.Me.CurrentTarget;

            if (target == null || !target.IsValid)
                return false;

            return target.GetAllAuras()
                .SingleOrDefault(o => o.Name == AuraName) == null;
        }
    }
}