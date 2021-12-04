﻿using System;
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
            ///// Use pnut for automated lib tests.
            TestRunner runner = new TestRunner(OutputFormat.Readable);
            var cases = new[] { "UTILS" };
            //var cases = new[] { "PNUT", "UTILS", "CMD", "MMTEX" };
            //runner.RunSuites(cases);
            //File.WriteAllLines(@"..\..\test_out.txt", runner.Context.OutputLines);

            // Use test host for debugging UI components.
            TestHost w = new TestHost();
            w.ShowDialog();
        }
    }
}
