using Paws.Core.Conditions;
using Styx.WoWInternals;

namespace Paws.Core.Abilities.Guardian
{
    /// <summary>
    ///     <para>Wild Charge (Feral), Talent</para>
    ///     <para>8 - 25 yd range</para>
    ///     <para>Instant, 15 sec cooldown</para>
    ///     <para>Requires Druid</para>
    ///     <para>Requires level 1</para>
    ///     <para>Requires Bear Form</para>
    ///     <para>Leap behind an enemy, dazing them for 3 sec.</para>
    ///     <para>http://www.wowhead.com/spell=49376/wild-charge</para>
    /// </summary>
    public class WildChargeAbility : AbilityBase
    {
        public WildChargeAbility()
            : base(WoWSpell.FromId(SpellBook.GuardianWildCharge), true, true)
        {
        }

        public override void ApplyDefaultSettings()
        {
            base.ApplyDefaultSettings();

            Conditions.Add(new BooleanCondition(Settings.GuardianWildChargeEnabled));
            Conditions.Add(new MeHasAttackableTargetCondition());
            Conditions.Add(new MeIsInBearFormCondition());
            Conditions.Add(new MeIsFacingTargetCondition());
            Conditions.Add(new MyTargetInLineOfSightCondition());
            Conditions.Add(new MyTargetIsNotWithinMeleeRangeCondition());
            Conditions.Add(new MyTargetDistanceCondition(Settings.GuardianWildChargeMinDistance,
                Settings.GuardianWildChargeMaxDistance));
        }
    }
}