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
            this.components = new System.ComponentModel.Container();
            this.treeView = new System.Windows.Forms.TreeView();
            this.menuFiles = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.lbFiles = new System.Windows.Forms.ListBox();
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.lblActiveFilters = new System.Windows.Forms.ToolStripLabel();
            this.splitContainer = new System.Windows.Forms.SplitContainer();
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
            this.treeView.ContextMenuStrip = this.menuFiles;
            this.treeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView.Location = new System.Drawing.Point(0, 0);
            this.treeView.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.treeView.Name = "treeView";
            this.treeView.Size = new System.Drawing.Size(262, 404);
            this.treeView.TabIndex = 0;
            this.treeView.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.TreeView_NodeMouseClick);
            // 
            // menuFiles
            // 
            this.menuFiles.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuFiles.Name = "menuTree";
            this.menuFiles.Size = new System.Drawing.Size(61, 4);
            this.menuFiles.Opening += new System.ComponentModel.CancelEventHandler(this.MenuFiles_Opening);
            // 
            // lbFiles
            // 
            this.lbFiles.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lbFiles.ContextMenuStrip = this.menuFiles;
            this.lbFiles.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbFiles.Location = new System.Drawing.Point(0, 0);
            this.lbFiles.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.lbFiles.Name = "lbFiles";
            this.lbFiles.Size = new System.Drawing.Size(334, 404);
            this.lbFiles.TabIndex = 1;
            // 
            // toolStrip
            // 
            this.toolStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblActiveFilters});
            this.toolStrip.Location = new System.Drawing.Point(0, 0);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Size = new System.Drawing.Size(600, 25);
            this.toolStrip.TabIndex = 3;
            this.toolStrip.Text = "toolStrip";
            // 
            // lblActiveFilters
            // 
            this.lblActiveFilters.Name = "lblActiveFilters";
            this.lblActiveFilters.Size = new System.Drawing.Size(93, 22);
            this.lblActiveFilters.Text = "Active Filters";
            // 
            // splitContainer
            // 
            this.splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer.Location = new System.Drawing.Point(0, 25);
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
            this.splitContainer.Size = new System.Drawing.Size(600, 404);
            this.splitContainer.SplitterDistance = 262;
            this.splitContainer.TabIndex = 4;
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

        private System.Windows.Forms.ContextMenuStrip menuFiles;

        private System.Windows.Forms.TreeView treeView;
        private System.Windows.Forms.ListBox lbFiles;

        private System.Windows.Forms.SplitContainer splitContainer;
    }
}
