using Paws.Core.Conditions;
using Styx.WoWInternals;

namespace Paws.Core.Abilities.Feral
{
    /// <summary>
    ///     Ranged ability used to attack targets where the height is higher than me (Helps for pulling flying mobs while
    ///     questing).
    /// </summary>
    public class MoonfireHeightIssueAbility : AbilityBase
    {
        public MoonfireHeightIssueAbility()
            : base(WoWSpell.FromId(SpellBook.Moonfire))
        {
        }

        public override void ApplyDefaultSettings()
        {
            base.ApplyDefaultSettings();

            Conditions.Add(new BooleanCondition(Settings.TargetHeightEnabled));
            Conditions.Add(new MeHasAttackableTargetCondition());
            Conditions.Add(new MyTargetIsTooHighCondition(Settings.TargetHeightMinDistance));
            Conditions.Add(new MyTargetDistanceCondition(0, 40));
            Conditions.Add(new MeIsFacingTargetCondition());
            Conditions.Add(new TargetDoesNotHaveAuraCondition(TargetType.MyCurrentTarget,
                SpellBook.MoonfireDotDebuffLowLevel));
            Conditions.Add(new TargetDoesNotHaveAuraCondition(TargetType.MyCurrentTarget,
                SpellBook.MoonfireDotDebuffHighLevel));
            Conditions.Add(new MyEnergyRangeCondition(35.0));
        }
    }
}