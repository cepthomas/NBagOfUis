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
    public partial class OptionsEditor : Form
    {
        #region Fields
        /// <summary>Working values so we don't destroy originals.</summary>
        Dictionary<string, bool> _values = new();

        /// <summary>Control.</summary>
        readonly TextBox txtAdd;

        /// <summary>Control.</summary>
        readonly CheckedListBox lbValues;

        /// <summary>Control.</summary>
        readonly Button btnCancel;

        /// <summary>Control.</summary>
        readonly Button btnOk;

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
            get { return _values; }
            set { _values = new Dictionary<string, bool>(value); _values.ForEach(kv => lbValues.Items.Add(kv.Key, kv.Value)); }
        }

        /// <summary>Custom label.</summary>
        public string Title { get; set; } = "Options Editor";

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
                Location = new(82, 204),
                Name = "txtAdd",
                Size = new(89, 22)
            };
            Controls.Add(txtAdd);

            lbValues = new()
            {
                CheckOnClick = true,
                Location = new(1, 1),
                Name = "lbValues",
                Size = new(182, 191)
            };
            lbValues.KeyDown += Values_KeyDown;
            Controls.Add(lbValues);

            btnCancel = new()
            {
                DialogResult = DialogResult.Cancel,
                Location = new(96, 238),
                Name = "btnCancel",
                Size = new(75, 30),
                Text = "Cancel",
                UseVisualStyleBackColor = true
            };
            Controls.Add(btnCancel);

            btnOk = new()
            {
                DialogResult = DialogResult.OK,
                Location = new(15, 238),
                Name = "btnOk",
                Size = new(75, 29),
                Text = "OK",
                UseVisualStyleBackColor = true
            };
            Controls.Add(btnOk);

            btnAdd = new()
            {
                Location = new(15, 203),
                Name = "btnAdd",
                Size = new(61, 23),
                Text = "Add:",
                UseVisualStyleBackColor = true
            };
            btnAdd.Click += Add_Click;
            Controls.Add(btnAdd);

            Adjust();

            // Form stuff.
            AcceptButton = btnOk;
            CancelButton = btnCancel;
            ClientSize = new(182, 275);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Name = "OptionsEditor";
            Text = "Option Editor";
            ShowInTaskbar = false;
            ShowIcon = false;
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
        /// Collect list contents.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            _values.Clear();

            for(int i = 0; i < lbValues.Items.Count; i++)
            {
                _values[lbValues.Items[i].ToString()!] = lbValues.GetItemChecked(i);
            }

            base.OnFormClosing(e);
        }

        /// <summary>
        /// Tweak UI area.
        /// </summary>
        void Adjust()
        {
            btnAdd.Visible = _allowEdit;
            txtAdd.Visible = _allowEdit;
            lbValues.Height = _allowEdit ? btnAdd.Top - 2 : btnAdd.Bottom + 1;
        }
    }
}
