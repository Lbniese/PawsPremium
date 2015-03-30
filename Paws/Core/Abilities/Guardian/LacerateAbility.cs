using Paws.Core.Conditions;
using Styx.WoWInternals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paws.Core.Abilities.Guardian
{
    public class LacerateAbility : PandemicAbilityBase
    {
        public LacerateAbility()
            : base(WoWSpell.FromId(SpellBook.Lacerate), true, true)
        {
            var lacerateIsEnabled = new BooleanCondition(Settings.LacerateEnabled);
            var inBearForm = new MeIsInBearFormCondition();
            var attackableTarget = new MeHasAttackableTargetCondition();
            var targetIsWithinMeleeRange = new MyTargetIsWithinMeleeRangeCondition();
            var facingTarget = new MeIsFacingTargetCondition();

            base.Conditions.Add(lacerateIsEnabled);
            base.Conditions.Add(inBearForm);
            base.Conditions.Add(attackableTarget);
            base.Conditions.Add(targetIsWithinMeleeRange);
            base.Conditions.Add(facingTarget);
            base.Conditions.Add(new ConditionTestSwitchCondition(
                new TargetHasAuraCondition(TargetType.MyCurrentTarget, SpellBook.Lacerate),
                new TargetHasAuraMaxStacksCondition(TargetType.MyCurrentTarget, this.Spell.Id, 3)
            ));

            base.PandemicConditions.Add(lacerateIsEnabled);
            base.PandemicConditions.Add(inBearForm);
            base.PandemicConditions.Add(attackableTarget);
            base.PandemicConditions.Add(targetIsWithinMeleeRange);
            base.PandemicConditions.Add(facingTarget);
            base.PandemicConditions.Add(new BooleanCondition(Settings.LacerateAllowClipping));
            base.PandemicConditions.Add(new TargetHasAuraCondition(TargetType.MyCurrentTarget, this.Spell.Id));
            base.PandemicConditions.Add(new TargetAuraMinTimeLeftCondition(TargetType.MyCurrentTarget, this.Spell.Id, TimeSpan.FromSeconds(4.5)));
        }
    }
}
