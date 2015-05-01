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

            newForm.ShowDialog();
        }
    }
}
