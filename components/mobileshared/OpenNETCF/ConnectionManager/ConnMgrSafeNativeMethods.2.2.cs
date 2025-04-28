using System;
using System.Runtime.InteropServices;

namespace OpenNETCF.Net
{
    public partial class ConnectionManager
    {
        partial class SafeNativeMethods
        {		
            #region --------- API Prototypes ---------
            [DllImport("cellcore.dll", EntryPoint = "ConnMgrRegisterForStatusChangeNotification", SetLastError = true)] //only available on WM5+
            internal static extern int ConnMgrRegisterForStatusChangeNotification(bool enable, IntPtr hWnd);         
		    #endregion
        }
    }
}
