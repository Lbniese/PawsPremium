using Styx.Common;
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
        public Keys HotKey { get; set; }
        public ModifierKeys ModifierKey { get; set; }

        private bool _pressHotKeyNowMode = false;
        bool _keyIsDown = false;

        public AddNewAbilityChainForm()
        {
            InitializeComponent();

            this.HotKey = Keys.None;
        }

        private void AddNewAbilityChainForm_Load(object sender, EventArgs e)
        {
            this.modifierKeyComboBox.SelectedIndex = 0;
        }

        private void hotKeyTriggerSetKeyButton_Click(object sender, EventArgs e)
        {
            this.HotKey = Keys.None;

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
                this.KeyDown += AddNewAbilityChainForm_KeyDown;
            }
        }

        private void AddNewAbilityChainForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (_pressHotKeyNowMode)
            {
                if (_keyIsDown) return;

                _keyIsDown = true;
            }
        }

        private void AddNewAbilityChainForm_KeyUp(object sender, KeyEventArgs e)
        {
            if (_pressHotKeyNowMode)
            {
                _keyIsDown = false;
                if (e.Modifiers != 0 && !e.Alt && !e.Control && !e.Shift)
                {
                    MessageBox.Show("Do not press any modifier keys such as Control, Shift, and Alt.\nUse the Checkboxes to assign those keys");
                    return;
                }

                this.HotKey = e.KeyData;

                this.hotKeyTriggerSetKeyButton.Text = e.KeyData.ToString();
                this.hotKeyTriggerSetKeyButton.ForeColor = Color.Green;
                
                _pressHotKeyNowMode = false;

                this.KeyUp -= AddNewAbilityChainForm_KeyUp;
                this.KeyDown -= AddNewAbilityChainForm_KeyDown;
            }
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.abilityChainNameTextBox.Text))
            {
                MessageBox.Show("The ability chain name text box cannot be blank.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (this.HotKey == Keys.None)
            {
                MessageBox.Show("The hot key must be set.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            this.ModifierKey = ConvertComboBoxIndexToModifierKey(this.modifierKeyComboBox.SelectedIndex);

            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private int ConvertModifierKeyToComboBoxIndex(ModifierKeys key)
        {
            switch (key)
            {
                case Styx.Common.ModifierKeys.Control:
                    return 1;
                case Styx.Common.ModifierKeys.Shift:
                    return 2;
                case Styx.Common.ModifierKeys.Alt:
                default:
                    return 0;
            }
        }

        private ModifierKeys ConvertComboBoxIndexToModifierKey(int index)
        {
            switch (index)
            {
                case 1:
                    return Styx.Common.ModifierKeys.Control;
                case 2:
                    return Styx.Common.ModifierKeys.Shift;
                case 0:
                default:
                    return Styx.Common.ModifierKeys.Alt;
            }
        }
    }
}
