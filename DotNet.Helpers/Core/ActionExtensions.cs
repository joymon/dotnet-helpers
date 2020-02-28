using System;
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
            ValidateAndThrowExceptions(action, delaysInMilliSecondsBetweenRetries, shouldRetry);
            var exceptions = new List<Exception>();
            while (exceptions.Count <= delaysInMilliSecondsBetweenRetries.Length)
            {
                try
                {
                    action();
                    return;
                }
                catch (ExceptionType ex)
                {
                    exceptions.Add(ex);
                    if (shouldRetry(ex) && exceptions.Count <= delaysInMilliSecondsBetweenRetries.Length)
                    {
                        Thread.Sleep(delaysInMilliSecondsBetweenRetries[exceptions.Count-1]);
                        continue;
                    }
                    else throw new AggregateException(exceptions); ;
                }
            }
        }
        public static void ExecuteWithRetry<ExceptionType>(this Action action, int[] delaysInMilliSecondsBetweenRetries) where ExceptionType : Exception
        {
            action.ExecuteWithRetry<ExceptionType>( delaysInMilliSecondsBetweenRetries, (ex) => true);
        }
        public static void ExecuteWithRetry<ExceptionType>(this Action action) where ExceptionType : Exception
        {
            action.ExecuteWithRetry<ExceptionType>(new int[] { 250,500,1000}, (ex) => true);
        }
        private static void ValidateAndThrowExceptions<ExceptionType>(Action action, int[] delaysInMilliSecondsBetweenRetries, Predicate<ExceptionType> shouldRetry) where ExceptionType : Exception
        {
            if (action == null) throw new ArgumentNullException(nameof(action));
            if (delaysInMilliSecondsBetweenRetries == null) throw new ArgumentNullException(nameof(delaysInMilliSecondsBetweenRetries));
            if (delaysInMilliSecondsBetweenRetries.Length == 0) throw new ArgumentOutOfRangeException($"The number of elements in{nameof(delaysInMilliSecondsBetweenRetries)} should be 1-5");
            if (shouldRetry == null) throw new ArgumentNullException(nameof(shouldRetry));
        }
    }
}
