using Paws.Core;
using Paws.Core.Managers;
using Paws.Interface.Controls;
using Paws.Interface.Controls.Feral;
using Paws.Interface.Controls.Guardian;
using Paws.Interface.Forms;
using Styx;
using Styx.Helpers;
using Styx.WoWInternals.WoWObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;


namespace Paws.Interface
{
    public partial class SettingsForm : Form
    {
        public WoWSpec SettingsMode { get; set; }

        private static LocalPlayer Me { get { return StyxWoW.Me; } }

        #region Load Events

        public SettingsForm()
        {
            this.SettingsMode = Me.Specialization;

            InitializeComponent();
            InitSettingsMode();
        }

        private void Settings_Load(object sender, EventArgs e)
        {
            LoadIntroduction();
            SetVersionInformation();
            PopulateProfiles();
            PopulateItemsListView();
        }

        #endregion

        #region Template

        /// <summary>
        /// Custom form drag and close handler.
        /// </summary>
        Point FormHeaderCursorPosition;
        private void FormHeader_MouseDown(object sender, MouseEventArgs e) { FormHeaderCursorPosition = new Point(-e.X, -e.Y); }
        private void FormHeader_MouseMove(object sender, MouseEventArgs e) { if (e.Button == MouseButtons.Left) { Point mousePos = Control.MousePosition; mousePos.Offset(FormHeaderCursorPosition.X, FormHeaderCursorPosition.Y); Location = mousePos; } }
        private void FormHeaderClose_Click(object sender, EventArgs e) { this.Dispose(); }

        #endregion

        #region Helper Functions

        /// <summary>
        /// Initializes the specialization mode in which the UI should display settings to the user.
        /// </summary>
        private void InitSettingsMode()
        {
            this.mobilityTab.Controls.Clear();
            this.offensiveTab.Controls.Clear();
            this.defensiveTab.Controls.Clear();
            this.healingTab.Controls.Clear();

            switch (this.SettingsMode)
            {
                case WoWSpec.DruidGuardian:
                    {
                        this.specializationButton.Text = "Feral";
                        this.mobilityTab.Controls.Add(new GuardianMobilitySettings(this));
                        this.offensiveTab.Controls.Add(new GuardianOffensiveSettings(this));
                        this.defensiveTab.Controls.Add(new GuardianDefensiveSettings(this));
                        this.healingTab.Controls.Add(new GuardianHealingSettings(this));
                        break;
                    }
                default:
                    {
                        this.specializationButton.Text = "Guardian";
                        this.mobilityTab.Controls.Add(new FeralMobilitySettings(this));
                        this.offensiveTab.Controls.Add(new FeralOffensiveSettings(this));
                        this.defensiveTab.Controls.Add(new FeralDefensiveSettings(this));
                        this.healingTab.Controls.Add(new FeralHealingSettings(this));
                        break;
                    }
            }
        }

        /// <summary>
        /// Loads the Rtf file into the introduction Rich text control.
        /// </summary>
        private void LoadIntroduction()
        {
            // This method has been changed so that we are no longer loading from file and instead loading from embedded resource
            // to comply with store requirements.

            this.rtfAbout.Rtf = Resources.release.release_notes;
        }

        /// <summary>
        /// Sets the Version information label.
        /// </summary>
        private void SetVersionInformation()
        {
            this.versionLabel.Text = string.Format("Version {0}", Main.Version);
        }

        /// <summary>
        /// Adds a list view item to the items list view. 
        /// </summary>
        private void AddItemToItemList(PawsItem item)
        {
            ListViewItem lvItem = new ListViewItem(item.Name);

            lvItem.UseItemStyleForSubItems = false;
            lvItem.SubItems.Add(item.Enabled ? "Enabled" : "Disabled", Color.White, item.Enabled ? Color.DarkGreen : Color.DarkRed, new System.Drawing.Font("Arial", 9.0f, FontStyle.Bold));
            lvItem.SubItems.Add(item.MyState.ToString());
            lvItem.SubItems.Add(item.GetConditionsDescription());
            lvItem.Tag = item;

            this.itemsListView.Items.Add(lvItem);
        }

        /// <summary>
        /// Binds the current settings instance to the UI controls.
        /// </summary>
        private void BindUISettings()
        {
            // Form Title //
            this.FormHeader.Text = string.Format("Paws [{0}] Settings: {1}", this.SettingsMode.ToString().Replace("Druid", string.Empty), this.aboutProfilesPesetsComboBox.SelectedItem);

            // General Tab //
            this.generalMarkOfTheWildEnabledCheckBox.Checked = SettingsManager.Instance.MarkOfTheWildEnabled;
            generalMarkOfTheWildEnabledCheckBox_CheckedChanged(this.generalMarkOfTheWildEnabledCheckBox, EventArgs.Empty);
            this.generalMarkOfTheWildDoNotApplyStealthedCheckBox.Checked = SettingsManager.Instance.MarkOfTheWildDoNotApplyIfStealthed;
            this.generalSootheEnabledCheckBox.Checked = SettingsManager.Instance.SootheEnabled;
            generalSootheEnabledCheckBox_CheckedChanged(generalSootheEnabledCheckBox, EventArgs.Empty);
            this.generalSootheReactionTimeTextBox.Text = SettingsManager.Instance.SootheReactionTimeInMs.ToString();
            this.generalTargetHeightCheckBox.Checked = SettingsManager.Instance.TargetHeightEnabled;
            generalTargetHeightCheckBox_CheckedChanged(this.generalTargetHeightCheckBox, EventArgs.Empty);
            this.generalTargetHeightTextBox.Text = SettingsManager.Instance.TargetHeightMinDistance.ToString("0.##");
            this.generalReleaseSpiritOnDeathEnabledCheckBox.Checked = SettingsManager.Instance.ReleaseSpiritOnDeathEnabled;
            generalReleaseSpiritOnDeathEnabledCheckBox_CheckedChanged(this.generalReleaseSpiritOnDeathEnabledCheckBox, EventArgs.Empty);
            this.generalReleaseSpiritOnDeathTimerTextBox.Text = SettingsManager.Instance.ReleaseSpiritOnDeathIntervalInMs.ToString();
            this.generalInterruptTimingMinMSTextBox.Text = SettingsManager.Instance.InterruptMinMilliseconds.ToString();
            this.generalInterruptTimingMaxMSTextBox.Text = SettingsManager.Instance.InterruptMaxMilliseconds.ToString();
            this.generalInterruptTimingSuccessRateTextBox.Text = SettingsManager.Instance.InterruptSuccessRate.ToString("0.##");
            this.mobilityGeneralMovementCheckBox.Checked = SettingsManager.Instance.AllowMovement;
            this.mobilityGeneralTargetFacingCheckBox.Checked = SettingsManager.Instance.AllowTargetFacing;

            // Mobility Tab //
            BindUISettingsToControlCollection(this.mobilityTab.Controls);

            // Offesnive Tab //
            BindUISettingsToControlCollection(this.offensiveTab.Controls);
            
            // Defensive Tab //
            BindUISettingsToControlCollection(this.defensiveTab.Controls);

            // Healing Tab //
            BindUISettingsToControlCollection(this.healingTab.Controls);
            
            // Trinkets/Racials Tab //
            this.miscTrinket1EnabledCheckBox.Checked = SettingsManager.Instance.Trinket1Enabled;
            miscTrinket1EnabledCheckBox_CheckedChanged(this.miscTrinket1EnabledCheckBox, EventArgs.Empty);
            this.miscTrinket1UseOnMe.Checked = SettingsManager.Instance.Trinket1UseOnMe;
            this.miscTrinket1UseOnEnemy.Checked = SettingsManager.Instance.Trinket1UseOnEnemy;
            this.miscTrinket1OnCooldownRadioButton.Checked = SettingsManager.Instance.Trinket1OnCoolDown;
            this.miscTrinket1LossOfControlRadioButton.Checked = SettingsManager.Instance.Trinket1LossOfControl;
            this.miscTrinket1TheseConditionsRadioButton.Checked = SettingsManager.Instance.Trinket1AdditionalConditions;
            this.miscTrinket1HealthMinCheckBox.Checked = SettingsManager.Instance.Trinket1HealthMinEnabled;
            this.miscTrinket1HealthMinTextBox.Text = SettingsManager.Instance.Trinket1HealthMin.ToString("0.##");
            this.miscTrinket1ManaMinCheckBox.Checked = SettingsManager.Instance.Trinket1ManaMinEnabled;
            this.miscTrinket1ManaMinTextBox.Text = SettingsManager.Instance.Trinket1ManaMin.ToString("0.##");
            this.miscTrinket2EnabledCheckBox.Checked = SettingsManager.Instance.Trinket2Enabled;
            miscTrinket2EnabledCheckBox_CheckedChanged(this.miscTrinket2EnabledCheckBox, EventArgs.Empty);
            this.miscTrinket2UseOnMe.Checked = SettingsManager.Instance.Trinket2UseOnMe;
            this.miscTrinket2UseOnEnemy.Checked = SettingsManager.Instance.Trinket2UseOnEnemy;
            this.miscTrinket2OnCooldownRadioButton.Checked = SettingsManager.Instance.Trinket2OnCoolDown;
            this.miscTrinket2LossOfControlRadioButton.Checked = SettingsManager.Instance.Trinket2LossOfControl;
            this.miscTrinket2TheseConditionsRadioButton.Checked = SettingsManager.Instance.Trinket2AdditionalConditions;
            this.miscTrinket2HealthMinCheckBox.Checked = SettingsManager.Instance.Trinket2HealthMinEnabled;
            this.miscTrinket2HealthMinTextBox.Text = SettingsManager.Instance.Trinket2HealthMin.ToString("0.##");
            this.miscTrinket2ManaMinCheckBox.Checked = SettingsManager.Instance.Trinket2ManaMinEnabled;
            this.miscTrinket2ManaMinTextBox.Text = SettingsManager.Instance.Trinket2ManaMin.ToString("0.##");

            this.miscTrinket1EnabledCheckBox.Text = Me.Inventory.Equipped.Trinket1 != null
                ? string.Format("Trinket: ({0})", Me.Inventory.Equipped.Trinket1.SafeName)
                : "Trinket #1 (Not Equipped)";

            this.miscTrinket2EnabledCheckBox.Text = Me.Inventory.Equipped.Trinket2 != null
                ? string.Format("Trinket: ({0})", Me.Inventory.Equipped.Trinket2.SafeName)
                : "Trinket #2 (Not Equipped)";

            // Trinkets/Racials Tab //
            this.miscRacialAbilityTaurenWarStompCheckBox.Checked = SettingsManager.Instance.TaurenWarStompEnabled;
            miscRacialAbilityTaurenWarStompCheckBox_CheckedChanged(this.miscRacialAbilityTaurenWarStompCheckBox, EventArgs.Empty);
            this.miscRacialAbilityTaurenWarStompMinEnemies.Text = SettingsManager.Instance.TaurenWarStompMinEnemies.ToString();
            this.miscRacialAbilityTrollBerserkingCheckBox.Checked = SettingsManager.Instance.TrollBerserkingEnabled;
            miscRacialAbilityTrollBerserkingCheckBox_CheckedChanged(this.miscRacialAbilityTrollBerserkingCheckBox, EventArgs.Empty);
            this.miscRacialAbilityTrollBerserkingOnCooldownRadioButton.Checked = SettingsManager.Instance.TrollBerserkingOnCooldown;
            this.miscRacialAbilityTrollBerserkingEnemyHealthRadioButton.Checked = SettingsManager.Instance.TrollBerserkingEnemyHealthCheck;
            this.miscRacialAbilityTrollBerserkingEnemyHealthMultiplierTextBox.Text = SettingsManager.Instance.TrollBerserkingEnemyHealthMultiplier.ToString("0.##");
            this.miscRacialAbilityTrollBerserkingSurroundedByEnemiesCheckBox.Checked = SettingsManager.Instance.TrollBerserkingSurroundedByEnemiesEnabled;
            this.miscRacialAbilityTrollBerserkingSurroundedByEnemiesTextBox.Text = SettingsManager.Instance.TrollBerserkingSurroundedByMinEnemies.ToString();
        }

        /// <summary>
        /// Bind settings to the specified set of controls.
        /// </summary>
        private void BindUISettingsToControlCollection(Control.ControlCollection controls)
        {
            foreach (var control in controls)
            {
                var settingsControl = control as ISettingsControl;
                if (settingsControl != null)
                {
                    settingsControl.BindUISettings();
                    return;
                }
            }
        }

        /// <summary>
        /// Applies all of the settings to the current settings instance. Does not save to file.
        /// </summary>
        private void ApplySettings()
        {
            // General Tab //
            SettingsManager.Instance.MarkOfTheWildEnabled = this.generalMarkOfTheWildEnabledCheckBox.Checked;
            SettingsManager.Instance.MarkOfTheWildDoNotApplyIfStealthed = this.generalMarkOfTheWildDoNotApplyStealthedCheckBox.Checked;
            SettingsManager.Instance.SootheEnabled = this.generalSootheEnabledCheckBox.Checked;
            SettingsManager.Instance.SootheReactionTimeInMs = Convert.ToInt32(this.generalSootheReactionTimeTextBox.Text);
            SettingsManager.Instance.TargetHeightEnabled = this.generalTargetHeightCheckBox.Checked;
            SettingsManager.Instance.TargetHeightMinDistance = this.generalTargetHeightTextBox.Text.ToFloat();
            SettingsManager.Instance.ReleaseSpiritOnDeathEnabled = this.generalReleaseSpiritOnDeathEnabledCheckBox.Checked;
            SettingsManager.Instance.ReleaseSpiritOnDeathIntervalInMs = Convert.ToInt32(this.generalReleaseSpiritOnDeathTimerTextBox.Text);
            SettingsManager.Instance.InterruptMinMilliseconds = Convert.ToInt32(this.generalInterruptTimingMinMSTextBox.Text);
            SettingsManager.Instance.InterruptMaxMilliseconds = Convert.ToInt32(this.generalInterruptTimingMaxMSTextBox.Text);
            SettingsManager.Instance.InterruptSuccessRate = Convert.ToDouble(this.generalInterruptTimingSuccessRateTextBox.Text);
            SettingsManager.Instance.AllowMovement = this.mobilityGeneralMovementCheckBox.Checked;
            SettingsManager.Instance.AllowTargetFacing = this.mobilityGeneralTargetFacingCheckBox.Checked;

            // Mobility Tab //
            ApplySettingsFromControlCollection(this.mobilityTab.Controls);

            // Offensive Tab //
            ApplySettingsFromControlCollection(this.offensiveTab.Controls);

            // Defensive Tab //
            ApplySettingsFromControlCollection(this.defensiveTab.Controls);

            // Healing Tab //
            ApplySettingsFromControlCollection(this.healingTab.Controls);
            
            // Trinket/Racials Tab //
            SettingsManager.Instance.Trinket1Enabled = this.miscTrinket1EnabledCheckBox.Checked;
            SettingsManager.Instance.Trinket1UseOnMe = this.miscTrinket1UseOnMe.Checked;
            SettingsManager.Instance.Trinket1UseOnEnemy = this.miscTrinket1UseOnEnemy.Checked;
            SettingsManager.Instance.Trinket1OnCoolDown = this.miscTrinket1OnCooldownRadioButton.Checked;
            SettingsManager.Instance.Trinket1LossOfControl = this.miscTrinket1LossOfControlRadioButton.Checked;
            SettingsManager.Instance.Trinket1AdditionalConditions = this.miscTrinket1TheseConditionsRadioButton.Checked;
            SettingsManager.Instance.Trinket1HealthMinEnabled = miscTrinket1HealthMinCheckBox.Checked;
            SettingsManager.Instance.Trinket1HealthMin = Convert.ToDouble(miscTrinket1HealthMinTextBox.Text);
            SettingsManager.Instance.Trinket1ManaMinEnabled = miscTrinket1ManaMinCheckBox.Checked;
            SettingsManager.Instance.Trinket1ManaMin = Convert.ToDouble(miscTrinket1ManaMinTextBox.Text);
            SettingsManager.Instance.Trinket2Enabled = this.miscTrinket2EnabledCheckBox.Checked;
            SettingsManager.Instance.Trinket2UseOnMe = this.miscTrinket2UseOnMe.Checked;
            SettingsManager.Instance.Trinket2UseOnEnemy = this.miscTrinket2UseOnEnemy.Checked;
            SettingsManager.Instance.Trinket2OnCoolDown = this.miscTrinket2OnCooldownRadioButton.Checked;
            SettingsManager.Instance.Trinket2LossOfControl = this.miscTrinket2LossOfControlRadioButton.Checked;
            SettingsManager.Instance.Trinket2AdditionalConditions = this.miscTrinket2TheseConditionsRadioButton.Checked;
            SettingsManager.Instance.Trinket2HealthMinEnabled = miscTrinket2HealthMinCheckBox.Checked;
            SettingsManager.Instance.Trinket2HealthMin = Convert.ToDouble(miscTrinket2HealthMinTextBox.Text);
            SettingsManager.Instance.Trinket2ManaMinEnabled = miscTrinket2ManaMinCheckBox.Checked;
            SettingsManager.Instance.Trinket2ManaMin = Convert.ToDouble(miscTrinket2ManaMinTextBox.Text);
            SettingsManager.Instance.TaurenWarStompEnabled = miscRacialAbilityTaurenWarStompCheckBox.Checked;
            SettingsManager.Instance.TaurenWarStompMinEnemies = Convert.ToInt32(miscRacialAbilityTaurenWarStompMinEnemies.Text);
            SettingsManager.Instance.TrollBerserkingEnabled = this.miscRacialAbilityTrollBerserkingCheckBox.Checked;
            SettingsManager.Instance.TrollBerserkingOnCooldown = this.miscRacialAbilityTrollBerserkingOnCooldownRadioButton.Checked;
            SettingsManager.Instance.TrollBerserkingEnemyHealthCheck = this.miscRacialAbilityTrollBerserkingEnemyHealthRadioButton.Checked;
            SettingsManager.Instance.TrollBerserkingEnemyHealthMultiplier = this.miscRacialAbilityTrollBerserkingEnemyHealthMultiplierTextBox.Text.ToFloat();
            SettingsManager.Instance.TrollBerserkingSurroundedByEnemiesEnabled = this.miscRacialAbilityTrollBerserkingSurroundedByEnemiesCheckBox.Checked;
            SettingsManager.Instance.TrollBerserkingSurroundedByMinEnemies = Convert.ToInt32(this.miscRacialAbilityTrollBerserkingSurroundedByEnemiesTextBox.Text);
        }

        /// <summary>
        /// Applies settings from the specified set of controls.
        /// </summary>
        private void ApplySettingsFromControlCollection(Control.ControlCollection controls)
        {
            foreach (var control in controls)
            {
                var settingsControl = control as ISettingsControl;
                if (settingsControl != null)
                {
                    settingsControl.ApplySettings();
                    return;
                }
            }
        }

        /// <summary>
        /// Retrieves the available profiles for the current character, and populates the profiles dropdown control.
        /// </summary>
        private void PopulateProfiles()
        {
            this.aboutProfilesPesetsComboBox.Items.Clear();

            foreach (var file in GlobalSettingsManager.GetCharacterProfileFiles())
            {
                this.aboutProfilesPesetsComboBox.Items.Add(Path.GetFileNameWithoutExtension(file));
            }

            this.aboutProfilesPesetsComboBox.SelectedItem = GlobalSettingsManager.Instance.LastUsedProfile;
        }

        /// <summary>
        /// Retrieves the available items from the item manager and populates the item list view control.
        /// </summary>
        private void PopulateItemsListView()
        {
            foreach (var item in ItemManager.Items)
            {
                AddItemToItemList(item);
            }
        }

        /// <summary>
        /// Gathers the list of items from the items list view and tells the item manager to save the items to file.
        /// </summary>
        private void SaveItemsData()
        {
            List<PawsItem> items = new List<PawsItem>();

            foreach (ListViewItem lvItem in this.itemsListView.Items)
            {
                items.Add(lvItem.Tag as PawsItem);
            }

            ItemManager.SaveDataSet(items);
        }

        #endregion

        #region UI Events: Profiles

        private void aboutProfilesPesetsComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            string pathToProfileFile = GlobalSettingsManager.GetFullPathToProfile(this.aboutProfilesPesetsComboBox.SelectedItem.ToString());

            SettingsManager.InitWithProfile(pathToProfileFile);

            BindUISettings();
        }

        private void aboutProfilesSaveAsButton_Click(object sender, EventArgs e)
        {
            var newForm = new SaveProfileAsNameForm();

            if (newForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                ApplySettings();

                SettingsManager.Instance.SaveToFile(GlobalSettingsManager.GetFullPathToProfile(newForm.NameTextBox.Text));
                GlobalSettingsManager.Instance.LastUsedProfile = newForm.NameTextBox.Text;
                GlobalSettingsManager.Instance.Save();

                PopulateProfiles();
            }
        }

        #endregion

        #region UI Events: Items

        private void itemsAddNewItemButton_Click(object sender, EventArgs e)
        {
            var newForm = new AddNewItemForm();

            if (newForm.ShowDialog() == DialogResult.OK)
            {
                AddItemToItemList(newForm.PawsItem);
            }
        }

        private void itemsEnableCheckedItemsButton_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem lvItem in this.itemsListView.CheckedItems)
            {
                var item = lvItem.Tag as PawsItem;

                item.Enabled = true;
                lvItem.SubItems[1].Text = "Enabled";
                lvItem.SubItems[1].BackColor = Color.DarkGreen;

                lvItem.Tag = item;
            }
        }

        private void itemsDisableCheckedItemsButton_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem lvItem in this.itemsListView.CheckedItems)
            {
                var item = lvItem.Tag as PawsItem;

                item.Enabled = false;
                lvItem.SubItems[1].Text = "Disabled";
                lvItem.SubItems[1].BackColor = Color.DarkRed;

                lvItem.Tag = item;
            }
        }

        private void itemsRemoveSelectedItemsButton_Click(object sender, EventArgs e)
        {
            if (this.itemsListView.CheckedItems.Count > 0)
            {
                var result = MessageBox.Show(string.Format("You are about to remove {0} {1}. Would you like to proceed?",
                    this.itemsListView.CheckedItems.Count, this.itemsListView.CheckedItems.Count == 1 ? "item" : "items"),
                    "Warning",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    foreach (ListViewItem item in this.itemsListView.CheckedItems)
                    {
                        item.Remove();
                    }
                }
            }
        }

        private void itemsListViewContextMenu_Opening(object sender, CancelEventArgs e)
        {
            if (this.itemsListView.SelectedItems.Count == 0)
            {
                e.Cancel = true;
                return;
            }

            ListViewItem selectedItem = this.itemsListView.SelectedItems[0];

            enabledToolStripMenuItem.Checked = (selectedItem.Tag as PawsItem).Enabled;
        }

        private void editItemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.itemsListView.SelectedItems.Count > 0)
            {
                ListViewItem lvItem = this.itemsListView.SelectedItems[0];

                var editForm = new AddNewItemForm(lvItem.Tag as PawsItem);

                if (editForm.ShowDialog() == DialogResult.OK)
                {
                    lvItem.SubItems[1].Text = editForm.PawsItem.Enabled ? "Enabled" : "Disabled";
                    lvItem.SubItems[1].BackColor = editForm.PawsItem.Enabled ? Color.DarkGreen : Color.DarkRed;

                    lvItem.SubItems[2].Text = editForm.PawsItem.MyState.ToString();
                    lvItem.SubItems[3].Text = editForm.PawsItem.GetConditionsDescription();

                    lvItem.Tag = editForm.PawsItem;
                }
            }
        }

        private void removeItemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in this.itemsListView.SelectedItems)
                item.Remove();
        }

        private void enabledToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in this.itemsListView.SelectedItems)
            {
                var pawsItem = (item.Tag as PawsItem);
                pawsItem.Enabled = !pawsItem.Enabled;

                item.SubItems[1].Text = pawsItem.Enabled ? "Enabled" : "Disabled";
                item.SubItems[1].BackColor = pawsItem.Enabled ? Color.DarkGreen : Color.DarkRed;
            }
        }

        #endregion

        #region UI Events: Control Toggles

        public void InterfaceElementColorToggle(object control)
        {
            if (control is CheckBox && (control as CheckBox).Parent != null)
            {
                var checkBoxControl = control as CheckBox;
                checkBoxControl.Parent.ForeColor = checkBoxControl.Checked ? Color.White : Color.Black;
                checkBoxControl.Parent.BackColor = checkBoxControl.Checked ? Color.DarkGreen : Color.LightGray;
            }
        }

        private void generalMarkOfTheWildEnabledCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            this.generalMarkOfTheWildPanel.Enabled = (sender as CheckBox).Checked;
            InterfaceElementColorToggle(sender);
        }

        private void generalSootheEnabledCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            this.generalSoothePanel.Enabled = (sender as CheckBox).Checked;
            InterfaceElementColorToggle(sender);
        }

        private void generalAutoTargetingEnabledCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            InterfaceElementColorToggle(sender);
        }

        private void miscTrinket1EnabledCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            this.miscTrinket1Panel.Enabled = (sender as CheckBox).Checked;
            InterfaceElementColorToggle(sender);
        }

        private void miscTrinket2EnabledCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            this.miscTrinket2Panel.Enabled = (sender as CheckBox).Checked;
            InterfaceElementColorToggle(sender);
        }

        private void miscRacialAbilityTaurenWarStompCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            this.miscRacialAbilityTaurenWarStompPanel.Enabled = (sender as CheckBox).Checked;
            InterfaceElementColorToggle(sender);
        }

        private void miscRacialAbilityTrollBerserkingCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            this.miscRacialAbilityTrollBerserkingPanel.Enabled = (sender as CheckBox).Checked;
            InterfaceElementColorToggle(sender);
        }

        private void generalTargetHeightCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            this.generalTargetHeightPanel.Enabled = (sender as CheckBox).Checked;
            InterfaceElementColorToggle(sender);
        }

        private void generalReleaseSpiritOnDeathEnabledCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            this.generalReleaseSpiritOnDeathPanel.Enabled = (sender as CheckBox).Checked;
            InterfaceElementColorToggle(sender);
        }

        #endregion

        #region UI Events: Close, Apply, Help, and Specialization Buttons

        private void closeButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void applyButton_Click(object sender, EventArgs e)
        {
            ApplySettings();

            SettingsManager.Instance.Save();

            GlobalSettingsManager.Instance.LastUsedProfile = this.aboutProfilesPesetsComboBox.SelectedItem.ToString();
            GlobalSettingsManager.Instance.Save();

            SaveItemsData();

            this.DialogResult = DialogResult.OK;
        }

        private void openSupportThreadButton_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.thebuddyforum.com/honorbuddy-forum/combat-routines/druid/204632-paws-feral-druid-combat-routine.html");
        }

        private void specializationButton_Click(object sender, EventArgs e)
        {
            switch (this.SettingsMode)
            {
                case WoWSpec.DruidFeral:
                    {
                        this.SettingsMode = WoWSpec.DruidGuardian;
                        break;
                    }
                case WoWSpec.DruidGuardian:
                    {
                        this.SettingsMode = WoWSpec.DruidFeral;
                        break;
                    }
            }

            InitSettingsMode();
            BindUISettings();
        }

        #endregion
    }
}
