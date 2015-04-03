using Paws.Core.Abilities.Attributes;
using Paws.Core.Conditions;
using Styx.WoWInternals;
using System.Collections.Generic;

namespace Paws.Core.Abilities.Shared
{
    /// <summary>
    /// <para>Mighty Bash</para>
    /// <para>Melee Range</para>
    /// <para>Instant, 50 sec cooldown</para>
    /// <para>Requires Druid</para>
    /// <para>Requires level 75</para>
    /// <para>Invokes the spirit of Ursoc to stun the target for 5 sec. Usable in all shapeshift forms.</para>
    /// <para>http://www.wowhead.com/spell=5211/mighty-bash</para>
    /// </summary>
    [AbilityChain(FriendlyName = "Mighty Bash (Stun)")]
    public class MightyBashAbility : AbilityBase
    {
        public List<ICondition> RequiredConditions { get; set; }

        public MightyBashAbility()
            : base(WoWSpell.FromId(SpellBook.MightyBash), true, true)
        {
            this.RequiredConditions = new List<ICondition>();
            base.Conditions.Add(new MeHasAttackableTargetCondition());
            base.Conditions.Add(new MyTargetIsWithinMeleeRangeCondition());
            base.Conditions.Add(new TargetDoesNotHaveAuraCondition(TargetType.MyCurrentTarget, this.Spell.Id));
        }

        public override void ApplyDefaultSettings()
        {
            base.ApplyDefaultSettings();
            base.Conditions.Add(new TargetDoesNotHaveAuraCondition(TargetType.Me, SpellBook.Prowl));
            base.Conditions.AddRange(this.RequiredConditions);

            base.Conditions.Add(new ConditionOrList(
                new ConditionTestSwitchCondition(
                    new MyExpectedSpecializationCondition(Styx.WoWSpec.DruidFeral),
                    new BooleanCondition(Settings.MightyBashEnabled),
                    false
                ),
                new ConditionTestSwitchCondition(
                    new MyExpectedSpecializationCondition(Styx.WoWSpec.DruidGuardian),
                    new BooleanCondition(Settings.GuardianMightyBashEnabled),
                    false
                )
            ));
        }
    }
}
