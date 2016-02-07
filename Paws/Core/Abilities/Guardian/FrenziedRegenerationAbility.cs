using Paws.Core.Conditions;
using Styx.WoWInternals;

namespace Paws.Core.Abilities.Guardian
{
    public class FrenziedRegenerationAbility : AbilityBase
    {
        public FrenziedRegenerationAbility()
            : base(WoWSpell.FromId(SpellBook.FrenziedRegeneration), true, true)
        {
            Category = AbilityCategory.Heal;
        }

        public override void ApplyDefaultSettings()
        {
            base.ApplyDefaultSettings();

            Conditions.Add(new BooleanCondition(Settings.FrenziedRegenerationEnabled));
            Conditions.Add(new MeIsInBearFormCondition());
            Conditions.Add(new MeIsInCombatCondition());
            Conditions.Add(new TargetHealthRangeCondition(TargetType.Me, 0, Settings.FrenziedRegenerationMinHealth));
            Conditions.Add(new MyRageCondition(Settings.FrenziedRegenerationMinRage));
        }
    }
}