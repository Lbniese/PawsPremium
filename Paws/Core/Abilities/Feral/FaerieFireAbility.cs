using Paws.Core.Conditions;
using Styx;
using Styx.WoWInternals;

namespace Paws.Core.Abilities.Feral
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
            Enabled = false;
            Class = WoWClass.None;

            ApplyRequiredConditions();
        }

        /// <summary>
        ///     Shortcut constructor to create the wow classes.
        /// </summary>
        public FaerieFireAbility(WoWClass @class, bool enabled)
            : base(WoWSpell.FromId(SpellBook.FaerieFire), true, true)
        {
            Enabled = Enabled;
            Class = @class;

            ApplyRequiredConditions();
            ApplyDefaultSettings();
        }

        public bool Enabled { get; set; }
        public WoWClass Class { get; set; }

        private void ApplyRequiredConditions()
        {
            RequiredConditions.Add(new MeHasAttackableTargetCondition());
            RequiredConditions.Add(new MyTargetIsNotPetCondition());
            RequiredConditions.Add(new TargetDoesNotHaveAuraCondition(TargetType.MyCurrentTarget, SpellBook.FaerieFire));
            RequiredConditions.Add(new TargetDoesNotHaveAuraCondition(TargetType.MyCurrentTarget, SpellBook.FaerieSwarm));
            RequiredConditions.Add(new TargetDoesNotHaveAuraCondition(TargetType.Me, SpellBook.Prowl));
            RequiredConditions.Add(new MyTargetDistanceCondition(0, 35.0));
        }

        public override void ApplyDefaultSettings()
        {
            base.ApplyDefaultSettings();

            Conditions.Add(new BooleanCondition(Enabled));
            Conditions.Add(new MeIsInCombatCondition());
            Conditions.Add(new MyTargetIsPlayerClassCondition(Class));
        }
    }
}