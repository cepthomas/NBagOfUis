using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using NBagOfUis;


namespace NBagOfUis.Test
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
