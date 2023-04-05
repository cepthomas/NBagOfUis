using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ephemera.NBagOfUis
{
    public partial class ChoiceSelector : UserControl
    {
        /// <summary>What the user picked.</summary>
        public string SelectedChoice { get; private set; } = "???";

        /// <summary>Value changed event.</summary>
        public event EventHandler? ChoiceChanged;

        /// <summary>
        /// Constructor.
        /// </summary>
        public ChoiceSelector()
        {
        }

        /// <summary>
        /// Make selection buttons.
        /// </summary>
        /// <param name="options"></param>
        public void SetOptions(List<string> options)
        {
            int ySpacing = 10;
            int yPos = ySpacing;
            int yHeight = 30;
            int xSpacing = 10;
            int xWidth = 130;

            //Clone options and add Cancel.
            var opts = options.ToList();
            opts.Add("Cancel");

            foreach (var opt in opts)
            {
                Button button = new()
                {
                    Text = opt,
                    Size = new(xWidth, yHeight),
                    Location = new(xSpacing, yPos),
                };
                button.Click += (object? sender, EventArgs e) =>
                {
                    SelectedChoice = (sender! as Button)!.Text;
                    ChoiceChanged?.Invoke(this, EventArgs.Empty);
                };
                Controls.Add(button);
                yPos += yHeight + ySpacing;
            }

            ClientSize = new(xSpacing + xWidth + xSpacing, yPos + ySpacing);
        }
    }
}
