using Paws.Core.Managers;
using Styx.Helpers;
using System;
using System.Windows.Forms;

namespace Paws.Interface.Controls.Feral
{
    public partial class FeralDefensiveSettings : UserControl, ISettingsControl
    {
        private SettingsManager Settings { get { return SettingsManager.Instance; } }

        public SettingsForm SettingsForm { get; set; }

        public FeralDefensiveSettings(SettingsForm settingsForm)
        {
            this.SettingsForm = settingsForm;
            InitializeComponent();
        }

        private void FeralMobilitySettings_Load(object sender, EventArgs e)
        {
            
        }

        public void BindUISettings()
        {
            this.defensiveSurvivalInstinctsEnabled.Checked = Settings.SurvivalInstinctsEnabled;
            defensiveSurvivalInstinctsEnabled_CheckedChanged(this.defensiveSurvivalInstinctsEnabled, EventArgs.Empty);
            this.defensiveSurvivalInstinctsMinHealth.Text = Settings.SurvivalInstinctsMinHealth.ToString("0.##");
            this.defensiveHeartOfTheWildEnabledCheckBox.Checked = Settings.HeartOfTheWildEnabled;
            defensiveHeartOfTheWildEnabledCheckBox_CheckedChanged(this.defensiveHeartOfTheWildEnabledCheckBox, EventArgs.Empty);
            this.defensiveHeartOfTheWildMinHealthTextBox.Text = Settings.HeartOfTheWildMinHealth.ToString("0.##");
            this.defensiveIncapacitatingRoarEnabledCheckBox.Checked = Settings.IncapacitatingRoarEnabled;
            defensiveIncapacitatingRoarEnabledCheckBox_CheckedChanged_1(this.defensiveIncapacitatingRoarEnabledCheckBox, EventArgs.Empty);
            this.defensiveIncapacitatingRoarMinEnemiesTextBox.Text = Settings.IncapacitatingRoarMinEnemies.ToString();
            this.defensiveMassEntanglementEnabledCheckBox.Checked = Settings.MassEntanglementEnabled;
            defensiveMassEntanglementEnabledCheckBox_CheckedChanged(this.defensiveMassEntanglementEnabledCheckBox, EventArgs.Empty);
            this.defensiveMassEntanglementMinEnemiesTextBox.Text = Settings.MassEntanglementMinEnemies.ToString();
            this.defensiveTyphoonEnabledCheckBox.Checked = Settings.TyphoonEnabled;
            defensiveTyphoonEnabledCheckBox_CheckedChanged(this.defensiveTyphoonEnabledCheckBox, EventArgs.Empty);
            this.defensiveSkullBashEnabledCheckBox.Checked = Settings.SkullBashEnabled;
            defensiveSkullBashEnabledCheckBox_CheckedChanged(this.defensiveSkullBashEnabledCheckBox, EventArgs.Empty);
            this.defensiveMightyBashEnabledCheckBox.Checked = Settings.MightyBashEnabled;
            defensiveMightyBashEnabledCheckBox_CheckedChanged(this.defensiveMightyBashEnabledCheckBox, EventArgs.Empty);
            this.defensiveMaimEnabledCheckBox.Checked = Settings.MaimEnabled;
            defensiveMaimEnabledCheckBox_CheckedChanged(this.defensiveMaimEnabledCheckBox, EventArgs.Empty);
            this.defensiveMaimMinComboPointsTextBox.Text = Settings.MaimMinComboPoints.ToString();
            this.defensiveFaerieFireRogueCheckBox.Checked = Settings.FaerieFireRogueEnabled;
            this.defensiveFaerieFireDruidCheckBox.Checked = Settings.FaerieFireDruidEnabled;
            this.defensiveFaerieFireWarriorCheckBox.Checked = Settings.FaerieFireWarriorEnabled;
            this.defensiveFaerieFirePaladinCheckBox.Checked = Settings.FaerieFirePaladinEnabled;
            this.defensiveFaerieFireMageCheckBox.Checked = Settings.FaerieFireMageEnabled;
            this.defensiveFaerieFireMonkCheckBox.Checked = Settings.FaerieFireMonkEnabled;
            this.defensiveFaerieFireHunterCheckBox.Checked = Settings.FaerieFireHunterEnabled;
            this.defensiveFaerieFirePriestCheckBox.Checked = Settings.FaerieFirePriestEnabled;
            this.defensiveFaerieFireDeathKnightCheckBox.Checked = Settings.FaerieFireDeathKnightEnabled;
            this.defensiveFaerieFireShamanCheckBox.Checked = Settings.FaerieFireShamanEnabled;
            this.defensiveFaerieFireWarlockCheckBox.Checked = Settings.FaerieFireWarlockEnabled;
            this.defensiveSnaresBearFormPowerShiftEnabled.Checked = Settings.BearFormPowerShiftEnabled;
            this.defensiveSnaresUseStampedingRoarCheckBox.Checked = Settings.RemoveSnareWithStampedingRoar;
            this.defensiveSnaresUseDashCheckBox.Checked = Settings.RemoveSnareWithDash;
            this.defensiveSnareReactionTimeTextBox.Text = Settings.SnareReactionTimeInMs.ToString();
        }

        public void ApplySettings()
        {
            Settings.SurvivalInstinctsEnabled = this.defensiveSurvivalInstinctsEnabled.Checked;
            Settings.SurvivalInstinctsMinHealth = Convert.ToDouble(this.defensiveSurvivalInstinctsMinHealth.Text);
            Settings.HeartOfTheWildEnabled = this.defensiveHeartOfTheWildEnabledCheckBox.Checked;
            Settings.HeartOfTheWildMinHealth = Convert.ToDouble(this.defensiveHeartOfTheWildMinHealthTextBox.Text);
            Settings.IncapacitatingRoarEnabled = this.defensiveIncapacitatingRoarEnabledCheckBox.Checked;
            Settings.IncapacitatingRoarMinEnemies = Convert.ToInt32(this.defensiveIncapacitatingRoarMinEnemiesTextBox.Text);
            Settings.MassEntanglementEnabled = this.defensiveMassEntanglementEnabledCheckBox.Checked;
            Settings.MassEntanglementMinEnemies = Convert.ToInt32(this.defensiveMassEntanglementMinEnemiesTextBox.Text);
            Settings.TyphoonEnabled = this.defensiveTyphoonEnabledCheckBox.Checked;
            Settings.SkullBashEnabled = this.defensiveSkullBashEnabledCheckBox.Checked;
            Settings.MightyBashEnabled = this.defensiveMightyBashEnabledCheckBox.Checked;
            Settings.MaimEnabled = this.defensiveMaimEnabledCheckBox.Checked;
            Settings.MaimMinComboPoints = Convert.ToInt32(this.defensiveMaimMinComboPointsTextBox.Text);
            Settings.FaerieFireRogueEnabled = this.defensiveFaerieFireRogueCheckBox.Checked;
            Settings.FaerieFireDruidEnabled = this.defensiveFaerieFireDruidCheckBox.Checked;
            Settings.FaerieFireWarriorEnabled = this.defensiveFaerieFireWarriorCheckBox.Checked;
            Settings.FaerieFirePaladinEnabled = this.defensiveFaerieFirePaladinCheckBox.Checked;
            Settings.FaerieFireMageEnabled = this.defensiveFaerieFireMageCheckBox.Checked;
            Settings.FaerieFireMonkEnabled = this.defensiveFaerieFireMonkCheckBox.Checked;
            Settings.FaerieFireHunterEnabled = this.defensiveFaerieFireHunterCheckBox.Checked;
            Settings.FaerieFirePriestEnabled = this.defensiveFaerieFirePriestCheckBox.Checked;
            Settings.FaerieFireDeathKnightEnabled = this.defensiveFaerieFireDeathKnightCheckBox.Checked;
            Settings.FaerieFireShamanEnabled = this.defensiveFaerieFireShamanCheckBox.Checked;
            Settings.FaerieFireWarlockEnabled = this.defensiveFaerieFireWarlockCheckBox.Checked;
            Settings.BearFormPowerShiftEnabled = this.defensiveSnaresBearFormPowerShiftEnabled.Checked;
            Settings.RemoveSnareWithStampedingRoar = this.defensiveSnaresUseStampedingRoarCheckBox.Checked;
            Settings.RemoveSnareWithDash = this.defensiveSnaresUseDashCheckBox.Checked;
            Settings.SnareReactionTimeInMs = Convert.ToInt32(this.defensiveSnareReactionTimeTextBox.Text);
        }

        #region UI Events: Control Toggles

        private void defensiveSurvivalInstinctsEnabled_CheckedChanged(object sender, EventArgs e)
        {
            this.defensiveSurvivalInstinctsPanel.Enabled = (sender as CheckBox).Checked;
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
            this.SettingsForm.InterfaceElementColorToggle(sender);
        }

        private void defensiveMaimEnabledCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            this.defensiveMaimPanel.Enabled = (sender as CheckBox).Checked;
            this.SettingsForm.InterfaceElementColorToggle(sender);
        }

        private void defensiveHeartOfTheWildEnabledCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            this.defensiveHeartOfTheWildPanel.Enabled = (sender as CheckBox).Checked;
            this.SettingsForm.InterfaceElementColorToggle(sender);
        }

        private void defensiveIncapacitatingRoarEnabledCheckBox_CheckedChanged_1(object sender, EventArgs e)
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
