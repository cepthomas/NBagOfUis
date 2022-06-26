using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
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
        /// Edit the properties in a dialog.
        /// </summary>
        /// <param name="title">To show.</param>
        /// <returns>List of tuples of name, category.</returns>
        public List<(string name, string cat)> Edit(string title)
        {
            // Make a copy for possible restoration.
            Type t = GetType();
            JsonSerializerOptions opts = new();
            string original = JsonSerializer.Serialize(this, t, opts);

            PropertyGridEx pg = new()
            {
                Dock = DockStyle.Fill,
                Location = new(14, 14),
                Size = new(350, 350),
                PropertySort = PropertySort.Categorized,
                SelectedObject = this
            };

            using Form f = new()
            {
                Text = title,
                ClientSize = new(450, 450),
                AutoScaleMode = AutoScaleMode.None,
                Location = Cursor.Position,
                StartPosition = FormStartPosition.Manual,
                FormBorderStyle = FormBorderStyle.SizableToolWindow,
                ShowIcon = false,
                ShowInTaskbar = false
            };

            // Detect changes of interest.
            List<(string name, string cat)> changes = new();
            pg.PropertyValueChanged += (sdr, args) => { changes.Add((args.ChangedItem.PropertyDescriptor.Name, args.ChangedItem.PropertyDescriptor.Category)); };

            f.Controls.Add(pg);

            f.ShowDialog();

            return changes;
        }

        /// <summary>
        /// Remove duplicate and invalid file names.
        /// </summary>
        void Cleanup()
        {
            // Clean up any bad file names.
            RecentFiles.RemoveAll(f => !File.Exists(f));
            RecentFiles = RecentFiles.Distinct().ToList();
        }
    }
}
