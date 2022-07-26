using System;
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
using System.Runtime.Serialization.Formatters.Binary;
using System.Drawing.Imaging;
using NBagOfTricks;


namespace NBagOfUis.Test
{
    public partial class TestHost : Form
    {
        readonly TestSettings _testClass = new();

        public TestHost()
        {
            SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.SupportsTransparentBackColor, true);
            InitializeComponent();

            Location = new(20, 20);

            ///// Text control.
            txtInfo.MatchColors.Add("50", Color.Purple);
            txtInfo.MatchColors.Add("55", Color.Green);
            txtInfo.BackColor = Color.Cornsilk;
            txtInfo.Prompt = ">>> ";

            ///// Filter tree. Adjust to taste.
            ftree.RootDirs = new List<string>() { $@"..\..\..\" };
            ftree.FilterExts = new List<string> { ".txt", ".md", ".xml", ".cs" };
            ftree.SingleClickSelect = true;
            ftree.Init();

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

            ///// PropertyGridEx and UiType editor host.
            for(int i = 0; i < 5; i++)
            {
                _testClass.TestList.Add($"List{i}");
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
            propGrid.SelectedObject = _testClass;
            var lbl = propGrid.AddLabel("Blue", null, "The sky is blue");
            propGrid.AddButton("Red", null, "Blood is red", (_, __) => lbl!.Text = "->Red");
            propGrid.AddButton("", img, "Image is red", (_, __) => lbl!.Text = "->IRed");
            //propGrid.MoveSplitter(100);
            propGrid.ResizeDescriptionArea(6);
            //propGrid.ExpandGroup("Cat1", false);
            //propGrid.ShowProperty("TestString", false);

            ///// Other stuff.
            btnSettings.Click += (_, __) => { DoSettings(); };
            btnGfx.Click += (_, __) => { new GraphicsForm().ShowDialog(); };
            btnCpu.CheckedChanged += (_, __) => { cpuMeter1.Enable = btnCpu.Checked; };
            // pot1 0 -> 100
            pot1.ValueChanged += (_, __) => { Tell($"pot1:{pot1.Value}"); };
            //pan -1 -> 1
            pan1.ValueChanged += (_, __) => { meterLog.AddValue(pan1.Value * 50.0 + 50.0); };
            // sl1 0 -> 100
            slider1.ValueChanged += (_, __) => { meterLinear.AddValue(slider1.Value); };
            // sl2 2 -> 19
            slider2.ValueChanged += (_, __) => { meterDots.AddValue(slider2.Value); };

            // Go-go-go.
            timer1.Enabled = true;
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            // Inspect.
            //var at = ftree.AllTags;
            //var tp = ftree.TaggedPaths;
            //var po = _testClass;
            base.OnFormClosing(e);
        }

        void DoSettings()
        {
            // Get the settings.
            TestSettings set = (TestSettings)Settings.Load(@".\Files", typeof(TestSettings), "test-settings.json");

            // Edit them.
            set.Edit("Edit me!!!", 400);

            // Mod and save.
            //_settings.Abool = chk1.Checked;
            set.FormGeometry = new Rectangle(Location.X, Location.Y, Size.Width, Size.Height);

            set.RecentFiles.Add(Path.GetFullPath("NBagOfUis.xml"));
            set.RecentFiles.Add(@"C:\bad\path\file.xyz");

            set.Save();

            // Check recent file list. Should be just one.
        }

        void Timer1_Tick(object? sender, EventArgs e)
        {
        }

        void ClickGrid_IndicatorEvent(object? sender, IndicatorEventArgs e)
        {
            int state = ++e.State % 3;
            clickGrid1.SetIndicator(e.Id, state);
        }

        void FilTree_FileSelectedEvent(object? sender, string fn)
        {
            Tell($"Selected file: {fn}");
        }

        void Tell(string msg)
        {
            txtInfo.AppendLine(msg);
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
