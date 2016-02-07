using Paws.Core.Abilities.Attributes;
using Paws.Core.Conditions;
using Styx.WoWInternals;

namespace Paws.Core.Abilities.Feral
{
    /// <summary>
    ///     <para>Incarnation: King of the Jungle, Talent, Shapeshift</para>
    ///     <para>Instant, 3 min cooldown</para>
    ///     <para>Requires Druid (Feral)</para>
    ///     <para>Requires level 60</para>
    ///     <para>An improved Cat Form that allows the use of Prowl while in combat and causes Shred</para>
    ///     <para>and Rake to function as if stealth were active. Lasts 30 seconds.</para>
    ///     <para>You may shapeshift in and out of this improved Cat Form for its duration.</para>
    ///     <para>http://www.wowhead.com/spell=102543/incarnation-king-of-the-jungle#comments</para>
    /// </summary>
    [AbilityChain(FriendlyName = "Incarnation: King of the Jungle")]
    public class IncarnationAbility : AbilityBase
    {
        public IncarnationAbility()
            : base(WoWSpell.FromId(SpellBook.FeralIncarnationForm), true, true)
        {
            Category = AbilityCategory.Buff;

            RequiredConditions.Add(new MeHasAttackableTargetCondition());
            RequiredConditions.Add(new TargetDoesNotHaveAuraCondition(TargetType.Me, SpellBook.FeralIncarnationForm));
            RequiredConditions.Add(new TargetDoesNotHaveAuraCondition(TargetType.Me, SpellBook.Prowl));
        }

        public override void ApplyDefaultSettings()
        {
            base.ApplyDefaultSettings();

            Conditions.Add(new BooleanCondition(Settings.IncarnationEnabled));
            Conditions.Add(new MyTargetIsWithinMeleeRangeCondition());
            if (Settings.IncarnationEnemyHealthCheck)
            {
                Conditions.Add(new MyTargetHealthMultiplierCondition(Settings.IncarnationEnemyHealthMultiplier));
            }
            if (Settings.IncarnationSurroundedByEnemiesEnabled)
            {
                Conditions.Add(new AttackableTargetsMinCountCondition(Settings.IncarnationSurroundedByMinEnemies));
            }
        }
    }
}