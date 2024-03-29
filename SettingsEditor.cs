using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing; //Rectangle
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Windows.Forms;
using Ephemera.NBagOfTricks;


namespace Ephemera.NBagOfUis
{
    public class SettingsEditor
    {
        /// <summary>
        /// Edit the properties in a dialog. Works with SettingsCore from nbot.
        /// </summary>
        /// <param name="settings">To edit.</param>
        /// <param name="title">To show.</param>
        /// <param name="height">Adjustable.</param>
        /// <param name="expand">Default expansion.</param>
        /// <returns>List of tuples of name, category.</returns>
        public static List<(string name, string cat)> Edit(object settings, string title, int height, bool expand = false)
        {
            // Make a copy for possible restoration.
            Type t = settings.GetType();
            JsonSerializerOptions opts = new();
            string original = JsonSerializer.Serialize(settings, t, opts);

            PropertyGridEx pg = new()
            {
                Dock = DockStyle.Fill,
                PropertySort = PropertySort.Categorized,
                SelectedObject = settings
            };

            if (settings is SettingsCore)
            {
                pg.AddButton("Clear recent", null, "Clear recent file list", (_, __) => (settings as SettingsCore)!.RecentFiles.Clear());
            }

            using Form f = new()
            {
                Text = title,
                AutoScaleMode = AutoScaleMode.None,
                Location = Cursor.Position,
                StartPosition = FormStartPosition.Manual,
                FormBorderStyle = FormBorderStyle.SizableToolWindow,
                ShowIcon = false,
                ShowInTaskbar = false
            };
            f.ClientSize = new(450, height); // do after construction

            // Detect changes of interest.
            List<(string name, string cat)> changes = new();
            pg.PropertyValueChanged += (sdr, args) => { changes.Add((args.ChangedItem!.PropertyDescriptor!.Name, args.ChangedItem.PropertyDescriptor.Category)); };
            if (expand)
            {
                pg.ExpandAllGridItems();
            }

            f.Controls.Add(pg);

            f.ShowDialog();

            return changes;
        }
    }
}
