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
        internal static void ExecutePreTryCatchWithMessageAndFinally(Action preAction, 
            Action action, 
            Action final)
        {
            preAction();
            action.ExecuteWithCatchAndFinally((ex => MessageBox.Show($"Exception - {ex}")), final);
        }
    }
}
#endif