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
        int _boxX = 5;
        int _boxY = 5;

        public TestHost()
        {
            SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.SupportsTransparentBackColor, true);
            InitializeComponent();

            Location = new(20, 20);

            ///// Misc controls.
            txtInfo.Colors.Add("50", Color.Purple);
            txtInfo.Colors.Add("55", Color.Green);
            txtInfo.BackColor = Color.Cornsilk;

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
            propGrid.ResizeDescriptionArea(4);
            //propGrid.ExpandGroup("Cat1", false);
            //propGrid.ShowProperty("TestString", false);

            ///// Graphics stuff.
            btnGraphics.Click += (_, __) => DoGraphics();

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

        void Settings_Click(object sender, EventArgs e)
        {
            // Get the settings.
            TestSettings set = (TestSettings)Settings.Load(@".\Files", typeof(TestSettings), "test-settings.json");

            // Edit them.
            set.Edit("Edit me!!!");

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

        void ChkCpu_CheckedChanged(object? sender, EventArgs e)
        {
            cpuMeter1.Enable = chkCpu.Checked;
        }

        void DoGraphics()
        {
            string inputDir;
            string outputDir;
            _boxX = lblGfx.Left;
            _boxY = lblGfx.Bottom + 5;

            var dir = MiscUtils.GetSourcePath();
            inputDir = Path.Join(dir, "files");
            outputDir = Path.Join(dir, "out");
            new DirectoryInfo(outputDir).Create();

            // Read bmp and convert to icon.
            var bmp = (Bitmap)Image.FromFile(Path.Join(inputDir, "glyphicons-22-snowflake.png")); // 26x26
            DumpBitmap(bmp, 10, 1, "raw snowflake");

            // Save icon.
            var ico = GraphicsUtils.CreateIcon(bmp, 32);
            GraphicsUtils.SaveIcon(ico, Path.Join(outputDir, "snowflake_32.ico")); // just 32.

            //var bbb = new Icon(ico, 48, 48);

            ico = GraphicsUtils.CreateIcon(bmp);
            GraphicsUtils.SaveIcon(ico, Path.Join(outputDir, "snowflake_all.ico")); // all sizes

            // Read icon and convert to bmp.
            ico = GraphicsUtils.CreateIcon(Path.Join(inputDir, "crabe.ico"));
            bmp = ico.ToBitmap();
            var box = MakePicBox(bmp.Size, $"icon => bmp");
            box.Image = bmp;
            // Write it back.
            GraphicsUtils.SaveIcon(ico, Path.Join(outputDir, "copy_crabe.ico")); // all original resolutions.

            // Convert grayscale.
            bmp = (Bitmap)Image.FromFile(Path.Join(inputDir, "color-picker-small.png"));
            bmp = GraphicsUtils.ConvertToGrayscale(bmp);
            box = MakePicBox(bmp.Size, $"grayscale");
            box.Image = bmp;

            // Save bmp.
            bmp.Save(Path.Join(outputDir, "grayscale.png"), ImageFormat.Png);

            // Colorize.
            bmp = (Bitmap)Image.FromFile(Path.Join(inputDir, "glyphicons-22-snowflake.png"));
            bmp = GraphicsUtils.ColorizeBitmap(bmp, Color.DeepPink);
            box = MakePicBox(bmp.Size, $"colorize");
            box.Image = bmp;

            // Pixel bitmap.
            int size = 128;
            PixelBitmap pbmp = new(size, size);
            foreach (var y in Enumerable.Range(0, size))
            {
                foreach (var x in Enumerable.Range(0, size))
                {
                    pbmp.SetPixel(x, y, 255, x*2, y*2, 150);
                }
            }
            box = MakePicBox(pbmp.Bitmap.Size, $"pixels");
            box.Image = pbmp.Bitmap;
        }

        /// <summary>
        /// For debug purposes.
        /// </summary>
        /// <param name="sz"></param>
        /// <param name="desc"></param>
        /// <returns></returns>
        PictureBox MakePicBox(Size sz, string desc)
        {
            int min = 100;

            Label lbl = new()
            {
                Location = new(_boxX, _boxY),
                Text = $"{desc}\nw:{sz.Width} h:{sz.Height}",
                Size = new(min, 40)
            };
            Controls.Add(lbl);

            PictureBox box = new()
            {
                BorderStyle = BorderStyle.FixedSingle,
                Location = new(_boxX, _boxY + lbl.Height + 5),
                Size = sz
            };
            Controls.Add(box);

            _boxX += Math.Max(sz.Width + 5, min);

            return box;
        }

        /// <summary>
        /// For debug purposes.
        /// </summary>
        /// <param name="bmp"></param>
        /// <param name="firstRow"></param>
        /// <param name="numRows"></param>
        /// <param name="info"></param>
        void DumpBitmap(Bitmap bmp, int firstRow, int numRows, string info)
        {
            Tell($"Dump Bitmap: {info}");

            if (firstRow >= 0 && firstRow < bmp.Height && numRows > 0)
            {
                for (int y = firstRow; y < firstRow + numRows && y < bmp.Height; y++) // This is not very efficient.
                {
                    for (int x = 0; x < bmp.Width; x++)
                    {
                        // Get the pixel from the image.
                        Color acol = bmp.GetPixel(x, y);
                        Tell($"r:{y} c:{x} {acol}");
                    }
                }
            }
            else
            {
                Tell("Bad rows!");
            }
        }

        /// <summary>
        /// For debug purposes.
        /// </summary>
        /// <param name="msg"></param>
        void Tell(string msg)
        {
            txtInfo.AppendText($"{msg}{Environment.NewLine}");
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
