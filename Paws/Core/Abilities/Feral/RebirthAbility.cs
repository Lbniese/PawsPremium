using Paws.Core.Conditions;
using Styx.WoWInternals;

namespace Paws.Core.Abilities.Feral
{
    /// <summary>
    /// <para>Rebirth</para>
    /// <para>40 yd range</para>
    /// <para>2 sec cast, 10 min cooldown</para>
    /// <para>Requires Druid</para>
    /// <para>Requires level 56</para>
    /// <para>Returns the spirit to the body, restoring a dead target to life with 60% health and</para>
    /// <para>20% mana. Castable in combat.</para>
    /// <para>Enhanced Rebirth (Level 92+)</para>
    /// <para>Rebirth no longer has a cast time.</para>
    /// <para>http://www.wowhead.com/spell=20484/rebirth</para>
    /// </summary>
    public class RebirthAbility : AbilityBase
    {
        public RebirthAbility()
            : base(WoWSpell.FromId(SpellBook.Rebirth), true, true)
        {
            base.Category = AbilityCategory.Heal;
        }

        public override void ApplyDefaultSettings()
        {
            base.ApplyDefaultSettings();

            if (Settings.RebirthOnlyDuringPredatorySwiftness)
            {
                base.Conditions.Add(new TargetHasAuraCondition(TargetType.Me, SpellBook.PredatorySwiftnessProc));
            }
            base.Conditions.Add(new TargetDoesNotHaveAuraCondition(TargetType.Me, SpellBook.Prowl));
        }
    }
}
