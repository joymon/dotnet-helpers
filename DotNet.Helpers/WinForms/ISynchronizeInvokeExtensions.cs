#if NETFULL
using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Forms;
using DotNet.Helpers.Core;
namespace DotNet.Helpers.WinForms
{
    public static class ISynchronizeInvokeExtensions
    {
        /// <summary>
        /// Invoke the action using BeginInvoke, if called from different thread than the control created.
        /// </summary>
        /// <param name="target">The control which used to invoke the action</param>
        /// <param name="action">Action to perform</param>
        public static void InvokeIfRequired(this ISynchronizeInvoke target, Action action)
        {
            if (target.InvokeRequired)
            {
                target.BeginInvoke(new Action(() =>
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