namespace Paws.Interface
{
    partial class AddNewItemForm
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
            this.saveButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.conditionsListView = new System.Windows.Forms.ListView();
            this.nameColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.newConditionButton = new System.Windows.Forms.Button();
            this.panel65 = new System.Windows.Forms.Panel();
            this.label74 = new System.Windows.Forms.Label();
            this.panel66 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.myStateComboBox = new System.Windows.Forms.ComboBox();
            this.myBagsButton = new System.Windows.Forms.Button();
            this.itemNameTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.removeSelectedConditionsButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.panel65.SuspendLayout();
            this.panel66.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // saveButton
            // 
            this.saveButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.saveButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.saveButton.Location = new System.Drawing.Point(430, 281);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(122, 23);
            this.saveButton.TabIndex = 0;
            this.saveButton.Text = "Save";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cancelButton.Location = new System.Drawing.Point(302, 281);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(122, 23);
            this.cancelButton.TabIndex = 1;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            // 
            // conditionsListView
            // 
            this.conditionsListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.conditionsListView.CheckBoxes = true;
            this.conditionsListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.nameColumn});
            this.conditionsListView.FullRowSelect = true;
            this.conditionsListView.GridLines = true;
            this.conditionsListView.Location = new System.Drawing.Point(12, 149);
            this.conditionsListView.MultiSelect = false;
            this.conditionsListView.Name = "conditionsListView";
            this.conditionsListView.Size = new System.Drawing.Size(540, 120);
            this.conditionsListView.TabIndex = 2;
            this.conditionsListView.UseCompatibleStateImageBehavior = false;
            this.conditionsListView.View = System.Windows.Forms.View.Details;
            // 
            // nameColumn
            // 
            this.nameColumn.Text = "Name";
            this.nameColumn.Width = 500;
            // 
            // newConditionButton
            // 
            this.newConditionButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.newConditionButton.BackColor = System.Drawing.Color.Gainsboro;
            this.newConditionButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.newConditionButton.ForeColor = System.Drawing.Color.Black;
            this.newConditionButton.Location = new System.Drawing.Point(292, 1);
            this.newConditionButton.Name = "newConditionButton";
            this.newConditionButton.Size = new System.Drawing.Size(122, 23);
            this.newConditionButton.TabIndex = 3;
            this.newConditionButton.Text = "+ New Condition...";
            this.newConditionButton.UseVisualStyleBackColor = false;
            this.newConditionButton.Click += new System.EventHandler(this.newConditionButton_Click);
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
            this.panel65.Size = new System.Drawing.Size(540, 26);
            this.panel65.TabIndex = 28;
            // 
            // label74
            // 
            this.label74.AutoSize = true;
            this.label74.Location = new System.Drawing.Point(4, 4);
            this.label74.Name = "label74";
            this.label74.Size = new System.Drawing.Size(60, 15);
            this.label74.TabIndex = 10;
            this.label74.Text = "New Item";
            // 
            // panel66
            // 
            this.panel66.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel66.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panel66.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel66.Controls.Add(this.label3);
            this.panel66.Controls.Add(this.myStateComboBox);
            this.panel66.Controls.Add(this.myBagsButton);
            this.panel66.Controls.Add(this.itemNameTextBox);
            this.panel66.Controls.Add(this.label2);
            this.panel66.Location = new System.Drawing.Point(12, 30);
            this.panel66.Name = "panel66";
            this.panel66.Size = new System.Drawing.Size(540, 87);
            this.panel66.TabIndex = 29;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(25, 50);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(52, 13);
            this.label3.TabIndex = 32;
            this.label3.Text = "My State:";
            // 
            // myStateComboBox
            // 
            this.myStateComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.myStateComboBox.FormattingEnabled = true;
            this.myStateComboBox.Items.AddRange(new object[] {
            "Not In Combat",
            "In Combat",
            "Combat Healing",
            "Resting"});
            this.myStateComboBox.Location = new System.Drawing.Point(83, 47);
            this.myStateComboBox.Name = "myStateComboBox";
            this.myStateComboBox.Size = new System.Drawing.Size(223, 21);
            this.myStateComboBox.TabIndex = 31;
            // 
            // myBagsButton
            // 
            this.myBagsButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.myBagsButton.Location = new System.Drawing.Point(456, 19);
            this.myBagsButton.Name = "myBagsButton";
            this.myBagsButton.Size = new System.Drawing.Size(70, 23);
            this.myBagsButton.TabIndex = 30;
            this.myBagsButton.Text = "My Bags...";
            this.myBagsButton.UseVisualStyleBackColor = true;
            this.myBagsButton.Click += new System.EventHandler(this.myBagsButton_Click);
            // 
            // itemNameTextBox
            // 
            this.itemNameTextBox.BackColor = System.Drawing.Color.Wheat;
            this.itemNameTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.itemNameTextBox.Location = new System.Drawing.Point(83, 21);
            this.itemNameTextBox.Name = "itemNameTextBox";
            this.itemNameTextBox.Size = new System.Drawing.Size(367, 20);
            this.itemNameTextBox.TabIndex = 17;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 23);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Item Name:";
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.DimGray;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.removeSelectedConditionsButton);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.newConditionButton);
            this.panel1.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel1.ForeColor = System.Drawing.Color.White;
            this.panel1.Location = new System.Drawing.Point(12, 123);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(540, 26);
            this.panel1.TabIndex = 29;
            // 
            // removeSelectedConditionsButton
            // 
            this.removeSelectedConditionsButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.removeSelectedConditionsButton.BackColor = System.Drawing.Color.Gainsboro;
            this.removeSelectedConditionsButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.removeSelectedConditionsButton.ForeColor = System.Drawing.Color.Red;
            this.removeSelectedConditionsButton.Location = new System.Drawing.Point(415, 1);
            this.removeSelectedConditionsButton.Name = "removeSelectedConditionsButton";
            this.removeSelectedConditionsButton.Size = new System.Drawing.Size(122, 23);
            this.removeSelectedConditionsButton.TabIndex = 30;
            this.removeSelectedConditionsButton.Text = "Remove Checked";
            this.removeSelectedConditionsButton.UseVisualStyleBackColor = false;
            this.removeSelectedConditionsButton.Click += new System.EventHandler(this.removeSelectedConditionsButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 15);
            this.label1.TabIndex = 10;
            this.label1.Text = "Conditions";
            // 
            // AddNewItemForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(564, 319);
            this.ControlBox = false;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel65);
            this.Controls.Add(this.panel66);
            this.Controls.Add(this.conditionsListView);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.saveButton);
            this.Name = "AddNewItemForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Item";
            this.panel65.ResumeLayout(false);
            this.panel65.PerformLayout();
            this.panel66.ResumeLayout(false);
            this.panel66.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.ListView conditionsListView;
        private System.Windows.Forms.Button newConditionButton;
        private System.Windows.Forms.Panel panel65;
        private System.Windows.Forms.Label label74;
        private System.Windows.Forms.Panel panel66;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button myBagsButton;
        private System.Windows.Forms.TextBox itemNameTextBox;
        private System.Windows.Forms.ColumnHeader nameColumn;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox myStateComboBox;
        private System.Windows.Forms.Button removeSelectedConditionsButton;
    }
}