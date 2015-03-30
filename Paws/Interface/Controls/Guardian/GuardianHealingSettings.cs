using Paws.Core.Managers;
using System;
using System.Windows.Forms;
using Styx.Helpers;

namespace Paws.Interface.Controls.Guardian
{
    public partial class GuardianHealingSettings : UserControl, ISettingsControl
    {
        private SettingsManager Settings { get { return SettingsManager.Instance; } }

        public SettingsForm SettingsForm { get; set; }

        public GuardianHealingSettings(SettingsForm settingsForm)
        {
            this.SettingsForm = settingsForm;
            InitializeComponent();
        }

        public void BindUISettings()
        {
            this.healingFrenziedRegenerationEnabledCheckBox.Checked = Settings.FrenziedRegenerationEnabled;
            healingFrenziedRegenerationEnabledCheckBox_CheckedChanged(this.healingFrenziedRegenerationEnabledCheckBox, EventArgs.Empty);
            this.healingFrenziedRegenerationMinHealthTextBox.Text = Settings.FrenziedRegenerationMinHealth.ToString("0.##");
            this.healingFrenziedRegenerationMinRageTextBox.Text = Settings.FrenziedRegenerationMinRage.ToString("0.##");
            this.healingRenewalEnabledCheckBox.Checked = Settings.GuardianRenewalEnabled;
            healingRenewalEnabledCheckBox_CheckedChanged(this.healingRenewalEnabledCheckBox, EventArgs.Empty);
            this.healingRenewalMinHealthTextBox.Text = Settings.GuardianRenewalMinHealth.ToString("0.##");
            this.healingHealthstoneEnabledCheckBox.Checked = Settings.GuardianHealthstoneEnabled;
            healingHealthstoneEnabledCheckBox_CheckedChanged(this.healingHealthstoneEnabledCheckBox, EventArgs.Empty);
            this.healingHealthstoneMinHealthTextBox.Text = Settings.GuardianHealthstoneMinHealth.ToString("0.##");
            this.healingRejuvenationEnabledCheckBox.Checked = Settings.GuardianRejuvenationEnabled;
            healingRejuvenationEnabledCheckBox_CheckedChanged(this.healingRejuvenationEnabledCheckBox, EventArgs.Empty);
            this.healingRejuvenationMinHealthTextBox.Text = Settings.GuardianRejuvenationMinHealth.ToString("0.##");
            this.healingHealingTouchEnabledCheckBox.Checked = Settings.GuardianHealingTouchEnabled;
            healingHealingTouchEnabledCheckBox_CheckedChanged(this.healingHealingTouchEnabledCheckBox, EventArgs.Empty);
            this.healingHealingTouchMinHealthTextBox.Text = Settings.GuardianHealingTouchMinHealth.ToString("0.##");
            this.healingHealingTouchDreamOfCenariusProcCheckBox.Checked = Settings.GuardianHealingTouchOnlyDuringDreamOfCenarius;
            this.healingCenarionWardEnabledCheckBox.Checked = Settings.GuardianCenarionWardEnabled;
            healingCenarionWardEnabledCheckBox_CheckedChanged(this.healingCenarionWardEnabledCheckBox, EventArgs.Empty);
            this.healingCenarionWardMinHealthTextBox.Text = Settings.GuardianCenarionWardMinHealth.ToString("0.##");
            this.healingNaturesVigilEnabledCheckBox.Checked = Settings.GuardianNaturesVigilEnabled;
            healingNaturesVigilEnabledCheckBox_CheckedChanged(this.healingNaturesVigilEnabledCheckBox, EventArgs.Empty);
            this.healingNaturesVigilMinHealthTextBox.Text = Settings.GuardianNaturesVigilMinHealth.ToString("0.##");
        }

        public void ApplySettings()
        {
            Settings.FrenziedRegenerationEnabled = this.healingFrenziedRegenerationEnabledCheckBox.Checked;
            Settings.FrenziedRegenerationMinHealth = Convert.ToDouble(this.healingFrenziedRegenerationMinHealthTextBox.Text);
            Settings.FrenziedRegenerationMinRage = Convert.ToDouble(this.healingFrenziedRegenerationMinRageTextBox.Text);
            Settings.GuardianRenewalEnabled = this.healingRenewalEnabledCheckBox.Checked;
            Settings.GuardianRenewalMinHealth = Convert.ToDouble(this.healingRenewalMinHealthTextBox.Text);
            Settings.GuardianHealthstoneEnabled = this.healingHealthstoneEnabledCheckBox.Checked;
            Settings.GuardianHealthstoneMinHealth = Convert.ToDouble(this.healingHealthstoneMinHealthTextBox.Text);
            Settings.GuardianRejuvenationEnabled = this.healingRejuvenationEnabledCheckBox.Checked;
            Settings.GuardianRejuvenationMinHealth = Convert.ToDouble(this.healingRejuvenationMinHealthTextBox.Text);
            Settings.GuardianHealingTouchEnabled = this.healingHealingTouchEnabledCheckBox.Checked;
            Settings.GuardianHealingTouchMinHealth = Convert.ToDouble(this.healingHealingTouchMinHealthTextBox.Text);
            Settings.GuardianHealingTouchOnlyDuringDreamOfCenarius = this.healingHealingTouchDreamOfCenariusProcCheckBox.Checked;
            Settings.GuardianCenarionWardEnabled = this.healingCenarionWardEnabledCheckBox.Checked;
            Settings.GuardianCenarionWardMinHealth = Convert.ToDouble(this.healingCenarionWardMinHealthTextBox.Text);
            Settings.GuardianNaturesVigilEnabled = this.healingNaturesVigilEnabledCheckBox.Checked;
            Settings.GuardianNaturesVigilMinHealth = Convert.ToDouble(this.healingNaturesVigilMinHealthTextBox.Text);
        }

        #region UI Events: Control Toggles

        private void healingFrenziedRegenerationEnabledCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            this.healingFrenziedRegenerationPanel.Enabled = (sender as CheckBox).Checked;
            this.SettingsForm.InterfaceElementColorToggle(sender);
        }

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

        #endregion
    }
}
