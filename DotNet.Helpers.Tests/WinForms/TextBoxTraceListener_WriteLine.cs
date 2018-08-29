#if NETFULL
using DotNet.Helpers.WinForms;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DotNet.Helpers.Tests.WinForms
{
    [TestClass]
    public class TextBoxTraceListener_WriteLine
    {
        [TestMethod]
        public async Task WhenCalled_TextBoxShouldGetValueWithNewLine()
        {
            string expected = "Test Message"+ Environment.NewLine;
            TextBox tb = new TextBox() { Text = "" };

            TextBoxTraceListener liste = new TextBoxTraceListener(tb);

            liste.WriteLine("Test Message");

            Assert.AreEqual(expected, tb.Text);
        }
    }
}
#endif