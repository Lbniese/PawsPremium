using Paws.Core.Abilities.Attributes;
using Paws.Core.Conditions;
using Styx.WoWInternals;

namespace Paws.Core.Abilities.Feral
{
    /// <summary>
    ///     <para>Berserk</para>
    ///     <para>Instant, 3 min cooldown</para>
    ///     <para>Requires Druid (Feral, Guardian)</para>
    ///     <para>Requires level 48</para>
    ///     <para>When used in Bear Form, removes the cooldown from mangle and causes it to hit up to 3 targets</para>
    ///     <para>and lasts 10 seconds.</para>
    ///     <para>When used in Cat Form, reduces the cost of all Cat Form abilities by 50% and lasts 15 seconds.</para>
    ///     <para>Empowered Berserk (Level 92+)</para>
    ///     <para>Increases the duration of Berserk by 5 seconds.</para>
    ///     <para>http://www.wowhead.com/spell=106952/berserk</para>
    /// </summary>
    [AbilityChain(FriendlyName = "Berserk")]
    public class BerserkAbility : AbilityBase
    {
        public BerserkAbility()
            : base(WoWSpell.FromId(SpellBook.BerserkDruid), false, true)
        {
            Category = AbilityCategory.Buff;

            RequiredConditions.Add(new MeIsInCatFormCondition());
            RequiredConditions.Add(new MeHasAttackableTargetCondition());
            RequiredConditions.Add(new TargetDoesNotHaveAuraCondition(TargetType.Me, SpellBook.BerserkDruid));
            RequiredConditions.Add(new TargetDoesNotHaveAuraCondition(TargetType.Me, SpellBook.Prowl));
            RequiredConditions.Add(new MyTargetIsWithinMeleeRangeCondition());
        }

        public override void ApplyDefaultSettings()
        {
            base.ApplyDefaultSettings();

            Conditions.Add(new BooleanCondition(Settings.BerserkEnabled));
            if (Settings.BerserkEnemyHealthCheck)
            {
                Conditions.Add(new MyTargetHealthMultiplierCondition(Settings.BerserkEnemyHealthMultiplier));
            }
            if (Settings.BerserkSurroundedByEnemiesEnabled)
            {
                Conditions.Add(new AttackableTargetsMinCountCondition(Settings.BerserkSurroundedByMinEnemies));
            }
        }
    }
}