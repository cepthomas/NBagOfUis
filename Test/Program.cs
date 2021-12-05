using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using NBagOfUis;


namespace NBagOfUis.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            // Use test host for debugging UI components.
            TestHost w = new TestHost();
            w.ShowDialog();
        }
    }
}
