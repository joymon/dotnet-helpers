#if NETFULL
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

using System.Windows.Forms;

namespace DotNet.Helpers.WinForms
{
    public class TextBoxTraceListener : TraceListener
    {
        private TextBox _target;

        public TextBoxTraceListener(TextBox target)
        {
            _target = target;
        }

        public override void Write(string message)
        {
            lock (lockObj)
            {
                _target.InvokeIfRequired(() => _target.AppendText(message));
            }
        }
        object lockObj = new object();
        public override void WriteLine(string message)
        {
            lock (lockObj)
            {
                _target.InvokeIfRequired(() => _target.AppendText(message + Environment.NewLine));
            }
        }
    }
}
#endif