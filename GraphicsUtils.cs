using System;
using System.Collections.Generic;
using System.IO;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Windows.Forms;
using System.ComponentModel;
using Ephemera.NBagOfTricks;


namespace Ephemera.NBagOfUis
{
    // Icon manipulation. Bits and pieces borrowed from:
    // https://gist.github.com/darkfall/1656050?permalink_comment_id=2989435#gistcomment-2989435
    // https://gist.github.com/darkfall/1656050
    // https://en.wikipedia.org/wiki/Grayscale#Converting_colour_to_grayscale

    /// <summary>
    /// Static general utility functions.
    /// </summary>
    public static class GraphicsUtils
    {
        /// <summary>
        /// Create icon from a file.
        /// </summary>
        /// <param name="fn"></param>
        /// <returns></returns>
        public static Icon CreateIcon(string fn)
        {
            using FileStream stream = new(fn, FileMode.Open);
            var ico = new Icon(stream);
            return ico;
        }

        /// <summary>
        /// Create icon from a bitmap.
        /// </summary>
        /// <param name="bmpSource">Source image.</param>
        /// <param name="size">Specific size or 0 for all common windows sizes.</param>
        /// <returns></returns>
        public static Icon CreateIcon(Bitmap bmpSource, int size = 0)
        {
            // The standards.
            int[] sizes = size == 0 ? [256, 48, 32, 16] : [size];

            // Generate bitmaps for all the sizes and toss them in streams
            List<MemoryStream> mss = [];

            foreach (int sz in sizes)
            {
                Bitmap result = new(sz, sz);
                result.SetResolution(bmpSource.HorizontalResolution, bmpSource.VerticalResolution);
                using (Graphics graphics = Graphics.FromImage(result))
                {
                    // Set high quality.
                    graphics.CompositingQuality = CompositingQuality.HighQuality;
                    graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    graphics.SmoothingMode = SmoothingMode.HighQuality;
                    // Draw the image.
                    graphics.DrawImage(bmpSource, 0, 0, result.Width, result.Height);
                }

                MemoryStream ms = new();
                result.Save(ms, ImageFormat.Png);
                mss.Add(ms);
            }

            using MemoryStream output = new();
            BinaryWriter bw = new(output);
            int offset = 0;

            // 0-1 reserved, 0
            bw.Write((byte)0);
            bw.Write((byte)0);

            // 2-3 image type, 1 = icon, 2 = cursor
            bw.Write((short)1);

            // 4-5 number of images
            bw.Write((short)sizes.Length);

            offset += 6 + (16 * sizes.Length);

            for (int i = 0; i < sizes.Length; i++)
            {
                // image entry 1
                // 0 image width
                bw.Write((byte)sizes[i]);
                // 1 image height
                bw.Write((byte)sizes[i]);
                // 2 number of colors
                bw.Write((byte)0);
                // 3 reserved
                bw.Write((byte)0);
                // 4-5 color planes
                bw.Write((short)0);
                // 6-7 bits per pixel
                bw.Write((short)32);
                // 8-11 size of image data
                bw.Write((int)mss[i].Length);
                // 12-15 offset of image data
                bw.Write((int)offset);

                offset += (int)mss[i].Length;
            }

            for (int i = 0; i < sizes.Length; i++)
            {
                // Write image data.
                bw.Write(mss[i].ToArray());
                mss[i].Close();
            }

            output.Seek(0, SeekOrigin.Begin);
            Icon ico = new(output);

            return ico;
        }

        /// <summary>
        /// Save to file.
        /// </summary>
        /// <param name="ico"></param>
        /// <param name="fn"></param>
        public static void SaveIcon(Icon ico, string fn)
        {
            using FileStream stream = new(fn, FileMode.OpenOrCreate);
            ico.Save(stream);
        }

        /// <summary>
        /// Recolor a control.
        /// </summary>
        /// <param name="comp"></param>
        /// <param name="clr"></param>
        public static void ColorizeControl(Component comp, Color clr)
        {
            switch (comp)
            {
                case ButtonBase btn:
                    btn.Image = ((Bitmap)btn.Image!).Colorize(clr);
                    break;

                case ToolStripItem btn:
                    btn.Image = ((Bitmap)btn.Image!).Colorize(clr);
                    break;

                default:
                    throw new ArgumentException($"Colorize unkown type {comp.GetType()}");
            }
        }
    }

    public class ToolStripCheckBoxRenderer : ToolStripSystemRenderer
    {
        /// <summary>Color to use when check box is selected.</summary>
        public Color SelectedColor { get; set; }

        /// <summary>
        /// Override for drawing.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnRenderButtonBackground(ToolStripItemRenderEventArgs e)
        {
            if (!(e.Item is not ToolStripButton btn) && btn.CheckOnClick && btn.Checked)
            {
                Rectangle bounds = new(Point.Empty, e.Item.Size);
                e.Graphics.FillRectangle(new SolidBrush(SelectedColor), bounds);
            }
            else
            {
                base.OnRenderButtonBackground(e);
            }
        }
    }
}
