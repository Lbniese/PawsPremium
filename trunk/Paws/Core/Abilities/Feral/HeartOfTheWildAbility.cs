using Paws.Core.Conditions;
using Styx.WoWInternals;
using System.Linq;
using System;

namespace Paws.Core.Abilities.Feral
{
    /// <summary>
    /// <para>Heart of the Wild, Talent</para>
    /// <para>Instant, 6 min cooldown</para>
    /// <para>Requires level 90</para>
    /// <para>When activated, dramatically imporves the Druid's ability to tank, cast spells, and heal for 45 seconds.</para>
    /// <para>http://www.wowhead.com/spell=108292/heart-of-the-wild</para>
    /// </summary>
    public class HeartOfTheWildAbility : AbilityBase
    {
        public HeartOfTheWildAbility()
            : base(WoWSpell.FromId(SpellBook.HeartOfTheWild), true, true)
        {
            base.Category = AbilityCategory.Buff;

            base.Conditions.Add(new BooleanCondition(Settings.HeartOfTheWildEnabled));
            base.Conditions.Add(new MeHasAttackableTargetCondition());
            base.Conditions.Add(new TargetDoesNotHaveAuraCondition(TargetType.Me, this.Spell.Id));
            base.Conditions.Add(new TargetDoesNotHaveAuraCondition(TargetType.Me, SpellBook.Prowl));
            base.Conditions.Add(new TargetHealthRangeCondition(TargetType.Me, 0, Settings.HeartOfTheWildMinHealth));
        }
    }
}
