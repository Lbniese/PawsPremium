using Paws.Core.Conditions;
using Styx.WoWInternals;

namespace Paws.Core.Abilities.Guardian
{
    /// <summary>
    /// <para>Wild Charge (Feral), Talent</para>
    /// <para>8 - 25 yd range</para>
    /// <para>Instant, 15 sec cooldown</para>
    /// <para>Requires Druid</para>
    /// <para>Requires level 1</para>
    /// <para>Requires Bear Form</para>
    /// <para>Leap behind an enemy, dazing them for 3 sec.</para>
    /// <para>http://www.wowhead.com/spell=49376/wild-charge</para>
    /// </summary>
    public class WildChargeAbility : AbilityBase
    {
        public WildChargeAbility()
            : base(WoWSpell.FromId(SpellBook.GuardianWildCharge), true, true)
        {
            base.Conditions.Add(new BooleanCondition(Settings.GuardianWildChargeEnabled));
            base.Conditions.Add(new MeHasAttackableTargetCondition());
            base.Conditions.Add(new MeIsInBearFormCondition());
            base.Conditions.Add(new MeIsFacingTargetCondition());
            base.Conditions.Add(new MyTargetInLineOfSightCondition());
            base.Conditions.Add(new MyTargetIsNotWithinMeleeRangeCondition());
            base.Conditions.Add(new MyTargetDistanceCondition(Settings.GuardianWildChargeMinDistance, Settings.GuardianWildChargeMaxDistance));
        }
    }
}
