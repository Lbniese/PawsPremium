namespace Paws.Interface.Forms
{
    partial class AddNewAbilityForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.cancelButton = new System.Windows.Forms.Button();
            this.saveButton = new System.Windows.Forms.Button();
            this.panel65 = new System.Windows.Forms.Panel();
            this.label74 = new System.Windows.Forms.Label();
            this.panel66 = new System.Windows.Forms.Panel();
            this.targetCurrentTargetRadioButton = new System.Windows.Forms.RadioButton();
            this.targetMeRadioButton = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.isRequiredCheckBox = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.abilitiesComboBox = new System.Windows.Forms.ComboBox();
            this.focusTargetRadioButton = new System.Windows.Forms.RadioButton();
            this.toolTipProvider = new System.Windows.Forms.ToolTip(this.components);
            this.panel65.SuspendLayout();
            this.panel66.SuspendLayout();
            this.SuspendLayout();
            // 
            // cancelButton
            // 
            this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cancelButton.Location = new System.Drawing.Point(326, 152);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 37;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            // 
            // saveButton
            // 
            this.saveButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.saveButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.saveButton.Location = new System.Drawing.Point(407, 152);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(75, 23);
            this.saveButton.TabIndex = 36;
            this.saveButton.Text = "Save";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // panel65
            // 
            this.panel65.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel65.BackColor = System.Drawing.Color.DimGray;
            this.panel65.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel65.Controls.Add(this.label74);
            this.panel65.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel65.ForeColor = System.Drawing.Color.White;
            this.panel65.Location = new System.Drawing.Point(12, 12);
            this.panel65.Name = "panel65";
            this.panel65.Size = new System.Drawing.Size(470, 26);
            this.panel65.TabIndex = 38;
            // 
            // label74
            // 
            this.label74.AutoSize = true;
            this.label74.Location = new System.Drawing.Point(4, 4);
            this.label74.Name = "label74";
            this.label74.Size = new System.Drawing.Size(66, 15);
            this.label74.TabIndex = 10;
            this.label74.Text = "Add Ability";
            // 
            // panel66
            // 
            this.panel66.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel66.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panel66.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel66.Controls.Add(this.focusTargetRadioButton);
            this.panel66.Controls.Add(this.targetCurrentTargetRadioButton);
            this.panel66.Controls.Add(this.targetMeRadioButton);
            this.panel66.Controls.Add(this.label1);
            this.panel66.Controls.Add(this.isRequiredCheckBox);
            this.panel66.Controls.Add(this.label2);
            this.panel66.Controls.Add(this.label3);
            this.panel66.Controls.Add(this.abilitiesComboBox);
            this.panel66.Location = new System.Drawing.Point(12, 30);
            this.panel66.Name = "panel66";
            this.panel66.Size = new System.Drawing.Size(470, 111);
            this.panel66.TabIndex = 39;
            // 
            // targetCurrentTargetRadioButton
            // 
            this.targetCurrentTargetRadioButton.AutoSize = true;
            this.targetCurrentTargetRadioButton.Checked = true;
            this.targetCurrentTargetRadioButton.Location = new System.Drawing.Point(177, 51);
            this.targetCurrentTargetRadioButton.Name = "targetCurrentTargetRadioButton";
            this.targetCurrentTargetRadioButton.Size = new System.Drawing.Size(93, 17);
            this.targetCurrentTargetRadioButton.TabIndex = 37;
            this.targetCurrentTargetRadioButton.TabStop = true;
            this.targetCurrentTargetRadioButton.Text = "Current Target";
            this.targetCurrentTargetRadioButton.UseVisualStyleBackColor = true;
            // 
            // targetMeRadioButton
            // 
            this.targetMeRadioButton.AutoSize = true;
            this.targetMeRadioButton.Location = new System.Drawing.Point(121, 51);
            this.targetMeRadioButton.Name = "targetMeRadioButton";
            this.targetMeRadioButton.Size = new System.Drawing.Size(40, 17);
            this.targetMeRadioButton.TabIndex = 36;
            this.targetMeRadioButton.Text = "Me";
            this.targetMeRadioButton.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 53);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 13);
            this.label1.TabIndex = 35;
            this.label1.Text = "Target:";
            // 
            // isRequiredCheckBox
            // 
            this.isRequiredCheckBox.AutoSize = true;
            this.isRequiredCheckBox.Location = new System.Drawing.Point(121, 79);
            this.isRequiredCheckBox.Name = "isRequiredCheckBox";
            this.isRequiredCheckBox.Size = new System.Drawing.Size(15, 14);
            this.isRequiredCheckBox.TabIndex = 34;
            this.toolTipProvider.SetToolTip(this.isRequiredCheckBox, "If this is selected, the ability chain will not be triggered unless this Ability " +
        "is not on cooldown.");
            this.isRequiredCheckBox.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(20, 79);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(94, 13);
            this.label2.TabIndex = 33;
            this.label2.Text = "Must be Available:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(20, 25);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(37, 13);
            this.label3.TabIndex = 32;
            this.label3.Text = "Ability:";
            // 
            // abilitiesComboBox
            // 
            this.abilitiesComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.abilitiesComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.abilitiesComboBox.FormattingEnabled = true;
            this.abilitiesComboBox.Location = new System.Drawing.Point(121, 22);
            this.abilitiesComboBox.Name = "abilitiesComboBox";
            this.abilitiesComboBox.Size = new System.Drawing.Size(325, 21);
            this.abilitiesComboBox.TabIndex = 31;
            // 
            // focusTargetRadioButton
            // 
            this.focusTargetRadioButton.AutoSize = true;
            this.focusTargetRadioButton.Location = new System.Drawing.Point(286, 51);
            this.focusTargetRadioButton.Name = "focusTargetRadioButton";
            this.focusTargetRadioButton.Size = new System.Drawing.Size(88, 17);
            this.focusTargetRadioButton.TabIndex = 38;
            this.focusTargetRadioButton.Text = "Focus Target";
            this.focusTargetRadioButton.UseVisualStyleBackColor = true;
            // 
            // AddNewAbilityForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(494, 185);
            this.ControlBox = false;
            this.Controls.Add(this.panel65);
            this.Controls.Add(this.panel66);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.saveButton);
            this.Name = "AddNewAbilityForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Ability";
            this.Load += new System.EventHandler(this.AddNewAbilityForm_Load);
            this.panel65.ResumeLayout(false);
            this.panel65.PerformLayout();
            this.panel66.ResumeLayout(false);
            this.panel66.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.Panel panel65;
        private System.Windows.Forms.Label label74;
        private System.Windows.Forms.Panel panel66;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox abilitiesComboBox;
        private System.Windows.Forms.RadioButton targetCurrentTargetRadioButton;
        private System.Windows.Forms.RadioButton targetMeRadioButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox isRequiredCheckBox;
        private System.Windows.Forms.RadioButton focusTargetRadioButton;
        private System.Windows.Forms.ToolTip toolTipProvider;
    }
}