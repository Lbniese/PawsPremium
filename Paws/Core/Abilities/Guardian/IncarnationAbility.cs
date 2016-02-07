using Paws.Core.Conditions;
using Styx.WoWInternals;

namespace Paws.Core.Abilities.Guardian
{
    /// <summary>
    ///     <para>Incarnation:Son of Ursoc, Talent, Shapeshift</para>
    ///     <para>Instant, 3 min cooldown</para>
    ///     <para>Requires Druid (Feral)</para>
    ///     <para>Requires level 60</para>
    /// </summary>
    public class IncarnationAbility : AbilityBase
    {
        public IncarnationAbility()
            : base(WoWSpell.FromId(SpellBook.GuardianIncarnationForm), true, true)
        {
            Category = AbilityCategory.Buff;
        }

        public override void ApplyDefaultSettings()
        {
            base.ApplyDefaultSettings();

            Conditions.Add(new BooleanCondition(Settings.GuardianIncarnationEnabled));
            Conditions.Add(new MeHasAttackableTargetCondition());
            Conditions.Add(new TargetDoesNotHaveAuraCondition(TargetType.Me, SpellBook.GuardianIncarnationForm));
            Conditions.Add(new TargetDoesNotHaveAuraCondition(TargetType.Me, SpellBook.Prowl));
            Conditions.Add(new MyTargetIsWithinMeleeRangeCondition());
            if (Settings.GuardianIncarnationEnemyHealthCheck)
            {
                Conditions.Add(new MyTargetHealthMultiplierCondition(Settings.GuardianIncarnationEnemyHealthMultiplier));
            }
        }
    }
}