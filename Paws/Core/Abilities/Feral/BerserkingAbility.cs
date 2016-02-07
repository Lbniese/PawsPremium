using Paws.Core.Conditions;
using Styx.WoWInternals;

namespace Paws.Core.Abilities.Feral
{
    /// <summary>
    ///     <para>Berserking</para>
    /// </summary>
    public class BerserkingAbility : AbilityBase
    {
        public BerserkingAbility()
            : base(WoWSpell.FromId(SpellBook.TrollRacialBerserking), true, true)
        {
            Category = AbilityCategory.Buff;

            RequiredConditions.Add(new MeIsInCatFormCondition());
            RequiredConditions.Add(new MeHasAttackableTargetCondition());
            RequiredConditions.Add(new TargetDoesNotHaveAuraCondition(TargetType.Me, Spell.Id));
            RequiredConditions.Add(new TargetDoesNotHaveAuraCondition(TargetType.Me, SpellBook.Prowl));
            RequiredConditions.Add(new MyTargetIsWithinMeleeRangeCondition());
        }

        public override void ApplyDefaultSettings()
        {
            base.ApplyDefaultSettings();

            Conditions.Add(new BooleanCondition(Settings.TrollBerserkingEnabled));
            if (Settings.TrollBerserkingEnemyHealthCheck)
            {
                Conditions.Add(new MyTargetHealthMultiplierCondition(Settings.TrollBerserkingEnemyHealthMultiplier));
            }
            if (Settings.TrollBerserkingSurroundedByEnemiesEnabled)
            {
                Conditions.Add(new AttackableTargetsMinCountCondition(Settings.TrollBerserkingSurroundedByMinEnemies));
            }
        }
    }
}