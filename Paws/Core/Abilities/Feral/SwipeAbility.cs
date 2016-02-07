using Paws.Core.Abilities.Attributes;
using Paws.Core.Conditions;
using Styx.WoWInternals;

namespace Paws.Core.Abilities.Feral
{
    /// <summary>
    ///     <para>Swipe</para>
    ///     <para>45 Energy, 8 yd range</para>
    ///     <para>Instant</para>
    ///     <para>Requires Druid (Feral)</para>
    ///     <para>Requires level 22</para>
    ///     <para>Requires Cat Form</para>
    ///     <para>Swipe nearby enemies, inflicting 175% Physical damage. Awards 1 combo point.</para>
    ///     <para>Does 20% additional damage against bleeding targets.</para>
    ///     <para>http://www.wowhead.com/spell=106785/swipe</para>
    /// </summary>
    [AbilityChain(FriendlyName = "Swipe")]
    public class SwipeAbility : AbilityBase
    {
        public SwipeAbility()
            : base(WoWSpell.FromId(SpellBook.FeralSwipe), true, true)
        {
            RequiredConditions.Add(new MeHasAttackableTargetCondition());
            RequiredConditions.Add(new TargetDoesNotHaveAuraCondition(TargetType.Me, SpellBook.Prowl));
            RequiredConditions.Add(new MeIsFacingTargetCondition());
            RequiredConditions.Add(new MeIsInCatFormCondition());
            RequiredConditions.Add(new ConditionTestSwitchCondition(
                new TargetHasAuraCondition(TargetType.Me, SpellBook.BerserkDruid),
                new MyEnergyRangeCondition(50.0/2.0),
                new MyEnergyRangeCondition(50.0)
                ));
        }

        public override void ApplyDefaultSettings()
        {
            base.ApplyDefaultSettings();

            Conditions.Add(new BooleanCondition(Settings.SwipeEnabled));
            Conditions.Add(new AttackableTargetsMinCountCondition(Settings.SwipeMinEnemies));
            Conditions.Add(new MyTargetDistanceCondition(0, Settings.AoeRange));
            if (Settings.SavageRoarEnabled)
            {
                Conditions.Add(new ConditionTestSwitchCondition(
                    new MeKnowsSpellCondition(SpellBook.SavageRoar),
                    new MySavageRoarAuraCondition()
                    ));
            }
        }
    }
}