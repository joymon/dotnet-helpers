﻿using System;
using System.Collections.Generic;
using System.Threading;

namespace DotNet.Helpers.Core
{
    /// <summary>
    /// Extensions on <see cref="Action"/> class.
    /// </summary>
    public static class ActionExtensions
    {
        /// <summary>
        /// Executes action in try, executes catchBody on <see cref="Exception"/>.
        /// </summary>
        /// <param name="action">The action body to execute</param>
        /// <param name="catchBody">The catch body to execute on any <see cref="Exception"/></param>
        /// <example>
        /// <code>
        /// 
        /// Action body = () => { throw new InvalidOperationException(); };
        /// body.ExecuteWithCatch((ex) =>
        /// {
        ///     // Catch body
        ///     Assert.IsInstanceOfType(ex, typeof(InvalidOperationException));
        /// });
        /// 
        /// </code>
        /// </example>
        public static void ExecuteWithCatch(this Action action, Action<Exception> catchBody)
        {
            action.ExecuteWithCatchAndFinally(catchBody, () => { });
        }

        /// <summary>
        /// Executes action in try, executes catchBody on <see cref="Exception"/> and finalBody inside finally block.
        /// </summary>
        /// <param name="action">The action body to execute</param>
        /// <param name="catchBody">The catch body to execute on any <see cref="Exception"/></param>
        /// <param name="finalBody">The finally body to execute on finally {} block</param>
        /// <example>
        /// This example shows how the ExecuteWithCatchAndFinally() can be used
        /// <code>
        /// Action body = () => { throw new Exception("dummy"); };
        /// body.ExecuteWithCatchAndFinally((ex) =>
        /// {
        ///     Console.WriteLine("Exception catch handler");
        /// }, () =>
        /// {
        ///     Console.WriteLine("Finally handler");
        /// });
        ///</code>
        /// </example>
        public static void ExecuteWithCatchAndFinally(this Action action, Action<Exception> catchBody, Action finalBody)
        {
            try
            {
                action();
            }
            catch (Exception ex)
            {
                catchBody(ex);
            }
            finally
            {
                finalBody();
            }
        }
        /// <summary>
        /// Executes <see cref="Action"/> with <paramref name="delaysInMilliSecondsBetweenRetries"/> retries, if the Exception thrown of type <typeparamref name="ExceptionType"/>.
        /// </summary>
        /// <typeparam name="ExceptionType">Type of Exception on which retry happens</typeparam>
        /// <param name="action">The action which to be executed with retry</param>
        /// <param name="delaysInMilliSecondsBetweenRetries">Array of delays in milliseconds</param>
        /// <param name="shouldRetry">The retry <see cref="Predicate{T}"/> to control retry behavior</param>
        /// <example>
        /// Below code shows how the retry can be controlled using <paramref name="shouldRetry"/> <see cref="Predicate{T}"/>. 
        /// Write the logic inside the <see cref="Predicate{T}"/> and return true to continue else return false.
        /// <code>
        /// <![CDATA[
        /// bool shouldRetryPredicateCalled = false;
        /// bool retried = false;
        /// ActionExtensions.ExecuteWithRetry<Exception>(() =>
        /// {
        ///     if (retried) Console.WriteLine("test action");
        ///     else { retried = true; throw new Exception("Fake exception"); }
        /// }, new int[] { 500 }, (ex) =>
        /// {
        ///     shouldRetryPredicateCalled = true;
        ///     return true;
        /// });
        /// Assert.IsTrue(shouldRetryPredicateCalled, "Not retried ");
        /// ]]>
        /// </code>
        /// </example>
        public static void ExecuteWithRetry<ExceptionType>(this Action action, int[] delaysInMilliSecondsBetweenRetries, Predicate<ExceptionType> shouldRetry) where ExceptionType : Exception
        {
            ValidateAndThrowExceptions<ExceptionType>(action);
            FuncExtensions.ExecuteWithRetry<bool, ExceptionType>(() =>
             {
                 action();
                 return true;
             }, delaysInMilliSecondsBetweenRetries, shouldRetry);
        }
        /// <summary>
        /// Executes <see cref="Action"/> with <paramref name="delaysInMilliSecondsBetweenRetries"/> retries, if the Exception thrown of type <typeparamref name="ExceptionType"/>.
        /// </summary>
        /// <typeparam name="ExceptionType">Type of Exception on which retry happens</typeparam>
        /// <param name="action">The action which to be executed with retry</param>
        /// <param name="delaysInMilliSecondsBetweenRetries">Array of delays in milliseconds</param>
        /// <example>
        /// Below example retry one time. The <paramref name="delaysInMilliSecondsBetweenRetries"/> is expecting delays in milli seconds as the name means.
        /// <code>
        /// <![CDATA[
        /// bool retried = false;
        /// ActionExtensions.ExecuteWithRetry<Exception>(() =>
        /// {
        ///     if (retried) Console.WriteLine("Retried");
        ///     else { retried = true; throw new Exception("Fake exception"); }
        /// }, new int[] { 500,1000 });
        /// Assert.IsTrue(retried, "Not retried ");
        /// ]]>
        /// </code>
        /// </example>
        public static void ExecuteWithRetry<ExceptionType>(this Action action, int[] delaysInMilliSecondsBetweenRetries) where ExceptionType : Exception
        {
            action.ExecuteWithRetry<ExceptionType>(delaysInMilliSecondsBetweenRetries, (ex) => true);
        }
        /// <summary>
        /// Executes action with 3 retries, if the Exception thrown of type <typeparamref name="ExceptionType"/>.
        /// </summary>
        /// <typeparam name="ExceptionType">Type of Exception on which retry happens</typeparam>
        /// <param name="action">The <see cref="Action"/> which to be executed with retry</param>
        /// <example>
        /// This example shows how to invoke an <see cref="Action"/> with retry.
        /// <code>
        /// <![CDATA[
        ///     bool retried = false;
        ///     ActionExtensions.ExecuteWithRetry<Exception>(() =>
        ///         {
        ///             if (retried) Console.WriteLine("test action");
        ///             else { retried = true; throw new Exception("Fake exception");
        ///         }
        ///     });
        /// ]]>
        /// </code>
        /// </example>
        /// <remarks>
        /// Retry interval is 250ms,500ms, 1second. Not configurable. Use <see cref="ExecuteWithRetry{ExceptionType}(Action, int[])" /> to control interval
        /// </remarks>
        public static void ExecuteWithRetry<ExceptionType>(this Action action) where ExceptionType : Exception
        {
            action.ExecuteWithRetry<ExceptionType>(new int[] { 250, 500, 1000 }, (ex) => true);
        }
        private static void ValidateAndThrowExceptions<ExceptionType>(Action action) where ExceptionType : Exception
        {
            if (action == null) throw new ArgumentNullException(nameof(action));
        }
    }
}
