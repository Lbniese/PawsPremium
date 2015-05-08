using Paws.Core.Conditions;
using Styx.WoWInternals;

namespace Paws.Core.Abilities
{
    //// <summary>
    /// The base Ability class for common melee feral conditions. This class cannot be directly instantiated. 
    /// </summary>
    public abstract class MeleeFeralAbilityBase : AbilityBase 
    {
        public bool SavageRoarCheck { get; set; }

        public MeleeFeralAbilityBase(WoWSpell spell, bool savageRoarCheck)
            : base(spell)
        {
            this.SavageRoarCheck = savageRoarCheck;

            base.RequiredConditions.Add(new MeHasAttackableTargetCondition());
            base.RequiredConditions.Add(new MeIsFacingTargetCondition());
            base.RequiredConditions.Add(new MeIsInCatFormCondition());
            base.RequiredConditions.Add(new MyTargetIsWithinMeleeRangeCondition());
        }

        public override void ApplyDefaultSettings()
        {
            base.ApplyDefaultSettings();

            if (this.SavageRoarCheck && Settings.SavageRoarEnabled)
            {
                base.Conditions.Add(new ConditionTestSwitchCondition(
                    new MeKnowsSpellCondition(SpellBook.SavageRoar),
                    new MySavageRoarAuraCondition()
                ));
            }
        }
    }
}
