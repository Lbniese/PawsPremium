using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Paws.Core;

namespace Paws.Interface.Forms
{
    public partial class AddNewItemForm : Form
    {
        public AddNewItemForm()
        {
            InitializeComponent();

            myStateComboBox.SelectedIndex = 0;
        }

        public AddNewItemForm(PawsItem item)
        {
            InitializeComponent();

            PawsItem = item;
            itemEntryTextBox.Text = PawsItem.Entry.ToString();
            itemNameTextBox.Text = PawsItem.Name;
            myStateComboBox.SelectedIndex = (int) PawsItem.MyState;

            foreach (
                var conditionItem in
                    PawsItem.Conditions.Select(condition => new ListViewItem(condition.ToString()) {Tag = condition}))
            {
                conditionsListView.Items.Add(conditionItem);
            }
        }

        public PawsItem PawsItem { get; set; }

        private void newConditionButton_Click(object sender, EventArgs e)
        {
            var newForm = new AddNewConditionForm();

            if (newForm.ShowDialog() != DialogResult.OK) return;
            var conditionItem = new ListViewItem(newForm.ItemCondition.ToString()) {Tag = newForm.ItemCondition};

            conditionsListView.Items.Add(conditionItem);
        }

        private void removeSelectedConditionsButton_Click(object sender, EventArgs e)
        {
            if (conditionsListView.CheckedItems.Count <= 0) return;
            var result = MessageBox.Show(string.Format("You are about to remove {0} {1}. Would you like to proceed?",
                conditionsListView.CheckedItems.Count,
                conditionsListView.CheckedItems.Count == 1 ? "condition" : "conditions"),
                Properties.Resources.AddNewItemForm_removeSelectedConditionsButton_Click_Warning,
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (result != DialogResult.Yes) return;
            foreach (ListViewItem condition in conditionsListView.CheckedItems)
            {
                condition.Remove();
            }
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(itemEntryTextBox.Text))
            {
                MessageBox.Show(
                    Properties.Resources.AddNewItemForm_saveButton_Click_You_must_enter_an_item_id_to_continue_,
                    Properties.Resources.AddNewItemForm_saveButton_Click_Notice, MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                return;
            }

            if (string.IsNullOrEmpty(itemNameTextBox.Text))
            {
                MessageBox.Show(
                    Properties.Resources.AddNewItemForm_saveButton_Click_You_must_enter_an_item_name_to_continue_,
                    Properties.Resources.AddNewItemForm_saveButton_Click_Notice, MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                return;
            }

            PawsItem = new PawsItem
            {
                Name = itemNameTextBox.Text,
                Entry = Convert.ToInt32(itemEntryTextBox.Text),
                Enabled = true,
                MyState = (MyState) myStateComboBox.SelectedIndex,
                Conditions = new List<ItemCondition>()
            };

            foreach (ListViewItem conditionListItem in conditionsListView.Items)
            {
                var theCondition = conditionListItem.Tag as ItemCondition;
                PawsItem.Conditions.Add(theCondition);
            }

            DialogResult = DialogResult.OK;
        }

        private void myBagsButton_Click(object sender, EventArgs e)
        {
            var newForm = new AddItemMyBagsForm();

            if (newForm.ShowDialog() != DialogResult.OK) return;
            var selectedItem = newForm.carriedItemsComboBox.SelectedItem as ItemSelectionEntry;

            if (selectedItem == null) return;
            itemEntryTextBox.Text = selectedItem.Entry.ToString();
            itemNameTextBox.Text = selectedItem.Name;
        }
    }
}