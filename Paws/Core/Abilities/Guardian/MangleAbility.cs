using Paws.Core.Conditions;
using Styx.WoWInternals;

namespace Paws.Core.Abilities.Guardian
{
    public class MangleAbility : AbilityBase
    {
        public MangleAbility()
            : base(WoWSpell.FromId(SpellBook.Mangle), true, true)
        {
        }

        public override void ApplyDefaultSettings()
        {
            base.ApplyDefaultSettings();

            Conditions.Add(new BooleanCondition(Settings.MangleEnabled));
            Conditions.Add(new MeIsInBearFormCondition());
            Conditions.Add(new MeHasAttackableTargetCondition());
            Conditions.Add(new MyTargetIsWithinMeleeRangeCondition());
            Conditions.Add(new MeIsFacingTargetCondition());
        }
    }
}