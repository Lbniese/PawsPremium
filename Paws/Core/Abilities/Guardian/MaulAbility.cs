using Paws.Core.Conditions;
using Styx.WoWInternals;

namespace Paws.Core.Abilities.Guardian
{
    public class MaulAbility : AbilityBase
    {
        public MaulAbility()
            : base(WoWSpell.FromId(SpellBook.Maul), false, true)
        {
            base.Conditions.Add(new BooleanCondition(Settings.MaulEnabled));
            base.Conditions.Add(new MeIsInBearFormCondition());
            base.Conditions.Add(new MeHasAttackableTargetCondition());
            base.Conditions.Add(new MyTargetIsWithinMeleeRangeCondition());
            base.Conditions.Add(new MeIsFacingTargetCondition());
            if (Settings.MaulOnlyDuringToothAndClawProc)
            {
                base.Conditions.Add(new TargetHasAuraCondition(TargetType.Me, SpellBook.ToothAndClawProc));
            }
            if (Settings.MaulRequireMinRage)
            {
                base.Conditions.Add(new MyRageCondition(Settings.MaulMinRage));
            }
        }
    }
}
