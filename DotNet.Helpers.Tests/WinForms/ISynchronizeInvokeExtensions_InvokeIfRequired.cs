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
        //[TestMethod]
        public void WhenCalledFromOtherThread_ShouldUseBeginInvoke()
        {
            Control ctrl = new Form();
            ctrl.Enabled = false;
            int threadIdWhereControlCreated = Thread.CurrentThread.ManagedThreadId;
            int threadIdWhereControlModified = -1;
            bool textChangesEventFired = false;
            ctrl.TextChanged += (sender, e) =>
            {
                textChangesEventFired = true;
            };
            Task task = Task.Run(() =>
              {
                  threadIdWhereControlModified = Thread.CurrentThread.ManagedThreadId;

                  if (ctrl.InvokeRequired)
                  {
                      ctrl.BeginInvoke(new Action(() =>
                      {
                          ctrl.Enabled = true;
                          ctrl.Text = "from therad";
                      }), null);
                  }
                  else
                  {
                      Assert.Fail("Reached same thread execution instead of InvokeRequired");
                  }
                  //ctrl.InvokeIfRequired(() =>
                  //{
                  //    threadIdWhereControlModified = Thread.CurrentThread.ManagedThreadId;
                  //    ctrl.Enabled = true;
                  //    ctrl.Text = "from therad";
                  //});
              });
            task.Wait();
            Assert.IsTrue(threadIdWhereControlCreated != threadIdWhereControlModified && textChangesEventFired && ctrl.Enabled);

        }
        [TestMethod]
        public void TestMethod1()
        {
            bool finished = false;
            TestForm testForm = StartFormMain();
            testForm.Finish += () =>
            {
                testForm.InvokeIfRequired(() =>
                {
                    finished = true;
                    testForm.Close();
                });
            };
            testForm.btnAction.Text = "trigger";
            while (!finished)
            {
                Application.DoEvents();
                Thread.Yield();
            }
            Assert.IsTrue(finished);
        }

        [STAThread]
        TestForm StartFormMain()
        {
            //Application.SetCompatibleTextRenderingDefault(false);
            Application.EnableVisualStyles();
            TestForm rm = new TestForm();
            //Application.Run(rm);
            rm.Show();
            return rm;
        }
    }
    public partial class TestForm : Form
    {
        internal TextBox txtResult = new TextBox();
        internal Button btnAction = new Button();
        public TestForm()
        {
            btnAction.TextChanged += BtnAction_TextChanged;
        }

        private void BtnAction_TextChanged(object sender, EventArgs e)
        {
            this.SetText();
        }


        public delegate void delFinish();
        public event delFinish Finish;

        public void SetText()
        {
            Thread runner = new Thread(() =>
            {
                Thread.Sleep(2000);

                if (this.txtResult.InvokeRequired)
                    this.txtResult.Invoke((MethodInvoker)(() =>
                    {
                        this.txtResult.Text = "Runner";

                        if (Finish != null)
                            Finish();
                    }));
                else
                {
                    this.txtResult.Text = "Runner";

                    if (Finish != null)
                        Finish();
                }

            });
            runner.Start();
        }
    }
}
#endif