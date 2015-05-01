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
    public partial class AddNewAbilityChainForm : Form
    {
        private bool _pressHotKeyNowMode = false;

        public AddNewAbilityChainForm()
        {
            InitializeComponent();
        }

        private void hotKeyTriggerSetKeyButton_Click(object sender, EventArgs e)
        {
            if (_pressHotKeyNowMode)
            {
                // get the key...
                _pressHotKeyNowMode = false;
                this.hotKeyTriggerSetKeyButton.Text = "Set Key";
                this.hotKeyTriggerSetKeyButton.ForeColor = Color.Black;
            }
            else
            {
                // go into press hotkey mode...
                _pressHotKeyNowMode = true;
                this.hotKeyTriggerSetKeyButton.Text = "Press Key Now";
                this.hotKeyTriggerSetKeyButton.ForeColor = Color.Red;

                this.KeyPreview = true;
                this.KeyUp += AddNewAbilityChainForm_KeyUp;
            }
        }

        void AddNewAbilityChainForm_KeyUp(object sender, KeyEventArgs e)
        {
            if (_pressHotKeyNowMode)
            {
                if (e.Modifiers != 0 && !e.Alt && !e.Control && !e.Shift)
                {
                    MessageBox.Show("Do not press any modifier keys such as Control, Shift, and Alt.\nUse the Checkboxes to assign those keys");
                    return;
                }

                this.hotKeyTriggerSetKeyButton.Text = e.KeyData.ToString();
                this.hotKeyTriggerSetKeyButton.ForeColor = Color.Green;
                
                _pressHotKeyNowMode = false;

                this.KeyUp -= AddNewAbilityChainForm_KeyUp;
            }
        }
    }
}
