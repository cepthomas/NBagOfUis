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
using static System.Net.WebRequestMethods;


// TODO1 new features:
// ? ui add/remove filter, userdir
// ? ui add/remove/clear recentfile
// ? copy file name/path
// - info/hover: filters, fullpath, size, thumbnail


namespace NBagOfUis
{
    /// <summary>
    /// Tree control with file type filters.
    /// </summary>
    public partial class FilTree : UserControl
    {
        #region Properties
        /// <summary>Base path(s) for the tree.</summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        public List<string> RootDirs { get; set; } = new();

        /// <summary>Ignore these noisy directories.</summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        public List<string> IgnoreDirs { get; set; } = new();

        /// <summary>Show only these file types. Empty is valid for files without extensions.</summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        public List<string> FilterExts { get; set; } = new();

        /// <summary>Client supplied recent files.</summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        public List<string> RecentFiles { get; set; } = new();

        /// <summary>Generate event with single or double click.</summary>
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
            public string VisibleName { get; set; } = "";
            public string FullName { get; set; } = "";
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
            //treeView.DrawMode = TreeViewDrawMode.OwnerDrawText;
            //OnResize(EventArgs.Empty);
        }

        /// <summary>
        /// Populate everything from the properties.
        /// </summary>
        public void Init()
        {
            // Show what we have.
            lblActiveFilters.Text = "Filters: " + (FilterExts.Count == 0 ? "None" : string.Join(" ", FilterExts));

            lbFiles.MouseClick += (object? sender, MouseEventArgs e) => SetClickSelection(e);
            lbFiles.MouseDoubleClick += (object? sender, MouseEventArgs e) => SetClickSelection(e);

            PopulateTreeView();

            if(treeView.Nodes.Count > 0)
            {
                treeView.SelectedNode = treeView.Nodes[0];
                PopulateFiles(treeView.Nodes[0]);
            }
        }
        #endregion

        #region Tree View
        /// <summary>
        /// Drill down through dirs/files.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void TreeView_NodeMouseClick(object? sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                PopulateFiles(e.Node);
            }
        }

        /// <summary>
        /// Fill the tree.
        /// </summary>
        void PopulateTreeView()
        {
            treeView.Nodes.Clear();

            // Recent files first.
            treeView.Nodes.Add(new TreeNode("Recent"));

            foreach (string path in RootDirs)
            {
                TreeNode node;

                DirectoryInfo info = new(path);
                if (info.Exists)
                {
                    node = new TreeNode(info.Name)
                    {
                        Tag = info
                    };

                    ShowDirectories(info.GetDirectories(), node);
                    treeView.Nodes.Add(node);
                }
                else
                {
                    throw new DirectoryNotFoundException($"Invalid root directory: {path}");
                }
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
                if (!IgnoreDirs.Contains(dir.Name))
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
        #endregion

        #region File List
        /// <summary>
        /// Populate the file selector.
        /// </summary>
        /// <param name="node">Selected node.</param>
        void PopulateFiles(TreeNode node)
        {
            lbFiles.Items.Clear();

            if (node.Tag is DirectoryInfo) // TODO1 refactor this
            {
                var nodeDirInfo = node.Tag as DirectoryInfo;

                EnumerationOptions opts = new() { };
                foreach (var file in nodeDirInfo!.EnumerateFiles("*", opts).OrderBy(f => char.IsLetterOrDigit(f.Name[0])))
                {
                    var ext = Path.GetExtension(file.Name).ToLower();

                    if (FilterExts.Contains(ext))
                    {
                        var item = new ListFileInfo()
                        {
                            FullName = file.FullName,
                            VisibleName = $"{file.Name} ({file.Length / 1024})"
                        };

                        lbFiles.Items.Add(item);
                        RecentFiles.Remove(file.FullName);
                        RecentFiles.Insert(0, file.FullName);
                    }
                }
            }
            else if (node.Text == "Recent")
            {
                foreach (var fn in RecentFiles)
                {
                    var ext = Path.GetExtension(fn).ToLower();

                    if (FilterExts.Contains(ext))
                    {
                        var fi = new FileInfo(fn);
                        if (fi.Exists)
                        {
                            var item = new ListFileInfo()
                            {
                                FullName = fn,
                                VisibleName = $"{fn} ({fi.Length / 1024})"
                            };

                            lbFiles.Items.Add(item);
                        }
                        else
                        {
                            // TODO1 notify client?
                            //RecentFiles.Remove(fn);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        void SetClickSelection(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if ((SingleClickSelect && e.Clicks == 1) || (!SingleClickSelect && e.Clicks >= 2))
                {
                    ListFileInfo? fi = lbFiles.SelectedItem as ListFileInfo;
                    FileSelectedEvent?.Invoke(this, fi!.FullName);
                }
            }
        }

        /// <summary>
        /// Populate the context menu.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void MenuFiles_Opening(object? sender, CancelEventArgs e)
        {
            menuFiles.Items.Clear();
            menuFiles.Items.Add("Edit Tags", null, MenuFiles_Click);
        }

        /// <summary>
        /// Context menu handler.
        /// Select the tags for this file.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void MenuFiles_Click(object? sender, EventArgs e)
        {
            if(sender is not null)
            {
                //ToolStripMenuItem item = (ToolStripMenuItem)sender;
                //string fn = lvFiles.SelectedItems[0].Tag.ToString()!;

                switch (sender.ToString())
                {
                    case "Edit Tags":
                        break;
                }
            }
        }
        #endregion

        #region Misc privates

        #endregion
    }
}
