using System;
using System.Threading;
using System.Windows.Forms;
using MobileShared;
using System.Runtime.InteropServices;
using System.Text;
using Microsoft.WindowsCE.Forms;

namespace MobileTracker
{
    static class Program
    {
        #region " P/Invoke DLL "

        ///Code is added to check multiple instance of the application
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        [DllImport("coredll.dll", SetLastError = true)]
        private static extern IntPtr CreateEvent(IntPtr lpEventAttributes, bool bManualReset, bool bInitialState, string lpName);
        [DllImport("coredll.dll", SetLastError = true)]
        private static extern bool CloseHandle(IntPtr hObject);
        private const int ERROR_ALREADY_EXISTS = 183;
        [DllImport("coredll.dll", SetLastError = true)]
        private static extern IntPtr FindWindow(string className, string wndName);
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        #endregion " P/Invoke DLL "

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [MTAThread]
        static void Main(string[] strArgs)
        {
            //Application.Run(new MainForm());

            try
            {
                Thread.CurrentThread.Name = "Main GUI Thread";

                ///New code for handling multiple instances
                string path = System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase;
                IntPtr handle = CreateEvent(IntPtr.Zero, false, false, path);
                int error = System.Runtime.InteropServices.Marshal.GetLastWin32Error();

                //Check if instance is already created for application
                if (handle != null && (error != ERROR_ALREADY_EXISTS))
                {
                    Application.Run(new MainForm(strArgs));
                }
                else
                {
                    // Upon startup CF implementation of mscoree looks for a window with a class #NETCF_AGL_PARK 
                    // and title set to the current process executable full path (\Program Files\MyApp\MyApp.exe). 
                    // If found, the runtime presumes that the current application is already running. In this case
                    // it will be reactivated. To do this the abovementioned window is sent a message 0x8001, 
                    // and then the new instance quits.
                    // The CF 2.0 is almost identical: This is only when you start the app multiple times before the Form has
                    // been appeared.
                    IntPtr hWnd = FindWindow("#NETCF_AGL_PARK_" + path, string.Empty);
                    if (hWnd != IntPtr.Zero)
                    {
                        Message msg = Microsoft.WindowsCE.Forms.Message.Create(hWnd, (int)0x8001, (IntPtr)0, (IntPtr)0);
                        MessageWindow.SendMessage(ref msg);
                    }
                }
                CloseHandle(handle);
            }
            catch (Exception ex)
            {
                MessageBox.Show("An unexpected error has occurred while launching Mobile Tracker. Please try again.");
            }


        }
    }
}