using System.Runtime.InteropServices;
using System.ComponentModel;

namespace OpenNETCF.WindowsCE
{
    /// <summary>
    /// This class provides access to common device management functionality
    /// </summary>
    public static class DeviceManagement
    {
        #region Platform Name
        /// <summary>
        /// Returns a string which identifies the device platform.
        /// </summary>
        /// <remarks>Valid values include:-
        /// <list type="bullet">
        /// <item><term>PocketPC</term><description>Pocket PC device or Emulator</description></item>
        /// <item><term>SmartPhone</term><description>Smartphone 2003 Device or Emulator</description></item>
        /// <item><term>CEPC platform</term><description>Windows CE.NET Emulator</description></item></list>
        /// Additional platform types will have other names.
        /// Useful when writing library code targetted at multiple platforms.</remarks>
        public static string PlatformName
        {
            get
            {
                //allocate buffer to receive value
                byte[] buffer = new byte[48];

                //call native function
                if (!NativeMethods.SystemParametersInfo(NativeMethods.SystemParametersInfoAction.GetPlatformType, buffer.Length, buffer, NativeMethods.SystemParametersInfoFlags.None))
                {
                    throw new Win32Exception(Marshal.GetLastWin32Error(), "Retrieving platform name failed");
                }

                //get string from buffer contents
                string platformname = System.Text.Encoding.Unicode.GetString(buffer, 0, buffer.Length);

                //trim any trailing null characters
                return platformname.Substring(0, platformname.IndexOf("\0"));
            }
        }
        #endregion

        #region Oem Info
        /// <summary>
        /// Returns OEM specific information from the device. This may include Model number
        /// </summary>
        public static string OemInfo
        {
            get
            {
                //allocate buffer to receive value
                byte[] buffer = new byte[128];

                //call native function
                if (!NativeMethods.SystemParametersInfo(NativeMethods.SystemParametersInfoAction.GetOemInfo, buffer.Length, buffer, NativeMethods.SystemParametersInfoFlags.None))
                {
                    throw new Win32Exception(Marshal.GetLastWin32Error(), "Retrieving OEM info failed");
                }

                //get string from buffer contents
                string oeminfo = System.Text.Encoding.Unicode.GetString(buffer, 0, buffer.Length);

                //trim any trailing null characters
                return oeminfo.Substring(0, oeminfo.IndexOf("\0"));

            }
        }
        #endregion
    }
}
