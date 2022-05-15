using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.Design;


namespace NBagOfUis
{
    /// <summary>Custom renderer for toolstrip checkbox color.</summary>
    public class CheckBoxRenderer : ToolStripSystemRenderer
    {
        /// <summary>Color to use when check box is selected.</summary>
        public Color SelectedColor { get; set; }

        /// <summary>
        /// Override for drawing.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnRenderButtonBackground(ToolStripItemRenderEventArgs e)
        {
            if (!(e.Item is not ToolStripButton btn) && btn.CheckOnClick && btn.Checked)
            {
                Rectangle bounds = new(Point.Empty, e.Item.Size);
                e.Graphics.FillRectangle(new SolidBrush(SelectedColor), bounds);
            }
            else
            {
                base.OnRenderButtonBackground(e);
            }
        }
    }
}
