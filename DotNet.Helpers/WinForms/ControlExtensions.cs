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
        /// <param name="control">The <see cref="Control"/> control which need to be disabled during execution</param>
        /// <param name="action">Action code to execute</param>
        /// <returns></returns>
        public static async Task ExecuteWithEnableAndDisableAsync(this Control control, Action action)
        {
            await Extensions.ExecutePreTryEmptyCatchAndFinallyAsync(
                () => control.Enabled = false,
                action,
                () => control.Enabled = true);
        }
        /// <summary>
        /// Execute a block of code by disabling the control. Enable the control after execution even if there was an exception.
        /// </summary>
        /// <param name="control">The <see cref="Control"/> control which need to be disabled during execution</param>
        /// <param name="action"> <see cref="Action"/> code to execute</param>
        /// <remarks>Any exception occuring inside <paramref name="action"/> will be eaten.</remarks>
        /// <returns></returns>
        public static void ExecuteWithEnableAndDisable(this Control control, Action action)
        {
            Extensions.ExecutePreTryCatchAndFinally(
                () => control.Enabled = false,
                () => action(),
                (ex) => { },
                () => control.Enabled = true);
        }
    }
}
#endif