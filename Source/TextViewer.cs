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

        /// <summary></summary>
        public bool WordWrap { get { return _rtb.WordWrap; } set { _rtb.WordWrap = value; } }
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
            AppendText($"{text}{Environment.NewLine}");
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
    }
}
