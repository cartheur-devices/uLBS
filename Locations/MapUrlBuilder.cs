using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;

namespace PositionSenderMobile.Locations
{
    public class MapUrlBuilder
    {
        private Coordinate _coordinate = new Coordinate();
        private int _zoomLevel = 17;
        private string _mapType = "roadmap";
        private string _apiKey = string.Empty;
        private int _xSize = 512;
        private int _ySize = 512;

        #region public properties
        public int XSize
        {
            get
            {
                return _xSize;
            }
            set
            {
                _xSize = value;
            }
        }
        public int YSize
        {
            get
            {
                return _ySize;
            }
            set
            {
                _ySize = value;
            }
        }
        /// <summary>
        /// Coordinate to use in url
        /// </summary>
        public Coordinate CenterCoordinate
        {
            get
            {
                return _coordinate;
            }
            set
            {
                _coordinate = value;
            }
        }

        /// <summary>
        /// Zoomlevel to use in url
        /// </summary>
        public int ZoomLevel
        {
            get
            {
                return _zoomLevel;
            }
            set
            {
                _zoomLevel = value;
            }
        }

        /// <summary>
        /// Maptype to use in url
        /// </summary>
        public string MapType
        {
            get
            {
                return _mapType;
            }
            set
            {
                _mapType = value;
            }
        }

        /// <summary>
        ///  API key to use in url
        /// </summary>
        public string GoogleMapsAPIKey
        {
            get
            {
                return _apiKey;
            }
            set
            {
                _apiKey = value;
            }
        }
        #endregion

        /// <summary>
        /// The resulting url
        /// </summary>
        public string MapUrl
        {
            get
            {
                return String.Format(CultureInfo.InvariantCulture,
                    "http://maps.google.com/staticmap?center={0},{1}&size={5}x{6}&markers={0},{1},greenc&zoom={2}&maptype={3}&key={4}",
                    _coordinate.Latitude, _coordinate.Longitude, _zoomLevel, _mapType, _apiKey, _xSize, _ySize);
            }
        }

        /// <summary>
        ///  Default string representation
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return MapUrl;
        }
    }
}
