using System;
using System.Runtime.InteropServices;

namespace MobileShared.Controls
{
    public class NotifyIcon
    {
        // TODO Change icon bases on state, for some reason icon disappears when using different icons

        //Declare click event
        public event System.EventHandler Click;

        private WindowSink windowSink;
        private int uID = 100;
        private NOTIFYICONDATA notdata = new NOTIFYICONDATA();
        //private bool _MinimizeIconVisible = false;

        //Constructor
        public NotifyIcon()
        {
            //Create instance of the MessageWindow subclass
            windowSink = new WindowSink(this);
            windowSink.uID = uID;
        }

        //Destructor
        ~NotifyIcon()
        {
            Remove();
        }

        /// <summary>
        /// Icon color to display (red, green or blue)
        /// </summary>
        /// <param name="IcoColor"></param>
        public void Add(string ResourceFolder, string IcoColor)
        {
            //System.Drawing.Icon ico = null;
            //System.IO.Stream IconFile = null;

            //switch (IcoColor.ToLower())
            //{
            //    case "red":
            //        IconFile = System.IO.File.OpenRead(String.Concat(ResourceFolder, "redicon.ico"));
            //        ico = new System.Drawing.Icon(IconFile, 16, 16);
            //        break;
            //    case "green":
            //        IconFile = System.IO.File.OpenRead(String.Concat(ResourceFolder, "greenicon.ico"));
            //        ico = new System.Drawing.Icon(IconFile, 16, 16);
            //        break;
            //    case "blue":
            //        IconFile = System.IO.File.OpenRead(String.Concat(ResourceFolder, "blueicon.ico"));
            //        ico = new System.Drawing.Icon(IconFile, 16, 16);
            //        break;
            //    default:
            //        break;
            //}
            
            IntPtr hIcon = LoadIcon(GetModuleHandle(null), "#32512");
            //if (ico != null)
            //{
            //    if (_MinimizeIconVisible)
            //    {
            //        TrayMessage(windowSink.Hwnd, NIM_MODIFY, (uint)uID, ico.Handle);
            //    }
            //    else
            //    {
            TrayMessage(windowSink.Hwnd, NIM_ADD, (uint)uID, hIcon);
                //    _MinimizeIconVisible = true;
                //}
            //}
        }

        public void Remove()
        {
            TrayMessage(NIM_DELETE);
            //_MinimizeIconVisible = false;
        }

        public void Modify(IntPtr hIcon)
        {
            TrayMessage(windowSink.Hwnd, NIM_MODIFY, (uint)uID, hIcon);
        }

        private void TrayMessage(int dwMessage)
        {
            int ret = Shell_NotifyIcon(dwMessage, ref notdata);
        }

        private void TrayMessage(IntPtr hwnd, int dwMessage, uint uID, IntPtr hIcon)
        {
            notdata.cbSize = 152;
            notdata.hIcon = hIcon;
            notdata.hWnd = hwnd;
            notdata.uCallbackMessage = WM_NOTIFY_TRAY;
            notdata.uFlags = NIF_ICON; //NIF_MESSAGE
            notdata.uID = uID;

            int ret = Shell_NotifyIcon(dwMessage, ref notdata);
        }

        #region API Declarations
        internal const int WM_LBUTTONDOWN = 0x0201;
        //User defined message
        internal const int WM_NOTIFY_TRAY = 0x0400 + 2001;

        internal const int NIM_ADD = 0x00000000;
        internal const int NIM_MODIFY = 0x00000001;
        internal const int NIM_DELETE = 0x00000002;

        const int NIF_MESSAGE = 0x00000001;
        const int NIF_ICON = 0x00000002;

        internal struct NOTIFYICONDATA
        {
            internal int cbSize;
            internal IntPtr hWnd;
            internal uint uID;
            internal uint uFlags;
            internal uint uCallbackMessage;
            internal IntPtr hIcon;
        }

        [DllImport("coredll.dll")]
        internal static extern int Shell_NotifyIcon(int dwMessage, ref NOTIFYICONDATA pnid);
        [DllImport("coredll.dll")]
        internal static extern IntPtr LoadIcon(IntPtr hInst, string IconName);
        [DllImport("coredll.dll")]
        internal static extern IntPtr GetModuleHandle(String lpModuleName);
        #endregion

        #region WindowSink
        internal class WindowSink : Microsoft.WindowsCE.Forms.MessageWindow
        {
            //Private members
            private int m_uID = 0;
            private NotifyIcon notifyIcon;

            //Constructor
            public WindowSink(NotifyIcon notIcon)
            {
                notifyIcon = notIcon;
            }

            public int uID
            {
                set
                {
                    m_uID = value;
                }
            }

            protected override void WndProc(ref Microsoft.WindowsCE.Forms.Message msg)
            {
                if (msg.Msg == WM_NOTIFY_TRAY)
                {
                    if ((int)msg.LParam == WM_LBUTTONDOWN)
                    {
                        if ((int)msg.WParam == m_uID)
                        {
                            //If somebody hooked, raise the event
                            if (notifyIcon.Click != null)
                                notifyIcon.Click(notifyIcon, null);
                        }
                    }
                }
            }
        }
        #endregion
    }
}