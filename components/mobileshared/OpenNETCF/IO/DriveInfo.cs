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
using System.Collections;
using System.IO;

namespace OpenNETCF.IO
{
    /// <summary>
    /// Provides access to information on a drive.
    /// </summary>
    /// <remarks>This class models a drive and provides methods and properties to query for drive information.
    /// Use <see cref="DriveInfo"/> to determine what drives are available, and the capacity and available free space on the drive.</remarks>
    public class DriveInfo
    {
        private string root;

        #region Constructor
        /// <summary>
        /// Provides access to information on the specified drive.
        /// </summary>
        /// <param name="driveName"></param>
        /// <remarks>Use this class to obtain information on drives.
        /// The drive name must be a valid Windows CE volume path e.g. "\Storage Card".
        /// You cannot use this method to obtain information on drive names that are a null reference (Nothing in Visual Basic) or use UNC (\\server\share) paths.</remarks>
        /// <exception cref="ArgumentNullException">The drive name cannot be a null reference (Nothing in Visual Basic).</exception>
        public DriveInfo(string driveName)
        {
            if (string.IsNullOrEmpty(driveName))
            {
                throw new ArgumentNullException("driveName");
            }

            this.root = driveName;

            bool success = NativeMethods.GetDiskFreeSpaceEx(driveName, ref available, ref size, ref total);
        }
        #endregion

        #region Root Directory
        /// <summary>
        /// Gets the root directory of a drive.
        /// </summary>
        public DirectoryInfo RootDirectory
        {
            get
            {
                return new DirectoryInfo(root);
            }
        }
        #endregion


        #region Available Free Space
        private long available;
        /// <summary>
        /// Indicates the amount of available free space on a drive.
        /// </summary>
        public long AvailableFreeSpace
        {
            get
            {
                return available;
            }
        }
        #endregion

        #region Total Free Space
        private long total;
        /// <summary>
        /// Gets the total amount of free space available on a drive.
        /// </summary>
        public long TotalFreeSpace
        {
            get
            {
                return total;
            }
        }
        #endregion

        #region Total Size
        private long size;
        /// <summary>
        /// Gets the total size of storage space on a drive.
        /// </summary>
        public long TotalSize
        {
            get
            {
                return size;
            }
        }
        #endregion


        #region To String
        /// <summary>
        /// Returns a drive name as a string.
        /// </summary>
        /// <returns>The name of the drive.</returns>
        public override string ToString()
        {
            return root;
        }
        #endregion


        #region Get Drives
        /// <summary>
        /// Retrieves the drive names of all logical drives on a computer.
        /// </summary>
        /// <returns></returns>
        public static DriveInfo[] GetDrives()
        {
            //storage cards are directories with the temporary attribute
            System.IO.FileAttributes attrStorageCard = System.IO.FileAttributes.Directory | System.IO.FileAttributes.Temporary;

            ArrayList drives = new ArrayList();

            //add the root (Object Store)
            drives.Add(new DriveInfo("\\"));

            DirectoryInfo rootDir = new DirectoryInfo("\\");

            //add all removable drives
            foreach (DirectoryInfo di in rootDir.GetDirectories())
            {
                //if directory and temporary
                if ((di.Attributes & attrStorageCard) == attrStorageCard)
                {
                    //add to collection of storage cards
                    drives.Add(new DriveInfo("\\" + di.Name));
                }
            }
            return (DriveInfo[])drives.ToArray(typeof(DriveInfo));
        }
        #endregion

    }
}
