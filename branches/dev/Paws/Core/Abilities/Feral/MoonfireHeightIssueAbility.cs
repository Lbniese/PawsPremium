using Paws.Core.Conditions;
using Styx.WoWInternals;

namespace Paws.Core.Abilities.Feral
{
    /// <summary>
    /// Ranged ability used to attack targets where the height is higher than me (Helps for pulling flying mobs while questing). 
    /// </summary>
    public class MoonfireHeightIssueAbility : AbilityBase
    {
        public MoonfireHeightIssueAbility()
            : base(WoWSpell.FromId(SpellBook.Moonfire))
        { }

        public override void ApplyDefaultSettings()
        {
            base.ApplyDefaultSettings();

            base.Conditions.Add(new BooleanCondition(Settings.TargetHeightEnabled));
            base.Conditions.Add(new MeHasAttackableTargetCondition());
            base.Conditions.Add(new MyTargetIsTooHighCondition(Settings.TargetHeightMinDistance));
            base.Conditions.Add(new MyTargetDistanceCondition(0, 40));
            base.Conditions.Add(new MeIsFacingTargetCondition());
            base.Conditions.Add(new TargetDoesNotHaveAuraCondition(TargetType.MyCurrentTarget, SpellBook.MoonfireDotDebuffLowLevel));
            base.Conditions.Add(new TargetDoesNotHaveAuraCondition(TargetType.MyCurrentTarget, SpellBook.MoonfireDotDebuffHighLevel));
            base.Conditions.Add(new MyEnergyRangeCondition(35.0));
        }
    }
}
