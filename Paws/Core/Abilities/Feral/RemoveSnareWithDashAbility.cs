using Paws.Core.Conditions;
using Styx.WoWInternals;

namespace Paws.Core.Abilities.Feral
{
    /// <summary>
    /// <para>Dash</para>
    /// <para>Instant, 3 min cooldown</para>
    /// <para>Requires Druid</para>
    /// <para>Requires level 24</para>
    /// <para>Activates Cat Form, removes all roots and snares, and increases movement speed by 70% while in Cat Form</para>
    /// <para>for 15 seconds.</para>
    /// <para>http://www.wowhead.com/spell=1850/dash</para>
    /// </summary>
    public class RemoveSnareWithDashAbility : AbilityBase
    {
        public RemoveSnareWithDashAbility()
            : base(WoWSpell.FromId(SpellBook.Dash), true, true)
        {
            base.Category = AbilityCategory.Defensive;
        }

        public override void ApplyDefaultSettings()
        {
            base.ApplyDefaultSettings();

            base.Conditions.Add(new BooleanCondition(Settings.RemoveSnareWithDash));
            base.Conditions.Add(new MeHasAttackableTargetCondition());
            base.Conditions.Add(new MeIsInCatFormCondition());
            base.Conditions.Add(new TargetDoesNotHaveAuraCondition(TargetType.Me, base.Spell.Id));
            base.Conditions.Add(new MeHasRootOrSnareCondition());
        }
    }
}
