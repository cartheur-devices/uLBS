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

namespace OpenNETCF.IO
{
	/// <summary>
	/// Use this abstract base class to create wrappers around Stream Interface
	/// Drivers that are not supported by the CF
	/// </summary>
	public abstract partial class StreamInterfaceDriver : IDisposable
	{
		/// <summary>
		/// Internal native handle to the open port
		/// </summary>
		protected IntPtr m_hPort		= IntPtr.Zero;
		string m_portName	= null;

		/// <summary>
		/// Create an instance of the StreamInterfaceDriver class
		/// </summary>
		/// <param name="portName">Name of port (prefix and index) to open</param>
		protected StreamInterfaceDriver(string portName)
		{
			m_portName = portName;
		}
        private static object m_syncRoot = new object();

        /// <summary>
        /// Create an instance of the StreamInterfaceDriver class
        /// </summary>
        protected StreamInterfaceDriver()
        {
        }

        /// <summary>
        /// Gets or Sets the Port Name
        /// </summary>
        protected string PortName
        {
            set { m_portName = value; }
            get { return m_portName; }
        }

        /// <summary>
        /// Gets the underlying native port Handle
        /// </summary>
        protected IntPtr Handle
        {
            get { return m_hPort; }
        }

        /// <summary>
        /// Returns true if the port is open, otherwise returns false
        /// </summary>
        protected bool IsOpen
        {
            get { return (m_hPort != IntPtr.Zero) && (m_hPort.ToInt32() != OpenNETCF.NativeMethods.INVALID_HANDLE_VALUE); }
        }

        /// <summary>
		/// Destructor
		/// </summary>
		~StreamInterfaceDriver()
		{
			this.Dispose();
		}

		/// <summary>
		/// Open the driver
		/// </summary>
        /// <param name="access">File Access (Read, Write or Both)</param>
		/// <param name="share">Share Mode (Read, Write or both)</param>
		[CLSCompliant(false)]
		protected void Open(System.IO.FileAccess access, System.IO.FileShare share)
		{
			m_hPort = FileHelper.CreateFile(m_portName, access, share, IO.FileCreateDisposition.OpenExisting, 0);

			if((int)m_hPort == -1)
			{
                throw new System.ComponentModel.Win32Exception(Marshal.GetLastWin32Error(), "Cannot open driver");
			}
		}

		/// <summary>
		/// Read data from the driver
		/// </summary>
		/// <param name="bytesToRead">The number of bytes requested.</param>
		/// <returns>A byte array returned by the driver</returns>
		protected byte[] Read(int bytesToRead)
		{
			byte[] buffer = new byte[bytesToRead];
			int read = 0;

            FileHelper.ReadFile(m_hPort, buffer, bytesToRead, ref read);

			return buffer;			
		}

		/// <summary>
		/// Write data to the driver
		/// </summary>
		/// <param name="data">Data to write</param>
		/// <returns>Number of bytes actually written</returns>
		protected int Write(byte[] data)
		{
			return Write(data, data.Length);
		}

		/// <summary>
		/// Write data to the driver
		/// </summary>
		/// <param name="data">Data to write</param>
		/// <param name="bytesToWrite">Number of bytes from passed in buffer to write</param>
		/// <returns>Number of bytes actually written</returns>
		protected int Write(byte[] data, int bytesToWrite)
		{
			int written = 0;

            FileHelper.WriteFile(m_hPort, data, bytesToWrite, ref written);

			return written;
		}

		/// <summary>
		/// Call a device specific IOControl
		/// </summary>
		/// <param name="controlCode">The IOCTL code</param>
		/// <param name="inData">Data to pass into the IOCTL</param>
		/// <param name="outData">Data returned by the IOCTL</param>
		[CLSCompliant(false)]
		protected void DeviceIoControl(uint controlCode, byte[] inData, byte[] outData)
		{
			int xfer = 0;

            if (outData == null)
            {
                if (inData == null)
                {
                    if (NativeMethods.DeviceIoControl(m_hPort, controlCode, IntPtr.Zero, 0, IntPtr.Zero, 0, ref xfer, IntPtr.Zero) == 0)
                    {
                        throw new System.ComponentModel.Win32Exception(Marshal.GetLastWin32Error(), "IOControl call failed");
                    }
                }
                else
                {
                    if (NativeMethods.DeviceIoControl(m_hPort, controlCode, inData, inData.Length, IntPtr.Zero, 0, ref xfer, IntPtr.Zero) == 0)
                    {
                        throw new System.ComponentModel.Win32Exception(Marshal.GetLastWin32Error(), "IOControl call failed");
                    }
                }
            }
            else if (inData == null)
            {
                if (NativeMethods.DeviceIoControl(m_hPort, controlCode, IntPtr.Zero, 0, inData, inData.Length, ref xfer, IntPtr.Zero) == 0)
                {
                    throw new System.ComponentModel.Win32Exception(Marshal.GetLastWin32Error(), "IOControl call failed");
                }
            }
            else if (NativeMethods.DeviceIoControl(m_hPort, controlCode, inData, inData.Length, outData, outData.Length, ref xfer, IntPtr.Zero) == 0)
			{
                throw new System.ComponentModel.Win32Exception(Marshal.GetLastWin32Error(), "IOControl call failed");
			}
		}

		/// <summary>
		/// This function moves the file pointer of an open file
        /// <seealso cref="System.IO.File.Move"/>
		/// </summary>
		/// <param name="distance">Bytes to move - a positive number moves forward, a negative moves backward</param>
		/// <param name="seekFrom">Starting position from where distance is measured</param>
		/// <returns>New file position</returns>
		/// <remarks>The current file position can be queried using seekFrom(0, MoveMethod.CurrentPosition)</remarks>
		protected int Seek(int distance, System.IO.SeekOrigin seekFrom)
		{
            int dist = NativeMethods.SetFilePointer(m_hPort, distance, 0, seekFrom);
			
			if(dist == 0)
			{
				throw new System.ComponentModel.Win32Exception(Marshal.GetLastWin32Error(),"Seek Failed");
			}

			return dist;
		}

		/// <summary>
		/// Close the driver
		/// </summary>
		protected void Close()
		{
			if(m_hPort != IntPtr.Zero)
			{
				NativeMethods.CloseHandle(m_hPort);
				m_hPort = IntPtr.Zero;
			}
		}

		/// <summary>
		/// Dispose
		/// </summary>
		public void Dispose()
		{
			Close();
		}
	}
}
