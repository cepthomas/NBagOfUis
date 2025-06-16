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
        readonly TestSettings _settings = new();

        public TestHost()
        {
            SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.SupportsTransparentBackColor, true);
            InitializeComponent();

            // Must do this first before initializing.
            _settings = (TestSettings)SettingsCore.Load("Files", typeof(TestSettings), "test-settings.json");

            Location = new Point(_settings.FormGeometry.X, _settings.FormGeometry.Y);
            //Size = new Size(_settings.FormGeometry.Width, _settings.FormGeometry.Height);

            ///// Text viewer.
            txtInfo.MatchText.Add("50", Color.LightPink);
            txtInfo.MatchText.Add("55", Color.LightYellow);
            txtInfo.BackColor = Color.LightCyan;
            txtInfo.Prompt = ">";

            ///// Click grid.
            clickGrid1.AddStateType(0, Color.Blue, Color.AliceBlue);
            clickGrid1.AddStateType(1, Color.AliceBlue, Color.Blue);
            clickGrid1.AddStateType(2, Color.Red, Color.Salmon);
            string[] names = ["dignis", "cras", "tincidu", "loborti", "feugiat", "vivamus", "at", "augue", "eget"];
            for (int i = 0; i < names.Length; i++)
            {
                clickGrid1.AddIndicator(names[i], i);
            }
            clickGrid1.IndicatorChanged += ClickGrid_IndicatorChanged;
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
            filTree.FilterExts = [".txt", ".ntr", ".md", ".xml", ".cs", ".py"];
            filTree.IgnoreDirs = [".vs", ".git", "bin", "obj", "lib"];
            filTree.RootDirs =
            [
                @"C:\Users\cepth\AppData\Roaming\Sublime Text\Packages\Notr",
                @"C:\Users\cepth\OneDrive\OneDriveDocuments\notes"
            ];
            //filTree.RecentFiles = new()
            //{
            //    @"C:\Dev\Apps\repos_common\audio_file_info.txt",
            //    @"C:\Dev\Apps\repos_common\build.txt"
            //};
            filTree.SplitterPosition = 40;
            filTree.SingleClickSelect = false;
            filTree.InitTree();
            filTree.FileSelected += (sender, fn) => { Tell($"Selected file: {fn}"); _settings.UpdateMru(fn); };

            ///// OptionsEditor and ChoiceSelector
            optionsEd.AllowEdit = true;
            optionsEd.BackColor = Color.LightCoral;
            optionsEd.Options = new() { { "Apple", true }, { "Orange", false }, { "Peach", true }, { "Bird", false }, { "Cow", true } };
            optionsEd.OptionsChanged += (_, __) => Tell($"Options changed:{optionsEd.Options.Where(o => o.Value == true).Count()}");
            choicer.Text = "Test choice";
            choicer.SetOptions(["Apple", "Orange", "Peach", "Bird", "Cow"]);
            choicer.ChoiceChanged += (_, __) => Tell($"Choicer changed:{choicer.SelectedChoice}");

            btnDump.Click += (_, __) =>
            {
                Tell($"ChoiceSelector: {choicer.SelectedChoice}");
                Tell($"OptionsEditor:");
                optionsEd.Options.ForEach(v => Tell($"{v.Key} is {v.Value}"));
            };

            ///// ClickClack.
            clickClack1.MinX = 24; // C0
            clickClack1.MaxX = 96; // C6
            clickClack1.GridX = [12, 24, 36, 48, 60, 72, 84];
            clickClack1.MinY = 0; // min velocity == note off
            clickClack1.MaxY = 127; // max velocity
            clickClack1.GridY = [32, 64, 96];
            clickClack1.MouseClickEvent += (_, e) => Tell(e.ToString());
            clickClack1.MouseMoveEvent += (_, e) => e.Text = $">>>{e}";

            ///// Drop down

            List<string> options =
            [
                "Open...",
                "Reload",
                "",
                "Die Die"
            ];
            dropDownButton1.SetOptions(options);
            dropDownButton1.Selected += (sender, sel) => { Tell($"Selected: {sel}"); };

            ///// Other stuff.
            btnSettings.Click += (_, __) => EditSettings();
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

            ///// Graphics.
            DoGraphics();

            ///// Go-go-go.
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

        void ClickGrid_IndicatorChanged(object? sender, IndicatorEventArgs e)
        {
            int state = ++e.State % 3;
            clickGrid1.SetIndicator(e.Id, state);
            txtInfo.AppendColor($"ClickGrid:{e.Id}->{state}", Color.Green);
        }

        void Tell(string msg)
        {
            txtInfo.AppendMatch(msg);
        }

        void SaveSettings()
        {
            _settings.FormGeometry = new Rectangle(Location.X, Location.Y, Width, Height);
            _settings.Save();
        }

        void DoGraphics()
        {
            string inputDir;
            string outputDir;

            var dir = MiscUtils.GetSourcePath();
            inputDir = Path.Join(dir, "files");
            outputDir = Path.Join(dir, "out");
            new DirectoryInfo(outputDir).Create();

            ///// bmp => ico /////
            var bmp = (Bitmap)Image.FromFile(Path.Join(inputDir, "glyphicons-22-snowflake.png")); // 26x26
            // Save single icon.
            var ico = GraphicsUtils.CreateIcon(bmp, 32);
            GraphicsUtils.SaveIcon(ico, Path.Join(outputDir, "snowflake_32.ico")); // just 32.
            // Save icon family.
            ico = GraphicsUtils.CreateIcon(bmp);
            GraphicsUtils.SaveIcon(ico, Path.Join(outputDir, "snowflake_all.ico")); // all sizes

            ///// ico => bmp /////
            ico = GraphicsUtils.CreateIcon(Path.Join(inputDir, "crabe.ico"));
            bmp = ico.ToBitmap();
            picGraphics.Image = bmp;
            // Write it back.
            GraphicsUtils.SaveIcon(ico, Path.Join(outputDir, "copy_crabe.ico")); // all original resolutions.
        }
    }

    public class TestSettings : SettingsCore
    {
        [DisplayName("Test List")]
        [Description("Describe Test List.")]
        [Category("Cat1")]
        [Browsable(true)]
        [Editor(typeof(StringListEditor), typeof(UITypeEditor))]
        public List<string> TestList { get; set; } = [];

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

        [DisplayName("Sub Settings")]
        [Description("Sorting?")]
        [Category("Cat3")]
        [Browsable(true)]
        public List<SubSettings> SubSettings { get; set; } = [];
    }

    public class SubSettings
    {
        [DisplayName("AAA")]
        [Description("AAA")]
        [Category("CatX")]
        [Browsable(true)]
        public int AAA { get; set; } = 1;

        [DisplayName("CCC")]
        [Description("CCC")]
        [Category("CatX")]
        [Browsable(true)]
        public int CCC { get; set; } = 1;

        [DisplayName("BBB")]
        [Description("BBB")]
        [Category("CatX")]
        [Browsable(true)]
        public int BBB { get; set; } = 1;
    }
}
