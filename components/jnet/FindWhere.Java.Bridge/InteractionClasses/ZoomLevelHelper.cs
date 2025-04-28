//*************************************************************************
//
// $Archive: /sdt/product/fol/versions/head/src/java/web_module/src/com/teleca/fleetonline/web/bean/ZoomLevelHelper.java $
// $Revision: 1.1.1.1 $
// $Date: 2004/09/27 11:00:42 $
// $Author: hotten $
//
//*************************************************************************

//
// * @(#)FILENAME
// *
// * Copyright (c) 2002 Teleca AU.
// * Bartholomew's Gate, 11-13 Charterhouse Buildings, EC1M 7AP
// * All rights reserved.
// *
// * This software is the confidential and proprietary information of Teleca AU.
// * ("Confidential Information").  You shall not
// * disclose such Confidential Information and shall use it only in
// * accordance with the terms of the license agreement you entered into
// * with Teleca AU.
// 
namespace com.teleca.fleetonline.web.bean
{

    ///
    // * Helps output the zoom level for the JSPs
    // * 
    // * @author $Author: hotten $
    // * @version $Revision: 1.1.1.1 $, $Date: 2004/09/27 11:00:42 $
    // 
    [System.Serializable]
    public class ZoomLevelHelper
    {
        //    *
        //	 * the actual zoom level
        //	 
        private int content = 2;

        public int Content
        {
            get { return content; }
            set { content = value; }
        }

        //    *
        //	 * Default level
        //	 * @see java.lang.Object#Object()
        //	 
        public ZoomLevelHelper()
        {
        }

        //    *
        //	 * Sets the level to output to the HTML
        //	 * @param level
        //	 
        public ZoomLevelHelper(int level)
        {
            this.content = level;
        }

        //    *
        //	 * Get the zoom level
        //	 * @return String
        //	 
	    /// <summary>
	    /// 
	    /// </summary>
	    /// <returns></returns>
        public virtual string getContent()
        {
            return "" + Content;
        }
    }
}