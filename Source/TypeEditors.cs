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
    /// <summary>Generic property editor for lists of strings.</summary>
    public class ListEditor : UITypeEditor
    {
        IWindowsFormsEditorService? _service = null;

        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            List<string> ls = value is null ? new() : (List<string>)value;

            TextBox tb = new TextBox
            {
                Multiline = true,
                ReadOnly = false,
                AcceptsReturn = true,
                ScrollBars = ScrollBars.Both,
                Height = 100,
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
