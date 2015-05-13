using Paws.Core;
using Paws.Core.Managers;
using Paws.Interface.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Paws.Interface.Controls
{
    public partial class AbilityChainsControl : UserControl
    {
        public AbilityChainsControl()
        {
            InitializeComponent();

            ReadData();
        }

        private void AbilityChainsControl_Load(object sender, EventArgs e)
        {
            
        }

        private void abilityChainsAddNewAbilityChainButton_Click(object sender, EventArgs e)
        {
            var newForm = new AddNewAbilityChainForm(null);

            if (newForm.ShowDialog() == DialogResult.OK)
            {
                AbilityChain abilityChain = new AbilityChain();

                abilityChain.Name = newForm.abilityChainNameTextBox.Text;
                abilityChain.Specialization = Styx.WoWSpec.DruidFeral;
                abilityChain.HotKey = newForm.HotKey;
                abilityChain.ModiferKey = newForm.ModifierKey;
                abilityChain.ChainedAbilities = newForm.ChainedAbilities;

                AddAbilityChainToListView(abilityChain);
            }
        }

        private void removeSelectedItemsButton_Click(object sender, EventArgs e)
        {
            if (this.abilityChainsListView.CheckedItems.Count > 0)
            {
                var result = MessageBox.Show(string.Format("You are about to remove {0} {1}. Would you like to proceed?",
                    this.abilityChainsListView.CheckedItems.Count, this.abilityChainsListView.CheckedItems.Count == 1 ? "ability chain" : "ability chains"),
                    "Warning",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    foreach (ListViewItem item in this.abilityChainsListView.CheckedItems)
                    {
                        item.Remove();
                    }
                }
            }
        }

        private void AddAbilityChainToListView(AbilityChain abilityChain)
        {
            string abilitiesStr = string.Empty;
            foreach (var ability in abilityChain.ChainedAbilities)
            {
                abilitiesStr += abilityChain.ChainedAbilities.Last() == ability ? ability.FriendlyName : ability.FriendlyName + "; ";
            }

            ListViewItem lvItem = new ListViewItem(abilityChain.Name);
            lvItem.SubItems.Add("Feral");
            lvItem.SubItems.Add(string.Format("{0} + {1}", abilityChain.ModiferKey, abilityChain.HotKey));
            lvItem.SubItems.Add(abilitiesStr);
            lvItem.Tag = abilityChain;

            this.abilityChainsListView.Items.Add(lvItem);
        }

        public void ReadData()
        {
            foreach (var abilityChain in AbilityChainsManager.Instance.AbilityChains)
            {
                AddAbilityChainToListView(abilityChain);
            }
        }

        public void SaveData()
        {
            var abilityChains = new List<AbilityChain>();

            foreach (ListViewItem lvItem in this.abilityChainsListView.Items)
            {
                abilityChains.Add(lvItem.Tag as AbilityChain);
            }

            AbilityChainsManager.SaveDataSet(abilityChains);
        }

        private void removeItemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in this.abilityChainsListView.SelectedItems)
                item.Remove();
        }

        private void editItemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.abilityChainsListView.SelectedItems.Count > 0)
            {
                ListViewItem lvItem = this.abilityChainsListView.SelectedItems[0];

                var editForm = new AddNewAbilityChainForm(lvItem.Tag as AbilityChain);

                if (editForm.ShowDialog() == DialogResult.OK)
                {
                    AbilityChain abilityChain = new AbilityChain();

                    abilityChain.Name = editForm.abilityChainNameTextBox.Text;
                    abilityChain.Specialization = Styx.WoWSpec.DruidFeral;
                    abilityChain.HotKey = editForm.HotKey;
                    abilityChain.ModiferKey = editForm.ModifierKey;
                    abilityChain.ChainedAbilities = editForm.ChainedAbilities;

                    string abilitiesStr = string.Empty;
                    foreach (var ability in abilityChain.ChainedAbilities)
                    {
                        abilitiesStr += abilityChain.ChainedAbilities.Last() == ability ? ability.FriendlyName : ability.FriendlyName + "; ";
                    }

                    lvItem.Text = editForm.abilityChainNameTextBox.Text;
                    lvItem.SubItems[1].Text = "Feral";
                    lvItem.SubItems[2].Text = string.Format("{0} + {1}", abilityChain.ModiferKey, abilityChain.HotKey);
                    lvItem.SubItems[3].Text = abilitiesStr;
                    lvItem.Tag = abilityChain;
                }
            }
        }
    }
}
