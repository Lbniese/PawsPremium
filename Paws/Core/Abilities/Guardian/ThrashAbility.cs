using System;
using Paws.Core.Conditions;
using Styx.WoWInternals;

namespace Paws.Core.Abilities.Guardian
{
    public class ThrashAbility : PandemicAbilityBase
    {
        public ThrashAbility()
            : base(WoWSpell.FromId(SpellBook.GuardianThrash), true, true)
        {
        }

        public override void ApplyDefaultSettings()
        {
            base.ApplyDefaultSettings();

            var thrashIsEnabled = new BooleanCondition(Settings.GuardianThrashEnabled);
            var inBearForm = new MeIsInBearFormCondition();
            var attackableTarget = new MeHasAttackableTargetCondition();
            var targetIsWithinMeleeRange = new MyTargetIsWithinMeleeRangeCondition();
            var facingTarget = new MeIsFacingTargetCondition();
            var minEnemies = new AttackableTargetsMinCountCondition(Settings.GuardianThrashMinEnemies);

            Conditions.Add(thrashIsEnabled);
            Conditions.Add(inBearForm);
            Conditions.Add(attackableTarget);
            Conditions.Add(targetIsWithinMeleeRange);
            Conditions.Add(facingTarget);
            Conditions.Add(minEnemies);
            Conditions.Add(new TargetDoesNotHaveAuraCondition(TargetType.MyCurrentTarget, Spell.Id));

            PandemicConditions.Add(thrashIsEnabled);
            PandemicConditions.Add(inBearForm);
            PandemicConditions.Add(attackableTarget);
            PandemicConditions.Add(targetIsWithinMeleeRange);
            PandemicConditions.Add(facingTarget);
            PandemicConditions.Add(minEnemies);
            PandemicConditions.Add(new BooleanCondition(Settings.GuardianThrashAllowClipping));
            PandemicConditions.Add(new TargetHasAuraCondition(TargetType.MyCurrentTarget, Spell.Id));
            PandemicConditions.Add(new TargetAuraMinTimeLeftCondition(TargetType.MyCurrentTarget, Spell.Id,
                TimeSpan.FromSeconds(4.8)));
        }
    }
}