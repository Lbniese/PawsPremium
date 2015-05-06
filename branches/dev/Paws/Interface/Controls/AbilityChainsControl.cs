using Paws.Core;
using Paws.Core.Managers;
using Paws.Interface.Forms;
using System;
using System.Linq;
using System.Windows.Forms;

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
                AbilityChain abilityChain = new AbilityChain();

                abilityChain.Name = newForm.abilityChainNameTextBox.Text;
                abilityChain.Specialization = Styx.WoWSpec.DruidFeral;
                abilityChain.HotKey = newForm.HotKey;
                abilityChain.ModiferKey = newForm.ModifierKey;
                abilityChain.ChainedAbilities = newForm.ChainedAbilities;

                string abilitiesStr = string.Empty;
                foreach (var ability in abilityChain.ChainedAbilities)
                {
                    abilitiesStr += abilityChain.ChainedAbilities.Last() == ability ? ability.FriendlyName : ability.FriendlyName + "; ";
                }

                ListViewItem lvItem = new ListViewItem(abilityChain.Name);
                lvItem.SubItems.Add("Feral");
                lvItem.SubItems.Add(string.Format("{0} + {1}", abilityChain.ModiferKey, abilityChain.HotKey));
                lvItem.SubItems.Add(abilitiesStr);

                this.abilityChainsListView.Items.Add(lvItem);

                AbilityChainsManager.Instance.RegisterAbilityChain(abilityChain);
            }
        }
    }
}
