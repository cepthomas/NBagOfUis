﻿namespace Ephemera.NBagOfUis.Test
{
    partial class GraphicsForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.txtInfo = new Ephemera.NBagOfUis.TextViewer();
            this.SuspendLayout();
            // 
            // txtInfo
            // 
            this.txtInfo.Location = new System.Drawing.Point(13, 4);
            this.txtInfo.MaxText = 50000;
            this.txtInfo.Name = "txtInfo";
            this.txtInfo.Prompt = "> ";
            this.txtInfo.Size = new System.Drawing.Size(1053, 141);
            this.txtInfo.TabIndex = 0;
            this.txtInfo.WordWrap = true;
            // 
            // GraphicsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1078, 547);
            this.Controls.Add(this.txtInfo);
            this.Name = "GraphicsForm";
            this.Text = "Graphics";
            this.ResumeLayout(false);

        }

        #endregion

        private TextViewer txtInfo;
    }
}