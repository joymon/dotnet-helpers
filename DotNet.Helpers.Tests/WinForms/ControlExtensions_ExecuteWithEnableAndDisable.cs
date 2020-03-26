#if NETFULL
using DotNet.Helpers.WinForms;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Windows.Forms;

namespace DotNet.Helpers.Tests.WinForms
{
    [TestClass]
    public class ControlExtensions_ExecuteWithEnableAndDisable
    {
        [TestMethod]
        public void WhenCalledWithoutException_Success()
        {
            Control ctrl = new Control
            {
                Enabled = true
            };
            ctrl.ExecuteWithEnableAndDisable(() =>
            {
                Assert.IsFalse(ctrl.Enabled);
            });
        }
        [TestMethod]
        public void WhenCalledWithException_ShouldEatExceptionAndEnableControl()
        {
            Control ctrl = new Control
            {
                Enabled = true
            };
            ctrl.ExecuteWithEnableAndDisable(() =>
            {
                throw new System.Exception();
            });
            Assert.IsTrue(ctrl.Enabled);
        }
    }
}
#endif