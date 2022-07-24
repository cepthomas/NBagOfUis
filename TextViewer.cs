using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace NBagOfUis
{
    public class TextViewer : UserControl
    {
        #region Properties
        /// <summary>The colors to display when text is matched.</summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Dictionary<string, Color> Colors { get; set; } = new Dictionary<string, Color>();

        /// <summary>Limit the size. Set to 0 to disable.</summary>
        public int MaxText { get; set; } = 50000;

        /// <summary>Cosmetics.</summary>
        public override Color BackColor { get { return _rtb.BackColor; } set { _rtb.BackColor = value; } }

        /// <summary>Cosmetics.</summary>
        public override Font Font { get { return _rtb.Font; } set { _rtb.Font = value; } }

        /// <summary>Word wrap toggle.</summary>
        public bool WordWrap { get { return _rtb.WordWrap; } set { _rtb.WordWrap = value; } }

        /// <summary>Optional prompt.</summary>
        public string Prompt { get; set; } = "";
        #endregion

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
                Font = new Font("Consolas", 10),
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
            AppendLine("Hello. C clears the display and W toggles word wrap");
            base.OnLoad(e);
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _rtb.Dispose();
            }
            base.Dispose(disposing);
        }

       /// <summary></summary>
        public void Clear()
        {
            _rtb.Clear();
        }

        /// <summary>
        /// A message to display to the user. Adds EOL.
        /// </summary>
        /// <param name="text">The message.</param>
        public void AppendLine(string text)
        {
            AppendText($"{Prompt}{text}{Environment.NewLine}");
        }

        /// <summary>
        /// A message to display to the user. Doesn't add EOL.
        /// </summary>
        /// <param name="text">The message.</param>
        public void AppendText(string text)
        {
            if (MaxText > 0 && _rtb.TextLength > MaxText)
            {
                _rtb.Select(0, MaxText / 5);
                _rtb.SelectedText = "";
            }

            _rtb.SelectionBackColor = BackColor; // default

            foreach (string s in Colors.Keys)
            {
                if (text.Contains(s))
                {
                    _rtb.SelectionBackColor = Colors[s];
                    break;
                }
            }

            _rtb.AppendText(text);
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
    }
}
