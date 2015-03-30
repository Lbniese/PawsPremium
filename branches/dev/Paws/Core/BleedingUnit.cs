using Styx.WoWInternals.WoWObjects;

namespace Paws.Core
{
    public class BleedingUnit
    {
        public WoWUnit Unit { get; set; }
        public float AppliedMultiplier { get; set; }

        public BleedingUnit(WoWUnit unit, float appliedMultiplier)
        {
            this.Unit = unit;
            this.AppliedMultiplier = appliedMultiplier;
        }
    }
}
