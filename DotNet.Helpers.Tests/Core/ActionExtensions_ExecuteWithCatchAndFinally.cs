using DotNet.Helpers.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace DotNet.Helpers.Tests.Core
{
    [TestClass]
    public class ActionExtensions_ExecuteWithCatchAndFinally
    {
        [TestMethod]
        public void WhenThereIsNoException_TryAndFinalBlocksShouldExecute()
        {
            int expected = 1;
            int expectedFromFinally = 2;
            int actualWithoutException = 0;
            int actualFromFnally = 0;
            Action body = () => { actualWithoutException = 1; };
            body.ExecuteWithCatchAndFinally((ex) => { }, () =>
            {
                actualFromFnally = 2;
            });
            bool result = expected == actualWithoutException && expectedFromFinally == actualFromFnally;
            Assert.AreEqual(result, true);
        }
        [TestMethod]
        public void WhenThereException_CatchAndFinalBlocksShouldExecute()
        {
            int expectedFromCatch = 1;
            int expectedFromFinally = 2;
            int actualWithException = 0;
            int actualFromFnally = 0;

            Action body = () => { throw new Exception("dummy"); };
            body.ExecuteWithCatchAndFinally((ex) =>
            {
                actualWithException = 1;
            }, () =>
            {
                actualFromFnally = 2;
            });

            bool result = expectedFromCatch == actualWithException && expectedFromFinally == actualFromFnally;
            Assert.AreEqual(result, true);
        }
        [TestMethod]
        public void WhenActionThrowsInvalidOperationException_CatchBlockShouldGetSameExeceptionType()
        {
            Action body = () => { throw new InvalidOperationException(); };
            body.ExecuteWithCatchAndFinally((ex) =>
            {
                Assert.IsInstanceOfType(ex, typeof(InvalidOperationException));
            },
                () =>
                {

                });


        }
        [TestMethod]
        public void WhenActionThrowsInvalidOperationException_CatchBlockShouldGetSameExeceptionObject()
        {
            Exception actual = new InvalidOperationException(Guid.NewGuid().ToString());
            Action body = () => { throw actual; };
            body.ExecuteWithCatchAndFinally((ex) =>
            {
                Assert.AreEqual(actual, ex);
            },
                () =>
                {

                });


        }
    }
}
