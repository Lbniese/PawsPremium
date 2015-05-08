using Paws.Core;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Paws.Interface
{
    public partial class AddNewItemForm : Form
    {
        public PawsItem PawsItem { get; set; }

        public AddNewItemForm()
        {
            InitializeComponent();

            this.myStateComboBox.SelectedIndex = 0;
        }

        public AddNewItemForm(PawsItem item)
        {
            InitializeComponent();

            this.PawsItem = item;
            this.itemNameTextBox.Text = this.PawsItem.Name;
            this.myStateComboBox.SelectedIndex = (int)this.PawsItem.MyState;

            foreach(var condition in this.PawsItem.Conditions)
            {
                var conditionItem = new ListViewItem(condition.ToString());
                conditionItem.Tag = condition;

                this.conditionsListView.Items.Add(conditionItem);
            }
        }

        private void newConditionButton_Click(object sender, EventArgs e)
        {
            AddNewConditionForm newForm = new AddNewConditionForm();

            if (newForm.ShowDialog() == DialogResult.OK)
            {
                var conditionItem = new ListViewItem(newForm.ItemCondition.ToString());
                conditionItem.Tag = newForm.ItemCondition;

                this.conditionsListView.Items.Add(conditionItem);
            }
        }

        private void removeSelectedConditionsButton_Click(object sender, EventArgs e)
        {
            if (this.conditionsListView.CheckedItems.Count > 0)
            {
                var result = MessageBox.Show(string.Format("You are about to remove {0} {1}. Would you like to proceed?",
                    this.conditionsListView.CheckedItems.Count, this.conditionsListView.CheckedItems.Count == 1 ? "condition" : "conditions"), 
                    "Warning", 
                    MessageBoxButtons.YesNo, 
                    MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    foreach (ListViewItem condition in this.conditionsListView.CheckedItems)
                    {
                        condition.Remove();
                    }
                }
            }
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.itemNameTextBox.Text))
            {
                MessageBox.Show("You must enter an item name to continue.", "Notice", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            this.PawsItem = new PawsItem()
            {
                Name = this.itemNameTextBox.Text,
                Enabled = true,
                MyState = (MyState)this.myStateComboBox.SelectedIndex,
                Conditions = new List<ItemCondition>()
            };

            foreach (ListViewItem conditionListItem in this.conditionsListView.Items)
            {
                var theCondition = conditionListItem.Tag as ItemCondition;
                this.PawsItem.Conditions.Add(theCondition);
            }

            this.DialogResult = DialogResult.OK;
        }

        private void myBagsButton_Click(object sender, EventArgs e)
        {
            var newForm = new AddItemMyBagsForm();

            if (newForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.itemNameTextBox.Text = newForm.carriedItemsComboBox.Text;
            }
        }
    }
}
