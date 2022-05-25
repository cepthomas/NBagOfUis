namespace NBagOfUis.Test
{
    partial class TestHost
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
            this.txtInfo = new NBagOfUis.TextViewer();
            this.btnSettings = new System.Windows.Forms.Button();
            this.btnGraphics = new System.Windows.Forms.Button();
            this.propGrid = new NBagOfUis.PropertyGridEx();
            this.clickGrid1 = new NBagOfUis.ClickGrid();
            this.chkCpu = new System.Windows.Forms.CheckBox();
            this.ftree = new NBagOfUis.FilTree();
            this.cpuMeter1 = new NBagOfUis.CpuMeter();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.slider1 = new NBagOfUis.Slider();
            this.lblGfx = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txtInfo
            // 
            this.txtInfo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtInfo.Location = new System.Drawing.Point(841, 401);
            this.txtInfo.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtInfo.MaxText = 5000;
            this.txtInfo.Name = "txtInfo";
            this.txtInfo.Size = new System.Drawing.Size(386, 391);
            this.txtInfo.TabIndex = 5;
            this.txtInfo.WordWrap = true;
            // 
            // btnSettings
            // 
            this.btnSettings.Location = new System.Drawing.Point(857, 290);
            this.btnSettings.Name = "btnSettings";
            this.btnSettings.Size = new System.Drawing.Size(61, 60);
            this.btnSettings.TabIndex = 22;
            this.btnSettings.Text = "Settings";
            this.btnSettings.UseVisualStyleBackColor = true;
            this.btnSettings.Click += new System.EventHandler(this.Settings_Click);
            // 
            // btnGraphics
            // 
            this.btnGraphics.Location = new System.Drawing.Point(857, 219);
            this.btnGraphics.Name = "btnGraphics";
            this.btnGraphics.Size = new System.Drawing.Size(61, 60);
            this.btnGraphics.TabIndex = 21;
            this.btnGraphics.Text = "Run GFX";
            this.btnGraphics.UseVisualStyleBackColor = true;
            // 
            // propGrid
            // 
            this.propGrid.Dirty = false;
            this.propGrid.Location = new System.Drawing.Point(501, 14);
            this.propGrid.Name = "propGrid";
            this.propGrid.Size = new System.Drawing.Size(337, 364);
            this.propGrid.TabIndex = 20;
            // 
            // clickGrid1
            // 
            this.clickGrid1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.clickGrid1.Location = new System.Drawing.Point(857, 13);
            this.clickGrid1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.clickGrid1.Name = "clickGrid1";
            this.clickGrid1.Size = new System.Drawing.Size(275, 156);
            this.clickGrid1.TabIndex = 1;
            // 
            // chkCpu
            // 
            this.chkCpu.AutoSize = true;
            this.chkCpu.Location = new System.Drawing.Point(857, 178);
            this.chkCpu.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.chkCpu.Name = "chkCpu";
            this.chkCpu.Size = new System.Drawing.Size(55, 24);
            this.chkCpu.TabIndex = 12;
            this.chkCpu.Text = "cpu";
            this.chkCpu.UseVisualStyleBackColor = true;
            this.chkCpu.CheckedChanged += new System.EventHandler(this.ChkCpu_CheckedChanged);
            // 
            // ftree
            // 
            this.ftree.Location = new System.Drawing.Point(1, 14);
            this.ftree.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.ftree.Name = "ftree";
            this.ftree.SingleClickSelect = true;
            this.ftree.Size = new System.Drawing.Size(483, 364);
            this.ftree.TabIndex = 11;
            this.ftree.FileSelectedEvent += new System.EventHandler<string>(this.FilTree_FileSelectedEvent);
            // 
            // cpuMeter1
            // 
            this.cpuMeter1.BackColor = System.Drawing.Color.Gainsboro;
            this.cpuMeter1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.cpuMeter1.DrawColor = System.Drawing.Color.DarkOrange;
            this.cpuMeter1.Enable = false;
            this.cpuMeter1.Label = "cpu";
            this.cpuMeter1.Location = new System.Drawing.Point(935, 178);
            this.cpuMeter1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cpuMeter1.Name = "cpuMeter1";
            this.cpuMeter1.Size = new System.Drawing.Size(197, 60);
            this.cpuMeter1.TabIndex = 7;
            // 
            // slider1
            // 
            this.slider1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.slider1.DrawColor = System.Drawing.Color.White;
            this.slider1.Label = "";
            this.slider1.Location = new System.Drawing.Point(935, 245);
            this.slider1.Maximum = 10D;
            this.slider1.Minimum = 0D;
            this.slider1.Name = "slider1";
            this.slider1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.slider1.Resolution = 0.1D;
            this.slider1.Size = new System.Drawing.Size(197, 60);
            this.slider1.TabIndex = 23;
            this.slider1.Value = 5D;
            // 
            // lblGfx
            // 
            this.lblGfx.AutoSize = true;
            this.lblGfx.Location = new System.Drawing.Point(9, 385);
            this.lblGfx.Name = "lblGfx";
            this.lblGfx.Size = new System.Drawing.Size(68, 20);
            this.lblGfx.TabIndex = 24;
            this.lblGfx.Text = "GFX here";
            // 
            // TestHost
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1240, 799);
            this.Controls.Add(this.lblGfx);
            this.Controls.Add(this.slider1);
            this.Controls.Add(this.btnSettings);
            this.Controls.Add(this.clickGrid1);
            this.Controls.Add(this.btnGraphics);
            this.Controls.Add(this.txtInfo);
            this.Controls.Add(this.cpuMeter1);
            this.Controls.Add(this.chkCpu);
            this.Controls.Add(this.propGrid);
            this.Controls.Add(this.ftree);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "TestHost";
            this.Text = "TestHost";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private TextViewer txtInfo;
        private System.Windows.Forms.Button btnSettings;
        private System.Windows.Forms.Button btnGraphics;
        private PropertyGridEx propGrid;
        private ClickGrid clickGrid1;
        private System.Windows.Forms.CheckBox chkCpu;
        private FilTree ftree;
        private CpuMeter cpuMeter1;
        private System.Windows.Forms.Timer timer1;
        private Slider slider1;
        private System.Windows.Forms.Label lblGfx;
    }
}