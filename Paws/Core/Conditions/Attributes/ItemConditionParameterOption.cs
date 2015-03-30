using System;
using System.Collections.Generic;

namespace Paws.Core.Conditions.Attributes
{
    public class ItemConditionParameterOption
    {
        public string Name { get; set; }
        public object Value { get; set; }

        public ItemConditionParameterOption(string name, object value)
        {
            this.Name = name;
            this.Value = value;
        }

        public static List<ItemConditionParameterOption> GenerateList(params object[] flatList)
        {
            if (flatList.Length % 2 != 0)
                throw new Exception("There must be an even number of objects to generate the ItemConditionParameterOption List.");

            var list = new List<ItemConditionParameterOption>();

            for (int i = 0; i < flatList.Length; i += 2)
            {
                list.Add(new ItemConditionParameterOption((string)flatList[i], flatList[i + 1]));
            }

            return list;
        }
    }
}
