using Paws.Core.Conditions;
using Styx.WoWInternals;

namespace Paws.Core.Abilities.Guardian
{
    public class SavageDefenseAbility : AbilityBase
    {
        public SavageDefenseAbility()
            : base(WoWSpell.FromId(SpellBook.SavageDefense), false, true)
        {
            Category = AbilityCategory.Defensive;
        }

        public override void ApplyDefaultSettings()
        {
            base.ApplyDefaultSettings();

            Conditions.Add(new BooleanCondition(Settings.SavageDefenseEnabled));
            Conditions.Add(new MeIsInBearFormCondition());
            Conditions.Add(new MeIsInCombatCondition());
            Conditions.Add(new TargetHealthRangeCondition(TargetType.Me, 0, Settings.SavageDefenseMinHealth));
            Conditions.Add(new TargetDoesNotHaveAuraCondition(TargetType.Me, SpellBook.SavageDefense));
            Conditions.Add(new MyRageCondition(Settings.SavageDefenseMinRage));
        }
    }
}