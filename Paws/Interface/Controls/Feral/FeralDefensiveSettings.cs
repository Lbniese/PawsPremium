using System;
using System.Windows.Forms;
using Paws.Core.Managers;
using Paws.Interface.Forms;

namespace Paws.Interface.Controls.Feral
{
    public partial class FeralDefensiveSettings : UserControl, ISettingsControl
    {
        public FeralDefensiveSettings(SettingsForm settingsForm)
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
            defensiveSurvivalInstinctsEnabled.Checked = Settings.SurvivalInstinctsEnabled;
            defensiveSurvivalInstinctsEnabled_CheckedChanged(defensiveSurvivalInstinctsEnabled, EventArgs.Empty);
            defensiveSurvivalInstinctsMinHealth.Text = Settings.SurvivalInstinctsMinHealth.ToString("0.##");
            defensiveHeartOfTheWildEnabledCheckBox.Checked = Settings.HeartOfTheWildEnabled;
            defensiveHeartOfTheWildEnabledCheckBox_CheckedChanged(defensiveHeartOfTheWildEnabledCheckBox,
                EventArgs.Empty);
            defensiveHeartOfTheWildMinHealthTextBox.Text = Settings.HeartOfTheWildMinHealth.ToString("0.##");
            defensiveIncapacitatingRoarEnabledCheckBox.Checked = Settings.IncapacitatingRoarEnabled;
            defensiveIncapacitatingRoarEnabledCheckBox_CheckedChanged_1(defensiveIncapacitatingRoarEnabledCheckBox);
            defensiveIncapacitatingRoarMinEnemiesTextBox.Text = Settings.IncapacitatingRoarMinEnemies.ToString();
            defensiveMassEntanglementEnabledCheckBox.Checked = Settings.MassEntanglementEnabled;
            defensiveMassEntanglementEnabledCheckBox_CheckedChanged(defensiveMassEntanglementEnabledCheckBox,
                EventArgs.Empty);
            defensiveMassEntanglementMinEnemiesTextBox.Text = Settings.MassEntanglementMinEnemies.ToString();
            defensiveTyphoonEnabledCheckBox.Checked = Settings.TyphoonEnabled;
            defensiveTyphoonEnabledCheckBox_CheckedChanged(defensiveTyphoonEnabledCheckBox, EventArgs.Empty);
            defensiveSkullBashEnabledCheckBox.Checked = Settings.SkullBashEnabled;
            defensiveSkullBashEnabledCheckBox_CheckedChanged(defensiveSkullBashEnabledCheckBox, EventArgs.Empty);
            defensiveMightyBashEnabledCheckBox.Checked = Settings.MightyBashEnabled;
            defensiveMightyBashEnabledCheckBox_CheckedChanged(defensiveMightyBashEnabledCheckBox, EventArgs.Empty);
            defensiveMaimEnabledCheckBox.Checked = Settings.MaimEnabled;
            defensiveMaimEnabledCheckBox_CheckedChanged(defensiveMaimEnabledCheckBox, EventArgs.Empty);
            defensiveMaimMinComboPointsTextBox.Text = Settings.MaimMinComboPoints.ToString();
            defensiveFaerieFireRogueCheckBox.Checked = Settings.FaerieFireRogueEnabled;
            defensiveFaerieFireDruidCheckBox.Checked = Settings.FaerieFireDruidEnabled;
            defensiveFaerieFireWarriorCheckBox.Checked = Settings.FaerieFireWarriorEnabled;
            defensiveFaerieFirePaladinCheckBox.Checked = Settings.FaerieFirePaladinEnabled;
            defensiveFaerieFireMageCheckBox.Checked = Settings.FaerieFireMageEnabled;
            defensiveFaerieFireMonkCheckBox.Checked = Settings.FaerieFireMonkEnabled;
            defensiveFaerieFireHunterCheckBox.Checked = Settings.FaerieFireHunterEnabled;
            defensiveFaerieFirePriestCheckBox.Checked = Settings.FaerieFirePriestEnabled;
            defensiveFaerieFireDeathKnightCheckBox.Checked = Settings.FaerieFireDeathKnightEnabled;
            defensiveFaerieFireShamanCheckBox.Checked = Settings.FaerieFireShamanEnabled;
            defensiveFaerieFireWarlockCheckBox.Checked = Settings.FaerieFireWarlockEnabled;
            defensiveSnaresBearFormPowerShiftEnabled.Checked = Settings.BearFormPowerShiftEnabled;
            defensiveSnaresUseStampedingRoarCheckBox.Checked = Settings.RemoveSnareWithStampedingRoar;
            defensiveSnaresUseDashCheckBox.Checked = Settings.RemoveSnareWithDash;
            defensiveSnareReactionTimeTextBox.Text = Settings.SnareReactionTimeInMs.ToString();
        }

        public void ApplySettings()
        {
            Settings.SurvivalInstinctsEnabled = defensiveSurvivalInstinctsEnabled.Checked;
            Settings.SurvivalInstinctsMinHealth = Convert.ToDouble(defensiveSurvivalInstinctsMinHealth.Text);
            Settings.HeartOfTheWildEnabled = defensiveHeartOfTheWildEnabledCheckBox.Checked;
            Settings.HeartOfTheWildMinHealth = Convert.ToDouble(defensiveHeartOfTheWildMinHealthTextBox.Text);
            Settings.IncapacitatingRoarEnabled = defensiveIncapacitatingRoarEnabledCheckBox.Checked;
            Settings.IncapacitatingRoarMinEnemies = Convert.ToInt32(defensiveIncapacitatingRoarMinEnemiesTextBox.Text);
            Settings.MassEntanglementEnabled = defensiveMassEntanglementEnabledCheckBox.Checked;
            Settings.MassEntanglementMinEnemies = Convert.ToInt32(defensiveMassEntanglementMinEnemiesTextBox.Text);
            Settings.TyphoonEnabled = defensiveTyphoonEnabledCheckBox.Checked;
            Settings.SkullBashEnabled = defensiveSkullBashEnabledCheckBox.Checked;
            Settings.MightyBashEnabled = defensiveMightyBashEnabledCheckBox.Checked;
            Settings.MaimEnabled = defensiveMaimEnabledCheckBox.Checked;
            Settings.MaimMinComboPoints = Convert.ToInt32(defensiveMaimMinComboPointsTextBox.Text);
            Settings.FaerieFireRogueEnabled = defensiveFaerieFireRogueCheckBox.Checked;
            Settings.FaerieFireDruidEnabled = defensiveFaerieFireDruidCheckBox.Checked;
            Settings.FaerieFireWarriorEnabled = defensiveFaerieFireWarriorCheckBox.Checked;
            Settings.FaerieFirePaladinEnabled = defensiveFaerieFirePaladinCheckBox.Checked;
            Settings.FaerieFireMageEnabled = defensiveFaerieFireMageCheckBox.Checked;
            Settings.FaerieFireMonkEnabled = defensiveFaerieFireMonkCheckBox.Checked;
            Settings.FaerieFireHunterEnabled = defensiveFaerieFireHunterCheckBox.Checked;
            Settings.FaerieFirePriestEnabled = defensiveFaerieFirePriestCheckBox.Checked;
            Settings.FaerieFireDeathKnightEnabled = defensiveFaerieFireDeathKnightCheckBox.Checked;
            Settings.FaerieFireShamanEnabled = defensiveFaerieFireShamanCheckBox.Checked;
            Settings.FaerieFireWarlockEnabled = defensiveFaerieFireWarlockCheckBox.Checked;
            Settings.BearFormPowerShiftEnabled = defensiveSnaresBearFormPowerShiftEnabled.Checked;
            Settings.RemoveSnareWithStampedingRoar = defensiveSnaresUseStampedingRoarCheckBox.Checked;
            Settings.RemoveSnareWithDash = defensiveSnaresUseDashCheckBox.Checked;
            Settings.SnareReactionTimeInMs = Convert.ToInt32(defensiveSnareReactionTimeTextBox.Text);
        }

        private void FeralMobilitySettings_Load(object sender, EventArgs e)
        {
        }

        #region UI Events: Control Toggles

        private void defensiveSurvivalInstinctsEnabled_CheckedChanged(object sender, EventArgs e)
        {
            var checkBox = sender as CheckBox;
            if (checkBox != null) defensiveSurvivalInstinctsPanel.Enabled = checkBox.Checked;
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
            SettingsForm.InterfaceElementColorToggle(sender);
        }

        private void defensiveMaimEnabledCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            var checkBox = sender as CheckBox;
            if (checkBox != null) defensiveMaimPanel.Enabled = checkBox.Checked;
            SettingsForm.InterfaceElementColorToggle(sender);
        }

        private void defensiveHeartOfTheWildEnabledCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            var checkBox = sender as CheckBox;
            if (checkBox != null) defensiveHeartOfTheWildPanel.Enabled = checkBox.Checked;
            SettingsForm.InterfaceElementColorToggle(sender);
        }

        private void defensiveIncapacitatingRoarEnabledCheckBox_CheckedChanged_1(object sender)
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