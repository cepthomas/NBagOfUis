using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using System.Drawing.Design;
using Ephemera.NBagOfTricks;


// TODO maybe delete/copy/move, hover: filters, fullpath, size, thumbnail.


namespace Ephemera.NBagOfUis
{
    /// <summary>
    /// Tree control with file type filters.
    /// </summary>
    public partial class FilTree : UserControl
    {
        #region Properties
        /// <summary>Stuff of interest to the user.</summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public FilTreeSettings Settings { get; set; } = new();

        /// <summary>Client supplies these.</summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        public List<string> RecentFiles { get; set; } = new();
        #endregion

        #region Events
        /// <summary>User has selected a file.</summary>
        public event EventHandler<string>? FileSelectedEvent = null;
        #endregion

        #region Types
        /// <summary>Convenience container.</summary>
        class ListFileInfo
        {
            public string Name { get; set; } = "";
            public string FullName { get; set; } = "";
            public string VisibleName { get; set; } = "";
            public override string ToString() { return VisibleName; }
        }
        #endregion

        #region Lifecycle
        /// <summary>
        /// Default constructor.
        /// </summary>
        public FilTree()
        {
            InitializeComponent();

            treeView.HideSelection = false;

            // Populate the context menus.
            treeView.ContextMenuStrip = new();
            treeView.ContextMenuStrip.Items.Add("Copy Name", null, (_, __0) => { GetInfo(treeView, "Name"); });
            treeView.ContextMenuStrip.Items.Add("Copy Path", null, (_, __0) => { GetInfo(treeView, "Path"); });

            lbFiles.ContextMenuStrip = new();
            lbFiles.ContextMenuStrip.Items.Add("Copy Name", null, (_, __0) => { GetInfo(lbFiles, "Name"); });
            lbFiles.ContextMenuStrip.Items.Add("Copy Path", null, (_, __0) => { GetInfo(lbFiles, "Path"); });

            lbFiles.MouseClick += (object? sender, MouseEventArgs e) => FileSelected(e);
            lbFiles.MouseDoubleClick += (object? sender, MouseEventArgs e) => FileSelected(e);

            // TODO? hover: filters, fullpath, size, thumbnail
        }

        /// <summary>
        /// Populate everything from the properties.
        /// </summary>
        public void Init()
        {
            // Show what we have.
            UpdateFromSettings();

            PopulateTreeView();

            if(treeView.Nodes.Count > 0)
            {
                treeView.SelectedNode = treeView.Nodes[0];
                PopulateFileList(treeView.Nodes[0]);
            }
        }
        #endregion

        #region Tree View
        /// <summary>
        /// Fill the tree.
        /// </summary>
        void PopulateTreeView()
        {
            treeView.Nodes.Clear();

            // Recent files first.
            treeView.Nodes.Add(new TreeNode("Recent"));
            Settings.RootDirs.RemoveAll(d => !Directory.Exists(d));
            Settings.RootDirs = Settings.RootDirs.Distinct().ToList();

            foreach (string path in Settings.RootDirs)
            {
                DirectoryInfo info = new(path);
                TreeNode node = new(info.Name) { Tag = info };

                ShowDirectories(info.GetDirectories(), node);
                treeView.Nodes.Add(node);
            }

            //// Open them up a bit.
            //foreach (TreeNode n in treeView.Nodes)
            //{
            //    n.Expand();
            //}
        }

        /// <summary>
        /// Recursively drill down through the directory structure and populate the tree.
        /// </summary>
        /// <param name="dirs"></param>
        /// <param name="parentNode"></param>
        void ShowDirectories(DirectoryInfo[] dirs, TreeNode parentNode)
        {
            foreach (DirectoryInfo dir in dirs)
            {
                if (!Settings.IgnoreDirs.Contains(dir.Name))
                {
                    if (dir.Attributes == FileAttributes.Directory) // ignore system and hidden etc.
                    {
                        TreeNode subDirNode = new(dir.Name, 0, 0)
                        {
                            Tag = dir,
                            ImageKey = "folder"
                        };

                        // Go a little lower now.
                        DirectoryInfo[] subDirs = dir.GetDirectories();
                        ShowDirectories(subDirs, subDirNode);
                        parentNode.Nodes.Add(subDirNode);
                    }
                }
            }
        }

        /// <summary>
        /// Drill down through dirs/files.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void TreeView_NodeMouseClick(object? sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                PopulateFileList(e.Node);
            }
        }
        #endregion

        #region File List
        /// <summary>
        /// Populate the file selector.
        /// </summary>
        /// <param name="node">Selected node.</param>
        void PopulateFileList(TreeNode node)
        {
            lbFiles.Items.Clear();

            if (node.Tag is DirectoryInfo)
            {
                var nodeDirInfo = node.Tag as DirectoryInfo;
                nodeDirInfo!.EnumerateFiles("*").OrderBy(f => char.IsLetterOrDigit(f.Name[0])).ForEach(finfo => DoOne(finfo, false));
            }
            else if (node.Text == "Recent")
            {
                RecentFiles.ForEach(fn => DoOne(new FileInfo(fn), true));
            }

            ///// Local common function.
            void DoOne(FileInfo finfo, bool full)
            {
                var ext = Path.GetExtension(finfo.Name).ToLower();

                if (Settings.FilterExts.Contains(ext))
                {
                    int kb = (int)(finfo.Length / 1024); // size not size on disk
                    int mb = kb / 1024;
                    string slen = kb > 9999 ? $"{mb}M" : $"{kb}K";

                    var item = new ListFileInfo()
                    {
                        Name = finfo.Name,
                        FullName = finfo.FullName,
                        VisibleName = $"{(full ? finfo.FullName : finfo.Name)} ({slen})"
                    };

                    lbFiles.Items.Add(item);
                }
            }
        }

        /// <summary>
        /// Handler for single or double mouse clicks.
        /// </summary>
        /// <param name="e"></param>
        void FileSelected(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if ((Settings.SingleClickSelect && e.Clicks == 1) || (!Settings.SingleClickSelect && e.Clicks >= 2))
                {
                    ListFileInfo? fi = lbFiles.SelectedItem as ListFileInfo;
                    if (fi is not null)
                    {
                        FileSelectedEvent?.Invoke(this, fi.FullName);
                    }
                }
            }
        }
        #endregion

        #region Misc privates
        /// <summary>
        /// Context menu handler.
        /// </summary>
        /// <param name="ctl"></param>
        /// <param name="which"></param>
        void GetInfo(Control ctl, string which)
        {
            if (ctl == treeView)
            {
                var node = treeView.SelectedNode;
                if (node is not null && node.Tag is DirectoryInfo)
                {
                    var nodeDirInfo = node.Tag as DirectoryInfo;
                    string s = which == "Path" ? nodeDirInfo!.FullName : nodeDirInfo!.Name;
                    Clipboard.SetText(s);
                }
            }
            else if (ctl == lbFiles)
            {
                ListFileInfo? fi = lbFiles.SelectedItem as ListFileInfo;
                if (fi is not null)
                {
                    string s = which == "Path" ? fi.FullName : fi.Name;
                    Clipboard.SetText(s);
                }
            }
        }

        /// <summary>
        /// Edit the settings.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Edit_Click(object? sender, EventArgs e)
        {
            var changes = SettingsEditor.Edit(Settings, "Edit me!!!", 400);
            //changes.ForEach(ch => Debug.WriteLine($"change name:{ch.name} cat:{ch.cat}"));

            // Detect changes of interest.
            bool navChange = false;
            bool restart = false;

            foreach (var (name, cat) in changes)
            {
                switch (name)
                {
                    case "RootDirs":
                    case "FilterExts":
                    case "IgnoreDirs":
                        navChange = true;
                        break;
                }
            }

            if (restart)
            {
                MessageBox.Show("Restart required for device changes to take effect");
            }

            if (navChange)
            {
                Init();
            }

            UpdateFromSettings();
        }

        /// <summary>
        /// Update from settings.
        /// </summary>
        void UpdateFromSettings()
        {
            lblActiveFilters.Text = "TODO - what?";// "Filters: " + (Settings.FilterExts.Count == 0 ? "None" : string.Join(" ", Settings.FilterExts));
            splitContainer.SplitterDistance = Settings.SplitterPosition * Width / 100;
        }
        #endregion
    }

    public class FilTreeSettings
    {
        #region Properties
        [DisplayName("Root Paths")]
        [Description("Your favorite places.")]
        [Browsable(true)]
        [Editor(typeof(StringListEditor), typeof(UITypeEditor))]
        public List<string> RootDirs { get; set; } = new();

        [DisplayName("Filters")]
        [Description("Show only these file types. Empty is valid for files without extensions.")]
        [Browsable(true)]
        [Editor(typeof(StringListEditor), typeof(UITypeEditor))]
        public List<string> FilterExts { get; set; } = new();

        /// <summary></summary>
        [DisplayName("Ignore Paths")]
        [Description("Ignore these noisy directories.")]
        [Browsable(true)]
        [Editor(typeof(StringListEditor), typeof(UITypeEditor))]
        public List<string> IgnoreDirs { get; set; } = new();

        [DisplayName("Splitter Position")]
        [Description("Percent of width.")]
        [Browsable(true)]
        public int SplitterPosition { get; set; } = 30;

        [DisplayName("Single Click Select")]
        [Description("Generate event with single or double click.")]
        [Browsable(true)]
        public bool SingleClickSelect { get; set; } = false;
        #endregion
    }
}
