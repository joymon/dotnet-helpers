﻿using System;
using System.Collections.Generic;
using System.Threading;

namespace DotNet.Helpers.Core
{
    public static class ActionExtensions
    {
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
        /// Executes action in try, executes catchBody on <see cref="Exception"/>.
        /// </summary>
        /// <param name="action">The action body to execute</param>
        /// <param name="catchBody">The catch body to execute on any <see cref="Exception"/></param>
        public static void ExecuteWithCatch(this Action action, Action<Exception> catchBody)
        {
            action.ExecuteWithCatchAndFinally(catchBody, () => { });
        }
        public static void ExecuteWithRetry<ExceptionType>(this Action action, int[] delaysInMilliSecondsBetweenRetries, Predicate<ExceptionType> shouldRetry) where ExceptionType : Exception
        {
            ValidateAndThrowExceptions<ExceptionType>(action, delaysInMilliSecondsBetweenRetries, shouldRetry);
            FuncExtensions.ExecuteWithRetry<bool, ExceptionType>(() =>
             {
                 action();
                 return true;
             }, delaysInMilliSecondsBetweenRetries, shouldRetry);
        }
        /// <summary>
        /// Executes action with <paramref name="delaysInMilliSecondsBetweenRetries"/> retries, if the Exception thrown of type <typeparamref name="ExceptionType"/>
        /// </summary>
        /// <typeparam name="ExceptionType">Type of Exception on which retry happens</typeparam>
        /// <param name="action">The action which to be executed with retry</param>
        /// <param name="delaysInMilliSecondsBetweenRetries">Array of delays in milliseconds</param>
        public static void ExecuteWithRetry<ExceptionType>(this Action action, int[] delaysInMilliSecondsBetweenRetries) where ExceptionType : Exception
        {
            action.ExecuteWithRetry<ExceptionType>(delaysInMilliSecondsBetweenRetries, (ex) => true);
        }
        /// <summary>
        /// Executes action with 3 retries, if the Exception thrown of type <typeparamref name="ExceptionType"/>
        /// </summary>
        /// <typeparam name="ExceptionType">Type of Exception on which retry happens</typeparam>
        /// <param name="action">The action which to be executed with retry</param>
        /// <remarks>Retry interval is 250ms,500ms & 1second. Not configurable. Use <see cref="ExecuteWithRetry{ExceptionType}(Action, int[])" /> to control interval</remarks>
        /// <example>
        /// <code>
        ///     bool retried = false;
        ///     ActionExtensions.ExecuteWithRetry<Exception>(() =>
        ///         {
        ///             if (retried) Console.WriteLine("test action");
        ///             else { retried = true; throw new Exception("Fake exception");
        ///         }
        ///     });
        /// </code>
        /// </example>
        public static void ExecuteWithRetry<ExceptionType>(this Action action) where ExceptionType : Exception
        {
            action.ExecuteWithRetry<ExceptionType>(new int[] { 250, 500, 1000 }, (ex) => true);
        }
        private static void ValidateAndThrowExceptions<ExceptionType>(Action action, int[] delaysInMilliSecondsBetweenRetries, Predicate<ExceptionType> shouldRetry) where ExceptionType : Exception
        {
            if (action == null) throw new ArgumentNullException(nameof(action));
        }
    }
}
