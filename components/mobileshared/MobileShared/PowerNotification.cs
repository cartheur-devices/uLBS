using System;
using System.Collections;
using System.ComponentModel;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;

namespace MobileShared
{
    public class PowerNotifications
    {
        IntPtr ptr = IntPtr.Zero;
        Thread t = null;
        bool done = false;

        [DllImport("coredll.dll")]
        private static extern IntPtr RequestPowerNotifications(IntPtr hMsgQ, int Flags);

        [DllImport("coredll.dll")]
        private static extern uint WaitForSingleObject(IntPtr hHandle, int wait);

        [DllImport("coredll.dll")]
        private static extern IntPtr CreateMsgQueue(string name, ref MsgQOptions options);

        [DllImport("coredll.dll")]
        private static extern bool ReadMsgQueue(IntPtr hMsgQ, byte[] lpBuffer, uint cbBufSize, ref uint lpNumRead, int dwTimeout, ref uint pdwFlags);

        public PowerNotifications()
        {
            MsgQOptions options = new MsgQOptions();
            options.dwFlags = 0;
            options.dwMaxMessages = 20;
            options.cbMaxMessage = 10000;
            options.bReadAccess = true;
            options.dwSize = (uint)System.Runtime.InteropServices.Marshal.SizeOf(options);
            ptr = CreateMsgQueue("PowerNotification", ref options);
            RequestPowerNotifications(ptr, (int)PowerEventType.PBT_TRANSITION);
            t = new Thread(new ThreadStart(DoWork));
            t.Name = "PowerNotification Thread";
        }

        [Flags]
        private enum PowerEventType
        {
            PBT_TRANSITION = 0x00000001,
            PBT_RESUME = 0x00000002,
            PBT_POWERSTATUSCHANGE = 0x00000004,
            PBT_POWERINFOCHANGE = 0x00000008,
            PBT_OEMBASE = 0x00010000,
            POWER_NOTIFY_ALL = -1
        }

        public void Start()
        {
            t.Start();
        }

        public void Stop()
        {
            done = true;
            t.Abort();
        }

        private void DoWork()
        {
            Byte[] buf = new Byte[1024];

            uint nRead = 0, flags = 0, res = 0;

            try
            {
                while (!done)
                {
                    res = WaitForSingleObject(ptr, 1000);
                    if (res == 0)
                    {
                        ReadMsgQueue(ptr, buf, (uint)buf.Length, ref nRead, -1, ref flags);
                        string msg = null;

                        System.Text.UnicodeEncoding enc = new System.Text.UnicodeEncoding();
                        msg = enc.GetString(buf, 12, (int)nRead - 13);

                        switch (msg)
                        {
                            case "on":
                            case "backlightoff":
                            case "suspend":
                            case "unattended":
                            case "resuming":
                                

                                RaisePowerNotifyEvent(new PowerNotifyEventArgs(msg));
                                break;
                            case "0":
                                // non power transition messages are ignored
                                break;
                            default:
                                msg = "Unknown Flag: " + msg;
                                break;
                        }
                        if (msg != null)
                            Console.WriteLine(msg);
                    }
                }
            }
            catch (Exception ex)
            {
                if (!done)
                {}
            }
        }

        uint ConvertByteArray(byte[] array, int offset)
        {
            uint res = 0;
            res += array[offset];
            res += array[offset + 1] * (uint)0x100;
            res += array[offset + 2] * (uint)0x10000;
            res += array[offset + 3] * (uint)0x1000000;
            return res;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct MsgQOptions
        {
            public uint dwSize;
            public uint dwFlags;
            public uint dwMaxMessages;
            public uint cbMaxMessage;
            public bool bReadAccess;
        }

        public event PowerNotifyEventHandler OnPowerNotifyEvent;
        public delegate void PowerNotifyEventHandler(object sender, PowerNotifyEventArgs e);
        public void RaisePowerNotifyEvent(PowerNotifyEventArgs e)
        {
            // Event will be null if there are no subscribers
            if (OnPowerNotifyEvent != null)
            {
                OnPowerNotifyEvent(this, e);
            }
        }
    }

    public class PowerNotifyEventArgs : EventArgs
    {
        public PowerNotifyEventArgs(string n)
        {
            powerstate = n;
        }

        private readonly string powerstate;
        public string DevicePowerState
        {
            get { return powerstate; }
        }        
    }

    public enum PowerState
    {
        POWER_STATE_ON = 0x00010000,        // on state
        POWER_STATE_OFF = 0x00020000,       // no power, full off
        POWER_STATE_SUSPEND = 0x00200000,   // suspend state
        POWER_FORCE = 4096,
        POWER_STATE_RESET = 0x00800000,     // reset state
        POWER_STATE_IDLE = 0x00100000,      // idle state
        POWER_STATE_BACKLIGHTOFF = 0x02000000,
        POWER_STATE_UNATTENDED = 0x00400000,// Unattended state.
        POWER_STATE_USERIDLE = 0x01000000   // user idle state
    }
}
