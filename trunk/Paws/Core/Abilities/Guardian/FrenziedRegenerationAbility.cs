using Paws.Core.Conditions;
using Styx.WoWInternals;

namespace Paws.Core.Abilities.Guardian
{
    public class FrenziedRegenerationAbility : AbilityBase
    {
        public FrenziedRegenerationAbility()
            : base(WoWSpell.FromId(SpellBook.FrenziedRegeneration), true, true)
        {
            base.Category = AbilityCategory.Heal;
        }

        public override void ApplyDefaultSettings()
        {
            base.ApplyDefaultSettings();

            base.Conditions.Add(new BooleanCondition(Settings.FrenziedRegenerationEnabled));
            base.Conditions.Add(new MeIsInBearFormCondition());
            base.Conditions.Add(new MeIsInCombatCondition());
            base.Conditions.Add(new TargetHealthRangeCondition(TargetType.Me, 0, Settings.FrenziedRegenerationMinHealth));
            base.Conditions.Add(new MyRageCondition(Settings.FrenziedRegenerationMinRage));
        }
    }
}
