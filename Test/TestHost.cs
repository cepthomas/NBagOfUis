using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using System.Diagnostics;
using System.Drawing.Design;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;
using Ephemera.NBagOfTricks;


namespace Ephemera.NBagOfUis.Test
{
    public partial class TestHost : Form
    {
        TestSettings _settings = new();

        public TestHost()
        {
            SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.SupportsTransparentBackColor, true);
            InitializeComponent();

            // Must do this first before initializing.
            string appDir = MiscUtils.GetAppDataDir("Test", "Ephemera");
            _settings = (TestSettings)SettingsCore.Load(appDir, typeof(TestSettings), "nbui-test-settings.json");

            Location = new Point(_settings.FormGeometry.X, _settings.FormGeometry.Y);
            //Size = new Size(_settings.FormGeometry.Width, _settings.FormGeometry.Height);

            ///// Text control.
            txtInfo.MatchColors.Add("50", Color.Purple);
            txtInfo.MatchColors.Add("55", Color.Green);
            txtInfo.BackColor = Color.Cornsilk;
            txtInfo.Prompt = ">>> ";

            ///// Click grid.
            clickGrid1.AddStateType(0, Color.Blue, Color.AliceBlue);
            clickGrid1.AddStateType(1, Color.AliceBlue, Color.Blue);
            clickGrid1.AddStateType(2, Color.Red, Color.Salmon);
            string[] names = { "dignis", "cras", "tincidu", "loborti", "feugiat", "vivamus", "at", "augue", "eget" };
            for (int i = 0; i < names.Length; i++)
            {
                clickGrid1.AddIndicator(names[i], i);
            }
            clickGrid1.IndicatorEvent += ClickGrid_IndicatorEvent;
            clickGrid1.Show(4, 60, 20);

            ///// PropertyGridEx and UiType editor host.
            _settings.TestList.Clear();
            for (int i = 0; i < 5; i++)
            {
                _settings.TestList.Add($"ListItem{i}");
            }

            ///// Slider.
            slider1.DrawColor = Color.Orange;
            slider1.Minimum = 0;
            slider1.Maximum = 100;
            slider1.Resolution = 5;
            slider1.Value = 40;
            slider1.Label = "|-|-|";
            slider1.ValueChanged += (_, __) => Tell($"Slider value: {slider1.Value}");

            ///// Property grid.
            Image img = Image.FromFile(@"Files\glyphicons-22-snowflake.png");
            propGrid.SelectedObject = _settings;
            var lbl = propGrid.AddLabel("Blue", null, "The sky is blue");
            propGrid.AddButton("Red", null, "Blood is red", (_, __) => lbl!.Text = "->Red");
            propGrid.AddButton("", img, "Image is red", (_, __) => lbl!.Text = "->IRed");
            //propGrid.MoveSplitter(100);
            //propGrid.ExpandAllGridItems();
            //propGrid.ExpandGroup("Cat1", false);
            //propGrid.ShowProperty("TestString", false);

            ///// FilTree.
            filTree.FilterExts = new List<string> { ".txt", ".md", ".xml", ".cs", ".py" };
            filTree.IgnoreDirs = new List<string> { ".vs", ".git", "bin", "obj", "lib" };
            filTree.RootDirs = new List<string>
            {
                @"C:\Users\cepth\AppData\Local\Ephemera\Notr",
                @"C:\Users\cepth\AppData\Roaming\Sublime Text\Packages\Notr",
                @"C:\Users\cepth\OneDrive\OneDrive Documents\_notes"
            };
            filTree.RecentFiles = new()
            {
                @"C:\Dev\repos\repos_common\audio_file_info.txt",
                @"C:\Dev\repos\repos_common\build.txt"
            };
            filTree.SplitterPosition = 40;
            filTree.SingleClickSelect = false;
            filTree.InitTree();
            filTree.FileSelectedEvent += (object? sender, string fn) => { Tell($"Selected file: {fn}"); _settings.UpdateMru(fn); };

            ///// OptionsEditor and ChoiceSelector
            optionsEd.AllowEdit = true;
            optionsEd.Options = new() { { "Apple", true }, { "Orange", false }, { "Peach", true }, { "Bird", false }, { "Cow", true } };
            choicer.Text = "Test choice";
            choicer.SetOptions(new() { "Apple", "Orange", "Peach", "Bird", "Cow" });
            btnDump.Click += (_, __) =>
            {
                Tell($"ChoiceSelector: {choicer.SelectedOption}");
                Tell($"OptionsEditor:");
                optionsEd.Options.ForEach(v => Tell($"{v.Key} is {v.Value}"));
            };

            ///// Other stuff.
            btnSettings.Click += (_, __) => EditSettings();
            btnGfx.Click += (_, __) => { new GraphicsForm().ShowDialog(); };
            btnCpu.CheckedChanged += (_, __) => cpuMeter1.Enable = btnCpu.Checked;
            // pot1 0 -> 100
            pot1.ValueChanged += (_, __) => Tell($"pot1:{pot1.Value}");
            //pan -1 -> 1
            pan1.ValueChanged += (_, __) => meterLog.AddValue(pan1.Value * 50.0 + 50.0);
            // sl1 0 -> 100
            slider1.ValueChanged += (_, __) => meterLinear.AddValue(slider1.Value);
            // sl2 2 -> 19
            slider2.ValueChanged += (_, __) => meterDots.AddValue(slider2.Value);
            //
            toolStripSlider1.ValueChanged += (_, __) => Tell($"toolStripSlider1:{toolStripSlider1.Value}");
            //
            pan1.ValueChanged += (_, __) => toolStripMeter1.AddValue(pan1.Value * 50.0 + 50.0);

            // Go-go-go.
            timer1.Enabled = true;
        }

        protected override void OnLoad(EventArgs e)
        {
            propGrid.ResizeDescriptionArea(6); // This doesn't work in constructor.

            base.OnLoad(e);
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            SaveSettings();
            base.OnFormClosing(e);
        }

        void EditSettings()
        {
            // Edit them.
            bool restart = false;
            var changes = SettingsEditor.Edit(_settings, "Edit me!!!", 400);

            foreach (var (name, cat) in changes)
            {
                switch (name)
                {
                    case "ControlColor":
                    case "FileLogLevel":
                    case "NotifLogLevel":
                        restart = true;
                        break;

                    //case "SingleClickSelect":
                    //    filTree.SingleClickSelect = _settings.SingleClickSelect;
                    //    break;

                    //case "SplitterPosition":
                    //    filTree.SplitterPosition = _settings.SplitterPosition;
                    //    break;
                }
            }

            if (restart)
            {
                MessageBox.Show("Restart required for device changes to take effect");
            }

            _settings.FormGeometry = new Rectangle(Location.X, Location.Y, Size.Width, Size.Height);
            _settings.Save();

            // Update
            //filTree.Init();
        }

        void Timer1_Tick(object? sender, EventArgs e)
        {
        }

        void ClickGrid_IndicatorEvent(object? sender, IndicatorEventArgs e)
        {
            int state = ++e.State % 3;
            clickGrid1.SetIndicator(e.Id, state);
            txtInfo.AppendLine($"ClickGrid:{e.Id}->{state}");
        }

        void Tell(string msg)
        {
            txtInfo.AppendLine(msg);
        }

        void SaveSettings()
        {
            _settings.FormGeometry = new Rectangle(Location.X, Location.Y, Width, Height);
            _settings.Save();
        }
    }

    public class TestSettings : SettingsCore
    {
        [DisplayName("Test List")]
        [Description("Describe Test List.")]
        [Category("Cat1")]
        [Browsable(true)]
        [Editor(typeof(StringListEditor), typeof(UITypeEditor))]
        public List<string> TestList { get; set; } = new();

        [DisplayName("Plain Font")]
        [Description("Describe Plain Font.")]
        [Category("Cat1")]
        [Browsable(true)]
        [JsonConverter(typeof(JsonFontConverter))]
        [Editor(typeof(FontEditor), typeof(UITypeEditor))]
        public Font? PlainFont { get; set; }

        [DisplayName("Monospace Font")]
        [Description("Describe Monospace Font.")]
        [Category("Cat1")]
        [Browsable(true)]
        [JsonConverter(typeof(JsonFontConverter))]
        [Editor(typeof(MonospaceFontEditor), typeof(UITypeEditor))]
        public Font? MonospaceFont { get; set; }

        [DisplayName("Test Color")]
        [Description("Describe Test Color.")]
        [Category("Cat2")]
        [Browsable(true)]
        [JsonConverter(typeof(JsonColorConverter))]
        [Editor(typeof(ColorEditor), typeof(UITypeEditor))]
        public Color? TestColor { get; set; } = Color.Salmon;

        [DisplayName("Test String")]
        [Description("Describe Test String.")]
        [Category("Cat2")]
        [Browsable(true)]
        public string TestString { get; set; } = "Just a test";

        [DisplayName("Single Click Select")]
        [Description("Generate event with single or double click.")]
        [Browsable(true)]
        public bool SingleClickSelect { get; set; } = false;

        [DisplayName("Splitter Position")]
        [Description("As percentage.")]
        [Range(10, 80)]
        [Browsable(true)]
        public int SplitterPosition { get; set; } = 30;
    }
}
