using System.Collections;
using com.teleca.fleetonline.repository;
using com.teleca.fleetonline.mapping;
using com.teleca.fleetonline.utils;
using com.teydo.fleetonline.utils;

namespace com.teleca.fleetonline.web.bean
{
    /// <summary>
    /// Compiles the map display elements.
    /// </summary>
    [System.Serializable]
    public class MapDisplayHelper
    {
        public MapDisplayHelper() { }

        private UserPositionData[] allPos = null;
        private ArrayList lbsRequestList = null;
        private ArrayList members = null;
        private LatLong[] geofenceCornerCoordinates = null;
        private bool showTracePopup = false;

        public UserPositionData[] AllPos  { get { return allPos; }  set { allPos = value; }  }
        public ArrayList LbsRequestList { get { return lbsRequestList; }  set { lbsRequestList = value; } }
        public ArrayList Members { get { return members; } set { members = value; } }
        public LatLong[] GeofenceCornerCoordinates  { get { return geofenceCornerCoordinates; } set { geofenceCornerCoordinates = value; }  }

        // geofence data
        private bool geofenceSet = false;
        private double longitude;
        private double latitude;
        private double radius;
        private bool digitizer;

        public bool GeofenceSet { get { return geofenceSet; } set { geofenceSet = value; } }
        public double Longitude { get { return longitude; } set { longitude = value; }  }
        public double Latitude { get { return latitude; }  set { latitude = value; } }
        public double Radius { get { return radius; } set { radius = value; } }
        public bool Digitizer { get { return digitizer; } set { digitizer = value; }  }

        public MapDisplayHelper(UserPositionData[] all)
        {
            allPos = all;
        }

        public MapDisplayHelper(ArrayList all)
        {
            lbsRequestList = all;
        }

        public MapDisplayHelper(double longitude, double latitude, double radius, bool digitizer)
        {
            this.longitude = longitude;
            this.latitude = latitude;
            this.radius = radius;
            this.digitizer = digitizer;
            this.geofenceSet = true;
        }

        public MapDisplayHelper(LatLong[] geofenceCornerCoordinates)
        {
            this.geofenceCornerCoordinates = geofenceCornerCoordinates;
        }

        private string isLivePosition(string label)
        {
            if (label == null)
                return "notdefined";
            int index = label.IndexOf(";");
            if (index != -1)
            {
                // live position
                return "live";
            }
            else
            {
                return "historic";
            }
        }

        public  string getGeofenceLine(int id)
        {
            LatLong latLong = GeofenceCornerCoordinates[id];
            return "serverResponse.extraDataArray[" + id + "] = new Array(" + latLong.getLatitude() + ", " + latLong.getLongitude() + ")";
        }

        public  string getGeofence()
        {
            string digStr = "false";
            if (Digitizer)
                digStr = "true";
            return "serverResponse.extraDataArray[" + 0 + "] = new Array(" + Latitude + ", " + Longitude + ", " + Radius + ", " + digStr + ")";
        }

        
        public static string calculateDirection(double degrees)
        {
            if (degrees > 360)
                degrees %= 360;


            if (degrees > 338 || (degrees <= 23 && degrees > 0))
                return GlobalConstants.NORTH;
            else if (degrees > 23 && degrees <= 68)
                return GlobalConstants.NORTHEAST;
            else if (degrees > 68 && degrees <= 113)
                return GlobalConstants.EAST;
            else if (degrees > 113 && degrees <= 158)
                return GlobalConstants.SOUTHEAST;
            else if (degrees > 158 && degrees <= 203)
                return GlobalConstants.SOUTH;
            else if (degrees > 203 && degrees <= 248)
                return GlobalConstants.SOUTHWEST;
            else if (degrees > 248 && degrees <= 293)
                return GlobalConstants.WEST;
            else if (degrees > 293 && degrees <= 338)
                return GlobalConstants.NORTHWEST;
            else
                return GlobalConstants.NODIRECTION;
        }

        public  void setAllPos(UserPositionData[] allPos)
        {
            this.AllPos = allPos;
        }

        public  void setMembers(ArrayList members)
        {
            this.Members = members;
        }
    }
}
