using Paws.Core.Conditions;
using Paws.Core.Abilities.Attributes;
using Styx.WoWInternals;

namespace Paws.Core.Abilities.Feral
{
    /// <summary>
    /// <para>Mark of the Wild</para>
    /// <para>5% of base mana, 30 yd range</para>
    /// <para>Instant</para>
    /// <para>Requires Druid</para>
    /// <para>Requires level 62</para>
    /// <para>Incareases the friendly target's Strength, Agility, and Intellect by 5% and</para>
    /// <para>Versatility by 3% for 1 hour.</para>
    /// <para>If target is in your party or raid, all party and raid members will be affected.</para>
    /// <para>http://www.wowhead.com/spell=1126/mark-of-the-wild</para>
    /// </summary>
    [AbilityChain(FriendlyName = "Mark of the Wild")]
    public class MarkOfTheWildAbility : AbilityBase
    {
        public MarkOfTheWildAbility()
            : base(WoWSpell.FromId(SpellBook.MarkOfTheWild), true, true)
        {
            base.Category = AbilityCategory.Buff;
        }

        public override void ApplyDefaultSettings()
        {
            base.ApplyDefaultSettings();

            base.Conditions.Add(new BooleanCondition(Settings.MarkOfTheWildEnabled));
            base.Conditions.Add(new MeNotInCombatCondition());
            base.Conditions.Add(new MeDoesNotHaveStatsBuffCondition());
            if (Settings.MarkOfTheWildDoNotApplyIfStealthed)
            {
                base.Conditions.Add(new TargetDoesNotHaveAuraCondition(TargetType.Me, SpellBook.Prowl));
            }
        }
    }
}
