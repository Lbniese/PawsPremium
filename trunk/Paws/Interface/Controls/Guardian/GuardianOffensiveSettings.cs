using Paws.Core.Managers;
using System;
using System.Windows.Forms;
using Styx.Helpers;

namespace Paws.Interface.Controls.Guardian
{
    public partial class GuardianOffensiveSettings : UserControl, ISettingsControl
    {
        private SettingsManager Settings { get { return SettingsManager.Instance; } }

        public SettingsForm SettingsForm { get; set; }

        public GuardianOffensiveSettings(SettingsForm settingsForm)
        {
            this.SettingsForm = settingsForm;
            InitializeComponent();
        }

        public void BindUISettings()
        {
            this.offensiveFaerieFireEnabledCheckBox.Checked = Settings.GuardianFaerieFireEnabled;
            offensiveFaerieFireEnabledCheckBox_CheckedChanged(this.offensiveFaerieFireEnabledCheckBox, EventArgs.Empty);
            this.offensiveFaerieFireMinDistanceTextBox.Text = Settings.GuardianFaerieFireMinDistance.ToString("0.##");
            this.offensiveFaerieFireMaxDistanceTextBox.Text = Settings.GuardianFaerieFireMaxDistance.ToString("0.##");
            this.offensiveGrowlEnabledCheckBox.Checked = Settings.GrowlEnabled;
            offensiveGrowlEnabledCheckBox_CheckedChanged(offensiveGrowlEnabledCheckBox, EventArgs.Empty);
            this.offensiveGrowlAnyTargetCheckBox.Checked = Settings.GrowlAnythingNotMe;
            this.offensiveGrowlGroupTankCheckBox.Checked = Settings.GrowlGroupTank;
            this.offensiveGrowlGroupHealerCheckBox.Checked = Settings.GrowlGroupHealer;
            this.offensiveGrowlGroupDPSCheckBox.Checked = Settings.GrowlGroupDPS;
            this.offensiveGrowlGroupPlayerPetCheckBox.Checked = Settings.GrowlGroupPlayerPet;
            this.offensiveBerserkEnabled.Checked = Settings.GuardianBerserkEnabled;
            offensiveBerserkEnabled_CheckedChanged(this.offensiveBerserkEnabled, EventArgs.Empty);
            this.offensiveBerserkOnCooldownCheckBox.Checked = Settings.GuardianBerserkOnCooldown;
            this.offensiveBerserkEnemyHealthRadioButton.Checked = Settings.GuardianBerserkEnemyHealthCheck;
            this.offensiveBerserkEnemyHealthMultiplierTextBox.Text = Settings.GuardianBerserkEnemyHealthMultiplier.ToString("0.##");
            this.offensiveBerserkSurroundedByEnemiesCheckBox.Checked = Settings.GuardianBerserkSurroundedByEnemiesEnabled;
            this.offensiveBerserkSurroundedByEnemiesTextBox.Text = Settings.GuardianBerserkSurroundedByMinEnemies.ToString();
            this.offensiveMangleEnabledCheckBox.Checked = Settings.MangleEnabled;
            offensiveMangleEnabledCheckBox_CheckedChanged(this.offensiveMangleEnabledCheckBox, EventArgs.Empty);
            this.offensiveLacerateEnabledCheckBox.Checked = Settings.LacerateEnabled;
            offensiveLacerateEnabledCheckBox_CheckedChanged(this.offensiveLacerateEnabledCheckBox, EventArgs.Empty);
            this.OffensiveLacerateAlloweClippingCheckBox.Checked = Settings.LacerateAllowClipping;
            this.offensivePulverizeEnabledCheckBox.Checked = Settings.PulverizeEnabled;
            offensivePulverizeEnabledCheckBox_CheckedChanged(this.offensivePulverizeEnabledCheckBox, EventArgs.Empty);
            this.OffensiveMaulEnabledCheckBox.Checked = Settings.MaulEnabled;
            OffensiveMaulEnabledCheckBox_CheckedChanged(this.OffensiveMaulEnabledCheckBox, EventArgs.Empty);
            this.offensiveMaulOnlyDuringToothAndClawProcCheckBox.Checked = Settings.MaulOnlyDuringToothAndClawProc;
            this.offensiveMaulRequireRageCheckBox.Checked = Settings.MaulRequireMinRage;
            this.offensiveMaulMinimumRageTextBox.Text = Settings.MaulMinRage.ToString("0.##");
            this.offensiveThrashEnabledCheckBox.Checked = Settings.GuardianThrashEnabled;
            offensiveThrashEnabledCheckBox_CheckedChanged(this.offensiveThrashEnabledCheckBox, EventArgs.Empty);
            this.offensiveThrashAllowClippingCheckBox.Checked = Settings.GuardianThrashAllowClipping;
            this.offensiveThrashMinEnemiesTextBox.Text = Settings.GuardianThrashMinEnemies.ToString("0.##");
            this.offensiveIncarnationEnabledCheckBox.Checked = Settings.GuardianIncarnationEnabled;
            offensiveIncarnationEnabledCheckBox_CheckedChanged(offensiveIncarnationEnabledCheckBox, EventArgs.Empty);
            this.offensiveIncarnationOnCooldownCheckBox.Checked = Settings.GuardianIncarnationOnCooldown;
            this.offensiveIncarnationEnemyHealthRadioButton.Checked = Settings.GuardianIncarnationEnemyHealthCheck;
            this.offensiveIncarnationEnemyHealthMultiplierTextBox.Text = Settings.GuardianIncarnationEnemyHealthMultiplier.ToString("0.##");
        }

        public void ApplySettings()
        {
            Settings.GuardianFaerieFireEnabled = this.offensiveFaerieFireEnabledCheckBox.Checked;
            Settings.GuardianFaerieFireMinDistance = Convert.ToDouble(this.offensiveFaerieFireMinDistanceTextBox.Text);
            Settings.GuardianFaerieFireMaxDistance = Convert.ToDouble(this.offensiveFaerieFireMaxDistanceTextBox.Text);
            Settings.GrowlEnabled = this.offensiveGrowlEnabledCheckBox.Checked;
            Settings.GrowlAnythingNotMe = this.offensiveGrowlAnyTargetCheckBox.Checked;
            Settings.GrowlGroupTank = this.offensiveGrowlGroupTankCheckBox.Checked;
            Settings.GrowlGroupHealer = this.offensiveGrowlGroupHealerCheckBox.Checked;
            Settings.GrowlGroupDPS = this.offensiveGrowlGroupDPSCheckBox.Checked;
            Settings.GrowlGroupPlayerPet = this.offensiveGrowlGroupPlayerPetCheckBox.Checked;
            Settings.GuardianBerserkEnabled = this.offensiveBerserkEnabled.Checked;
            Settings.GuardianBerserkOnCooldown = this.offensiveBerserkOnCooldownCheckBox.Checked;
            Settings.GuardianBerserkEnemyHealthCheck = this.offensiveBerserkEnemyHealthRadioButton.Checked;
            Settings.GuardianBerserkEnemyHealthMultiplier = this.offensiveBerserkEnemyHealthMultiplierTextBox.Text.ToFloat();
            Settings.GuardianBerserkSurroundedByEnemiesEnabled = this.offensiveBerserkSurroundedByEnemiesCheckBox.Checked;
            Settings.GuardianBerserkSurroundedByMinEnemies = Convert.ToInt32(this.offensiveBerserkSurroundedByEnemiesTextBox.Text);
            Settings.MangleEnabled = this.offensiveMangleEnabledCheckBox.Checked;
            Settings.LacerateEnabled = this.offensiveLacerateEnabledCheckBox.Checked;
            Settings.LacerateAllowClipping = this.OffensiveLacerateAlloweClippingCheckBox.Checked;
            Settings.PulverizeEnabled = this.offensivePulverizeEnabledCheckBox.Checked;
            Settings.MaulEnabled = this.OffensiveMaulEnabledCheckBox.Checked;
            Settings.MaulOnlyDuringToothAndClawProc = this.offensiveMaulOnlyDuringToothAndClawProcCheckBox.Checked;
            Settings.MaulRequireMinRage = this.offensiveMaulRequireRageCheckBox.Checked;
            Settings.MaulMinRage = Convert.ToDouble(this.offensiveMaulMinimumRageTextBox.Text);
            Settings.GuardianThrashEnabled = this.offensiveThrashEnabledCheckBox.Checked;
            Settings.GuardianThrashAllowClipping = this.offensiveThrashAllowClippingCheckBox.Checked;
            Settings.GuardianThrashMinEnemies = Convert.ToInt32(this.offensiveThrashMinEnemiesTextBox.Text);
            Settings.GuardianIncarnationEnabled = this.offensiveIncarnationEnabledCheckBox.Checked;
            Settings.GuardianIncarnationOnCooldown = this.offensiveIncarnationOnCooldownCheckBox.Checked;
            Settings.GuardianIncarnationEnemyHealthCheck = this.offensiveIncarnationEnemyHealthRadioButton.Checked;
            Settings.GuardianIncarnationEnemyHealthMultiplier = this.offensiveIncarnationEnemyHealthMultiplierTextBox.Text.ToFloat();
        }

        #region UI Events: Control Toggles

        private void offensiveFaerieFireEnabledCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            this.offensiveFaerieFirePanel.Enabled = (sender as CheckBox).Checked;
            this.SettingsForm.InterfaceElementColorToggle(sender);
        }

        private void offensiveGrowlEnabledCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            this.offensiveGrowlPanel.Enabled = (sender as CheckBox).Checked;
            this.SettingsForm.InterfaceElementColorToggle(sender);
        }

        private void offensiveBerserkEnabled_CheckedChanged(object sender, System.EventArgs e)
        {
            this.offensiveBerserkPanel.Enabled = (sender as CheckBox).Checked;
            this.SettingsForm.InterfaceElementColorToggle(sender);
        }

        private void offensiveMangleEnabledCheckBox_CheckedChanged(object sender, System.EventArgs e)
        {
            this.SettingsForm.InterfaceElementColorToggle(sender);
        }

        private void offensiveLacerateEnabledCheckBox_CheckedChanged(object sender, System.EventArgs e)
        {
            this.offensiveLaceratePanel.Enabled = (sender as CheckBox).Checked;
            this.SettingsForm.InterfaceElementColorToggle(sender);
        }

        private void offensivePulverizeEnabledCheckBox_CheckedChanged(object sender, System.EventArgs e)
        {
            this.SettingsForm.InterfaceElementColorToggle(sender);
        }

        private void OffensiveMaulEnabledCheckBox_CheckedChanged(object sender, System.EventArgs e)
        {
            this.offensiveMaulPanel.Enabled = (sender as CheckBox).Checked;
            this.SettingsForm.InterfaceElementColorToggle(sender);
        }

        private void offensiveThrashEnabledCheckBox_CheckedChanged(object sender, System.EventArgs e)
        {
            this.offensiveThrashPanel.Enabled = (sender as CheckBox).Checked;
            this.SettingsForm.InterfaceElementColorToggle(sender);   
        }

        private void offensiveIncarnationEnabledCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            this.offensiveIncarnationPanel.Enabled = (sender as CheckBox).Checked;
            this.SettingsForm.InterfaceElementColorToggle(sender);
        }

        #endregion
    }
}
