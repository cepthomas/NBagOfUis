namespace NBagOfUis
{
    partial class FilTree
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
            this.treeView = new System.Windows.Forms.TreeView();
            this.lbFiles = new System.Windows.Forms.ListBox();
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.lblActiveFilters = new System.Windows.Forms.ToolStripLabel();
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnEdit = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // treeView
            // 
            this.treeView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.treeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView.Location = new System.Drawing.Point(0, 0);
            this.treeView.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.treeView.Name = "treeView";
            this.treeView.Size = new System.Drawing.Size(262, 402);
            this.treeView.TabIndex = 0;
            this.treeView.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.TreeView_NodeMouseClick);
            // 
            // lbFiles
            // 
            this.lbFiles.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lbFiles.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbFiles.ItemHeight = 20;
            this.lbFiles.Location = new System.Drawing.Point(0, 0);
            this.lbFiles.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.lbFiles.Name = "lbFiles";
            this.lbFiles.Size = new System.Drawing.Size(334, 402);
            this.lbFiles.TabIndex = 1;
            // 
            // toolStrip
            // 
            this.toolStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblActiveFilters,
            this.toolStripSeparator1,
            this.btnEdit,
            this.toolStripSeparator2});
            this.toolStrip.Location = new System.Drawing.Point(0, 0);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Size = new System.Drawing.Size(600, 27);
            this.toolStrip.TabIndex = 3;
            this.toolStrip.Text = "toolStrip";
            // 
            // lblActiveFilters
            // 
            this.lblActiveFilters.Name = "lblActiveFilters";
            this.lblActiveFilters.Size = new System.Drawing.Size(93, 24);
            this.lblActiveFilters.Text = "Active Filters";
            // 
            // splitContainer
            // 
            this.splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer.Location = new System.Drawing.Point(0, 27);
            this.splitContainer.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.splitContainer.Name = "splitContainer";
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.Controls.Add(this.treeView);
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.Controls.Add(this.lbFiles);
            this.splitContainer.Size = new System.Drawing.Size(600, 402);
            this.splitContainer.SplitterDistance = 262;
            this.splitContainer.TabIndex = 4;
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 27);
            // 
            // btnEdit
            // 
            this.btnEdit.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnEdit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(39, 24);
            this.btnEdit.Text = "Edit";
            this.btnEdit.ToolTipText = "Edit filters and more";
            this.btnEdit.Click += new System.EventHandler(this.Edit_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 27);
            // 
            // FilTree
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer);
            this.Controls.Add(this.toolStrip);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "FilTree";
            this.Size = new System.Drawing.Size(600, 429);
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
            this.splitContainer.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.ToolStripLabel lblActiveFilters;
        private System.Windows.Forms.ToolStripButton btnEdit;
        private System.Windows.Forms.TreeView treeView;
        private System.Windows.Forms.ListBox lbFiles;

        private System.Windows.Forms.SplitContainer splitContainer;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
    }
}
