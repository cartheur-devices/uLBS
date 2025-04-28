namespace com.teleca.fleetonline.mapping
{

    //*************************************************************************
    //
    // $Source: D:/Data/cvs/FOL/src/java/ejb_module/src/com/teleca/fleetonline/mapping/LatLong.java,v $
    // $Revision: 1.2 $
    // $Date: 2006/07/05 10:43:29 $
    //
    // Copyright : Teydo BV, Bilthoven, all rights reserved worldwide.
    //
    //*************************************************************************
    [System.Serializable]
    public class LatLong
    {

        private double latitude; // coordinate on vertical axis
        private double longitude; // coordinate on horizontal axis

        public LatLong()
        {

        }

        public LatLong(double latitude, double longitude)
        {
            this.latitude = latitude;
            this.longitude = longitude;
        }

        //    *
        //	 * @return Returns the latitude.
        //	 
        public virtual double getLatitude()
        {
            return latitude;
        }

        //    *
        //	 * @param latitude The latitude to set.
        //	 
        public virtual void setLatitude(double latitude)
        {
            this.latitude = latitude;
        }

        //    *
        //	 * @return Returns the longitude.
        //	 
        public virtual double getLongitude()
        {
            return longitude;
        }

        //    *
        //	 * @param longitude The longitude to set.
        //	 
        public virtual void setLongitude(double longitude)
        {
            this.longitude = longitude;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return "latitude: " + getLatitude() + "; longitude: " + getLongitude();
        }
    }
}