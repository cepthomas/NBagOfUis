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

        /// <summary>Show only these file types. Empty is valid for files without extensions.</summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        public List<string> FilterExts { get; set; } = new();

        /// <summary>Generate event with single or double click.</summary>
        public bool SingleClickSelect { get; set; } = false;
        #endregion

        #region Events
        /// <summary>User has selected a file.</summary>
        public event EventHandler<string>? FileSelectedEvent = null;
        #endregion

        #region Lifecycle
        /// <summary>
        /// Default constructor.
        /// </summary>
        public FilTree()
        {
            InitializeComponent();

            treeView.HideSelection = false;
            treeView.DrawMode = TreeViewDrawMode.OwnerDrawText;
        }

        /// <summary>
        /// Populate everything from the properties.
        /// </summary>
        public void Init()
        {
            // Show what we have.
            lblActiveFilters.Text = "Filters: " + (FilterExts.Count == 0 ? "None" : string.Join(" ", FilterExts));

            PopulateTreeView();
            if(treeView.Nodes.Count > 0)
            {
                treeView.SelectedNode = treeView.Nodes[0];
                PopulateFiles(treeView.Nodes[0]);
            }
            else
            {
                throw new DirectoryNotFoundException($"No root directories");
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

            foreach (string path in RootDirs)
            {
                TreeNode rootNode;

                DirectoryInfo info = new(path);
                if (info.Exists)
                {
                    rootNode = new TreeNode(info.Name)
                    {
                        Tag = info
                    };

                    ShowDirectories(info.GetDirectories(), rootNode);
                    treeView.Nodes.Add(rootNode);
                }
                else
                {
                    throw new DirectoryNotFoundException($"Invalid root directory: {path}");
                }
            }

            // Open them up a bit.
            foreach (TreeNode n in treeView.Nodes)
            {
                n.Expand();
            }
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
        #endregion

        #region File List
        /// <summary>
        /// Populate the file selector.
        /// </summary>
        /// <param name="node">Selected directory.</param>
        void PopulateFiles(TreeNode node)
        {
            TreeNode clickedNode = node;

            lvFiles.Items.Clear();
            var nodeDirInfo = clickedNode.Tag as DirectoryInfo;

            if(nodeDirInfo is not null)
            {
                EnumerationOptions opts = new() { };
                foreach (var file in nodeDirInfo.EnumerateFiles("*", opts).OrderBy(f => char.IsLetterOrDigit(f.Name[0])))
                {
                    var ext = Path.GetExtension(file.Name).ToLower();

                    if (FilterExts.Contains(ext))
                    {
                        var item = new ListViewItem(new[] { file.Name, (file.Length / 1024).ToString() })
                        {
                            Tag = file.FullName
                        };
                        lvFiles.Items.Add(item);
                    }
                }
            }
        }

        /// <summary>
        /// Single click file selection.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void ListFiles_MouseClick(object? sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && SingleClickSelect && FileSelectedEvent is not null)
            {
                FileSelectedEvent.Invoke(this, lvFiles.SelectedItems[0].Tag.ToString()!);
            }
        }

        /// <summary>
        /// Double click file selection.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void ListFiles_MouseDoubleClick(object? sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && !SingleClickSelect && FileSelectedEvent is not null)
            {
                FileSelectedEvent.Invoke(this, lvFiles.SelectedItems[0].Tag.ToString()!);
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
        /// <summary>
        /// See above.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void TreeView_DrawNode(object? sender, DrawTreeNodeEventArgs e)
        {
            e.DrawDefault = true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnResize(EventArgs e)
        {
            lvFiles.Columns[0].Width = lvFiles.Width / 2;
            lvFiles.Columns[1].Width = -2;
        }
        #endregion
    }
}
