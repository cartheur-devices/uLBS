//// Copyright (c) Jason Fuller. 
////
//// Use of this source code is subject to the terms of the license found in the
//// file License.txt.  If you did not accept the terms of that license, you are
//// not authorized to use this source code.

//#region Using directives

//using System;
//using System.Drawing;
//using System.Collections;
//using System.Windows.Forms;

//#endregion

//namespace PocketEarth
//{
//    public enum MapStyle
//    {
//        Road,
//        Aerial,
//        Hybrid
//    }

//    /// <summary>
//    /// A map displayed on the screen.
//    /// </summary>
//    public class Map
//    {
//        private int width, height;
//        private int zoomLevel;
//        /// <summary>
//        /// The X-coordinate of the center of the map, in pixels, in world coordinates (at the current zoom level).
//        /// </summary>
//        private int x;
//        /// <summary>
//        /// The Y-coordinate of the center of the map, in pixels, in world coordinates (at the current zoom level).
//        /// </summary>
//        private int y;
//        /// <summary>
//        /// The latitude of the center of the map.
//        /// </summary>
//        private double latitude;
//        /// <summary>
//        /// The longitude of the center of the map.
//        /// </summary>
//        private double longitude;
//        private MapStyle mapStyle;

//        private const int earthRadius = 6378137;
//        private const int buffer = 0;
//        private const double offsetMeters = 20971520;
//        private const double baseMetersPerPixel = 163840;

//        /// <summary>
//        /// Minimum allowed zoom level
//        /// </summary>
//        public int MinimumZoom
//        {
//            get { return 3; }
//        }

//        /// <summary>
//        /// Maximum allowed zoom level
//        /// </summary>
//        public int MaximumZoom
//        {
//            get { return 19; }
//        }


//        public double Latitude
//        {
//            get
//            {
//                if (double.IsNaN(latitude))
//                {

//                    // We have panned, so we must recalculate the latitude.
//                    latitude = YToLatitudeAtZoom(Y, zoomLevel);
//                }
//                return latitude;
//            }
//            set
//            {
//                latitude = value;
//                y = LatitudeToYAtZoom(latitude, zoomLevel);
//            }
//        }

//        public double Longitude
//        {
//            get
//            {
//                if (double.IsNaN(longitude))
//                {
//                    // We have panned, so we must recalculate the longitude.
//                    longitude = XToLongitudeAtZoom(X, zoomLevel);
//                }
//                return longitude;
//            }
//            set
//            {
//                longitude = value;
//                x = LongitudeToXAtZoom(longitude, zoomLevel);
//            }
//        }


//        /// <summary>
//        /// The X-coordinate of the center of the map, in pixels,
//        /// in world coordinates (at the current zoom level).
//        /// </summary>
//        public int X
//        {
//            get { return x; }
//            set
//            {
//                x = value;
//                // We don't bother recalculating the longitude until asked for.
//                // So right now, we just invalidate it.
//                longitude = double.NaN;
//            }
//        }

//        /// <summary>
//        /// The Y-coordinate of the center of the map, in pixels,
//        /// in world coordinates (at the current zoom level).
//        /// </summary>
//        public int Y
//        {
//            get { return y; }
//            set
//            {
//                y = value;
//                // We don't bother recalculating the latitude until asked for.
//                // So right now, we just invalidate it.
//                latitude = double.NaN;
//            }
//        }

//        /// <summary>
//        /// The style of map (road, aerial, or hybrid).
//        /// </summary>
//        public MapStyle MapStyle
//        {
//            get { return mapStyle; }
//            set { mapStyle = value; }
//        }

//        /// <summary>
//        /// The current zoom level.
//        /// </summary>
//        public int ZoomLevel
//        {
//            get { return zoomLevel; }
//            set
//            {
//                // Make sure we have a valid lat/long at the old zoom level
//                // before we change the zoom level.
//                double lon = Longitude;
//                double lat = Latitude;
//                zoomLevel = value;
//                x = LongitudeToXAtZoom(lon, zoomLevel);
//                y = LatitudeToYAtZoom(lat, zoomLevel);
//            }
//        }


//        public Map(
//            int width,
//            int height,
//            double latitude,
//            double longitude,
//            int zoomLevel,
//            MapStyle mapStyle)
//        {
//            this.width = width;
//            this.height = height;
//            this.zoomLevel = zoomLevel;
//            this.Latitude = latitude;
//            this.Longitude = longitude;
//            this.MapStyle = mapStyle;
//        }


//        public void Paint(Graphics g)
//        {
//            int drawingX, drawingY; // Where we are currently drawing on the window
//            int originX, originY;

//            // Get the X,Y coordinates (in world coordinates) of the
//            // origin of the window.
//            originX = X - this.width / 2;
//            originY = Y - this.height / 2;
//            int s = MapTiles.TileSize;
//            // Calculate how much we need to shift the first tile (and
//            // therefore all tiles) by to line up (originX, originY)
//            // with the origin of the window.
//            int drawingOffsetX = originX % s;
//            int drawingOffsetY = originY % s;
//            int b = Map.buffer;
//            int w = this.width;
//            int h = this.height;

//            // Calculate the first and last tile indices needed to fill
//            // the whole winodw.
//            int txStart = (int)Math.Floor((originX - b) / s);
//            int tyStart = (int)Math.Floor((originY - b) / s);
//            int txEnd = (int)Math.Floor((originX + w + b) / s);
//            int tyEnd = (int)Math.Floor((originY + h + b) / s);
//            txStart = Math.Max(0, txStart);
//            tyStart = Math.Max(0, tyStart);

//            // Loop through all the tiles needed to fill the window.
//            for (int tx = txStart; tx <= txEnd; tx++)
//            {
//                drawingX = (tx - txStart) * MapTiles.TileSize - drawingOffsetX;
//                for (int ty = tyStart; ty <= tyEnd; ty++)
//                {
//                    drawingY = (ty - tyStart) * MapTiles.TileSize - drawingOffsetY;
//                    Bitmap bmp = MapTiles.GetMapTile(tx, ty, ZoomLevel, MapStyle);
//                    g.DrawImage(bmp, drawingX, drawingY);
//                }
//            }
//        }


//        private static double MetersPerPixel(int zl)
//        {
//            return baseMetersPerPixel / (1 << zl);
//        }

//        private const double earthCircum = earthRadius * 2.0 * Math.PI;

//        private const double earthHalfCirc = earthCircum / 2;

//        private int LongitudeToXAtZoom(double lon, int zl)
//        {

//            double arc = Map.earthCircum / ((1 << zl) * 256);

//            double metersX = Map.earthRadius * DegToRad(lon);

//            return (int)Math.Round((Map.earthHalfCirc + metersX) / arc);

//        }

//        private int LatitudeToYAtZoom(double lat, int zl)
//        {

//            double arc = Map.earthCircum / ((1 << zl) * 256);

//            double sinLat = Math.Sin(DegToRad(lat));

//            double metersY = Map.earthRadius / 2 * Math.Log((1 + sinLat) / (1 - sinLat));

//            return (int)Math.Round((Map.earthHalfCirc - metersY) / arc);

//        }

//        private double YToLatitudeAtZoom(int y, int zl)
//        {

//            double arc = Map.earthCircum / ((1 << zl) * 256);

//            double metersY = Map.earthHalfCirc - y * arc;

//            return RadToDeg(Math.PI / 2 - 2 * Math.Atan(Math.Exp(-metersY / Map.earthRadius)));

//        }

//        private double XToLongitudeAtZoom(int x, int zl)
//        {

//            //double metersX = x * MetersPerPixel(zl) - offsetMeters;

//            //return RadToDeg(metersX / earthRadius);

//            double arc = Map.earthCircum / ((1 << zl) * 256);

//            double metersX = x * arc - Map.earthHalfCirc;

//            return RadToDeg(metersX / Map.earthRadius);

//        }


//        private static double DegToRad(double d)
//        {
//            return d * Math.PI / 180.0;
//        }

//        private static double RadToDeg(double r)
//        {
//            return r * 180.0 / Math.PI;
//        }


//        /// <summary>
//        /// View the rectangle defined by lat1, lon1, lat2, lon2.
//        /// </summary>
//        /// <param name="lat1"></param>
//        /// <param name="lon1"></param>
//        /// <param name="lat2"></param>
//        /// <param name="lon2"></param>
//        public void SetViewport(double lat1, double lon1, double lat2, double lon2)
//        {
//            // The zoom level currently being tried.
//            int z = 1;
//            int w, h;
//            // Add 2 at a time to the zoom level because odd zoom levels are 
//            // all our UI (and the menu items) support.
//            const int step = 2;

//            // Zoom in until the rectangle specified by lat1,lon1,lat2,lon2
//            // is bigger than the window. 
//            do
//            {
//                z += step;
//                // Calculate the width and height of the window at the current
//                // zoom level z.
//                w = Math.Abs(this.LongitudeToXAtZoom(lon1, z) - this.LongitudeToXAtZoom(lon2, z));
//                h = Math.Abs(this.LatitudeToYAtZoom(lat1, z) - this.LatitudeToYAtZoom(lat2, z));
//            } while (z < MaximumZoom
//                     && w < this.width
//                     && h < this.height);

//            this.zoomLevel = Math.Min(MaximumZoom, z);
//            this.Latitude = 0.5 * (lat1 + lat2);
//            this.Longitude = 0.5 * (lon1 + lon2);
//        }

//        public void GetViewport(
//            out double lat1,
//            out double lon1,
//            out double lat2,
//            out double lon2)
//        {
//            lat1 = YToLatitudeAtZoom(Y - height / 2, ZoomLevel);
//            lon1 = XToLongitudeAtZoom(X + width / 2, ZoomLevel);
//            lat2 = YToLatitudeAtZoom(Y + height / 2, ZoomLevel);
//            lon2 = XToLongitudeAtZoom(X - width / 2, ZoomLevel);
//        }
//    }
//}