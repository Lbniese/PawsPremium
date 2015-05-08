using Paws.Core.Managers;
using Styx.Helpers;
using System;
using System.Windows.Forms;

namespace Paws.Interface.Controls.Feral
{
    public partial class FeralOffensiveSettings : UserControl, ISettingsControl
    {
        private SettingsManager Settings { get { return SettingsManager.Instance; } }

        public SettingsForm SettingsForm { get; set; }

        public FeralOffensiveSettings(SettingsForm settingsForm)
        {
            this.SettingsForm = settingsForm;
            InitializeComponent();
        }

        private void FeralMobilitySettings_Load(object sender, EventArgs e)
        {
            
        }

        public void BindUISettings()
        {
            this.offensiveSavageRoarEnabledCheckBox.Checked = Settings.SavageRoarEnabled;
            offensiveSavageRoarEnabledCheckBox_CheckedChanged(this.offensiveSavageRoarEnabledCheckBox, EventArgs.Empty);
            this.offensiveSavageRoarAllowClippingCheckBox.Checked = Settings.SavageRoarAllowClipping;
            this.offensiveSavageRoarMinComboPointsTextBox.Text = Settings.SavageRoarMinComboPoints.ToString();
            this.offensiveTigersFuryEnabledCheckBox.Checked = Settings.TigersFuryEnabled;
            offensiveTigersFuryEnabledCheckBox_CheckedChanged(this.offensiveTigersFuryEnabledCheckBox, EventArgs.Empty);
            this.offensiveTigersFuryOnCooldownRadio.Checked = Settings.TigersFuryOnCooldown;
            this.offensiveTigersFurySyncWithBerserkRadio.Checked = Settings.TigersFurySyncWithBerserk;
            this.offensiveTigersFuryMaxEnergyRadio.Checked = Settings.TigersFuryUseMaxEnergy;
            this.offensiveTigersFuryMaxEnergyTextBox.Text = Settings.TigersFuryMaxEnergy.ToString("0.##");
            this.offensiveBerserkEnabled.Checked = Settings.BerserkEnabled;
            offensiveBerserkEnabled_CheckedChanged(this.offensiveBerserkEnabled, EventArgs.Empty);
            this.offensiveBerserkOnCooldownCheckBox.Checked = Settings.BerserkOnCooldown;
            this.offensiveBerserkEnemyHealthRadioButton.Checked = Settings.BerserkEnemyHealthCheck;
            this.offensiveBerserkEnemyHealthMultiplierTextBox.Text = Settings.BerserkEnemyHealthMultiplier.ToString("0.##");
            this.offensiveBerserkSurroundedByEnemiesCheckBox.Checked = Settings.BerserkSurroundedByEnemiesEnabled;
            this.offensiveBerserkSurroundedByEnemiesTextBox.Text = Settings.BerserkSurroundedByMinEnemies.ToString();
            this.offensiveIncarnationEnabledCheckBox.Checked = Settings.IncarnationEnabled;
            offensiveIncarnationEnabledCheckBox_CheckedChanged(this.offensiveIncarnationEnabledCheckBox, EventArgs.Empty);
            this.offensiveIncarnationOnCooldownCheckBox.Checked = Settings.IncarnationOnCooldown;
            this.offensiveIncarnationEnemyHealthRadioButton.Checked = Settings.IncarnationEnemyHealthCheck;
            this.offensiveIncarnationEnemyHealthMultiplierTextBox.Text = Settings.IncarnationEnemyHealthMultiplier.ToString("0.##");
            this.offensiveIncarnationSurroundedByEnemiesCheckBox.Checked = Settings.IncarnationSurroundedByEnemiesEnabled;
            this.offensiveIncarnationSurroundedByEnemiesTextBox.Text = Settings.IncarnationSurroundedByMinEnemies.ToString();
            this.offensiveMoonfireEnabledCheckBox.Checked = Settings.MoonfireEnabled;
            offensiveMoonfireEnabledCheckBox_CheckedChanged(this.offensiveMoonfireEnabledCheckBox, EventArgs.Empty);
            this.offensiveMoonfireAllowClippingCheckBox.Checked = Settings.MoonfireAllowClipping;
            this.offensiveMoonfireLunarInspirationOnlyCheckBox.Checked = Settings.MoonfireOnlyWithLunarInspiration;
            this.offensiveRakeEnabledCheckBox.Checked = Settings.RakeEnabled;
            offensiveRakeEnabledCheckBox_CheckedChanged(this.offensiveRakeEnabledCheckBox, EventArgs.Empty);
            this.offensiveRakeAllowClippingCheckBox.Checked = Settings.RakeAllowClipping;
            this.offensiveRakeAllowMultiplierClippingCheckBox.Checked = Settings.RakeAllowMultiplierClipping;
            this.offensiveRakeMaxEnemiesTextBox.Text = Settings.RakeMaxEnemies.ToString();            
            this.offensiveRakeStealthOpenerCheckBox.Checked = Settings.RakeStealthOpener;
            this.offensiveShredEnabledCheckBox.Checked = Settings.ShredEnabled;
            offensiveShredEnabledCheckBox_CheckedChanged(this.offensiveShredEnabledCheckBox, EventArgs.Empty);
            this.offensiveShredStealthOpener.Checked = Settings.ShredStealthOpener;
            this.offensiveRipEnabledCheckBox.Checked = Settings.RipEnabled;
            offensiveRipEnabledCheckBox_CheckedChanged(this.offensiveRipEnabledCheckBox, EventArgs.Empty);
            this.offensiveRipEnemyHealthCheckBox.Checked = Settings.RipEnemyHealthCheck;
            this.offensiveRipEnemyHealthMultiplierTextBox.Text = Settings.RipEnemyHealthMultiplier.ToString("0.##");
            this.offensiveRipAllowClippingCheckBox.Checked = Settings.RipAllowClipping;
            this.offensiveFerociousBiteEnabledCheckBox.Checked = Settings.FerociousBiteEnabled;
            offensiveFerociousBiteEnabledCheckBox_CheckedChanged(this.offensiveFerociousBiteEnabledCheckBox, EventArgs.Empty);
            this.offensiveFerociousBiteMinEnergy.Text = Settings.FerociousBiteMinEnergy.ToString("0.##");
            this.offensiveThrashEnabledCheckBox.Checked = Settings.ThrashEnabled;
            offensiveThrashEnabledCheckBox_CheckedChanged(this.offensiveThrashEnabledCheckBox, EventArgs.Empty);
            this.offensiveThrashAllowClippingCheckBox.Checked = Settings.ThrashAllowClipping;
            this.offensiveThrashMinEnemiesTextBox.Text = Settings.ThrashMinEnemies.ToString();
            this.offensiveThrashClearcastingProcCheckBox.Checked = Settings.ThrashClearcastingProcEnabled;
            this.offensiveSwipeEnabledCheckBox.Checked = Settings.SwipeEnabled;
            offensiveSwipeEnabledCheckBox_CheckedChanged(this.offensiveSwipeEnabledCheckBox, EventArgs.Empty);
            this.offensiveSwipeMinEnemiesTextBox.Text = Settings.SwipeMinEnemies.ToString();
            this.offensiveForceOfNatureEnabledCheckBox.Checked = Settings.ForceOfNatureEnabled;
            offensiveForceOfNatureEnabledCheckBox_CheckedChanged(this.offensiveForceOfNatureEnabledCheckBox, EventArgs.Empty);
            this.offensiveBloodtalonsEnabledCheckBox.Checked = Settings.BloodtalonsEnabled;
            offensiveBloodtalonsEnabledCheckBox_CheckedChanged(this.offensiveBloodtalonsEnabledCheckBox, EventArgs.Empty);
            this.offensiveBloodtalonsApplyImmediatelyRadioButton.Checked = Settings.BloodtalonsApplyImmediately;
            this.offensiveBloodtalonsApplyToFinishersRadioButton.Checked = Settings.BloodtalonsApplyToFinishers;
        }

        public void ApplySettings()
        {
            Settings.SavageRoarEnabled = this.offensiveSavageRoarEnabledCheckBox.Checked;
            Settings.SavageRoarAllowClipping = this.offensiveSavageRoarAllowClippingCheckBox.Checked;
            Settings.SavageRoarMinComboPoints = Convert.ToInt32(this.offensiveSavageRoarMinComboPointsTextBox.Text);
            Settings.TigersFuryEnabled = this.offensiveTigersFuryEnabledCheckBox.Checked;
            Settings.TigersFuryOnCooldown = this.offensiveTigersFuryOnCooldownRadio.Checked;
            Settings.TigersFurySyncWithBerserk = this.offensiveTigersFurySyncWithBerserkRadio.Checked;
            Settings.TigersFuryUseMaxEnergy = this.offensiveTigersFuryMaxEnergyRadio.Checked;
            Settings.TigersFuryMaxEnergy = Convert.ToDouble(this.offensiveTigersFuryMaxEnergyTextBox.Text);
            Settings.BerserkEnabled = this.offensiveBerserkEnabled.Checked;
            Settings.BerserkOnCooldown = this.offensiveBerserkOnCooldownCheckBox.Checked;
            Settings.BerserkEnemyHealthCheck = this.offensiveBerserkEnemyHealthRadioButton.Checked;
            Settings.BerserkEnemyHealthMultiplier = this.offensiveBerserkEnemyHealthMultiplierTextBox.Text.ToFloat();
            Settings.BerserkSurroundedByEnemiesEnabled = this.offensiveBerserkSurroundedByEnemiesCheckBox.Checked;
            Settings.BerserkSurroundedByMinEnemies = Convert.ToInt32(this.offensiveBerserkSurroundedByEnemiesTextBox.Text);
            Settings.IncarnationEnabled = this.offensiveIncarnationEnabledCheckBox.Checked;
            Settings.IncarnationOnCooldown = this.offensiveIncarnationOnCooldownCheckBox.Checked;
            Settings.IncarnationEnemyHealthCheck = this.offensiveIncarnationEnemyHealthRadioButton.Checked;
            Settings.IncarnationEnemyHealthMultiplier = this.offensiveIncarnationEnemyHealthMultiplierTextBox.Text.ToFloat();
            Settings.IncarnationSurroundedByEnemiesEnabled = this.offensiveIncarnationSurroundedByEnemiesCheckBox.Checked;
            Settings.IncarnationSurroundedByMinEnemies = Convert.ToInt32(this.offensiveIncarnationSurroundedByEnemiesTextBox.Text);
            Settings.MoonfireEnabled = this.offensiveMoonfireEnabledCheckBox.Checked;
            Settings.MoonfireAllowClipping = this.offensiveMoonfireAllowClippingCheckBox.Checked;
            Settings.MoonfireOnlyWithLunarInspiration = this.offensiveMoonfireLunarInspirationOnlyCheckBox.Checked;
            Settings.RakeEnabled = this.offensiveRakeEnabledCheckBox.Checked;
            Settings.RakeAllowClipping = this.offensiveRakeAllowClippingCheckBox.Checked;
            Settings.RakeAllowMultiplierClipping = this.offensiveRakeAllowMultiplierClippingCheckBox.Checked;
            Settings.RakeMaxEnemies = Convert.ToInt32(this.offensiveRakeMaxEnemiesTextBox.Text);
            Settings.RakeStealthOpener = this.offensiveRakeStealthOpenerCheckBox.Checked;
            Settings.ShredEnabled = this.offensiveShredEnabledCheckBox.Checked;
            Settings.ShredStealthOpener = this.offensiveShredStealthOpener.Checked;
            Settings.RipEnabled = this.offensiveRipEnabledCheckBox.Checked;
            Settings.RipAllowClipping = this.offensiveRipAllowClippingCheckBox.Checked;
            Settings.RipEnemyHealthCheck = this.offensiveRipEnemyHealthCheckBox.Checked;
            Settings.RipEnemyHealthMultiplier = this.offensiveRipEnemyHealthMultiplierTextBox.Text.ToFloat();
            Settings.FerociousBiteEnabled = this.offensiveFerociousBiteEnabledCheckBox.Checked;
            Settings.FerociousBiteMinEnergy = Convert.ToDouble(this.offensiveFerociousBiteMinEnergy.Text);
            Settings.ThrashEnabled = this.offensiveThrashEnabledCheckBox.Checked;
            Settings.ThrashAllowClipping = this.offensiveThrashAllowClippingCheckBox.Checked;
            Settings.ThrashMinEnemies = Convert.ToInt32(this.offensiveThrashMinEnemiesTextBox.Text);
            Settings.ThrashClearcastingProcEnabled = this.offensiveThrashClearcastingProcCheckBox.Checked;
            Settings.SwipeEnabled = this.offensiveSwipeEnabledCheckBox.Checked;
            Settings.SwipeMinEnemies = Convert.ToInt32(this.offensiveSwipeMinEnemiesTextBox.Text);
            Settings.ForceOfNatureEnabled = this.offensiveForceOfNatureEnabledCheckBox.Checked;
            Settings.BloodtalonsEnabled = this.offensiveBloodtalonsEnabledCheckBox.Checked;
            Settings.BloodtalonsApplyImmediately = this.offensiveBloodtalonsApplyImmediatelyRadioButton.Checked;
            Settings.BloodtalonsApplyToFinishers = this.offensiveBloodtalonsApplyToFinishersRadioButton.Checked;
        }

        #region UI Events: Control Toggles

        private void offensiveSavageRoarEnabledCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            this.offensiveSavageRoarPanel.Enabled = (sender as CheckBox).Checked;
            this.SettingsForm.InterfaceElementColorToggle(sender);
        }

        private void offensiveTigersFuryEnabledCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            this.offensiveTigersFuryPanel.Enabled = (sender as CheckBox).Checked;
            this.SettingsForm.InterfaceElementColorToggle(sender);
        }

        private void offensiveBerserkEnabled_CheckedChanged(object sender, EventArgs e)
        {
            this.offensiveBerserkPanel.Enabled = (sender as CheckBox).Checked;
            this.SettingsForm.InterfaceElementColorToggle(sender);
        }

        private void offensiveIncarnationEnabledCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            this.offensiveIncarnationPanel.Enabled = (sender as CheckBox).Checked;
            this.SettingsForm.InterfaceElementColorToggle(sender);
        }

        private void offensiveMoonfireEnabledCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            this.offensiveMoonfirePanel.Enabled = (sender as CheckBox).Checked;
            this.SettingsForm.InterfaceElementColorToggle(sender);
        }

        private void offensiveRakeEnabledCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            this.offensiveRakePanel.Enabled = (sender as CheckBox).Checked;
            this.SettingsForm.InterfaceElementColorToggle(sender);
        }

        private void offensiveShredEnabledCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            this.offensiveShredPanel.Enabled = (sender as CheckBox).Checked;
            this.SettingsForm.InterfaceElementColorToggle(sender);
        }

        private void offensiveRipEnabledCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            this.offensiveRipPanel.Enabled = (sender as CheckBox).Checked;
            this.SettingsForm.InterfaceElementColorToggle(sender);
        }

        private void offensiveFerociousBiteEnabledCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            this.offensiveFerociousBitePanel.Enabled = (sender as CheckBox).Checked;
            this.SettingsForm.InterfaceElementColorToggle(sender);
        }

        private void offensiveThrashEnabledCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            this.offensiveThrashPanel.Enabled = (sender as CheckBox).Checked;
            this.SettingsForm.InterfaceElementColorToggle(sender);
        }

        private void offensiveSwipeEnabledCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            this.offensiveSwipePanel.Enabled = (sender as CheckBox).Checked;
            this.SettingsForm.InterfaceElementColorToggle(sender);
        }

        private void offensiveBloodtalonsEnabledCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            this.offensiveBloodtalonsPanel.Enabled = (sender as CheckBox).Checked;
            this.SettingsForm.InterfaceElementColorToggle(sender);
        }

        private void offensiveForceOfNatureEnabledCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            this.SettingsForm.InterfaceElementColorToggle(sender);
        }

        #endregion
    }
}
