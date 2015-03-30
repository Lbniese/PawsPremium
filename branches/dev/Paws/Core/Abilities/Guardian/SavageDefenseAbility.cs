using Paws.Core.Conditions;
using Styx.WoWInternals;

namespace Paws.Core.Abilities.Guardian
{
    public class SavageDefenseAbility : AbilityBase
    {
        public SavageDefenseAbility()
            : base(WoWSpell.FromId(SpellBook.SavageDefense), false, true)
        {
            base.Category = AbilityCategory.Defensive;

            base.Conditions.Add(new BooleanCondition(Settings.SavageDefenseEnabled));
            base.Conditions.Add(new MeIsInBearFormCondition());
            base.Conditions.Add(new MeIsInCombatCondition());
            base.Conditions.Add(new TargetHealthRangeCondition(TargetType.Me, 0, Settings.SavageDefenseMinHealth));
            base.Conditions.Add(new TargetDoesNotHaveAuraCondition(TargetType.Me, SpellBook.SavageDefense));
            base.Conditions.Add(new MyRageCondition(Settings.SavageDefenseMinRage));
        }
    }
}
