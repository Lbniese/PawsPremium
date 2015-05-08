using Paws.Core.Conditions;
using Styx;
using Styx.WoWInternals;
using Styx.WoWInternals.WoWObjects;
using System.Collections.Generic;

namespace Paws.Core.Abilities.Feral
{
    /// <summary>
    /// <para>Faerie Fire</para>
    /// <para>Instant, 35 yd range</para>
    /// <para>Requires Druid (Feral, Guardian)</para>
    /// <para>Requires level 28</para>
    /// <para>Faeries surround the target, preventing stealth and invisibility. Deals (32.5008% of Attack</para>
    /// <para>power) damage when cast from Bear Form.</para>
    /// <para>Enhanced Faerie Fire (Level 92+)</para>
    /// <para>While you are in Bear Form, Faerie Fire no longer has a cooldown and deals 100% increased damage.</para>
    /// <para>http://www.wowhead.com/spell=770/faerie-fire</para>
    /// </summary>
    public class FaerieFireAbility: AbilityBase
    {
        public bool Enabled { get; set; }
        public WoWClass Class { get; set; }

        public FaerieFireAbility()
            : base(WoWSpell.FromId(SpellBook.FaerieFire), true, true)
        {
            this.Enabled = false;
            this.Class = WoWClass.None;

            ApplyRequiredConditions();
        }

        /// <summary>
        /// Shortcut constructor to create the wow classes.
        /// </summary>
        public FaerieFireAbility(WoWClass @class, bool enabled)
            : base(WoWSpell.FromId(SpellBook.FaerieFire), true, true)
        {
            this.Enabled = Enabled;
            this.Class = @class;

            ApplyRequiredConditions();
            ApplyDefaultSettings();
        }

        private void ApplyRequiredConditions()
        {
            base.RequiredConditions.Add(new MeHasAttackableTargetCondition());
            base.RequiredConditions.Add(new MyTargetIsNotPetCondition());
            base.RequiredConditions.Add(new TargetDoesNotHaveAuraCondition(TargetType.MyCurrentTarget, SpellBook.FaerieFire));
            base.RequiredConditions.Add(new TargetDoesNotHaveAuraCondition(TargetType.MyCurrentTarget, SpellBook.FaerieSwarm));
            base.RequiredConditions.Add(new TargetDoesNotHaveAuraCondition(TargetType.Me, SpellBook.Prowl));
            base.RequiredConditions.Add(new MyTargetDistanceCondition(0, 35.0));
        }

        public override void ApplyDefaultSettings()
        {
            base.ApplyDefaultSettings();

            base.Conditions.Add(new BooleanCondition(this.Enabled));
            base.Conditions.Add(new MeIsInCombatCondition());
            base.Conditions.Add(new MyTargetIsPlayerClassCondition(this.Class));
        }
    }
}
