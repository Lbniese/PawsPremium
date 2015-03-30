using Paws.Core.Conditions.Attributes;
using System;
using System.Reflection;
using System.Windows.Forms;

namespace Paws.Interface
{
    public partial class LongTextBoxValueControl : UserControl, IItemConditionParameterControl
    {
        public PropertyInfo BoundProperty { get; set; }

        public LongTextBoxValueControl(PropertyInfo property)
        {
            InitializeComponent();

            // Knowing the property info allows us to dynamically generarte the available options based on the class attributes
            // and bind the results to a specified type

            this.BoundProperty = property;

            // we need to know what options are available - the property type can tell us based on the set attributes.

            var parameterAttribute = this.BoundProperty.GetCustomAttribute<ItemConditionParameterAttribute>();

            if (parameterAttribute == null)
                throw new Exception("Invalid Property Type Passed to TwoRadioOptionsControl. Ensure that the property contains an ItemConditionParameterAttribute.");

            this.ValueLabel.Text = string.IsNullOrEmpty(parameterAttribute.Name) ? property.Name : parameterAttribute.Name;
        }

        public object GetParameterValue()
        {
            return this.ValueTextBox.Text;
        }
    }
}
