using Paws.Core.Conditions;
using Styx.WoWInternals;

namespace Paws.Core.Abilities
{
    //// <summary>
    /// The base Ability class for common melee feral conditions. This class cannot be directly instantiated.
    /// 
    public abstract class MeleeFeralAbilityBase : AbilityBase
    {
        protected MeleeFeralAbilityBase(WoWSpell spell, bool savageRoarCheck)
            : base(spell)
        {
            SavageRoarCheck = savageRoarCheck;

            RequiredConditions.Add(new MeHasAttackableTargetCondition());
            RequiredConditions.Add(new MeIsFacingTargetCondition());
            RequiredConditions.Add(new MeIsInCatFormCondition());
            RequiredConditions.Add(new MyTargetIsWithinMeleeRangeCondition());
        }

        public bool SavageRoarCheck { get; set; }

        public override void ApplyDefaultSettings()
        {
            base.ApplyDefaultSettings();

            if (SavageRoarCheck && Settings.SavageRoarEnabled)
            {
                Conditions.Add(new ConditionTestSwitchCondition(
                    new MeKnowsSpellCondition(SpellBook.SavageRoar),
                    new MySavageRoarAuraCondition()
                    ));
            }
        }
    }
}