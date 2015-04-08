﻿using Paws.Core.Conditions;
using Styx.WoWInternals;

namespace Paws.Core.Abilities.Feral
{
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
