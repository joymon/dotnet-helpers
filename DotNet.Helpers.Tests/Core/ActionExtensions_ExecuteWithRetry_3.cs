﻿using DotNet.Helpers.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace DotNet.Helpers.Tests.Core
{
    [TestClass]
    public class ActionExtensions_ExecuteWithRetry_3
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void WhenActionParameterIsNull_ThrowArgumentNullException()
        {
            ActionExtensions.ExecuteWithRetry<Exception>(null, new[] { 1, 2 }, null);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void WhenActionParameterIsNotNullButDelaysIsNull_ThrowArgumentNullException()
        {
            ActionExtensions.ExecuteWithRetry<Exception>(() => Console.WriteLine("test action"), null, null);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void WhenActionParameterIsNotNullAndDelaysIsNoNullButPredicateIsNull_ShouldRunAction()
        {
            bool executed = false;
            ActionExtensions.ExecuteWithRetry<Exception>(() => executed = true, new int[] { 500 }, null);
            Assert.IsTrue(executed, "Action didn't execute");
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void WhenActionParameterIsNotNullAndDelaysIsNoNullButPredicateIsNullAndExceptionHappened_ThrowArgumentNullException()
        {
            ActionExtensions.ExecuteWithRetry<Exception>(() => Console.WriteLine("test action"), new int[] { 500 }, null);
        }
        [TestMethod]
        public void WhenAllParametersArePresent_shouldRetryPredicateNeedsToBeCalled()
        {
            bool shouldRetryPredicateCalled = false;
            bool retried = false;
            ActionExtensions.ExecuteWithRetry<Exception>(() =>
            {
                if (retried) Console.WriteLine("test action");
                else { retried = true; throw new Exception("Fake exception"); }
            }, new int[] { 500 }, (ex) =>
            {
                shouldRetryPredicateCalled = true;
                return true;
            });
            Assert.IsTrue(shouldRetryPredicateCalled, "Not retried ");
        }
    }
}
