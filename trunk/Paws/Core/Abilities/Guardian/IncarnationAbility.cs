using Paws.Core.Conditions;
using Styx.WoWInternals;

namespace Paws.Core.Abilities.Guardian
{
    /// <summary>
    /// <para>Incarnation:Son of Ursoc, Talent, Shapeshift</para>
    /// <para>Instant, 3 min cooldown</para>
    /// <para>Requires Druid (Feral)</para>
    /// <para>Requires level 60</para>
    /// </summary>
    public class IncarnationAbility : AbilityBase
    {
        public IncarnationAbility()
            : base(WoWSpell.FromId(SpellBook.GuardianIncarnationForm), true, true)
        {
            base.Category = AbilityCategory.Buff;

            base.Conditions.Add(new BooleanCondition(Settings.GuardianIncarnationEnabled));
            base.Conditions.Add(new MeHasAttackableTargetCondition());
            base.Conditions.Add(new TargetDoesNotHaveAuraCondition(TargetType.Me, SpellBook.GuardianIncarnationForm));
            base.Conditions.Add(new TargetDoesNotHaveAuraCondition(TargetType.Me, SpellBook.Prowl));
            base.Conditions.Add(new MyTargetIsWithinMeleeRangeCondition());
            if (Settings.GuardianIncarnationEnemyHealthCheck)
            {
                base.Conditions.Add(new MyTargetHealthMultiplierCondition(Settings.GuardianIncarnationEnemyHealthMultiplier));
            }
        }
    }
}
