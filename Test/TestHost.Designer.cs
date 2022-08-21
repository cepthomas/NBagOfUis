﻿namespace NBagOfUis.Test
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
            this.propGrid = new NBagOfUis.PropertyGridEx();
            this.clickGrid1 = new NBagOfUis.ClickGrid();
            this.ftree = new NBagOfUis.FilTree();
            this.cpuMeter1 = new NBagOfUis.CpuMeter();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.slider1 = new NBagOfUis.Slider();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnGfx = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnSettings = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnCpu = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.meterDots = new NBagOfUis.Meter();
            this.meterLog = new NBagOfUis.Meter();
            this.meterLinear = new NBagOfUis.Meter();
            this.slider2 = new NBagOfUis.Slider();
            this.pan1 = new NBagOfUis.Pan();
            this.pot1 = new NBagOfUis.Pot();
            this.toolStripSlider1 = new NBagOfUis.ToolStripSlider();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtInfo
            // 
            this.txtInfo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtInfo.Location = new System.Drawing.Point(1, 445);
            this.txtInfo.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtInfo.MaxText = 5000;
            this.txtInfo.Name = "txtInfo";
            this.txtInfo.Prompt = "** ";
            this.txtInfo.Size = new System.Drawing.Size(483, 222);
            this.txtInfo.TabIndex = 5;
            this.txtInfo.WordWrap = true;
            // 
            // propGrid
            // 
            this.propGrid.Dirty = false;
            this.propGrid.Location = new System.Drawing.Point(491, 309);
            this.propGrid.Name = "propGrid";
            this.propGrid.Size = new System.Drawing.Size(274, 358);
            this.propGrid.TabIndex = 20;
            // 
            // clickGrid1
            // 
            this.clickGrid1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.clickGrid1.Location = new System.Drawing.Point(491, 40);
            this.clickGrid1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.clickGrid1.Name = "clickGrid1";
            this.clickGrid1.Size = new System.Drawing.Size(274, 143);
            this.clickGrid1.TabIndex = 1;
            // 
            // ftree
            // 
            this.ftree.Location = new System.Drawing.Point(1, 40);
            this.ftree.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.ftree.Name = "ftree";
            this.ftree.SingleClickSelect = true;
            this.ftree.Size = new System.Drawing.Size(483, 395);
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
            this.cpuMeter1.Location = new System.Drawing.Point(491, 207);
            this.cpuMeter1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cpuMeter1.Name = "cpuMeter1";
            this.cpuMeter1.Size = new System.Drawing.Size(274, 71);
            this.cpuMeter1.TabIndex = 7;
            // 
            // slider1
            // 
            this.slider1.BackColor = System.Drawing.Color.Gainsboro;
            this.slider1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.slider1.DrawColor = System.Drawing.Color.Orange;
            this.slider1.Label = "slider1";
            this.slider1.Location = new System.Drawing.Point(787, 40);
            this.slider1.Maximum = 1D;
            this.slider1.Minimum = 0D;
            this.slider1.Name = "slider1";
            this.slider1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.slider1.Resolution = 0.1D;
            this.slider1.Size = new System.Drawing.Size(197, 51);
            this.slider1.TabIndex = 23;
            this.slider1.Value = 0.30000000000000004D;
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnGfx,
            this.toolStripSeparator1,
            this.btnSettings,
            this.toolStripSeparator2,
            this.btnCpu,
            this.toolStripSeparator3,
            this.toolStripSlider1,
            this.toolStripSeparator4});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1000, 33);
            this.toolStrip1.TabIndex = 25;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btnGfx
            // 
            this.btnGfx.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnGfx.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnGfx.Name = "btnGfx";
            this.btnGfx.Size = new System.Drawing.Size(34, 30);
            this.btnGfx.Text = "gfx";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 33);
            // 
            // btnSettings
            // 
            this.btnSettings.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnSettings.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSettings.Name = "btnSettings";
            this.btnSettings.Size = new System.Drawing.Size(64, 30);
            this.btnSettings.Text = "settings";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 33);
            // 
            // btnCpu
            // 
            this.btnCpu.CheckOnClick = true;
            this.btnCpu.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnCpu.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnCpu.Name = "btnCpu";
            this.btnCpu.Size = new System.Drawing.Size(37, 30);
            this.btnCpu.Text = "cpu";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 33);
            // 
            // meterDots
            // 
            this.meterDots.BackColor = System.Drawing.Color.Gainsboro;
            this.meterDots.DrawColor = System.Drawing.Color.White;
            this.meterDots.Label = "meterDots";
            this.meterDots.Location = new System.Drawing.Point(787, 289);
            this.meterDots.Maximum = 10D;
            this.meterDots.MeterType = NBagOfUis.MeterType.ContinuousDots;
            this.meterDots.Minimum = 0D;
            this.meterDots.Name = "meterDots";
            this.meterDots.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.meterDots.Size = new System.Drawing.Size(197, 63);
            this.meterDots.TabIndex = 26;
            // 
            // meterLog
            // 
            this.meterLog.BackColor = System.Drawing.Color.Gainsboro;
            this.meterLog.DrawColor = System.Drawing.Color.Azure;
            this.meterLog.Label = "meter log";
            this.meterLog.Location = new System.Drawing.Point(787, 369);
            this.meterLog.Maximum = 3D;
            this.meterLog.MeterType = NBagOfUis.MeterType.Log;
            this.meterLog.Minimum = -60D;
            this.meterLog.Name = "meterLog";
            this.meterLog.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.meterLog.Size = new System.Drawing.Size(197, 59);
            this.meterLog.TabIndex = 27;
            // 
            // meterLinear
            // 
            this.meterLinear.BackColor = System.Drawing.Color.Gainsboro;
            this.meterLinear.DrawColor = System.Drawing.Color.White;
            this.meterLinear.Label = "meterLinear";
            this.meterLinear.Location = new System.Drawing.Point(787, 445);
            this.meterLinear.Maximum = 100D;
            this.meterLinear.MeterType = NBagOfUis.MeterType.Linear;
            this.meterLinear.Minimum = 0D;
            this.meterLinear.Name = "meterLinear";
            this.meterLinear.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.meterLinear.Size = new System.Drawing.Size(197, 59);
            this.meterLinear.TabIndex = 28;
            // 
            // slider2
            // 
            this.slider2.BackColor = System.Drawing.Color.Gainsboro;
            this.slider2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.slider2.DrawColor = System.Drawing.Color.SlateBlue;
            this.slider2.Label = "SL2";
            this.slider2.Location = new System.Drawing.Point(787, 99);
            this.slider2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.slider2.Maximum = 19D;
            this.slider2.Minimum = 2D;
            this.slider2.Name = "slider2";
            this.slider2.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.slider2.Resolution = 0.1D;
            this.slider2.Size = new System.Drawing.Size(67, 163);
            this.slider2.TabIndex = 29;
            this.slider2.Value = 7D;
            // 
            // pan1
            // 
            this.pan1.BackColor = System.Drawing.Color.Gainsboro;
            this.pan1.DrawColor = System.Drawing.Color.Crimson;
            this.pan1.Location = new System.Drawing.Point(862, 216);
            this.pan1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pan1.Name = "pan1";
            this.pan1.Size = new System.Drawing.Size(122, 46);
            this.pan1.TabIndex = 30;
            this.pan1.Value = 0.1D;
            // 
            // pot1
            // 
            this.pot1.BackColor = System.Drawing.Color.Gainsboro;
            this.pot1.DrawColor = System.Drawing.Color.Green;
            this.pot1.ForeColor = System.Drawing.Color.Black;
            this.pot1.Label = "p99";
            this.pot1.Location = new System.Drawing.Point(862, 99);
            this.pot1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pot1.Maximum = 100D;
            this.pot1.Minimum = 0D;
            this.pot1.Name = "pot1";
            this.pot1.Resolution = 5D;
            this.pot1.Size = new System.Drawing.Size(122, 107);
            this.pot1.TabIndex = 31;
            this.pot1.Taper = NBagOfUis.Taper.Linear;
            this.pot1.Value = 50D;
            // 
            // toolStripSlider1
            // 
            this.toolStripSlider1.AutoSize = false;
            this.toolStripSlider1.Name = "toolStripSlider1";
            this.toolStripSlider1.Size = new System.Drawing.Size(150, 30);
            // 
            // toolStripSlider1
            // 
            this.toolStripSlider1.AccessibleName = "toolStripSlider1";
            this.toolStripSlider1.DrawColor = System.Drawing.Color.White;
            this.toolStripSlider1.Label = "";
            this.toolStripSlider1.Maximum = 10D;
            this.toolStripSlider1.Minimum = 0D;
            this.toolStripSlider1.Name = "Slider";
            this.toolStripSlider1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.toolStripSlider1.Resolution = 0.1D;
            this.toolStripSlider1.Size = new System.Drawing.Size(150, 30);
            this.toolStripSlider1.Value = 5D;
            this.toolStripSlider1.Text = "toolStripSlider1";
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 33);
            // 
            // TestHost
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1000, 674);
            this.Controls.Add(this.pot1);
            this.Controls.Add(this.pan1);
            this.Controls.Add(this.slider2);
            this.Controls.Add(this.meterLinear);
            this.Controls.Add(this.meterLog);
            this.Controls.Add(this.meterDots);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.slider1);
            this.Controls.Add(this.clickGrid1);
            this.Controls.Add(this.txtInfo);
            this.Controls.Add(this.cpuMeter1);
            this.Controls.Add(this.propGrid);
            this.Controls.Add(this.ftree);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "TestHost";
            this.Text = "TestHost";
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private TextViewer txtInfo;
        private PropertyGridEx propGrid;
        private ClickGrid clickGrid1;
        private FilTree ftree;
        private CpuMeter cpuMeter1;
        private System.Windows.Forms.Timer timer1;
        private Slider slider1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btnGfx;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton btnSettings;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton btnCpu;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private Meter meterDots;
        private Meter meterLog;
        private Meter meterLinear;
        private Slider slider2;
        private Pan pan1;
        private Pot pot1;
        private ToolStripSlider toolStripSlider1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
    }
}