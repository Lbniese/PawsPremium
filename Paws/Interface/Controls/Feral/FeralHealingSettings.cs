using System;
using System.Globalization;
using System.Windows.Forms;
using Paws.Core.Managers;
using Paws.Interface.Forms;
using Styx.Helpers;

namespace Paws.Interface.Controls.Feral
{
    public partial class FeralHealingSettings : UserControl, ISettingsControl
    {
        public FeralHealingSettings(SettingsForm settingsForm)
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
            healingRejuvenationEnabledCheckBox.Checked = Settings.RejuvenationEnabled;
            healingRejuvenationEnabledCheckBox_CheckedChanged(healingRejuvenationEnabledCheckBox, EventArgs.Empty);
            healingRejuvenationMinHealthTextBox.Text = Settings.RejuvenationMinHealth.ToString("0.##");
            healingHealingTouchEnabledCheckBox.Checked = Settings.HealingTouchEnabled;
            healingHealingTouchEnabledCheckBox_CheckedChanged(healingHealingTouchEnabledCheckBox, EventArgs.Empty);
            healingHealingTouchPredatorySwiftnessProcCheckBox.Checked =
                Settings.HealingTouchOnlyDuringPredatorySwiftness;
            healingHealingTouchMinHealthTextBox.Text = Settings.HealingTouchMinHealth.ToString("0.##");
            healingRenewalEnabledCheckBox.Checked = Settings.RenewalEnabled;
            healingRenewalEnabledCheckBox_CheckedChanged(healingRenewalEnabledCheckBox, EventArgs.Empty);
            healingRenewalMinHealthTextBox.Text = Settings.RenewalMinHealth.ToString("0.##");
            healingHealthstoneEnabledCheckBox.Checked = Settings.HealthstoneEnabled;
            healingHealthstoneEnabledCheckBox_CheckedChanged(healingHealthstoneEnabledCheckBox, EventArgs.Empty);
            healingHealthstoneMinHealthTextBox.Text = Settings.HealthstoneMinHealth.ToString("0.##");
            healingRebirthOnlyDuringPredatorySwiftnessCheckBox.Checked = Settings.RebirthOnlyDuringPredatorySwiftness;
            healingRebirthAnyAllyCheckBox.Checked = Settings.RebirthAnyAlly;
            healingRebirthAnyAllyCheckBox_CheckedChanged(healingRebirthAnyAllyCheckBox, EventArgs.Empty);
            healingRebirthTankCheckBox.Checked = Settings.RebirthTank;
            healingRebirthHealerCheckBox.Checked = Settings.RebirthHealer;
            healingRebirthDPSCheckBox.Checked = Settings.RebirthDps;
            healingHealMyAlliesEnabledCheckBox.Checked = Settings.HealMyAlliesEnabled;
            healingHealMyAlliesEnabledCheckBox_CheckedChanged(healingHealMyAlliesEnabledCheckBox, EventArgs.Empty);
            healingHealMyAlliesMyHealthCheckEnabledCheckBox.Checked = Settings.HealMyAlliesMyHealthCheckEnabled;
            healingHealMyAlliesMyHealthMinTextBox.Text = Settings.HealMyAlliesMyMinHealth.ToString("0.##");
            healingHealMyAlliesHealingTouchEnabledCheckBox.Checked = Settings.HealMyAlliesWithHealingTouchEnabled;
            healingHealMyAlliesHealingTouchOnlyDuringPredatorySwiftnessCheckBox.Checked =
                Settings.HealMyAlliesWithHealingTouchOnlyDuringPsEnabled;
            healingHealMyAlliesHealingTouchMinHealthTextBox.Text =
                Settings.HealMyAlliesWithHealingTouchMinHealth.ToString("0.##");
            healingHealMyAlliesRejuvenationEnabledCheckBox.Checked = Settings.HealMyAlliesWithRejuvenationEnabled;
            healingHealMyAlliesRejuvenationMinHealthTextBox.Text =
                Settings.HealMyAlliesWithRejuvenationMinHealth.ToString("0.##");
            healingHealMyAlliesRejuvenationMaxAlliesTextBox.Text =
                Settings.HealMyAlliesWithRejuvenationMaxAllies.ToString("0.##");
            healingHealMyAlliesAnyAllyCheckBox.Checked = Settings.HealMyAlliesAnyAlly;
            healingHealMyAlliesAnyAllyCheckBox_CheckedChanged(healingHealMyAlliesAnyAllyCheckBox, EventArgs.Empty);
            healingHealMyAlliesTankCheckBox.Checked = Settings.HealMyAlliesTank;
            healingHealMyAlliesHealerCheckBox.Checked = Settings.HealMyAlliesHealer;
            healingHealMyAlliesDPSCheckBox.Checked = Settings.HealMyAlliesDps;
            healingHealMyAlliesApplyWeightsToRolesEnabledCheckBox.Checked = Settings.HealMyAlliesApplyWeightsEnabled;
            healingHealMyAlliesApplyWeightToTankTrackBar.Value =
                ConvertHealthWeightToTrackBarValue(Settings.HealMyAlliesTankWeight);
            healingHealMyAlliesApplyWeightToHealerTrackBar.Value =
                ConvertHealthWeightToTrackBarValue(Settings.HealMyAlliesHealerWeight);
            healingHealMyAlliesApplyWeightToDPSTrackBar.Value =
                ConvertHealthWeightToTrackBarValue(Settings.HealMyAlliesDpsWeight);
            healingCenarionWardEnabledCheckBox.Checked = Settings.CenarionWardEnabled;
            healingCenarionWardEnabledCheckBox_CheckedChanged(healingCenarionWardEnabledCheckBox, EventArgs.Empty);
            healingCenarionWardMinHealthTextBox.Text = Settings.CenarionWardMinHealth.ToString("0.##");
            healingHeartOfTheWildSyncWithCenarionWardCheckBox.Checked = Settings.HeartOfTheWildSyncWithCenarionWard;
            healingNaturesVigilEnabledCheckBox.Checked = Settings.NaturesVigilEnabled;
            healingNaturesVigilEnabledCheckBox_CheckedChanged(healingNaturesVigilEnabledCheckBox, EventArgs.Empty);
            healingNaturesVigilMinHealthTextBox.Text = Settings.NaturesVigilMinHealth.ToString("0.##");
        }

        public void ApplySettings()
        {
            Settings.RejuvenationEnabled = healingRejuvenationEnabledCheckBox.Checked;
            Settings.RejuvenationMinHealth = Convert.ToDouble(healingRejuvenationMinHealthTextBox.Text);
            Settings.HealingTouchEnabled = healingHealingTouchEnabledCheckBox.Checked;
            Settings.HealingTouchOnlyDuringPredatorySwiftness =
                healingHealingTouchPredatorySwiftnessProcCheckBox.Checked;
            Settings.HealingTouchMinHealth = Convert.ToDouble(healingHealingTouchMinHealthTextBox.Text);
            Settings.RenewalEnabled = healingRenewalEnabledCheckBox.Checked;
            Settings.RenewalMinHealth = Convert.ToDouble(healingRenewalMinHealthTextBox.Text);
            Settings.HealthstoneEnabled = healingHealthstoneEnabledCheckBox.Checked;
            Settings.HealthstoneMinHealth = Convert.ToDouble(healingHealthstoneMinHealthTextBox.Text);
            Settings.RebirthOnlyDuringPredatorySwiftness = healingRebirthOnlyDuringPredatorySwiftnessCheckBox.Checked;
            Settings.RebirthAnyAlly = healingRebirthAnyAllyCheckBox.Checked;
            Settings.RebirthTank = healingRebirthTankCheckBox.Checked;
            Settings.RebirthHealer = healingRebirthHealerCheckBox.Checked;
            Settings.RebirthDps = healingRebirthDPSCheckBox.Checked;
            Settings.HealMyAlliesEnabled = healingHealMyAlliesEnabledCheckBox.Checked;
            Settings.HealMyAlliesMyHealthCheckEnabled = healingHealMyAlliesMyHealthCheckEnabledCheckBox.Checked;
            Settings.HealMyAlliesMyMinHealth = Convert.ToDouble(healingHealMyAlliesMyHealthMinTextBox.Text);
            Settings.HealMyAlliesWithHealingTouchEnabled = healingHealMyAlliesHealingTouchEnabledCheckBox.Checked;
            Settings.HealMyAlliesWithHealingTouchOnlyDuringPsEnabled =
                healingHealMyAlliesHealingTouchOnlyDuringPredatorySwiftnessCheckBox.Checked;
            Settings.HealMyAlliesWithHealingTouchMinHealth =
                Convert.ToDouble(healingHealMyAlliesHealingTouchMinHealthTextBox.Text);
            Settings.HealMyAlliesWithRejuvenationEnabled = healingHealMyAlliesRejuvenationEnabledCheckBox.Checked;
            Settings.HealMyAlliesWithRejuvenationMinHealth =
                Convert.ToDouble(healingHealMyAlliesRejuvenationMinHealthTextBox.Text);
            Settings.HealMyAlliesWithRejuvenationMaxAllies =
                Convert.ToInt32(healingHealMyAlliesRejuvenationMaxAlliesTextBox.Text);
            Settings.HealMyAlliesAnyAlly = healingHealMyAlliesAnyAllyCheckBox.Checked;
            Settings.HealMyAlliesTank = healingHealMyAlliesTankCheckBox.Checked;
            Settings.HealMyAlliesHealer = healingHealMyAlliesHealerCheckBox.Checked;
            Settings.HealMyAlliesDps = healingHealMyAlliesDPSCheckBox.Checked;
            Settings.HealMyAlliesApplyWeightsEnabled = healingHealMyAlliesApplyWeightsToRolesEnabledCheckBox.Checked;
            Settings.HealMyAlliesTankWeight = healingHealMyAlliesApplyWeightToTankLabel.Text.ToFloat();
            Settings.HealMyAlliesHealerWeight = healingHealMyAlliesApplyWeightToHealerLabel.Text.ToFloat();
            Settings.HealMyAlliesDpsWeight = healingHealMyAlliesApplyWeightToDPSLabel.Text.ToFloat();
            Settings.CenarionWardEnabled = healingCenarionWardEnabledCheckBox.Checked;
            Settings.CenarionWardMinHealth = Convert.ToDouble(healingCenarionWardMinHealthTextBox.Text);
            Settings.HeartOfTheWildSyncWithCenarionWard = healingHeartOfTheWildSyncWithCenarionWardCheckBox.Checked;
            Settings.NaturesVigilEnabled = healingNaturesVigilEnabledCheckBox.Checked;
            Settings.NaturesVigilMinHealth = Convert.ToDouble(healingNaturesVigilMinHealthTextBox.Text);
        }

        private void FeralMobilitySettings_Load(object sender, EventArgs e)
        {
        }

        private static int ConvertHealthWeightToTrackBarValue(float weight)
        {
            var weightString = weight.ToString(CultureInfo.InvariantCulture);
            var split = weightString.Split('.');

            if (split[0] == "2") return 10;
            return split.Length > 1 ? Convert.ToInt32(split[1]) : 0;
        }

        #region UI Events: Control Toggles

        private void healingRejuvenationEnabledCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            var checkBox = sender as CheckBox;
            if (checkBox != null) healingRejuvenationPanel.Enabled = checkBox.Checked;
            SettingsForm.InterfaceElementColorToggle(sender);
        }

        private void healingHealingTouchEnabledCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            var checkBox = sender as CheckBox;
            if (checkBox != null) healingHealingTouchPanel.Enabled = checkBox.Checked;
            SettingsForm.InterfaceElementColorToggle(sender);
        }

        private void healingRenewalEnabledCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            var checkBox = sender as CheckBox;
            if (checkBox != null) healingRenewalPanel.Enabled = checkBox.Checked;
            SettingsForm.InterfaceElementColorToggle(sender);
        }

        private void healingCenarionWardEnabledCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            var checkBox = sender as CheckBox;
            if (checkBox != null) healingCenarionWardPanel.Enabled = checkBox.Checked;
            SettingsForm.InterfaceElementColorToggle(sender);
        }

        private void healingNaturesVigilEnabledCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            var checkBox = sender as CheckBox;
            if (checkBox != null) healingNaturesVigilPanel.Enabled = checkBox.Checked;
            SettingsForm.InterfaceElementColorToggle(sender);
        }

        private void healingHealthstoneEnabledCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            var checkBox = sender as CheckBox;
            if (checkBox != null) healingHealthstonePanel.Enabled = checkBox.Checked;
            SettingsForm.InterfaceElementColorToggle(sender);
        }

        private void healingHealMyAlliesEnabledCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            var checkBox = sender as CheckBox;
            if (checkBox != null) healingHealMyAlliesPanel.Enabled = checkBox.Checked;
            SettingsForm.InterfaceElementColorToggle(sender);
        }

        private void healingRebirthAnyAllyCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (healingRebirthAnyAllyCheckBox.Checked)
            {
                healingRebirthTankCheckBox.Checked = true;
                healingRebirthHealerCheckBox.Checked = true;
                healingRebirthDPSCheckBox.Checked = true;

                healingRebirthTankCheckBox.Enabled = false;
                healingRebirthHealerCheckBox.Enabled = false;
                healingRebirthDPSCheckBox.Enabled = false;
            }
            else
            {
                healingRebirthTankCheckBox.Enabled = true;
                healingRebirthTankCheckBox.Enabled = true;
                healingRebirthDPSCheckBox.Enabled = true;
            }
        }

        private void healingHealMyAlliesAnyAllyCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (healingHealMyAlliesAnyAllyCheckBox.Checked)
            {
                healingHealMyAlliesTankCheckBox.Checked = true;
                healingHealMyAlliesHealerCheckBox.Checked = true;
                healingHealMyAlliesDPSCheckBox.Checked = true;

                healingHealMyAlliesTankCheckBox.Enabled = false;
                healingHealMyAlliesHealerCheckBox.Enabled = false;
                healingHealMyAlliesDPSCheckBox.Enabled = false;
            }
            else
            {
                healingHealMyAlliesTankCheckBox.Enabled = true;
                healingHealMyAlliesHealerCheckBox.Enabled = true;
                healingHealMyAlliesDPSCheckBox.Enabled = true;
            }
        }

        private void healingHealMyAlliesApplyWeightToTankTrackBar_ValueChanged(object sender, EventArgs e)
        {
            SetWeightToLabel(healingHealMyAlliesApplyWeightToTankTrackBar, healingHealMyAlliesApplyWeightToTankLabel);
        }

        private void healingHealMyAlliesApplyWeightToHealerTrackBar_ValueChanged(object sender, EventArgs e)
        {
            SetWeightToLabel(healingHealMyAlliesApplyWeightToHealerTrackBar, healingHealMyAlliesApplyWeightToHealerLabel);
        }

        private void healingHealMyAlliesApplyWeightToDPSTrackBar_ValueChanged(object sender, EventArgs e)
        {
            SetWeightToLabel(healingHealMyAlliesApplyWeightToDPSTrackBar, healingHealMyAlliesApplyWeightToDPSLabel);
        }

        private static void SetWeightToLabel(TrackBar trackBar, Control label)
        {
            label.Text = string.Format("{0}.{1}", trackBar.Value >= 10 ? "2" : "1",
                trackBar.Value >= 10 ? "0" : trackBar.Value.ToString());
        }

        private void healingHealMyAlliesApplyWeightsToRolesEnabledCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            healingHealMyAlliesApplyWeightToTankTrackBar.Enabled =
                healingHealMyAlliesApplyWeightsToRolesEnabledCheckBox.Checked;
            healingHealMyAlliesApplyWeightToHealerTrackBar.Enabled =
                healingHealMyAlliesApplyWeightsToRolesEnabledCheckBox.Checked;
            healingHealMyAlliesApplyWeightToDPSTrackBar.Enabled =
                healingHealMyAlliesApplyWeightsToRolesEnabledCheckBox.Checked;

            healingHealMyAlliesApplyWeightToTankLabel.Enabled =
                healingHealMyAlliesApplyWeightsToRolesEnabledCheckBox.Checked;
            healingHealMyAlliesApplyWeightToHealerLabel.Enabled =
                healingHealMyAlliesApplyWeightsToRolesEnabledCheckBox.Checked;
            healingHealMyAlliesApplyWeightToDPSLabel.Enabled =
                healingHealMyAlliesApplyWeightsToRolesEnabledCheckBox.Checked;
        }

        #endregion
    }
}