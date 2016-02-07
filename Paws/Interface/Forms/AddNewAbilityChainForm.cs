using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Paws.Core;
using Paws.Core.Conditions;
using Styx.Common;

namespace Paws.Interface.Forms
{
    public partial class AddNewAbilityChainForm : Form
    {
        private bool _keyIsDown;

        private bool _pressHotKeyNowMode;

        public AddNewAbilityChainForm(AbilityChain abilityChain)
        {
            InitializeComponent();

            HotKey = Keys.None;
            ChainedAbilities = new List<ChainedAbility>();
            modifierKeyComboBox.SelectedIndex = 0;

            if (abilityChain == null) return;
            abilityChainNameTextBox.Text = abilityChain.Name;

            HotKey = abilityChain.HotKey;
            hotKeyTriggerSetKeyButton.Text = HotKey.ToString();
            hotKeyTriggerSetKeyButton.ForeColor = Color.Green;

            ModifierKey = abilityChain.ModiferKey;

            modifierKeyComboBox.SelectedIndex = ConvertModifierKeyToComboBoxIndex(abilityChain.ModiferKey);

            foreach (var chainedAbility in abilityChain.ChainedAbilities)
            {
                var abilityItem = new ListViewItem(chainedAbility.ToString());
                abilityItem.SubItems.Add(chainedAbility.TargetType == TargetType.Me ? "Me" : "My Current Target");
                abilityItem.SubItems.Add(chainedAbility.MustBeReady ? "Yes" : "No");

                abilityItem.Tag = chainedAbility;

                abilitiesListView.Items.Add(abilityItem);
            }
        }

        public Keys HotKey { get; set; }
        public ModifierKeys ModifierKey { get; set; }
        public List<ChainedAbility> ChainedAbilities { get; set; }

        private void AddNewAbilityChainForm_Load(object sender, EventArgs e)
        {
        }

        private void hotKeyTriggerSetKeyButton_Click(object sender, EventArgs e)
        {
            HotKey = Keys.None;

            if (_pressHotKeyNowMode)
            {
                // get the key...
                _pressHotKeyNowMode = false;
                hotKeyTriggerSetKeyButton.Text =
                    Properties.Resources.AddNewAbilityChainForm_hotKeyTriggerSetKeyButton_Click_Set_Key;
                hotKeyTriggerSetKeyButton.ForeColor = Color.Black;
            }
            else
            {
                // go into press hotkey mode...
                _pressHotKeyNowMode = true;
                hotKeyTriggerSetKeyButton.Text =
                    Properties.Resources.AddNewAbilityChainForm_hotKeyTriggerSetKeyButton_Click_Press_Key_Now;
                hotKeyTriggerSetKeyButton.ForeColor = Color.Red;

                KeyPreview = true;
                KeyUp += AddNewAbilityChainForm_KeyUp;
                KeyDown += AddNewAbilityChainForm_KeyDown;
            }
        }

        private void AddNewAbilityChainForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (!_pressHotKeyNowMode) return;
            if (_keyIsDown) return;

            _keyIsDown = true;
        }

        private void AddNewAbilityChainForm_KeyUp(object sender, KeyEventArgs e)
        {
            if (!_pressHotKeyNowMode) return;
            _keyIsDown = false;
            if (e.Modifiers != 0 && !e.Alt && !e.Control && !e.Shift)
            {
                MessageBox.Show(Properties.Resources.AddNewAbilityChainForm_AddNewAbilityChainForm_KeyUp_);
                return;
            }

            HotKey = e.KeyData;

            hotKeyTriggerSetKeyButton.Text = e.KeyData.ToString();
            hotKeyTriggerSetKeyButton.ForeColor = Color.Green;

            _pressHotKeyNowMode = false;

            KeyUp -= AddNewAbilityChainForm_KeyUp;
            KeyDown -= AddNewAbilityChainForm_KeyDown;
        }

        private void newAbilityButton_Click(object sender, EventArgs e)
        {
            var newForm = new AddNewAbilityForm();

            if (newForm.ShowDialog() != DialogResult.OK) return;
            var abilityItem = new ListViewItem(newForm.AllowedAbility.ToString());
            abilityItem.SubItems.Add(newForm.AllowedAbility.TargetType == TargetType.Me ? "Me" : "My Current Target");
            abilityItem.SubItems.Add(newForm.AllowedAbility.MustBeReady ? "Yes" : "No");

            abilityItem.Tag = newForm.AllowedAbility;

            abilitiesListView.Items.Add(abilityItem);
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(abilityChainNameTextBox.Text))
            {
                MessageBox.Show(
                    Properties.Resources
                        .AddNewAbilityChainForm_saveButton_Click_The_ability_chain_name_text_box_cannot_be_blank_,
                    Properties.Resources.AddNewAbilityChainForm_saveButton_Click_Error, MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            if (HotKey == Keys.None)
            {
                MessageBox.Show(Properties.Resources.AddNewAbilityChainForm_saveButton_Click_The_hot_key_must_be_set_,
                    Properties.Resources.AddNewAbilityChainForm_saveButton_Click_Error, MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            if (abilitiesListView.Items.Count < 1)
            {
                MessageBox.Show(
                    Properties.Resources.AddNewAbilityChainForm_saveButton_Click_There_must_be_at_least_one_ability_,
                    Properties.Resources.AddNewAbilityChainForm_saveButton_Click_Error, MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            ModifierKey = ConvertComboBoxIndexToModifierKey(modifierKeyComboBox.SelectedIndex);

            foreach (ListViewItem listViewItem in abilitiesListView.Items)
            {
                ChainedAbilities.Add(listViewItem.Tag as ChainedAbility);
            }

            DialogResult = DialogResult.OK;
        }

        private void removeSelectedAbilitiesButton_Click(object sender, EventArgs e)
        {
            if (abilitiesListView.CheckedItems.Count <= 0) return;
            var result = MessageBox.Show(string.Format("You are about to remove {0} {1}. Would you like to proceed?",
                abilitiesListView.CheckedItems.Count,
                abilitiesListView.CheckedItems.Count == 1 ? "ability" : "abilities"),
                Properties.Resources.AddNewAbilityChainForm_removeSelectedAbilitiesButton_Click_Warning,
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (result != DialogResult.Yes) return;
            foreach (ListViewItem item in abilitiesListView.CheckedItems)
            {
                item.Remove();
            }
        }

        private static int ConvertModifierKeyToComboBoxIndex(ModifierKeys key)
        {
            switch (key)
            {
                case Styx.Common.ModifierKeys.Control:
                    return 1;
                case Styx.Common.ModifierKeys.Shift:
                    return 2;
                default:
                    return 0;
            }
        }

        private static ModifierKeys ConvertComboBoxIndexToModifierKey(int index)
        {
            switch (index)
            {
                case 1:
                    return Styx.Common.ModifierKeys.Control;
                case 2:
                    return Styx.Common.ModifierKeys.Shift;
                default:
                    return Styx.Common.ModifierKeys.Alt;
            }
        }

        private void moveSelectedItemUpButton_Click(object sender, EventArgs e)
        {
            if (abilitiesListView.SelectedItems.Count <= 0) return;
            var listViewItem = abilitiesListView.SelectedItems[0];
            var index = listViewItem.Index;

            if (index <= 0) return;
            // when the item is remove, it needs to be reinserted at index - 1 position within the list.
            abilitiesListView.Items.Remove(listViewItem);
            abilitiesListView.Items.Insert(index - 1, listViewItem);

            listViewItem.Selected = true;
            abilitiesListView.Focus();
        }

        private void moveSelectedItemDownButton_Click(object sender, EventArgs e)
        {
            if (abilitiesListView.SelectedItems.Count <= 0) return;
            var listViewItem = abilitiesListView.SelectedItems[0];
            var index = listViewItem.Index;

            if (index >= abilitiesListView.Items.Count - 1) return;
            // when the item is remove, it needs to be reinserted at index + 1 position within the list.
            abilitiesListView.Items.Remove(listViewItem);
            abilitiesListView.Items.Insert(index + 1, listViewItem);

            listViewItem.Selected = true;
            abilitiesListView.Focus();
        }
    }
}