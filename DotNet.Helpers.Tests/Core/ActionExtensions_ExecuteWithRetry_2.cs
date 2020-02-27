using DotNet.Helpers.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace DotNet.Helpers.Tests.Core
{
    [TestClass]
    public class ActionExtensions_ExecuteWithRetry_2
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void WhenActionParameterIsNull_ThrowArgumentNullException()
        {
            ActionExtensions.ExecuteWithRetry<Exception>(null, new[] { 1, 2 });
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void WhenActionParameterIsNotNullButDelaysIsNull_ThrowArgumentNullException()
        {
            ActionExtensions.ExecuteWithRetry<Exception>(() => Console.WriteLine("test action"), null);
        }
        [TestMethod]
        public void WhenActionParameterIsNotNullAndDelaysIsNoNullButPredicateIsNull_ShouldRunAction()
        {
            bool executed = false;
            ActionExtensions.ExecuteWithRetry<Exception>(() => executed = true, new int[] { 500 });
            Assert.IsTrue(executed, "Action didn't execute");
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void WhenActionParameterIsNotNullAndDelaysIsEmptyArray_ThrowArgumentNullException()
        {
            ActionExtensions.ExecuteWithRetry<Exception>(() => Console.WriteLine("test action"), new int[] { });
        }
        [TestMethod]
        public void WhenActionParameterIsNotNullAndDelaysIsNoNullButExceptionHappened_ShouldRetry()
        {
            bool retried = false;
            ActionExtensions.ExecuteWithRetry<Exception>(() =>
            {
                if (retried) Console.WriteLine("test action");
                else { retried = true; throw new Exception("Fake exception"); }

            }, new int[] { 500 });
            Assert.IsTrue(retried, "Not retried ");
        }
        [TestMethod]
        [ExpectedException(typeof(AggregateException))]
        public void WhenRetryFails_ShouldThrowAggregateException()
        {
            bool retried = false;
            ActionExtensions.ExecuteWithRetry<Exception>(() =>
            {
                throw new Exception("Fake exception");

            }, new int[] { 500 });
        }
    }
}
