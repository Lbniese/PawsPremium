using System;
using System.Windows.Forms;
using Paws.Core.Managers;
using Paws.Interface.Forms;

namespace Paws.Interface.Controls.Guardian
{
    public partial class GuardianHealingSettings : UserControl, ISettingsControl
    {
        public GuardianHealingSettings(SettingsForm settingsForm)
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
            healingFrenziedRegenerationEnabledCheckBox.Checked = Settings.FrenziedRegenerationEnabled;
            healingFrenziedRegenerationEnabledCheckBox_CheckedChanged(healingFrenziedRegenerationEnabledCheckBox,
                EventArgs.Empty);
            healingFrenziedRegenerationMinHealthTextBox.Text = Settings.FrenziedRegenerationMinHealth.ToString("0.##");
            healingFrenziedRegenerationMinRageTextBox.Text = Settings.FrenziedRegenerationMinRage.ToString("0.##");
            healingRenewalEnabledCheckBox.Checked = Settings.GuardianRenewalEnabled;
            healingRenewalEnabledCheckBox_CheckedChanged(healingRenewalEnabledCheckBox, EventArgs.Empty);
            healingRenewalMinHealthTextBox.Text = Settings.GuardianRenewalMinHealth.ToString("0.##");
            healingHealthstoneEnabledCheckBox.Checked = Settings.GuardianHealthstoneEnabled;
            healingHealthstoneEnabledCheckBox_CheckedChanged(healingHealthstoneEnabledCheckBox, EventArgs.Empty);
            healingHealthstoneMinHealthTextBox.Text = Settings.GuardianHealthstoneMinHealth.ToString("0.##");
            healingRejuvenationEnabledCheckBox.Checked = Settings.GuardianRejuvenationEnabled;
            healingRejuvenationEnabledCheckBox_CheckedChanged(healingRejuvenationEnabledCheckBox, EventArgs.Empty);
            healingRejuvenationMinHealthTextBox.Text = Settings.GuardianRejuvenationMinHealth.ToString("0.##");
            healingHealingTouchEnabledCheckBox.Checked = Settings.GuardianHealingTouchEnabled;
            healingHealingTouchEnabledCheckBox_CheckedChanged(healingHealingTouchEnabledCheckBox, EventArgs.Empty);
            healingHealingTouchMinHealthTextBox.Text = Settings.GuardianHealingTouchMinHealth.ToString("0.##");
            healingHealingTouchDreamOfCenariusProcCheckBox.Checked =
                Settings.GuardianHealingTouchOnlyDuringDreamOfCenarius;
            healingCenarionWardEnabledCheckBox.Checked = Settings.GuardianCenarionWardEnabled;
            healingCenarionWardEnabledCheckBox_CheckedChanged(healingCenarionWardEnabledCheckBox, EventArgs.Empty);
            healingCenarionWardMinHealthTextBox.Text = Settings.GuardianCenarionWardMinHealth.ToString("0.##");
            healingNaturesVigilEnabledCheckBox.Checked = Settings.GuardianNaturesVigilEnabled;
            healingNaturesVigilEnabledCheckBox_CheckedChanged(healingNaturesVigilEnabledCheckBox, EventArgs.Empty);
            healingNaturesVigilMinHealthTextBox.Text = Settings.GuardianNaturesVigilMinHealth.ToString("0.##");
        }

        public void ApplySettings()
        {
            Settings.FrenziedRegenerationEnabled = healingFrenziedRegenerationEnabledCheckBox.Checked;
            Settings.FrenziedRegenerationMinHealth = Convert.ToDouble(healingFrenziedRegenerationMinHealthTextBox.Text);
            Settings.FrenziedRegenerationMinRage = Convert.ToDouble(healingFrenziedRegenerationMinRageTextBox.Text);
            Settings.GuardianRenewalEnabled = healingRenewalEnabledCheckBox.Checked;
            Settings.GuardianRenewalMinHealth = Convert.ToDouble(healingRenewalMinHealthTextBox.Text);
            Settings.GuardianHealthstoneEnabled = healingHealthstoneEnabledCheckBox.Checked;
            Settings.GuardianHealthstoneMinHealth = Convert.ToDouble(healingHealthstoneMinHealthTextBox.Text);
            Settings.GuardianRejuvenationEnabled = healingRejuvenationEnabledCheckBox.Checked;
            Settings.GuardianRejuvenationMinHealth = Convert.ToDouble(healingRejuvenationMinHealthTextBox.Text);
            Settings.GuardianHealingTouchEnabled = healingHealingTouchEnabledCheckBox.Checked;
            Settings.GuardianHealingTouchMinHealth = Convert.ToDouble(healingHealingTouchMinHealthTextBox.Text);
            Settings.GuardianHealingTouchOnlyDuringDreamOfCenarius =
                healingHealingTouchDreamOfCenariusProcCheckBox.Checked;
            Settings.GuardianCenarionWardEnabled = healingCenarionWardEnabledCheckBox.Checked;
            Settings.GuardianCenarionWardMinHealth = Convert.ToDouble(healingCenarionWardMinHealthTextBox.Text);
            Settings.GuardianNaturesVigilEnabled = healingNaturesVigilEnabledCheckBox.Checked;
            Settings.GuardianNaturesVigilMinHealth = Convert.ToDouble(healingNaturesVigilMinHealthTextBox.Text);
        }

        #region UI Events: Control Toggles

        private void healingFrenziedRegenerationEnabledCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            var checkBox = sender as CheckBox;
            if (checkBox != null) healingFrenziedRegenerationPanel.Enabled = checkBox.Checked;
            SettingsForm.InterfaceElementColorToggle(sender);
        }

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

        #endregion
    }
}