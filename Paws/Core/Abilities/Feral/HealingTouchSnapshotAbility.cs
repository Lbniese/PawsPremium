using Paws.Core.Conditions;
using Styx.WoWInternals;

namespace Paws.Core.Abilities.Feral
{
    /// <summary>
    /// <para>Healing Touch</para>
    /// <para>10.35% of base mana, 40 yd range</para>
    /// <para>2.5 second cast</para>
    /// <para>Requires Druid</para>
    /// <para>Requires level 26</para>
    /// <para>Heals a friendsly target for (360% of Spell power).</para>
    /// <para>http://www.wowhead.com/spell=5185/healing-touch</para>
    /// </summary>
    public class HealingTouchSnapshotAbility : AbilityBase
    {
        public HealingTouchSnapshotAbility()
            : base(WoWSpell.FromId(SpellBook.HealingTouch))
        {
            base.Category = AbilityCategory.Bloodtalons;
        }

        public override void ApplyDefaultSettings()
        {
            base.ApplyDefaultSettings();

            base.Conditions.Add(new BooleanCondition(Settings.BloodtalonsEnabled));
            base.Conditions.Add(new TargetDoesNotHaveAuraCondition(TargetType.Me, SpellBook.Prowl));
            base.Conditions.Add(new TargetHasAuraCondition(TargetType.Me, SpellBook.PredatorySwiftnessProc));
        }
    }
}