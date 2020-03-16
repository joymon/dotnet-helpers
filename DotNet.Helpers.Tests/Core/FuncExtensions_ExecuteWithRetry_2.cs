using DotNet.Helpers.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace DotNet.Helpers.Tests.Core
{
    [TestClass]
    public class FuncExtensions_ExecuteWithRetry_2
    {
        #region input validation tests
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void WhenFuncParameterIsNull_ThrowArgumentNullException()
        {
            FuncExtensions.ExecuteWithRetry<Int32, Exception>(null, new[] { 1, 2 });
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void WhenFuncParameterIsNotNullButDelaysIsNull_ThrowArgumentNullException()
        {
            FuncExtensions.ExecuteWithRetry<Int32, Exception>(() => 1, null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void WhenFuncParameterIsNotNullAndDelaysIsEmptyArray_ThrowArgumentNullException()
        {
            FuncExtensions.ExecuteWithRetry<bool,Exception>(() => true, new int[] { });
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void WhenFuncParameterIsNotNullAndDelaysHasMoreThan5_ThrowArgumentOutOfRangeException()
        {
            FuncExtensions.ExecuteWithRetry<bool, Exception>(() => true, new int[] { 1, 2, 3, 4, 5, 6 });
        }
        #endregion

        [TestMethod]
        public void WhenFuncParameterIsNotNullAndDelaysIsNoNullButPredicateIsNull_ShouldRunFunc()
        {
            bool executed = false;
            executed = FuncExtensions.ExecuteWithRetry<bool, Exception>(() => true, new int[] { 500 });
            Assert.IsTrue(executed, "Func didn't execute");
        }
        [TestMethod]
        public void WhenFuncParameterIsNotNullAndDelaysHas5_ShouldRun()
        {
            FuncExtensions.ExecuteWithRetry<bool,Exception>(() => true, new int[] { 1, 2, 3, 4, 5 });
        }

        [TestMethod]
        public void WhenFuncParameterIsNotNullAndDelaysIsNoNullButExceptionHappened_ShouldRetry()
        {
            bool retried = false;
            FuncExtensions.ExecuteWithRetry<bool,Exception>(() =>
            {
                if (retried) return true;
                else { retried = true; throw new Exception("Fake exception"); }

            }, new int[] { 500 });
            Assert.IsTrue(retried, "Not retried ");
        }
        [TestMethod]
        [ExpectedException(typeof(AggregateException))]
        public void WhenRetryFails_ShouldThrowAggregateException()
        {
            FuncExtensions.ExecuteWithRetry<bool,Exception>(() =>
            {
                throw new Exception("Fake exception");

            }, new int[] { 500 });
        }
    }
}
