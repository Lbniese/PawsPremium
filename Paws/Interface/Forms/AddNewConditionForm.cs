using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Paws.Core;
using Paws.Core.Conditions;
using Paws.Core.Managers;
using Paws.Interface.Controls;

namespace Paws.Interface.Forms
{
    public partial class AddNewConditionForm : Form
    {
        public AddNewConditionForm()
        {
            InitializeComponent();
        }

        /// <summary>
        ///     Gets or sets the Item Condition associated with this form.
        /// </summary>
        public ItemCondition ItemCondition { get; set; }

        private void AddNewConditionForm_Load(object sender, EventArgs e)
        {
            foreach (var itemCondition in ItemManager.GetAllowedItemConditions())
            {
                conditionsComboBox.Items.Add(itemCondition);
            }
        }

        private void conditionsComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selectedItemCondition = conditionsComboBox.SelectedItem as ItemCondition;

            if (selectedItemCondition == null) return;
            typeLabel.Text = selectedItemCondition.ClassType.Name;
            parametersPanel.Controls.Clear();

            if (selectedItemCondition.RequiredProperties.Count > 0)
            {
                foreach (var property in selectedItemCondition.RequiredProperties)
                {
                    // Bind the controls to the property. This allows us to dynamically build the necessary parameters based on
                    // what the class' conditions needs are and keeps everything decoupled from the GUI.
                    if (property.PropertyType == typeof (string))
                    {
                        parametersPanel.Controls.Add(new LongTextBoxValueControl(property) {Dock = DockStyle.Top});
                    }
                    else if (property.PropertyType == typeof (TargetType) || property.PropertyType == typeof (bool))
                    {
                        parametersPanel.Controls.Add(new TwoRadioOptionsControl(property) {Dock = DockStyle.Top});
                    }
                    else if (property.PropertyType == typeof (int) || property.PropertyType == typeof (double) ||
                             property.PropertyType == typeof (float))
                    {
                        parametersPanel.Controls.Add(new NumberTextBoxValueControl(property) {Dock = DockStyle.Top});
                    }
                }
            }
            else
            {
                var noParametersLabel = new Label
                {
                    AutoSize = true,
                    ForeColor = Color.White,
                    Text =
                        Properties.Resources
                            .AddNewConditionForm_conditionsComboBox_SelectedIndexChanged_There_are_no_parameters_required_for_this_condition_
                };

                parametersPanel.Controls.Add(noParametersLabel);
            }
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            ItemCondition = conditionsComboBox.SelectedItem as ItemCondition;

            if (ItemCondition == null)
            {
                MessageBox.Show(Properties.Resources.AddNewConditionForm_saveButton_Click_Please_select_a_Condition_,
                    Properties.Resources.AddNewConditionForm_saveButton_Click_Warning, MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            // Dynamically create an instance of our type.
            ItemCondition.CreateInstance();

            // This is where we map our properties based on the required parameters
            foreach (var rawControl in parametersPanel.Controls)
            {
                // We don't want the label that's created if there are no required parameters
                var control = rawControl as IItemConditionParameterControl;

                if (control == null) continue;
                var property = ItemCondition.RequiredProperties
                    .SingleOrDefault(o => o == control.BoundProperty);

                ItemCondition.SetPropertyValue(property, control.GetParameterValue());
            }

            DialogResult = DialogResult.OK;
        }
    }
}