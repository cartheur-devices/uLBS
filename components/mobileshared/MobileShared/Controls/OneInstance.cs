using System;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Drawing;
using Microsoft.WindowsCE.Forms;

namespace MobileShared.Controls
{
    /// <summary>
    /// 
    /// </summary>
    public class OneInstance : MessageWindow
    {
        private IntPtr HWND_BROADCAST = (IntPtr)65535;
        private const int ERROR_ALREADY_EXISTS = 183;

        private int ourActivateMessage;
        private Form mainForm;
        private bool closing = false;

        // p/invokes
        //
        /// <summary>
        /// The RegisterWindowMessage function defines a new window message 
        /// that is guaranteed to be unique throughout the system. The message 
        /// value can be used when sending or posting messages.
        /// </summary>
        /// <param name="lpString"></param>
        /// <returns></returns>
        [DllImport("CoreDll.Dll")]
        private static extern int RegisterWindowMessage(string lpString);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [DllImport("CoreDll.dll", EntryPoint = "GetLastError")]
        private static extern int GetLastError();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="lpMutexAttributes"></param>
        /// <param name="InitialOwner"></param>
        /// <param name="MutexName"></param>
        /// <returns></returns>
        [DllImport("CoreDll.dll", EntryPoint = "CreateMutexW")]
        private static extern int CreateMutex(IntPtr lpMutexAttributes, bool InitialOwner, string MutexName);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private bool IsInstanceRunning()
        {
            string applicationName = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;
            if (CreateMutex(IntPtr.Zero, true, applicationName) != 0)
            {
                return (GetLastError() == ERROR_ALREADY_EXISTS);
            }
            return false;
        }

        bool blnIsrunning = false;
        public bool IsClosingRequired
        {
            get { return blnIsrunning; }
            set { blnIsrunning = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        private void ActivateInstance()
        {
            if (this.IsInstanceRunning())
            {
                this.closing = true;
                Message activateMessage = Message.Create(HWND_BROADCAST,
                    this.ourActivateMessage,
                    IntPtr.Zero,    
                    IntPtr.Zero);
                MessageWindow.PostMessage(ref activateMessage);
                blnIsrunning = true;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parent"></param>
        public OneInstance(Form mainForm)
        {
            this.mainForm = mainForm;

            string applicationName = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;
            this.ourActivateMessage = RegisterWindowMessage(applicationName);

            this.ActivateInstance();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="msg"></param>
        protected override void WndProc(ref Message msg)
        {
            if (!this.closing && msg.Msg == this.ourActivateMessage)
            {
                this.mainForm.BringToFront();
            }

            // call the base class WndProc for default message handling
            //
            base.WndProc(ref msg);
        }
    }
}
