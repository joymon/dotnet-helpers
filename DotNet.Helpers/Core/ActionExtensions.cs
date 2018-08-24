using System;
using System.Collections.Generic;
using System.Text;

namespace DotNet.Helpers.Core
{
    public static class ActionExtensions
    {
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
