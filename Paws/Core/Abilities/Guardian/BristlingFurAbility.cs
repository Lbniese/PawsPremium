using Paws.Core.Conditions;
using Styx.WoWInternals;

namespace Paws.Core.Abilities.Guardian
{
    public class BristlingFurAbility : AbilityBase
    {
        public BristlingFurAbility()
            : base(WoWSpell.FromId(SpellBook.BristlingFur), false, true)
        {
            Category = AbilityCategory.Defensive;
        }

        public override void ApplyDefaultSettings()
        {
            base.ApplyDefaultSettings();

            Conditions.Add(new BooleanCondition(Settings.BristlingFurEnabled));
            Conditions.Add(new MeIsInBearFormCondition());
            Conditions.Add(new MeIsInCombatCondition());
            Conditions.Add(new TargetHealthRangeCondition(TargetType.Me, 0, Settings.BristlingFurMinHealth));
            Conditions.Add(new TargetDoesNotHaveAuraCondition(TargetType.Me, Spell.Id));
        }
    }
}