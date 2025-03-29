namespace System.TreeMachine {
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Diagnostics;
    using NUnit.Framework;

    [SetUpFixture]
    public class Tests {

        [OneTimeSetUp]
        public void OneTimeSetUp() {
            Trace.Listeners.Add( new ConsoleTraceListener() );
        }

        [OneTimeTearDown]
        public void OneTimeTearDown() {
            Trace.Flush();
        }

    }
}
