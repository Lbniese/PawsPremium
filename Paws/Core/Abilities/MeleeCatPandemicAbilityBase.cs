using Paws.Core.Conditions;
using Styx.WoWInternals;

namespace Paws.Core.Abilities
{
    //// <summary>
    /// The base Ability class for common pandemic melee feral conditions. This class cannot be directly instantiated.
    /// 
    public abstract class MeleeFeralPandemicAbilityBase : PandemicAbilityBase
    {
        protected MeleeFeralPandemicAbilityBase(WoWSpell spell, bool savageRoarCheck)
            : base(spell)
        {
            SavageRoarCheck = savageRoarCheck;
        }

        private bool SavageRoarCheck { get; set; }

        public override void ApplyDefaultSettings()
        {
            base.ApplyDefaultSettings();

            Conditions.Add(new MeHasAttackableTargetCondition());
            Conditions.Add(new MeIsFacingTargetCondition());
            Conditions.Add(new MeIsInCatFormCondition());
            Conditions.Add(new MyTargetIsWithinMeleeRangeCondition());
            if (SavageRoarCheck && Settings.SavageRoarEnabled)
            {
                Conditions.Add(new ConditionTestSwitchCondition(
                    new MeKnowsSpellCondition(SpellBook.SavageRoar),
                    new MySavageRoarAuraCondition()
                    ));
            }

            PandemicConditions.Add(new MeHasAttackableTargetCondition());
            PandemicConditions.Add(new MeIsFacingTargetCondition());
            PandemicConditions.Add(new MeIsInCatFormCondition());
            Conditions.Add(new MyTargetIsWithinMeleeRangeCondition());
            if (SavageRoarCheck && Settings.SavageRoarEnabled)
            {
                PandemicConditions.Add(new ConditionTestSwitchCondition(
                    new MeKnowsSpellCondition(SpellBook.SavageRoar),
                    new MySavageRoarAuraCondition()
                    ));
            }
        }
    }
}