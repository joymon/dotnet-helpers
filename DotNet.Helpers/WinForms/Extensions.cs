#if NETFULL
using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Forms;
using DotNet.Helpers.Core;
namespace DotNet.Helpers.WinForms
{
    public static class Extensions
    {
        public static void ExecutePreTryCatchWithMessageAndFinally(Action preAction, Action action, Action final)
        {
            preAction();
            action.ExecuteWithCatchAndFinally((ex => MessageBox.Show($"Exception - {ex}")), final);
        }
        public static async Task ExecuteWithEnableAndDisableAsync(this Control btn, Action action)
        {
            WinForms.Extensions.ExecutePreTryCatchWithMessageAndFinally(
                () => btn.Enabled = false,
                () => action(),
                () => btn.Enabled = true);
        }
        public static void InvokeIfRequired(this ISynchronizeInvoke _target, Action action)
        {
            if (_target.InvokeRequired)
            {
                _target.BeginInvoke(new Action(() =>
                {
                    action();
                }), null);
            }
            else
            {
                action();
            }
        }
    }
}
#endif