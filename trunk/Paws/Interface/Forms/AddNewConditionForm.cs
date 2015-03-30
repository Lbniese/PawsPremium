using Paws.Core;
using Paws.Core.Conditions;
using Paws.Core.Managers;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Paws.Interface
{
    public partial class AddNewConditionForm : Form
    {
        /// <summary>
        /// Gets or sets the Item Condition associated with this form.
        /// </summary>
        public ItemCondition ItemCondition { get; set; }

        public AddNewConditionForm()
        {
            InitializeComponent();
        }

        private void AddNewConditionForm_Load(object sender, EventArgs e)
        {
            foreach (var itemCondition in ItemManager.GetAllowedItemConditions())
            {
                this.conditionsComboBox.Items.Add(itemCondition);
            }
        }

        private void conditionsComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selectedItemCondition = this.conditionsComboBox.SelectedItem as ItemCondition;

            if (selectedItemCondition != null)
            {
                this.typeLabel.Text = selectedItemCondition.ClassType.Name;
                this.parametersPanel.Controls.Clear();

                if (selectedItemCondition.RequiredProperties.Count > 0)
                {
                    foreach (var property in selectedItemCondition.RequiredProperties)
                    {
                        // Bind the controls to the property. This allows us to dynamically build the necessary parameters based on
                        // what the class' conditions needs are and keeps everything decoupled from the GUI.
                        if (property.PropertyType == typeof(string))
                        {
                            this.parametersPanel.Controls.Add(new LongTextBoxValueControl(property) { Dock = DockStyle.Top });
                        }
                        else if (property.PropertyType == typeof(TargetType) || property.PropertyType == typeof(bool))
                        {
                            this.parametersPanel.Controls.Add(new TwoRadioOptionsControl(property) { Dock = DockStyle.Top });
                        }
                        else if (property.PropertyType == typeof(int) || property.PropertyType == typeof(double) || property.PropertyType == typeof(float))
                        {
                            this.parametersPanel.Controls.Add(new NumberTextBoxValueControl(property) { Dock = DockStyle.Top });
                        }
                    }
                }
                else
                {
                    Label noParametersLabel = new Label()
                    {
                        AutoSize = true,
                        ForeColor = Color.White,
                        Text = "There are no parameters required for this condition."
                    };

                    this.parametersPanel.Controls.Add(noParametersLabel);
                }
            }
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            this.ItemCondition = this.conditionsComboBox.SelectedItem as ItemCondition;

            if (this.ItemCondition == null)
            {
                MessageBox.Show("Please select a Condition.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Dynamically create an instance of our type.
            this.ItemCondition.CreateInstance();

            // This is where we map our properties based on the required parameters
            foreach (var rawControl in this.parametersPanel.Controls)
            {
                // We don't want the label that's created if there are no required parameters
                var control = rawControl as IItemConditionParameterControl;

                if (control != null)
                {
                    var property = this.ItemCondition.RequiredProperties
                        .SingleOrDefault(o => o == control.BoundProperty);

                    this.ItemCondition.SetPropertyValue(property, control.GetParameterValue());
                }
            }

            this.DialogResult = DialogResult.OK;
        }
    }
}
