using Paws.Core.Conditions;
using Styx.WoWInternals;

namespace Paws.Core.Abilities
{
    //// <summary>
    /// The base Ability class for common pandemic melee feral conditions. This class cannot be directly instantiated. 
    /// </summary>
    public abstract class MeleeFeralPandemicAbilityBase : PandemicAbilityBase 
    {
        public MeleeFeralPandemicAbilityBase(WoWSpell spell, bool savageRoarCheck)
            : base(spell)
        {
            base.Conditions.Add(new MeHasAttackableTargetCondition());
            base.Conditions.Add(new MeIsFacingTargetCondition());
            base.Conditions.Add(new MeIsInCatFormCondition());
            base.Conditions.Add(new MyTargetIsWithinMeleeRangeCondition());
            if (savageRoarCheck && Settings.SavageRoarEnabled)
            {
                base.Conditions.Add(new ConditionTestSwitchCondition(
                    new MeKnowsSpellCondition(SpellBook.SavageRoar),
                    new MySavageRoarAuraCondition()
                ));
            }

            base.PandemicConditions.Add(new MeHasAttackableTargetCondition());
            base.PandemicConditions.Add(new MeIsFacingTargetCondition());
            base.PandemicConditions.Add(new MeIsInCatFormCondition());
            base.Conditions.Add(new MyTargetIsWithinMeleeRangeCondition());
            if (savageRoarCheck && Settings.SavageRoarEnabled)
            {
                base.PandemicConditions.Add(new ConditionTestSwitchCondition(
                    new MeKnowsSpellCondition(SpellBook.SavageRoar),
                    new MySavageRoarAuraCondition()
                ));
            }
        }
    }
}
