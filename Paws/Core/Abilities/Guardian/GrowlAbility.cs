using Paws.Core.Conditions;
using Styx;
using Styx.WoWInternals;
using Styx.WoWInternals.WoWObjects;
using System.Collections.Generic;

namespace Paws.Core.Abilities.Guardian
{
    public class GrowlAbility: AbilityBase
    {
        public GrowlAbility()
            : base(WoWSpell.FromId(SpellBook.Growl), true, true)
        {
            base.Category = AbilityCategory.Defensive;
        }

        public override void ApplyDefaultSettings()
        {
            base.ApplyDefaultSettings();

            base.Conditions.Add(new BooleanCondition(Settings.GrowlEnabled));
            base.Conditions.Add(new MeIsInBearFormCondition());
            base.Conditions.Add(new MeHasAttackableTargetCondition());
            base.Conditions.Add(new TargetDoesNotHaveAuraCondition(TargetType.Me, SpellBook.Prowl));
            base.Conditions.Add(new MyTargetDistanceCondition(0, 30));
        }
    }
}
