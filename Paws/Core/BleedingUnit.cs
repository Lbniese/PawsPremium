using Styx.WoWInternals.WoWObjects;

namespace Paws.Core
{
    public class BleedingUnit
    {
        public BleedingUnit(WoWUnit unit, float appliedMultiplier)
        {
            Unit = unit;
            AppliedMultiplier = appliedMultiplier;
        }

        public WoWUnit Unit { get; set; }
        public float AppliedMultiplier { get; set; }
        public bool Requeue { get; set; }
    }
}