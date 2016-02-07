using System;
using System.Reflection;
using System.Windows.Forms;
using Paws.Core.Conditions.Attributes;

namespace Paws.Interface.Controls
{
    public partial class NumberTextBoxValueControl : UserControl, IItemConditionParameterControl
    {
        public NumberTextBoxValueControl(PropertyInfo property)
        {
            InitializeComponent();

            // Knowing the property info allows us to dynamically generarte the available options based on the class attributes
            // and bind the results to a specified type

            BoundProperty = property;

            // we need to know what options are available - the property type can tell us based on the set attributes.

            var parameterAttribute = BoundProperty.GetCustomAttribute<ItemConditionParameterAttribute>();

            if (parameterAttribute == null)
                throw new Exception(
                    "Invalid Property Type Passed to TwoRadioOptionsControl. Ensure that the property contains an ItemConditionParameterAttribute.");

            ValueLabel.Text = string.IsNullOrEmpty(parameterAttribute.Name) ? property.Name : parameterAttribute.Name;
            DescriptorLabel.Text = parameterAttribute.Descriptor;
        }

        public PropertyInfo BoundProperty { get; set; }

        public object GetParameterValue()
        {
            return ValueTextBox.Text;
        }
    }
}