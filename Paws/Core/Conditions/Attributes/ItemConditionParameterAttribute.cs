using System;
using System.Collections.Generic;

namespace Paws.Core.Conditions.Attributes
{
    [AttributeUsage(AttributeTargets.Property, Inherited = true)]
    public class ItemConditionParameterAttribute : Attribute
    {
        /// <summary>
        ///  Only necessary if we want the label to be different than the property name itself.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Useful for setting the descriptor type of a property, such as "%" or "yd"
        /// </summary>
        public string Descriptor { get; set; }

        public List<ItemConditionParameterOption> Options { get; set; }

        public ItemConditionParameterAttribute() { }

        // Unfortunately, for how attributes list, we can only pass in primitive types, so we are going to use this bit of an ugly hack.
        public ItemConditionParameterAttribute(params object[] options)
        {
            this.Options = ItemConditionParameterOption.GenerateList(options);
        }
    }
}
