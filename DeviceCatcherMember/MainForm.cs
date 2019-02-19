using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UsbMonitor;

namespace DeviceCatcherMember
{
    public partial class MainForm : UsbMonitorForm
    {
        private UsbMonitorManager usbMonitor;

        public MainForm()
        {
            InitializeComponent();

            this.usbMonitor = new UsbMonitorManager(this);
            this.usbMonitor.UsbOem += OnUsb;
            this.usbMonitor.UsbVolume += OnUsb;
            this.usbMonitor.UsbPort += OnUsb;
            this.usbMonitor.UsbDeviceInterface += OnUsb;
            this.usbMonitor.UsbHandle += OnUsb;
            this.usbMonitor.UsbChanged += OnUsb;
        }

        private void OnUsb(object sender, UsbEventArgs e)
        {
            this.textBox.Text += e.ToString() + "\r\n";
        }
    }
}
