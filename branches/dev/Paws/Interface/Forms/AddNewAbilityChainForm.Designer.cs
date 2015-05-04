namespace Paws.Interface.Forms
{
    partial class AddNewAbilityChainForm
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
            this.cancelButton = new System.Windows.Forms.Button();
            this.saveButton = new System.Windows.Forms.Button();
            this.panel65 = new System.Windows.Forms.Panel();
            this.label74 = new System.Windows.Forms.Label();
            this.panel66 = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.modifierKeyComboBox = new System.Windows.Forms.ComboBox();
            this.hotKeyTriggerSetKeyButton = new System.Windows.Forms.Button();
            this.abilityChainNameTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.removeSelectedAbilitiesButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.newAbilityButton = new System.Windows.Forms.Button();
            this.abilitiesListView = new System.Windows.Forms.ListView();
            this.nameColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.panel65.SuspendLayout();
            this.panel66.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // cancelButton
            // 
            this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cancelButton.Location = new System.Drawing.Point(290, 394);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(122, 23);
            this.cancelButton.TabIndex = 3;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            // 
            // saveButton
            // 
            this.saveButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.saveButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.saveButton.Location = new System.Drawing.Point(418, 394);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(122, 23);
            this.saveButton.TabIndex = 2;
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
            this.panel65.Size = new System.Drawing.Size(528, 26);
            this.panel65.TabIndex = 30;
            // 
            // label74
            // 
            this.label74.AutoSize = true;
            this.label74.Location = new System.Drawing.Point(4, 4);
            this.label74.Name = "label74";
            this.label74.Size = new System.Drawing.Size(72, 15);
            this.label74.TabIndex = 10;
            this.label74.Text = "Information";
            // 
            // panel66
            // 
            this.panel66.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel66.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panel66.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel66.Controls.Add(this.groupBox1);
            this.panel66.Controls.Add(this.abilityChainNameTextBox);
            this.panel66.Controls.Add(this.label2);
            this.panel66.Location = new System.Drawing.Point(12, 30);
            this.panel66.Name = "panel66";
            this.panel66.Size = new System.Drawing.Size(528, 141);
            this.panel66.TabIndex = 31;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.modifierKeyComboBox);
            this.groupBox1.Controls.Add(this.hotKeyTriggerSetKeyButton);
            this.groupBox1.Location = new System.Drawing.Point(19, 47);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(420, 77);
            this.groupBox1.TabIndex = 21;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Hotkey Details";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(67, 51);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(28, 13);
            this.label4.TabIndex = 21;
            this.label4.Text = "Key:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(48, 22);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 13);
            this.label3.TabIndex = 19;
            this.label3.Text = "Modifier:";
            // 
            // modifierKeyComboBox
            // 
            this.modifierKeyComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.modifierKeyComboBox.FormattingEnabled = true;
            this.modifierKeyComboBox.Items.AddRange(new object[] {
            "Alt",
            "Control",
            "Shift"});
            this.modifierKeyComboBox.Location = new System.Drawing.Point(100, 19);
            this.modifierKeyComboBox.Name = "modifierKeyComboBox";
            this.modifierKeyComboBox.Size = new System.Drawing.Size(166, 21);
            this.modifierKeyComboBox.TabIndex = 20;
            // 
            // hotKeyTriggerSetKeyButton
            // 
            this.hotKeyTriggerSetKeyButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.hotKeyTriggerSetKeyButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.hotKeyTriggerSetKeyButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.hotKeyTriggerSetKeyButton.Location = new System.Drawing.Point(100, 46);
            this.hotKeyTriggerSetKeyButton.Name = "hotKeyTriggerSetKeyButton";
            this.hotKeyTriggerSetKeyButton.Size = new System.Drawing.Size(127, 23);
            this.hotKeyTriggerSetKeyButton.TabIndex = 18;
            this.hotKeyTriggerSetKeyButton.Text = "Set Key";
            this.hotKeyTriggerSetKeyButton.UseVisualStyleBackColor = true;
            this.hotKeyTriggerSetKeyButton.Click += new System.EventHandler(this.hotKeyTriggerSetKeyButton_Click);
            // 
            // abilityChainNameTextBox
            // 
            this.abilityChainNameTextBox.BackColor = System.Drawing.Color.Wheat;
            this.abilityChainNameTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.abilityChainNameTextBox.Location = new System.Drawing.Point(119, 21);
            this.abilityChainNameTextBox.Name = "abilityChainNameTextBox";
            this.abilityChainNameTextBox.Size = new System.Drawing.Size(320, 20);
            this.abilityChainNameTextBox.TabIndex = 17;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 23);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(98, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Ability Chain Name:";
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.DimGray;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.removeSelectedAbilitiesButton);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.newAbilityButton);
            this.panel1.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel1.ForeColor = System.Drawing.Color.White;
            this.panel1.Location = new System.Drawing.Point(12, 177);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(528, 26);
            this.panel1.TabIndex = 33;
            // 
            // removeSelectedAbilitiesButton
            // 
            this.removeSelectedAbilitiesButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.removeSelectedAbilitiesButton.BackColor = System.Drawing.Color.Gainsboro;
            this.removeSelectedAbilitiesButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.removeSelectedAbilitiesButton.ForeColor = System.Drawing.Color.Red;
            this.removeSelectedAbilitiesButton.Location = new System.Drawing.Point(403, 1);
            this.removeSelectedAbilitiesButton.Name = "removeSelectedAbilitiesButton";
            this.removeSelectedAbilitiesButton.Size = new System.Drawing.Size(122, 23);
            this.removeSelectedAbilitiesButton.TabIndex = 30;
            this.removeSelectedAbilitiesButton.Text = "Remove Checked";
            this.removeSelectedAbilitiesButton.UseVisualStyleBackColor = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 15);
            this.label1.TabIndex = 10;
            this.label1.Text = "Abilities";
            // 
            // newAbilityButton
            // 
            this.newAbilityButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.newAbilityButton.BackColor = System.Drawing.Color.Gainsboro;
            this.newAbilityButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.newAbilityButton.ForeColor = System.Drawing.Color.Black;
            this.newAbilityButton.Location = new System.Drawing.Point(280, 1);
            this.newAbilityButton.Name = "newAbilityButton";
            this.newAbilityButton.Size = new System.Drawing.Size(122, 23);
            this.newAbilityButton.TabIndex = 3;
            this.newAbilityButton.Text = "+ Add Ability...";
            this.newAbilityButton.UseVisualStyleBackColor = false;
            // 
            // abilitiesListView
            // 
            this.abilitiesListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.abilitiesListView.CheckBoxes = true;
            this.abilitiesListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.nameColumn});
            this.abilitiesListView.FullRowSelect = true;
            this.abilitiesListView.GridLines = true;
            this.abilitiesListView.Location = new System.Drawing.Point(12, 200);
            this.abilitiesListView.MultiSelect = false;
            this.abilitiesListView.Name = "abilitiesListView";
            this.abilitiesListView.Size = new System.Drawing.Size(528, 188);
            this.abilitiesListView.TabIndex = 32;
            this.abilitiesListView.UseCompatibleStateImageBehavior = false;
            this.abilitiesListView.View = System.Windows.Forms.View.Details;
            // 
            // nameColumn
            // 
            this.nameColumn.Text = "Name";
            this.nameColumn.Width = 500;
            // 
            // AddNewAbilityChainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(552, 429);
            this.ControlBox = false;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.abilitiesListView);
            this.Controls.Add(this.panel65);
            this.Controls.Add(this.panel66);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.saveButton);
            this.Name = "AddNewAbilityChainForm";
            this.Text = "Ability Chain";
            this.Load += new System.EventHandler(this.AddNewAbilityChainForm_Load);
            this.panel65.ResumeLayout(false);
            this.panel65.PerformLayout();
            this.panel66.ResumeLayout(false);
            this.panel66.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.Panel panel65;
        private System.Windows.Forms.Label label74;
        private System.Windows.Forms.Panel panel66;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button removeSelectedAbilitiesButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button newAbilityButton;
        private System.Windows.Forms.ListView abilitiesListView;
        private System.Windows.Forms.ColumnHeader nameColumn;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button hotKeyTriggerSetKeyButton;
        public System.Windows.Forms.TextBox abilityChainNameTextBox;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox modifierKeyComboBox;
    }
}