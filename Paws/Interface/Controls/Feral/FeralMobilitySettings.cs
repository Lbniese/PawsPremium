using Paws.Core.Managers;
using System;
using System.Windows.Forms;

namespace Paws.Interface.Controls.Feral
{
    public partial class FeralMobilitySettings : UserControl, ISettingsControl
    {
        private SettingsManager Settings { get { return SettingsManager.Instance; } }

        public SettingsForm SettingsForm { get; set; }

        public FeralMobilitySettings(SettingsForm settingsForm)
        {
            this.SettingsForm = settingsForm;
            InitializeComponent();
        }

        private void FeralMobilitySettings_Load(object sender, EventArgs e)
        {
            
        }

        public void BindUISettings()
        {
            this.generalCatFormEnabled.Checked = Settings.CatFormEnabled;
            generalCatFormEnabled_CheckedChanged(this.generalCatFormEnabled, EventArgs.Empty);
            this.generalCatFormOnlyDuringPullOrCombatRadioButton.Checked = Settings.CatFormOnlyDuringPullOrCombat;
            this.generalCatFormAlwaysRadioButton.Checked = Settings.CatFormAlways;
            this.generalCatFormDoNotOverrideBearFormCheckBox.Checked = Settings.CatFormDoNotOverrideBearForm;
            this.generalCatFormDoNotOverrideTravelFormCheckBox.Checked = Settings.CatFormDoNotOverrideTravelForm;
            this.generalProwlEnabledCheckBox.Checked = Settings.ProwlEnabled;
            generalProwlEnabledCheckBox_CheckedChanged(this.generalProwlEnabledCheckBox, EventArgs.Empty);
            this.mobilityProwlAlwaysRadioButton.Checked = Settings.ProwlAlways;
            this.mobilityProwlOnlyDuringPullRadioButton.Checked = Settings.ProwlOnlyDuringPull;
            this.generalProwlMaxDistanceTextBox.Text = Settings.ProwlMaxDistance.ToString("0.##");
            this.mobilityWildChargeEnabledCheckBox.Checked = Settings.WildChargeEnabled;
            mobilityWildChargeEnabledCheckBox_CheckedChanged(this.mobilityWildChargeEnabledCheckBox, EventArgs.Empty);
            this.mobilityWildChargeMinDistanceTextBox.Text = Settings.WildChargeMinDistance.ToString("0.##");
            this.mobilityWildChargeMaxDistanceTextBox.Text = Settings.WildChargeMaxDistance.ToString("0.##");
            this.mobilityDisplacerBeastEnabledCheckBox.Checked = Settings.DisplacerBeastEnabled;
            mobilityDisplacerBeastEnabledCheckBox_CheckedChanged(this.mobilityDisplacerBeastEnabledCheckBox, EventArgs.Empty);
            this.mobilityDisplacerBeastMinDistanceTextBox.Text = Settings.DisplacerBeastMinDistance.ToString("0.##");
            this.mobilityDisplacerBeastMaxDistanceTextBox.Text = Settings.DisplacerBeastMaxDistance.ToString("0.##");
            this.mobilityDashEnabledCheckBox.Checked = Settings.DashEnabled;
            mobilityDashEnabledCheckBox_CheckedChanged(this.mobilityDashEnabledCheckBox, EventArgs.Empty);
            this.mobilityDashMinDistanceTextBox.Text = Settings.DashMinDistance.ToString("0.##");
            this.mobilityDashMaxDistanceTextBox.Text = Settings.DashMaxDistance.ToString("0.##");
            this.mobilityStampedingRoarEnabledCheckBox.Checked = Settings.StampedingRoarEnabled;
            mobilityStampedingRoarEnabledCheckBox_CheckedChanged(this.mobilityStampedingRoarEnabledCheckBox, EventArgs.Empty);
            this.mobilityStampedingRoarMinDistanceTextBox.Text = Settings.StampedingRoarMinDistance.ToString("0.##");
            this.mobilityStampedingRoarMaxDistanceTextBox.Text = Settings.StampedingRoarMaxDistance.ToString("0.##");
        }

        public void ApplySettings()
        {
            Settings.CatFormEnabled = this.generalCatFormEnabled.Checked;
            Settings.CatFormOnlyDuringPullOrCombat = this.generalCatFormOnlyDuringPullOrCombatRadioButton.Checked;
            Settings.CatFormAlways = this.generalCatFormAlwaysRadioButton.Checked;
            Settings.CatFormDoNotOverrideBearForm = this.generalCatFormDoNotOverrideBearFormCheckBox.Checked;
            Settings.CatFormDoNotOverrideTravelForm = this.generalCatFormDoNotOverrideTravelFormCheckBox.Checked;
            Settings.ProwlEnabled = this.generalProwlEnabledCheckBox.Checked;
            Settings.ProwlAlways = this.mobilityProwlAlwaysRadioButton.Checked;
            Settings.ProwlOnlyDuringPull = this.mobilityProwlOnlyDuringPullRadioButton.Checked;
            Settings.ProwlMaxDistance = Convert.ToDouble(this.generalProwlMaxDistanceTextBox.Text);
            Settings.DashEnabled = this.mobilityDashEnabledCheckBox.Checked;
            Settings.DashMinDistance = Convert.ToDouble(this.mobilityDashMinDistanceTextBox.Text);
            Settings.DashMaxDistance = Convert.ToDouble(this.mobilityDashMaxDistanceTextBox.Text);
            Settings.StampedingRoarEnabled = this.mobilityStampedingRoarEnabledCheckBox.Checked;
            Settings.StampedingRoarMinDistance = Convert.ToDouble(this.mobilityStampedingRoarMinDistanceTextBox.Text);
            Settings.StampedingRoarMaxDistance = Convert.ToDouble(this.mobilityStampedingRoarMaxDistanceTextBox.Text);
            Settings.WildChargeEnabled = this.mobilityWildChargeEnabledCheckBox.Checked;
            Settings.WildChargeMinDistance = Convert.ToDouble(this.mobilityWildChargeMinDistanceTextBox.Text);
            Settings.WildChargeMaxDistance = Convert.ToDouble(this.mobilityWildChargeMaxDistanceTextBox.Text);
            Settings.DisplacerBeastEnabled = this.mobilityDisplacerBeastEnabledCheckBox.Checked;
            Settings.DisplacerBeastMinDistance = Convert.ToDouble(this.mobilityDisplacerBeastMinDistanceTextBox.Text);
            Settings.DisplacerBeastMaxDistance = Convert.ToDouble(this.mobilityDisplacerBeastMaxDistanceTextBox.Text);
            Settings.StampedingRoarEnabled = this.mobilityStampedingRoarEnabledCheckBox.Checked;
            Settings.StampedingRoarMinDistance = Convert.ToDouble(this.mobilityStampedingRoarMinDistanceTextBox.Text);
            Settings.StampedingRoarMaxDistance = Convert.ToDouble(this.mobilityStampedingRoarMaxDistanceTextBox.Text);
        }

        #region UI Events: Control Toggles

        private void generalCatFormEnabled_CheckedChanged(object sender, EventArgs e)
        {
            this.generalCatFormPanel.Enabled = (sender as CheckBox).Checked;
            this.SettingsForm.InterfaceElementColorToggle(sender);
        }

        private void generalProwlEnabledCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            this.generalProwlPanel.Enabled = (sender as CheckBox).Checked;
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
