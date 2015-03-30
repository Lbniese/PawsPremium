using System;
using System.Collections.Generic;

namespace Paws.Core.Abilities
{
    [Serializable]
    public class BlockedAbilityList : HashSet<BlockedAbility>
    {
        public BlockedAbility GetBlockedAbilityByType(Type t)
        {
            foreach(var blockedAbility in this)
            {
                var type = blockedAbility.GetType();

                if (blockedAbility.Ability.GetType().Equals(t))
                {
                    return blockedAbility;
                }
            }

            return null;
        }
    }
}
