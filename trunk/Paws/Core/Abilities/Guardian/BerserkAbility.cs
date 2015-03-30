﻿using Paws.Core.Conditions;
using Styx.CommonBot;
using Styx.WoWInternals;

namespace Paws.Core.Abilities.Guardian
{
    /// <summary>
    /// <para>Berserk</para>
    /// <para>Instant, 3 min cooldown</para>
    /// <para>Requires Druid (Feral, Guardian)</para>
    /// <para>Requires level 48</para>
    /// <para>When used in Bear Form, removes the cooldown from mangle and causes it to hit up to 3 targets</para>
    /// <para>and lasts 10 seconds.</para>
    /// <para>When used in Cat Form, reduces the cost of all Cat Form abilities by 50% and lasts 15 seconds.</para>
    /// <para>Empowered Berserk (Level 92+)</para>
    /// <para>Increases the duration of Berserk by 5 seconds.</para>
    /// <para>http://www.wowhead.com/spell=106952/berserk</para>
    /// </summary>
    public class BerserkAbility : AbilityBase
    {
        public BerserkAbility()
            : base(WoWSpell.FromId(SpellBook.BerserkGuardian), false, true)
        {
            base.Category = AbilityCategory.Buff;

            base.Conditions.Add(new BooleanCondition(Settings.GuardianBerserkEnabled));
            base.Conditions.Add(new MeIsInBearFormCondition());
            base.Conditions.Add(new MeHasAttackableTargetCondition());
            base.Conditions.Add(new TargetDoesNotHaveAuraCondition(TargetType.Me, this.Spell.Id));
            base.Conditions.Add(new MyTargetIsWithinMeleeRangeCondition());
            if (Settings.GuardianBerserkEnemyHealthCheck)
            {
                base.Conditions.Add(new MyTargetHealthMultiplierCondition(Settings.GuardianBerserkEnemyHealthMultiplier));
            }
            if (Settings.GuardianBerserkSurroundedByEnemiesEnabled)
            {
                base.Conditions.Add(new AttackableTargetsMinCountCondition(Settings.GuardianBerserkSurroundedByMinEnemies));
            }
        }
    }
}
