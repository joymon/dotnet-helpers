#if NETFULL
using DotNet.Helpers.WinForms;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DotNet.Helpers.Tests.WinForms
{
    [TestClass]
    public class ISynchronizeInvokeExtensions_InvokeIfRequired
    {
        [TestMethod]
        public void WhenCalledFromOtherThread_ShouldUseBeginInvoke()
        {
            bool finished = false;
            TestForm testForm = new TestForm();
            testForm.Show();
            testForm.Finish += (sender, args) =>
            {
                testForm.InvokeIfRequired(() =>
                {
                    finished = true;
                    testForm.Close();
                });
            };
            testForm.Text = "trigger";
            while (!finished)
            {
                Application.DoEvents();
                Thread.Yield();
            }
            Assert.IsTrue(finished);
        }
    }
    public partial class TestForm : Form
    {
        public event EventHandler Finish;
        public TestForm()
        {
            this.TextChanged += (sender, args) =>
            {
                Thread runner = new Thread(() =>
                {
                    if (Finish != null)
                        Finish(this, EventArgs.Empty);
                });
                runner.Start();
            };
        }
    }
}
#endif
