﻿using Paws.Core.Abilities.Attributes;
using Styx.WoWInternals;

namespace Paws.Core.Abilities.Shared
{
    [AbilityChain(FriendlyName = "Cyclone")]
    public class CycloneAbility : AbilityBase
    {
        public CycloneAbility()
            : base(WoWSpell.FromId(SpellBook.Cyclone), true, true)
        {
            Category = AbilityCategory.Defensive;
        }
    }
}