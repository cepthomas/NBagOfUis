using System;
using System.Collections.Generic;
using System.IO;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;


namespace Ephemera.NBagOfUis
{
    // Bits and pieces borrowed from:
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
        /// <param name="bmp"></param>
        /// <param name="size">Specific size or 0 for all common windows sizes.</param>
        /// <returns></returns>
        public static Icon CreateIcon(Bitmap bmp, int size = 0)
        {
            // The standards.
            int[] sizes = size == 0 ? new int[] { 256, 48, 32, 16 } : new int[] { size };

            // Generate bitmaps for all the sizes and toss them in streams
            List<MemoryStream> mss = new();

            foreach (int sz in sizes)
            {
                Bitmap outbmp = ResizeBitmap(bmp, sz, sz);
                MemoryStream ms = new();
                outbmp.Save(ms, ImageFormat.Png);
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
        /// De-colorize.
        /// </summary>
        /// <param name="bmp"></param>
        /// <returns></returns>
        public static Bitmap ConvertToGrayscale(Bitmap bmp)
        {
            Bitmap result = new(bmp.Width, bmp.Height);

            // Conversion algo (worst to best)
            // - Simple averaging.
            // - channel-dependent luminance perception
            //   Y = R * 0.2126 + G * 0.7152 + B * 0.0722; // 0.0 to 255.0
            // - gamma-compression-corrected approximation:
            //   Y = 0.299 R + 0.587 G + 0.114 B
            ColorMatrix mat = new(new float[][]
            {
                new float[] {.30f, .30f, .30f,  0,  0},
                new float[] {.59f, .59f, .59f,  0,  0},
                new float[] {.11f, .11f, .11f,  0,  0},
                new float[] {  0,    0,    0,   1,  0},
                new float[] {  0,    0,    0,   0,  1}
            });

            // Identity matrix for dev.
            //ColorMatrix mat = new(new float[][]
            //{
            //    new float[] {  1,  0,  0,  0,  0},
            //    new float[] {  0,  1,  0,  0,  0},
            //    new float[] {  0,  0,  1,  0,  0},
            //    new float[] {  0,  0,  0,  1,  0},
            //    new float[] {  0,  0,  0,  0,  1}
            //});

            using (Graphics g = Graphics.FromImage(result))
            {
                using ImageAttributes attributes = new();
                attributes.SetColorMatrix(mat);
                g.DrawImage(bmp, new Rectangle(0, 0, bmp.Width, bmp.Height), 0, 0, bmp.Width, bmp.Height, GraphicsUnit.Pixel, attributes);
            }

            return result;
        }

        /// <summary>
        /// Resize the image to the specified width and height.
        /// </summary>
        /// <param name="bmp">The image to resize.</param>
        /// <param name="width">The width to resize to.</param>
        /// <param name="height">The height to resize to.</param>
        /// <returns>The resized image.</returns>
        public static Bitmap ResizeBitmap(Bitmap bmp, int width, int height)
        {
            Bitmap result = new(width, height);
            result.SetResolution(bmp.HorizontalResolution, bmp.VerticalResolution);

            using (Graphics graphics = Graphics.FromImage(result))
            {
                // Set high quality.
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;

                // Draw the image.
                graphics.DrawImage(bmp, 0, 0, result.Width, result.Height);
            }

            return result;
        }

        /// <summary>
        /// Colorize a bitmap. Mainly for beautifying glyphicons.
        /// </summary>
        /// <param name="original"></param>
        /// <param name="newcol"></param>
        /// <param name="replace">Optional source color to replace. Defaults to black.</param>
        /// <returns></returns>
        public static Bitmap ColorizeBitmap(Bitmap original, Color newcol, Color replace = default)
        {
            Bitmap newbmp = new(original.Width, original.Height);

            for (int y = 0; y < newbmp.Height; y++) // This is not very efficient. Use a matrix instead.
            {
                for (int x = 0; x < newbmp.Width; x++)
                {
                    // Get the pixel from the image.
                    // 0 is fully transparent, and 255 is fully opaque
                    Color acol = original.GetPixel(x, y);

                    if(acol.R == replace.R && acol.G == replace.G && acol.B == replace.B)
                    {
                        acol = Color.FromArgb(acol.A, newcol.R, newcol.G, newcol.B);
                    }
                    newbmp.SetPixel(x, y, acol);
                }
            }

            return newbmp;
        }

        /// <summary>
        /// Helper to get next contrast color in the sequence.
        /// From http://colorbrewer2.org qualitative.
        /// </summary>
        /// <param name="i"></param>
        /// <param name="dark">Dark or light series, usually dark.</param>
        /// <returns></returns>
        public static Color GetSequenceColor(int i, bool dark = true)
        {
            Color col = Color.Black;

            switch (i % 8)
            {
                case 0: col = dark ? Color.FromArgb(27, 158, 119) : Color.FromArgb(141, 211, 199); break;
                case 1: col = dark ? Color.FromArgb(217, 95, 2) : Color.FromArgb(255, 255, 179); break;
                case 2: col = dark ? Color.FromArgb(117, 112, 179) : Color.FromArgb(190, 186, 218); break;
                case 3: col = dark ? Color.FromArgb(231, 41, 138) : Color.FromArgb(251, 128, 114); break;
                case 4: col = dark ? Color.FromArgb(102, 166, 30) : Color.FromArgb(128, 177, 211); break;
                case 5: col = dark ? Color.FromArgb(230, 171, 2) : Color.FromArgb(253, 180, 98); break;
                case 6: col = dark ? Color.FromArgb(166, 118, 29) : Color.FromArgb(179, 222, 105); break;
                case 7: col = dark ? Color.FromArgb(102, 102, 102) : Color.FromArgb(252, 205, 229); break;
            }

            return col;
        }

        /// <summary>
        /// Mix two colors.
        /// </summary>
        /// <param name="one"></param>
        /// <param name="two"></param>
        /// <returns></returns>
        public static Color HalfMix(Color one, Color two)
        {
            return Color.FromArgb(
                (one.A + two.A) >> 1,
                (one.R + two.R) >> 1,
                (one.G + two.G) >> 1,
                (one.B + two.B) >> 1);
        }
    }
}
