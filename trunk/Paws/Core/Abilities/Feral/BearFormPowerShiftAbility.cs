using Paws.Core.Conditions;
using Styx.WoWInternals;

namespace Paws.Core.Abilities.Feral
{
    public class BearFormPowerShiftAbility : AbilityBase
    {
        public BearFormPowerShiftAbility()
            : base(WoWSpell.FromId(SpellBook.BearForm))
        {
            base.Category = AbilityCategory.Buff;

            base.Conditions.Add(new BooleanCondition(Settings.BearFormPowerShiftEnabled));
            base.Conditions.Add(new TargetDoesNotHaveAuraCondition(TargetType.Me, base.Spell.Id));
        }
    }
}
