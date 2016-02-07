using Paws.Core.Conditions;
using Styx.WoWInternals;

namespace Paws.Core.Abilities.Guardian
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
    public class BerserkAbility : AbilityBase
    {
        public BerserkAbility()
            : base(WoWSpell.FromId(SpellBook.BerserkGuardian), false, true)
        {
            Category = AbilityCategory.Buff;
        }

        public override void ApplyDefaultSettings()
        {
            base.ApplyDefaultSettings();

            Conditions.Add(new BooleanCondition(Settings.GuardianBerserkEnabled));
            Conditions.Add(new MeIsInBearFormCondition());
            Conditions.Add(new MeHasAttackableTargetCondition());
            Conditions.Add(new TargetDoesNotHaveAuraCondition(TargetType.Me, Spell.Id));
            Conditions.Add(new MyTargetIsWithinMeleeRangeCondition());
            if (Settings.GuardianBerserkEnemyHealthCheck)
            {
                Conditions.Add(new MyTargetHealthMultiplierCondition(Settings.GuardianBerserkEnemyHealthMultiplier));
            }
            if (Settings.GuardianBerserkSurroundedByEnemiesEnabled)
            {
                Conditions.Add(new AttackableTargetsMinCountCondition(Settings.GuardianBerserkSurroundedByMinEnemies));
            }
        }
    }
}