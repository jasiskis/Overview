using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using WatiN.Core;
using NUnit;
using WatiN.Core.Native.Windows;

namespace Test.Integrated
{
    public class ApplicationRunner
    {
        public Browser browser { get; private set; }

        public ApplicationRunner(){
            this.browser = new IE("http://localhost:1071/Overview.aspx");
            browser.ShowWindow(NativeMethods.WindowShowStyle.ShowMaximized);
            browser.WaitForComplete(10000);
        }

        public void Fecha(){
            browser.Close();
        }
    }
}
