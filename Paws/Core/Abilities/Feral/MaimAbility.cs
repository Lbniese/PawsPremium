using Paws.Core.Abilities.Attributes;
using Paws.Core.Conditions;
using Styx.WoWInternals;

namespace Paws.Core.Abilities.Feral
{
    /// <summary>
    ///     <para>Maim</para>
    ///     <para>35 Energy / 1 Energy, Melee Range</para>
    ///     <para>Instant, 10 sec cooldown</para>
    ///     <para>Requires Druid (Feral)</para>
    ///     <para>Requires level 82</para>
    ///     <para>Requires Cat Form</para>
    ///     <para>Finishing move that causes Physical damage and stuns the target. Damage and durationincreased per</para>
    ///     <para>Combo point:</para>
    ///     <para>1 point : 70% damage, 1 sec</para>
    ///     <para>2 points: 80% damage, 2 sec</para>
    ///     <para>3 points: 90% damage, 3 sec</para>
    ///     <para>4 points: 100% damage, 4 sec</para>
    ///     <para>5 points: 110% damage, 5 sec</para>
    ///     <para>http://www.wowhead.com/spell=22570/maim</para>
    /// </summary>
    [AbilityChain(FriendlyName = "Maim (Stun)")]
    public class MaimAbility : MeleeFeralAbilityBase
    {
        public MaimAbility()
            : base(WoWSpell.FromId(SpellBook.Maim), false)
        {
            RequiredConditions.Add(new SpellIsNotOnCooldownCondition(Spell));
            RequiredConditions.Add(new TargetDoesNotHaveAuraCondition(TargetType.Me, SpellBook.Prowl));
            RequiredConditions.Add(new MyEnergyRangeCondition(35.0));
            Conditions.Add(new MyComboPointsCondition(1));
        }

        public override void ApplyDefaultSettings()
        {
            base.ApplyDefaultSettings();

            Conditions.Add(new BooleanCondition(Settings.MaimEnabled));
            Conditions.Add(new MyComboPointsCondition(Settings.MaimMinComboPoints));
            Conditions.Add(new TargetDoesNotHaveAuraCondition(TargetType.MyCurrentTarget, Spell.Id));
            Conditions.Add(new TargetDoesNotHaveSpellMechanicCondition(TargetType.MyCurrentTarget,
                WoWSpellMechanic.Stunned));
        }
    }
}