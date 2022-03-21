using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Windows.Forms;
using NBagOfTricks;


namespace NBagOfUis
{
    public class Settings
    {
        #region Persisted Common Non-editable Properties
        [Browsable(false)]
        [JsonConverter(typeof(JsonRectangleConverter))]
        public Rectangle FormGeometry { get; set; } = new(50, 50, 800, 800);

        [Browsable(false)]
        public List<string> RecentFiles { get; set; } = new();
        #endregion

        #region Fields
        /// <summary>The fully  qualified file path.</summary>
        protected string _fp = "???";
        #endregion

        #region Persistence
        /// <summary>
        /// Create object from file.
        /// </summary>
        /// <param name="dir">Where the file lives.</param>
        /// <param name="t">Derived type please.</param>
        /// <param name="fn">The file name, default is settings.json.</param>
        /// <returns></returns>
        public static object Load(string dir, Type t, string fn = "settings.json")
        {
            object? set = null;

            string fp = Path.Combine(dir, fn);
            if (File.Exists(fp))
            {
                string json = File.ReadAllText(fp);
                set = JsonSerializer.Deserialize(json, t);
            }

            if(set is null)
            {
                // Doesn't exist, create a new one.
                set = Activator.CreateInstance(t);
            }

            Settings sb = (Settings)set!;
            sb._fp = fp;
            sb.Cleanup();

            return set!;
        }

        /// <summary>
        /// Save object to file.
        /// </summary>
        public void Save()
        {
            Cleanup();

            Type t = GetType();
            JsonSerializerOptions opts = new() { WriteIndented = true };
            string json = JsonSerializer.Serialize(this, t, opts);

            File.WriteAllText(_fp, json);
        }
        #endregion

        /// <summary>
        /// Remove duplicates and invalid file names.
        /// </summary>
        void Cleanup()
        {
            // Clean up any bad file names.
            RecentFiles.RemoveAll(f => !File.Exists(f));
            RecentFiles = RecentFiles.Distinct().ToList();
        }

        /// <summary>
        /// Edit the properties in a dialog.
        /// </summary>
        /// <param name="title">To show.</param>
        /// <returns>The names of modified properties.</returns>
        public List<(string name, string cat)> Edit(string title)
        {
            using Form f = new()
            {
                Text = title,
                Size = new Size(450, 450),
                //Location = new Point(200, 200),
                //StartPosition = FormStartPosition.Manual,
                StartPosition = FormStartPosition.WindowsDefaultLocation,
                FormBorderStyle = FormBorderStyle.SizableToolWindow,
                //FormBorderStyle = FormBorderStyle.FixedToolWindow,
                ShowIcon = false,
                ShowInTaskbar = false
            };

            PropertyGridEx pg = new()
            {
                Dock = DockStyle.Fill,
                PropertySort = PropertySort.Categorized,
                SelectedObject = this
            };

            // Detect changes of interest.
            List<(string name, string cat)> changes = new();

            pg.PropertyValueChanged += (sdr, args) => changes.Add((args.ChangedItem.PropertyDescriptor.Category, args.ChangedItem.PropertyDescriptor.Name));

            f.Controls.Add(pg);
            f.ShowDialog();

            Save();

            return changes;
        }
    }
}
