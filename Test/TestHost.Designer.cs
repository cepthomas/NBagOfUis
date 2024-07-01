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
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TestHost));
            txtInfo = new TextViewer();
            propGrid = new PropertyGridEx();
            clickGrid1 = new ClickGrid();
            cpuMeter1 = new CpuMeter();
            timer1 = new System.Windows.Forms.Timer(components);
            slider1 = new Slider();
            toolStrip1 = new System.Windows.Forms.ToolStrip();
            btnGfx = new System.Windows.Forms.ToolStripButton();
            toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            btnSettings = new System.Windows.Forms.ToolStripButton();
            toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            btnCpu = new System.Windows.Forms.ToolStripButton();
            toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            toolStripSlider1 = new ToolStripSlider();
            toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            toolStripMeter1 = new ToolStripMeter();
            toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            meterDots = new Meter();
            meterLog = new Meter();
            meterLinear = new Meter();
            slider2 = new Slider();
            pan1 = new Pan();
            pot1 = new Pot();
            filTree = new FilTree();
            choicer = new ChoiceSelector();
            optionsEd = new OptionsEditor();
            btnDump = new System.Windows.Forms.Button();
            clickClack1 = new ClickClack();
            toolStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // txtInfo
            // 
            txtInfo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            txtInfo.Location = new System.Drawing.Point(14, 638);
            txtInfo.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            txtInfo.MaxText = 5000;
            txtInfo.Name = "txtInfo";
            txtInfo.Prompt = "** ";
            txtInfo.Size = new System.Drawing.Size(968, 155);
            txtInfo.TabIndex = 5;
            txtInfo.WordWrap = true;
            // 
            // propGrid
            // 
            propGrid.Location = new System.Drawing.Point(490, 203);
            propGrid.Name = "propGrid";
            propGrid.Size = new System.Drawing.Size(274, 420);
            propGrid.TabIndex = 20;
            // 
            // clickGrid1
            // 
            clickGrid1.BackColor = System.Drawing.Color.LightPink;
            clickGrid1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            clickGrid1.Location = new System.Drawing.Point(491, 53);
            clickGrid1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            clickGrid1.Name = "clickGrid1";
            clickGrid1.Size = new System.Drawing.Size(274, 143);
            clickGrid1.TabIndex = 1;
            // 
            // cpuMeter1
            // 
            cpuMeter1.BackColor = System.Drawing.Color.Gainsboro;
            cpuMeter1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            cpuMeter1.DrawColor = System.Drawing.Color.DarkOrange;
            cpuMeter1.Enable = false;
            cpuMeter1.Label = "cpu";
            cpuMeter1.Location = new System.Drawing.Point(1017, 485);
            cpuMeter1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            cpuMeter1.Name = "cpuMeter1";
            cpuMeter1.Size = new System.Drawing.Size(197, 76);
            cpuMeter1.TabIndex = 7;
            // 
            // slider1
            // 
            slider1.BackColor = System.Drawing.Color.Gainsboro;
            slider1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            slider1.DrawColor = System.Drawing.Color.Orange;
            slider1.Label = "slider1";
            slider1.Location = new System.Drawing.Point(1017, 53);
            slider1.Maximum = 1D;
            slider1.Minimum = 0D;
            slider1.Name = "slider1";
            slider1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            slider1.Resolution = 0.1D;
            slider1.Size = new System.Drawing.Size(197, 51);
            slider1.TabIndex = 23;
            slider1.Value = 0.30000000000000004D;
            // 
            // toolStrip1
            // 
            toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { btnGfx, toolStripSeparator1, btnSettings, toolStripSeparator2, btnCpu, toolStripSeparator3, toolStripSlider1, toolStripSeparator4, toolStripMeter1, toolStripSeparator5 });
            toolStrip1.Location = new System.Drawing.Point(0, 0);
            toolStrip1.Name = "toolStrip1";
            toolStrip1.Size = new System.Drawing.Size(1225, 43);
            toolStrip1.TabIndex = 25;
            toolStrip1.Text = "toolStrip1";
            // 
            // btnGfx
            // 
            btnGfx.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            btnGfx.ImageTransparentColor = System.Drawing.Color.Magenta;
            btnGfx.Name = "btnGfx";
            btnGfx.Size = new System.Drawing.Size(34, 40);
            btnGfx.Text = "gfx";
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new System.Drawing.Size(6, 43);
            // 
            // btnSettings
            // 
            btnSettings.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            btnSettings.ImageTransparentColor = System.Drawing.Color.Magenta;
            btnSettings.Name = "btnSettings";
            btnSettings.Size = new System.Drawing.Size(64, 40);
            btnSettings.Text = "settings";
            // 
            // toolStripSeparator2
            // 
            toolStripSeparator2.Name = "toolStripSeparator2";
            toolStripSeparator2.Size = new System.Drawing.Size(6, 43);
            // 
            // btnCpu
            // 
            btnCpu.CheckOnClick = true;
            btnCpu.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            btnCpu.ImageTransparentColor = System.Drawing.Color.Magenta;
            btnCpu.Name = "btnCpu";
            btnCpu.Size = new System.Drawing.Size(37, 40);
            btnCpu.Text = "cpu";
            // 
            // toolStripSeparator3
            // 
            toolStripSeparator3.Name = "toolStripSeparator3";
            toolStripSeparator3.Size = new System.Drawing.Size(6, 43);
            // 
            // toolStripSlider1
            // 
            toolStripSlider1.AccessibleName = "toolStripSlider1";
            toolStripSlider1.AutoSize = false;
            toolStripSlider1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            toolStripSlider1.DrawColor = System.Drawing.Color.LightSeaGreen;
            toolStripSlider1.Label = "HOOHAA";
            toolStripSlider1.Maximum = 10D;
            toolStripSlider1.Minimum = 0D;
            toolStripSlider1.Name = "toolStripSlider1";
            toolStripSlider1.Resolution = 0.1D;
            toolStripSlider1.Size = new System.Drawing.Size(150, 40);
            toolStripSlider1.Text = "toolStripSlider1";
            toolStripSlider1.Value = 5D;
            // 
            // toolStripSeparator4
            // 
            toolStripSeparator4.Name = "toolStripSeparator4";
            toolStripSeparator4.Size = new System.Drawing.Size(6, 43);
            // 
            // toolStripMeter1
            // 
            toolStripMeter1.AutoSize = false;
            toolStripMeter1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            toolStripMeter1.DrawColor = System.Drawing.Color.GreenYellow;
            toolStripMeter1.Label = "dotty";
            toolStripMeter1.Maximum = 100D;
            toolStripMeter1.MeterType = MeterType.ContinuousDots;
            toolStripMeter1.Minimum = 0D;
            toolStripMeter1.Name = "toolStripMeter1";
            toolStripMeter1.Size = new System.Drawing.Size(150, 40);
            toolStripMeter1.Text = "toolStripMeter1";
            // 
            // toolStripSeparator5
            // 
            toolStripSeparator5.Name = "toolStripSeparator5";
            toolStripSeparator5.Size = new System.Drawing.Size(6, 43);
            // 
            // meterDots
            // 
            meterDots.BackColor = System.Drawing.Color.Gainsboro;
            meterDots.DrawColor = System.Drawing.Color.White;
            meterDots.Label = "meterDots";
            meterDots.Location = new System.Drawing.Point(1017, 285);
            meterDots.Maximum = 10D;
            meterDots.MeterType = MeterType.ContinuousDots;
            meterDots.Minimum = 0D;
            meterDots.Name = "meterDots";
            meterDots.Orientation = System.Windows.Forms.Orientation.Horizontal;
            meterDots.Size = new System.Drawing.Size(197, 63);
            meterDots.TabIndex = 26;
            // 
            // meterLog
            // 
            meterLog.BackColor = System.Drawing.Color.Gainsboro;
            meterLog.DrawColor = System.Drawing.Color.Azure;
            meterLog.Label = "meter log";
            meterLog.Location = new System.Drawing.Point(1017, 354);
            meterLog.Maximum = 3D;
            meterLog.MeterType = MeterType.Log;
            meterLog.Minimum = -60D;
            meterLog.Name = "meterLog";
            meterLog.Orientation = System.Windows.Forms.Orientation.Horizontal;
            meterLog.Size = new System.Drawing.Size(197, 59);
            meterLog.TabIndex = 27;
            // 
            // meterLinear
            // 
            meterLinear.BackColor = System.Drawing.Color.Gainsboro;
            meterLinear.DrawColor = System.Drawing.Color.White;
            meterLinear.Label = "meterLinear";
            meterLinear.Location = new System.Drawing.Point(1017, 420);
            meterLinear.Maximum = 100D;
            meterLinear.MeterType = MeterType.Linear;
            meterLinear.Minimum = 0D;
            meterLinear.Name = "meterLinear";
            meterLinear.Orientation = System.Windows.Forms.Orientation.Horizontal;
            meterLinear.Size = new System.Drawing.Size(197, 59);
            meterLinear.TabIndex = 28;
            // 
            // slider2
            // 
            slider2.BackColor = System.Drawing.Color.Gainsboro;
            slider2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            slider2.DrawColor = System.Drawing.Color.SlateBlue;
            slider2.Label = "SL2";
            slider2.Location = new System.Drawing.Point(1017, 112);
            slider2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            slider2.Maximum = 19D;
            slider2.Minimum = 2D;
            slider2.Name = "slider2";
            slider2.Orientation = System.Windows.Forms.Orientation.Vertical;
            slider2.Resolution = 0.1D;
            slider2.Size = new System.Drawing.Size(67, 163);
            slider2.TabIndex = 29;
            slider2.Value = 7D;
            // 
            // pan1
            // 
            pan1.BackColor = System.Drawing.Color.Gainsboro;
            pan1.DrawColor = System.Drawing.Color.Crimson;
            pan1.Location = new System.Drawing.Point(1092, 229);
            pan1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            pan1.Name = "pan1";
            pan1.Size = new System.Drawing.Size(122, 46);
            pan1.TabIndex = 30;
            pan1.Value = 0.1D;
            // 
            // pot1
            // 
            pot1.BackColor = System.Drawing.Color.Gainsboro;
            pot1.DrawColor = System.Drawing.Color.Green;
            pot1.ForeColor = System.Drawing.Color.Black;
            pot1.Label = "p99";
            pot1.Location = new System.Drawing.Point(1092, 112);
            pot1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            pot1.Maximum = 100D;
            pot1.Minimum = 0D;
            pot1.Name = "pot1";
            pot1.Resolution = 5D;
            pot1.Size = new System.Drawing.Size(122, 107);
            pot1.TabIndex = 31;
            pot1.Taper = Taper.Linear;
            pot1.Value = 50D;
            // 
            // filTree
            // 
            filTree.Location = new System.Drawing.Point(12, 53);
            filTree.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            filTree.Name = "filTree";
            filTree.Size = new System.Drawing.Size(472, 340);
            filTree.TabIndex = 32;
            // 
            // choicer
            // 
            choicer.BackColor = System.Drawing.Color.PowderBlue;
            choicer.Location = new System.Drawing.Point(771, 362);
            choicer.Name = "choicer";
            choicer.Size = new System.Drawing.Size(211, 261);
            choicer.TabIndex = 33;
            // 
            // optionsEd
            // 
            optionsEd.AllowEdit = true;
            optionsEd.Location = new System.Drawing.Point(771, 93);
            optionsEd.Name = "optionsEd";
            optionsEd.Size = new System.Drawing.Size(211, 255);
            optionsEd.TabIndex = 34;
            // 
            // btnDump
            // 
            btnDump.Location = new System.Drawing.Point(771, 53);
            btnDump.Name = "btnDump";
            btnDump.Size = new System.Drawing.Size(211, 29);
            btnDump.TabIndex = 35;
            btnDump.Text = "Dump these";
            btnDump.UseVisualStyleBackColor = true;
            // 
            // clickClack1
            // 
            clickClack1.GridX = (System.Collections.Generic.List<int>)resources.GetObject("clickClack1.GridX");
            clickClack1.GridY = (System.Collections.Generic.List<int>)resources.GetObject("clickClack1.GridY");
            clickClack1.Location = new System.Drawing.Point(17, 405);
            clickClack1.MaxX = 100;
            clickClack1.MaxY = 100;
            clickClack1.MinX = 0;
            clickClack1.MinY = 0;
            clickClack1.Name = "clickClack1";
            clickClack1.Size = new System.Drawing.Size(453, 209);
            clickClack1.TabIndex = 36;
            // 
            // TestHost
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(1225, 807);
            Controls.Add(clickClack1);
            Controls.Add(btnDump);
            Controls.Add(optionsEd);
            Controls.Add(choicer);
            Controls.Add(filTree);
            Controls.Add(pot1);
            Controls.Add(pan1);
            Controls.Add(slider2);
            Controls.Add(meterLinear);
            Controls.Add(meterLog);
            Controls.Add(meterDots);
            Controls.Add(toolStrip1);
            Controls.Add(slider1);
            Controls.Add(clickGrid1);
            Controls.Add(txtInfo);
            Controls.Add(cpuMeter1);
            Controls.Add(propGrid);
            Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            Name = "TestHost";
            Text = "TestHost";
            toolStrip1.ResumeLayout(false);
            toolStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
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
        private ChoiceSelector choicer;
        private OptionsEditor optionsEd;
        private System.Windows.Forms.Button btnDump;
        private ClickClack clickClack1;
    }
}