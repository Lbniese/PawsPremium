using System;
using System.Windows.Forms;
using Paws.Core.Managers;
using Paws.Interface.Forms;
using Styx.Helpers;

namespace Paws.Interface.Controls.Guardian
{
    public partial class GuardianOffensiveSettings : UserControl, ISettingsControl
    {
        public GuardianOffensiveSettings(SettingsForm settingsForm)
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
            offensiveFaerieFireEnabledCheckBox.Checked = Settings.GuardianFaerieFireEnabled;
            offensiveFaerieFireEnabledCheckBox_CheckedChanged(offensiveFaerieFireEnabledCheckBox, EventArgs.Empty);
            offensiveFaerieFireMinDistanceTextBox.Text = Settings.GuardianFaerieFireMinDistance.ToString("0.##");
            offensiveFaerieFireMaxDistanceTextBox.Text = Settings.GuardianFaerieFireMaxDistance.ToString("0.##");
            offensiveGrowlEnabledCheckBox.Checked = Settings.GrowlEnabled;
            offensiveGrowlEnabledCheckBox_CheckedChanged(offensiveGrowlEnabledCheckBox, EventArgs.Empty);
            offensiveGrowlAnyTargetCheckBox.Checked = Settings.GrowlAnythingNotMe;
            offensiveGrowlGroupTankCheckBox.Checked = Settings.GrowlGroupTank;
            offensiveGrowlGroupHealerCheckBox.Checked = Settings.GrowlGroupHealer;
            offensiveGrowlGroupDPSCheckBox.Checked = Settings.GrowlGroupDps;
            offensiveGrowlGroupPlayerPetCheckBox.Checked = Settings.GrowlGroupPlayerPet;
            offensiveBerserkEnabled.Checked = Settings.GuardianBerserkEnabled;
            offensiveBerserkEnabled_CheckedChanged(offensiveBerserkEnabled, EventArgs.Empty);
            offensiveBerserkOnCooldownCheckBox.Checked = Settings.GuardianBerserkOnCooldown;
            offensiveBerserkEnemyHealthRadioButton.Checked = Settings.GuardianBerserkEnemyHealthCheck;
            offensiveBerserkEnemyHealthMultiplierTextBox.Text =
                Settings.GuardianBerserkEnemyHealthMultiplier.ToString("0.##");
            offensiveBerserkSurroundedByEnemiesCheckBox.Checked = Settings.GuardianBerserkSurroundedByEnemiesEnabled;
            offensiveBerserkSurroundedByEnemiesTextBox.Text = Settings.GuardianBerserkSurroundedByMinEnemies.ToString();
            offensiveMangleEnabledCheckBox.Checked = Settings.MangleEnabled;
            offensiveMangleEnabledCheckBox_CheckedChanged(offensiveMangleEnabledCheckBox, EventArgs.Empty);
            offensiveLacerateEnabledCheckBox.Checked = Settings.LacerateEnabled;
            offensiveLacerateEnabledCheckBox_CheckedChanged(offensiveLacerateEnabledCheckBox, EventArgs.Empty);
            OffensiveLacerateAlloweClippingCheckBox.Checked = Settings.LacerateAllowClipping;
            offensivePulverizeEnabledCheckBox.Checked = Settings.PulverizeEnabled;
            offensivePulverizeEnabledCheckBox_CheckedChanged(offensivePulverizeEnabledCheckBox, EventArgs.Empty);
            OffensiveMaulEnabledCheckBox.Checked = Settings.MaulEnabled;
            OffensiveMaulEnabledCheckBox_CheckedChanged(OffensiveMaulEnabledCheckBox, EventArgs.Empty);
            offensiveMaulOnlyDuringToothAndClawProcCheckBox.Checked = Settings.MaulOnlyDuringToothAndClawProc;
            offensiveMaulRequireRageCheckBox.Checked = Settings.MaulRequireMinRage;
            offensiveMaulMinimumRageTextBox.Text = Settings.MaulMinRage.ToString("0.##");
            offensiveThrashEnabledCheckBox.Checked = Settings.GuardianThrashEnabled;
            offensiveThrashEnabledCheckBox_CheckedChanged(offensiveThrashEnabledCheckBox, EventArgs.Empty);
            offensiveThrashAllowClippingCheckBox.Checked = Settings.GuardianThrashAllowClipping;
            offensiveThrashMinEnemiesTextBox.Text = Settings.GuardianThrashMinEnemies.ToString("0.##");
            offensiveIncarnationEnabledCheckBox.Checked = Settings.GuardianIncarnationEnabled;
            offensiveIncarnationEnabledCheckBox_CheckedChanged(offensiveIncarnationEnabledCheckBox, EventArgs.Empty);
            offensiveIncarnationOnCooldownCheckBox.Checked = Settings.GuardianIncarnationOnCooldown;
            offensiveIncarnationEnemyHealthRadioButton.Checked = Settings.GuardianIncarnationEnemyHealthCheck;
            offensiveIncarnationEnemyHealthMultiplierTextBox.Text =
                Settings.GuardianIncarnationEnemyHealthMultiplier.ToString("0.##");
        }

        public void ApplySettings()
        {
            Settings.GuardianFaerieFireEnabled = offensiveFaerieFireEnabledCheckBox.Checked;
            Settings.GuardianFaerieFireMinDistance = Convert.ToDouble(offensiveFaerieFireMinDistanceTextBox.Text);
            Settings.GuardianFaerieFireMaxDistance = Convert.ToDouble(offensiveFaerieFireMaxDistanceTextBox.Text);
            Settings.GrowlEnabled = offensiveGrowlEnabledCheckBox.Checked;
            Settings.GrowlAnythingNotMe = offensiveGrowlAnyTargetCheckBox.Checked;
            Settings.GrowlGroupTank = offensiveGrowlGroupTankCheckBox.Checked;
            Settings.GrowlGroupHealer = offensiveGrowlGroupHealerCheckBox.Checked;
            Settings.GrowlGroupDps = offensiveGrowlGroupDPSCheckBox.Checked;
            Settings.GrowlGroupPlayerPet = offensiveGrowlGroupPlayerPetCheckBox.Checked;
            Settings.GuardianBerserkEnabled = offensiveBerserkEnabled.Checked;
            Settings.GuardianBerserkOnCooldown = offensiveBerserkOnCooldownCheckBox.Checked;
            Settings.GuardianBerserkEnemyHealthCheck = offensiveBerserkEnemyHealthRadioButton.Checked;
            Settings.GuardianBerserkEnemyHealthMultiplier = offensiveBerserkEnemyHealthMultiplierTextBox.Text.ToFloat();
            Settings.GuardianBerserkSurroundedByEnemiesEnabled = offensiveBerserkSurroundedByEnemiesCheckBox.Checked;
            Settings.GuardianBerserkSurroundedByMinEnemies =
                Convert.ToInt32(offensiveBerserkSurroundedByEnemiesTextBox.Text);
            Settings.MangleEnabled = offensiveMangleEnabledCheckBox.Checked;
            Settings.LacerateEnabled = offensiveLacerateEnabledCheckBox.Checked;
            Settings.LacerateAllowClipping = OffensiveLacerateAlloweClippingCheckBox.Checked;
            Settings.PulverizeEnabled = offensivePulverizeEnabledCheckBox.Checked;
            Settings.MaulEnabled = OffensiveMaulEnabledCheckBox.Checked;
            Settings.MaulOnlyDuringToothAndClawProc = offensiveMaulOnlyDuringToothAndClawProcCheckBox.Checked;
            Settings.MaulRequireMinRage = offensiveMaulRequireRageCheckBox.Checked;
            Settings.MaulMinRage = Convert.ToDouble(offensiveMaulMinimumRageTextBox.Text);
            Settings.GuardianThrashEnabled = offensiveThrashEnabledCheckBox.Checked;
            Settings.GuardianThrashAllowClipping = offensiveThrashAllowClippingCheckBox.Checked;
            Settings.GuardianThrashMinEnemies = Convert.ToInt32(offensiveThrashMinEnemiesTextBox.Text);
            Settings.GuardianIncarnationEnabled = offensiveIncarnationEnabledCheckBox.Checked;
            Settings.GuardianIncarnationOnCooldown = offensiveIncarnationOnCooldownCheckBox.Checked;
            Settings.GuardianIncarnationEnemyHealthCheck = offensiveIncarnationEnemyHealthRadioButton.Checked;
            Settings.GuardianIncarnationEnemyHealthMultiplier =
                offensiveIncarnationEnemyHealthMultiplierTextBox.Text.ToFloat();
        }

        #region UI Events: Control Toggles

        private void offensiveFaerieFireEnabledCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            var checkBox = sender as CheckBox;
            if (checkBox != null) offensiveFaerieFirePanel.Enabled = checkBox.Checked;
            SettingsForm.InterfaceElementColorToggle(sender);
        }

        private void offensiveGrowlEnabledCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            var checkBox = sender as CheckBox;
            if (checkBox != null) offensiveGrowlPanel.Enabled = checkBox.Checked;
            SettingsForm.InterfaceElementColorToggle(sender);
        }

        private void offensiveBerserkEnabled_CheckedChanged(object sender, EventArgs e)
        {
            var checkBox = sender as CheckBox;
            if (checkBox != null) offensiveBerserkPanel.Enabled = checkBox.Checked;
            SettingsForm.InterfaceElementColorToggle(sender);
        }

        private void offensiveMangleEnabledCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            SettingsForm.InterfaceElementColorToggle(sender);
        }

        private void offensiveLacerateEnabledCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            var checkBox = sender as CheckBox;
            if (checkBox != null) offensiveLaceratePanel.Enabled = checkBox.Checked;
            SettingsForm.InterfaceElementColorToggle(sender);
        }

        private void offensivePulverizeEnabledCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            SettingsForm.InterfaceElementColorToggle(sender);
        }

        private void OffensiveMaulEnabledCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            var checkBox = sender as CheckBox;
            if (checkBox != null) offensiveMaulPanel.Enabled = checkBox.Checked;
            SettingsForm.InterfaceElementColorToggle(sender);
        }

        private void offensiveThrashEnabledCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            var checkBox = sender as CheckBox;
            if (checkBox != null) offensiveThrashPanel.Enabled = checkBox.Checked;
            SettingsForm.InterfaceElementColorToggle(sender);
        }

        private void offensiveIncarnationEnabledCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            var checkBox = sender as CheckBox;
            if (checkBox != null) offensiveIncarnationPanel.Enabled = checkBox.Checked;
            SettingsForm.InterfaceElementColorToggle(sender);
        }

        #endregion
    }
}