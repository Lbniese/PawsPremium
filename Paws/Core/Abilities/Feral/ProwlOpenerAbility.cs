﻿using Paws.Core.Conditions;
using Styx.WoWInternals;

namespace Paws.Core.Abilities.Feral
{
    /// <summary>
    ///     Chooses Rake or Shred as the prowl opener. Favors Rake over Shred if both are known and both are selected.
    /// </summary>
    public class ProwlOpenerAbility : MeleeFeralAbilityBase
    {
        public ProwlOpenerAbility()
            : base(WoWSpell.FromId(SpellBook.Rake), false)
        {
            RequiredConditions.Add(new TargetHasAuraCondition(TargetType.Me, SpellBook.Prowl));
        }

        public override void Update()
        {
            // Rake will take a higher priority than shred if both are selected as openers
            if (Settings.RakeEnabled && !Settings.RakeStealthOpener && Settings.ShredEnabled &&
                Settings.ShredStealthOpener)
                Spell = WoWSpell.FromId(SpellBook.Shred);

            // If we don't know rake, yet, set the spell to shred
            if (!Me.KnowsSpell(SpellBook.Rake))
                Spell = WoWSpell.FromId(SpellBook.Shred);
        }

        public override void ApplyDefaultSettings()
        {
            base.ApplyDefaultSettings();

            Conditions.Add(new ConditionOrList(
                new BooleanCondition(Settings.RakeEnabled && Settings.RakeStealthOpener),
                new BooleanCondition(Settings.ShredEnabled && Settings.ShredStealthOpener)
                ));
        }
    }
}