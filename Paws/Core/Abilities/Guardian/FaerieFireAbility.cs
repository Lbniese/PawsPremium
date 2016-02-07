using Paws.Core.Conditions;
using Styx.WoWInternals;

namespace Paws.Core.Abilities.Guardian
{
    /// <summary>
    ///     <para>Faerie Fire</para>
    ///     <para>Instant, 35 yd range</para>
    ///     <para>Requires Druid (Feral, Guardian)</para>
    ///     <para>Requires level 28</para>
    ///     <para>Faeries surround the target, preventing stealth and invisibility. Deals (32.5008% of Attack</para>
    ///     <para>power) damage when cast from Bear Form.</para>
    ///     <para>Enhanced Faerie Fire (Level 92+)</para>
    ///     <para>While you are in Bear Form, Faerie Fire no longer has a cooldown and deals 100% increased damage.</para>
    ///     <para>http://www.wowhead.com/spell=770/faerie-fire</para>
    /// </summary>
    public class FaerieFireAbility : AbilityBase
    {
        public FaerieFireAbility()
            : base(WoWSpell.FromId(SpellBook.FaerieFire), true, true)
        {
        }

        public override void ApplyDefaultSettings()
        {
            base.ApplyDefaultSettings();

            Conditions.Add(new BooleanCondition(Settings.GuardianFaerieFireEnabled));
            Conditions.Add(new MeHasAttackableTargetCondition());
            Conditions.Add(new TargetDoesNotHaveAuraCondition(TargetType.MyCurrentTarget, SpellBook.FaerieFire));
            Conditions.Add(new TargetDoesNotHaveAuraCondition(TargetType.MyCurrentTarget, SpellBook.GuardianFaerieSwarm));
            Conditions.Add(new TargetDoesNotHaveAuraCondition(TargetType.Me, SpellBook.Prowl));
            Conditions.Add(new MyTargetDistanceCondition(Settings.GuardianFaerieFireMinDistance,
                Settings.GuardianFaerieFireMaxDistance));
        }
    }
}