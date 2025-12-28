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
using Ephemera.NBagOfTricks;

// Shut up warnings in this file. Using manual checking where needed.
#pragma warning disable CS8602

namespace Ephemera.NBagOfUis
{
    //----------------------------------------------------------------
    /// <summary>Generic property editor for lists of strings.</summary>
    public class StringListEditor : UITypeEditor
    {
        IWindowsFormsEditorService? _service = null;

        public override object EditValue(ITypeDescriptorContext? context, IServiceProvider provider, object? value)
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

        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext? context) { return UITypeEditorEditStyle.DropDown; }
    }

    //----------------------------------------------------------------
    /// <summary>Selector for monospaced fonts.</summary>
    public class MonospaceFontEditor : UITypeEditor
    {
        public override object EditValue(ITypeDescriptorContext? context, IServiceProvider provider, object? value)
        {
            FontDialog dlg = new()
            {
                FixedPitchOnly = true,
                Font = (value as Font)!
            };

            return dlg.ShowDialog() == DialogResult.OK ? dlg.Font : (value as Font)!;
        }

        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext? context) { return UITypeEditorEditStyle.Modal; }
    }

    //----------------------------------------------------------------
    /// <summary>Select from list supplied to cache. Value can be int or string.</summary>
    public class GenericListTypeEditor : UITypeEditor
    {
        #region Store global property options here.
        static readonly Dictionary<string, List<string>> _options = [];

        /// <summary>These get set by the client.</summary>
        public static void SetOptions(string propName, List<string> options)
        {
            _options[propName] = options;
        }

        public static List<string> GetOptions(string propName)
        {
            if (!_options.TryGetValue(propName, out var result))
            {
                throw new InvalidOperationException($"No options provided for property {propName}");
            }
            return result;
        }
        #endregion

        /// <summary>Standard property editor.</summary>
        public override object? EditValue(ITypeDescriptorContext? context, IServiceProvider provider, object? value)
        {
            IWindowsFormsEditorService? _service = provider.GetService(typeof(IWindowsFormsEditorService)) as IWindowsFormsEditorService;

            var propName = context.PropertyDescriptor.Name;
            var propType = context.PropertyDescriptor.PropertyType;
            var options = GetOptions(propName);

            var lb = new ListBox
            {
                Width = 150,
                SelectionMode = SelectionMode.One
            };
            lb.Click += (_, __) => _service!.CloseDropDown();
            options.ForEach(v => lb.Items.Add(v));
            _service!.DropDownControl(lb);

            var ret = propType switch
            {
                var t when t == typeof(int) || t == typeof(int?) => lb.SelectedIndex, // TODO1 handle -1 case
                var t when t == typeof(string) => lb.SelectedItem, // TODO1 handle -1 case
                _ => throw new InvalidOperationException($"Property {propName} type must in or string")
            };

            return ret;
        }

        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext? context) { return UITypeEditorEditStyle.DropDown; }
    }

    //----------------------------------------------------------------
    /// <summary>Convert between list int and string versions.</summary>
    public class GenericConverter : Int64Converter
    {
        /// <summary>int to string</summary>
        public override object? ConvertTo(ITypeDescriptorContext? context, System.Globalization.CultureInfo? culture, object? value, Type destinationType)
        {
            if (context is null || context.Instance is null || value is not int || (int)value < 0) { return base.ConvertTo(context, culture, value, destinationType); }
            var propName = context.PropertyDescriptor.Name;
            var options = GenericListTypeEditor.GetOptions(propName);
            return options[(int)value];
        }

        /// <summary>string to int</summary>
        public override object? ConvertFrom(ITypeDescriptorContext? context, System.Globalization.CultureInfo? culture, object value)
        {
            if (context is null || context.Instance is null || value is not int || (int)value < 0) { return base.ConvertFrom(context, culture, value); }
            var propName = context.PropertyDescriptor.Name;
            var options = GenericListTypeEditor.GetOptions(propName);
            var res = options.FirstOrDefault(ch => ch == (string)value);
            return res;
        }
    }

    //----------------------------------------------------------------
    /// <summary>Broken - don't use.</summary>
    public class PathListEditor : UITypeEditor // TODO doesn't work yet.
    {
        IWindowsFormsEditorService? _service = null;

        public override object EditValue(ITypeDescriptorContext? context, IServiceProvider provider, object? value)
        {
            List<string> ls = value is null ? new() : (List<string>)value;

            ListBox lb = new()
            {
                DataSource = ls,
                ContextMenuStrip = new()
            };

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
                switch (args.ClickedItem!.Text)
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

        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext? context) { return UITypeEditorEditStyle.DropDown; }
    }
}
