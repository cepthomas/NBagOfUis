using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Ephemera.NBagOfTricks;


namespace Ephemera.NBagOfUis
{
    public partial class OptionsEditor : UserControl
    {
        #region Fields
        /// <summary>Working values so we don't destroy originals.</summary>
        Dictionary<string, bool> _values = new();

        /// <summary>Control.</summary>
        readonly TextBox txtAdd;

        /// <summary>Control.</summary>
        readonly CheckedListBox lbValues;

        /// <summary>Control.</summary>
        readonly Button btnAdd;

        /// <summary>Flag.</summary>
        bool _allowEdit = false;
        #endregion

        #region Properties
        /// <summary>The values to edit. Key is text, value is bool enable. Clone in case user cancels.</summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        public Dictionary<string, bool> Values
        { 
            get
            {
                _values.Clear();
                for (int i = 0; i < lbValues.Items.Count; i++)
                {
                    _values[lbValues.Items[i].ToString()!] = lbValues.GetItemChecked(i);
                }
                return _values;
            }
            set
            {
                _values = new Dictionary<string, bool>(value);
                _values.ForEach(kv => lbValues.Items.Add(kv.Key, kv.Value));
            }
        }

        /// <summary>If true, user can add and delete values, otherwise just select.</summary>
        public bool AllowEdit
        {
            get { return _allowEdit; }
            set { _allowEdit = value; Adjust(); }
        }
        #endregion

        /// <summary>
        /// Constructor.
        /// </summary>
        public OptionsEditor()
        {
            txtAdd = new()
            {
                Name = "txtAdd",
                Size = new(90, 22)
            };
            Controls.Add(txtAdd);

            btnAdd = new()
            {
                Name = "btnAdd",
                Size = new(60, 27),
                Text = "Add:",
                UseVisualStyleBackColor = true
            };
            btnAdd.Click += Add_Click;
            Controls.Add(btnAdd);

            lbValues = new()
            {
                //CheckOnClick = true,
                Name = "lbValues",
            };
            lbValues.KeyDown += Values_KeyDown;
            Controls.Add(lbValues);
        }

        /// <summary>
        /// Create everything.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            txtAdd.Location = new(82, 208);
            btnAdd.Location = new(15, 207);
            lbValues.Location = new(1, 1);
            lbValues.Size = new(Width, Height - btnAdd.Top);

            Adjust();

            base.OnLoad(e);
        }

        /// <summary>
        /// User is adding a new value.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Add_Click(object? sender, EventArgs e)
        {
            // If textbox is not empty, add to collection.
            string s = txtAdd.Text.Trim().Replace(" ", "_");
            if (s.Length > 0)
            {
                lbValues.Items.Add(s, true);
            }
            Adjust();
        }

        /// <summary>
        /// User might be removing a value.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Values_KeyDown(object? sender, KeyEventArgs e)
        {
            if(AllowEdit && e.KeyCode == Keys.Delete)
            {
                lbValues.Items.RemoveAt(lbValues.SelectedIndex);
                e.Handled = true;
            }
        }

        /// <summary>
        /// Tweak UI area.
        /// </summary>
        void Adjust()
        {
            btnAdd.Visible = _allowEdit;
            txtAdd.Visible = _allowEdit;
            lbValues.Height = _allowEdit ? btnAdd.Top - 1 : btnAdd.Bottom + 1;
        }
    }
}
