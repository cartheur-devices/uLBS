using System;
using System.Collections.Generic;
using System.Globalization;
using System.Xml;
using System.Runtime.Serialization;
using System.IO;

namespace PositionSenderMobile.Locations
{
    public class Location
    {
        private List<LocationMap> _maps = new List<LocationMap>();
        private string _id = string.Empty;
        private string _name = string.Empty;
        private Coordinate _coordinate = new Coordinate();
        private string _webLink = string.Empty;
        private string _error = string.Empty;

        /// <summary>
        /// Gets the list of retrieved maps for this cache
        /// </summary>
        public List<LocationMap> Maps
        {
            get
            {
                return _maps;
            }
        }

        /// <summary>
        /// Gets the ID of this cache
        /// </summary>
        public string CacheID
        {
            get
            {
                return _id;
            }
            set
            {
                _id = value;
            }
        }

        /// <summary>
        /// Gets the name of this cache
        /// </summary>
        public string CacheName
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
            }
        }

        /// <summary>
        /// Gets the coordinates of this cache
        /// </summary>
        public Coordinate CacheCoordinates
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
        /// Gets the weblink for this cache
        /// </summary>
        public string WebLink
        {
            get
            {
                return _webLink;
            }
            set
            {
                _webLink = value;
            }
        }
        /// <summary>
        /// Import a .LOC file from groundspeak geocaching site
        /// </summary>
        /// <param name="locFilePath">the filepath</param>
        public void ImportLocFile(string locFilePath)
        {
            XmlDocument locDoc = new XmlDocument();

            _error = string.Empty;

            try
            {
                locDoc.Load(locFilePath);

                XmlNode nameNode = locDoc.SelectSingleNode("//loc/waypoint/name");
                if (nameNode == null)
                {
                    throw new Exception("Invalid LOC file syntax, name is missing");
                }
                
                _id = nameNode.Attributes["id"].Value;
                _name = nameNode.InnerText;

                XmlNode coordinateNode = locDoc.SelectSingleNode("//loc/waypoint/coord");
                if (coordinateNode == null)
                {
                    throw new Exception("Invalid LOC file syntax, coordinates are missing");
                }

                _coordinate.Latitude = Double.Parse(coordinateNode.Attributes["lat"].Value, CultureInfo.InvariantCulture);
                _coordinate.Longitude = Double.Parse(coordinateNode.Attributes["lon"].Value, CultureInfo.InvariantCulture);

                XmlNode webLinkNode = locDoc.SelectSingleNode("//loc/waypoint/link");
                if (coordinateNode == null)
                {
                    throw new Exception("Invalid LOC file syntax, weblink is missing");
                }

                _webLink = webLinkNode.InnerText;

                // get map(s) from google!
                MapUrlBuilder builder = new MapUrlBuilder();
                builder.CenterCoordinate = _coordinate;
                builder.MapType = "mobile";
                builder.ZoomLevel = 17;
                builder.GoogleMapsAPIKey = "ABQIAAAAQNQ-lLBY61toS5K59LauWRTXW8Kv5ecGOOs1mS93E0Z_7nXLTRR3mz79IpNA64iGZJfqdbM1-x_ibA";

                _maps.Add(new LocationMap(builder.MapUrl));
            }
            catch (Exception ex)
            {
                _error = "Fout opgetreden bij importeren LOC bestand: " + ex.Message;
                throw ex;
            }
        }

        /// <summary>
        /// Error string (if any)
        /// </summary>
        public string Error
        {
            get
            {
                string retVal =  _error;
                _error = string.Empty;
                return retVal;
            }
        }
    }
}
