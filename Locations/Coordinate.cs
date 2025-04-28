using System;
using System.Collections.Generic;
using System.Text;

namespace PositionSenderMobile.Locations
{
    /// <summary>
    /// Class to represent coordinates and perform calculations
    /// </summary>
    public class Coordinate
    {
        #region Private member variables
        // Coordinate values
        private double _latitude = 0.0;
        private double _longitude = 0.0;
        #endregion

        #region private helper methods
        /// <summary>
        /// COnvert degrees to radians
        /// </summary>
        /// <param name="value">degree value</param>
        /// <returns>radian value</returns>
        private double ToRadians(double value)
        {
            return (value * Math.PI / 180.0);
        }
        /// <summary>
        /// Returns integral part of a double (CF does not support Math.Truncate())
        /// </summary>
        /// <param name="value">value to truncate</param>
        /// <returns>truncated value</returns>
        private double Truncate(double value)
        {
            double result = value;

            if (Math.Sign(value) < 0)
            {
                result = Math.Ceiling(value);
            }
            else
            {
                result = Math.Floor(value);
            }
            return result;
        }
        #endregion

        #region constructor(s)
        /// <summary>
        /// Default constructor (no arguments)
        /// </summary>
        public Coordinate()
        {
        }

        /// <summary>
        /// Constructor with simple latitude / longitude arguments
        /// </summary>
        /// <param name="latitude">latitude</param>
        /// <param name="longitude">longitude</param>
        public Coordinate(double latitude, double longitude)
        {
            _latitude = latitude;
            _longitude = longitude;
        }

        /// <summary>
        /// COnstructor with degrees/minutes components
        /// </summary>
        /// <param name="latitudeDegrees"></param>
        /// <param name="latitudeMinutes"></param>
        /// <param name="longitudeDegrees"></param>
        /// <param name="longitudeMinutes"></param>
        public Coordinate(double latitudeDegrees, double latitudeMinutes,
                          double longitudeDegrees, double longitudeMinutes)
        {
            _latitude = latitudeDegrees + (latitudeMinutes / 60);
            _longitude = longitudeDegrees + (longitudeMinutes / 60);
        }

        /// <summary>
        /// COnstrutor with all of them
        /// </summary>
        /// <param name="latitudeDegrees"></param>
        /// <param name="latitudeMinutes"></param>
        /// <param name="latitudeSeconds"></param>
        /// <param name="longitudeDegrees"></param>
        /// <param name="longitudeMinutes"></param>
        /// <param name="longitudeSeconds"></param>
        public Coordinate(double latitudeDegrees, double latitudeMinutes, double latitudeSeconds,
                          double longitudeDegrees, double longitudeMinutes, double longitudeSeconds)
        {
            _latitude = latitudeDegrees + ((latitudeMinutes + (latitudeSeconds / 60)) / 60);
            _longitude = longitudeDegrees + ((longitudeMinutes + (longitudeSeconds / 60)) / 60);
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets / sets the latitude component
        /// </summary>
        public double Latitude
        {
            get
            {
                return _latitude;
            }
            set
            {
                _latitude = value;
            }
        }

        /// <summary>
        /// Gets / sets the longitude component
        /// </summary>
        public double Longitude
        {
            get
            {
                return _longitude;
            }
            set
            {
                _longitude = value;
            }
        }
        #endregion

        #region public coordinate string writers
        /// <summary>
        /// FOrmat according latitude/longitude
        /// </summary>
        public string LatitudeLongitude
        {
            get
            {
                string result = String.Format("{1} {0:00.000000} {3} {2:000.000000}", 
                    _latitude, _latitude >= 0.0 ? "N" : "S", _longitude, _longitude >= 0.0 ? "E" : "W");
                return result;
            }
        }

        /// <summary>
        /// FOrmat according to DM format
        /// </summary>
        public string DegreesMinutes
        {
            get
            {
                string result = String.Empty;
                double latitudeDegrees = 0.0, latitudeMinutes = 0.0;
                double longitudeDegrees = 0.0, longitudeMinutes = 0.0;

                latitudeDegrees = Truncate(_latitude);
                latitudeMinutes = (_latitude - latitudeDegrees) * 60;

                longitudeDegrees = Truncate(_longitude);
                longitudeMinutes = (_longitude - longitudeDegrees) * 60;

                result = String.Format("{2} {0:00} {1:00.000}\' {5} {3:000} {4:00.000}\'",
                    latitudeDegrees, latitudeMinutes, _latitude >= 0.0 ? "N" : "S",
                    longitudeDegrees, longitudeMinutes, _longitude >= 0.0 ? "E" : "W");
                return result;
            }
        }

        /// <summary>
        /// FOrmat according to DMS format
        /// </summary>
        public string DegreesMinutesSeconds
        {
            get
            {
                string result = String.Empty;
                double latitudeDegrees = 0.0, latitudeMinutes = 0.0, latitudeSeconds = 0.0;
                double longitudeDegrees = 0.0, longitudeMinutes = 0.0, longitudeSeconds = 0.0;

                latitudeDegrees = Truncate(_latitude);
                latitudeMinutes = Truncate((_latitude - latitudeDegrees) * 60);
                latitudeSeconds = (((_latitude - latitudeDegrees) * 60) - latitudeMinutes) * 60;

                longitudeDegrees = Truncate(_longitude);
                longitudeMinutes = Truncate((_longitude - longitudeDegrees) * 60);
                longitudeSeconds = (((_longitude - longitudeDegrees) * 60) - longitudeMinutes) * 60;

                result = String.Format("{3} {0:00} {1:00}\' {2:00.0000}\" {7} {4:000} {5:00}\' {6:00.0000}\"",
                    latitudeDegrees, latitudeMinutes, latitudeSeconds, _latitude >= 0.0 ? "N" : "S", 
                    longitudeDegrees, longitudeMinutes, longitudeSeconds, _longitude >= 0.0 ? "E" : "W");
                return result;
            }
        }
        #endregion

        #region public methods
        /// <summary>
        /// Calculates the distance between this coordinate and the argument
        /// uing law of cosines
        /// </summary>
        /// <param name="coordinate">Point to calculate distance to</param>
        /// <returns>The distance</returns>
        public double Distance(Coordinate coordinate)
        {
            double R = 6371.0;      // earth's radius in km
            double lat1 = ToRadians(_latitude);
            double lat2 = ToRadians(coordinate.Latitude);
            double deltaLongitude = ToRadians(coordinate.Longitude - _longitude);

            double distance = Math.Acos(Math.Sin(lat1) * Math.Sin(lat2) +
                              Math.Cos(lat1) * Math.Cos(lat2) * Math.Cos(deltaLongitude)) * R;

            return distance * 1000.0; // distance in meters
        }
        #endregion
    }
}
