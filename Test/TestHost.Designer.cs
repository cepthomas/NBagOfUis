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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.txtInfo = new NBagOfUis.TextViewer();
            this.vkbd = new NBagOfUis.VirtualKeyboard();
            this.propGrid = new NBagOfUis.PropertyGridEx();
            this.waveViewer2 = new NBagOfUis.WaveViewer();
            this.waveViewer1 = new NBagOfUis.WaveViewer();
            this.chkRunBars = new System.Windows.Forms.CheckBox();
            this.barBar = new NBagOfUis.BarBar();
            this.clickGrid1 = new NBagOfUis.ClickGrid();
            this.timeBar = new NBagOfUis.TimeBar();
            this.chkCpu = new System.Windows.Forms.CheckBox();
            this.ftree = new NBagOfUis.FilTree();
            this.meter3 = new NBagOfUis.Meter();
            this.meter2 = new NBagOfUis.Meter();
            this.cpuMeter1 = new NBagOfUis.CpuMeter();
            this.slider2 = new NBagOfUis.Slider();
            this.pan1 = new NBagOfUis.Pan();
            this.meter1 = new NBagOfUis.Meter();
            this.pot1 = new NBagOfUis.Pot();
            this.slider1 = new NBagOfUis.Slider();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.btnGraphics = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.txtInfo);
            this.splitContainer1.Panel1.Controls.Add(this.vkbd);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.btnGraphics);
            this.splitContainer1.Panel2.Controls.Add(this.propGrid);
            this.splitContainer1.Panel2.Controls.Add(this.waveViewer2);
            this.splitContainer1.Panel2.Controls.Add(this.waveViewer1);
            this.splitContainer1.Panel2.Controls.Add(this.chkRunBars);
            this.splitContainer1.Panel2.Controls.Add(this.barBar);
            this.splitContainer1.Panel2.Controls.Add(this.clickGrid1);
            this.splitContainer1.Panel2.Controls.Add(this.timeBar);
            this.splitContainer1.Panel2.Controls.Add(this.chkCpu);
            this.splitContainer1.Panel2.Controls.Add(this.ftree);
            this.splitContainer1.Panel2.Controls.Add(this.meter3);
            this.splitContainer1.Panel2.Controls.Add(this.meter2);
            this.splitContainer1.Panel2.Controls.Add(this.cpuMeter1);
            this.splitContainer1.Panel2.Controls.Add(this.slider2);
            this.splitContainer1.Panel2.Controls.Add(this.pan1);
            this.splitContainer1.Panel2.Controls.Add(this.meter1);
            this.splitContainer1.Panel2.Controls.Add(this.pot1);
            this.splitContainer1.Panel2.Controls.Add(this.slider1);
            this.splitContainer1.Size = new System.Drawing.Size(1436, 900);
            this.splitContainer1.SplitterDistance = 106;
            this.splitContainer1.SplitterWidth = 6;
            this.splitContainer1.TabIndex = 4;
            // 
            // txtInfo
            // 
            this.txtInfo.Font = new System.Drawing.Font("Consolas", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txtInfo.Location = new System.Drawing.Point(840, 0);
            this.txtInfo.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtInfo.MaxText = 5000;
            this.txtInfo.Name = "txtInfo";
            this.txtInfo.Size = new System.Drawing.Size(592, 106);
            this.txtInfo.TabIndex = 5;
            this.txtInfo.Text = "";
            // 
            // vkbd
            // 
            this.vkbd.Dock = System.Windows.Forms.DockStyle.Fill;
            this.vkbd.Location = new System.Drawing.Point(0, 0);
            this.vkbd.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.vkbd.Name = "vkbd";
            this.vkbd.ShowNoteNames = false;
            this.vkbd.Size = new System.Drawing.Size(1436, 106);
            this.vkbd.TabIndex = 0;
            this.vkbd.KeyboardEvent += new System.EventHandler<NBagOfUis.VirtualKeyboard.KeyboardEventArgs>(this.Vkbd_KeyboardEvent);
            // 
            // propGrid
            // 
            this.propGrid.Dirty = false;
            this.propGrid.Location = new System.Drawing.Point(580, 188);
            this.propGrid.Name = "propGrid";
            this.propGrid.Size = new System.Drawing.Size(337, 385);
            this.propGrid.TabIndex = 20;
            // 
            // waveViewer2
            // 
            this.waveViewer2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.waveViewer2.DrawColor = System.Drawing.Color.Orange;
            this.waveViewer2.Location = new System.Drawing.Point(943, 486);
            this.waveViewer2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.waveViewer2.Marker1 = -1;
            this.waveViewer2.Marker2 = -1;
            this.waveViewer2.MarkerColor = System.Drawing.Color.Black;
            this.waveViewer2.Mode = NBagOfUis.WaveViewer.DrawMode.Envelope;
            this.waveViewer2.Name = "waveViewer2";
            this.waveViewer2.Size = new System.Drawing.Size(353, 87);
            this.waveViewer2.TabIndex = 19;
            // 
            // waveViewer1
            // 
            this.waveViewer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.waveViewer1.DrawColor = System.Drawing.Color.Orange;
            this.waveViewer1.Location = new System.Drawing.Point(943, 391);
            this.waveViewer1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.waveViewer1.Marker1 = -1;
            this.waveViewer1.Marker2 = -1;
            this.waveViewer1.MarkerColor = System.Drawing.Color.Black;
            this.waveViewer1.Mode = NBagOfUis.WaveViewer.DrawMode.Envelope;
            this.waveViewer1.Name = "waveViewer1";
            this.waveViewer1.Size = new System.Drawing.Size(353, 87);
            this.waveViewer1.TabIndex = 18;
            // 
            // chkRunBars
            // 
            this.chkRunBars.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkRunBars.AutoSize = true;
            this.chkRunBars.Checked = true;
            this.chkRunBars.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkRunBars.Font = new System.Drawing.Font("Cooper Black", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.chkRunBars.Location = new System.Drawing.Point(943, 183);
            this.chkRunBars.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.chkRunBars.Name = "chkRunBars";
            this.chkRunBars.Size = new System.Drawing.Size(89, 27);
            this.chkRunBars.TabIndex = 16;
            this.chkRunBars.Text = "Run Bars";
            this.chkRunBars.UseVisualStyleBackColor = true;
            // 
            // barBar
            // 
            this.barBar.BeatsPerBar = 4;
            this.barBar.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.barBar.FontLarge = new System.Drawing.Font("Microsoft Sans Serif", 19.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.barBar.FontSmall = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.barBar.Location = new System.Drawing.Point(943, 302);
            this.barBar.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.barBar.MarkerColor = System.Drawing.Color.Black;
            this.barBar.Name = "barBar";
            this.barBar.ProgressColor = System.Drawing.Color.White;
            this.barBar.Size = new System.Drawing.Size(353, 63);
            this.barBar.Snap = NBagOfUis.BarBar.SnapType.Subdiv;
            this.barBar.SubdivsPerBeat = 8;
            this.barBar.TabIndex = 15;
            // 
            // clickGrid1
            // 
            this.clickGrid1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.clickGrid1.Location = new System.Drawing.Point(12, 470);
            this.clickGrid1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.clickGrid1.Name = "clickGrid1";
            this.clickGrid1.Size = new System.Drawing.Size(484, 156);
            this.clickGrid1.TabIndex = 1;
            // 
            // timeBar
            // 
            this.timeBar.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.timeBar.FontLarge = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.timeBar.FontSmall = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.timeBar.Location = new System.Drawing.Point(943, 218);
            this.timeBar.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.timeBar.MarkerColor = System.Drawing.Color.Black;
            this.timeBar.Name = "timeBar";
            this.timeBar.ProgressColor = System.Drawing.Color.Orange;
            this.timeBar.Size = new System.Drawing.Size(353, 64);
            this.timeBar.SnapMsec = 0;
            this.timeBar.TabIndex = 13;
            // 
            // chkCpu
            // 
            this.chkCpu.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkCpu.AutoSize = true;
            this.chkCpu.Location = new System.Drawing.Point(618, 20);
            this.chkCpu.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.chkCpu.Name = "chkCpu";
            this.chkCpu.Size = new System.Drawing.Size(43, 30);
            this.chkCpu.TabIndex = 12;
            this.chkCpu.Text = "cpu";
            this.chkCpu.UseVisualStyleBackColor = true;
            this.chkCpu.CheckedChanged += new System.EventHandler(this.ChkCpu_CheckedChanged);
            // 
            // ftree
            // 
            this.ftree.Location = new System.Drawing.Point(12, 20);
            this.ftree.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.ftree.Name = "ftree";
            this.ftree.SingleClickSelect = true;
            this.ftree.Size = new System.Drawing.Size(484, 441);
            this.ftree.TabIndex = 11;
            this.ftree.FileSelectedEvent += new System.EventHandler<string>(this.FilTree_FileSelectedEvent);
            // 
            // meter3
            // 
            this.meter3.BackColor = System.Drawing.Color.Gainsboro;
            this.meter3.DrawColor = System.Drawing.Color.Violet;
            this.meter3.Label = "meter dots";
            this.meter3.Location = new System.Drawing.Point(872, 20);
            this.meter3.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.meter3.Maximum = 10D;
            this.meter3.MeterType = NBagOfUis.MeterType.ContinuousDots;
            this.meter3.Minimum = -10D;
            this.meter3.Name = "meter3";
            this.meter3.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.meter3.Size = new System.Drawing.Size(180, 60);
            this.meter3.TabIndex = 10;
            // 
            // meter2
            // 
            this.meter2.BackColor = System.Drawing.Color.Gainsboro;
            this.meter2.DrawColor = System.Drawing.Color.Azure;
            this.meter2.Label = "meter log";
            this.meter2.Location = new System.Drawing.Point(872, 93);
            this.meter2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.meter2.Maximum = 3D;
            this.meter2.MeterType = NBagOfUis.MeterType.Log;
            this.meter2.Minimum = -60D;
            this.meter2.Name = "meter2";
            this.meter2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.meter2.Size = new System.Drawing.Size(180, 60);
            this.meter2.TabIndex = 9;
            // 
            // cpuMeter1
            // 
            this.cpuMeter1.BackColor = System.Drawing.Color.Gainsboro;
            this.cpuMeter1.DrawColor = System.Drawing.Color.DarkOrange;
            this.cpuMeter1.Enable = false;
            this.cpuMeter1.Label = "cpu";
            this.cpuMeter1.Location = new System.Drawing.Point(677, 20);
            this.cpuMeter1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cpuMeter1.Name = "cpuMeter1";
            this.cpuMeter1.Size = new System.Drawing.Size(175, 60);
            this.cpuMeter1.TabIndex = 7;
            // 
            // slider2
            // 
            this.slider2.BackColor = System.Drawing.Color.Gainsboro;
            this.slider2.DecPlaces = 1;
            this.slider2.DrawColor = System.Drawing.Color.SlateBlue;
            this.slider2.Label = "Vertical";
            this.slider2.Location = new System.Drawing.Point(1251, 20);
            this.slider2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.slider2.Maximum = 10D;
            this.slider2.Minimum = 0D;
            this.slider2.Name = "slider2";
            this.slider2.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.slider2.ResetValue = 0D;
            this.slider2.Size = new System.Drawing.Size(42, 133);
            this.slider2.TabIndex = 6;
            this.slider2.Value = 5.4D;
            // 
            // pan1
            // 
            this.pan1.BackColor = System.Drawing.Color.Gainsboro;
            this.pan1.DrawColor = System.Drawing.Color.Crimson;
            this.pan1.Location = new System.Drawing.Point(677, 93);
            this.pan1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pan1.Name = "pan1";
            this.pan1.Size = new System.Drawing.Size(175, 60);
            this.pan1.TabIndex = 5;
            this.pan1.Value = 0D;
            // 
            // meter1
            // 
            this.meter1.BackColor = System.Drawing.Color.Gainsboro;
            this.meter1.DrawColor = System.Drawing.Color.Orange;
            this.meter1.Label = "meter xyz";
            this.meter1.Location = new System.Drawing.Point(1078, 93);
            this.meter1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.meter1.Maximum = 100D;
            this.meter1.MeterType = NBagOfUis.MeterType.Linear;
            this.meter1.Minimum = 0D;
            this.meter1.Name = "meter1";
            this.meter1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.meter1.Size = new System.Drawing.Size(153, 60);
            this.meter1.TabIndex = 3;
            // 
            // pot1
            // 
            this.pot1.BackColor = System.Drawing.Color.Gainsboro;
            this.pot1.DecPlaces = 2;
            this.pot1.DrawColor = System.Drawing.Color.Green;
            this.pot1.ForeColor = System.Drawing.Color.Black;
            this.pot1.Label = "p99";
            this.pot1.Location = new System.Drawing.Point(580, 60);
            this.pot1.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.pot1.Maximum = 1D;
            this.pot1.Minimum = 0D;
            this.pot1.Name = "pot1";
            this.pot1.Size = new System.Drawing.Size(81, 91);
            this.pot1.TabIndex = 1;
            this.pot1.Taper = NBagOfUis.Taper.Linear;
            this.pot1.Value = 0.5D;
            // 
            // slider1
            // 
            this.slider1.BackColor = System.Drawing.Color.Gainsboro;
            this.slider1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.slider1.DecPlaces = 2;
            this.slider1.DrawColor = System.Drawing.Color.Orange;
            this.slider1.Label = "Horizontal";
            this.slider1.Location = new System.Drawing.Point(1078, 20);
            this.slider1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.slider1.Maximum = 1D;
            this.slider1.Minimum = 0D;
            this.slider1.Name = "slider1";
            this.slider1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.slider1.ResetValue = 0D;
            this.slider1.Size = new System.Drawing.Size(153, 60);
            this.slider1.TabIndex = 2;
            this.slider1.Value = 0.3D;
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.Timer1_Tick);
            // 
            // btnGraphics
            // 
            this.btnGraphics.Location = new System.Drawing.Point(509, 20);
            this.btnGraphics.Name = "btnGraphics";
            this.btnGraphics.Size = new System.Drawing.Size(61, 60);
            this.btnGraphics.TabIndex = 21;
            this.btnGraphics.Text = "Run GFX";
            this.btnGraphics.UseVisualStyleBackColor = true;
            this.btnGraphics.Click += new System.EventHandler(this.Graphics_Click);
            // 
            // TestHost
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1436, 900);
            this.Controls.Add(this.splitContainer1);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "TestHost";
            this.Text = "TestHost";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.TestHost_FormClosing);
            this.Load += new System.EventHandler(this.TestHost_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private NBagOfUis.VirtualKeyboard vkbd;
        private NBagOfUis.Pot pot1;
        private NBagOfUis.Slider slider1;
        private NBagOfUis.Meter meter1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private NBagOfUis.Pan pan1;
        private NBagOfUis.Slider slider2;
        private NBagOfUis.CpuMeter cpuMeter1;
        private NBagOfUis.Meter meter2;
        private NBagOfUis.Meter meter3;
        private NBagOfUis.FilTree ftree;
        private System.Windows.Forms.CheckBox chkCpu;
        private NBagOfUis.TimeBar timeBar;
        private System.Windows.Forms.Timer timer1;
        private NBagOfUis.ClickGrid clickGrid1;
        private NBagOfUis.BarBar barBar;
        private System.Windows.Forms.CheckBox chkRunBars;
        private NBagOfUis.WaveViewer waveViewer2;
        private NBagOfUis.WaveViewer waveViewer1;
        private NBagOfUis.PropertyGridEx propGrid;
        private TextViewer txtInfo;
        private System.Windows.Forms.Button btnGraphics;
    }
}