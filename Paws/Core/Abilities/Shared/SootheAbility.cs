using Paws.Core.Abilities.Attributes;
using Paws.Core.Conditions;
using Styx.WoWInternals;

namespace Paws.Core.Abilities.Shared
{
    [AbilityChain(FriendlyName = "Soothe")]
    public class SootheAbility : AbilityBase
    {
        public SootheAbility()
            : base(WoWSpell.FromId(SpellBook.Soothe))
        {
            Category = AbilityCategory.Defensive;
        }

        public override void ApplyDefaultSettings()
        {
            base.ApplyDefaultSettings();

            Conditions.Add(new MeHasAttackableTargetCondition());
            Conditions.Add(new MyTargetIsEnragedCondition());
            Conditions.Add(new TargetDoesNotHaveAuraCondition(TargetType.Me, SpellBook.Prowl));
            Conditions.Add(new MyTargetDistanceCondition(0, 35));
        }
    }
}