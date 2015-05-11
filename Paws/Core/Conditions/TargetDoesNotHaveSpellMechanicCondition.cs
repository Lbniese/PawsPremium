using Paws.Core.Managers;
using Styx;
using Styx.WoWInternals;
using System.Linq;

namespace Paws.Core.Conditions
{
    /// <summary>
    /// Condition based on if the specified target does not have the provided aura.
    /// </summary>
    public class TargetDoesNotHaveSpellMechanicCondition : ICondition
    {
        /// <summary>
        /// The target used to satisfy the condition against.
        /// </summary>
        public TargetType Target { get; set; }

        /// <summary>
        /// The Spell Mechanic used to determine if the target does not have active.
        /// </summary>
        public WoWSpellMechanic SpellMechanic { get; set; }
        
        public TargetDoesNotHaveSpellMechanicCondition()
            : this(TargetType.Me, WoWSpellMechanic.None)
        { }

        public TargetDoesNotHaveSpellMechanicCondition(TargetType target, WoWSpellMechanic spellMechanic)
        {
            this.Target = target;
            this.SpellMechanic = spellMechanic;
        }

        public bool Satisfied()
        {
            var target = UnitManager.TargetTypeConverter(this.Target);

            if (target == null || !target.IsValid)
                return false;

            return !target.GetAllAuras().Any(o => o.Spell != null && o.Spell.Mechanic.HasFlag(this.SpellMechanic));
        }
    }
}
