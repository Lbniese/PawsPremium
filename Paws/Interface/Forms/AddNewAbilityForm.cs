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
        public AddNewAbilityForm()
        {
            InitializeComponent();
        }

        private void saveButton_Click(object sender, EventArgs e)
        {

        }

        private void AddNewAbilityForm_Load(object sender, EventArgs e)
        {
            foreach (var ability in AbilityChainsManager.GetAllowedAbilities())
            {
                // this.conditionsComboBox.Items.Add(ability);
            }
        }
    }
}
