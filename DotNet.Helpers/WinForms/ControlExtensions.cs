#if NETFULL
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Threading.Tasks;

namespace DotNet.Helpers.WinForms
{
    public static class ControlExtensions
    {
        public static async Task ExecuteWithEnableAndDisableAsync(this Control btn, Action action)
        {
            Extensions.ExecutePreTryCatchWithMessageAndFinally(
                () => btn.Enabled = false,
                () => action(),
                () => btn.Enabled = true);
        }
    }
}
#endif