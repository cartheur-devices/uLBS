using System;
using System.Runtime.InteropServices;

namespace MobileShared
{
#if !LIVECONTACTS
    /// <summary>
    /// Capture hardware buttons and disable/enable entire hardware keyboard.
    /// </summary>
    public class Keyboard
    {
        /// <summary>
        /// Hardware key modifiers.
        /// </summary>
        private enum KeyModifiers
        {
            None = 0,
            Alt = 1,
            Control = 2,
            Shift = 4,
            Windows = 8,
            Modkeyup = 0x1000,
        }

        /// <summary>
        /// Enumeration of hardware keyboard button codes.
        /// </summary>
        private enum KeysHardware
        {
            //3 Action
            //37 Left
            //38 Up
            //39 Right
            //40 down
            //48 to 57 0 to 9
            //114 Talk (Green button)
            //115 End (Red Button)
            //91 Home
            //27 Back
            //121 Record
            //128 Flip
            //129 Power
            //117 Vol Up
            //119 Star (*)
            //120 Pound/Sharp (#)
            //118 Vol Down
            //112 Menu 1
            //113 Menu 2

            ///// <summary>Messaging</summary>
            //Hardware1 = 193,
            ///// <summary>Internet Explorer</summary>
            //Hardware2 = 194,
            ///// <summary>Comm Manager</summary>
            //Hardware3 = 195,
            ///// <summary>Camera</summary>
            //Hardware4 = 196,
            ///// <summary>Record(Hold)</summary>
            //Hardware5 = 197,
            ///// <summary>Record</summary>
            //Hardware6 = 198,
            ///// <summary>Home (hold)</summary>
            //Hardware7 = 202,
            ///// <summary>Left (hold)</summary>
            //Hardware8 = 203,
            ///// <summary>Right (hold)</summary>
            //Hardware9 = 204,
            ///// <summary>Enter</summary>
            //Hardware10 = 13,
            ///// <summary>Left</summary>
            //Hardware11 = 37,
            ///// <summary>Right</summary>
            //Hardware12 = 39,
            ///// <summary>Up</summary>
            //Hardware13 = 38,
            ///// <summary>Down</summary>
            //Hardware14 = 40,
            /// <summary>Call (green button)</summary>
            Answer = 114,
            ///// <summary>Hangup (red button)</summary>
            //Hangup = 115
        }

        /// <summary>
        /// The RegisterHotKey function defines a system-wide hot key.
        /// </summary>
        /// <remarks>When a key is pressed, the system looks for a match against all hot keys. Upon finding a match, the system posts the WM_HOTKEY message to the message queue of the thread that registered the hot key. This message is posted to the beginning of the queue so it is removed by the next iteration of the message loop.</remarks>
        /// <param name="hWnd">Handle to window.</param>
        /// <param name="id">Hot key identifier.</param>
        /// <param name="Modifiers">Key-modifier options.</param>
        /// <param name="key">Virtual-key code.</param>
        [DllImport("coredll.dll", SetLastError = true)]
        private static extern void RegisterHotKey(IntPtr hWnd, int id, KeyModifiers Modifiers, int key);

        /// <summary>Disable functionality of given hardware key.</summary>
        /// <param name="modifiers">Hardware key <see cref="KeyModifiers">modifier(shift, alt)</see>.</param>
        /// <param name="keyID">Hardware <see cref="KeysHardware">keycode</see></param>
        [DllImport("coredll.dll")]
        private static extern void UnregisterFunc1(KeyModifiers modifiers, int keyID);


        public static void UnRegisterKey(IntPtr hWnd)
        {
            UnregisterFunc1(KeyModifiers.None, (int)KeysHardware.Answer);
            if(!hWnd.Equals(IntPtr.Zero) )
                RegisterHotKey(hWnd, (int)KeysHardware.Answer, KeyModifiers.None , (int)KeysHardware.Answer);

        }
        /// <summary>
        /// Register all hardware keys. Disable current key functionality.
        /// </summary>
        /// <param name="hWnd">Window handle which is listening for hardware keys.</param>
        public static void RegisterRecordKey(IntPtr hWnd)
        {
            UnregisterFunc1(KeyModifiers.None, (int)KeysHardware.Answer);
            RegisterHotKey(hWnd, (int)KeysHardware.Answer, KeyModifiers.None, (int)KeysHardware.Answer);

            //UnregisterFunc1(KeyModifiers.None, (int)KeysHardware.Hangup);
            //RegisterHotKey(hWnd, (int)KeysHardware.Hangup, KeyModifiers.None, (int)KeysHardware.Hangup);

            //UnregisterFunc1(KeyModifiers.Windows, (int)KeysHardware.Hardware1);
            //RegisterHotKey(hWnd, (int)KeysHardware.Hardware1, KeyModifiers.Windows, (int)KeysHardware.Hardware1);

            //UnregisterFunc1(KeyModifiers.Windows, (int)KeysHardware.Hardware2);
            //RegisterHotKey(hWnd, (int)KeysHardware.Hardware2, KeyModifiers.Windows, (int)KeysHardware.Hardware2);

            //UnregisterFunc1(KeyModifiers.Windows, (int)KeysHardware.Hardware3);
            //RegisterHotKey(hWnd, (int)KeysHardware.Hardware3, KeyModifiers.Windows, (int)KeysHardware.Hardware3);

            //UnregisterFunc1(KeyModifiers.Windows, (int)KeysHardware.Hardware4);
            //RegisterHotKey(hWnd, (int)KeysHardware.Hardware4, KeyModifiers.Windows, (int)KeysHardware.Hardware4);

            //UnregisterFunc1(KeyModifiers.Windows, (int)KeysHardware.Hardware5);
            //RegisterHotKey(hWnd, (int)KeysHardware.Hardware5, KeyModifiers.Windows, (int)KeysHardware.Hardware5);

            //UnregisterFunc1(KeyModifiers.Windows, (int)KeysHardware.Hardware5);
            //RegisterHotKey(hWnd, (int)KeysHardware.Hardware5, KeyModifiers.Windows, (int)KeysHardware.Hardware6);
        }
    }
#endif
}