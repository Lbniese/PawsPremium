using Paws.Core.Conditions.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace Paws.Interface
{
    public partial class TwoRadioOptionsControl : UserControl, IItemConditionParameterControl
    {
        public PropertyInfo BoundProperty { get; set; }
        public List<ItemConditionParameterOption> BoundOptions { get; set; }

        /// <summary>
        /// Requires that the bound property has two options in order to bind properly.
        /// </summary>
        public TwoRadioOptionsControl(PropertyInfo property)
        {
            InitializeComponent();

            // Knowing the property info allows us to dynamically generarte the available options based on the class attributes
            // and bind the results to a specified type

            this.BoundProperty = property;

            var parameterAttribute = this.BoundProperty.GetCustomAttribute<ItemConditionParameterAttribute>();

            if (parameterAttribute == null)
                throw new Exception("Invalid Property Type Passed to TwoRadioOptionsControl. Ensure that the property contains an ItemConditionParameterAttribute with two options present.");

            if (parameterAttribute.Options == null || parameterAttribute.Options.Count() != 2)
                throw new Exception("Invalid Property Type Passed to TwoRadioOptionsControl. Ensure that the property contains an ItemConditionParameterAttribute with two options present.");

            this.BoundOptions = parameterAttribute.Options;
            
            this.ValueLabel.Text = string.IsNullOrEmpty(parameterAttribute.Name) ? property.Name : parameterAttribute.Name;

            this.OptionOneRadioButton.Text = (parameterAttribute.Options[0] as ItemConditionParameterOption).Name;
            this.OptionTwoRadioButton.Text = (parameterAttribute.Options[1] as ItemConditionParameterOption).Name;
        }

        public object GetParameterValue()
        {
            if (this.OptionOneRadioButton.Checked) return this.BoundOptions[0].Value;
            if (this.OptionTwoRadioButton.Checked) return this.BoundOptions[1].Value;

            return null;
        }
    }
}
