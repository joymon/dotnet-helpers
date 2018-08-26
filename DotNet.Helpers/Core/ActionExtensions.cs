using System;
using System.Collections.Generic;
using System.Text;

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
    }
}
