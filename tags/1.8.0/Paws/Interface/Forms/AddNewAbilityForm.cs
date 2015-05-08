using Paws.Core;
using Paws.Core.Conditions;
using Paws.Core.Managers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Paws.Interface.Forms
{
    public partial class AddNewAbilityForm : Form
    {
        public ChainedAbility AllowedAbility { get; private set; }

        public AddNewAbilityForm()
        {
            InitializeComponent();
        }

        private void AddNewAbilityForm_Load(object sender, EventArgs e)
        {
            foreach (var ability in AbilityChainsManager.GetAllowedAbilities())
            {
                this.abilitiesComboBox.Items.Add(ability);
            }
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            this.AllowedAbility = this.abilitiesComboBox.SelectedItem as ChainedAbility;

            if (this.AllowedAbility == null)
            {
                MessageBox.Show("Please select an Ability.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Dynamically create an instance of our type.
            this.AllowedAbility.CreateInstance();

            this.AllowedAbility.MustBeReady = this.isRequiredCheckBox.Checked;
            this.AllowedAbility.TargetType = this.targetMeRadioButton.Checked ? TargetType.Me : TargetType.MyCurrentTarget;

            this.DialogResult = DialogResult.OK;
        }
    }
}
