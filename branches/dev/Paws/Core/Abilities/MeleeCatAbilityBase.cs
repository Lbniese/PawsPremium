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
        }

        public override void ApplyDefaultSettings()
        {
            base.ApplyDefaultSettings();

            base.Conditions.Add(new MeHasAttackableTargetCondition());
            base.Conditions.Add(new MeIsFacingTargetCondition());
            base.Conditions.Add(new MeIsInCatFormCondition());
            base.Conditions.Add(new MyTargetIsWithinMeleeRangeCondition());
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
