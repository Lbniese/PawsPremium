namespace Paws.Interface.Controls
{
    partial class NumberTextBoxValueControl
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
            this.DescriptorLabel = new System.Windows.Forms.Label();
            this.ValueTextBox = new System.Windows.Forms.TextBox();
            this.ValueLabel = new System.Windows.Forms.Label();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 95F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.DescriptorLabel, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.ValueTextBox, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.ValueLabel, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(402, 25);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // DescriptorLabel
            // 
            this.DescriptorLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.DescriptorLabel.AutoSize = true;
            this.DescriptorLabel.Location = new System.Drawing.Point(186, 6);
            this.DescriptorLabel.Name = "DescriptorLabel";
            this.DescriptorLabel.Size = new System.Drawing.Size(15, 13);
            this.DescriptorLabel.TabIndex = 18;
            this.DescriptorLabel.Text = "%";
            // 
            // ValueTextBox
            // 
            this.ValueTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.ValueTextBox.BackColor = System.Drawing.Color.Wheat;
            this.ValueTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ValueTextBox.Location = new System.Drawing.Point(98, 3);
            this.ValueTextBox.Name = "ValueTextBox";
            this.ValueTextBox.Size = new System.Drawing.Size(82, 20);
            this.ValueTextBox.TabIndex = 17;
            this.ValueTextBox.Text = "0";
            this.ValueTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
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
            // NumberTextBoxValueControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "NumberTextBoxValueControl";
            this.Size = new System.Drawing.Size(402, 25);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        public System.Windows.Forms.Label ValueLabel;
        public System.Windows.Forms.TextBox ValueTextBox;
        public System.Windows.Forms.Label DescriptorLabel;
    }
}
