// Ask RIL for celltower infos  * Attention: Quick hack

using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace MobileShared
{
    public class RIL
    {
        public delegate void RILRESULTCALLBACK(int dwCode, IntPtr hrCmdID, IntPtr lpData, int cbData, int dwParam);
        public delegate void RILNOTIFYCALLBACK(int dwCode, IntPtr lpData, int cbData, int dwParam);

        [StructLayout(LayoutKind.Explicit)]
        public class RILCELLTOWERINFO
        {
            [FieldOffset(0)]
            uint dwLen;
            [FieldOffset(4)]
            uint dwParams;
            [FieldOffset(8)]
            public uint dwMobileCountryCode;
            [FieldOffset(12)]
            public uint dwMobileNetworkCode;
            [FieldOffset(16)]
            public uint dwLocationAreaCode;
            [FieldOffset(20)]
            public uint dwCellID;
            [FieldOffset(24)]
            uint dwBaseStationID;
            [FieldOffset(28)]
            uint dwBroadcastControlChannel;
            [FieldOffset(32)]
            public uint dwRxLevel;
            [FieldOffset(36)]
            uint dwRxLevelFull;
            [FieldOffset(40)]
            uint dwRxLevelSub;
            [FieldOffset(44)]
            uint dwRxQuality;
            [FieldOffset(48)]
            uint dwRxQualityFull;
            [FieldOffset(52)]
            uint dwRxQualitySub;
            /* More minor interesting fields below */

        }

        private static bool done = false;
        private static RILCELLTOWERINFO result;

        #region DLLImport
        [DllImport("ril.dll")]
        private static extern IntPtr RIL_Initialize(int dwIndex, RILRESULTCALLBACK pfnResult, RILNOTIFYCALLBACK pfnNotify, int dwNotificationClasses, int dwParam, out IntPtr lphRil);
        [DllImport("ril.dll", EntryPoint = "RIL_DevSpecific")]
        private static extern IntPtr RIL_DevSpecific(IntPtr hRil, IntPtr dwParam, IntPtr size);
        [DllImport("ril.dll", EntryPoint = "RIL_GetCellTowerInfo")]
        private static extern IntPtr RIL_GetCellTowerInfo(IntPtr hRil);
        [DllImport("ril.dll")]
        private static extern IntPtr RIL_Deinitialize(IntPtr hRil);
        #endregion

        public static void f_notify(int dwCode, IntPtr lpData, int cbData, int dwParam)
        {
        }

        public static void f_result(int dwCode, IntPtr hrCmdID, IntPtr lpData, int cbData, int dwParam)
        {
            result = new RILCELLTOWERINFO();
            Marshal.PtrToStructure(lpData, result);

            done = true;
        }

        public static RILCELLTOWERINFO GetCellTowerInfo()
        {
            IntPtr hRil, res;

            RILRESULTCALLBACK thisresult = new RILRESULTCALLBACK(f_result);
            RILNOTIFYCALLBACK notify = new RILNOTIFYCALLBACK(f_notify);

            res = RIL_Initialize(1, thisresult, notify, 0, 0, out hRil);
            if (res != IntPtr.Zero)
                return null; //("Could not initialize Ril")
            RIL.done = false;
            RIL.result = null;
            res = RIL_GetCellTowerInfo(hRil);
            int i = 10;
            while (i-- > 0 && !RIL.done)
            {
                System.Threading.Thread.Sleep(1000);
            }

            RIL_Deinitialize(hRil);

            // Filter invalid CellID
            if (result.dwLocationAreaCode > 65535)
                return null;

            return result;
        }
    }
}
