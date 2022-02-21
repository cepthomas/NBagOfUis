﻿using System;
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
        /// <summary>
        /// Color to use when check box is selected.
        /// </summary>
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

    /// <summary>Toolstrip checkbox button colorizer.</summary>
    public class TsRenderer : ToolStripProfessionalRenderer
    {
        public Color SelectedColor { get; set; } = Color.LightSalmon;

        protected override void OnRenderButtonBackground(ToolStripItemRenderEventArgs e)
        {
            if (e.Item is ToolStripButton btn && btn.CheckOnClick)
            {
                using (var brush = new SolidBrush(btn.Checked ? SelectedColor : SystemColors.Control))
                {
                    var bounds = new Rectangle(Point.Empty, e.Item.Size);
                    e.Graphics.FillRectangle(brush, bounds);
                }
            }
            else
            {
                base.OnRenderButtonBackground(e);
            }
        }
    }

    /// <summary>Generic property editor for lists of strings.</summary>
    public class ListEditor : UITypeEditor
    {
        private IWindowsFormsEditorService? _service = null;

        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            List<string> ls = value is null ? new() : (List<string>)value;

            TextBox tb = new TextBox
            {
                Multiline = true,
                ReadOnly = false,
                AcceptsReturn = true,
                ScrollBars = ScrollBars.Both,
                Height = (ls.Count + 1) * 30,
                Text = string.Join(Environment.NewLine, ls)
            };
            _service = provider.GetService(typeof(IWindowsFormsEditorService)) as IWindowsFormsEditorService;
            _service?.DropDownControl(tb);
            ls = tb.Text.SplitByToken(Environment.NewLine);

            return ls;
        }

        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context) { return UITypeEditorEditStyle.DropDown; }
    }

    /// <summary>Selector for monospaced fonts.</summary>
    public class MonospaceFontEditor : UITypeEditor
    {
        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            FontDialog dlg = new()
            {
                FixedPitchOnly = true,
                Font = value as Font
            };


            return dlg.ShowDialog() == DialogResult.OK ? dlg.Font! : value;
        }

        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context) { return UITypeEditorEditStyle.Modal; }
    }
}