using Paws.Core.Conditions;
using Styx.WoWInternals;
using System;

namespace Paws.Core.Abilities.Guardian
{
    public class ThrashAbility : PandemicAbilityBase
    {
        public ThrashAbility()
            : base(WoWSpell.FromId(SpellBook.GuardianThrash), true, true)
        { }

        public override void ApplyDefaultSettings()
        {
            base.ApplyDefaultSettings();

            var thrashIsEnabled = new BooleanCondition(Settings.GuardianThrashEnabled);
            var inBearForm = new MeIsInBearFormCondition();
            var attackableTarget = new MeHasAttackableTargetCondition();
            var targetIsWithinMeleeRange = new MyTargetIsWithinMeleeRangeCondition();
            var facingTarget = new MeIsFacingTargetCondition();
            var minEnemies = new AttackableTargetsMinCountCondition(Settings.GuardianThrashMinEnemies);

            base.Conditions.Add(thrashIsEnabled);
            base.Conditions.Add(inBearForm);
            base.Conditions.Add(attackableTarget);
            base.Conditions.Add(targetIsWithinMeleeRange);
            base.Conditions.Add(facingTarget);
            base.Conditions.Add(minEnemies);
            base.Conditions.Add(new TargetDoesNotHaveAuraCondition(TargetType.MyCurrentTarget, this.Spell.Id));

            base.PandemicConditions.Add(thrashIsEnabled);
            base.PandemicConditions.Add(inBearForm);
            base.PandemicConditions.Add(attackableTarget);
            base.PandemicConditions.Add(targetIsWithinMeleeRange);
            base.PandemicConditions.Add(facingTarget);
            base.PandemicConditions.Add(minEnemies);
            base.PandemicConditions.Add(new BooleanCondition(Settings.GuardianThrashAllowClipping));
            base.PandemicConditions.Add(new TargetHasAuraCondition(TargetType.MyCurrentTarget, this.Spell.Id));
            base.PandemicConditions.Add(new TargetAuraMinTimeLeftCondition(TargetType.MyCurrentTarget, this.Spell.Id, TimeSpan.FromSeconds(4.8)));
        }
    }
}
