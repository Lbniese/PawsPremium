using Paws.Core.Managers;
using Styx.Helpers;
using System;
using System.Windows.Forms;

namespace Paws.Interface.Controls.Feral
{
    public partial class FeralHealingSettings : UserControl, ISettingsControl
    {
        private SettingsManager Settings { get { return SettingsManager.Instance; } }

        public SettingsForm SettingsForm { get; set; }

        public FeralHealingSettings(SettingsForm settingsForm)
        {
            this.SettingsForm = settingsForm;
            InitializeComponent();
        }

        private void FeralMobilitySettings_Load(object sender, EventArgs e)
        {
            
        }

        public void BindUISettings()
        {
            this.healingRejuvenationEnabledCheckBox.Checked = Settings.RejuvenationEnabled;
            healingRejuvenationEnabledCheckBox_CheckedChanged(this.healingRejuvenationEnabledCheckBox, EventArgs.Empty);
            this.healingRejuvenationMinHealthTextBox.Text = Settings.RejuvenationMinHealth.ToString("0.##");
            this.healingHealingTouchEnabledCheckBox.Checked = Settings.HealingTouchEnabled;
            healingHealingTouchEnabledCheckBox_CheckedChanged(this.healingHealingTouchEnabledCheckBox, EventArgs.Empty);
            this.healingHealingTouchPredatorySwiftnessProcCheckBox.Checked = Settings.HealingTouchOnlyDuringPredatorySwiftness;
            this.healingHealingTouchMinHealthTextBox.Text = Settings.HealingTouchMinHealth.ToString("0.##");
            this.healingRenewalEnabledCheckBox.Checked = Settings.RenewalEnabled;
            healingRenewalEnabledCheckBox_CheckedChanged(this.healingRenewalEnabledCheckBox, EventArgs.Empty);
            this.healingRenewalMinHealthTextBox.Text = Settings.RenewalMinHealth.ToString("0.##");
            this.healingHealthstoneEnabledCheckBox.Checked = Settings.HealthstoneEnabled;
            healingHealthstoneEnabledCheckBox_CheckedChanged(this.healingHealthstoneEnabledCheckBox, EventArgs.Empty);
            this.healingHealthstoneMinHealthTextBox.Text = Settings.HealthstoneMinHealth.ToString("0.##");
            this.healingRebirthOnlyDuringPredatorySwiftnessCheckBox.Checked = Settings.RebirthOnlyDuringPredatorySwiftness;
            this.healingRebirthAnyAllyCheckBox.Checked = Settings.RebirthAnyAlly;
            healingRebirthAnyAllyCheckBox_CheckedChanged(this.healingRebirthAnyAllyCheckBox, EventArgs.Empty);
            this.healingRebirthTankCheckBox.Checked = Settings.RebirthTank;
            this.healingRebirthHealerCheckBox.Checked = Settings.RebirthHealer;
            this.healingRebirthDPSCheckBox.Checked = Settings.RebirthDPS;
            this.healingHealMyAlliesEnabledCheckBox.Checked = Settings.HealMyAlliesEnabled;
            healingHealMyAlliesEnabledCheckBox_CheckedChanged(this.healingHealMyAlliesEnabledCheckBox, EventArgs.Empty);
            this.healingHealMyAlliesMyHealthCheckEnabledCheckBox.Checked = Settings.HealMyAlliesMyHealthCheckEnabled;
            this.healingHealMyAlliesMyHealthMinTextBox.Text = Settings.HealMyAlliesMyMinHealth.ToString("0.##");
            this.healingHealMyAlliesHealingTouchEnabledCheckBox.Checked = Settings.HealMyAlliesWithHealingTouchEnabled;
            this.healingHealMyAlliesHealingTouchOnlyDuringPredatorySwiftnessCheckBox.Checked = Settings.HealMyAlliesWithHealingTouchOnlyDuringPSEnabled;
            this.healingHealMyAlliesHealingTouchMinHealthTextBox.Text = Settings.HealMyAlliesWithHealingTouchMinHealth.ToString("0.##");
            this.healingHealMyAlliesRejuvenationEnabledCheckBox.Checked = Settings.HealMyAlliesWithRejuvenationEnabled;
            this.healingHealMyAlliesRejuvenationMinHealthTextBox.Text = Settings.HealMyAlliesWithRejuvenationMinHealth.ToString("0.##");
            this.healingHealMyAlliesRejuvenationMaxAlliesTextBox.Text = Settings.HealMyAlliesWithRejuvenationMaxAllies.ToString("0.##");
            this.healingHealMyAlliesAnyAllyCheckBox.Checked = Settings.HealMyAlliesAnyAlly;
            healingHealMyAlliesAnyAllyCheckBox_CheckedChanged(this.healingHealMyAlliesAnyAllyCheckBox, EventArgs.Empty);
            this.healingHealMyAlliesTankCheckBox.Checked = Settings.HealMyAlliesTank;
            this.healingHealMyAlliesHealerCheckBox.Checked = Settings.HealMyAlliesHealer;
            this.healingHealMyAlliesDPSCheckBox.Checked = Settings.HealMyAlliesDPS;
            this.healingHealMyAlliesApplyWeightsToRolesEnabledCheckBox.Checked = Settings.HealMyAlliesApplyWeightsEnabled;
            this.healingHealMyAlliesApplyWeightToTankTrackBar.Value = ConvertHealthWeightToTrackBarValue(Settings.HealMyAlliesTankWeight);
            this.healingHealMyAlliesApplyWeightToHealerTrackBar.Value = ConvertHealthWeightToTrackBarValue(Settings.HealMyAlliesHealerWeight);
            this.healingHealMyAlliesApplyWeightToDPSTrackBar.Value = ConvertHealthWeightToTrackBarValue(Settings.HealMyAlliesDPSWeight);
            this.healingCenarionWardEnabledCheckBox.Checked = Settings.CenarionWardEnabled;
            healingCenarionWardEnabledCheckBox_CheckedChanged(this.healingCenarionWardEnabledCheckBox, EventArgs.Empty);
            this.healingCenarionWardMinHealthTextBox.Text = Settings.CenarionWardMinHealth.ToString("0.##");
            this.healingHeartOfTheWildSyncWithCenarionWardCheckBox.Checked = Settings.HeartOfTheWildSyncWithCenarionWard;
            this.healingNaturesVigilEnabledCheckBox.Checked = Settings.NaturesVigilEnabled;
            healingNaturesVigilEnabledCheckBox_CheckedChanged(this.healingNaturesVigilEnabledCheckBox, EventArgs.Empty);
            this.healingNaturesVigilMinHealthTextBox.Text = Settings.NaturesVigilMinHealth.ToString("0.##");
        }

        public void ApplySettings()
        {
            Settings.RejuvenationEnabled = this.healingRejuvenationEnabledCheckBox.Checked;
            Settings.RejuvenationMinHealth = Convert.ToDouble(this.healingRejuvenationMinHealthTextBox.Text);
            Settings.HealingTouchEnabled = this.healingHealingTouchEnabledCheckBox.Checked;
            Settings.HealingTouchOnlyDuringPredatorySwiftness = this.healingHealingTouchPredatorySwiftnessProcCheckBox.Checked;
            Settings.HealingTouchMinHealth = Convert.ToDouble(this.healingHealingTouchMinHealthTextBox.Text);
            Settings.RenewalEnabled = this.healingRenewalEnabledCheckBox.Checked;
            Settings.RenewalMinHealth = Convert.ToDouble(this.healingRenewalMinHealthTextBox.Text);
            Settings.HealthstoneEnabled = this.healingHealthstoneEnabledCheckBox.Checked;
            Settings.HealthstoneMinHealth = Convert.ToDouble(this.healingHealthstoneMinHealthTextBox.Text);
            Settings.RebirthOnlyDuringPredatorySwiftness = this.healingRebirthOnlyDuringPredatorySwiftnessCheckBox.Checked;
            Settings.RebirthAnyAlly = this.healingRebirthAnyAllyCheckBox.Checked;
            Settings.RebirthTank = this.healingRebirthTankCheckBox.Checked;
            Settings.RebirthHealer = this.healingRebirthHealerCheckBox.Checked;
            Settings.RebirthDPS = this.healingRebirthDPSCheckBox.Checked;
            Settings.HealMyAlliesEnabled = this.healingHealMyAlliesEnabledCheckBox.Checked;
            Settings.HealMyAlliesMyHealthCheckEnabled = this.healingHealMyAlliesMyHealthCheckEnabledCheckBox.Checked;
            Settings.HealMyAlliesMyMinHealth = Convert.ToDouble(this.healingHealMyAlliesMyHealthMinTextBox.Text);
            Settings.HealMyAlliesWithHealingTouchEnabled = this.healingHealMyAlliesHealingTouchEnabledCheckBox.Checked;
            Settings.HealMyAlliesWithHealingTouchOnlyDuringPSEnabled = this.healingHealMyAlliesHealingTouchOnlyDuringPredatorySwiftnessCheckBox.Checked;
            Settings.HealMyAlliesWithHealingTouchMinHealth = Convert.ToDouble(this.healingHealMyAlliesHealingTouchMinHealthTextBox.Text);
            Settings.HealMyAlliesWithRejuvenationEnabled = this.healingHealMyAlliesRejuvenationEnabledCheckBox.Checked;
            Settings.HealMyAlliesWithRejuvenationMinHealth = Convert.ToDouble(this.healingHealMyAlliesRejuvenationMinHealthTextBox.Text);
            Settings.HealMyAlliesWithRejuvenationMaxAllies = Convert.ToInt32(this.healingHealMyAlliesRejuvenationMaxAlliesTextBox.Text);
            Settings.HealMyAlliesAnyAlly = this.healingHealMyAlliesAnyAllyCheckBox.Checked;
            Settings.HealMyAlliesTank = this.healingHealMyAlliesTankCheckBox.Checked;
            Settings.HealMyAlliesHealer = this.healingHealMyAlliesHealerCheckBox.Checked;
            Settings.HealMyAlliesDPS = this.healingHealMyAlliesDPSCheckBox.Checked;
            Settings.HealMyAlliesApplyWeightsEnabled = this.healingHealMyAlliesApplyWeightsToRolesEnabledCheckBox.Checked;
            Settings.HealMyAlliesTankWeight = this.healingHealMyAlliesApplyWeightToTankLabel.Text.ToFloat();
            Settings.HealMyAlliesHealerWeight = this.healingHealMyAlliesApplyWeightToHealerLabel.Text.ToFloat();
            Settings.HealMyAlliesDPSWeight = this.healingHealMyAlliesApplyWeightToDPSLabel.Text.ToFloat();
            Settings.CenarionWardEnabled = this.healingCenarionWardEnabledCheckBox.Checked;
            Settings.CenarionWardMinHealth = Convert.ToDouble(this.healingCenarionWardMinHealthTextBox.Text);
            Settings.HeartOfTheWildSyncWithCenarionWard = this.healingHeartOfTheWildSyncWithCenarionWardCheckBox.Checked;
            Settings.NaturesVigilEnabled = this.healingNaturesVigilEnabledCheckBox.Checked;
            Settings.NaturesVigilMinHealth = Convert.ToDouble(this.healingNaturesVigilMinHealthTextBox.Text);
        }

        #region UI Events: Control Toggles

        private void healingRejuvenationEnabledCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            this.healingRejuvenationPanel.Enabled = (sender as CheckBox).Checked;
            this.SettingsForm.InterfaceElementColorToggle(sender);
        }

        private void healingHealingTouchEnabledCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            this.healingHealingTouchPanel.Enabled = (sender as CheckBox).Checked;
            this.SettingsForm.InterfaceElementColorToggle(sender);
        }

        private void healingRenewalEnabledCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            this.healingRenewalPanel.Enabled = (sender as CheckBox).Checked;
            this.SettingsForm.InterfaceElementColorToggle(sender);
        }

        private void healingCenarionWardEnabledCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            this.healingCenarionWardPanel.Enabled = (sender as CheckBox).Checked;
            this.SettingsForm.InterfaceElementColorToggle(sender);
        }

        private void healingNaturesVigilEnabledCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            this.healingNaturesVigilPanel.Enabled = (sender as CheckBox).Checked;
            this.SettingsForm.InterfaceElementColorToggle(sender);
        }

        private void healingHealthstoneEnabledCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            this.healingHealthstonePanel.Enabled = (sender as CheckBox).Checked;
            this.SettingsForm.InterfaceElementColorToggle(sender);
        }

        private void healingHealMyAlliesEnabledCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            this.healingHealMyAlliesPanel.Enabled = (sender as CheckBox).Checked;
            this.SettingsForm.InterfaceElementColorToggle(sender);
        }

        private void healingRebirthAnyAllyCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (this.healingRebirthAnyAllyCheckBox.Checked)
            {
                this.healingRebirthTankCheckBox.Checked = true;
                this.healingRebirthHealerCheckBox.Checked = true;
                this.healingRebirthDPSCheckBox.Checked = true;

                this.healingRebirthTankCheckBox.Enabled = false;
                this.healingRebirthHealerCheckBox.Enabled = false;
                this.healingRebirthDPSCheckBox.Enabled = false;
            }
            else
            {
                this.healingRebirthTankCheckBox.Enabled = true;
                this.healingRebirthTankCheckBox.Enabled = true;
                this.healingRebirthDPSCheckBox.Enabled = true;
            }
        }

        private void healingHealMyAlliesAnyAllyCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (this.healingHealMyAlliesAnyAllyCheckBox.Checked)
            {
                this.healingHealMyAlliesTankCheckBox.Checked = true;
                this.healingHealMyAlliesHealerCheckBox.Checked = true;
                this.healingHealMyAlliesDPSCheckBox.Checked = true;

                this.healingHealMyAlliesTankCheckBox.Enabled = false;
                this.healingHealMyAlliesHealerCheckBox.Enabled = false;
                this.healingHealMyAlliesDPSCheckBox.Enabled = false;
            }
            else
            {
                this.healingHealMyAlliesTankCheckBox.Enabled = true;
                this.healingHealMyAlliesHealerCheckBox.Enabled = true;
                this.healingHealMyAlliesDPSCheckBox.Enabled = true;
            }
        }

        private void healingHealMyAlliesApplyWeightToTankTrackBar_ValueChanged(object sender, EventArgs e)
        {
            SetWeightToLabel(this.healingHealMyAlliesApplyWeightToTankTrackBar, this.healingHealMyAlliesApplyWeightToTankLabel);
        }

        private void healingHealMyAlliesApplyWeightToHealerTrackBar_ValueChanged(object sender, EventArgs e)
        {
            SetWeightToLabel(this.healingHealMyAlliesApplyWeightToHealerTrackBar, this.healingHealMyAlliesApplyWeightToHealerLabel);
        }

        private void healingHealMyAlliesApplyWeightToDPSTrackBar_ValueChanged(object sender, EventArgs e)
        {
            SetWeightToLabel(this.healingHealMyAlliesApplyWeightToDPSTrackBar, this.healingHealMyAlliesApplyWeightToDPSLabel);
        }

        private void SetWeightToLabel(TrackBar trackBar, Label label)
        {
            label.Text = string.Format("{0}.{1}", (trackBar.Value >= 10 ? "2" : "1"), (trackBar.Value >= 10 ? "0" : trackBar.Value.ToString()));
        }

        private void healingHealMyAlliesApplyWeightsToRolesEnabledCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            this.healingHealMyAlliesApplyWeightToTankTrackBar.Enabled = this.healingHealMyAlliesApplyWeightsToRolesEnabledCheckBox.Checked;
            this.healingHealMyAlliesApplyWeightToHealerTrackBar.Enabled = this.healingHealMyAlliesApplyWeightsToRolesEnabledCheckBox.Checked;
            this.healingHealMyAlliesApplyWeightToDPSTrackBar.Enabled = this.healingHealMyAlliesApplyWeightsToRolesEnabledCheckBox.Checked;

            this.healingHealMyAlliesApplyWeightToTankLabel.Enabled = this.healingHealMyAlliesApplyWeightsToRolesEnabledCheckBox.Checked;
            this.healingHealMyAlliesApplyWeightToHealerLabel.Enabled = this.healingHealMyAlliesApplyWeightsToRolesEnabledCheckBox.Checked;
            this.healingHealMyAlliesApplyWeightToDPSLabel.Enabled = this.healingHealMyAlliesApplyWeightsToRolesEnabledCheckBox.Checked;
        }

        #endregion

        private int ConvertHealthWeightToTrackBarValue(float weight)
        {
            var weightString = weight.ToString();
            var split = weightString.Split('.');

            if (split[0] == "2") return 10;
            if (split.Length > 1) return Convert.ToInt32(split[1]);

            return 0;
        }
    }
}
