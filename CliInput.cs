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
    /// <summary>For hotkeys.</summary>
    public enum Modifier { None, Ctrl, Alt }

    /// <summary>Internal cli entry data container.</summary>
    //internal record CliEntry(string s, Modifier mod);



    //        HashSet<(char, Modifier)> _modifiers = new();



    public class CliInputEventArgs : EventArgs
    {
        /// <summary>Test for hotkey press.</summary>
        public Modifier Mod { get; set; } = Modifier.None;

        /// <summary>User text, no eol.</summary>
        public string Text { get; set; } = "";

        ///// <summary>Client has taken ownership of the data.</summary>
        //public bool Handled { get; set; } = false;
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

        #region Events
        /// <summary>User has entered something.</summary>
        public event EventHandler<CliInputEventArgs>? InputEvent;
        #endregion

        #region Fields
        /// <summary>Contained control.</summary>
        readonly RichTextBox _rtb;

        /// <summary>Most recent at beginning.</summary>
        List<string> _history = [];

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
            char c = char.ToLower((char)e.KeyData);

            switch (e.Control, e.Alt, e.KeyCode)
            {
                case (false, false, Keys.Enter):
                    if (_rtb.Text.Length > 0)
                    {
                        // Add to history and notify client.
                        var t = _rtb.Text;
                        AddToHistory(t);

                        CliInputEventArgs la = new() { Text = t };
                        InputEvent?.Invoke(this, la);
                        // Clear line.
                        _rtb.Text = $"{Prompt}";
                    }
                    break;

                case (false, false, Keys.Escape):
                    // Throw away current.
                    _rtb.Text = $"{Prompt}";
                    break;

                case (false, false, Keys.Up):
                    // Go through history older.
                    if (_historyIndex < _history.Count - 1)
                    {
                        _historyIndex++;
                        _rtb.Text = $"{Prompt}{_history[_historyIndex]}";
                    }
                    break;

                case (false, false, Keys.Down):
                    // Go through history newer.
                    if (_historyIndex > 0)
                    {
                        _historyIndex--;
                        _rtb.Text = $"{Prompt}{_history[_historyIndex]}";
                    }
                    break;

                case (true, false, _) when char.IsAsciiLetterOrDigit(c):
                    // Hot key?
                    InputEvent?.Invoke(this, new() { Mod = Modifier.Ctrl, Text = c.ToString() });
                    break;

                case (false, true, _) when char.IsAsciiLetterOrDigit(c):
                    // Hot key?
                    InputEvent?.Invoke(this, new() { Mod = Modifier.Alt, Text = c.ToString() });
                    break;
            }

            //switch (e.KeyCode)
            //{
            //    case Keys.Enter:
            //        if (_rtb.Text.Length > 0)
            //        {
            //            // Add to history and notify client.
            //            var t = _rtb.Text;
            //            AddToHistory(t);

            //            CliInputEventArgs la = new() { Text = t };
            //            InputEvent?.Invoke(this, la);
            //            // Clear line.
            //            _rtb.Text = $"{Prompt}";
            //        }
            //        break;

            //    case Keys.Escape:
            //        // Throw away current.
            //        _rtb.Text = $"{Prompt}";
            //        break;

            //    case Keys.Up:
            //        // Go through history older.
            //        if (_historyIndex < _history.Count - 1)
            //        {
            //            _historyIndex++;
            //            _rtb.Text = $"{Prompt}{_history[_historyIndex]}";
            //        }
            //        break;

            //    case Keys.Down:
            //        // Go through history newer.
            //        if (_historyIndex > 0)
            //        {
            //            _historyIndex--;
            //            _rtb.Text = $"{Prompt}{_history[_historyIndex]}";
            //        }
            //        break;

            //    default:
            //        if (_rtb.Text.Length == 0) // check hotkey?
            //        {
            //            var ch = (char)e.KeyValue;
            //            CliInputEventArgs ca = new() { Mod_X = Modifier.None };
            //            InputEvent?.Invoke(this, ca);
            //            e.Handled = ca.Handled;
            //        }
            //        break;
            //}
        }
        #endregion
    }
}
