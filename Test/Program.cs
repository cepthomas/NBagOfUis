using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using Ephemera.NBagOfUis;


namespace Ephemera.NBagOfUis.Test
{
    class Program
    {
        [STAThread]
        static void Main(string[] _)
        {
            // Use test host for debugging UI components.
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            var host = new TestHost();
            host.MakeIcon("C:\\Dev\\repos\\Lua\\_stuff\\marks_small.png");
            Application.Run(host);
        }
    }
}
