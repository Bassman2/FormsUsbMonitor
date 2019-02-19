using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace UsbMonitor
{
    public class UsbMonitorForm : Form, IUsbMonitorEvents, IUsbMonitorOverrides
    {
        //private const int WM_DEVICECHANGE = 0x0219;

        ////private IntPtr windowHandle;

        private DeviceChangeManager deviceChangeManager = new DeviceChangeManager();

        #region IUsbMonitorEvents
        /// <summary>
        /// Event for USB update
        /// </summary>

        public event EventHandler<UsbEventOemArgs> UsbOem;

        public event EventHandler<UsbEventVolumeArgs> UsbVolume;

        public event EventHandler<UsbEventPortArgs> UsbPort;

        public event EventHandler<UsbEventDeviceInterfaceArgs> UsbDeviceInterface;

        public event EventHandler<UsbEventHandleArgs> UsbHandle;

        public event EventHandler<UsbEventArgs> UsbChanged;

        public void CallUsbOem(object sender, UsbEventOemArgs args)
        {
            this.UsbOem?.Invoke(sender, args);
        }
        public void CallUsbVolumem(object sender, UsbEventVolumeArgs args)
        {
            this.UsbVolume?.Invoke(sender, args);
        }
        public void CallUsbPort(object sender, UsbEventPortArgs args)
        {
            this.UsbPort?.Invoke(sender, args);
        }
        public void CallUsbDeviceInterface(object sender, UsbEventDeviceInterfaceArgs args)
        {
            this.UsbDeviceInterface?.Invoke(sender, args);
        }
        public void CallUsbHandle(object sender, UsbEventHandleArgs args)
        {
            this.UsbHandle?.Invoke(sender, args);
        }
        public void CallUsbChanged(object sender, UsbEventArgs args)
        {
            this.UsbChanged?.Invoke(sender, args);
        }

        #endregion

        #region IUsbMonitorOverrides

        /// <summary>
        /// Override to handle USB interface notification.
        /// </summary>
        /// <param name="args">Update arguments</param>
        public virtual void OnUsbOem(UsbEventOemArgs args)
        { }

        public virtual void OnUsbVolume(UsbEventVolumeArgs args)
        { }

        public virtual void OnUsbPort(UsbEventPortArgs args)
        { }

        public virtual void OnUsbInterface(UsbEventDeviceInterfaceArgs args)
        { }

        public virtual void OnUsbHandle(UsbEventHandleArgs args)
        { }

        /// <summary>
        /// Override to handle USB changed notification.
        /// </summary>
        public virtual void OnUsbChanged(UsbEventArgs args)
        { }

        #endregion

        public UsbMonitorForm()
        {
            Start();
        }
        /// <summary>
        /// Enable USB notification.
        /// </summary>
        public void Start()
        {
            this.deviceChangeManager.Register(this.Handle);
        }

        /// <summary>
        /// Disable USB notification.
        /// </summary>
        public void Stop()
        {
            this.deviceChangeManager.Unregister();
        }
        
        
        [System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.Demand, Name = "FullTrust")]
        protected override void WndProc(ref Message m)
        {
            bool handled = false;
            DeviceChangeManager.HwndHandler(this, m.HWnd, m.Msg, m.WParam, m.LParam, ref handled);
            // Listen for operating system messages.
            //if (m.Msg == WM_DEVICECHANGE)
            //{
            //    UsbDeviceChangeEvent deviceChangeEvent = (UsbDeviceChangeEvent)m.WParam.ToInt32();
            //    switch (deviceChangeEvent)
            //    {

            //        case UsbDeviceChangeEvent.Arrival:
            //        case UsbDeviceChangeEvent.QueryRemove:
            //        case UsbDeviceChangeEvent.QueryRemoveFailed:
            //        case UsbDeviceChangeEvent.RemovePending:
            //        case UsbDeviceChangeEvent.RemoveComplete:
            //            UsbDeviceType deviceType = (UsbDeviceType)Marshal.ReadInt32(m.LParam, 4);
            //            switch (deviceType)
            //            {
            //                case UsbDeviceType.OEM:
            //                    var oemArgs = this.deviceChangeManager.OnDeviceOem(deviceChangeEvent, m.LParam);
            //                    // fire event
            //                    this.UsbOem?.Invoke(this, oemArgs);
            //                    // call virtual method
            //                    OnUsbOem(oemArgs);
            //                    break;
            //                case UsbDeviceType.Volume:
            //                    var volumeArgs = this.deviceChangeManager.OnDeviceVolume(deviceChangeEvent, m.LParam);
            //                    // fire event
            //                    this.UsbVolume?.Invoke(this, volumeArgs);
            //                    // call virtual method
            //                    OnUsbVolume(volumeArgs);
            //                    break;
            //                case UsbDeviceType.Port:
            //                    var portArgs = this.deviceChangeManager.OnDevicePort(deviceChangeEvent, m.LParam);
            //                    // fire event
            //                    this.UsbPort?.Invoke(this, portArgs);
            //                    // call virtual method
            //                    OnUsbPort(portArgs);
            //                    break;
            //                case UsbDeviceType.DeviceInterface:
            //                    var interfaceArgs = this.deviceChangeManager.OnDeviceInterface(deviceChangeEvent, m.LParam);
            //                    // fire event
            //                    this.UsbDeviceInterface?.Invoke(this, interfaceArgs);
            //                    // call virtual method
            //                    OnUsbInterface(interfaceArgs);
            //                    break;
            //                case UsbDeviceType.Handle:
            //                    var handleArgs = this.deviceChangeManager.OnDeviceHandle(deviceChangeEvent, m.LParam);
            //                    // fire event
            //                    this.UsbHandle?.Invoke(this, handleArgs);
            //                    // call virtual method
            //                    OnUsbHandle(handleArgs);
            //                    break;
            //                default:
            //                    break;
            //            }
            //            break;

            //        case UsbDeviceChangeEvent.Changed:
            //            var changedArgs = new UsbEventArgs(deviceChangeEvent);
            //            // fire event
            //            this.UsbChanged?.Invoke(this, changedArgs);
            //            // call virtual method
            //            OnUsbChanged(changedArgs);
            //            break;
            //    }
            //}
            base.WndProc(ref m);
        }
        

    }
}
