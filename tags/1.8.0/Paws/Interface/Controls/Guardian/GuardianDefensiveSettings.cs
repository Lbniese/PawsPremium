using Paws.Core.Managers;
using System;
using System.Windows.Forms;
using Styx.Helpers;

namespace Paws.Interface.Controls.Guardian
{
    public partial class GuardianDefensiveSettings : UserControl, ISettingsControl
    {
        private SettingsManager Settings { get { return SettingsManager.Instance; } }

        public SettingsForm SettingsForm { get; set; }

        public GuardianDefensiveSettings(SettingsForm settingsForm)
        {
            this.SettingsForm = settingsForm;
            InitializeComponent();
        }

        public void BindUISettings()
        {
            this.defensiveSurvivalInstinctsEnabledCheckBox.Checked = Settings.GuardianSurvivalInstinctsEnabled;
            defensiveSurvivalInstinctsEnabledCheckBox_CheckedChanged(this.defensiveSurvivalInstinctsEnabledCheckBox, EventArgs.Empty);
            this.defensiveSurvivalInstinctsMinHealthTextBox.Text = Settings.GuardianSurvivalInstinctsMinHealth.ToString("0.##");
            this.defensiveBarkskinEnabledCheckBox.Checked = Settings.BarkskinEnabled;
            defensiveBarkskinEnabledCheckBox_CheckedChanged(this.defensiveBarkskinEnabledCheckBox, EventArgs.Empty);
            this.defensiveBarkskinMinHealthTextBox.Text = Settings.BarkskinMinHealth.ToString("0.##");
            this.defensiveBristlingFurEnabledCheckBox.Checked = Settings.BristlingFurEnabled;
            defensiveBristlingFurEnabledCheckBox_CheckedChanged(defensiveBristlingFurEnabledCheckBox, EventArgs.Empty);
            this.defensiveBristlingFurMinHealthTextBox.Text = Settings.BristlingFurMinHealth.ToString("0.##");
            this.defensiveSavageDefenseEnabledCheckBox.Checked = Settings.SavageDefenseEnabled;
            defensiveSavageDefenseEnabledCheckBox_CheckedChanged(this.defensiveSavageDefenseEnabledCheckBox, EventArgs.Empty);
            this.defensiveSavageDefenseMinHealthTextBox.Text = Settings.SavageDefenseMinHealth.ToString("0.##");
            this.defensiveSavageDefenseMinRageTextBox.Text = Settings.SavageDefenseMinRage.ToString("0.##");
            this.defensiveSkullBashEnabledCheckBox.Checked = Settings.GuardianSkullBashEnabled;
            defensiveSkullBashEnabledCheckBox_CheckedChanged(this.defensiveSkullBashEnabledCheckBox, EventArgs.Empty);
            this.defensiveTyphoonEnabledCheckBox.Checked = Settings.GuardianTyphoonEnabled;
            defensiveTyphoonEnabledCheckBox_CheckedChanged(this.defensiveTyphoonEnabledCheckBox, EventArgs.Empty);
            this.defensiveMightyBashEnabledCheckBox.Checked = Settings.GuardianMightyBashEnabled;
            defensiveMightyBashEnabledCheckBox_CheckedChanged(this.defensiveMightyBashEnabledCheckBox, EventArgs.Empty);
            this.defensiveIncapacitatingRoarEnabledCheckBox.Checked = Settings.GuardianIncapacitatingRoarEnabled;
            defensiveIncapacitatingRoarEnabledCheckBox_CheckedChanged(this.defensiveIncapacitatingRoarEnabledCheckBox, EventArgs.Empty);
            this.defensiveIncapacitatingRoarMinEnemiesTextBox.Text = Settings.GuardianIncapacitatingRoarMinEnemies.ToString();
            this.defensiveMassEntanglementEnabledCheckBox.Checked = Settings.GuardianMassEntanglementEnabled;
            defensiveMassEntanglementEnabledCheckBox_CheckedChanged(this.defensiveMassEntanglementEnabledCheckBox, EventArgs.Empty);
            this.defensiveMassEntanglementMinEnemiesTextBox.Text = Settings.GuardianMassEntanglementMinEnemies.ToString();
        }

        public void ApplySettings()
        {
            Settings.GuardianSurvivalInstinctsEnabled = this.defensiveSurvivalInstinctsEnabledCheckBox.Checked;
            Settings.GuardianSurvivalInstinctsMinHealth = Convert.ToDouble(this.defensiveSurvivalInstinctsMinHealthTextBox.Text);
            Settings.BarkskinEnabled = this.defensiveBarkskinEnabledCheckBox.Checked;
            Settings.BarkskinMinHealth = Convert.ToDouble(this.defensiveBarkskinMinHealthTextBox.Text);
            Settings.BristlingFurEnabled = this.defensiveBristlingFurEnabledCheckBox.Checked;
            Settings.BristlingFurMinHealth = Convert.ToDouble(this.defensiveBristlingFurMinHealthTextBox.Text);
            Settings.SavageDefenseEnabled = this.defensiveSavageDefenseEnabledCheckBox.Checked;
            Settings.SavageDefenseMinHealth = Convert.ToDouble(this.defensiveSavageDefenseMinHealthTextBox.Text);
            Settings.SavageDefenseMinRage = Convert.ToDouble(this.defensiveSavageDefenseMinRageTextBox.Text);
            Settings.GuardianSkullBashEnabled = this.defensiveSkullBashEnabledCheckBox.Checked;
            Settings.GuardianTyphoonEnabled = defensiveTyphoonEnabledCheckBox.Checked;
            Settings.GuardianMightyBashEnabled = defensiveMightyBashEnabledCheckBox.Checked;
            Settings.GuardianIncapacitatingRoarEnabled = this.defensiveIncapacitatingRoarEnabledCheckBox.Checked;
            Settings.GuardianIncapacitatingRoarMinEnemies = Convert.ToInt32(this.defensiveIncapacitatingRoarMinEnemiesTextBox.Text);
            Settings.GuardianMassEntanglementEnabled = this.defensiveMassEntanglementEnabledCheckBox.Checked;
            Settings.GuardianMassEntanglementMinEnemies = Convert.ToInt32(this.defensiveMassEntanglementMinEnemiesTextBox.Text);
        }

        #region UI Events: Control Toggles

        private void defensiveSurvivalInstinctsEnabledCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            this.defensiveSurvivalInstinctsPanel.Enabled = (sender as CheckBox).Checked;
            this.SettingsForm.InterfaceElementColorToggle(sender);
        }

        private void defensiveBarkskinEnabledCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            this.defensiveBarkskinPanel.Enabled = (sender as CheckBox).Checked;
            this.SettingsForm.InterfaceElementColorToggle(sender);
        }

        private void defensiveBristlingFurEnabledCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            this.defensiveBristlingFurPanel.Enabled = (sender as CheckBox).Checked;
            this.SettingsForm.InterfaceElementColorToggle(sender);
        }

        private void defensiveSavageDefenseEnabledCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            this.defensiveBarkskinPanel.Enabled = (sender as CheckBox).Checked;
            this.SettingsForm.InterfaceElementColorToggle(sender);
        }

        private void defensiveSkullBashEnabledCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            this.SettingsForm.InterfaceElementColorToggle(sender);
        }

        private void defensiveTyphoonEnabledCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            this.SettingsForm.InterfaceElementColorToggle(sender);
        }

        private void defensiveMightyBashEnabledCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            this.SettingsForm.InterfaceElementColorToggle(sender);
        }

        private void defensiveIncapacitatingRoarEnabledCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            this.defensiveIncapacitatingRoarPanel.Enabled = (sender as CheckBox).Checked;
            this.SettingsForm.InterfaceElementColorToggle(sender);
        }

        private void defensiveMassEntanglementEnabledCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            this.defensiveMassEntanglementPanel.Enabled = (sender as CheckBox).Checked;
            this.SettingsForm.InterfaceElementColorToggle(sender);
        }

        #endregion
    }
}
