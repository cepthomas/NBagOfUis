using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using System.Reflection;
using System.Drawing;
using System.Drawing.Design;
using System.Diagnostics;


namespace Ephemera.NBagOfUis
{
    /// <summary>Extends the PropertyGrid to add some features.</summary>
    public class PropertyGridEx : PropertyGrid
    {
        #region Properties
        /// <summary>Edited flag.</summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        public bool Dirty { get; set; } = false;
        #endregion

        #region Internal PropertyGrid control fields
        readonly Control? _propertyGridView;
        readonly Control? _docComment;
        readonly ToolStrip? _toolstrip;
        #endregion

        #region Events
        /// <summary>The property grid is reporting something.</summary>
        public event EventHandler<PropertyGridExEventArgs>? PropertyGridExChange;

        /// <summary>General event for raising events not natively supported by the property grid.</summary>
        public class PropertyGridExEventArgs : EventArgs
        {
            /// <summary>General info.</summary>
            public string EventType { get; set; } = "";

            /// <summary>General data.</summary>
            public object? EventData { get; set; } = null;
        }

        /// <summary>Children can call this to send something back to the host.</summary>
        public void RaisePropertyGridExEvent(string eventType, object? ps = null)
        {
            PropertyGridExChange?.Invoke(this, new PropertyGridExEventArgs() { EventType = eventType, EventData = ps });
        }
        #endregion

        /// <summary>Initializes a new instance of the class.</summary>
        public PropertyGridEx()
        {
            // Add any initialization after the InitializeComponent() call.
            SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.SupportsTransparentBackColor, true);

            foreach (var c in Controls)
            {
                string sc = c.ToString()!;

                if (sc.Contains("PropertyGridInternal.DocComment"))
                {
                    _docComment = (Control?)c;
                    // Magical knowledge.
                    //var _docCommentTitle = _docComment.Controls[0] as Label;
                    //_docCommentTitle.BackColor = Color.Green;
                    //var _docCommentDescription = _docComment.Controls[1] as Label;
                    //_docCommentDescription.BackColor = Color.Orange;
                }
                else if (sc.Contains("PropertyGridInternal.PropertyGridView"))
                {
                    _propertyGridView = (Control?)c;
                }
                else if (sc.Contains("PropertyGridToolStrip"))
                {
                    _toolstrip = (ToolStrip?)c;
                }
            }

            // Capture user edits.
            PropertyValueChanged += (_, __) => Dirty = true;

            Dirty = false;
        }

        /// <summary>Add a custom button to the property grid.</summary>
        /// <param name="text"></param>
        /// <param name="image"></param>
        /// <param name="tooltip"></param>
        /// <param name="onClick"></param>
        /// <returns>The button or null if failed.</returns>
        public ToolStripButton? AddButton(string text, Image? image, string tooltip, EventHandler onClick)
        {
            ToolStripButton? btn = null;

            if(_toolstrip is not null)
            {
                btn = new ToolStripButton(text, image, onClick) { ToolTipText = tooltip };
                _toolstrip.Items.Add(btn);
            }

            return btn;
        }

        /// <summary>Add a label to the property grid.</summary>
        /// <param name="text"></param>
        /// <param name="image"></param>
        /// <param name="tooltip"></param>
        /// <returns>The label or null if failed.</returns>
        public ToolStripLabel? AddLabel(string text, Image? image, string tooltip)
        {
            ToolStripLabel? lbl = null;

            if (_toolstrip is not null)
            {
                lbl = new(text, image) { ToolTipText = tooltip };
                _toolstrip.Items.Add(lbl);
            }

            return lbl;
        }

        /// <summary>Add a separator to the property grid.</summary>
        /// <returns>The separator or null if failed.</returns>
        public ToolStripSeparator? AddSeparator()
        {
            ToolStripSeparator? sep = null;

            if (_toolstrip is not null)
            {
                sep = new ToolStripSeparator();
                _toolstrip.Items.Add(sep);
            }

            return sep;
        }

        /// <summary>Moves the vertical splitter.</summary>
        /// <param name="x"></param>
        public void MoveSplitter(int x)
        {
            if(_propertyGridView is not null)
            {
                var mtf = _propertyGridView.GetType().GetMethod("MoveSplitterTo",
                    BindingFlags.NonPublic | BindingFlags.InvokeMethod | BindingFlags.Instance);
                mtf!.Invoke(_propertyGridView, new object[] { (int)x });
            }
        }

        /// <summary>Alter the bottom description area. Must be in OnLoad()!</summary>
        /// <param name="numl">Number of lines to show.</param>
        public void ResizeDescriptionArea(int numl)
        {
            if (_docComment is not null)
            {
                var field = _docComment.GetType().BaseType!.GetField("<UserSized>k__BackingField", BindingFlags.Instance | BindingFlags.NonPublic);
                field!.SetValue(_docComment, true);
                var prop = _docComment.GetType().GetProperty("Lines");
                prop!.SetValue(_docComment, numl, null);
                HelpVisible = true;
            }
        }

        /// <summary>Expand or collapse the group.</summary>
        /// <param name="groupName">Name of the group to act on.</param>
        /// <param name="expand">Expand or collapse.</param>
        public void ExpandGroup(string groupName, bool expand)
        {
            if(SelectedGridItem is not null)
            {
                GridItem root = SelectedGridItem;

                // Get the parent
                while (root.Parent is not null)
                {
                    root = root.Parent;
                }

                foreach (GridItem g in root.GridItems)
                {
                    if (g.GridItemType == GridItemType.Category && g.Label is not null && g.Label.Trim() == groupName.Trim())
                    {
                        g.Expanded = expand;
                        break;
                    }
                }
            }
        }

        /// <summary>Show or hide a named property.</summary>
        /// <param name="which">Name of the property.</param>
        /// <param name="visible">True or false.</param>
        public void ShowProperty(string which, bool visible)
        {
            // Manipulate the browsable attribute.
            PropertyDescriptorCollection pdc = TypeDescriptor.GetProperties(SelectedObject);
            PropertyDescriptor descriptor = pdc[which]!;

            if (descriptor is not null)
            {
                // Get the backing field because the property has no setter.
                BrowsableAttribute attribute = (BrowsableAttribute)descriptor.Attributes[typeof(BrowsableAttribute)]!;
                //var ff = attribute.GetType().GetRuntimeFields();
                FieldInfo fieldToChange = attribute.GetType().GetField("<Browsable>k__BackingField", BindingFlags.NonPublic | BindingFlags.Instance)!;
                fieldToChange.SetValue(attribute, visible);

                // Force an update.
                Refresh();
            }
        }
    }
}