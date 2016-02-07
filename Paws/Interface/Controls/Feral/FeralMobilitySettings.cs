using System;
using System.Windows.Forms;
using Paws.Core.Managers;
using Paws.Interface.Forms;

namespace Paws.Interface.Controls.Feral
{
    public partial class FeralMobilitySettings : UserControl, ISettingsControl
    {
        public FeralMobilitySettings(SettingsForm settingsForm)
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
            generalCatFormEnabled.Checked = Settings.CatFormEnabled;
            generalCatFormEnabled_CheckedChanged(generalCatFormEnabled, EventArgs.Empty);
            generalCatFormOnlyDuringPullOrCombatRadioButton.Checked = Settings.CatFormOnlyDuringPullOrCombat;
            generalCatFormAlwaysRadioButton.Checked = Settings.CatFormAlways;
            generalCatFormDoNotOverrideBearFormCheckBox.Checked = Settings.CatFormDoNotOverrideBearForm;
            generalCatFormDoNotOverrideTravelFormCheckBox.Checked = Settings.CatFormDoNotOverrideTravelForm;
            generalProwlEnabledCheckBox.Checked = Settings.ProwlEnabled;
            generalProwlEnabledCheckBox_CheckedChanged(generalProwlEnabledCheckBox, EventArgs.Empty);
            mobilityProwlAlwaysRadioButton.Checked = Settings.ProwlAlways;
            mobilityProwlOnlyDuringPullRadioButton.Checked = Settings.ProwlOnlyDuringPull;
            generalProwlMaxDistanceTextBox.Text = Settings.ProwlMaxDistance.ToString("0.##");
            mobilityWildChargeEnabledCheckBox.Checked = Settings.WildChargeEnabled;
            mobilityWildChargeEnabledCheckBox_CheckedChanged(mobilityWildChargeEnabledCheckBox, EventArgs.Empty);
            mobilityWildChargeMinDistanceTextBox.Text = Settings.WildChargeMinDistance.ToString("0.##");
            mobilityWildChargeMaxDistanceTextBox.Text = Settings.WildChargeMaxDistance.ToString("0.##");
            mobilityDisplacerBeastEnabledCheckBox.Checked = Settings.DisplacerBeastEnabled;
            mobilityDisplacerBeastEnabledCheckBox_CheckedChanged(mobilityDisplacerBeastEnabledCheckBox, EventArgs.Empty);
            mobilityDisplacerBeastMinDistanceTextBox.Text = Settings.DisplacerBeastMinDistance.ToString("0.##");
            mobilityDisplacerBeastMaxDistanceTextBox.Text = Settings.DisplacerBeastMaxDistance.ToString("0.##");
            mobilityDashEnabledCheckBox.Checked = Settings.DashEnabled;
            mobilityDashEnabledCheckBox_CheckedChanged(mobilityDashEnabledCheckBox, EventArgs.Empty);
            mobilityDashMinDistanceTextBox.Text = Settings.DashMinDistance.ToString("0.##");
            mobilityDashMaxDistanceTextBox.Text = Settings.DashMaxDistance.ToString("0.##");
            mobilityStampedingRoarEnabledCheckBox.Checked = Settings.StampedingRoarEnabled;
            mobilityStampedingRoarEnabledCheckBox_CheckedChanged(mobilityStampedingRoarEnabledCheckBox, EventArgs.Empty);
            mobilityStampedingRoarMinDistanceTextBox.Text = Settings.StampedingRoarMinDistance.ToString("0.##");
            mobilityStampedingRoarMaxDistanceTextBox.Text = Settings.StampedingRoarMaxDistance.ToString("0.##");
        }

        public void ApplySettings()
        {
            Settings.CatFormEnabled = generalCatFormEnabled.Checked;
            Settings.CatFormOnlyDuringPullOrCombat = generalCatFormOnlyDuringPullOrCombatRadioButton.Checked;
            Settings.CatFormAlways = generalCatFormAlwaysRadioButton.Checked;
            Settings.CatFormDoNotOverrideBearForm = generalCatFormDoNotOverrideBearFormCheckBox.Checked;
            Settings.CatFormDoNotOverrideTravelForm = generalCatFormDoNotOverrideTravelFormCheckBox.Checked;
            Settings.ProwlEnabled = generalProwlEnabledCheckBox.Checked;
            Settings.ProwlAlways = mobilityProwlAlwaysRadioButton.Checked;
            Settings.ProwlOnlyDuringPull = mobilityProwlOnlyDuringPullRadioButton.Checked;
            Settings.ProwlMaxDistance = Convert.ToDouble(generalProwlMaxDistanceTextBox.Text);
            Settings.DashEnabled = mobilityDashEnabledCheckBox.Checked;
            Settings.DashMinDistance = Convert.ToDouble(mobilityDashMinDistanceTextBox.Text);
            Settings.DashMaxDistance = Convert.ToDouble(mobilityDashMaxDistanceTextBox.Text);
            Settings.StampedingRoarEnabled = mobilityStampedingRoarEnabledCheckBox.Checked;
            Settings.StampedingRoarMinDistance = Convert.ToDouble(mobilityStampedingRoarMinDistanceTextBox.Text);
            Settings.StampedingRoarMaxDistance = Convert.ToDouble(mobilityStampedingRoarMaxDistanceTextBox.Text);
            Settings.WildChargeEnabled = mobilityWildChargeEnabledCheckBox.Checked;
            Settings.WildChargeMinDistance = Convert.ToDouble(mobilityWildChargeMinDistanceTextBox.Text);
            Settings.WildChargeMaxDistance = Convert.ToDouble(mobilityWildChargeMaxDistanceTextBox.Text);
            Settings.DisplacerBeastEnabled = mobilityDisplacerBeastEnabledCheckBox.Checked;
            Settings.DisplacerBeastMinDistance = Convert.ToDouble(mobilityDisplacerBeastMinDistanceTextBox.Text);
            Settings.DisplacerBeastMaxDistance = Convert.ToDouble(mobilityDisplacerBeastMaxDistanceTextBox.Text);
            Settings.StampedingRoarEnabled = mobilityStampedingRoarEnabledCheckBox.Checked;
            Settings.StampedingRoarMinDistance = Convert.ToDouble(mobilityStampedingRoarMinDistanceTextBox.Text);
            Settings.StampedingRoarMaxDistance = Convert.ToDouble(mobilityStampedingRoarMaxDistanceTextBox.Text);
        }

        private void FeralMobilitySettings_Load(object sender, EventArgs e)
        {
        }

        #region UI Events: Control Toggles

        private void generalCatFormEnabled_CheckedChanged(object sender, EventArgs e)
        {
            var checkBox = sender as CheckBox;
            if (checkBox != null) generalCatFormPanel.Enabled = checkBox.Checked;
            SettingsForm.InterfaceElementColorToggle(sender);
        }

        private void generalProwlEnabledCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            var checkBox = sender as CheckBox;
            if (checkBox != null) generalProwlPanel.Enabled = checkBox.Checked;
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