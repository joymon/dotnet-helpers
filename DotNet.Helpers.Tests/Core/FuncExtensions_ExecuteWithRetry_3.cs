using DotNet.Helpers.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace DotNet.Helpers.Tests.Core
{
    [TestClass]
    public class FuncExtensions_ExecuteWithRetry_3
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void WhenFuncParameterIsNull_ThrowArgumentNullException()
        {
            FuncExtensions.ExecuteWithRetry<Int32, Exception>(null, new[] { 1, 2 }, ex => true);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void WhenFuncAndDelaysParameterAreNotNullButPredicateIsNull_ThrowArgumentNullException()
        {
            FuncExtensions.ExecuteWithRetry<Int32, Exception>(() => 1, new[] { 1, 2 }, null);
        }
        [TestMethod]
        public void WhenAllParametersArePresent_shouldRetryPredicateNeedsToBeCalled()
        {
            bool shouldRetryPredicateCalled = false;
            bool retried = false;
            FuncExtensions.ExecuteWithRetry<int, Exception>(() =>
             {
                 if (retried) { Console.WriteLine("test func"); return 1; }
                 else { retried = true; throw new Exception("Fake exception"); }
             },
            new int[] { 500 },
            (ex) =>
            {
                shouldRetryPredicateCalled = true;
                return true; // This decides whether to retry or not along with the number of delays.
            });
            Assert.IsTrue(shouldRetryPredicateCalled, "Not retried ");
        }
    }
}
