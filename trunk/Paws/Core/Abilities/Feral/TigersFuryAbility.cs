using Paws.Core.Conditions;
using Paws.Core.Abilities.Attributes;
using Styx.WoWInternals;
using System;

namespace Paws.Core.Abilities.Feral
{
    /// <summary>
    /// <para>Tiger's Fury</para>
    /// <para>Instant, 30 sec cooldown</para>
    /// <para>Requires Druid (Feral)</para>
    /// <para>Requires level 10</para>
    /// <para>Increases physical damage done by 15% for 8 seconds and instantly restores</para>
    /// <para>60 Energy.</para>
    /// <para>http://www.wowhead.com/spell=5217/tigers-fury</para>
    /// </summary>
    [AbilityChain(FriendlyName = "Tiger's Fury")]
    public class TigersFuryAbility : AbilityBase
    {
        public TigersFuryAbility()
            : base(WoWSpell.FromId(SpellBook.TigersFury), true, true)
        {
            base.Category = AbilityCategory.Buff;

            base.RequiredConditions.Add(new MeIsInCatFormCondition());
            base.RequiredConditions.Add(new MeHasAttackableTargetCondition());
            base.RequiredConditions.Add(new TargetDoesNotHaveAuraCondition(TargetType.Me, SpellBook.Prowl));
        }

        public override void ApplyDefaultSettings()
        {
            base.ApplyDefaultSettings();

            base.Conditions.Add(new BooleanCondition(Settings.TigersFuryEnabled));
            base.Conditions.Add(new TargetDoesNotHaveAuraCondition(TargetType.Me, base.Spell.Id));
            base.Conditions.Add(new MyTargetIsWithinMeleeRangeCondition());
            base.Conditions.Add(new ConditionTestSwitchCondition(
                new BooleanCondition(Settings.TigersFuryUseMaxEnergy),
                new ConditionOrList(
                    new MyEnergyRangeCondition(0, Settings.TigersFuryMaxEnergy),
                    new TargetHasAuraCondition(TargetType.Me, SpellBook.BloodtalonsProc)
                )
            ));

            if (Settings.TigersFurySyncWithBerserk)
            {
                base.Conditions.Add(new ConditionTestSwitchCondition(
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
