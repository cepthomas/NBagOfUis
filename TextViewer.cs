using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Diagnostics.CodeAnalysis;
using Ephemera.NBagOfTricks;


namespace Ephemera.NBagOfUis
{
    public class TextViewer : UserControl
    {
        /// <summary>Spec to identify and colorize line regions.</summary>
        readonly public record struct Matcher (string Text, Color? FgColor = null, Color? BgColor = null);

        #region Properties
        /// <summary>The colors to display when text is matched.</summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public List<Matcher> Matchers
        {
            set { _matchers.Clear(); value.ForEach(v => _matchers.Add(v.Text, v)); }
        }
        public Dictionary<string, Matcher> _matchers = [];

        /// <summary>Cosmetics. Note this needs to be set in client constructor not OnLoad(). Unknown reason.</summary>
        public override Color BackColor { get { return _rtb.BackColor; } set { _rtb.BackColor = value; } }

        /// <summary>Cosmetics.</summary>
        [AllowNull]
        public override Font Font { get { return _rtb.Font; } set { _rtb.Font = value; } }

        /// <summary>Word wrap toggle.</summary>
        public bool WordWrap { get { return _rtb.WordWrap; } set { _rtb.WordWrap = value; } }

        /// <summary>Optional prompt.</summary>
        public string Prompt { get; set; } = "";

        /// <summary>Limit the size.</summary>
        public int MaxText { get; set; } = 10000;

        /// <summary>Colorize mathces fore or back.</summary>
        public bool MatchUseBackground { get; set; } = true;
        #endregion

        #region Fields
        /// <summary>Contained control.</summary>
        readonly RichTextBox _rtb;
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
                ScrollBars = RichTextBoxScrollBars.Both,
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

        #region Public text appenders
        /// <summary>
        /// A message to display to the user. Adds EOL.
        /// </summary>
        /// <param name="text">The message.</param>
        /// <param name="nl">Add NL.</param>
        public void Append(string text, bool nl = true)
        {
            this.InvokeIfRequired(_ =>
            {
                Color? fg = null;
                Color? bg = null;

                foreach (var s in _matchers.Keys)
                {
                    if (text.Contains(s))
                    {
                        var mm = _matchers[s];
                        fg = mm.FgColor;
                        bg = mm.BgColor;
                        break;
                    }
                }

                AppendText(text, nl, fg, bg);
            });
        }

        /// <summary>
        /// Clean up.
        /// </summary>
        public void Clear()
        {
            _rtb.Clear();
        }
        #endregion

        #region Private functions
        /// <summary>
        /// Low level appender with color.
        /// </summary>
        /// <param name="text">The message.</param>
        /// <param name="nl">Full line.</param>
        /// <param name="fgColor">Specific color to use.</param>
        /// <param name="bgColor">Specific color to use.</param>
        void AppendText(string text, bool nl, Color? fgColor = null, Color? bgColor = null)
        {
            this.InvokeIfRequired(_ =>
            {
                // Trim buffer.
                if (MaxText > 0 && _rtb.TextLength > MaxText)
                {
                    _rtb.Select(0, MaxText / 5);
                    _rtb.SelectedText = "";
                }

                _rtb.SelectionColor = fgColor ?? ForeColor;
                _rtb.SelectionBackColor = bgColor ?? BackColor;
                _rtb.AppendText(text);

                if (nl)
                {
                    _rtb.AppendText(Environment.NewLine);
                    _rtb.ScrollToCaret();
                }
            });
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
