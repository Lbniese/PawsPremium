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
        public FaerieFireAbility(WoWClass playerClass, bool settingDef)
            : base(WoWSpell.FromId(SpellBook.FaerieFire), true, true)
        {
            base.Conditions.Add(new BooleanCondition(settingDef));
            base.Conditions.Add(new MeIsInCombatCondition());
            base.Conditions.Add(new MeHasAttackableTargetCondition());
            base.Conditions.Add(new MyTargetIsNotPetCondition());
            base.Conditions.Add(new MyTargetIsPlayerClassCondition(playerClass));
            base.Conditions.Add(new TargetDoesNotHaveAuraCondition(TargetType.MyCurrentTarget, SpellBook.FaerieFire));
            base.Conditions.Add(new TargetDoesNotHaveAuraCondition(TargetType.MyCurrentTarget, SpellBook.FaerieSwarm));
            base.Conditions.Add(new TargetDoesNotHaveAuraCondition(TargetType.Me, SpellBook.Prowl));
            base.Conditions.Add(new MyTargetDistanceCondition(0, 35.0));
        }
    }
}
