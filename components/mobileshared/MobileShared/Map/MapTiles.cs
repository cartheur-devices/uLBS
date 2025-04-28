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
//#endregion

//namespace PocketEarth
//{
//    /// <summary>
//    /// Summary description for MapTiles.
//    /// </summary>
//    public class MapTiles
//    {
//        /// <summary>
//        /// MSN VirtualEarth version
//        /// </summary>
//        private const int tileVersion = 1;

//        /// <summary>
//        /// The width or height of one tile.
//        /// </summary>
//        public const int TileSize = 256;


//        private static TileCache tileCache = new TileCache();

//        private MapTiles()
//        {
//        }

//        /// <summary>
//        /// Remove any bitmaps from the cache that are not good.
//        /// Good is defined as having been retrieved from the server.
//        /// </summary>
//        public static void PurgeBadBitmaps()
//        {
//            tileCache.PurgeBadBitmaps();
//        }

//        public static void PurgeAllBitmaps()
//        {
//            tileCache.PurgeAllBitmaps();
//        }


//        /// <summary>
//        /// Gets a bitmap (possibly cached) for a map tile from 
//        /// the VirtualEarth server.
//        /// </summary>
//        /// <param name="tx">tile X</param>
//        /// <param name="ty">tile Y</param>
//        /// <param name="zoomLevel">zoom level</param>
//        /// <param name="mapStyle">map style</param>
//        public static Bitmap GetMapTile(
//            int tx,
//            int ty,
//            int zoomLevel,
//            MapStyle mapStyle)
//        {
//            Bitmap tileBitmap = null;

//            // Parameter checking
//            int max = 1 << zoomLevel;
//            if (tx < 0 || ty < 0 || tx >= max || ty >= max)
//            {
//                return null;
//            }

//            string url = BuildUrl(tx, ty, zoomLevel, mapStyle);

//            // Look in the cache.
//            tileBitmap = tileCache.LookupUrl(url);
//            if (tileBitmap != null)
//            {
//                return tileBitmap;
//            }

//            return tileBitmap;
//        }


//        /// <summary>
//        /// Convert a tile's xy coordinates to a quad key.
//        /// A quad key is a string of base 4 digits.  The leftmost digit is
//        /// which quadrant the tile is in at zoom level 1.  A quadrant has 4 
//        /// cells laid out like this:
//        /// 0 1
//        /// 2 3
//        /// The second digit is which quadrant within the first quadrant the 
//        /// tile is.  And so on.
//        /// The longer the string of digits, the more zoomed in the tile is.
//        /// </summary>
//        /// <param name="zoomLevel">zoom level</param>
//        /// <param name="ty">tile Y</param>
//        /// <param name="ty">tile Y</param>
//        /// <returns>The quad key</returns>
//        private static string TileToQuadKey(
//            int tx,
//            int ty,
//            int zl)
//        {
//            string quad = "";
//            for (int i = zl; i > 0; i--)
//            {
//                int mask = 1 << (i - 1);
//                int cell = 0;
//                if ((tx & mask) != 0)
//                {
//                    cell++;
//                }
//                if ((ty & mask) != 0)
//                {
//                    cell += 2;
//                }
//                quad += cell;
//            }
//            return quad;
//        }


//        /// <summary>
//        /// Builds the URL for the image of the tile at (tx,ty)
//        /// </summary>
//        /// <param name="tx">tile X</param>
//        /// <param name="ty">tileY</param>
//        /// <param name="zoomLevel">zoom level</param>
//        /// <param name="ms">map style</param>
//        /// <returns>The URL</returns>
//        private static string BuildUrl(
//            int tx,
//            int ty,
//            int zoomLevel,
//            MapStyle ms)
//        {
//            string[] tileUrlPrefixes = new string[4];
//            // Simple load balancing
//            tileUrlPrefixes[0] = "http://r0.ortho.tiles.virtualearth.net/tiles/";
//            tileUrlPrefixes[1] = "http://r1.ortho.tiles.virtualearth.net/tiles/";
//            tileUrlPrefixes[2] = "http://r2.ortho.tiles.virtualearth.net/tiles/";
//            tileUrlPrefixes[3] = "http://r3.ortho.tiles.virtualearth.net/tiles/";
//            int server = ((tx & 1) + ((ty & 1) << 1)) % tileUrlPrefixes.Length;

//            return tileUrlPrefixes[server]
//                    + (ms == MapStyle.Road ? 'r' :
//                       ms == MapStyle.Aerial ? 'a' : 'h')
//                    + TileToQuadKey(tx, ty, zoomLevel)
//                    + (ms == MapStyle.Road ? ".png" : ".jpeg")
//                    + "?g=" + MapTiles.tileVersion;
//        }
//    }
//}