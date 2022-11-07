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
        #region Component Designer generated code
        CheckedListBox lbOptions;
        TextBox txtEdit;
        ContextMenuStrip cms;
        #endregion

        #region Properties
        /// <summary>The values to edit. Key is text, value is bool enable.</summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        public Dictionary<string, bool> Options
        { 
            get
            {
                Dictionary<string, bool> options = new();
                for (int i = 0; i < lbOptions.Items.Count; i++)
                {
                    options[lbOptions.Items[i].ToString()!] = lbOptions.GetItemChecked(i);
                }
                return options;
            }
            set
            {
                lbOptions.Items.Clear();
                value.ForEach(kv => lbOptions.Items.Add(kv.Key, kv.Value));
            }
        }

        /// <summary>If true, user can add and delete values, otherwise just select.</summary>
        public bool AllowEdit { get; set; }
        #endregion

        /// <summary>
        /// Constructor.
        /// </summary>
        public OptionsEditor()
        {
            lbOptions = new()
            {
                FormattingEnabled = true,
                Location = new(17, 12),
                Name = "lbOptions",
                Size = new(107, 92),
                TabIndex = 0,

                BorderStyle = BorderStyle.None,
                Dock = DockStyle.Fill,
                ContextMenuStrip = cms
            };

            txtEdit = new()
            {
                Location = new(8, 156),
                Name = "txtEdit",
                Size = new(125, 27),
                TabIndex = 2,

                Visible = false
            };
            txtEdit.KeyDown += TxtEdit_KeyDown;
            txtEdit.Leave += TxtEdit_Leave;

            cms = new()
            {
                ImageScalingSize = new(20, 20),
                Name = "cms",
                Size = new(123, 100)
            };

            ToolStripMenuItem allToolStripMenuItem = new()
            {
                Name = "allToolStripMenuItem",
                Size = new(122, 24),
                Text = "All"
            };
            allToolStripMenuItem.Click += All_Click;
            cms.Items.Add(allToolStripMenuItem);

            ToolStripMenuItem noneToolStripMenuItem = new()
            {
                Name = "noneToolStripMenuItem",
                Size = new(122, 24),
                Text = "None"
            };
            noneToolStripMenuItem.Click += None_Click;
            cms.Items.Add(noneToolStripMenuItem);

            ToolStripMenuItem addToolStripMenuItem = new()
            {
                Name = "addToolStripMenuItem",
                Size = new(122, 24),
                Text = "Add"
            };
            addToolStripMenuItem.Click += Add_Click;
            cms.Items.Add(addToolStripMenuItem);

            ToolStripMenuItem deleteToolStripMenuItem = new()
            {
                Name = "deleteToolStripMenuItem",
                Size = new(122, 24),
                Text = "Delete"
            };
            deleteToolStripMenuItem.Click += Delete_Click;
            cms.Items.Add(deleteToolStripMenuItem);

            AutoScaleDimensions = new(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(txtEdit);
            Controls.Add(lbOptions);
            Name = "OptionsEditor";
            Size = new(171, 276);
            BackColorChanged += (_, __) => lbOptions.BackColor = BackColor;
        }

        /// <summary>
        /// Select all.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void All_Click(object? sender, EventArgs e)
        {
            for (int i = 0; i < lbOptions.Items.Count; i++)
            {
                lbOptions.SetItemChecked(i, true);
            }
        }

        /// <summary>
        /// Select none.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void None_Click(object? sender, EventArgs e)
        {
            for (int i = 0; i < lbOptions.Items.Count; i++)
            {
                lbOptions.SetItemChecked(i, false);
            }
        }

        /// <summary>
        /// Delete selection.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Delete_Click(object? sender, EventArgs e)
        {
            if (lbOptions.SelectedIndex >= 0)
            {
                lbOptions.Items.RemoveAt(lbOptions.SelectedIndex);
            }
        }

        /// <summary>
        /// Add new one.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Add_Click(object? sender, EventArgs e)
        {
            txtEdit.Text = "";
            txtEdit.Location = new(5, PointToClient(MousePosition).Y);
            txtEdit.Visible = true;
            txtEdit.Focus();
            txtEdit.Select(0, 0);
        }

        /// <summary>
        /// Process return and escape keys.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void TxtEdit_KeyDown(object? sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Return:
                    // Indicates validate and save.
                    lbOptions.Focus();
                    break;

                case Keys.Escape:
                    // Indicates cancel.
                    txtEdit.Text = "";
                    lbOptions.Focus();
                    break;

                default:
                    // Normal op.
                    break;
            }
        }

        /// <summary>
        /// Validate and save edit.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void TxtEdit_Leave(object? sender, EventArgs e)
        {
            if (txtEdit.Text != "")
            {
                // If textbox is not empty, add to collection.
                string s = txtEdit.Text.Trim().Replace(" ", "_");
                lbOptions.Items.Add(s, true);
            }

            txtEdit.Visible = false;
        }
    }
}
