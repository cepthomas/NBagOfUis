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
        public Dictionary<string, Color> MatchText { get; set; } = [];

        /// <summary>Cosmetics. Note this needs to be set in client constructor not OnLoad(). Unknown reason.</summary>
        public override Color BackColor { get { return _rtb.BackColor; } set { _rtb.BackColor = value; } }

        /// <summary>Cosmetics.</summary>
        public override Font Font { get { return _rtb.Font; } set { _rtb.Font = value; } }

        /// <summary>Word wrap toggle.</summary>
        public bool WordWrap { get { return _rtb.WordWrap; } set { _rtb.WordWrap = value; } }

        /// <summary>Optional prompt.</summary>
        public string Prompt { get; set; } = "";

        /// <summary>Limit the size.</summary>
        public int MaxText { get; set; } = 10000;
        #endregion

        #region Fields
        /// <summary>Contained control.</summary>
        readonly RichTextBox _rtb;

        /// <summary>Ansi parse state.</summary>
        ParseState _state = ParseState.Idle;
        enum ParseState
        {
            Idle,       /// Plain text
            Look,       /// Looking for '[' in sequence
            Collect     /// Collect sequence arrgs
        }

        /// <summary>Accumulated ansi arguments.</summary>
        string _ansiArgs = "";

        // Keys.
        const char CANCEL = (char)0x03;
        const char BACKSPACE = (char)0x08;
        const char TAB = (char)0x09; // horizontal tab
        const char LINEFEED = (char)0x0A; // Line feed
        const char CLEAR = (char)0x0C; // Form feed
        const char RETURN = (char)0x0D; // Carriage return
        const char ESCAPE = (char)0x1B;
        #endregion

        #region Lifecycle
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

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
        #endregion

        #region Public appenders
        /// <summary>
        /// Output text wwith explicit color.
        /// </summary>
        /// <param name="text">The text to show.</param>
        /// <param name="fg"></param>
        /// <param name="bg"></param>
        /// <param name="nl">Add new line.</param>
        public void Append(string text, Color? fg = null, Color? bg = null, bool nl = true)
        {
            this.InvokeIfRequired(_ =>
            {
                _rtb.SelectionColor = (Color)(fg == null ? _rtb.ForeColor : fg);
                _rtb.SelectionBackColor = (Color)(bg == null ? _rtb.SelectionBackColor : bg);

                Write(text, nl);
            });
        }

        /// <summary>
        /// Output text wwith ansi encoding.    
        /// </summary>
        /// <param name="text">The text to show.</param>
        /// <param name="nl">Add new line.</param>
        public void AppendAnsi(string text, bool nl = true)
        {
            this.InvokeIfRequired(_ =>
            {
                for (int i = 0; i < text.Length; i++)
                {
                    char c = text[i];

                    switch (_state, c)
                    {
                        case (ParseState.Idle, ESCAPE):
                            _state = ParseState.Look;
                            break;

                        case (ParseState.Idle, RETURN):
                            _ansiArgs = "";
                            Write("");
                            break;

                        case (ParseState.Look, '['):
                            _ansiArgs = "";
                            _state = ParseState.Collect;
                            break;

                        case (ParseState.Collect, 'm'):
                            var (fg, bg) = ColorFromAnsi(_ansiArgs);
                            _rtb.SelectionColor = fg;
                            _rtb.SelectionBackColor = bg;
                            _state = ParseState.Idle;
                            _ansiArgs = "";
                            break;

                        case (ParseState.Collect, _):
                            _ansiArgs += c;
                            break;

                        // TODO these useful?
                        //case (ParseState.Idle, CANCEL):
                        //case (ParseState.Idle, BACKSPACE):
                        //case (ParseState.Idle, TAB):
                        //case (ParseState.Idle, LINEFEED):
                        //case (ParseState.Idle, CLEAR):
                        //    break;

                        case (ParseState.Idle, _):
                            Write(c.ToString(), false);
                            break;

                        case (_, _):
                            // Anything else is a syntax error.
                            Write($"ERROR<{_ansiArgs}", false);
                            _state = ParseState.Idle;
                            break;
                    }
                }

                Write("", nl);
            });
        }

        /// <summary>
        /// Output text using text matching color.
        /// </summary>
        /// <param name="text">The text to show.</param>
        /// <param name="line">Light up whole line otherwise just the word.</param>
        /// <param name="nl">Add new line.</param>
        public void AppendMatch(string text, bool line = true, bool nl = true)
        {
            this.InvokeIfRequired(_ =>
            {
                //TODO use regex?
                if (line)
                {
                    foreach (string s in MatchText.Keys)
                    {
                        if (text.Contains(s))
                        {
                            _rtb.SelectionBackColor = MatchText[s];
                            break;
                        }
                    }

                    Write(text, nl);
                }
                else
                {
                    foreach (string s in MatchText.Keys)
                    {
                        int ind = 0;
                        var pos = text.IndexOf(s);
                        if (pos == -1)
                        {
                            continue;
                        }
                    }

                    Write(text, nl);
                }
            });
        }
        #endregion

        #region Private functions
        /// <summary>
        /// Low level output. Assumes caller has taken care of cross-thread issues.
        /// </summary>
        void Write(string text, bool eol = true)
        {
            // Trim buffer.
            if (_rtb.TextLength > MaxText)
            {
                int end = MaxText / 5;
                while (_rtb.Text[end] != LINEFEED) end++;
                _rtb.Select(0, end);
                _rtb.SelectedText = "";
            }

            _rtb.AppendText(text);
            if (eol)
            {
                _rtb.AppendText(Environment.NewLine);
            }
            _rtb.ScrollToCaret();
        }

        /// <summary>
        /// Decode ansi escape sequence arguments.
        /// </summary>
        /// <param name="ansi">Ansi args string</param>
        /// <returns>Foreground and background colors. Color is Empty if invalid ansi string.</returns>
        (Color fg, Color bg) ColorFromAnsi(string ansi)
        {
            Color fg = Color.Empty;
            Color bg = Color.Empty;

            var parts = ansi.SplitByToken(";").Select(i => int.Parse(i)).ToList();

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
                        _rtb.Clear();
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
        #endregion
    }
}
