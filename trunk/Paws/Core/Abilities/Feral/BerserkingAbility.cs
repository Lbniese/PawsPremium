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

            base.Conditions.Add(new BooleanCondition(Settings.TrollBerserkingEnabled));
            base.Conditions.Add(new MeIsInCatFormCondition());
            base.Conditions.Add(new MeHasAttackableTargetCondition());
            base.Conditions.Add(new TargetDoesNotHaveAuraCondition(TargetType.Me, this.Spell.Id));
            base.Conditions.Add(new TargetDoesNotHaveAuraCondition(TargetType.Me, SpellBook.Prowl));
            base.Conditions.Add(new MyTargetIsWithinMeleeRangeCondition());
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
