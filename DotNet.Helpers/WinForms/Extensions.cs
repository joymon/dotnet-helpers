#if NETFULL
using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Forms;
using DotNet.Helpers.Core;
namespace DotNet.Helpers.WinForms
{
    internal static class Extensions
    {
        internal static async Task ExecutePreTryEmptyCatchAndFinallyAsync(Action preAction,
            Action action,
            Action final)
        {
            preAction();
            try
            {
                Task t = Task.Run(action);
                t.Wait();
            }
            catch
            {

            }
            finally
            {
                final();
            }
        }
        internal static void ExecutePreTryCatchAndFinally(Action preAction,
            Action action,
            Action<Exception> catchAction,
            Action final)
        {
            preAction();
            action.ExecuteWithCatchAndFinally(ex => catchAction(ex), final);
        }
    }
}
#endif