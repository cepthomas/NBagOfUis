using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.ComponentModel;
using System.Windows.Forms;
using System.Drawing;
using Ephemera.NBagOfTricks;


namespace Ephemera.NBagOfUis
{
    public class TermInputEventArgs : EventArgs
    {
        /// <summary>Test for key press on empty line.</summary>
        public char? HotKey { get; set; }

        /// <summary>Full line, no eol.</summary>
        public string? Line { get; set; } = null;

        /// <summary>Client has taken ownership of the data.</summary>
        public bool Handled { get; set; } = false;
    }

    public class CliInput : UserControl
    {
        #region Properties
        /// <summary>Cosmetics.</summary>
        public override Color BackColor { get { return _rtb.BackColor; } set { _rtb.BackColor = value; } }

        /// <summary>Cosmetics.</summary>
        public override Font Font { get { return _rtb.Font; } set { _rtb.Font = value; } }

        /// <summary>Optional prompt.</summary>
        public string Prompt { get; set; } = "???";
        #endregion

        #region Fields
        /// <summary>User has entered something.</summary>
        public event EventHandler<TermInputEventArgs>? InputEvent;
        #endregion

        #region Fields
        /// <summary>Contained control.</summary>
        readonly RichTextBox _rtb;

        /// <summary>Most recent at beginning.</summary>
        List<string> _history = new();

        /// <summary>Current location in list.</summary>
        int _historyIndex = 0;
        #endregion

        #region Lifecycle
        /// <summary>
        /// Constructor sets some defaults.
        /// </summary>
        public CliInput()
        {
            _rtb = new()
            {
                Dock = DockStyle.Fill,
                Font = new Font("Consolas", 10),
                BorderStyle = BorderStyle.None,
                ForeColor = Color.Black,
                Multiline = false,
                ReadOnly = false,
                ScrollBars = RichTextBoxScrollBars.Horizontal,
                AcceptsTab = true,
                TabIndex = 0,
                Text = ""
            };

            _rtb.KeyDown += Rtb_KeyDown;
            Controls.Add(_rtb);
        }
        #endregion

        #region Misc functions
        /// <summary>
        /// Update the history with the new entry.
        /// </summary>
        /// <param name="s"></param>
        void AddToHistory(string s)
        {
            if (s.Length > 0)
            {
                var newlist = new List<string> { s };
                // Check for dupes and max size.
                _history.ForEach(v => { if (!newlist.Contains(v) && newlist.Count <= 20) newlist.Add(v); });
                _history = newlist;
                _historyIndex = 0;
            }
        }
        #endregion

        #region Handle input
        /// <summary>
        /// Catch a few keys.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Rtb_KeyDown(object? sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    if (_rtb.Text.Length > 0)
                    {
                        // Add to history and notify client.
                        var t = _rtb.Text;
                        AddToHistory(t);
                        TermInputEventArgs la = new() { Line = t };
                        InputEvent?.Invoke(this, la);
                        // Clear line.
                        _rtb.Text = $"{Prompt}";
                    }
                    break;

                case Keys.Escape:
                    // Throw away current.
                    _rtb.Text = $"{Prompt}";
                    break;

                case Keys.Up:
                    // Go through history older.
                    if (_historyIndex < _history.Count - 1)
                    {
                        _historyIndex++;
                        _rtb.Text = $"{Prompt}{_history[_historyIndex]}";
                    }
                    break;

                case Keys.Down:
                    // Go through history newer.
                    if (_historyIndex > 0)
                    {
                        _historyIndex--;
                        _rtb.Text = $"{Prompt}{_history[_historyIndex]}";
                    }
                    break;

                default:
                    if (_rtb.Text.Length == 0) // check hotkey?
                    {
                        var ch = (char)e.KeyValue;
                        TermInputEventArgs ca = new() { HotKey = ch };
                        InputEvent?.Invoke(this, ca);
                        e.Handled = ca.Handled;
                    }
                    break;
            }
        }
        #endregion
    }
}
