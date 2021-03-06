﻿using Paws.Core.Conditions;
using Styx.WoWInternals;

namespace Paws.Core.Abilities.Feral
{
    /// <summary>
    ///     <para>Wrath, Solar</para>
    ///     <para>3.5% of base manage, 40 yd range</para>
    ///     <para>2 sec cast</para>
    ///     <para>Requires Druid</para>
    ///     <para>Requires level 1</para>
    ///     <para>A Solar spell that causes (117% of Spell power) Nature damage to the target.</para>
    ///     <para>http://www.wowhead.com/spell=5176/wrath</para>
    /// </summary>
    public class WrathAbility : AbilityBase
    {
        public WrathAbility()
            : base(WoWSpell.FromId(SpellBook.Wrath))
        {
            Conditions.Add(new MeHasAttackableTargetCondition());
            Conditions.Add(new MeIsFacingTargetCondition());
            Conditions.Add(new MyTargetDistanceCondition(0, 38));
        }

        public override void ApplyDefaultSettings()
        {
            base.ApplyDefaultSettings();

            Conditions.Add(new MeDoesNotKnowSpellCondition(SpellBook.CatForm));
        }
    }
}