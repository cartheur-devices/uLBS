using Microsoft.WindowsCE.Forms;
using log4net;

namespace MobileTracker
{
    /// <summary>
    /// Used for listening for windows messages send to MobileTracker.MainForm
    /// </summary>
    public class MyMessageWindow : MessageWindow
    {
        protected static readonly ILog log = LogManager.GetLogger(typeof(MyMessageWindow));
        /// <summary>Windows message received when hardware button is pressed.</summary>
        private const int WM_HOTKEY = 0x0312;
        /// <summary>Windows message send when power event occurs.</summary>
        private const int WM_POWERBROADCAST = 0x0218;

        /// <summary>Form control.</summary>
        private MobileTracker.MainForm MainForm;

        /// <summary>Default constructor.</summary>
        /// <param name="MainForm">Form control to handle.</param>
        public MyMessageWindow(MobileTracker.MainForm MainForm)
        {
            this.MainForm = MainForm;
        }

        /// <summary>
        /// Overrides receiving windows messages.
        /// </summary>
        /// <remarks>Only handles <see cref="WM_HOTKEY">WM_HOTKEY</see></remarks>
        /// <param name="msg">Message receives</param>
        protected override void WndProc(ref Message msg)
        {
            try
            {
                // Skip handling hardware buttons
                if (msg.Msg == WM_HOTKEY)
                {
                    MainForm.Panic();
                }

                //// Handle power event (re-enable full screen mode after power button has been pressed)
                //if (msg.Msg == WM_POWERBROADCAST)
                //{
                //    //MainForm.FullScreen();
                //}

                base.WndProc(ref msg);
            }
            catch (System.Exception ex)
            {
                log.Error("Error in WndProc:", ex);              
            }
        }
        
    }
}