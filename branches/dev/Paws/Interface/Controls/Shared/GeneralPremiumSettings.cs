using Paws.Core.Managers;
using System;
using System.Windows.Forms;
using Styx.Helpers;

namespace Paws.Interface.Controls.Shared
{
    public partial class GeneralPremiumSettings : UserControl, ISettingsControl
    {
        private SettingsManager Settings { get { return SettingsManager.Instance; } }

        public SettingsForm SettingsForm { get; set; }

        public GeneralPremiumSettings(SettingsForm settingsForm)
        {
            this.SettingsForm = settingsForm;
            InitializeComponent();
        }

        private void FeralMobilitySettings_Load(object sender, EventArgs e)
        {
            
        }

        public void BindUISettings()
        {
            this.generalMarkOfTheWildEnabledCheckBox.Checked = Settings.MarkOfTheWildEnabled;
            generalMarkOfTheWildEnabledCheckBox_CheckedChanged(this.generalMarkOfTheWildEnabledCheckBox, EventArgs.Empty);
            this.generalMarkOfTheWildDoNotApplyStealthedCheckBox.Checked = Settings.MarkOfTheWildDoNotApplyIfStealthed;
            this.generalSootheEnabledCheckBox.Checked = Settings.SootheEnabled;
            generalSootheEnabledCheckBox_CheckedChanged(generalSootheEnabledCheckBox, EventArgs.Empty);
            this.generalSootheReactionTimeTextBox.Text = Settings.SootheReactionTimeInMs.ToString();
            this.generalTargetHeightCheckBox.Checked = Settings.TargetHeightEnabled;
            generalTargetHeightCheckBox_CheckedChanged(this.generalTargetHeightCheckBox, EventArgs.Empty);
            this.generalTargetHeightTextBox.Text = Settings.TargetHeightMinDistance.ToString("0.##");
            this.generalReleaseSpiritOnDeathEnabledCheckBox.Checked = Settings.ReleaseSpiritOnDeathEnabled;
            generalReleaseSpiritOnDeathEnabledCheckBox_CheckedChanged(this.generalReleaseSpiritOnDeathEnabledCheckBox, EventArgs.Empty);
            this.generalReleaseSpiritOnDeathTimerTextBox.Text = Settings.ReleaseSpiritOnDeathIntervalInMs.ToString();
            this.generalInterruptTimingMinMSTextBox.Text = Settings.InterruptMinMilliseconds.ToString();
            this.generalInterruptTimingMaxMSTextBox.Text = Settings.InterruptMaxMilliseconds.ToString();
            this.generalInterruptTimingSuccessRateTextBox.Text = Settings.InterruptSuccessRate.ToString("0.##");
            this.mobilityGeneralMovementCheckBox.Checked = Settings.AllowMovement;
            this.mobilityGeneralTargetFacingCheckBox.Checked = Settings.AllowTargetFacing;
            this.mobilityAutoTargetCheckBox.Checked = Settings.AllowTargeting;
            this.mobilityForceCombatCheckBox.Checked = Settings.ForceCombat;
            this.generalMultiDotRotationEnabledCheckBox.Checked = Settings.MultiDOTRotationEnabled;
        }

        public void ApplySettings()
        {
            Settings.MarkOfTheWildEnabled = this.generalMarkOfTheWildEnabledCheckBox.Checked;
            Settings.MarkOfTheWildDoNotApplyIfStealthed = this.generalMarkOfTheWildDoNotApplyStealthedCheckBox.Checked;
            Settings.SootheEnabled = this.generalSootheEnabledCheckBox.Checked;
            Settings.SootheReactionTimeInMs = Convert.ToInt32(this.generalSootheReactionTimeTextBox.Text);
            Settings.TargetHeightEnabled = this.generalTargetHeightCheckBox.Checked;
            Settings.TargetHeightMinDistance = this.generalTargetHeightTextBox.Text.ToFloat();
            Settings.ReleaseSpiritOnDeathEnabled = this.generalReleaseSpiritOnDeathEnabledCheckBox.Checked;
            Settings.ReleaseSpiritOnDeathIntervalInMs = Convert.ToInt32(this.generalReleaseSpiritOnDeathTimerTextBox.Text);
            Settings.InterruptMinMilliseconds = Convert.ToInt32(this.generalInterruptTimingMinMSTextBox.Text);
            Settings.InterruptMaxMilliseconds = Convert.ToInt32(this.generalInterruptTimingMaxMSTextBox.Text);
            Settings.InterruptSuccessRate = Convert.ToDouble(this.generalInterruptTimingSuccessRateTextBox.Text);
            Settings.AllowMovement = this.mobilityGeneralMovementCheckBox.Checked;
            Settings.AllowTargetFacing = this.mobilityGeneralTargetFacingCheckBox.Checked;
            Settings.AllowTargeting = this.mobilityAutoTargetCheckBox.Checked;
            Settings.ForceCombat = this.mobilityForceCombatCheckBox.Checked;
            Settings.MultiDOTRotationEnabled = this.generalMultiDotRotationEnabledCheckBox.Checked;
        }

        #region UI Events: Control Toggles

        private void generalMarkOfTheWildEnabledCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            this.generalMarkOfTheWildPanel.Enabled = (sender as CheckBox).Checked;
            this.SettingsForm.InterfaceElementColorToggle(sender);
        }

        private void generalSootheEnabledCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            this.generalSoothePanel.Enabled = (sender as CheckBox).Checked;
            this.SettingsForm.InterfaceElementColorToggle(sender);
        }

        private void generalTargetHeightCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            this.generalTargetHeightPanel.Enabled = (sender as CheckBox).Checked;
            this.SettingsForm.InterfaceElementColorToggle(sender);
        }

        private void generalReleaseSpiritOnDeathEnabledCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            this.generalReleaseSpiritOnDeathPanel.Enabled = (sender as CheckBox).Checked;
            this.SettingsForm.InterfaceElementColorToggle(sender);
        }

        #endregion
    }
}
