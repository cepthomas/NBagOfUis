namespace Ephemera.NBagOfUis.Test
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
            this.txtInfo = new Ephemera.NBagOfUis.TextViewer();
            this.propGrid = new Ephemera.NBagOfUis.PropertyGridEx();
            this.clickGrid1 = new Ephemera.NBagOfUis.ClickGrid();
            this.cpuMeter1 = new Ephemera.NBagOfUis.CpuMeter();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.slider1 = new Ephemera.NBagOfUis.Slider();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnGfx = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnSettings = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnCpu = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSlider1 = new Ephemera.NBagOfUis.ToolStripSlider();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMeter1 = new Ephemera.NBagOfUis.ToolStripMeter();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.meterDots = new Ephemera.NBagOfUis.Meter();
            this.meterLog = new Ephemera.NBagOfUis.Meter();
            this.meterLinear = new Ephemera.NBagOfUis.Meter();
            this.slider2 = new Ephemera.NBagOfUis.Slider();
            this.pan1 = new Ephemera.NBagOfUis.Pan();
            this.pot1 = new Ephemera.NBagOfUis.Pot();
            this.filTree = new Ephemera.NBagOfUis.FilTree();
            this.choicer = new Ephemera.NBagOfUis.MultipleChoiceSelector();
            this.options = new Ephemera.NBagOfUis.OptionsEditor();
            this.btnDump = new System.Windows.Forms.Button();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtInfo
            // 
            this.txtInfo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtInfo.Location = new System.Drawing.Point(14, 402);
            this.txtInfo.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtInfo.MaxText = 5000;
            this.txtInfo.Name = "txtInfo";
            this.txtInfo.Prompt = "** ";
            this.txtInfo.Size = new System.Drawing.Size(470, 159);
            this.txtInfo.TabIndex = 5;
            this.txtInfo.WordWrap = true;
            // 
            // propGrid
            // 
            this.propGrid.Location = new System.Drawing.Point(490, 203);
            this.propGrid.Name = "propGrid";
            this.propGrid.Size = new System.Drawing.Size(274, 358);
            this.propGrid.TabIndex = 20;
            // 
            // clickGrid1
            // 
            this.clickGrid1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.clickGrid1.Location = new System.Drawing.Point(491, 53);
            this.clickGrid1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.clickGrid1.Name = "clickGrid1";
            this.clickGrid1.Size = new System.Drawing.Size(274, 143);
            this.clickGrid1.TabIndex = 1;
            // 
            // cpuMeter1
            // 
            this.cpuMeter1.BackColor = System.Drawing.Color.Gainsboro;
            this.cpuMeter1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.cpuMeter1.DrawColor = System.Drawing.Color.DarkOrange;
            this.cpuMeter1.Enable = false;
            this.cpuMeter1.Label = "cpu";
            this.cpuMeter1.Location = new System.Drawing.Point(1017, 485);
            this.cpuMeter1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cpuMeter1.Name = "cpuMeter1";
            this.cpuMeter1.Size = new System.Drawing.Size(197, 76);
            this.cpuMeter1.TabIndex = 7;
            // 
            // slider1
            // 
            this.slider1.BackColor = System.Drawing.Color.Gainsboro;
            this.slider1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.slider1.DrawColor = System.Drawing.Color.Orange;
            this.slider1.Label = "slider1";
            this.slider1.Location = new System.Drawing.Point(1017, 53);
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
            this.toolStripSeparator4,
            this.toolStripMeter1,
            this.toolStripSeparator5});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1225, 43);
            this.toolStrip1.TabIndex = 25;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btnGfx
            // 
            this.btnGfx.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnGfx.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnGfx.Name = "btnGfx";
            this.btnGfx.Size = new System.Drawing.Size(34, 40);
            this.btnGfx.Text = "gfx";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 43);
            // 
            // btnSettings
            // 
            this.btnSettings.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnSettings.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSettings.Name = "btnSettings";
            this.btnSettings.Size = new System.Drawing.Size(64, 40);
            this.btnSettings.Text = "settings";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 43);
            // 
            // btnCpu
            // 
            this.btnCpu.CheckOnClick = true;
            this.btnCpu.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnCpu.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnCpu.Name = "btnCpu";
            this.btnCpu.Size = new System.Drawing.Size(37, 40);
            this.btnCpu.Text = "cpu";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 43);
            // 
            // toolStripSlider1
            // 
            this.toolStripSlider1.AccessibleName = "toolStripSlider1";
            this.toolStripSlider1.AutoSize = false;
            this.toolStripSlider1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.toolStripSlider1.DrawColor = System.Drawing.Color.LightSeaGreen;
            this.toolStripSlider1.Label = "HOOHAA";
            this.toolStripSlider1.Maximum = 10D;
            this.toolStripSlider1.Minimum = 0D;
            this.toolStripSlider1.Name = "toolStripSlider1";
            this.toolStripSlider1.Resolution = 0.1D;
            this.toolStripSlider1.Size = new System.Drawing.Size(150, 40);
            this.toolStripSlider1.Text = "toolStripSlider1";
            this.toolStripSlider1.Value = 5D;
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 43);
            // 
            // toolStripMeter1
            // 
            this.toolStripMeter1.AutoSize = false;
            this.toolStripMeter1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.toolStripMeter1.DrawColor = System.Drawing.Color.GreenYellow;
            this.toolStripMeter1.Label = "dotty";
            this.toolStripMeter1.Maximum = 100D;
            this.toolStripMeter1.MeterType = Ephemera.NBagOfUis.MeterType.ContinuousDots;
            this.toolStripMeter1.Minimum = 0D;
            this.toolStripMeter1.Name = "toolStripMeter1";
            this.toolStripMeter1.Size = new System.Drawing.Size(150, 40);
            this.toolStripMeter1.Text = "toolStripMeter1";
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 43);
            // 
            // meterDots
            // 
            this.meterDots.BackColor = System.Drawing.Color.Gainsboro;
            this.meterDots.DrawColor = System.Drawing.Color.White;
            this.meterDots.Label = "meterDots";
            this.meterDots.Location = new System.Drawing.Point(1017, 285);
            this.meterDots.Maximum = 10D;
            this.meterDots.MeterType = Ephemera.NBagOfUis.MeterType.ContinuousDots;
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
            this.meterLog.Location = new System.Drawing.Point(1017, 354);
            this.meterLog.Maximum = 3D;
            this.meterLog.MeterType = Ephemera.NBagOfUis.MeterType.Log;
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
            this.meterLinear.Location = new System.Drawing.Point(1017, 420);
            this.meterLinear.Maximum = 100D;
            this.meterLinear.MeterType = Ephemera.NBagOfUis.MeterType.Linear;
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
            this.slider2.Location = new System.Drawing.Point(1017, 112);
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
            this.pan1.Location = new System.Drawing.Point(1092, 229);
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
            this.pot1.Location = new System.Drawing.Point(1092, 112);
            this.pot1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pot1.Maximum = 100D;
            this.pot1.Minimum = 0D;
            this.pot1.Name = "pot1";
            this.pot1.Resolution = 5D;
            this.pot1.Size = new System.Drawing.Size(122, 107);
            this.pot1.TabIndex = 31;
            this.pot1.Taper = Ephemera.NBagOfUis.Taper.Linear;
            this.pot1.Value = 50D;
            // 
            // filTree
            // 
            this.filTree.Location = new System.Drawing.Point(12, 53);
            this.filTree.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.filTree.Name = "filTree";
            this.filTree.Size = new System.Drawing.Size(472, 340);
            this.filTree.TabIndex = 32;
            // 
            // choicer
            // 
            this.choicer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.choicer.Location = new System.Drawing.Point(771, 285);
            this.choicer.Name = "choicer";
            this.choicer.Size = new System.Drawing.Size(211, 196);
            this.choicer.TabIndex = 33;
            // 
            // options
            // 
            this.options.AllowEdit = false;
            this.options.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.options.Location = new System.Drawing.Point(771, 93);
            this.options.Name = "options";
            this.options.Size = new System.Drawing.Size(211, 182);
            this.options.TabIndex = 34;
            // 
            // btnDump
            // 
            this.btnDump.Location = new System.Drawing.Point(771, 53);
            this.btnDump.Name = "btnDump";
            this.btnDump.Size = new System.Drawing.Size(211, 29);
            this.btnDump.TabIndex = 35;
            this.btnDump.Text = "Dump these";
            this.btnDump.UseVisualStyleBackColor = true;
            // 
            // TestHost
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1225, 571);
            this.Controls.Add(this.btnDump);
            this.Controls.Add(this.options);
            this.Controls.Add(this.choicer);
            this.Controls.Add(this.filTree);
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
        private CpuMeter cpuMeter1;
        private Meter meterDots;
        private Meter meterLog;
        private Meter meterLinear;
        private Slider slider1;
        private Slider slider2;
        private Pan pan1;
        private Pot pot1;
        private ToolStripSlider toolStripSlider1;
        private ToolStripMeter toolStripMeter1;

        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btnGfx;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton btnSettings;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton btnCpu;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private FilTree filTree;
        private MultipleChoiceSelector choicer;
        private OptionsEditor options;
        private System.Windows.Forms.Button btnDump;
    }
}