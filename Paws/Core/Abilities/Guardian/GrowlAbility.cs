using Paws.Core.Conditions;
using Styx.WoWInternals;

namespace Paws.Core.Abilities.Guardian
{
    public class GrowlAbility : AbilityBase
    {
        public GrowlAbility()
            : base(WoWSpell.FromId(SpellBook.Growl), true, true)
        {
            Category = AbilityCategory.Defensive;
        }

        public override void ApplyDefaultSettings()
        {
            base.ApplyDefaultSettings();

            Conditions.Add(new BooleanCondition(Settings.GrowlEnabled));
            Conditions.Add(new MeIsInBearFormCondition());
            Conditions.Add(new MeHasAttackableTargetCondition());
            Conditions.Add(new TargetDoesNotHaveAuraCondition(TargetType.Me, SpellBook.Prowl));
            Conditions.Add(new MyTargetDistanceCondition(0, 30));
        }
    }
}