using System;

namespace Paws.Core.Abilities
{
    public sealed class BlockedAbility
    {
        public BlockedAbility(IAbility ability, DateTime dateTime)
        {
            Ability = ability;
            BlockedDateAndTime = dateTime;
        }

        public IAbility Ability { get; set; }
        public DateTime BlockedDateAndTime { get; set; }
    }
}