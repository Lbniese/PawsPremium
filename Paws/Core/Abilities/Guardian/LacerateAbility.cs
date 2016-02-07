using System;
using Paws.Core.Conditions;
using Styx.WoWInternals;

namespace Paws.Core.Abilities.Guardian
{
    public class LacerateAbility : PandemicAbilityBase
    {
        public LacerateAbility()
            : base(WoWSpell.FromId(SpellBook.Lacerate), true, true)
        {
        }

        public override void ApplyDefaultSettings()
        {
            base.ApplyDefaultSettings();

            var lacerateIsEnabled = new BooleanCondition(Settings.LacerateEnabled);
            var inBearForm = new MeIsInBearFormCondition();
            var attackableTarget = new MeHasAttackableTargetCondition();
            var targetIsWithinMeleeRange = new MyTargetIsWithinMeleeRangeCondition();
            var facingTarget = new MeIsFacingTargetCondition();

            Conditions.Add(lacerateIsEnabled);
            Conditions.Add(inBearForm);
            Conditions.Add(attackableTarget);
            Conditions.Add(targetIsWithinMeleeRange);
            Conditions.Add(facingTarget);
            Conditions.Add(new ConditionTestSwitchCondition(
                new TargetHasAuraCondition(TargetType.MyCurrentTarget, SpellBook.Lacerate),
                new TargetHasAuraMaxStacksCondition(TargetType.MyCurrentTarget, Spell.Id, 3)
                ));

            PandemicConditions.Add(lacerateIsEnabled);
            PandemicConditions.Add(inBearForm);
            PandemicConditions.Add(attackableTarget);
            PandemicConditions.Add(targetIsWithinMeleeRange);
            PandemicConditions.Add(facingTarget);
            PandemicConditions.Add(new BooleanCondition(Settings.LacerateAllowClipping));
            PandemicConditions.Add(new TargetHasAuraCondition(TargetType.MyCurrentTarget, Spell.Id));
            PandemicConditions.Add(new TargetAuraMinTimeLeftCondition(TargetType.MyCurrentTarget, Spell.Id,
                TimeSpan.FromSeconds(4.5)));
        }
    }
}