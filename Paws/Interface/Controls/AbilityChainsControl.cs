using System;
using System.Linq;
using System.Windows.Forms;
using Paws.Core;
using Paws.Core.Managers;
using Paws.Interface.Forms;
using Styx;

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

            if (newForm.ShowDialog() != DialogResult.OK) return;
            var abilityChain = new AbilityChain
            {
                Name = newForm.abilityChainNameTextBox.Text,
                Specialization = WoWSpec.DruidFeral,
                HotKey = newForm.HotKey,
                ModiferKey = newForm.ModifierKey,
                ChainedAbilities = newForm.ChainedAbilities
            };


            AddAbilityChainToListView(abilityChain);
        }

        private void removeSelectedItemsButton_Click(object sender, EventArgs e)
        {
            if (abilityChainsListView.CheckedItems.Count <= 0) return;
            var result = MessageBox.Show(string.Format("You are about to remove {0} {1}. Would you like to proceed?",
                abilityChainsListView.CheckedItems.Count,
                abilityChainsListView.CheckedItems.Count == 1 ? "ability chain" : "ability chains"),
                Properties.Resources.AbilityChainsControl_removeSelectedItemsButton_Click_Warning,
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (result != DialogResult.Yes) return;
            foreach (ListViewItem item in abilityChainsListView.CheckedItems)
            {
                item.Remove();
            }
        }

        private void AddAbilityChainToListView(AbilityChain abilityChain)
        {
            var abilitiesStr = abilityChain.ChainedAbilities.Aggregate(string.Empty,
                (current, ability) =>
                    current +
                    (abilityChain.ChainedAbilities.Last() == ability
                        ? ability.FriendlyName
                        : ability.FriendlyName + "; "));

            var lvItem = new ListViewItem(abilityChain.Name);
            lvItem.SubItems.Add(Properties.Resources.AbilityChainsControl_editItemToolStripMenuItem_Click_Feral);
            lvItem.SubItems.Add(string.Format("{0} + {1}", abilityChain.ModiferKey, abilityChain.HotKey));
            lvItem.SubItems.Add(abilitiesStr);
            lvItem.Tag = abilityChain;

            abilityChainsListView.Items.Add(lvItem);
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
            var abilityChains =
                (from ListViewItem lvItem in abilityChainsListView.Items select lvItem.Tag as AbilityChain).ToList();

            AbilityChainsManager.SaveDataSet(abilityChains);
        }

        private void removeItemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in abilityChainsListView.SelectedItems)
                item.Remove();
        }

        private void editItemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (abilityChainsListView.SelectedItems.Count <= 0) return;
            var lvItem = abilityChainsListView.SelectedItems[0];

            var editForm = new AddNewAbilityChainForm(lvItem.Tag as AbilityChain);

            if (editForm.ShowDialog() != DialogResult.OK) return;
            var abilityChain = new AbilityChain
            {
                Name = editForm.abilityChainNameTextBox.Text,
                Specialization = WoWSpec.DruidFeral,
                HotKey = editForm.HotKey,
                ModiferKey = editForm.ModifierKey,
                ChainedAbilities = editForm.ChainedAbilities
            };


            var abilitiesStr = abilityChain.ChainedAbilities.Aggregate(string.Empty,
                (current, ability) =>
                    current +
                    (abilityChain.ChainedAbilities.Last() == ability
                        ? ability.FriendlyName
                        : ability.FriendlyName + "; "));

            lvItem.Text = editForm.abilityChainNameTextBox.Text;
            lvItem.SubItems[1].Text = Properties.Resources.AbilityChainsControl_editItemToolStripMenuItem_Click_Feral;
            lvItem.SubItems[2].Text = string.Format("{0} + {1}", abilityChain.ModiferKey, abilityChain.HotKey);
            lvItem.SubItems[3].Text = abilitiesStr;
            lvItem.Tag = abilityChain;
        }
    }
}