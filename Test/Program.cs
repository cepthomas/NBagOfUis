using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Ephemera.NBagOfUis;


namespace Ephemera.NBagOfUis.Test
{
    class Program
    {
        [STAThread]
        static void Main(string[] _)
        {
            // Use test host for debugging UI components.
            TestHost w = new();
            w.ShowDialog();
        }
    }
}
