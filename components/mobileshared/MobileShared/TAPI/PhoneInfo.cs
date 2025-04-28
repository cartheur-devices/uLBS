using System;
using System.Data;
using System.Runtime.InteropServices;
using System.Text;
using Microsoft.WindowsMobile.Status;

namespace MobileTracker.Codebase.TAPI
{
    public static class PhoneInfo
    {
        [Flags]
        public enum CellSystemConnectedFlags : uint
        {
            SN_CELLSYSTEMCONNECTED_GPRS_BITMASK = 1,
            SN_CELLSYSTEMCONNECTED_1XRTT_BITMASK = 2,
            SN_CELLSYSTEMCONNECTED_1XEVDO_BITMASK = 4,
            SN_CELLSYSTEMCONNECTED_EDGE_BITMASK = 8,
            SN_CELLSYSTEMCONNECTED_UMTS_BITMASK = 16,
            SN_CELLSYSTEMCONNECTED_EVDV_BITMASK = 32,
            SN_CELLSYSTEMCONNECTED_HSDPA_BITMASK = 64,
            SN_CELLSYSTEMCONNECTED_CSD_BITMASK = 2147483648
        }

        public static string GetIMSI()
        {
            string subsciberId;
            IntPtr hLine;
            int dwNumDev;
            int num1 = 0x20000;
            LINEINITIALIZEEXPARAMS lineInitializeParams = new LINEINITIALIZEEXPARAMS();
            lineInitializeParams.dwTotalSize = (uint)Marshal.SizeOf(lineInitializeParams);
            lineInitializeParams.dwNeededSize = lineInitializeParams.dwTotalSize;
            lineInitializeParams.dwOptions = 2;
            lineInitializeParams.hEvent = IntPtr.Zero;
            lineInitializeParams.hCompletionPort = IntPtr.Zero;

            #region lineInitializeEx
            int result = Tapi.lineInitializeEx(out hLine, IntPtr.Zero,
            IntPtr.Zero, null, out dwNumDev, ref num1, ref lineInitializeParams);
            if (result != 0)
            {
                throw new ApplicationException(string.Format("lineInitializeEx failed!\n\nError Code:{0}", result.ToString()));
            }
            #endregion

            #region lineNegotiateAPIVerison
            int version;
            int dwAPIVersionLow = 0x10004;
            int dwAPIVersionHigh = 0x20000;
            LINEEXTENSIONID lineExtensionID;
            result = Tapi.lineNegotiateAPIVersion(hLine, 0, dwAPIVersionLow, dwAPIVersionHigh, out version, out lineExtensionID);
            if (result != 0)
            {
                throw new ApplicationException(string.Format("lineNegotiateAPIVersion failed!\n\nError Code: {0}", result.ToString()));
            }
            #endregion

            #region lineOpen
            IntPtr hLine2 = IntPtr.Zero;
            result = Tapi.lineOpen(hLine, 0, out hLine2, version, 0, IntPtr.Zero, 0x00000002, 0x00000004, IntPtr.Zero);
            if (result != 0)
            {
                throw new ApplicationException(string.Format("lineNegotiateAPIVersion failed!\n\nError Code: {0}", result.ToString()));
            }
            #endregion

            #region lineGetGeneralInfo
            int structSize = Marshal.SizeOf(new LINEGENERALINFO());
            byte[] bytes = new byte[structSize];
            byte[] tmpBytes = BitConverter.GetBytes(structSize);

            for (int index = 0; index < tmpBytes.Length; index++)
            {
                bytes[index] = tmpBytes[index];
            }
            #endregion

            #region make initial query to retrieve necessary size
            result = Tapi.lineGetGeneralInfo(hLine2, bytes);
            if (result != 0)
            {
                throw new ApplicationException(string.Format("lineGetGeneralInfo failed!\n\nError Code: {0}", result.ToString()));
            }

            // get the needed size
            int neededSize = BitConverter.ToInt32(bytes, 4);

            // resize the array
            bytes = new byte[neededSize];

            // write out the new allocated size to the byte stream
            tmpBytes = BitConverter.GetBytes(neededSize);
            for (int index = 0; index < tmpBytes.Length; index++)
            {
                bytes[index] = tmpBytes[index];
            }

            // fetch the information with properly size buffer
            result = Tapi.lineGetGeneralInfo(hLine2, bytes);

            if (result != 0)
            {
                throw new ApplicationException(Marshal.GetLastWin32Error().ToString());
            }
            #endregion

            #region actual data fetching
            int size;
            int offset;

            // subscriber id
            size = BitConverter.ToInt32(bytes, 44);
            offset = BitConverter.ToInt32(bytes, 48);
            subsciberId = Encoding.Unicode.GetString(bytes, offset, size);
            subsciberId = subsciberId.Substring(0, subsciberId.IndexOf('\0'));
            #endregion

            //tear down
            Tapi.lineClose(hLine2);
            Tapi.lineShutdown(hLine);

            return subsciberId;
        }

        public static int GetCellularConnectionType()
        {
            int type = 0; // Prior to WM6 cannot get the below details so just say "cellular"

            if (Environment.OSVersion.Version.Major > 5)
            {
                RegistryState cellularSystem =
                  new RegistryState(@"HKEY_LOCAL_MACHINE\System\State\Phone", "Cellular System Connected");
                uint cellSystemConnected = (uint)cellularSystem.CurrentValue;

                if ((cellSystemConnected & (uint)CellSystemConnectedFlags.SN_CELLSYSTEMCONNECTED_GPRS_BITMASK) != 0)
                    type = 2;
                else if ((cellSystemConnected & (uint)CellSystemConnectedFlags.SN_CELLSYSTEMCONNECTED_EDGE_BITMASK) != 0)
                    type = 9;
                else if ((cellSystemConnected & (uint)CellSystemConnectedFlags.SN_CELLSYSTEMCONNECTED_UMTS_BITMASK) != 0)
                    type = 6;
                else if ((cellSystemConnected & (uint)CellSystemConnectedFlags.SN_CELLSYSTEMCONNECTED_HSDPA_BITMASK) != 0)
                    type = 8;
            }
            return type;
        }
    }
}
