using System.Drawing;
using System.Net;
using System;

namespace PositionSenderMobile.Locations
{
    public class LocationMap
    {
        private Bitmap _map = new Bitmap(512, 512);

        /// <summary>
        /// Default constructor
        /// </summary>
        public LocationMap()
        {
        }

        /// <summary>
        /// Csontructor taking a GoogleMaps API webrequest URI
        /// </summary>
        /// <param name="url">url to use as request</param>
        public LocationMap(string url)
        {
            _map = FromUrl(url);
        }

        /// <summary>
        /// Resolves the url and returns the map image
        /// </summary>
        /// <param name="url">orl to resolve</param>
        /// <returns>the resulting bitmap</returns>
        private Bitmap FromUrl(string url)
        {
            WebRequest request = HttpWebRequest.Create(url);
            WebResponse response = request.GetResponse();
            Bitmap bmp = new Bitmap(response.GetResponseStream());
            return bmp;
        }

        /// <summary>
        /// The bitmap
        /// </summary>
        public Bitmap Map
        {
            get
            {
                return _map;
            }
            set
            {
                _map = value;
            }
        }
    }
}
