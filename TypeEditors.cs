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
using NBagOfTricks;


namespace NBagOfUis
{
    /// <summary>Generic property editor for lists of strings.</summary>
    public class StringListEditor : UITypeEditor
    {
        IWindowsFormsEditorService? _service = null;

        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            List<string> ls = value is null ? new() : (List<string>)value;

            TextBox tb = new()
            {
                Multiline = true,
                ReadOnly = false,
                AcceptsReturn = true,
                ScrollBars = ScrollBars.Both,
                Height = 100,
                Text = string.Join(Environment.NewLine, ls)
            };
            tb.Select(0, 0);

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

    /// <summary>Broken - don't use.</summary>
    public class PathListEditor_TODO : UITypeEditor
    {
        IWindowsFormsEditorService? _service = null;

        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            List<string> ls = value is null ? new() : (List<string>)value;

            ListBox lb = new();
            lb.DataSource = ls;

            lb.ContextMenuStrip = new();

            lb.ContextMenuStrip.Opening += (sender, _) =>
            {
                lb.ContextMenuStrip.Items.Clear();
                lb.ContextMenuStrip.Items.Add("Add");
                if (lb.Items.Count > 0)
                {
                    lb.ContextMenuStrip.Items.Add("Remove");

                    if (lb.SelectedIndex > 0)
                    {
                        lb.ContextMenuStrip.Items.Add("Up");
                    }

                    if (lb.SelectedIndex < lb.Items.Count - 1)
                    {
                        lb.ContextMenuStrip.Items.Add("Down");
                    }
                }
            };

            lb.ContextMenuStrip.ItemClicked += (sender, args) =>
            {
                switch (args.ClickedItem.Text)
                {
                    case "Add":
                        FolderBrowserDialog dlg = new()
                        {
                            Description = "Select the folder to add.",
                            ShowNewFolderButton = false
                            //SelectedPath = init?
                        };

                        if (dlg.ShowDialog() == DialogResult.OK) // UI locks here!!
                        {
                            var f = dlg.SelectedPath;
                            lb.Items.Insert(lb.SelectedIndex, f);
                        }
                        break;

                    case "Remove":
                        int i = lb.SelectedIndex;
                        lb.Items.RemoveAt(i);
                        break;

                    case "Up":
                    case "Down":
                        break;
                }
            };

            _service = provider.GetService(typeof(IWindowsFormsEditorService)) as IWindowsFormsEditorService;
            _service?.DropDownControl(lb);

            return ls;
        }

        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context) { return UITypeEditorEditStyle.DropDown; }
    }
}
