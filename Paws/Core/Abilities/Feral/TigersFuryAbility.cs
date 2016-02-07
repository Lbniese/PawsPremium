using System;
using Paws.Core.Abilities.Attributes;
using Paws.Core.Conditions;
using Styx.WoWInternals;

namespace Paws.Core.Abilities.Feral
{
    /// <summary>
    ///     <para>Tiger's Fury</para>
    ///     <para>Instant, 30 sec cooldown</para>
    ///     <para>Requires Druid (Feral)</para>
    ///     <para>Requires level 10</para>
    ///     <para>Increases physical damage done by 15% for 8 seconds and instantly restores</para>
    ///     <para>60 Energy.</para>
    ///     <para>http://www.wowhead.com/spell=5217/tigers-fury</para>
    /// </summary>
    [AbilityChain(FriendlyName = "Tiger's Fury")]
    public class TigersFuryAbility : AbilityBase
    {
        public TigersFuryAbility()
            : base(WoWSpell.FromId(SpellBook.TigersFury), true, true)
        {
            Category = AbilityCategory.Buff;

            RequiredConditions.Add(new MeIsInCatFormCondition());
            RequiredConditions.Add(new MeHasAttackableTargetCondition());
            RequiredConditions.Add(new TargetDoesNotHaveAuraCondition(TargetType.Me, SpellBook.Prowl));
        }

        public override void ApplyDefaultSettings()
        {
            base.ApplyDefaultSettings();

            Conditions.Add(new BooleanCondition(Settings.TigersFuryEnabled));
            Conditions.Add(new TargetDoesNotHaveAuraCondition(TargetType.Me, Spell.Id));
            Conditions.Add(new MyTargetIsWithinMeleeRangeCondition());
            Conditions.Add(new ConditionTestSwitchCondition(
                new BooleanCondition(Settings.TigersFuryUseMaxEnergy),
                new ConditionOrList(
                    new MyEnergyRangeCondition(0, Settings.TigersFuryMaxEnergy),
                    new TargetHasAuraCondition(TargetType.Me, SpellBook.BloodtalonsProc)
                    )
                ));

            if (Settings.TigersFurySyncWithBerserk)
            {
                Conditions.Add(new ConditionTestSwitchCondition(
                    new MeKnowsSpellCondition(SpellBook.BerserkFeralOrGuardian),
                    new ConditionTestSwitchCondition(
                        new SpellIsOnCooldownCondition(SpellBook.BerserkFeralOrGuardian),
                        new MySpellCooldownTimeLeft(SpellBook.BerserkFeralOrGuardian, TimeSpan.FromSeconds(30))
                        )
                    ));
            }
        }
    }
}