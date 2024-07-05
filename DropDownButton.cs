using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Linq;
using Ephemera.NBagOfTricks;


namespace Ephemera.NBagOfUis
{
    /// <summary>Button with builtin Drop down selector.</summary>
    public class DropDownButton : Button
    {
        #region Events
        /// <summary>Drop down selection event.</summary>
        public event EventHandler<string>? Selected;
        #endregion

        #region Fields
        /// <summary>The menu.</summary>
        readonly ContextMenuStrip _menu = new();
        #endregion

        /// <summary>
        /// Constructor.
        /// </summary>
        public DropDownButton()
        {
            ContextMenuStrip = _menu;
        }

        /// <summary>
        /// Populate menu with options.
        /// </summary>
        /// <param name="options"></param>
        public void SetOptions(List<string> options)
        {
            _menu.Items.Clear();
            options.ForEach(o => _menu.Items.Add(o == "" ? new ToolStripSeparator() : new ToolStripMenuItem(o, null, Menu_Click)));
        }

        /// <summary>
        /// Handle drop down selection.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Menu_Click(object? sender, EventArgs e)
        {
            Selected?.Invoke(this, sender!.ToString()!);
        }

        /// <summary>
        /// Handle mouse down.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseDown(MouseEventArgs e)
        {
            _menu.Show(this, 0, Height);
        }

        /// <summary>
        /// Draw everything including a little arrow.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            int x = ClientRectangle.Width - 12;
            int y = ClientRectangle.Height - 12;

            using var br = new SolidBrush(Color.Gray);
            Point[] arrows = new[] { new Point(x, y), new Point(x + 10, y), new Point(x + 5, y + 10) };
            e.Graphics.FillPolygon(br, arrows);
        }
    }
}