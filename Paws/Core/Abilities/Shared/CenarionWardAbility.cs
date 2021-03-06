﻿using System;
using Paws.Core.Abilities.Attributes;
using Paws.Core.Conditions;
using Styx;
using Styx.WoWInternals;

namespace Paws.Core.Abilities.Shared
{
    /// <summary>
    ///     <para>Cenarion Ward</para>
    ///     <para>9.2% of base mana, 40 yd range</para>
    ///     <para>Instant, 30 sec cooldown</para>
    ///     <para>Requires Druid</para>
    ///     <para>Requires level 30</para>
    ///     <para>Protects a friendly target for 30 seconds.  Any damage taken will consume the ward and heal the target</para>
    ///     <para>for (879.9% of Spell power) over 6 seconds.</para>
    ///     <para>http://www.wowhead.com/spell=102351/cenarion-ward</para>
    /// </summary>
    [AbilityChain(FriendlyName = "Cenarion Ward")]
    public class CenarionWardAbility : AbilityBase
    {
        public CenarionWardAbility()
            : base(WoWSpell.FromId(SpellBook.CenarionWard), true, true)
        {
            Category = AbilityCategory.Buff;
        }

        public override void ApplyDefaultSettings()
        {
            base.ApplyDefaultSettings();

            Conditions.Add(new MeHasAttackableTargetCondition());
            Conditions.Add(new ConditionOrList(
                new ConditionTestSwitchCondition(
                    new MyExpectedSpecializationCondition(WoWSpec.DruidFeral),
                    new ConditionDependencyList(
                        new BooleanCondition(Settings.CenarionWardEnabled),
                        new TargetHealthRangeCondition(TargetType.Me, 0.0, Settings.CenarionWardMinHealth),
                        new ConditionTestSwitchCondition(
                            new BooleanCondition(Settings.HeartOfTheWildSyncWithCenarionWard),
                            new ConditionTestSwitchCondition(
                                new MeKnowsSpellCondition(SpellBook.HeartOfTheWild),
                                new ConditionTestSwitchCondition(
                                    new SpellIsOnCooldownCondition(SpellBook.HeartOfTheWild),
                                    new MySpellCooldownTimeLeft(SpellBook.HeartOfTheWild, TimeSpan.FromSeconds(30))
                                    )
                                )
                            )
                        ),
                    false
                    ),
                new ConditionTestSwitchCondition(
                    new MyExpectedSpecializationCondition(WoWSpec.DruidGuardian),
                    new ConditionDependencyList(
                        new BooleanCondition(Settings.GuardianCenarionWardEnabled),
                        new TargetHealthRangeCondition(TargetType.Me, 0.0, Settings.GuardianCenarionWardMinHealth)
                        ),
                    false
                    )
                ));
            Conditions.Add(new TargetDoesNotHaveAuraCondition(TargetType.Me, Spell.Id));
            Conditions.Add(new TargetDoesNotHaveAuraCondition(TargetType.Me, SpellBook.Prowl));
        }
    }
}