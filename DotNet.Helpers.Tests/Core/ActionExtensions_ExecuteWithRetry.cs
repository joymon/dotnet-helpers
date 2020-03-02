using DotNet.Helpers.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace DotNet.Helpers.Tests.Core
{

    [TestClass]
    public class ActionExtensions_ExecuteWithRetry
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void WhenActionParameterIsNull_ThrowArgumentNullException()
        {
            ActionExtensions.ExecuteWithRetry<Exception>(null);
        }
        [TestMethod]
        [ExpectedException(typeof(AggregateException))]
        public void WhenActionParamIsNotNullAndExceptionThrown_AggregateException()
        {
            ActionExtensions.ExecuteWithRetry<Exception>(() => throw new Exception("test exception"));

        }
        [TestMethod]
        public void WhenActionParameterIsNotNullButExceptionHappened_ShouldRetry()
        {
            bool retried = false;
            ActionExtensions.ExecuteWithRetry<Exception>(() =>
            {
                if (retried) Console.WriteLine("test action");
                else { retried = true; throw new Exception("Fake exception"); }
            });
            Assert.IsTrue(retried, "Not retried ");
        }
        [TestMethod]
        public void WhenActionParameterIsNotNullButExceptionHappenedFirst3Times_ShouldSuccessLastAttempt()
        {
            int retryCount = 0;
            ActionExtensions.ExecuteWithRetry<Exception>(() =>
            {
                if (retryCount == 3) Console.WriteLine("test action");
                else { retryCount++; throw new Exception("Fake exception"); }
            });
            Assert.AreEqual(3,retryCount, "Not retried ");
        }

        [TestMethod]
        public void WhenActionParamIsNotNullAndExceptionThrownEverytime_AggregateExceptionWith3Children()
        {
            int countOfChildExceptions = 0;
            try
            {
                ActionExtensions.ExecuteWithRetry<Exception>(() => throw new Exception("test exception"));
            }
            catch(AggregateException aex)
            {
                countOfChildExceptions = aex.InnerExceptions.Count;
            }
            Assert.AreEqual(4, countOfChildExceptions, "AggregateException don't have enough child exceptions");
        }
    }
}
