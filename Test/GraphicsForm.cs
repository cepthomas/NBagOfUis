﻿using System;
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
using Ephemera.NBagOfTricks;


namespace Ephemera.NBagOfUis.Test
{
    public partial class GraphicsForm : Form
    {
        int _boxX = 5;
        int _boxY = 5;

        public GraphicsForm()
        {
            InitializeComponent();

            DoGraphics();
        }

        void DoGraphics()
        {
            string inputDir;
            string outputDir;

            _boxX = Left + 5;
            _boxY = txtInfo.Bottom + 5;

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

            ico = GraphicsUtils.CreateIcon(bmp);
            GraphicsUtils.SaveIcon(ico, Path.Join(outputDir, "snowflake_all.ico")); // all sizes

            // Read icon and convert to bmp.
            ico = GraphicsUtils.CreateIcon(Path.Join(inputDir, "crabe.ico"));
            bmp = ico.ToBitmap();
            var box = MakePicBox(bmp.Size, $"icon => bmp");
            box.Image = bmp;
            // Write it back.
            GraphicsUtils.SaveIcon(ico, Path.Join(outputDir, "copy_crabe.ico")); // all original resolutions.
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
        /// For debug purposes. This is not very efficient, could be improved.
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
                for (int y = firstRow; y < firstRow + numRows && y < bmp.Height; y++)
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
            txtInfo.AppendColor(msg);
        }
    }
}
