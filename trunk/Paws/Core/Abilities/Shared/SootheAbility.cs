using Paws.Core.Conditions;
using Styx.WoWInternals;

namespace Paws.Core.Abilities.Shared
{
    public class SootheAbility : AbilityBase
    {
        public SootheAbility()
            : base(WoWSpell.FromId(SpellBook.Soothe))
        {
            base.Category = AbilityCategory.Defensive;

            base.Conditions.Add(new MeHasAttackableTargetCondition());
            base.Conditions.Add(new MyTargetIsEnragedCondition());
            base.Conditions.Add(new TargetDoesNotHaveAuraCondition(TargetType.Me, SpellBook.Prowl));
            base.Conditions.Add(new MyTargetDistanceCondition(0, 35));
        }
    }
}
