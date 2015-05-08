using Paws.Core.Conditions;
using Styx.WoWInternals;

namespace Paws.Core.Abilities.Feral
{
    /// <summary>
    /// <para>Berserking</para>
    /// </summary>
    public class BerserkingAbility : AbilityBase
    {
        public BerserkingAbility()
            : base(WoWSpell.FromId(SpellBook.TrollRacialBerserking), true, true)
        {
            base.Category = AbilityCategory.Buff;

            base.RequiredConditions.Add(new MeIsInCatFormCondition());
            base.RequiredConditions.Add(new MeHasAttackableTargetCondition());
            base.RequiredConditions.Add(new TargetDoesNotHaveAuraCondition(TargetType.Me, this.Spell.Id));
            base.RequiredConditions.Add(new TargetDoesNotHaveAuraCondition(TargetType.Me, SpellBook.Prowl));
            base.RequiredConditions.Add(new MyTargetIsWithinMeleeRangeCondition());
        }

        public override void ApplyDefaultSettings()
        {
            base.ApplyDefaultSettings();

            base.Conditions.Add(new BooleanCondition(Settings.TrollBerserkingEnabled));
            if (Settings.TrollBerserkingEnemyHealthCheck)
            {
                base.Conditions.Add(new MyTargetHealthMultiplierCondition(Settings.TrollBerserkingEnemyHealthMultiplier));
            }
            if (Settings.TrollBerserkingSurroundedByEnemiesEnabled)
            {
                base.Conditions.Add(new AttackableTargetsMinCountCondition(Settings.TrollBerserkingSurroundedByMinEnemies));
            }
        }
    }
}
