﻿#if NETFULL
using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Forms;
using DotNet.Helpers.Core;
namespace DotNet.Helpers.WinForms
{
    public static class ISynchronizeInvokeExtensions
    {
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