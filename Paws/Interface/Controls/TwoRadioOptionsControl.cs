using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using Paws.Core.Conditions.Attributes;

namespace Paws.Interface.Controls
{
    public partial class TwoRadioOptionsControl : UserControl, IItemConditionParameterControl
    {
        /// <summary>
        ///     Requires that the bound property has two options in order to bind properly.
        /// </summary>
        public TwoRadioOptionsControl(PropertyInfo property)
        {
            InitializeComponent();

            // Knowing the property info allows us to dynamically generarte the available options based on the class attributes
            // and bind the results to a specified type

            BoundProperty = property;

            var parameterAttribute = BoundProperty.GetCustomAttribute<ItemConditionParameterAttribute>();

            if (parameterAttribute == null)
                throw new Exception(
                    "Invalid Property Type Passed to TwoRadioOptionsControl. Ensure that the property contains an ItemConditionParameterAttribute with two options present.");

            if (parameterAttribute.Options == null || parameterAttribute.Options.Count() != 2)
                throw new Exception(
                    "Invalid Property Type Passed to TwoRadioOptionsControl. Ensure that the property contains an ItemConditionParameterAttribute with two options present.");

            BoundOptions = parameterAttribute.Options;

            ValueLabel.Text = string.IsNullOrEmpty(parameterAttribute.Name) ? property.Name : parameterAttribute.Name;

            OptionOneRadioButton.Text = parameterAttribute.Options[0].Name;
            OptionTwoRadioButton.Text = parameterAttribute.Options[1].Name;
        }

        public List<ItemConditionParameterOption> BoundOptions { get; set; }
        public PropertyInfo BoundProperty { get; set; }

        public object GetParameterValue()
        {
            if (OptionOneRadioButton.Checked) return BoundOptions[0].Value;
            return OptionTwoRadioButton.Checked ? BoundOptions[1].Value : null;
        }
    }
}