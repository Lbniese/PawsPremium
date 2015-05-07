namespace Paws.Interface.Controls
{
    partial class AbilityChainsControl
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AbilityChainsControl));
            this.panel46 = new System.Windows.Forms.Panel();
            this.label95 = new System.Windows.Forms.Label();
            this.abilityChainsAddNewAbilityChainButton = new System.Windows.Forms.Button();
            this.abilityChainsListView = new System.Windows.Forms.ListView();
            this.nameColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.enabledColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.hotkeyColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.abilitiesColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.abilityChainsListViewContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.editItemToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.removeItemToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.removeSelectedItemsButton = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.panel46.SuspendLayout();
            this.abilityChainsListViewContextMenu.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel46
            // 
            this.panel46.BackColor = System.Drawing.Color.DimGray;
            this.panel46.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel46.Controls.Add(this.label95);
            this.panel46.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel46.ForeColor = System.Drawing.Color.White;
            this.panel46.Location = new System.Drawing.Point(3, 3);
            this.panel46.Name = "panel46";
            this.panel46.Size = new System.Drawing.Size(764, 26);
            this.panel46.TabIndex = 30;
            // 
            // label95
            // 
            this.label95.AutoSize = true;
            this.label95.Location = new System.Drawing.Point(4, 4);
            this.label95.Name = "label95";
            this.label95.Size = new System.Drawing.Size(83, 15);
            this.label95.TabIndex = 10;
            this.label95.Text = "Ability Chains";
            // 
            // abilityChainsAddNewAbilityChainButton
            // 
            this.abilityChainsAddNewAbilityChainButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.abilityChainsAddNewAbilityChainButton.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.abilityChainsAddNewAbilityChainButton.Location = new System.Drawing.Point(3, 97);
            this.abilityChainsAddNewAbilityChainButton.Name = "abilityChainsAddNewAbilityChainButton";
            this.abilityChainsAddNewAbilityChainButton.Size = new System.Drawing.Size(184, 23);
            this.abilityChainsAddNewAbilityChainButton.TabIndex = 29;
            this.abilityChainsAddNewAbilityChainButton.Text = "+ Add New Chain...";
            this.abilityChainsAddNewAbilityChainButton.UseVisualStyleBackColor = true;
            this.abilityChainsAddNewAbilityChainButton.Click += new System.EventHandler(this.abilityChainsAddNewAbilityChainButton_Click);
            // 
            // abilityChainsListView
            // 
            this.abilityChainsListView.CheckBoxes = true;
            this.abilityChainsListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.nameColumnHeader,
            this.enabledColumnHeader,
            this.hotkeyColumnHeader,
            this.abilitiesColumnHeader});
            this.abilityChainsListView.ContextMenuStrip = this.abilityChainsListViewContextMenu;
            this.abilityChainsListView.FullRowSelect = true;
            this.abilityChainsListView.GridLines = true;
            this.abilityChainsListView.Location = new System.Drawing.Point(3, 126);
            this.abilityChainsListView.MultiSelect = false;
            this.abilityChainsListView.Name = "abilityChainsListView";
            this.abilityChainsListView.Size = new System.Drawing.Size(764, 316);
            this.abilityChainsListView.TabIndex = 28;
            this.abilityChainsListView.UseCompatibleStateImageBehavior = false;
            this.abilityChainsListView.View = System.Windows.Forms.View.Details;
            // 
            // nameColumnHeader
            // 
            this.nameColumnHeader.Text = "Ability Chain Name";
            this.nameColumnHeader.Width = 200;
            // 
            // enabledColumnHeader
            // 
            this.enabledColumnHeader.Text = "Spec";
            this.enabledColumnHeader.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.enabledColumnHeader.Width = 87;
            // 
            // hotkeyColumnHeader
            // 
            this.hotkeyColumnHeader.Text = "Hot Key Trigger";
            this.hotkeyColumnHeader.Width = 124;
            // 
            // abilitiesColumnHeader
            // 
            this.abilitiesColumnHeader.Text = "Abilities";
            this.abilitiesColumnHeader.Width = 318;
            // 
            // abilityChainsListViewContextMenu
            // 
            this.abilityChainsListViewContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.editItemToolStripMenuItem,
            this.removeItemToolStripMenuItem});
            this.abilityChainsListViewContextMenu.Name = "itemsListViewContextMenu";
            this.abilityChainsListViewContextMenu.Size = new System.Drawing.Size(198, 48);
            // 
            // editItemToolStripMenuItem
            // 
            this.editItemToolStripMenuItem.Name = "editItemToolStripMenuItem";
            this.editItemToolStripMenuItem.Size = new System.Drawing.Size(197, 22);
            this.editItemToolStripMenuItem.Text = "Edit Ability Chain...";
            this.editItemToolStripMenuItem.Click += new System.EventHandler(this.editItemToolStripMenuItem_Click);
            // 
            // removeItemToolStripMenuItem
            // 
            this.removeItemToolStripMenuItem.Name = "removeItemToolStripMenuItem";
            this.removeItemToolStripMenuItem.Size = new System.Drawing.Size(197, 22);
            this.removeItemToolStripMenuItem.Text = "Remove Ability Chain...";
            this.removeItemToolStripMenuItem.Click += new System.EventHandler(this.removeItemToolStripMenuItem_Click);
            // 
            // removeSelectedItemsButton
            // 
            this.removeSelectedItemsButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.removeSelectedItemsButton.ForeColor = System.Drawing.Color.Red;
            this.removeSelectedItemsButton.Location = new System.Drawing.Point(583, 97);
            this.removeSelectedItemsButton.Name = "removeSelectedItemsButton";
            this.removeSelectedItemsButton.Size = new System.Drawing.Size(184, 23);
            this.removeSelectedItemsButton.TabIndex = 31;
            this.removeSelectedItemsButton.Text = "Remove Checked Items";
            this.removeSelectedItemsButton.UseVisualStyleBackColor = true;
            this.removeSelectedItemsButton.Click += new System.EventHandler(this.removeSelectedItemsButton_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.LightGray;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.richTextBox1);
            this.panel1.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel1.ForeColor = System.Drawing.Color.DimGray;
            this.panel1.Location = new System.Drawing.Point(3, 32);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(764, 59);
            this.panel1.TabIndex = 31;
            // 
            // richTextBox1
            // 
            this.richTextBox1.BackColor = System.Drawing.Color.LightGray;
            this.richTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBox1.Font = new System.Drawing.Font("Arial", 7F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.richTextBox1.Location = new System.Drawing.Point(3, 4);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.ReadOnly = true;
            this.richTextBox1.Size = new System.Drawing.Size(756, 50);
            this.richTextBox1.TabIndex = 0;
            this.richTextBox1.Text = resources.GetString("richTextBox1.Text");
            // 
            // AbilityChainsControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.removeSelectedItemsButton);
            this.Controls.Add(this.panel46);
            this.Controls.Add(this.abilityChainsAddNewAbilityChainButton);
            this.Controls.Add(this.abilityChainsListView);
            this.Font = new System.Drawing.Font("Arial", 9F);
            this.Name = "AbilityChainsControl";
            this.Padding = new System.Windows.Forms.Padding(3);
            this.Size = new System.Drawing.Size(771, 445);
            this.Load += new System.EventHandler(this.AbilityChainsControl_Load);
            this.panel46.ResumeLayout(false);
            this.panel46.PerformLayout();
            this.abilityChainsListViewContextMenu.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel46;
        private System.Windows.Forms.Label label95;
        private System.Windows.Forms.Button abilityChainsAddNewAbilityChainButton;
        private System.Windows.Forms.ListView abilityChainsListView;
        private System.Windows.Forms.ColumnHeader nameColumnHeader;
        private System.Windows.Forms.ColumnHeader enabledColumnHeader;
        private System.Windows.Forms.ColumnHeader hotkeyColumnHeader;
        private System.Windows.Forms.ColumnHeader abilitiesColumnHeader;
        private System.Windows.Forms.Button removeSelectedItemsButton;
        private System.Windows.Forms.ContextMenuStrip abilityChainsListViewContextMenu;
        private System.Windows.Forms.ToolStripMenuItem editItemToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem removeItemToolStripMenuItem;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RichTextBox richTextBox1;

    }
}
