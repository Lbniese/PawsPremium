using Paws.Core.Conditions;
using Styx.WoWInternals;

namespace Paws.Core.Abilities.Guardian
{
    public class BristlingFurAbility : AbilityBase
    {
        public BristlingFurAbility()
            : base(WoWSpell.FromId(SpellBook.BristlingFur), false, true)
        {
            base.Category = AbilityCategory.Defensive;
        }

        public override void ApplyDefaultSettings()
        {
            base.ApplyDefaultSettings();

            base.Conditions.Add(new BooleanCondition(Settings.BristlingFurEnabled));
            base.Conditions.Add(new MeIsInBearFormCondition());
            base.Conditions.Add(new MeIsInCombatCondition());
            base.Conditions.Add(new TargetHealthRangeCondition(TargetType.Me, 0, Settings.BristlingFurMinHealth));
            base.Conditions.Add(new TargetDoesNotHaveAuraCondition(TargetType.Me, base.Spell.Id));
        }
    }
}
