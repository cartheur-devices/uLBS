#region OpenNETCF Copyright Information
/*
 *******************************************************************
|                                                                   |
|           OpenNETCF Smart Device Framework 2.2                    |
|                                                                   |
|                                                                   |
|       Copyright (c) 2000-2008 OpenNETCF Consulting LLC            |
|       ALL RIGHTS RESERVED                                         |
|                                                                   |
|   The entire contents of this file is protected by U.S. and       |
|   International Copyright Laws. Unauthorized reproduction,        |
|   reverse-engineering, and distribution of all or any portion of  |
|   the code contained in this file is strictly prohibited and may  |
|   result in severe civil and criminal penalties and will be       |
|   prosecuted to the maximum extent possible under the law.        |
|                                                                   |
|   RESTRICTIONS                                                    |
|                                                                   |
|   THIS SOURCE CODE AND ALL RESULTING INTERMEDIATE FILES           |
|   ARE CONFIDENTIAL AND PROPRIETARY TRADE                          |
|   SECRETS OF OPENNETCF CONSULTING LLC THE REGISTERED DEVELOPER IS |
|   LICENSED TO DISTRIBUTE THE PRODUCT AND ALL ACCOMPANYING .NET    |
|   CONTROLS AS PART OF A COMPILED EXECUTABLE PROGRAM ONLY.         |
|                                                                   |
|   THE SOURCE CODE CONTAINED WITHIN THIS FILE AND ALL RELATED      |
|   FILES OR ANY PORTION OF ITS CONTENTS SHALL AT NO TIME BE        |
|   COPIED, TRANSFERRED, SOLD, DISTRIBUTED, OR OTHERWISE MADE       |
|   AVAILABLE TO OTHER INDIVIDUALS WITHOUT EXPRESS WRITTEN CONSENT  |
|   AND PERMISSION FROM OPENNETCF CONSULTING LLC                    |
|                                                                   |
|   CONSULT THE END USER LICENSE AGREEMENT FOR INFORMATION ON       |
|   ADDITIONAL RESTRICTIONS.                                        |
|                                                                   |
 ******************************************************************* 
*/
#endregion

using System;
using System.Runtime.InteropServices;
using System.Threading;
using System.Collections.Generic;

namespace OpenNETCF.Net
{
    public partial class ConnectionManager
    {
        private static StatusNotificationPump StatusChangeWnd = null;
        #region Events 

        /// <summary>
        /// Occurs when the connection detail item list changes
        /// </summary>
        public static event EventHandler ConnectionDetailItemsChanged;
        #endregion

        #region Constructor/Destructor      
        static ConnectionManager()
        {
            if (Environment.OSVersion.Version.Build >= 5)
            {
                StatusChangeWnd = new StatusNotificationPump();
                SafeNativeMethods.ConnMgrRegisterForStatusChangeNotification(true, StatusChangeWnd.Hwnd);
            }
        }
        
        ~ConnectionManager()
        {
            if (Environment.OSVersion.Version.Build >= 5)
                SafeNativeMethods.ConnMgrRegisterForStatusChangeNotification(false, StatusChangeWnd.Hwnd);

            RequestDisconnect();
        }

        #endregion
    }
}
