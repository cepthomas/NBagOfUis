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
        /// <summary>The colors to display when text is matched.</summary>
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

        #region Public appenders
        /// <summary>
        /// A message to display to the user. Adds EOL.
        /// </summary>
        /// <param name="text">The message.</param>
        /// <param name="color">Specific color to use.</param>
        public void AppendLine(string text, Color? color = null)
        {
            AppendText($"{Prompt}{text}{Environment.NewLine}", color);
        }

        /// <summary>
        /// A message to display to the user. Doesn't add EOL.
        /// </summary>
        /// <param name="text">The message.</param>
        /// <param name="color">Specific color to use.</param>
        public void AppendText(string text, Color? color = null)
        {
            this.InvokeIfRequired(_ =>
            {
                // Trim buffer.
                if (MaxText > 0 && _rtb.TextLength > MaxText)
                {
                    _rtb.Select(0, MaxText / 5);
                    _rtb.SelectedText = "";
                }

                _rtb.SelectionBackColor = BackColor; // default

                if (color is not null)
                {
                    _rtb.SelectionBackColor = (Color)color;
                }
                else // check matches
                {
                    foreach (string s in MatchText.Keys)
                    {
                        if (text.Contains(s))
                        {
                            _rtb.SelectionBackColor = MatchText[s];
                            break;
                        }
                    }
                }

                _rtb.AppendText(text);
                _rtb.ScrollToCaret();
            });
        }
        
        /// <summary>
        /// Output text wwith explicit color.
        /// </summary>
        /// <param name="text">The text to show.</param>
        /// <param name="fg"></param>
        /// <param name="bg"></param>
        /// <param name="nl">Add new line.</param>
        public void AppendColor(string text, Color? fg = null, Color? bg = null, bool nl = true)
        {
            this.InvokeIfRequired(_ =>
            {
                _rtb.SelectionColor = (Color)(fg == null ? _rtb.ForeColor : fg);
                _rtb.SelectionBackColor = (Color)(bg == null ? _rtb.SelectionBackColor : bg);

                Write(text, nl);
            });
        }

        /// <summary>
        /// Output text using text matching color.
        /// </summary>
        /// <param name="text">The text to show.</param>
        /// <param name="line">Color whole line.</param>
        /// <param name="nl">Add new line.</param>
        public void AppendMatch(string text, bool line = true, bool nl = true)
        {
            this.InvokeIfRequired(_ =>
            {
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
                    Write("TODO debug this??");
                    return;

                    foreach (string s in MatchText.Keys)
                    {
                        bool lineDone = false;
                        while (!lineDone)
                        {
                            var pos = text.IndexOf(s);
                            if (pos == -1)
                            {
                                lineDone = true;
                            }
                            else
                            {
                            }
                        }
                    }
                }
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
        /// Low level output. Assumes caller has taken care of cross-thread issues.
        /// </summary>
        void Write(string text, bool eol = true)
        {
            // Trim buffer.
            if (_rtb.TextLength > MaxText)
            {
                int end = MaxText / 5;
                while (_rtb.Text[end] != (char)0x0A) end++;
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
