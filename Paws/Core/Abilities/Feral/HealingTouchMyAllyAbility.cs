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
    public class HealingTouchMyAllyAbility : AbilityBase
    {
        public HealingTouchMyAllyAbility()
            : base(WoWSpell.FromId(SpellBook.HealingTouch))
        {
            base.Category = AbilityCategory.Heal;

            base.RequiredConditions.Add(new TargetDoesNotHaveAuraCondition(TargetType.Me, SpellBook.Prowl));
        }

        public override void ApplyDefaultSettings()
        {
            base.ApplyDefaultSettings();

            base.Conditions.Add(new BooleanCondition(Settings.HealMyAlliesEnabled));
            base.Conditions.Add(new BooleanCondition(Settings.HealMyAlliesWithHealingTouchEnabled));
            base.Conditions.Add(new TargetHealthRangeCondition(TargetType.MyCurrentTarget, 5.0, Settings.HealMyAlliesWithHealingTouchMinHealth));
            if (Settings.HealMyAlliesMyHealthCheckEnabled)
            {
                base.Conditions.Add(new TargetHealthRangeCondition(TargetType.Me, Settings.HealMyAlliesMyMinHealth, 100.0));
            }
            if (Settings.HealMyAlliesWithHealingTouchOnlyDuringPSEnabled)
            {
                base.Conditions.Add(new TargetHasAuraCondition(TargetType.Me, SpellBook.PredatorySwiftnessProc));
            }
        }
    }
}