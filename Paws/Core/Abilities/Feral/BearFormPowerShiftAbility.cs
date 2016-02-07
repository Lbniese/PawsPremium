using Paws.Core.Abilities.Attributes;
using Paws.Core.Conditions;
using Styx.WoWInternals;

namespace Paws.Core.Abilities.Feral
{
    [AbilityChain(FriendlyName = "Bear Form")]
    public class BearFormPowerShiftAbility : AbilityBase
    {
        public BearFormPowerShiftAbility()
            : base(WoWSpell.FromId(SpellBook.BearForm))
        {
            Category = AbilityCategory.Buff;
            RequiredConditions.Add(new TargetDoesNotHaveAuraCondition(TargetType.Me, Spell.Id));
        }

        public override void ApplyDefaultSettings()
        {
            base.ApplyDefaultSettings();

            Conditions.Add(new BooleanCondition(Settings.BearFormPowerShiftEnabled));
        }
    }
}