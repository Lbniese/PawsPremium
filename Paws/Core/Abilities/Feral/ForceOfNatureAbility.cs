using Paws.Core.Conditions;
using Paws.Core.Abilities.Attributes;
using Styx.WoWInternals;

namespace Paws.Core.Abilities.Feral
{
    /// <summary>
    /// <para>Force of Nature</para>
    /// <para>40 yd range, Feral, Talent</para>
    /// <para>Instant, 20 second recharge</para>
    /// <para>3 Charges</para>
    /// <para>Requires Druid (Feral)</para>
    /// <para>Requires level 60</para>
    /// <para>Summons a Treant which will immediately root your current target for 30 seconds, and will melee</para>
    /// <para>for (Attack power * 0.333 / 3.5 * 2 + 1) Physical damage every 2 seconds. Lasts 15 seconds.</para>
    /// <para>Maximum 3 charges.</para>
    /// <para>http://www.wowhead.com/spell=102703/force-of-nature</para>
    /// </summary>
    [AbilityChain(FriendlyName = "Force of Nature", AvailableSpecs = AvailableSpecs.Feral)]
    public class ForceOfNatureAbility : AbilityBase
    {
        public ForceOfNatureAbility()
            : base(WoWSpell.FromId(SpellBook.ForceOfNature), true, true)
        {
            base.RequiredConditions.Add(new MeHasAttackableTargetCondition());
            base.RequiredConditions.Add(new TargetDoesNotHaveAuraCondition(TargetType.Me, SpellBook.Prowl));
            base.RequiredConditions.Add(new MyTargetDistanceCondition(0, 10));
        }

        public override void ApplyDefaultSettings()
        {
            base.ApplyDefaultSettings();

            base.Conditions.Add(new BooleanCondition(Settings.ForceOfNatureEnabled));
        }
    }
}
