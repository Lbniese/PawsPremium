namespace Paws.Interface.Controls
{
    partial class TwoRadioOptionsControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.OptionTwoRadioButton = new System.Windows.Forms.RadioButton();
            this.ValueLabel = new System.Windows.Forms.Label();
            this.OptionOneRadioButton = new System.Windows.Forms.RadioButton();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 95F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.87622F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 66.12378F));
            this.tableLayoutPanel1.Controls.Add(this.OptionTwoRadioButton, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.ValueLabel, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.OptionOneRadioButton, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(402, 25);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // OptionTwoRadioButton
            // 
            this.OptionTwoRadioButton.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.OptionTwoRadioButton.AutoSize = true;
            this.OptionTwoRadioButton.Location = new System.Drawing.Point(201, 4);
            this.OptionTwoRadioButton.Name = "OptionTwoRadioButton";
            this.OptionTwoRadioButton.Size = new System.Drawing.Size(80, 17);
            this.OptionTwoRadioButton.TabIndex = 2;
            this.OptionTwoRadioButton.Text = "Option Two";
            this.OptionTwoRadioButton.UseVisualStyleBackColor = true;
            // 
            // ValueLabel
            // 
            this.ValueLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.ValueLabel.AutoSize = true;
            this.ValueLabel.Location = new System.Drawing.Point(3, 6);
            this.ValueLabel.Name = "ValueLabel";
            this.ValueLabel.Size = new System.Drawing.Size(34, 13);
            this.ValueLabel.TabIndex = 0;
            this.ValueLabel.Text = "Value";
            // 
            // OptionOneRadioButton
            // 
            this.OptionOneRadioButton.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.OptionOneRadioButton.AutoSize = true;
            this.OptionOneRadioButton.Checked = true;
            this.OptionOneRadioButton.Location = new System.Drawing.Point(98, 4);
            this.OptionOneRadioButton.Name = "OptionOneRadioButton";
            this.OptionOneRadioButton.Size = new System.Drawing.Size(79, 17);
            this.OptionOneRadioButton.TabIndex = 1;
            this.OptionOneRadioButton.TabStop = true;
            this.OptionOneRadioButton.Text = "Option One";
            this.OptionOneRadioButton.UseVisualStyleBackColor = true;
            // 
            // TwoRadioOptionsControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "TwoRadioOptionsControl";
            this.Size = new System.Drawing.Size(402, 25);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        public System.Windows.Forms.RadioButton OptionTwoRadioButton;
        public System.Windows.Forms.Label ValueLabel;
        public System.Windows.Forms.RadioButton OptionOneRadioButton;
    }
}
