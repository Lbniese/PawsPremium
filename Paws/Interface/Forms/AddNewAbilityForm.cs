using System;
using System.Windows.Forms;
using Paws.Core;
using Paws.Core.Conditions;
using Paws.Core.Managers;

namespace Paws.Interface.Forms
{
    public partial class AddNewAbilityForm : Form
    {
        public AddNewAbilityForm()
        {
            InitializeComponent();
        }

        public ChainedAbility AllowedAbility { get; private set; }

        private void AddNewAbilityForm_Load(object sender, EventArgs e)
        {
            foreach (var ability in AbilityChainsManager.GetAllowedAbilities())
            {
                abilitiesComboBox.Items.Add(ability);
            }
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            AllowedAbility = abilitiesComboBox.SelectedItem as ChainedAbility;

            if (AllowedAbility == null)
            {
                MessageBox.Show(Properties.Resources.AddNewAbilityForm_saveButton_Click_Please_select_an_Ability_,
                    Properties.Resources.AddNewAbilityForm_saveButton_Click_Warning, MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            // Dynamically create an instance of our type.
            AllowedAbility.CreateInstance();

            AllowedAbility.MustBeReady = isRequiredCheckBox.Checked;
            AllowedAbility.TargetType = targetMeRadioButton.Checked ? TargetType.Me : TargetType.MyCurrentTarget;

            DialogResult = DialogResult.OK;
        }
    }
}