using System;
using System.Windows.Forms;
using Paws.Core.Managers;
using Paws.Interface.Forms;
using Styx.Helpers;

namespace Paws.Interface.Controls.Feral
{
    public partial class FeralOffensiveSettings : UserControl, ISettingsControl
    {
        public FeralOffensiveSettings(SettingsForm settingsForm)
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
            offensiveSavageRoarEnabledCheckBox.Checked = Settings.SavageRoarEnabled;
            offensiveSavageRoarEnabledCheckBox_CheckedChanged(offensiveSavageRoarEnabledCheckBox, EventArgs.Empty);
            offensiveSavageRoarAllowClippingCheckBox.Checked = Settings.SavageRoarAllowClipping;
            offensiveSavageRoarMinComboPointsTextBox.Text = Settings.SavageRoarMinComboPoints.ToString();
            offensiveTigersFuryEnabledCheckBox.Checked = Settings.TigersFuryEnabled;
            offensiveTigersFuryEnabledCheckBox_CheckedChanged(offensiveTigersFuryEnabledCheckBox, EventArgs.Empty);
            offensiveTigersFuryOnCooldownRadio.Checked = Settings.TigersFuryOnCooldown;
            offensiveTigersFurySyncWithBerserkRadio.Checked = Settings.TigersFurySyncWithBerserk;
            offensiveTigersFuryMaxEnergyRadio.Checked = Settings.TigersFuryUseMaxEnergy;
            offensiveTigersFuryMaxEnergyTextBox.Text = Settings.TigersFuryMaxEnergy.ToString("0.##");
            offensiveBerserkEnabled.Checked = Settings.BerserkEnabled;
            offensiveBerserkEnabled_CheckedChanged(offensiveBerserkEnabled, EventArgs.Empty);
            offensiveBerserkOnCooldownCheckBox.Checked = Settings.BerserkOnCooldown;
            offensiveBerserkEnemyHealthRadioButton.Checked = Settings.BerserkEnemyHealthCheck;
            offensiveBerserkEnemyHealthMultiplierTextBox.Text = Settings.BerserkEnemyHealthMultiplier.ToString("0.##");
            offensiveBerserkSurroundedByEnemiesCheckBox.Checked = Settings.BerserkSurroundedByEnemiesEnabled;
            offensiveBerserkSurroundedByEnemiesTextBox.Text = Settings.BerserkSurroundedByMinEnemies.ToString();
            offensiveIncarnationEnabledCheckBox.Checked = Settings.IncarnationEnabled;
            offensiveIncarnationEnabledCheckBox_CheckedChanged(offensiveIncarnationEnabledCheckBox, EventArgs.Empty);
            offensiveIncarnationOnCooldownCheckBox.Checked = Settings.IncarnationOnCooldown;
            offensiveIncarnationEnemyHealthRadioButton.Checked = Settings.IncarnationEnemyHealthCheck;
            offensiveIncarnationEnemyHealthMultiplierTextBox.Text =
                Settings.IncarnationEnemyHealthMultiplier.ToString("0.##");
            offensiveIncarnationSurroundedByEnemiesCheckBox.Checked = Settings.IncarnationSurroundedByEnemiesEnabled;
            offensiveIncarnationSurroundedByEnemiesTextBox.Text = Settings.IncarnationSurroundedByMinEnemies.ToString();
            offensiveMoonfireEnabledCheckBox.Checked = Settings.MoonfireEnabled;
            offensiveMoonfireEnabledCheckBox_CheckedChanged(offensiveMoonfireEnabledCheckBox, EventArgs.Empty);
            offensiveMoonfireAllowClippingCheckBox.Checked = Settings.MoonfireAllowClipping;
            offensiveMoonfireLunarInspirationOnlyCheckBox.Checked = Settings.MoonfireOnlyWithLunarInspiration;
            offensiveRakeEnabledCheckBox.Checked = Settings.RakeEnabled;
            offensiveRakeEnabledCheckBox_CheckedChanged(offensiveRakeEnabledCheckBox, EventArgs.Empty);
            offensiveRakeAllowClippingCheckBox.Checked = Settings.RakeAllowClipping;
            offensiveRakeAllowMultiplierClippingCheckBox.Checked = Settings.RakeAllowMultiplierClipping;
            offensiveRakeMaxEnemiesTextBox.Text = Settings.RakeMaxEnemies.ToString();
            offensiveRakeStealthOpenerCheckBox.Checked = Settings.RakeStealthOpener;
            offensiveShredEnabledCheckBox.Checked = Settings.ShredEnabled;
            offensiveShredEnabledCheckBox_CheckedChanged(offensiveShredEnabledCheckBox, EventArgs.Empty);
            offensiveShredStealthOpener.Checked = Settings.ShredStealthOpener;
            offensiveRipEnabledCheckBox.Checked = Settings.RipEnabled;
            offensiveRipEnabledCheckBox_CheckedChanged(offensiveRipEnabledCheckBox, EventArgs.Empty);
            offensiveRipEnemyHealthCheckBox.Checked = Settings.RipEnemyHealthCheck;
            offensiveRipEnemyHealthMultiplierTextBox.Text = Settings.RipEnemyHealthMultiplier.ToString("0.##");
            offensiveRipAllowClippingCheckBox.Checked = Settings.RipAllowClipping;
            offensiveFerociousBiteEnabledCheckBox.Checked = Settings.FerociousBiteEnabled;
            offensiveFerociousBiteEnabledCheckBox_CheckedChanged(offensiveFerociousBiteEnabledCheckBox, EventArgs.Empty);
            offensiveFerociousBiteMinEnergy.Text = Settings.FerociousBiteMinEnergy.ToString("0.##");
            offensiveThrashEnabledCheckBox.Checked = Settings.ThrashEnabled;
            offensiveThrashEnabledCheckBox_CheckedChanged(offensiveThrashEnabledCheckBox, EventArgs.Empty);
            offensiveThrashAllowClippingCheckBox.Checked = Settings.ThrashAllowClipping;
            offensiveThrashMinEnemiesTextBox.Text = Settings.ThrashMinEnemies.ToString();
            offensiveThrashClearcastingProcCheckBox.Checked = Settings.ThrashClearcastingProcEnabled;
            offensiveSwipeEnabledCheckBox.Checked = Settings.SwipeEnabled;
            offensiveSwipeEnabledCheckBox_CheckedChanged(offensiveSwipeEnabledCheckBox, EventArgs.Empty);
            offensiveSwipeMinEnemiesTextBox.Text = Settings.SwipeMinEnemies.ToString();
            offensiveForceOfNatureEnabledCheckBox.Checked = Settings.ForceOfNatureEnabled;
            offensiveForceOfNatureEnabledCheckBox_CheckedChanged(offensiveForceOfNatureEnabledCheckBox, EventArgs.Empty);
            offensiveBloodtalonsEnabledCheckBox.Checked = Settings.BloodtalonsEnabled;
            offensiveBloodtalonsEnabledCheckBox_CheckedChanged(offensiveBloodtalonsEnabledCheckBox, EventArgs.Empty);
            offensiveBloodtalonsApplyImmediatelyRadioButton.Checked = Settings.BloodtalonsApplyImmediately;
            offensiveBloodtalonsApplyToFinishersRadioButton.Checked = Settings.BloodtalonsApplyToFinishers;
        }

        public void ApplySettings()
        {
            Settings.SavageRoarEnabled = offensiveSavageRoarEnabledCheckBox.Checked;
            Settings.SavageRoarAllowClipping = offensiveSavageRoarAllowClippingCheckBox.Checked;
            Settings.SavageRoarMinComboPoints = Convert.ToInt32(offensiveSavageRoarMinComboPointsTextBox.Text);
            Settings.TigersFuryEnabled = offensiveTigersFuryEnabledCheckBox.Checked;
            Settings.TigersFuryOnCooldown = offensiveTigersFuryOnCooldownRadio.Checked;
            Settings.TigersFurySyncWithBerserk = offensiveTigersFurySyncWithBerserkRadio.Checked;
            Settings.TigersFuryUseMaxEnergy = offensiveTigersFuryMaxEnergyRadio.Checked;
            Settings.TigersFuryMaxEnergy = Convert.ToDouble(offensiveTigersFuryMaxEnergyTextBox.Text);
            Settings.BerserkEnabled = offensiveBerserkEnabled.Checked;
            Settings.BerserkOnCooldown = offensiveBerserkOnCooldownCheckBox.Checked;
            Settings.BerserkEnemyHealthCheck = offensiveBerserkEnemyHealthRadioButton.Checked;
            Settings.BerserkEnemyHealthMultiplier = offensiveBerserkEnemyHealthMultiplierTextBox.Text.ToFloat();
            Settings.BerserkSurroundedByEnemiesEnabled = offensiveBerserkSurroundedByEnemiesCheckBox.Checked;
            Settings.BerserkSurroundedByMinEnemies = Convert.ToInt32(offensiveBerserkSurroundedByEnemiesTextBox.Text);
            Settings.IncarnationEnabled = offensiveIncarnationEnabledCheckBox.Checked;
            Settings.IncarnationOnCooldown = offensiveIncarnationOnCooldownCheckBox.Checked;
            Settings.IncarnationEnemyHealthCheck = offensiveIncarnationEnemyHealthRadioButton.Checked;
            Settings.IncarnationEnemyHealthMultiplier = offensiveIncarnationEnemyHealthMultiplierTextBox.Text.ToFloat();
            Settings.IncarnationSurroundedByEnemiesEnabled = offensiveIncarnationSurroundedByEnemiesCheckBox.Checked;
            Settings.IncarnationSurroundedByMinEnemies =
                Convert.ToInt32(offensiveIncarnationSurroundedByEnemiesTextBox.Text);
            Settings.MoonfireEnabled = offensiveMoonfireEnabledCheckBox.Checked;
            Settings.MoonfireAllowClipping = offensiveMoonfireAllowClippingCheckBox.Checked;
            Settings.MoonfireOnlyWithLunarInspiration = offensiveMoonfireLunarInspirationOnlyCheckBox.Checked;
            Settings.RakeEnabled = offensiveRakeEnabledCheckBox.Checked;
            Settings.RakeAllowClipping = offensiveRakeAllowClippingCheckBox.Checked;
            Settings.RakeAllowMultiplierClipping = offensiveRakeAllowMultiplierClippingCheckBox.Checked;
            Settings.RakeMaxEnemies = Convert.ToInt32(offensiveRakeMaxEnemiesTextBox.Text);
            Settings.RakeStealthOpener = offensiveRakeStealthOpenerCheckBox.Checked;
            Settings.ShredEnabled = offensiveShredEnabledCheckBox.Checked;
            Settings.ShredStealthOpener = offensiveShredStealthOpener.Checked;
            Settings.RipEnabled = offensiveRipEnabledCheckBox.Checked;
            Settings.RipAllowClipping = offensiveRipAllowClippingCheckBox.Checked;
            Settings.RipEnemyHealthCheck = offensiveRipEnemyHealthCheckBox.Checked;
            Settings.RipEnemyHealthMultiplier = offensiveRipEnemyHealthMultiplierTextBox.Text.ToFloat();
            Settings.FerociousBiteEnabled = offensiveFerociousBiteEnabledCheckBox.Checked;
            Settings.FerociousBiteMinEnergy = Convert.ToDouble(offensiveFerociousBiteMinEnergy.Text);
            Settings.ThrashEnabled = offensiveThrashEnabledCheckBox.Checked;
            Settings.ThrashAllowClipping = offensiveThrashAllowClippingCheckBox.Checked;
            Settings.ThrashMinEnemies = Convert.ToInt32(offensiveThrashMinEnemiesTextBox.Text);
            Settings.ThrashClearcastingProcEnabled = offensiveThrashClearcastingProcCheckBox.Checked;
            Settings.SwipeEnabled = offensiveSwipeEnabledCheckBox.Checked;
            Settings.SwipeMinEnemies = Convert.ToInt32(offensiveSwipeMinEnemiesTextBox.Text);
            Settings.ForceOfNatureEnabled = offensiveForceOfNatureEnabledCheckBox.Checked;
            Settings.BloodtalonsEnabled = offensiveBloodtalonsEnabledCheckBox.Checked;
            Settings.BloodtalonsApplyImmediately = offensiveBloodtalonsApplyImmediatelyRadioButton.Checked;
            Settings.BloodtalonsApplyToFinishers = offensiveBloodtalonsApplyToFinishersRadioButton.Checked;
        }

        private void FeralMobilitySettings_Load(object sender, EventArgs e)
        {
        }

        #region UI Events: Control Toggles

        private void offensiveSavageRoarEnabledCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            var checkBox = sender as CheckBox;
            if (checkBox != null) offensiveSavageRoarPanel.Enabled = checkBox.Checked;
            SettingsForm.InterfaceElementColorToggle(sender);
        }

        private void offensiveTigersFuryEnabledCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            var checkBox = sender as CheckBox;
            if (checkBox != null) offensiveTigersFuryPanel.Enabled = checkBox.Checked;
            SettingsForm.InterfaceElementColorToggle(sender);
        }

        private void offensiveBerserkEnabled_CheckedChanged(object sender, EventArgs e)
        {
            var checkBox = sender as CheckBox;
            if (checkBox != null) offensiveBerserkPanel.Enabled = checkBox.Checked;
            SettingsForm.InterfaceElementColorToggle(sender);
        }

        private void offensiveIncarnationEnabledCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            var checkBox = sender as CheckBox;
            if (checkBox != null) offensiveIncarnationPanel.Enabled = checkBox.Checked;
            SettingsForm.InterfaceElementColorToggle(sender);
        }

        private void offensiveMoonfireEnabledCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            var checkBox = sender as CheckBox;
            if (checkBox != null) offensiveMoonfirePanel.Enabled = checkBox.Checked;
            SettingsForm.InterfaceElementColorToggle(sender);
        }

        private void offensiveRakeEnabledCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            var checkBox = sender as CheckBox;
            if (checkBox != null) offensiveRakePanel.Enabled = checkBox.Checked;
            SettingsForm.InterfaceElementColorToggle(sender);
        }

        private void offensiveShredEnabledCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            var checkBox = sender as CheckBox;
            if (checkBox != null) offensiveShredPanel.Enabled = checkBox.Checked;
            SettingsForm.InterfaceElementColorToggle(sender);
        }

        private void offensiveRipEnabledCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            var checkBox = sender as CheckBox;
            if (checkBox != null) offensiveRipPanel.Enabled = checkBox.Checked;
            SettingsForm.InterfaceElementColorToggle(sender);
        }

        private void offensiveFerociousBiteEnabledCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            var checkBox = sender as CheckBox;
            if (checkBox != null) offensiveFerociousBitePanel.Enabled = checkBox.Checked;
            SettingsForm.InterfaceElementColorToggle(sender);
        }

        private void offensiveThrashEnabledCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            var checkBox = sender as CheckBox;
            if (checkBox != null) offensiveThrashPanel.Enabled = checkBox.Checked;
            SettingsForm.InterfaceElementColorToggle(sender);
        }

        private void offensiveSwipeEnabledCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            var checkBox = sender as CheckBox;
            if (checkBox != null) offensiveSwipePanel.Enabled = checkBox.Checked;
            SettingsForm.InterfaceElementColorToggle(sender);
        }

        private void offensiveBloodtalonsEnabledCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            var checkBox = sender as CheckBox;
            if (checkBox != null) offensiveBloodtalonsPanel.Enabled = checkBox.Checked;
            SettingsForm.InterfaceElementColorToggle(sender);
        }

        private void offensiveForceOfNatureEnabledCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            SettingsForm.InterfaceElementColorToggle(sender);
        }

        #endregion
    }
}