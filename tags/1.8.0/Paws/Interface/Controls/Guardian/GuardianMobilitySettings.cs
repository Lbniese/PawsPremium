using Paws.Core.Managers;
using System;
using System.Windows.Forms;

namespace Paws.Interface.Controls.Guardian
{
    public partial class GuardianMobilitySettings : UserControl, ISettingsControl
    {
        private SettingsManager Settings { get { return SettingsManager.Instance; } }

        public SettingsForm SettingsForm { get; set; }

        public GuardianMobilitySettings(SettingsForm settingsForm)
        {
            this.SettingsForm = settingsForm;
            InitializeComponent();
        }

        private void FeralMobilitySettings_Load(object sender, EventArgs e)
        {
            
        }

        public void BindUISettings()
        {
            this.mobilityBearFormEnabled.Checked = Settings.BearFormEnabled;
            mobilityBearFormEnabled_CheckedChanged(this.mobilityBearFormEnabled, EventArgs.Empty);
            this.mobilityBearFormOnlyDuringPullOrCombatRadioButton.Checked = Settings.BearFormOnlyDuringPullOrCombat;
            this.mobilityBearFormAlwaysRadioButton.Checked = Settings.BearFormAlways;
            this.generalBearFormDoNotOverrideCatFormCheckBox.Checked = Settings.BearFormDoNotOverrideCatForm;
            this.mobilityBearFormDoNotOverrideTravelFormCheckBox.Checked = Settings.BearFormDoNotOverrideTravelForm;

            this.mobilityWildChargeEnabledCheckBox.Checked = Settings.GuardianWildChargeEnabled;
            mobilityWildChargeEnabledCheckBox_CheckedChanged(this.mobilityWildChargeEnabledCheckBox, EventArgs.Empty);
            this.mobilityWildChargeMinDistanceTextBox.Text = Settings.GuardianWildChargeMinDistance.ToString("0.##");
            this.mobilityWildChargeMaxDistanceTextBox.Text = Settings.GuardianWildChargeMaxDistance.ToString("0.##");
            this.mobilityDisplacerBeastEnabledCheckBox.Checked = Settings.GuardianDisplacerBeastEnabled;
            mobilityDisplacerBeastEnabledCheckBox_CheckedChanged(this.mobilityDisplacerBeastEnabledCheckBox, EventArgs.Empty);
            this.mobilityDisplacerBeastMinDistanceTextBox.Text = Settings.GuardianDisplacerBeastMinDistance.ToString("0.##");
            this.mobilityDisplacerBeastMaxDistanceTextBox.Text = Settings.GuardianDisplacerBeastMaxDistance.ToString("0.##");
            this.mobilityDashEnabledCheckBox.Checked = Settings.GuardianDashEnabled;
            mobilityDashEnabledCheckBox_CheckedChanged(this.mobilityDashEnabledCheckBox, EventArgs.Empty);
            this.mobilityDashMinDistanceTextBox.Text = Settings.GuardianDashMinDistance.ToString("0.##");
            this.mobilityDashMaxDistanceTextBox.Text = Settings.GuardianDashMaxDistance.ToString("0.##");
            this.mobilityStampedingRoarEnabledCheckBox.Checked = Settings.GuardianStampedingRoarEnabled;
            mobilityStampedingRoarEnabledCheckBox_CheckedChanged(this.mobilityStampedingRoarEnabledCheckBox, EventArgs.Empty);
            this.mobilityStampedingRoarMinDistanceTextBox.Text = Settings.GuardianStampedingRoarMinDistance.ToString("0.##");
            this.mobilityStampedingRoarMaxDistanceTextBox.Text = Settings.GuardianStampedingRoarMaxDistance.ToString("0.##");
        }

        public void ApplySettings()
        {
            Settings.BearFormEnabled = this.mobilityBearFormEnabled.Checked;
            Settings.BearFormOnlyDuringPullOrCombat = this.mobilityBearFormOnlyDuringPullOrCombatRadioButton.Checked;
            Settings.BearFormAlways = this.mobilityBearFormAlwaysRadioButton.Checked;
            Settings.BearFormDoNotOverrideCatForm = this.generalBearFormDoNotOverrideCatFormCheckBox.Checked;
            Settings.BearFormDoNotOverrideTravelForm = this.mobilityBearFormDoNotOverrideTravelFormCheckBox.Checked;

            Settings.GuardianDashEnabled = this.mobilityDashEnabledCheckBox.Checked;
            Settings.GuardianDashMinDistance = Convert.ToDouble(this.mobilityDashMinDistanceTextBox.Text);
            Settings.GuardianDashMaxDistance = Convert.ToDouble(this.mobilityDashMaxDistanceTextBox.Text);
            Settings.GuardianStampedingRoarEnabled = this.mobilityStampedingRoarEnabledCheckBox.Checked;
            Settings.GuardianStampedingRoarMinDistance = Convert.ToDouble(this.mobilityStampedingRoarMinDistanceTextBox.Text);
            Settings.GuardianStampedingRoarMaxDistance = Convert.ToDouble(this.mobilityStampedingRoarMaxDistanceTextBox.Text);
            Settings.GuardianWildChargeEnabled = this.mobilityWildChargeEnabledCheckBox.Checked;
            Settings.GuardianWildChargeMinDistance = Convert.ToDouble(this.mobilityWildChargeMinDistanceTextBox.Text);
            Settings.GuardianWildChargeMaxDistance = Convert.ToDouble(this.mobilityWildChargeMaxDistanceTextBox.Text);
            Settings.GuardianDisplacerBeastEnabled = this.mobilityDisplacerBeastEnabledCheckBox.Checked;
            Settings.GuardianDisplacerBeastMinDistance = Convert.ToDouble(this.mobilityDisplacerBeastMinDistanceTextBox.Text);
            Settings.GuardianDisplacerBeastMaxDistance = Convert.ToDouble(this.mobilityDisplacerBeastMaxDistanceTextBox.Text);
            Settings.GuardianStampedingRoarEnabled = this.mobilityStampedingRoarEnabledCheckBox.Checked;
            Settings.GuardianStampedingRoarMinDistance = Convert.ToDouble(this.mobilityStampedingRoarMinDistanceTextBox.Text);
            Settings.GuardianStampedingRoarMaxDistance = Convert.ToDouble(this.mobilityStampedingRoarMaxDistanceTextBox.Text);
        }

        #region UI Events: Control Toggles

        private void mobilityBearFormEnabled_CheckedChanged(object sender, EventArgs e)
        {
            this.mobilityBearFormPanel.Enabled = (sender as CheckBox).Checked;
            this.SettingsForm.InterfaceElementColorToggle(sender);
        }

        private void mobilityDashEnabledCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            this.mobilityDashPanel.Enabled = (sender as CheckBox).Checked;
            this.SettingsForm.InterfaceElementColorToggle(sender);
        }

        private void mobilityStampedingRoarEnabledCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            this.mobilityStampedingRoarPanel.Enabled = (sender as CheckBox).Checked;
            this.SettingsForm.InterfaceElementColorToggle(sender);
        }

        private void mobilityWildChargeEnabledCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            this.mobilityWildChargePanel.Enabled = (sender as CheckBox).Checked;
            this.SettingsForm.InterfaceElementColorToggle(sender);
        }

        private void mobilityDisplacerBeastEnabledCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            this.mobilityDisplacerBeastPanel.Enabled = (sender as CheckBox).Checked;
            this.SettingsForm.InterfaceElementColorToggle(sender);
        }

        #endregion
    }
}
