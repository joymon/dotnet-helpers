using System;
using System.Collections.Generic;
using System.Threading;

namespace DotNet.Helpers.Core
{
    public static class FuncExtensions
    {
        /// <summary>
        /// Executes function with 3 retries, if the Exception thrown of type <typeparamref name="ExceptionType"/>
        /// </summary>
        /// <typeparam name="ResultType"></typeparam>
        /// <typeparam name="ExceptionType"></typeparam>
        /// <param name="function">The Func to execute</param>
        /// <param name="delaysInMilliSecondsBetweenRetries">Array of delays in milliseconds</param>
        /// <param name="shouldRetry">The retry <see cref="Predicate{T}"/> to control retry behavior</param>
        /// <returns></returns>
        public static ResultType ExecuteWithRetry<ResultType,ExceptionType>(this Func<ResultType> function, int[] delaysInMilliSecondsBetweenRetries, Predicate<ExceptionType> shouldRetry) where ExceptionType : Exception
        {
            ValidateAndThrowExceptions(function, delaysInMilliSecondsBetweenRetries, shouldRetry);
            var exceptions = new List<Exception>();
            while (exceptions.Count <= delaysInMilliSecondsBetweenRetries.Length)
            {
                try
                {
                    ResultType result=function();
                    return result;
                }
                catch (ExceptionType ex)
                {
                    exceptions.Add(ex);
                    if (shouldRetry(ex) && exceptions.Count <= delaysInMilliSecondsBetweenRetries.Length)
                    {
                        Thread.Sleep(delaysInMilliSecondsBetweenRetries[exceptions.Count - 1]);
                        continue;
                    }
                    else throw new AggregateException(exceptions);
                }
            }
            throw new AggregateException(exceptions);
        }
        private static void ValidateAndThrowExceptions<ResultType,ExceptionType>(Func<ResultType> action, int[] delaysInMilliSecondsBetweenRetries, Predicate<ExceptionType> shouldRetry) where ExceptionType : Exception
        {
            if (action == null) throw new ArgumentNullException(nameof(action));
            if (delaysInMilliSecondsBetweenRetries == null) throw new ArgumentNullException(nameof(delaysInMilliSecondsBetweenRetries));
            if (delaysInMilliSecondsBetweenRetries.Length == 0) throw new ArgumentOutOfRangeException($"The number of elements in{nameof(delaysInMilliSecondsBetweenRetries)} should be 1-5");
            if (delaysInMilliSecondsBetweenRetries.Length > 5) throw new ArgumentOutOfRangeException($"The number of elements in{nameof(delaysInMilliSecondsBetweenRetries)} should be 1-5");
            if (shouldRetry == null) throw new ArgumentNullException(nameof(shouldRetry));
        }
    }
}
