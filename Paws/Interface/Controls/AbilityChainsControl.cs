using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Paws.Interface.Forms;
using Paws.Core;

namespace Paws.Interface.Controls
{
    public partial class AbilityChainsControl : UserControl
    {
        public AbilityChainsControl()
        {
            InitializeComponent();
        }

        private void abilityChainsAddNewAbilityChainButton_Click(object sender, EventArgs e)
        {
            var newForm = new AddNewAbilityChainForm();

            if (newForm.ShowDialog() == DialogResult.OK)
            {
                // HotkeysManager.Register("Burst", Keys.F1, ModifierKeys.Alt, KeyIsPressed);

                AbilityChain abilityChain = new AbilityChain();

                abilityChain.Name = newForm.abilityChainNameTextBox.Text;
                abilityChain.HotKey = newForm.HotKey;
                abilityChain.ModiferKey = newForm.ModifierKey;

                ListViewItem lvItem = new ListViewItem(abilityChain.Name);
                lvItem.SubItems.Add("Enabled");
                lvItem.SubItems.Add(string.Format("{0} + {1}", abilityChain.ModiferKey, abilityChain.HotKey));
                lvItem.SubItems.Add("Not Set");

                this.abilityChainsListView.Items.Add(lvItem);
            }
        }
    }
}
