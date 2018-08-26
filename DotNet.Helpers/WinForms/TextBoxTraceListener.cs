#if NETFULL
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows.Forms;

namespace DotNet.Helpers.WinForms
{
    /// <summary>
    /// Trace listener class to display trace in <see cref="TextBox"/>
    /// </summary>
    public class TextBoxTraceListener : TraceListener
    {
        private TextBox _target;
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="target"><see cref="TextBox"/> control which displays the trace.</param>
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