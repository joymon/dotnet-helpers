#if NETFULL
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Threading.Tasks;

namespace DotNet.Helpers.WinForms
{
    /// <summary>
    /// Extensions for Control and its derived classes.
    /// </summary>   
    public static class ControlExtensions
    {
        /// <summary>
        /// Execute a block of code by disabling the control. Enable the control after execution even if there was an exception.
        /// </summary>
        /// <param name="control">The control which need to be disabled during execution</param>
        /// <param name="action">Action code to execute</param>
        /// <returns></returns>
        public static async Task ExecuteWithEnableAndDisableAsync(this Control control, Action action)
        {
            Extensions.ExecutePreTryCatchWithMessageAndFinally(
                () => btn.Enabled = false,
                () => action(),
                () => btn.Enabled = true);
        }
    }
}
#endif