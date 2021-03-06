﻿#if NETFULL
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
    public class ControlExtensions_ExecuteWithEnableAndDisableAsync
    {
        [TestMethod]
        public async Task WhenCalled_ControlShouldBeDisabledDuringExecution()
        {
            Control ctrl = new Control
            {
                Enabled = true
            };
            await ctrl.ExecuteWithEnableAndDisableAsync(() =>
            {
                Assert.IsFalse(ctrl.Enabled);
            });
            Assert.IsTrue(ctrl.Enabled);
        }
        [TestMethod]
        public async Task WhenCalledWithException_ControlShouldBeEnabledAfterExecution()
        {
            Control ctrl = new Control
            {
                Enabled = true
            };
            await ctrl.ExecuteWithEnableAndDisableAsync(() =>
            {
                throw new NotImplementedException();
            });
            Assert.IsTrue(ctrl.Enabled);
        }
    }
}
#endif