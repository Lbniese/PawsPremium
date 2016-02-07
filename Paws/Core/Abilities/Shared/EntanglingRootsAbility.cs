using Paws.Core.Abilities.Attributes;
using Styx.WoWInternals;

namespace Paws.Core.Abilities.Shared
{
    [AbilityChain(FriendlyName = "Entangling Roots")]
    public class EntanglingRootsAbility : AbilityBase
    {
        public EntanglingRootsAbility()
            : base(WoWSpell.FromId(SpellBook.EntanglingRoots), true, true)
        {
            Category = AbilityCategory.Defensive;
        }
    }
}