using Paws.Core.Conditions;
using Styx.WoWInternals;

namespace Paws.Core.Abilities.Guardian
{
    public class PulverizeAbility : AbilityBase
    {
        public PulverizeAbility()
            : base(WoWSpell.FromId(SpellBook.Pulverize))
        {
        }

        public override void ApplyDefaultSettings()
        {
            base.ApplyDefaultSettings();

            Conditions.Add(new BooleanCondition(Settings.PulverizeEnabled));
            Conditions.Add(new MeIsInBearFormCondition());
            Conditions.Add(new MeHasAttackableTargetCondition());
            Conditions.Add(new MyTargetIsWithinMeleeRangeCondition());
            Conditions.Add(new MeIsFacingTargetCondition());
            Conditions.Add(new TargetHasAuraMinStacksCondition(TargetType.MyCurrentTarget, SpellBook.Lacerate, 3));
        }
    }
}