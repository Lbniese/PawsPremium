using System;

namespace Paws.Core.Conditions.Attributes
{
    [AttributeUsage(AttributeTargets.Class, Inherited = true)]
    public class ItemConditionAttribute : Attribute
    {
        public string FriendlyName { get; set; }
    }
}
