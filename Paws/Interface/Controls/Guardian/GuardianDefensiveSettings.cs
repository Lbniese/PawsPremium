using System;
using System.Windows.Forms;
using Paws.Core.Managers;
using Paws.Interface.Forms;

namespace Paws.Interface.Controls.Guardian
{
    public partial class GuardianDefensiveSettings : UserControl, ISettingsControl
    {
        public GuardianDefensiveSettings(SettingsForm settingsForm)
        {
            SettingsForm = settingsForm;
            InitializeComponent();
        }

        private static SettingsManager Settings
        {
            get { return SettingsManager.Instance; }
        }

        public SettingsForm SettingsForm { get; set; }

        public void BindUiSettings()
        {
            defensiveSurvivalInstinctsEnabledCheckBox.Checked = Settings.GuardianSurvivalInstinctsEnabled;
            defensiveSurvivalInstinctsEnabledCheckBox_CheckedChanged(defensiveSurvivalInstinctsEnabledCheckBox,
                EventArgs.Empty);
            defensiveSurvivalInstinctsMinHealthTextBox.Text =
                Settings.GuardianSurvivalInstinctsMinHealth.ToString("0.##");
            defensiveBarkskinEnabledCheckBox.Checked = Settings.BarkskinEnabled;
            defensiveBarkskinEnabledCheckBox_CheckedChanged(defensiveBarkskinEnabledCheckBox, EventArgs.Empty);
            defensiveBarkskinMinHealthTextBox.Text = Settings.BarkskinMinHealth.ToString("0.##");
            defensiveBristlingFurEnabledCheckBox.Checked = Settings.BristlingFurEnabled;
            defensiveBristlingFurEnabledCheckBox_CheckedChanged(defensiveBristlingFurEnabledCheckBox, EventArgs.Empty);
            defensiveBristlingFurMinHealthTextBox.Text = Settings.BristlingFurMinHealth.ToString("0.##");
            defensiveSavageDefenseEnabledCheckBox.Checked = Settings.SavageDefenseEnabled;
            defensiveSavageDefenseEnabledCheckBox_CheckedChanged(defensiveSavageDefenseEnabledCheckBox, EventArgs.Empty);
            defensiveSavageDefenseMinHealthTextBox.Text = Settings.SavageDefenseMinHealth.ToString("0.##");
            defensiveSavageDefenseMinRageTextBox.Text = Settings.SavageDefenseMinRage.ToString("0.##");
            defensiveSkullBashEnabledCheckBox.Checked = Settings.GuardianSkullBashEnabled;
            defensiveSkullBashEnabledCheckBox_CheckedChanged(defensiveSkullBashEnabledCheckBox, EventArgs.Empty);
            defensiveTyphoonEnabledCheckBox.Checked = Settings.GuardianTyphoonEnabled;
            defensiveTyphoonEnabledCheckBox_CheckedChanged(defensiveTyphoonEnabledCheckBox, EventArgs.Empty);
            defensiveMightyBashEnabledCheckBox.Checked = Settings.GuardianMightyBashEnabled;
            defensiveMightyBashEnabledCheckBox_CheckedChanged(defensiveMightyBashEnabledCheckBox, EventArgs.Empty);
            defensiveIncapacitatingRoarEnabledCheckBox.Checked = Settings.GuardianIncapacitatingRoarEnabled;
            defensiveIncapacitatingRoarEnabledCheckBox_CheckedChanged(defensiveIncapacitatingRoarEnabledCheckBox,
                EventArgs.Empty);
            defensiveIncapacitatingRoarMinEnemiesTextBox.Text = Settings.GuardianIncapacitatingRoarMinEnemies.ToString();
            defensiveMassEntanglementEnabledCheckBox.Checked = Settings.GuardianMassEntanglementEnabled;
            defensiveMassEntanglementEnabledCheckBox_CheckedChanged(defensiveMassEntanglementEnabledCheckBox,
                EventArgs.Empty);
            defensiveMassEntanglementMinEnemiesTextBox.Text = Settings.GuardianMassEntanglementMinEnemies.ToString();
        }

        public void ApplySettings()
        {
            Settings.GuardianSurvivalInstinctsEnabled = defensiveSurvivalInstinctsEnabledCheckBox.Checked;
            Settings.GuardianSurvivalInstinctsMinHealth =
                Convert.ToDouble(defensiveSurvivalInstinctsMinHealthTextBox.Text);
            Settings.BarkskinEnabled = defensiveBarkskinEnabledCheckBox.Checked;
            Settings.BarkskinMinHealth = Convert.ToDouble(defensiveBarkskinMinHealthTextBox.Text);
            Settings.BristlingFurEnabled = defensiveBristlingFurEnabledCheckBox.Checked;
            Settings.BristlingFurMinHealth = Convert.ToDouble(defensiveBristlingFurMinHealthTextBox.Text);
            Settings.SavageDefenseEnabled = defensiveSavageDefenseEnabledCheckBox.Checked;
            Settings.SavageDefenseMinHealth = Convert.ToDouble(defensiveSavageDefenseMinHealthTextBox.Text);
            Settings.SavageDefenseMinRage = Convert.ToDouble(defensiveSavageDefenseMinRageTextBox.Text);
            Settings.GuardianSkullBashEnabled = defensiveSkullBashEnabledCheckBox.Checked;
            Settings.GuardianTyphoonEnabled = defensiveTyphoonEnabledCheckBox.Checked;
            Settings.GuardianMightyBashEnabled = defensiveMightyBashEnabledCheckBox.Checked;
            Settings.GuardianIncapacitatingRoarEnabled = defensiveIncapacitatingRoarEnabledCheckBox.Checked;
            Settings.GuardianIncapacitatingRoarMinEnemies =
                Convert.ToInt32(defensiveIncapacitatingRoarMinEnemiesTextBox.Text);
            Settings.GuardianMassEntanglementEnabled = defensiveMassEntanglementEnabledCheckBox.Checked;
            Settings.GuardianMassEntanglementMinEnemies =
                Convert.ToInt32(defensiveMassEntanglementMinEnemiesTextBox.Text);
        }

        #region UI Events: Control Toggles

        private void defensiveSurvivalInstinctsEnabledCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            var checkBox = sender as CheckBox;
            if (checkBox != null) defensiveSurvivalInstinctsPanel.Enabled = checkBox.Checked;
            SettingsForm.InterfaceElementColorToggle(sender);
        }

        private void defensiveBarkskinEnabledCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            var checkBox = sender as CheckBox;
            if (checkBox != null) defensiveBarkskinPanel.Enabled = checkBox.Checked;
            SettingsForm.InterfaceElementColorToggle(sender);
        }

        private void defensiveBristlingFurEnabledCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            var checkBox = sender as CheckBox;
            if (checkBox != null) defensiveBristlingFurPanel.Enabled = checkBox.Checked;
            SettingsForm.InterfaceElementColorToggle(sender);
        }

        private void defensiveSavageDefenseEnabledCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            var checkBox = sender as CheckBox;
            if (checkBox != null) defensiveBarkskinPanel.Enabled = checkBox.Checked;
            SettingsForm.InterfaceElementColorToggle(sender);
        }

        private void defensiveSkullBashEnabledCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            SettingsForm.InterfaceElementColorToggle(sender);
        }

        private void defensiveTyphoonEnabledCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            SettingsForm.InterfaceElementColorToggle(sender);
        }

        private void defensiveMightyBashEnabledCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            SettingsForm.InterfaceElementColorToggle(sender);
        }

        private void defensiveIncapacitatingRoarEnabledCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            var checkBox = sender as CheckBox;
            if (checkBox != null) defensiveIncapacitatingRoarPanel.Enabled = checkBox.Checked;
            SettingsForm.InterfaceElementColorToggle(sender);
        }

        private void defensiveMassEntanglementEnabledCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            var checkBox = sender as CheckBox;
            if (checkBox != null) defensiveMassEntanglementPanel.Enabled = checkBox.Checked;
            SettingsForm.InterfaceElementColorToggle(sender);
        }

        #endregion
    }
}