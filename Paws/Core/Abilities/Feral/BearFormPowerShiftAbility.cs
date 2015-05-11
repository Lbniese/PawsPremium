using Paws.Core.Conditions;
using Paws.Core.Abilities.Attributes;
using Styx.WoWInternals;
using Styx;
using System.Collections.Generic;

namespace Paws.Core.Abilities.Feral
{
    [AbilityChain(FriendlyName = "Bear Form", AvailableSpecs = AvailableSpecs.Shared)]
    public class BearFormPowerShiftAbility : AbilityBase
    {
        public BearFormPowerShiftAbility()
            : base(WoWSpell.FromId(SpellBook.BearForm))
        {
            base.Category = AbilityCategory.Buff;
            base.RequiredConditions.Add(new TargetDoesNotHaveAuraCondition(TargetType.Me, base.Spell.Id));
        }

        public override void ApplyDefaultSettings()
        {
            base.ApplyDefaultSettings();

            base.Conditions.Add(new BooleanCondition(Settings.BearFormPowerShiftEnabled));
        }
    }
}
