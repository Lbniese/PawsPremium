using Paws.Core.Conditions;
using Styx.WoWInternals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paws.Core.Abilities.Guardian
{
    public class MangleAbility : AbilityBase
    {
        public MangleAbility()
            : base(WoWSpell.FromId(SpellBook.Mangle), true, true)
        { }

        public override void ApplyDefaultSettings()
        {
            base.ApplyDefaultSettings();

            base.Conditions.Add(new BooleanCondition(Settings.MangleEnabled));
            base.Conditions.Add(new MeIsInBearFormCondition());
            base.Conditions.Add(new MeHasAttackableTargetCondition());
            base.Conditions.Add(new MyTargetIsWithinMeleeRangeCondition());
            base.Conditions.Add(new MeIsFacingTargetCondition());
        }
    }
}
