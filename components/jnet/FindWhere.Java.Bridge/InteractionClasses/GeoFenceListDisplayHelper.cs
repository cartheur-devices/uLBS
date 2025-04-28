using com.teleca.fleetonline.repository;
using System.Collections;
namespace com.teleca.fleetonline.web.bean
{

    //*************************************************************************
    //
    // $Source: D:/Data/cvs/FOL5/web_module/src/com/teleca/fleetonline/web/bean/GeoFenceListDisplayHelper.java,v $
    // $Revision: 1.1 $
    // $Date: 2008/04/10 14:28:22 $
    //
    // Copyright : Teydo BV, Bilthoven, all rights reserved worldwide.
    //
    //*************************************************************************


    using GeoFenceData = com.teleca.fleetonline.repository.GeoFenceData;
    using UserInfoData = com.teleca.fleetonline.repository.UserInfoData;
    using GlobalConstants = com.teleca.fleetonline.utils.GlobalConstants;

    ///
    // *
    // 
    [System.Serializable]
    public class GeoFenceListDisplayHelper
    {

        //    *
        //	 * 
        //	 
        private const long serialVersionUID = 1L;
        private static string numFormat = ".0000000000;-.0000000000";        
        private static string radiusFormat = "########,#######0.##";
       
        private ArrayList dataList = null;

        public ArrayList DataList
        {
            get { return dataList; }
            set { dataList = value; }
        }
        private int distanceUnit = GlobalConstants.DISTANCE_KM;

        public int DistanceUnit
        {
            get { return distanceUnit; }
            set { distanceUnit = value; }
        }        

        public GeoFenceListDisplayHelper()
        {
        }    
    }
}