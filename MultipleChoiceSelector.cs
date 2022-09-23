﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NBagOfUis
{
    public partial class MultipleChoiceSelector : Form
    {
        /// <summary>What the user picked.</summary>
        public string SelectedOption { get; private set; } = "???";

        /// <summary>
        /// Coonstructor.
        /// </summary>
        public MultipleChoiceSelector()
        {
            Name = "MultipleChoiceSelector";
            Text = "Pick One";
            StartPosition = FormStartPosition.Manual;
            Location = Cursor.Position;
            FormBorderStyle = FormBorderStyle.SizableToolWindow;
            ShowIcon = false;
            ShowInTaskbar = false;
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

            foreach (var opt in options)
            {
                Button button = new()
                {
                    Text = opt,
                    Size = new(xWidth, yHeight),
                    Location = new(xSpacing, yPos),
                    DialogResult = DialogResult.OK
                };
                button.Click += (object? sender, EventArgs e) => SelectedOption = (sender! as Button)!.Text;
                Controls.Add(button);
                yPos += yHeight + ySpacing;
            }

            // Cancel.
            Button cancel = new()
            {
                Text = "Cancel",
                Size = new(xWidth, yHeight),
                Location = new(xSpacing, yPos),
                DialogResult = DialogResult.Cancel
            };
            Controls.Add(cancel);
            yPos += yHeight + ySpacing;

            ClientSize = new(xSpacing + xWidth + xSpacing, yPos + ySpacing);
        }
    }
}