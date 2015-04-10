using Paws.Core.Conditions;
using Styx.WoWInternals;

namespace Paws.Core.Abilities.Feral
{
    /// <summary>
    /// <para>Wild Charge (Feral), Talent</para>
    /// <para>8 - 25 yd range</para>
    /// <para>Instant, 15 sec cooldown</para>
    /// <para>Requires Druid</para>
    /// <para>Requires level 1</para>
    /// <para>Requires Cat Form</para>
    /// <para>Leap behind an enemy, dazing them for 3 sec.</para>
    /// <para>http://www.wowhead.com/spell=49376/wild-charge</para>
    /// </summary>
    public class WildChargeAbility : AbilityBase
    {
        public WildChargeAbility()
            : base(WoWSpell.FromId(SpellBook.WildCharge), true, true)
        {
            base.Conditions.Add(new MeHasAttackableTargetCondition());
            base.Conditions.Add(new MeIsInCatFormCondition());
            base.Conditions.Add(new MeIsFacingTargetCondition());
            base.Conditions.Add(new MyTargetInLineOfSightCondition());
        }

        public override void ApplyDefaultSettings()
        {
            base.ApplyDefaultSettings();

            base.Conditions.Add(new BooleanCondition(Settings.WildChargeEnabled));
            base.Conditions.Add(new MyTargetIsNotWithinMeleeRangeCondition());
            base.Conditions.Add(new MyTargetDistanceCondition(Settings.WildChargeMinDistance, Settings.WildChargeMaxDistance));
        }
    }
}
