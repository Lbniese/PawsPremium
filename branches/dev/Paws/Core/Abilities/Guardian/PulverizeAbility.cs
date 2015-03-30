using Paws.Core.Conditions;
using Styx.WoWInternals;

namespace Paws.Core.Abilities.Guardian
{
    public class PulverizeAbility : AbilityBase
    {
        public PulverizeAbility()
            : base(WoWSpell.FromId(SpellBook.Pulverize))
        {
            base.Conditions.Add(new BooleanCondition(Settings.PulverizeEnabled));
            base.Conditions.Add(new MeIsInBearFormCondition());
            base.Conditions.Add(new MeHasAttackableTargetCondition());
            base.Conditions.Add(new MyTargetIsWithinMeleeRangeCondition());
            base.Conditions.Add(new MeIsFacingTargetCondition());
            base.Conditions.Add(new TargetHasAuraMinStacksCondition(TargetType.MyCurrentTarget, SpellBook.Lacerate, 3));
        }
    }
}
