using System.Collections;
using com.teleca.fleetonline.repository;
namespace com.teleca.fleetonline.web.bean
{

    //*************************************************************************
    //
    // $Source: D:/Data/cvs/FOL/src/java/web_module/src/com/teleca/fleetonline/web/bean/NotificationListDisplayHelper.java,v $
    // $Revision: 1.7 $
    // $Date: 2007/07/24 12:17:47 $
    //
    // Copyright : Teydo BV, Bilthoven, all rights reserved worldwide.
    //
    //*************************************************************************


    ///
    // *
    // 
    [System.Serializable]
    public class NotificationListDisplayHelper
    {

        //    *
        //	 * 
        //	 
        private const long serialVersionUID = 1L;
        private ArrayList dataList = null;

        public ArrayList DataList { get { return dataList; } set { dataList = value; } }

        public NotificationListDisplayHelper()
        {
        }

        public NotificationListDisplayHelper(ArrayList dataList)
        {
            this.dataList = dataList;
        }
    }
}