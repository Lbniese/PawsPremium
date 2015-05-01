using Paws.Core.Conditions;

namespace Paws.Core.Abilities
{
    public class ChainedAbility
    {
        public IAbility Ability { get; set; }
        public TargetType TargetType { get; set; }
        public bool IsRequired { get; set; }

        public ChainedAbility(IAbility ability, TargetType targetType, bool isRequired = true)
        {
            this.Ability = ability;
            this.TargetType = targetType;
            this.IsRequired = isRequired;
        }
    }
}
