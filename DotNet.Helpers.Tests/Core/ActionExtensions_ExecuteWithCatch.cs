using DotNet.Helpers.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace DotNet.Helpers.Tests.Core
{
    [TestClass]
    public class ActionExtensions_ExecuteWithCatch
    {
        [TestMethod]
        public void WhenThereIsNoException_TryShouldExecute()
        {
            int expected = 1;
            int actualWithoutException = 0;
            Action body = () => { actualWithoutException = 1; };
            body.ExecuteWithCatch((ex) => { });
            bool result = expected == actualWithoutException;
            Assert.AreEqual(result, true);
        }
        [TestMethod]
        public void WhenThereException_CatchShouldExecute()
        {
            int expectedFromCatch = 1;
            int actualWithException = 0;

            Action body = () => { throw new Exception("dummy"); };
            body.ExecuteWithCatch((ex) =>
            {
                actualWithException = 1;
            });

            bool result = expectedFromCatch == actualWithException;
            Assert.AreEqual(result, true);
        }
        [TestMethod]
        public void WhenActionThrowsInvalidOperationException_CatchBlockShouldGetSameExeceptionType()
        {
            Action body = () => { throw new InvalidOperationException(); };
            body.ExecuteWithCatch((ex) =>
            {
                Assert.IsInstanceOfType(ex, typeof(InvalidOperationException));
            });
        }
        [TestMethod]
        public void WhenActionThrowsInvalidOperationException_CatchBlockShouldGetSameExeceptionObject()
        {
            Exception actual = new InvalidOperationException(Guid.NewGuid().ToString());
            Action body = () => { throw actual; };
            body.ExecuteWithCatch((ex) =>
            {
                Assert.AreEqual(actual, ex);
            });
        }
        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void WhenActionThrowsExceptionAndCatchRelayIt_ThrowsExeception()
        {
            Action body = () => { throw new Exception(); };
            body.ExecuteWithCatch((ex) =>
            {
                throw ex;
            });
        }
    }
}
