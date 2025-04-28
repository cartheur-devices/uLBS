//// Copyright (c) Jason Fuller. 
////
//// Use of this source code is subject to the terms of the license found in the
//// file License.txt.  If you did not accept the terms of that license, you are
//// not authorized to use this source code.

//#region Using directives

//using System;
//using System.Collections;
//using System.Drawing;
//using System.IO;
//using System.Net;
//using System.Text;
//using System.Windows.Forms;
//using Microsoft.Win32;

//#endregion

//namespace PocketEarth
//{


//    /// <summary>
//    /// Summary description for TileCache.
//    /// </summary>
//    public class TileCache
//    {
//        /// <summary>
//        /// The cache of Tiles.
//        /// </summary>
//        private ArrayList tiles = new ArrayList();


//        /// <summary>
//        /// The file cache.
//        /// </summary>
//        private ArrayList files = new ArrayList();



//        /// <summary>
//        /// The maximum number of files to cache.  Files tend to average 10K each.
//        /// </summary>
//        public int MaxCachedFiles
//        {
//            get
//            {
//                // Each file tends to be around 10KB.
//                return 1000;
                
//            }
//        }

//        /// <summary>
//        /// The maximum number of tiles (bitmaps) to cache in memory.
//        /// </summary>
//        private int maxCachedTiles = 4;

//        /// <summary>
//        /// The directory in persistent storage where the cached files live.
//        /// </summary>
//        private string persistentStoragePath;

//        /// <summary>
//        /// The directory on the storage card where the cached files live.
//        /// </summary>
//        private string storageCardPath;

//        /// <summary>
//        /// The bitmap to show when the real bitmap cannot be gotten 
//        /// from the server.
//        /// </summary>
//        private static Bitmap badBitmap = new Bitmap(MapTiles.TileSize, MapTiles.TileSize);


//        [System.Runtime.InteropServices.DllImport("coredll.dll")]
//        private static extern bool SHGetSpecialFolderPath(IntPtr hwndOwner, StringBuilder lpszPath, int nFolder, bool fCreate);





//        public struct CachedFileInfo
//        {
//            public string Name;
//            public DateTime CreationTime;

//            public CachedFileInfo(string name, DateTime time)
//            {
//                Name = name;
//                CreationTime = time;
//            }
//        }


//        /// <summary>
//        /// Compares two FileInfos based on CreationTime.
//        /// </summary>
//        public class TimeSorter : IComparer
//        {
//            int IComparer.Compare(Object x, Object y)
//            {
//                CachedFileInfo f1 = (CachedFileInfo)x;
//                CachedFileInfo f2 = (CachedFileInfo)y;
//                if (f1.CreationTime < f2.CreationTime)
//                {
//                    return -1;
//                }
//                else if (f1.CreationTime == f2.CreationTime)
//                {
//                    return 0;
//                }
//                else
//                {
//                    return +1;
//                }
//            }
//        }



//        public TileCache()
//        {
//            // Set up file cache.

//            DirectoryInfo di = new DirectoryInfo(CachePath);
//            // Read in all the pre-existing files.
//            foreach (FileInfo fileinfo in di.GetFiles())
//            {
//                files.Add(new CachedFileInfo(fileinfo.FullName, fileinfo.CreationTime));
//            }
//            files.Sort(new TimeSorter());
//        }


//        /// <summary>
//        /// The directory where the file cache lives if persistent storage is used.
//        /// </summary>
//        private string PersistentStoragePath
//        {
//            get
//            {
//                if (persistentStoragePath == null)
//                {
//                    int MAX_PATH = 260;
//                    int CSIDL_APPDATA = 26;
//                    StringBuilder sb = new StringBuilder(MAX_PATH);
//                    SHGetSpecialFolderPath(IntPtr.Zero, sb, CSIDL_APPDATA, false);
//                    persistentStoragePath = sb.ToString() + "\\VirtualEarthMobile";
//                    if (!Directory.Exists(persistentStoragePath))
//                    {
//                        Directory.CreateDirectory(persistentStoragePath);
//                    }
//                }
//                return persistentStoragePath;
//            }
//        }

//        /// <summary>
//        /// The directory where the file cache lives if a storage card is used.
//        /// </summary>
//        private string StorageCardPath
//        {
//            get
//            {
//                if (storageCardPath == null)
//                {
//                    DirectoryInfo rootDir = new DirectoryInfo(@"\");

//                    foreach (DirectoryInfo di in rootDir.GetDirectories())
//                    {
//                        if ((di.Attributes & FileAttributes.Temporary) == FileAttributes.Temporary)
//                        {
//                            storageCardPath = "\\" + di.Name + "\\VirtualEarthMobileCache";
//                            if (!Directory.Exists(storageCardPath))
//                            {
//                                Directory.CreateDirectory(storageCardPath);
//                            }
//                            break;
//                        }
//                    }
//                }
//                return storageCardPath;
//            }
//        }

//        /// <summary>
//        /// The directory where the file cache lives.
//        /// </summary>
//        private string CachePath
//        {
//            get
//            {
//                int useStorageCard = Convert.ToInt32(Registry.CurrentUser.GetValue("Software\\Jason Fuller's\\VirtualEarthMobile\\UseStorageCard"));
//                if (useStorageCard != 0)
//                {
//                    string stgCardPath = StorageCardPath;
//                    if (Directory.Exists(stgCardPath))
//                    {
//                        return stgCardPath;
//                    }
//                }
//                return PersistentStoragePath;
//            }
//        }




//        /// <summary>
//        /// Looks up the cached file corresponding to the given url.
//        /// If found, the resulting bitmap is cached.
//        /// </summary>
//        /// <param name="url">The url of the tile bitmap</param>
//        /// <returns>The bitmap, or bull if not found.</returns>
//        private Bitmap LookupCachedFile(string url)
//        {
//            Bitmap bitmap;
//            string filename = UrlToFilename(url);

//            if (!File.Exists(filename))
//            {
//                return null;
//            }

//            FileStream stm = File.Open(filename, FileMode.Open);
//            if (stm != null)
//            {
//                bitmap = new Bitmap(stm);
//                stm.Close();
//                // Cache the image
//                this.Add(url, bitmap);
//            }
//            else
//            {
//                bitmap = null;
//            }
//            return bitmap;
//        }


//        /// <summary>
//        /// Convert a URL to a local filename where the content of the URL
//        /// was or will be stored.
//        /// </summary>
//        /// <param name="url">The url.</param>
//        /// <returns>A filename in the local filesystem.</returns>
//        private string UrlToFilename(string url)
//        {
//            // Extract the local filename from the url.
//            int start = url.LastIndexOf('/');
//            int end = url.LastIndexOf('?');
//            string filename = CachePath + "\\" + url.Substring(start + 1, end - start - 1);
//            return filename;
//        }




//        /// <summary>
//        /// Looks up the bitmap corresponding to the given url.
//        /// </summary>
//        /// <param name="url">The url of the tile bitmap.</param>
//        /// <returns>The bitmap, or null if not found.</returns>
//        public Bitmap LookupUrl(string url)
//        {
//            Bitmap bitmap = LookupCachedBitmap(url);
//            if (bitmap != null)
//            {
//                return bitmap;
//            }

//            bitmap = LookupCachedFile(url);
//            if (bitmap != null)
//            {
//                return bitmap;
//            }

//            bitmap = LookupBitmapFromServer(url);
//            if (bitmap != null)
//            {
//                return bitmap;
//            }

//            // We do this twice because fairly often the first attempt
//            // will fail, but a retry will succeed.
//            bitmap = LookupBitmapFromServer(url);
//            if (bitmap != null)
//            {
//                return bitmap;
//            }

//            // Last resort: Return an empty bitmap
//            this.Add(url, badBitmap);
//            return badBitmap;
//        }


//        /// <summary>
//        /// Get a bitmap from the Virtual Earth server.  If found, cache the 
//        /// file and the bitmap.
//        /// </summary>
//        /// <param name="url">The url to retrieve.</param>
//        /// <returns>The bitmap.</returns>
//        private Bitmap LookupBitmapFromServer(string url)
//        {
//            WebResponse response = null;
//            Stream stream = null;

//            // Get bitmap from VirtualEarth server.
//            WebRequest req = WebRequest.Create(url);
//            Cursor.Current = Cursors.WaitCursor;
//            try
//            {
//                response = req.GetResponse();
//                stream = response.GetResponseStream();
//            }
//            catch (WebException/* ex*/)
//            {
//#if false
//                MessageBox.Show("Could not get the image from the MSN VirtualEarth server.  "
//                    /*+ ex.Message*/,
//                                "PocketEarth");
//#endif
//            }
//            Cursor.Current = Cursors.Default;

//            if (stream != null)
//            {
//                // Create the local file
//                string filename = UrlToFilename(url);
//                FileStream fileStream = File.Open(filename, FileMode.CreateNew);
//                byte[] bytes = new byte[1024];
//                int bytesRead;
//                while ((bytesRead = stream.Read(bytes, 0, 1024)) != 0)
//                {
//                    fileStream.Write(bytes, 0, bytesRead);
//                }
//                fileStream.Close();
//                response.Close();

//                // Cache the local file
//                files.Add(new CachedFileInfo(filename, DateTime.Now));

//                // Delete the least recently-used file if necessary
//                while (files.Count > MaxCachedFiles)
//                {
//                    File.Delete(((CachedFileInfo)files[0]).Name);
//                    files.RemoveAt(0);
//                }
//            }

//            // Now get a bitmap from the cached file we just created.
//            return LookupCachedFile(url);
//        }



//        /// <summary>
//        /// Looks up the cached bitmap corresponding to the given url.
//        /// </summary>
//        /// <param name="url">The url of the tile bitmap.</param>
//        /// <returns>The bitmap, or null if not found.</returns>
//        public Bitmap LookupCachedBitmap(string url)
//        {
//            Tile foundTile = null;
//            Bitmap bitmap;

//            foreach (Tile t in this.tiles)
//            {
//                if (t.Url == url)
//                {
//                    foundTile = t;
//                }
//            }
//            if (foundTile != null)
//            {
//                // Since someone is looking up this tile, move it to the end
//                // of the list, which marks it as the most recently used tile.
//                tiles.Remove(foundTile);
//                tiles.Add(foundTile);
//                bitmap = foundTile.Bitmap;
//            }
//            else
//            {
//                bitmap = null;
//            }
//            return bitmap;
//        }


//        /// <summary>
//        /// Adds a tile to the cache.
//        /// </summary>
//        /// <param name="url"></param>
//        /// <param name="bitmap"></param>
//        public void Add(string url, Bitmap bitmap)
//        {
//            // Create a new tile and add it to the end of the list,
//            // which marks it as the most recently used.
//            tiles.Add(new Tile(url, bitmap));

//            // Remove older tiles if necessary.
//            if (tiles.Count > maxCachedTiles)
//            {
//                // Remove from the front of the list, where the 
//                // least recently used tile is.
//                tiles.RemoveAt(0);

//                // I know you're not supposed to call GC.Collect but if I don't,
//                // I get OutOfMemory exceptions.
//                GC.Collect();
//                GC.WaitForPendingFinalizers();
//            }
//        }




//        /// <summary>
//        /// Remove from the cache any tiles whose bitmap is bad.
//        /// (We'll try to refetch the bitmap from the server next time.)
//        /// </summary>
//        public void PurgeBadBitmaps()
//        {
//            // List of bad tiles to remove.
//            ArrayList bads = new ArrayList();

//            foreach (Tile t in tiles)
//            {
//                if (t.Bitmap == badBitmap)
//                {
//                    bads.Add(t);
//                }
//            }
//            foreach (Tile t in bads)
//            {
//                tiles.Remove(t);
//            }
//        }

//        public void PurgeAllBitmaps()
//        {
//            // Throw away the old cache and create a new one.
//            tiles = new ArrayList();
//        }
//    }
//}
