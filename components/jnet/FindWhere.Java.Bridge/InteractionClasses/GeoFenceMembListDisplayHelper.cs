using System.Collections;
namespace com.teleca.fleetonline.web.bean
{

    //*************************************************************************
    //
    // $Source: D:/Data/cvs/FOL5/web_module/src/com/teleca/fleetonline/web/bean/GeoFenceMembListDisplayHelper.java,v $
    // $Revision: 1.1 $
    // $Date: 2008/04/10 14:28:22 $
    //
    // Copyright : Teydo BV, Bilthoven, all rights reserved worldwide.
    //
    //*************************************************************************
    
    ///
    // *
    // 
    [System.Serializable]
    public class GeoFenceMembListDisplayHelper
    {

        //	private static SimpleDateFormat dateTimeFormatter = new SimpleDateFormat ("yyyy,MM,dd,HH,mm");

        //    *
        //	 * 
        //	 
        private const long serialVersionUID = 1L;
        private ArrayList dataList = null;

        public ArrayList DataList
        {
            get { return dataList; }
            set { dataList = value; }
        }


        public GeoFenceMembListDisplayHelper()
        {
        }

        public GeoFenceMembListDisplayHelper(ArrayList  dataList)
        {
            this.dataList = dataList;
        }
    }
}