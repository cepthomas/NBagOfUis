using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using Ephemera.NBagOfTricks;


namespace Ephemera.NBagOfUis
{
    public class TextViewer : UserControl
    {
        #region Properties
        /// <summary>The colors to display when text is matched. TODO option for word or whole line.</summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Dictionary<string, Color> MatchText { get; set; } = new Dictionary<string, Color>();

        /// <summary>Limit the size. Set to 0 to disable.</summary>
        public int MaxText { get; set; } = 50000;

        /// <summary>Cosmetics. Note this needs to be set in client constructor not OnLoad(). Unknown reason.</summary>
        public override Color BackColor { get { return _rtb.BackColor; } set { _rtb.BackColor = value; } }

        /// <summary>Cosmetics.</summary>
        public override Font Font { get { return _rtb.Font; } set { _rtb.Font = value; } }

        /// <summary>Word wrap toggle.</summary>
        public bool WordWrap { get { return _rtb.WordWrap; } set { _rtb.WordWrap = value; } }

        /// <summary>Optional prompt.</summary>
        public string Prompt { get; set; } = "";
        #endregion



        /// <summary>Use ansi escape sequences, otherwise use MatchText.</summary>
        public bool AnsiColor { get; set; } = false;

        /// <summary>Buffer height in lines.</summary>
        public int BufferSize { get; set; } = 120;



        #region Fields
        /// <summary>Contained control.</summary>
        readonly RichTextBox _rtb;
        #endregion

        /// <summary>
        /// Constructor sets some defaults.
        /// </summary>
        public TextViewer()
        {
            _rtb = new()
            {
                Dock = DockStyle.Fill,
                Font = new Font("Cascadia Code", 9),
                BorderStyle = BorderStyle.None,
                ForeColor = Color.Black,
                ReadOnly = true,
                ScrollBars = RichTextBoxScrollBars.Both
            };

            _rtb.KeyDown += Rtb_KeyDown;
            Controls.Add(_rtb);
        }

        /// <summary>
        /// Ready to see.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            //AppendLine("Hello. C clears the display and W toggles word wrap");
        }

        /// <summary></summary>
        public void Clear()
        {
            _rtb.Clear();
        }

        // Name dec oct  hex   C   key Description
        // BS   8   010  0x08  \b  ^H  Backspace
        // HT   9   011  0x09  \t  ^I  Horizontal TAB
        // LF   10  012  0x0A  \n  ^J  Linefeed (newline)
        // FF   12  014  0x0C  \f  ^L  Formfeed (new page)
        // CR   13  015  0x0D  \r  ^M  Carriage return
        // ESC  27  033  0x1B  \e* ^[  Escape character

        // Keys.
        const char CANCEL = (char)0x03;
        const char BACKSPACE = (char)0x08;
        const char TAB = (char)0x09; // horizontal tab
        const char LINEFEED = (char)0x0A; // Line feed
        const char CLEAR = (char)0x0C; // Form feed
        const char RETURN = (char)0x0D; // Carriage return
        const char ESCAPE = (char)0x1B;

        // Cancel = 0x03,
        // Back = 0x08,
        // Tab = 0x09,
        // LineFeed = 0x0A,
        // Clear = 0x0C,
        // Return = 0x0D,
        // Escape = 0x1B,

        // standard colors
        // ESC[30–37m

        // ESC[38;2;R;G;Bm   Set foreground color as RGB.
        // ESC[48;2;R;G;Bm   Set background color as RGB.
        // 
        // ESC[38;5;IDm   Set foreground color.
        // ESC[48;5;IDm   Set background color.
        // 
        // # Ex: Set style to bold, red foreground.
        // ESC[1;31mHello
        // # Set style to dimmed white foreground with red background.
        // ESC[2;37;41mWorld


        public enum ParseState { Idle, Look, Collect }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="s"></param>
        void Parse(string s)
        {
            ParseState state = ParseState.Idle;

            string args = "";
            Color fgCurrent = Color.Empty;
            Color bgCurrent = Color.Empty;


            for (int i = 0; i < s.Length; i++)
            {
                char c = s[i];

                switch (c)
                {
                    case ESCAPE:
                        state = ParseState.Look;
                        break;

                    case RETURN:
                        // Reset.
                        fgCurrent = Color.Empty;
                        bgCurrent = Color.Empty;
                        args = "";
                        break;

                    default:
                        switch (state)
                        {
                            case ParseState.Look:
                                if (c == '[')
                                {
                                    state = ParseState.Collect;
                                }
                                else
                                {
                                    // syntax error
                                }
                                break;

                            case ParseState.Collect:
                                if (c == 'm')
                                {
                                    var (fg, bg) = ColorFromAnsi(args);
                                    fgCurrent = fg;
                                    bgCurrent = bg;
                                    state = ParseState.Idle;
                                }
                                else
                                {
                                    args += c;
                                }
                                break;

                            case ParseState.Idle:

                                break;
                        }
                        if (state == ParseState.Look && c == '[')
                        {
                            state = ParseState.Collect;
                        }

                        break;

                    case CANCEL:
                    case BACKSPACE:
                    case TAB:
                    case LINEFEED:
                    case CLEAR:
                        // TODO these useful?
                        break;
                }
            }
        }

        /// <summary>
        /// append-explicit text, color, [eol]
        /// </summary>
        /// <param name="text"></param>
        /// <param name="fgColor"></param>
        /// <param name="bgColor"></param>
        /// <param name="eol"></param>
        public void Append(string text, Color? fgColor = null, Color? bgColor = null, bool eol = true)
        {
            this.InvokeIfRequired(_ =>
            {
                _rtb.ForeColor = (Color)(fgColor == null ? _rtb.ForeColor : fgColor);
                _rtb.SelectionBackColor = (Color)(bgColor == null ? _rtb.SelectionBackColor : bgColor);

                Write(text, eol);
            });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        /// <param name="eol"></param>
        public void AppendAnsi(string text, bool eol = true)
        {
            this.InvokeIfRequired(_ =>
            {
                Parse(text);

                //text = Ansi.ColorFromAnsi(text, false);

                // ESC = \033
                // One of: ESC[IDm  ESC[38;5;IDm  ESC[48;5;IDm  ESC[38;2;R;G;Bm  ESC[48;2;R;G;Bm
                //var (color, invert) = Ansi.ColorFromAnsi("bad string");
                //UT_TRUE(color.IsEmpty);
                //(color, invert) = Ansi.ColorFromAnsi("\033[34m");
                //UT_FALSE(invert);
                //UT_EQUAL(color.Name, "ff00007f");

                //    if (_settings.AnsiColor)
                //    {
                //        _logColors.TryGetValue(e.Level, out int color);
                //        Write($"\u001b[{color}m{e.Message}\u001b[0m");
                //    }
                //    else
                //    {
                //        Write(e.Message);
                //    }

                Write(text, eol);
            });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        /// <param name="eol"></param>
        public void AppendMatch(string text, bool eol = true)
        {
            this.InvokeIfRequired(_ =>
            {
                // check matches  use regex 
                foreach (string s in MatchText.Keys)
                {
                    if (text.Contains(s))
                    {
                        _rtb.SelectionBackColor = MatchText[s];
                        break;
                    }
                }

                Write(text, eol);
            });
        }

        /// <summary>
        /// 
        /// </summary>
        void Write(string text, bool eol)
        {
            // Trim buffer.
            if (MaxText > 0 && _rtb.TextLength > MaxText)
            {
                _rtb.Select(0, MaxText / 5); // TODO by full lines.
                _rtb.SelectedText = "";
            }

            _rtb.AppendText(text);
            if (eol)
            {
                _rtb.AppendText(Environment.NewLine);
            }
            _rtb.ScrollToCaret();
        }

        ///// <summary>
        ///// A message to display to the user. Doesn't add EOL.
        ///// </summary>
        ///// <param name="text">The message.</param>
        ///// <param name="color">Explicit color to use.</param>
        //public void AppendText(string text, Color? color = null)
        //{
        //    if (AnsiColor)
        //    {
        //    }
        //    else
        //    {
        //    }
            
        //    this.InvokeIfRequired(_ =>
        //    {
        //        _rtb.SelectionBackColor = BackColor; // default
        //        if (color is not null)
        //        {
        //            _rtb.SelectionBackColor = (Color)color;
        //        }
        //        else if (AnsiColor)
        //        {
        //        }
        //        else 
        //        {
        //        }
        //        _rtb.AppendText(text);
        //        _rtb.ScrollToCaret();
        //    });
        //}

        ///// <summary>
        ///// A message to display to the user. Adds EOL.
        ///// </summary>
        ///// <param name="text">The message.</param>
        ///// <param name="color">Specific selection color to use.</param>
        //public void AppendLine(string text, Color? color = null)
        //{
        //    AppendText($"{Prompt}{text}{Environment.NewLine}", color);
        //}

        /// <summary>
        /// Catch a few keys.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Rtb_KeyDown(object? sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.C:
                    if (e.Modifiers == 0)
                    {
                        Clear();
                        e.Handled = true;
                    }
                    break;

                case Keys.W:
                    if (e.Modifiers == 0)
                    {
                        WordWrap = !WordWrap;
                        e.Handled = true;
                    }
                    break;
            }
        }


        ////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Convert ansi specs like: ESC[IDm  ESC[38;5;IDm ESC[38;2;R;G;Bm to Color.
        /// </summary>
        /// <param name="ansi">Ansi string</param>
        /// <returns>Color and whether it's fg or bg. Color is Empty if invalid ansi string.</returns>
        //public (Color color, bool invert) ColorFromAnsi(string ansi)
        public (Color fg, Color bg) ColorFromAnsi(string ansi)
        {
            Color fg = Color.Empty;
            Color bg = Color.Empty;
            //bool invert = false;

            var shansi = ansi.Replace("\033[", "").Replace("m", "");
            var parts = shansi.SplitByToken(";").Select(i => int.Parse(i)).ToList();

            var p0 = parts.Count >= 1 ? parts[0] : 0;
            var p1 = parts.Count >= 2 ? parts[1] : 0;
            var p2 = parts.Count == 3 ? parts[2] : 0;

            switch (parts.Count)
            {
                /////// Standard 8/16 colors. ESC[IDm
                case 1 when p0 >= 30 && p0 <= 37:
                    fg = MakeStdColor(p0 - 30);
                    break;

                case 1 when p0 >= 40 && p0 <= 47:
                    bg = MakeStdColor(p0 - 40);
                    //invert = true;
                    break;

                case 1 when p0 >= 90 && p0 <= 97:
                    fg = MakeStdColor(p0 - 90);
                    break;

                case 1 when p0 >= 100 && p0 <= 107:
                    bg = MakeStdColor(p0 - 100);
                    //invert = true;
                    break;

                /////// 256 colors. ESC[38;5;IDm  ESC[48;5;IDm
                case 3 when (p0 == 38 || p0 == 48) && p1 == 5 && p2 >= 0 && p2 <= 15:
                    // 256 colors - standard color.
                    var clr1 = MakeStdColor(p2);
                    if (p0 == 48) bg = clr1; else fg = clr1;
                    break;

                case 3 when (p0 == 38 || p0 == 48) && p1 == 5 && p2 >= 16 && p2 <= 231:
                    // 256 colors - rgb color.
                    int[] map6 = [0, 95, 135, 175, 215, 255];
                    int im = p2 - 16;
                    int r = map6[im / 36];
                    int g = map6[(im / 6) % 6];
                    int b = map6[im % 6];

                    var clr2 = Color.FromArgb(r, g, b);
                    if (p0 == 48) bg = clr2; else fg = clr2;
                    break;

                case 3 when (p0 == 38 || p0 == 48) && p1 == 5 && p2 >= 232 && p2 <= 255:
                    // 256 colors - grey
                    int i = p2 - 232; // 0 - 23
                    int grey = i * 10 + 8;

                    var clr3 = Color.FromArgb(grey, grey, grey);
                    if (p0 == 48) bg = clr3; else fg = clr3;
                    break;

                /////// Explicit rgb colors. ESC[38;2;R;G;Bm  ESC[48;2;R;G;Bm
                case 5 when p0 == 38 || p0 == 48 && p1 == 2:

                    var clr4 = Color.FromArgb(parts[2], parts[3], parts[4]);
                    if (p0 == 48) bg = clr4; else fg = clr4;
                    break;
            }

            return (fg, bg);
    
            static Color MakeStdColor(int id)
            {
                (int r, int g, int b)[] std_colors = [
                    (0, 0, 0), (127, 0, 0), (0, 127, 0), (127, 127, 0), (0, 0, 127), (127, 0, 127), (0, 127, 127), (191, 191, 191),
                    (127, 127, 127), (0, 0, 0), (0, 255, 0), (255, 255, 0), (0, 0, 255), (255, 0, 255), (0, 255, 255), (255, 255, 255)];
                return Color.FromArgb(std_colors[id].r, std_colors[id].g, std_colors[id].b);
            }
        }
    }
}
