using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using Ephemera.NBagOfTricks;
using Ephemera.NBagOfUis;


namespace Ephemera.NBagOfUis.Test
{
    class Program
    {
        [STAThread]
        static void Main(string[] _)
        {
            // Ensure paths.
            var outputDir = Path.Join(MiscUtils.GetSourcePath(), "out");
            Directory.CreateDirectory(outputDir);

            // Use test host for debugging UI components.
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            var host = new TestHost();
            Application.Run(host);
        }
    }
}
