using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Paws.Core;
using Paws.Core.Managers;
using Paws.Interface.Controls;
using Paws.Interface.Controls.Feral;
using Paws.Interface.Controls.Guardian;
using Paws.Resources;
using Styx;
using Styx.Helpers;
using Styx.WoWInternals.WoWObjects;

namespace Paws.Interface.Forms
{
    public partial class SettingsForm : Form
    {
        private AbilityChainsControl _abilityChainsControl;
        public WoWSpec SettingsMode { get; set; }

        private static LocalPlayer Me
        {
            get { return StyxWoW.Me; }
        }

        #region Load Events

        public SettingsForm()
        {
            SettingsMode = Me.Specialization;

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
        ///     Custom form drag and close handler.
        /// </summary>
        private Point _formHeaderCursorPosition;

        private void FormHeader_MouseDown(object sender, MouseEventArgs e)
        {
            _formHeaderCursorPosition = new Point(-e.X, -e.Y);
        }

        private void FormHeader_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;
            var mousePos = MousePosition;
            mousePos.Offset(_formHeaderCursorPosition.X, _formHeaderCursorPosition.Y);
            Location = mousePos;
        }

        private void FormHeaderClose_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        #endregion

        #region Helper Functions

        /// <summary>
        ///     Initializes the specialization mode in which the UI should display settings to the user.
        /// </summary>
        private void InitSettingsMode()
        {
            AddPremiumContent();

            mobilityTab.Controls.Clear();
            offensiveTab.Controls.Clear();
            defensiveTab.Controls.Clear();
            healingTab.Controls.Clear();

            switch (SettingsMode)
            {
                case WoWSpec.DruidGuardian:
                {
                    specializationButton.Text = Properties.Resources.SettingsForm_InitSettingsMode_Feral;
                    mobilityTab.Controls.Add(new GuardianMobilitySettings(this));
                    offensiveTab.Controls.Add(new GuardianOffensiveSettings(this));
                    defensiveTab.Controls.Add(new GuardianDefensiveSettings(this));
                    healingTab.Controls.Add(new GuardianHealingSettings(this));
                    break;
                }
                default:
                {
                    specializationButton.Text = Properties.Resources.SettingsForm_InitSettingsMode_Guardian;
                    mobilityTab.Controls.Add(new FeralMobilitySettings(this));
                    offensiveTab.Controls.Add(new FeralOffensiveSettings(this));
                    defensiveTab.Controls.Add(new FeralDefensiveSettings(this));
                    healingTab.Controls.Add(new FeralHealingSettings(this));
                    break;
                }
            }
        }

        /// <summary>
        ///     Method responsibile for setting up the visibility of the premium content.
        /// </summary>
        private void AddPremiumContent()
        {
            if (Main.Product == Product.Premium)
            {
                var tabPage = new TabPage("Ability Chains");

                _abilityChainsControl = new AbilityChainsControl();

                tabPage.Controls.Add(_abilityChainsControl);

                vt.TabPages.Add(tabPage);
            }
        }

        /// <summary>
        ///     Loads the Rtf file into the introduction Rich text control.
        /// </summary>
        private void LoadIntroduction()
        {
            // This method has been changed so that we are no longer loading from file and instead loading from embedded resource
            // to comply with store requirements.

            rtfAbout.Rtf = release.release_notes;
        }

        /// <summary>
        ///     Sets the Version information label.
        /// </summary>
        private void SetVersionInformation()
        {
            versionLabel.Text = string.Format("Version {0}", Main.Version);
        }

        /// <summary>
        ///     Adds a list view item to the items list view.
        /// </summary>
        private void AddItemToItemList(PawsItem item)
        {
            var lvItem = new ListViewItem(item.Name);

            lvItem.UseItemStyleForSubItems = false;
            lvItem.SubItems.Add(item.Enabled ? Properties.Resources.SettingsForm_itemsEnableCheckedItemsButton_Click_Enabled : Properties.Resources.SettingsForm_itemsDisableCheckedItemsButton_Click_Disabled, Color.White,
                item.Enabled ? Color.DarkGreen : Color.DarkRed, new Font("Arial", 9.0f, FontStyle.Bold));
            lvItem.SubItems.Add(item.MyState.ToString());
            lvItem.SubItems.Add(item.GetConditionsDescription());
            lvItem.Tag = item;

            itemsListView.Items.Add(lvItem);
        }

        /// <summary>
        ///     Binds the current settings instance to the UI controls.
        /// </summary>
        private void BindUiSettings()
        {
            // Form Title //
            FormHeader.Text = string.Format("Paws {0} [{1}] Settings: {2}",
                Main.Product == Product.Community ? "Community" : "Premium",
                SettingsMode.ToString().Replace("Druid", string.Empty), aboutProfilesPesetsComboBox.SelectedItem);

            // General Tab //
            generalMarkOfTheWildEnabledCheckBox.Checked = SettingsManager.Instance.MarkOfTheWildEnabled;
            generalMarkOfTheWildEnabledCheckBox_CheckedChanged(generalMarkOfTheWildEnabledCheckBox, EventArgs.Empty);
            generalMarkOfTheWildDoNotApplyStealthedCheckBox.Checked =
                SettingsManager.Instance.MarkOfTheWildDoNotApplyIfStealthed;
            generalSootheEnabledCheckBox.Checked = SettingsManager.Instance.SootheEnabled;
            generalSootheEnabledCheckBox_CheckedChanged(generalSootheEnabledCheckBox, EventArgs.Empty);
            generalSootheReactionTimeTextBox.Text = SettingsManager.Instance.SootheReactionTimeInMs.ToString();
            generalTargetHeightCheckBox.Checked = SettingsManager.Instance.TargetHeightEnabled;
            generalTargetHeightCheckBox_CheckedChanged(generalTargetHeightCheckBox, EventArgs.Empty);
            generalTargetHeightTextBox.Text = SettingsManager.Instance.TargetHeightMinDistance.ToString("0.##");
            generalReleaseSpiritOnDeathEnabledCheckBox.Checked = SettingsManager.Instance.ReleaseSpiritOnDeathEnabled;
            generalReleaseSpiritOnDeathEnabledCheckBox_CheckedChanged(generalReleaseSpiritOnDeathEnabledCheckBox,
                EventArgs.Empty);
            generalReleaseSpiritOnDeathTimerTextBox.Text =
                SettingsManager.Instance.ReleaseSpiritOnDeathIntervalInMs.ToString();
            generalInterruptTimingMinMSTextBox.Text = SettingsManager.Instance.InterruptMinMilliseconds.ToString();
            generalInterruptTimingMaxMSTextBox.Text = SettingsManager.Instance.InterruptMaxMilliseconds.ToString();
            generalInterruptTimingSuccessRateTextBox.Text =
                SettingsManager.Instance.InterruptSuccessRate.ToString("0.##");
            mobilityGeneralMovementCheckBox.Checked = SettingsManager.Instance.AllowMovement;
            mobilityGeneralTargetFacingCheckBox.Checked = SettingsManager.Instance.AllowTargetFacing;
            mobilityAutoTargetCheckBox.Checked = SettingsManager.Instance.AllowTargeting;
            mobilityForceCombatCheckBox.Checked = SettingsManager.Instance.ForceCombat;

            // Mobility Tab //
            BindUiSettingsToControlCollection(mobilityTab.Controls);

            // Offesnive Tab //
            BindUiSettingsToControlCollection(offensiveTab.Controls);

            // Defensive Tab //
            BindUiSettingsToControlCollection(defensiveTab.Controls);

            // Healing Tab //
            BindUiSettingsToControlCollection(healingTab.Controls);

            // Trinkets/Racials Tab //
            miscTrinket1EnabledCheckBox.Checked = SettingsManager.Instance.Trinket1Enabled;
            miscTrinket1EnabledCheckBox_CheckedChanged(miscTrinket1EnabledCheckBox, EventArgs.Empty);
            miscTrinket1UseOnMe.Checked = SettingsManager.Instance.Trinket1UseOnMe;
            miscTrinket1UseOnEnemy.Checked = SettingsManager.Instance.Trinket1UseOnEnemy;
            miscTrinket1OnCooldownRadioButton.Checked = SettingsManager.Instance.Trinket1OnCoolDown;
            miscTrinket1LossOfControlRadioButton.Checked = SettingsManager.Instance.Trinket1LossOfControl;
            miscTrinket1TheseConditionsRadioButton.Checked = SettingsManager.Instance.Trinket1AdditionalConditions;
            miscTrinket1HealthMinCheckBox.Checked = SettingsManager.Instance.Trinket1HealthMinEnabled;
            miscTrinket1HealthMinTextBox.Text = SettingsManager.Instance.Trinket1HealthMin.ToString("0.##");
            miscTrinket1ManaMinCheckBox.Checked = SettingsManager.Instance.Trinket1ManaMinEnabled;
            miscTrinket1ManaMinTextBox.Text = SettingsManager.Instance.Trinket1ManaMin.ToString("0.##");
            miscTrinket2EnabledCheckBox.Checked = SettingsManager.Instance.Trinket2Enabled;
            miscTrinket2EnabledCheckBox_CheckedChanged(miscTrinket2EnabledCheckBox, EventArgs.Empty);
            miscTrinket2UseOnMe.Checked = SettingsManager.Instance.Trinket2UseOnMe;
            miscTrinket2UseOnEnemy.Checked = SettingsManager.Instance.Trinket2UseOnEnemy;
            miscTrinket2OnCooldownRadioButton.Checked = SettingsManager.Instance.Trinket2OnCoolDown;
            miscTrinket2LossOfControlRadioButton.Checked = SettingsManager.Instance.Trinket2LossOfControl;
            miscTrinket2TheseConditionsRadioButton.Checked = SettingsManager.Instance.Trinket2AdditionalConditions;
            miscTrinket2HealthMinCheckBox.Checked = SettingsManager.Instance.Trinket2HealthMinEnabled;
            miscTrinket2HealthMinTextBox.Text = SettingsManager.Instance.Trinket2HealthMin.ToString("0.##");
            miscTrinket2ManaMinCheckBox.Checked = SettingsManager.Instance.Trinket2ManaMinEnabled;
            miscTrinket2ManaMinTextBox.Text = SettingsManager.Instance.Trinket2ManaMin.ToString("0.##");

            miscTrinket1EnabledCheckBox.Text = Me.Inventory.Equipped.Trinket1 != null
                ? string.Format("Trinket: ({0})", Me.Inventory.Equipped.Trinket1.SafeName)
                : "Trinket #1 (Not Equipped)";

            miscTrinket2EnabledCheckBox.Text = Me.Inventory.Equipped.Trinket2 != null
                ? string.Format("Trinket: ({0})", Me.Inventory.Equipped.Trinket2.SafeName)
                : "Trinket #2 (Not Equipped)";

            // Trinkets/Racials Tab //
            miscRacialAbilityTaurenWarStompCheckBox.Checked = SettingsManager.Instance.TaurenWarStompEnabled;
            miscRacialAbilityTaurenWarStompCheckBox_CheckedChanged(miscRacialAbilityTaurenWarStompCheckBox,
                EventArgs.Empty);
            miscRacialAbilityTaurenWarStompMinEnemies.Text =
                SettingsManager.Instance.TaurenWarStompMinEnemies.ToString();
            miscRacialAbilityTrollBerserkingCheckBox.Checked = SettingsManager.Instance.TrollBerserkingEnabled;
            miscRacialAbilityTrollBerserkingCheckBox_CheckedChanged(miscRacialAbilityTrollBerserkingCheckBox,
                EventArgs.Empty);
            miscRacialAbilityTrollBerserkingOnCooldownRadioButton.Checked =
                SettingsManager.Instance.TrollBerserkingOnCooldown;
            miscRacialAbilityTrollBerserkingEnemyHealthRadioButton.Checked =
                SettingsManager.Instance.TrollBerserkingEnemyHealthCheck;
            miscRacialAbilityTrollBerserkingEnemyHealthMultiplierTextBox.Text =
                SettingsManager.Instance.TrollBerserkingEnemyHealthMultiplier.ToString("0.##");
            miscRacialAbilityTrollBerserkingSurroundedByEnemiesCheckBox.Checked =
                SettingsManager.Instance.TrollBerserkingSurroundedByEnemiesEnabled;
            miscRacialAbilityTrollBerserkingSurroundedByEnemiesTextBox.Text =
                SettingsManager.Instance.TrollBerserkingSurroundedByMinEnemies.ToString();
        }

        /// <summary>
        ///     Bind settings to the specified set of controls.
        /// </summary>
        private void BindUiSettingsToControlCollection(Control.ControlCollection controls)
        {
            foreach (var control in controls)
            {
                var settingsControl = control as ISettingsControl;
                if (settingsControl != null)
                {
                    settingsControl.BindUiSettings();
                    return;
                }
            }
        }

        /// <summary>
        ///     Applies all of the settings to the current settings instance. Does not save to file.
        /// </summary>
        private void ApplySettings()
        {
            // General Tab //
            SettingsManager.Instance.MarkOfTheWildEnabled = generalMarkOfTheWildEnabledCheckBox.Checked;
            SettingsManager.Instance.MarkOfTheWildDoNotApplyIfStealthed =
                generalMarkOfTheWildDoNotApplyStealthedCheckBox.Checked;
            SettingsManager.Instance.SootheEnabled = generalSootheEnabledCheckBox.Checked;
            SettingsManager.Instance.SootheReactionTimeInMs = Convert.ToInt32(generalSootheReactionTimeTextBox.Text);
            SettingsManager.Instance.TargetHeightEnabled = generalTargetHeightCheckBox.Checked;
            SettingsManager.Instance.TargetHeightMinDistance = generalTargetHeightTextBox.Text.ToFloat();
            SettingsManager.Instance.ReleaseSpiritOnDeathEnabled = generalReleaseSpiritOnDeathEnabledCheckBox.Checked;
            SettingsManager.Instance.ReleaseSpiritOnDeathIntervalInMs =
                Convert.ToInt32(generalReleaseSpiritOnDeathTimerTextBox.Text);
            SettingsManager.Instance.InterruptMinMilliseconds = Convert.ToInt32(generalInterruptTimingMinMSTextBox.Text);
            SettingsManager.Instance.InterruptMaxMilliseconds = Convert.ToInt32(generalInterruptTimingMaxMSTextBox.Text);
            SettingsManager.Instance.InterruptSuccessRate =
                Convert.ToDouble(generalInterruptTimingSuccessRateTextBox.Text);
            SettingsManager.Instance.AllowMovement = mobilityGeneralMovementCheckBox.Checked;
            SettingsManager.Instance.AllowTargetFacing = mobilityGeneralTargetFacingCheckBox.Checked;
            SettingsManager.Instance.AllowTargeting = mobilityAutoTargetCheckBox.Checked;
            SettingsManager.Instance.ForceCombat = mobilityForceCombatCheckBox.Checked;

            // Mobility Tab //
            ApplySettingsFromControlCollection(mobilityTab.Controls);

            // Offensive Tab //
            ApplySettingsFromControlCollection(offensiveTab.Controls);

            // Defensive Tab //
            ApplySettingsFromControlCollection(defensiveTab.Controls);

            // Healing Tab //
            ApplySettingsFromControlCollection(healingTab.Controls);

            // Trinket/Racials Tab //
            SettingsManager.Instance.Trinket1Enabled = miscTrinket1EnabledCheckBox.Checked;
            SettingsManager.Instance.Trinket1UseOnMe = miscTrinket1UseOnMe.Checked;
            SettingsManager.Instance.Trinket1UseOnEnemy = miscTrinket1UseOnEnemy.Checked;
            SettingsManager.Instance.Trinket1OnCoolDown = miscTrinket1OnCooldownRadioButton.Checked;
            SettingsManager.Instance.Trinket1LossOfControl = miscTrinket1LossOfControlRadioButton.Checked;
            SettingsManager.Instance.Trinket1AdditionalConditions = miscTrinket1TheseConditionsRadioButton.Checked;
            SettingsManager.Instance.Trinket1HealthMinEnabled = miscTrinket1HealthMinCheckBox.Checked;
            SettingsManager.Instance.Trinket1HealthMin = Convert.ToDouble(miscTrinket1HealthMinTextBox.Text);
            SettingsManager.Instance.Trinket1ManaMinEnabled = miscTrinket1ManaMinCheckBox.Checked;
            SettingsManager.Instance.Trinket1ManaMin = Convert.ToDouble(miscTrinket1ManaMinTextBox.Text);
            SettingsManager.Instance.Trinket2Enabled = miscTrinket2EnabledCheckBox.Checked;
            SettingsManager.Instance.Trinket2UseOnMe = miscTrinket2UseOnMe.Checked;
            SettingsManager.Instance.Trinket2UseOnEnemy = miscTrinket2UseOnEnemy.Checked;
            SettingsManager.Instance.Trinket2OnCoolDown = miscTrinket2OnCooldownRadioButton.Checked;
            SettingsManager.Instance.Trinket2LossOfControl = miscTrinket2LossOfControlRadioButton.Checked;
            SettingsManager.Instance.Trinket2AdditionalConditions = miscTrinket2TheseConditionsRadioButton.Checked;
            SettingsManager.Instance.Trinket2HealthMinEnabled = miscTrinket2HealthMinCheckBox.Checked;
            SettingsManager.Instance.Trinket2HealthMin = Convert.ToDouble(miscTrinket2HealthMinTextBox.Text);
            SettingsManager.Instance.Trinket2ManaMinEnabled = miscTrinket2ManaMinCheckBox.Checked;
            SettingsManager.Instance.Trinket2ManaMin = Convert.ToDouble(miscTrinket2ManaMinTextBox.Text);
            SettingsManager.Instance.TaurenWarStompEnabled = miscRacialAbilityTaurenWarStompCheckBox.Checked;
            SettingsManager.Instance.TaurenWarStompMinEnemies =
                Convert.ToInt32(miscRacialAbilityTaurenWarStompMinEnemies.Text);
            SettingsManager.Instance.TrollBerserkingEnabled = miscRacialAbilityTrollBerserkingCheckBox.Checked;
            SettingsManager.Instance.TrollBerserkingOnCooldown =
                miscRacialAbilityTrollBerserkingOnCooldownRadioButton.Checked;
            SettingsManager.Instance.TrollBerserkingEnemyHealthCheck =
                miscRacialAbilityTrollBerserkingEnemyHealthRadioButton.Checked;
            SettingsManager.Instance.TrollBerserkingEnemyHealthMultiplier =
                miscRacialAbilityTrollBerserkingEnemyHealthMultiplierTextBox.Text.ToFloat();
            SettingsManager.Instance.TrollBerserkingSurroundedByEnemiesEnabled =
                miscRacialAbilityTrollBerserkingSurroundedByEnemiesCheckBox.Checked;
            SettingsManager.Instance.TrollBerserkingSurroundedByMinEnemies =
                Convert.ToInt32(miscRacialAbilityTrollBerserkingSurroundedByEnemiesTextBox.Text);
        }

        /// <summary>
        ///     Applies settings from the specified set of controls.
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
        ///     Retrieves the available profiles for the current character, and populates the profiles dropdown control.
        /// </summary>
        private void PopulateProfiles()
        {
            aboutProfilesPesetsComboBox.Items.Clear();

            foreach (var file in GlobalSettingsManager.GetCharacterProfileFiles())
            {
                aboutProfilesPesetsComboBox.Items.Add(Path.GetFileNameWithoutExtension(file));
            }

            aboutProfilesPesetsComboBox.SelectedItem = GlobalSettingsManager.Instance.LastUsedProfile;
        }

        /// <summary>
        ///     Retrieves the available items from the item manager and populates the item list view control.
        /// </summary>
        private void PopulateItemsListView()
        {
            foreach (var item in ItemManager.Items)
            {
                AddItemToItemList(item);
            }
        }

        /// <summary>
        ///     Gathers the list of items from the items list view and tells the item manager to save the items to file.
        /// </summary>
        private void SaveItemsData()
        {
            var items = new List<PawsItem>();

            foreach (ListViewItem lvItem in itemsListView.Items)
            {
                items.Add(lvItem.Tag as PawsItem);
            }

            ItemManager.SaveDataSet(items);
        }

        private void SavePremiumContentData()
        {
            if (Main.Product == Product.Premium)
            {
                if (_abilityChainsControl != null)
                {
                    _abilityChainsControl.SaveData();
                }
            }
        }

        #endregion

        #region UI Events: Profiles

        private void aboutProfilesPesetsComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            var pathToProfileFile =
                GlobalSettingsManager.GetFullPathToProfile(aboutProfilesPesetsComboBox.SelectedItem.ToString());

            SettingsManager.InitWithProfile(pathToProfileFile);

            BindUiSettings();
        }

        private void aboutProfilesSaveAsButton_Click(object sender, EventArgs e)
        {
            var newForm = new SaveProfileAsNameForm();

            if (newForm.ShowDialog() == DialogResult.OK)
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
            foreach (ListViewItem lvItem in itemsListView.CheckedItems)
            {
                var item = lvItem.Tag as PawsItem;

                if (item == null) continue;
                item.Enabled = true;
                lvItem.SubItems[1].Text = Properties.Resources.SettingsForm_itemsEnableCheckedItemsButton_Click_Enabled;
                lvItem.SubItems[1].BackColor = Color.DarkGreen;

                lvItem.Tag = item;
            }
        }

        private void itemsDisableCheckedItemsButton_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem lvItem in itemsListView.CheckedItems)
            {
                var item = lvItem.Tag as PawsItem;

                if (item == null) continue;
                item.Enabled = false;
                lvItem.SubItems[1].Text = Properties.Resources.SettingsForm_itemsDisableCheckedItemsButton_Click_Disabled;
                lvItem.SubItems[1].BackColor = Color.DarkRed;

                lvItem.Tag = item;
            }
        }

        private void itemsRemoveSelectedItemsButton_Click(object sender, EventArgs e)
        {
            if (itemsListView.CheckedItems.Count <= 0) return;
            var result =
                MessageBox.Show(string.Format("You are about to remove {0} {1}. Would you like to proceed?",
                    itemsListView.CheckedItems.Count, itemsListView.CheckedItems.Count == 1 ? "item" : "items"),
                    Properties.Resources.SettingsForm_itemsRemoveSelectedItemsButton_Click_Warning,
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

            if (result != DialogResult.Yes) return;
            foreach (ListViewItem item in itemsListView.CheckedItems)
            {
                item.Remove();
            }
        }

        private void itemsListViewContextMenu_Opening(object sender, CancelEventArgs e)
        {
            if (itemsListView.SelectedItems.Count == 0)
            {
                e.Cancel = true;
                return;
            }

            var selectedItem = itemsListView.SelectedItems[0];

            enabledToolStripMenuItem.Checked = (selectedItem.Tag as PawsItem).Enabled;
        }

        private void editItemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (itemsListView.SelectedItems.Count <= 0) return;
            var lvItem = itemsListView.SelectedItems[0];

            var editForm = new AddNewItemForm(lvItem.Tag as PawsItem);

            if (editForm.ShowDialog() != DialogResult.OK) return;
            lvItem.Text = editForm.PawsItem.Name;

            lvItem.SubItems[1].Text = editForm.PawsItem.Enabled ? Properties.Resources.SettingsForm_itemsEnableCheckedItemsButton_Click_Enabled : Properties.Resources.SettingsForm_itemsDisableCheckedItemsButton_Click_Disabled;
            lvItem.SubItems[1].BackColor = editForm.PawsItem.Enabled ? Color.DarkGreen : Color.DarkRed;

            lvItem.SubItems[2].Text = editForm.PawsItem.MyState.ToString();
            lvItem.SubItems[3].Text = editForm.PawsItem.GetConditionsDescription();

            lvItem.Tag = editForm.PawsItem;
        }

        private void removeItemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in itemsListView.SelectedItems)
                item.Remove();
        }

        private void enabledToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in itemsListView.SelectedItems)
            {
                var pawsItem = item.Tag as PawsItem;
                if (pawsItem == null) continue;
                pawsItem.Enabled = !pawsItem.Enabled;

                item.SubItems[1].Text = pawsItem.Enabled ? Properties.Resources.SettingsForm_itemsEnableCheckedItemsButton_Click_Enabled : Properties.Resources.SettingsForm_itemsDisableCheckedItemsButton_Click_Disabled;
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
            var checkBox = sender as CheckBox;
            if (checkBox != null) generalMarkOfTheWildPanel.Enabled = checkBox.Checked;
            InterfaceElementColorToggle(sender);
        }

        private void generalSootheEnabledCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            var checkBox = sender as CheckBox;
            if (checkBox != null) generalSoothePanel.Enabled = checkBox.Checked;
            InterfaceElementColorToggle(sender);
        }

        private void miscTrinket1EnabledCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            var checkBox = sender as CheckBox;
            if (checkBox != null) miscTrinket1Panel.Enabled = checkBox.Checked;
            InterfaceElementColorToggle(sender);
        }

        private void miscTrinket2EnabledCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            var checkBox = sender as CheckBox;
            if (checkBox != null) miscTrinket2Panel.Enabled = checkBox.Checked;
            InterfaceElementColorToggle(sender);
        }

        private void miscRacialAbilityTaurenWarStompCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            var checkBox = sender as CheckBox;
            if (checkBox != null) miscRacialAbilityTaurenWarStompPanel.Enabled = checkBox.Checked;
            InterfaceElementColorToggle(sender);
        }

        private void miscRacialAbilityTrollBerserkingCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            var checkBox = sender as CheckBox;
            if (checkBox != null)
                miscRacialAbilityTrollBerserkingPanel.Enabled = checkBox.Checked;
            InterfaceElementColorToggle(sender);
        }

        private void generalTargetHeightCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            var checkBox = sender as CheckBox;
            if (checkBox != null) generalTargetHeightPanel.Enabled = checkBox.Checked;
            InterfaceElementColorToggle(sender);
        }

        private void generalReleaseSpiritOnDeathEnabledCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            var checkBox = sender as CheckBox;
            if (checkBox != null) generalReleaseSpiritOnDeathPanel.Enabled = checkBox.Checked;
            InterfaceElementColorToggle(sender);
        }

        #endregion

        #region UI Events: Close, Apply, Help, and Specialization Buttons

        private void closeButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void applyButton_Click(object sender, EventArgs e)
        {
            ApplySettings();

            SettingsManager.Instance.Save();

            GlobalSettingsManager.Instance.LastUsedProfile = aboutProfilesPesetsComboBox.SelectedItem.ToString();
            GlobalSettingsManager.Instance.Save();

            SaveItemsData();

            SavePremiumContentData();

            DialogResult = DialogResult.OK;
        }

        private void openSupportThreadButton_Click(object sender, EventArgs e)
        {
            Process.Start(
                Main.Product == Product.Premium
                    ? "https://www.thebuddyforum.com/thebuddystore/honorbuddy-store/honorbuddy-store-routines/druid/214189-paws-feral-guardian-premium-edition.html"
                    : "https://www.thebuddyforum.com/thebuddystore/honorbuddy-store/honorbuddy-store-routines/druid/210106-paws-feral-guardian-community-edition.html");
        }

        private void specializationButton_Click(object sender, EventArgs e)
        {
            switch (SettingsMode)
            {
                case WoWSpec.DruidFeral:
                {
                    SettingsMode = WoWSpec.DruidGuardian;
                    break;
                }
                case WoWSpec.DruidGuardian:
                {
                    SettingsMode = WoWSpec.DruidFeral;
                    break;
                }
            }

            InitSettingsMode();
            BindUiSettings();
        }

        #endregion
    }
}