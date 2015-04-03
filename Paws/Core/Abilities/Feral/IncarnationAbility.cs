using Paws.Core.Conditions;
using Paws.Core.Abilities.Attributes;
using Styx.WoWInternals;

namespace Paws.Core.Abilities.Feral
{
    /// <summary>
    /// <para>Incarnation: King of the Jungle, Talent, Shapeshift</para>
    /// <para>Instant, 3 min cooldown</para>
    /// <para>Requires Druid (Feral)</para>
    /// <para>Requires level 60</para>
    /// <para>An improved Cat Form that allows the use of Prowl while in combat and causes Shred</para>
    /// <para>and Rake to function as if stealth were active. Lasts 30 seconds.</para>
    /// <para>You may shapeshift in and out of this improved Cat Form for its duration.</para>
    /// <para>http://www.wowhead.com/spell=102543/incarnation-king-of-the-jungle#comments</para>
    /// </summary>
    [AbilityChain(FriendlyName = "Incarnation: King of the Jungle")]
    public class IncarnationAbility : AbilityBase
    {
        public IncarnationAbility()
            : base(WoWSpell.FromId(SpellBook.FeralIncarnationForm), true, true)
        {
            base.Category = AbilityCategory.Buff;
        }

        public override void ApplyDefaultSettings()
        {
            base.ApplyDefaultSettings();

            base.Conditions.Add(new BooleanCondition(Settings.IncarnationEnabled));
            base.Conditions.Add(new MeHasAttackableTargetCondition());
            base.Conditions.Add(new TargetDoesNotHaveAuraCondition(TargetType.Me, SpellBook.FeralIncarnationForm));
            base.Conditions.Add(new TargetDoesNotHaveAuraCondition(TargetType.Me, SpellBook.Prowl));
            base.Conditions.Add(new MyTargetIsWithinMeleeRangeCondition());
            if (Settings.IncarnationEnemyHealthCheck)
            {
                base.Conditions.Add(new MyTargetHealthMultiplierCondition(Settings.IncarnationEnemyHealthMultiplier));
            }
            if (Settings.IncarnationSurroundedByEnemiesEnabled)
            {
                base.Conditions.Add(new AttackableTargetsMinCountCondition(Settings.IncarnationSurroundedByMinEnemies));
            }
        }
    }
}
