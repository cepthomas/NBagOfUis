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
        /// <summary>Client may supply these.</summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        public List<string>? RecentFiles { get; set; } = null;

        /// <summary>Your favorite places.</summary>
        public List<string> RootDirs { get; set; } = new();

        /// <summary>Show only these file types. Empty is valid for files without extensions..</summary>
        public List<string> FilterExts { get; set; } = new();

        /// <summary>Ignore these noisy directories</summary>
        public List<string> IgnoreDirs { get; set; } = new();

        /// <summary>Splitter Position as Percent of width..</summary>
        public int SplitterPosition
        {
            get { return splitContainer.SplitterDistance * 100 / Width; }
            set { splitContainer.SplitterDistance = value * Width / 100; }
        }

        /// <summary>Generate event with single or double click..</summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        public bool SingleClickSelect { get; set; } = false;
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
            treeView.ContextMenuStrip.Items.Add("Copy Name", null, (_, __) => { GetInfo(treeView, "Name"); });
            treeView.ContextMenuStrip.Items.Add("Copy Path", null, (_, __) => { GetInfo(treeView, "Path"); });

            lbFiles.ContextMenuStrip = new();
            lbFiles.ContextMenuStrip.Items.Add("Copy Name", null, (_, __) => { GetInfo(lbFiles, "Name"); });
            lbFiles.ContextMenuStrip.Items.Add("Copy Path", null, (_, __) => { GetInfo(lbFiles, "Path"); });

            lbFiles.MouseClick += (object? sender, MouseEventArgs e) => FileSelected(e);
            lbFiles.MouseDoubleClick += (object? sender, MouseEventArgs e) => FileSelected(e);

            btnEdit.Click += (_, __) => {  }; //TODO1 something
            //lblActiveFilters.Text = "Filters: " + (FilterExts.Count == 0 ? "None" : string.Join(" ", FilterExts));
        }

        /// <summary>
        /// Populate everything from the properties.
        /// </summary>
        public void Init()
        {
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
            RootDirs.RemoveAll(d => !Directory.Exists(d));
            RootDirs.Distinct().ForEach(d =>
            {
                DirectoryInfo info = new(d);
                TreeNode node = new(info.Name) { Tag = info };
                ShowDirectories(info.GetDirectories(), node);
                treeView.Nodes.Add(node);
            });

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
                if (!IgnoreDirs.Contains(dir.Name))
                {
                    // Ignore system and hidden etc.
                    bool ignore = (dir.Attributes & FileAttributes.System) > 0;
                    ignore |= (dir.Attributes & FileAttributes.Hidden) > 0;

                    if (!ignore)
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
                RecentFiles?.ForEach(fn => DoOne(new FileInfo(fn), true));
            }

            ///// Local common function.
            void DoOne(FileInfo finfo, bool full)
            {
                var ext = Path.GetExtension(finfo.Name).ToLower();

                if (FilterExts.Contains(ext))
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
                if ((SingleClickSelect && e.Clicks == 1) || (!SingleClickSelect && e.Clicks >= 2))
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
        #endregion
    }
}
