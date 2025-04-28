//*************************************************************************
//
// $Archive: /sdt/product/fol/versions/head/src/java/web_module/src/com/teleca/fleetonline/web/bean/ResponseTypeHelper.java $
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
    // * Output's the correct 'request type' for the HTML
    // * 
    // * @author $Author: hotten $
    // * @version $Revision: 1.1.1.1 $, $Date: 2004/09/27 11:00:42 $
    // 
    [System.Serializable]
    public class ResponseTypeHelper
    {

        internal string type = "UNKNOWN";

        public ResponseTypeHelper()
        {
        }

        //    *
        //	 * setup the helper
        //	 * @param type the request type string
        //	 
        public ResponseTypeHelper(string type)
        {
            this.type = type;
        }

        public sealed override string ToString()
        {
            return @get();
        }

        //    *
        //	 * Return the data
        //	 * @return String the request type
        //	 
        public string @get()
        {
            return type;
        }
    }
}