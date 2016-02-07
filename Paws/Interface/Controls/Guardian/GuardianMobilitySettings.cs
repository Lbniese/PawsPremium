using System;
using System.Windows.Forms;
using Paws.Core.Managers;
using Paws.Interface.Forms;

namespace Paws.Interface.Controls.Guardian
{
    public partial class GuardianMobilitySettings : UserControl, ISettingsControl
    {
        public GuardianMobilitySettings(SettingsForm settingsForm)
        {
            SettingsForm = settingsForm;
            InitializeComponent();
        }

        private SettingsManager Settings
        {
            get { return SettingsManager.Instance; }
        }

        public SettingsForm SettingsForm { get; set; }

        public void BindUiSettings()
        {
            mobilityBearFormEnabled.Checked = Settings.BearFormEnabled;
            mobilityBearFormEnabled_CheckedChanged(mobilityBearFormEnabled, EventArgs.Empty);
            mobilityBearFormOnlyDuringPullOrCombatRadioButton.Checked = Settings.BearFormOnlyDuringPullOrCombat;
            mobilityBearFormAlwaysRadioButton.Checked = Settings.BearFormAlways;
            generalBearFormDoNotOverrideCatFormCheckBox.Checked = Settings.BearFormDoNotOverrideCatForm;
            mobilityBearFormDoNotOverrideTravelFormCheckBox.Checked = Settings.BearFormDoNotOverrideTravelForm;

            mobilityWildChargeEnabledCheckBox.Checked = Settings.GuardianWildChargeEnabled;
            mobilityWildChargeEnabledCheckBox_CheckedChanged(mobilityWildChargeEnabledCheckBox, EventArgs.Empty);
            mobilityWildChargeMinDistanceTextBox.Text = Settings.GuardianWildChargeMinDistance.ToString("0.##");
            mobilityWildChargeMaxDistanceTextBox.Text = Settings.GuardianWildChargeMaxDistance.ToString("0.##");
            mobilityDisplacerBeastEnabledCheckBox.Checked = Settings.GuardianDisplacerBeastEnabled;
            mobilityDisplacerBeastEnabledCheckBox_CheckedChanged(mobilityDisplacerBeastEnabledCheckBox, EventArgs.Empty);
            mobilityDisplacerBeastMinDistanceTextBox.Text = Settings.GuardianDisplacerBeastMinDistance.ToString("0.##");
            mobilityDisplacerBeastMaxDistanceTextBox.Text = Settings.GuardianDisplacerBeastMaxDistance.ToString("0.##");
            mobilityDashEnabledCheckBox.Checked = Settings.GuardianDashEnabled;
            mobilityDashEnabledCheckBox_CheckedChanged(mobilityDashEnabledCheckBox, EventArgs.Empty);
            mobilityDashMinDistanceTextBox.Text = Settings.GuardianDashMinDistance.ToString("0.##");
            mobilityDashMaxDistanceTextBox.Text = Settings.GuardianDashMaxDistance.ToString("0.##");
            mobilityStampedingRoarEnabledCheckBox.Checked = Settings.GuardianStampedingRoarEnabled;
            mobilityStampedingRoarEnabledCheckBox_CheckedChanged(mobilityStampedingRoarEnabledCheckBox, EventArgs.Empty);
            mobilityStampedingRoarMinDistanceTextBox.Text = Settings.GuardianStampedingRoarMinDistance.ToString("0.##");
            mobilityStampedingRoarMaxDistanceTextBox.Text = Settings.GuardianStampedingRoarMaxDistance.ToString("0.##");
        }

        public void ApplySettings()
        {
            Settings.BearFormEnabled = mobilityBearFormEnabled.Checked;
            Settings.BearFormOnlyDuringPullOrCombat = mobilityBearFormOnlyDuringPullOrCombatRadioButton.Checked;
            Settings.BearFormAlways = mobilityBearFormAlwaysRadioButton.Checked;
            Settings.BearFormDoNotOverrideCatForm = generalBearFormDoNotOverrideCatFormCheckBox.Checked;
            Settings.BearFormDoNotOverrideTravelForm = mobilityBearFormDoNotOverrideTravelFormCheckBox.Checked;

            Settings.GuardianDashEnabled = mobilityDashEnabledCheckBox.Checked;
            Settings.GuardianDashMinDistance = Convert.ToDouble(mobilityDashMinDistanceTextBox.Text);
            Settings.GuardianDashMaxDistance = Convert.ToDouble(mobilityDashMaxDistanceTextBox.Text);
            Settings.GuardianStampedingRoarEnabled = mobilityStampedingRoarEnabledCheckBox.Checked;
            Settings.GuardianStampedingRoarMinDistance = Convert.ToDouble(mobilityStampedingRoarMinDistanceTextBox.Text);
            Settings.GuardianStampedingRoarMaxDistance = Convert.ToDouble(mobilityStampedingRoarMaxDistanceTextBox.Text);
            Settings.GuardianWildChargeEnabled = mobilityWildChargeEnabledCheckBox.Checked;
            Settings.GuardianWildChargeMinDistance = Convert.ToDouble(mobilityWildChargeMinDistanceTextBox.Text);
            Settings.GuardianWildChargeMaxDistance = Convert.ToDouble(mobilityWildChargeMaxDistanceTextBox.Text);
            Settings.GuardianDisplacerBeastEnabled = mobilityDisplacerBeastEnabledCheckBox.Checked;
            Settings.GuardianDisplacerBeastMinDistance = Convert.ToDouble(mobilityDisplacerBeastMinDistanceTextBox.Text);
            Settings.GuardianDisplacerBeastMaxDistance = Convert.ToDouble(mobilityDisplacerBeastMaxDistanceTextBox.Text);
            Settings.GuardianStampedingRoarEnabled = mobilityStampedingRoarEnabledCheckBox.Checked;
            Settings.GuardianStampedingRoarMinDistance = Convert.ToDouble(mobilityStampedingRoarMinDistanceTextBox.Text);
            Settings.GuardianStampedingRoarMaxDistance = Convert.ToDouble(mobilityStampedingRoarMaxDistanceTextBox.Text);
        }

        private void FeralMobilitySettings_Load(object sender, EventArgs e)
        {
        }

        #region UI Events: Control Toggles

        private void mobilityBearFormEnabled_CheckedChanged(object sender, EventArgs e)
        {
            var checkBox = sender as CheckBox;
            if (checkBox != null) mobilityBearFormPanel.Enabled = checkBox.Checked;
            SettingsForm.InterfaceElementColorToggle(sender);
        }

        private void mobilityDashEnabledCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            var checkBox = sender as CheckBox;
            if (checkBox != null) mobilityDashPanel.Enabled = checkBox.Checked;
            SettingsForm.InterfaceElementColorToggle(sender);
        }

        private void mobilityStampedingRoarEnabledCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            var checkBox = sender as CheckBox;
            if (checkBox != null) mobilityStampedingRoarPanel.Enabled = checkBox.Checked;
            SettingsForm.InterfaceElementColorToggle(sender);
        }

        private void mobilityWildChargeEnabledCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            var checkBox = sender as CheckBox;
            if (checkBox != null) mobilityWildChargePanel.Enabled = checkBox.Checked;
            SettingsForm.InterfaceElementColorToggle(sender);
        }

        private void mobilityDisplacerBeastEnabledCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            var checkBox = sender as CheckBox;
            if (checkBox != null) mobilityDisplacerBeastPanel.Enabled = checkBox.Checked;
            SettingsForm.InterfaceElementColorToggle(sender);
        }

        #endregion
    }
}