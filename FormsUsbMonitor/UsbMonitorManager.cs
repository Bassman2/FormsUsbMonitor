using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UsbMonitor
{
    /// <summary>
    /// USB Monitor class to notify if the USB content changes
    /// </summary>
    public partial class UsbMonitorManager : IMessageFilter
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="form">Main form of the application.</param>
        /// <param name="start">Enable USB notification on startup or not.</param>
        public UsbMonitorManager(Form form, bool start = true)
        {
            this.windowHandle = form.Handle;
            Application.AddMessageFilter(this);
            if (start)
            {
                Start();
            }
        }

        public bool PreFilterMessage(ref Message m)
        {
            bool handled = false;
            if (m.HWnd == this.windowHandle)
            {
                HwndHandler(m.HWnd, m.Msg, m.WParam, m.LParam, ref handled);
            }
            return false;
        }
    }
}
