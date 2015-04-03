using Paws.Core.Conditions;
using Styx.WoWInternals;

namespace Paws.Core.Abilities.Feral
{
    /// <summary>
    /// <para>Prowl</para>
    /// <para>Instant, 10 sec cooldown</para>
    /// <para>Requires Druid</para>
    /// <para>Requires level 6</para>
    /// <para>Activates Cat Form and places the Druid into stealth, but reduces</para>
    /// <para>movement speed by 30%. Lasts until cancelled.</para>
    /// <para>http://www.wowhead.com/spell=5215/prowl</para>
    /// </summary>
    public class ProwlAbility : AbilityBase
    {
        public ProwlAbility()
            : base(WoWSpell.FromId(SpellBook.Prowl), true, true)
        {
            base.Category = AbilityCategory.Buff;
        }

        public override void ApplyDefaultSettings()
        {
            base.ApplyDefaultSettings();

            base.Conditions.Add(new BooleanCondition(Settings.ProwlEnabled));
            base.Conditions.Add(new MeHasAttackableTargetCondition());
            base.Conditions.Add(new MeNotInCombatCondition());
            base.Conditions.Add(new TargetDoesNotHaveAuraCondition(TargetType.Me, SpellBook.Prowl));
            base.Conditions.Add(new MyTargetDistanceCondition(0, Settings.ProwlMaxDistance));
        }
    }
}
