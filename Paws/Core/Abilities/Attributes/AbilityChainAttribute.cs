using Styx;
using System;
using System.Collections.Generic;

namespace Paws.Core.Abilities.Attributes
{
    [AttributeUsage(AttributeTargets.Class, Inherited = true)]
    public class AbilityChainAttribute : Attribute
    {
        public string FriendlyName { get; set; }
        public AvailableSpecs AvailableSpecs { get; set; }
    }
}
