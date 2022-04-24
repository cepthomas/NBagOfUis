﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using System.Diagnostics;
using System.Drawing.Design;
using System.Text.Json.Serialization;
using NBagOfTricks;
using System.Runtime.Serialization.Formatters.Binary;

namespace NBagOfUis.Test
{
    public partial class TestHost : Form
    {
        readonly TestSettings _testClass = new();

        public TestHost()
        {
            SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.SupportsTransparentBackColor, true);
            InitializeComponent();
        }

        void TestHost_Load(object? sender, EventArgs e)
        {
            Location = new(20, 20);

            ///// Misc controls.
            txtInfo.Colors.Add("note:7", Color.Purple);
            txtInfo.Colors.Add("vel:10", Color.Green);
            txtInfo.BackColor = Color.Cornsilk;

            vkbd.ShowNoteNames = true;

            pot1.ValueChanged += Pot1_ValueChanged;
            pan1.ValueChanged += Pan1_ValueChanged;
            slider1.ValueChanged += Slider1_ValueChanged;
            slider2.ValueChanged += Slider2_ValueChanged;

            ///// Filter tree.
            string root = $@"C:\Dev\repos\NBagOfUis";
            ftree.RootDirs = new List<string>() { root };
            ftree.FilterExts = new List<string> { ".txt", ".md", ".xml", ".cs" };
            ftree.SingleClickSelect = true;
            ftree.Init();

            ///// Wave viewer.
            // Simple sin.
            float[] data1 = new float[150];
            for (int i = 0; i < data1.Length; i++)
            {
                data1[i] = (float)Math.Sin(Math.PI * i / 180.0);
            }
            waveViewer1.Mode = WaveViewer.DrawMode.Raw;
            waveViewer1.DrawColor = Color.Green;
            waveViewer1.Init(data1, 1.0f);
            waveViewer1.Marker1 = 20;
            waveViewer1.Marker2 = 130;

            // Real data.
            string[] sdata = File.ReadAllLines(@"C:\Dev\repos\NBagOfUis\Test\Files\wav.txt");
            float[] data2 = new float[sdata.Length];
            for (int i = 0; i < sdata.Length; i++)
            {
                data2[i] = float.Parse(sdata[i]);
            }
            waveViewer2.Mode = WaveViewer.DrawMode.Envelope;
            waveViewer2.DrawColor = Color.Green;
            waveViewer2.Init(data2, 1.0f);
            waveViewer2.Marker1 = -1; // hide
            waveViewer2.Marker2 = data2.Length / 2;

            ///// Click grid.
            clickGrid1.AddStateType(0, Color.Blue, Color.AliceBlue);
            clickGrid1.AddStateType(1, Color.AliceBlue, Color.Blue);
            clickGrid1.AddStateType(2, Color.Red, Color.Salmon);

            string[] names = { "dignissim", "cras", "tincidunt", "lobortis", "feugiat", "vivamus", "at", "augue", "eget" };
            for (int i = 0; i < names.Length; i++)
            {
                clickGrid1.AddIndicator(names[i], i);
            }

            clickGrid1.IndicatorEvent += ClickGrid_IndicatorEvent;
            clickGrid1.Show(4, 60, 20);

            ///// Time bar.
            timeBar.SnapMsec = 10;
            timeBar.Length = new TimeSpan(0, 0, 1, 23, 456);
            timeBar.Start = new TimeSpan(0, 0, 0, 10, 333);
            timeBar.End = new TimeSpan(0, 0, 0, 44, 777);
            timeBar.CurrentTimeChanged += TimeBar_CurrentTimeChanged1;
            timeBar.ProgressColor = Color.CornflowerBlue;
            timeBar.BackColor = Color.Salmon;

            ///// Bar bar.
            barBar.ZeroBased = true;
            barBar.BeatsPerBar = 4;
            barBar.SubdivsPerBeat = 16;
            barBar.Snap = BarBar.SnapType.Bar;
            barBar.Length = new BarSpan(16, 0, 0);
            barBar.Start = new BarSpan(2, 1, 11);
            barBar.End = new BarSpan(11, 3, 6);
            barBar.CurrentTimeChanged += BarBar1_CurrentTimeChanged;
            barBar.ProgressColor = Color.MediumPurple;
            barBar.BackColor = Color.LawnGreen;
            //barBar.Test();

            ///// PropertyGridEx and UiType editor host.
            for(int i = 0; i < 5; i++)
            {
                _testClass.TestList.Add($"List{i}");
            }

            Image img = Image.FromFile(@"Files\glyphicons-22-snowflake.png");
            propGrid.SelectedObject = _testClass;
            var lbl = propGrid.AddLabel("Blue", null, "The sky is blue");
            propGrid.AddButton("Red", null, "Blood is red", (_, __) => lbl!.Text = "->Red");
            propGrid.AddButton("", img, "Image is red", (_, __) => lbl!.Text = "->IRed");
            //propGrid.MoveSplitter(100);
            propGrid.ResizeDescriptionArea(4);
            //propGrid.ExpandGroup("Cat1", false);
            //propGrid.ShowProperty("TestString", false);

            // Go-go-go.
            timer1.Enabled = true;
        }

        void TestHost_FormClosing(object? sender, FormClosingEventArgs e)
        {
            // Inspect.
            //var at = ftree.AllTags;
            //var tp = ftree.TaggedPaths;
            //var po = _testClass;
        }

        void Graphics_Click(object sender, EventArgs e)
        {
            new Graphics().ShowDialog();
        }

        void Settings_Click(object sender, EventArgs e)
        {
            // Get the settings.
            TestSettings set = (TestSettings)Settings.Load(@".\Files", typeof(TestSettings), "test-settings.json");


            // Edit them.
            set.Edit("Edit me!!!");

            // Mod and save.
            //_settings.Abool = chk1.Checked;
            set.FormGeometry = new Rectangle(Location.X, Location.Y, Size.Width, Size.Height);

            set.RecentFiles.Add(@"C:\Dev\WinFormsApp1\obj\Debug\net5.0-windows\WinFormsApp1.AssemblyInfo.cs");
            set.RecentFiles.Add("bad_path");

            set.Save();

            // Check recent file list. Should just one.
        }

        void Timer1_Tick(object? sender, EventArgs e)
        {
            if (chkRunBars.Checked)
            {
                // Update time bar.
                timeBar.IncrementCurrent(timer1.Interval + 3); // not-real time for testing
                if (timeBar.Current >= timeBar.End) // done/reset
                {
                    timeBar.Current = timeBar.Start;
                }

                // Update bar bar.
                barBar.IncrementCurrent(1);
                if (barBar.Current >= barBar.End) // done/reset
                {
                    barBar.Current = barBar.Start;
                }
            }
        }

        void TimeBar_CurrentTimeChanged1(object? sender, EventArgs e)
        {
        }

        void BarBar1_CurrentTimeChanged(object? sender, EventArgs e)
        {
        }

        void ClickGrid_IndicatorEvent(object? sender, IndicatorEventArgs e)
        {
            int state = ++e.State % 3;
            clickGrid1.SetIndicator(e.Id, state);
        }

        void FilTree_FileSelectedEvent(object? sender, string fn)
        {
            txtInfo.AppendLine($"Selected file: {fn}");
        }

        void Pot1_ValueChanged(object? sender, EventArgs e)
        {
            // 0 -> 1
            meter2.AddValue(pot1.Value);
        }

        void Slider1_ValueChanged(object? sender, EventArgs e)
        {
            // 0 -> 1
            meter1.AddValue(slider1.Value * 100.0);
        }

        void Slider2_ValueChanged(object? sender, EventArgs e)
        {
            // 0 -> 10
            meter1.AddValue(slider2.Value * 10.0);
        }

        void Pan1_ValueChanged(object? sender, EventArgs e)
        {
            // -1 -> +1
            meter1.AddValue(pan1.Value * 50.0 + 50.0);
        }

        void Vkbd_KeyboardEvent(object? sender, VirtualKeyboard.KeyboardEventArgs e)
        {
            string s = $"note:{e.NoteId} vel:{e.Velocity}";
            txtInfo.AppendLine(s);

            meter3.AddValue(e.NoteId / 8.0 - 10.0);
        }

        void ChkCpu_CheckedChanged(object? sender, EventArgs e)
        {
            cpuMeter1.Enable = chkCpu.Checked;
        }

        void TimeBar_CurrentTimeChanged(object? sender, EventArgs e)
        {
            //txtInfo.AddLine($"Current time:{timeBar.CurrentTime}");
        }
    }

    public class TestSettings : Settings
    {
        [DisplayName("Test List")]
        [Description("Describe Test List.")]
        [Category("Cat1")]
        [Browsable(true)]
        [Editor(typeof(StringListEditor), typeof(UITypeEditor))]
        public List<string> TestList { get; set; } = new();

        [DisplayName("Test Font")]
        [Description("Describe Test Font.")]
        [Category("Cat1")]
        [Browsable(true)]
        [JsonConverter(typeof(JsonFontConverter))]
        [Editor(typeof(MonospaceFontEditor), typeof(UITypeEditor))]
        public Font? TestFont { get; set; }

        [DisplayName("Test Color")]
        [Description("Describe Test Color.")]
        [Category("Cat2")]
        [Browsable(true)]
        [JsonConverter(typeof(JsonColorConverter))]
        [Editor(typeof(ColorEditor), typeof(UITypeEditor))]
        public Color? TestColor { get; set; }

        [DisplayName("Test String")]
        [Description("Describe Test String.")]
        [Category("Cat2")]
        [Browsable(true)]
        public string TestString { get; set; } = "";
    }
}
