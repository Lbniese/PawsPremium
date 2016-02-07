using Paws.Core.Conditions;
using Styx.WoWInternals;

namespace Paws.Core.Abilities.Feral
{
    /// <summary>
    ///     <para>Healing Touch</para>
    ///     <para>10.35% of base mana, 40 yd range</para>
    ///     <para>2.5 second cast</para>
    ///     <para>Requires Druid</para>
    ///     <para>Requires level 26</para>
    ///     <para>Heals a friendsly target for (360% of Spell power).</para>
    ///     <para>http://www.wowhead.com/spell=5185/healing-touch</para>
    /// </summary>
    public class HealingTouchSnapshotAbility : AbilityBase
    {
        public HealingTouchSnapshotAbility()
            : base(WoWSpell.FromId(SpellBook.HealingTouch))
        {
            Category = AbilityCategory.Bloodtalons;

            RequiredConditions.Add(new TargetDoesNotHaveAuraCondition(TargetType.Me, SpellBook.Prowl));
            RequiredConditions.Add(new TargetHasAuraCondition(TargetType.Me, SpellBook.PredatorySwiftnessProc));
        }

        public override void ApplyDefaultSettings()
        {
            base.ApplyDefaultSettings();

            Conditions.Add(new BooleanCondition(Settings.BloodtalonsEnabled));
        }
    }
}