using Paws.Core.Abilities.Attributes;
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
    [AbilityChain(FriendlyName = "Healing Touch")]
    public class HealingTouchMyAllyAbility : AbilityBase
    {
        public HealingTouchMyAllyAbility()
            : base(WoWSpell.FromId(SpellBook.HealingTouch))
        {
            Category = AbilityCategory.Heal;

            RequiredConditions.Add(new TargetDoesNotHaveAuraCondition(TargetType.Me, SpellBook.Prowl));
        }

        public override void ApplyDefaultSettings()
        {
            base.ApplyDefaultSettings();

            Conditions.Add(new BooleanCondition(Settings.HealMyAlliesEnabled));
            Conditions.Add(new BooleanCondition(Settings.HealMyAlliesWithHealingTouchEnabled));
            Conditions.Add(new TargetHealthRangeCondition(TargetType.MyCurrentTarget, 5.0,
                Settings.HealMyAlliesWithHealingTouchMinHealth));
            if (Settings.HealMyAlliesMyHealthCheckEnabled)
            {
                Conditions.Add(new TargetHealthRangeCondition(TargetType.Me, Settings.HealMyAlliesMyMinHealth, 100.0));
            }
            if (Settings.HealMyAlliesWithHealingTouchOnlyDuringPsEnabled)
            {
                Conditions.Add(new TargetHasAuraCondition(TargetType.Me, SpellBook.PredatorySwiftnessProc));
            }
        }
    }
}