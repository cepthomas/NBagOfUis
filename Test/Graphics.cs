using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NBagOfTricks;


namespace NBagOfUis.Test
{
    public partial class Graphics : Form
    {
        string _input;
        string _output;

        int _boxX = 5;
        int _boxY = 5;

        readonly List<string> _msgs = new();

        /// <summary>
        /// 
        /// </summary>
        public Graphics()
        {
            InitializeComponent();

            var dir = MiscUtils.GetSourcePath();
            _input = Path.Join(dir, "files");
            _output = Path.Join(dir, "out");
            new DirectoryInfo(_output).Create();

            Run();
        }

        /// <summary>
        /// Go!
        /// </summary>
        void Run()
        {
            _msgs.Clear();

            // Read bmp and convert to icon.
            var bmp = (Bitmap)Image.FromFile(Path.Join(_input, "glyphicons-22-snowflake.png")); // 26x26
            //DumpBitmap(bmp, 10, 1, "raw snowflake");

            // Save icon.
            var ico = GraphicsUtils.CreateIcon(bmp, 32);
            GraphicsUtils.SaveIcon(ico, Path.Join(_output, "snowflake_32.ico")); // just 32.

            var bbb = new Icon(ico, 48, 48);

            ico = GraphicsUtils.CreateIcon(bmp);
            GraphicsUtils.SaveIcon(ico, Path.Join(_output, "snowflake_all.ico")); // all sizes

            // Read icon and convert to bmp.
            ico = GraphicsUtils.CreateIcon(Path.Join(_input, "crabe.ico"));
            bmp = ico.ToBitmap();
            var box = MakePicBox(bmp.Size, $"icon => bmp");
            box.Image = bmp;
            // Write it back.
            GraphicsUtils.SaveIcon(ico, Path.Join(_output, "copy_crabe.ico")); // all original resolutions.

            // Convert grayscale.
            bmp = (Bitmap)Image.FromFile(Path.Join(_input, "color-picker-small.png"));
            bmp = GraphicsUtils.ConvertToGrayscale(bmp);
            box = MakePicBox(bmp.Size, $"grayscale");
            box.Image = bmp;

            // Save bmp.
            bmp.Save(Path.Join(_output, "grayscale.png"), ImageFormat.Png);

            // Colorize.
            bmp = (Bitmap)Image.FromFile(Path.Join(_input, "glyphicons-22-snowflake.png"));
            bmp = GraphicsUtils.ColorizeBitmap(bmp, Color.DeepPink);
            box = MakePicBox(bmp.Size, $"colorize");
            box.Image = bmp;

            //var t = _msgs.Count > 0 ? string.Join(Environment.NewLine, _msgs) : "Nothing to report";
            //Clipboard.SetText(t);
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
            LogMessage($"Dump Bitmap: {info}");

            if (firstRow >= 0 && firstRow < bmp.Height && numRows > 0)
            {
                for (int y = firstRow; y < firstRow + numRows && y < bmp.Height; y++) // This is not very efficient.
                {
                    for (int x = 0; x < bmp.Width; x++)
                    {
                        // Get the pixel from the image.
                        Color acol = bmp.GetPixel(x, y);
                        LogMessage($"r:{y} c:{x} {acol}");
                    }
                }
            }
            else
            {
                LogMessage("ERR Bad rows!");
            }
        }

        /// <summary>
        /// For debug purposes.
        /// </summary>
        /// <param name="msg"></param>
        void LogMessage(string msg)
        {
            _msgs.Add(msg);
            string s = $"{DateTime.Now:mm\\:ss\\.fff} {msg}{Environment.NewLine}";
            rtbInfo.AppendText(s);
        }
    }
}
