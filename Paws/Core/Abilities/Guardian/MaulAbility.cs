using Paws.Core.Conditions;
using Styx.WoWInternals;

namespace Paws.Core.Abilities.Guardian
{
    public class MaulAbility : AbilityBase
    {
        public MaulAbility()
            : base(WoWSpell.FromId(SpellBook.Maul), false, true)
        {
        }

        public override void ApplyDefaultSettings()
        {
            base.ApplyDefaultSettings();

            Conditions.Add(new BooleanCondition(Settings.MaulEnabled));
            Conditions.Add(new MeIsInBearFormCondition());
            Conditions.Add(new MeHasAttackableTargetCondition());
            Conditions.Add(new MyTargetIsWithinMeleeRangeCondition());
            Conditions.Add(new MeIsFacingTargetCondition());
            if (Settings.MaulOnlyDuringToothAndClawProc)
            {
                Conditions.Add(new TargetHasAuraCondition(TargetType.Me, SpellBook.ToothAndClawProc));
            }
            if (Settings.MaulRequireMinRage)
            {
                Conditions.Add(new MyRageCondition(Settings.MaulMinRage));
            }
        }
    }
}