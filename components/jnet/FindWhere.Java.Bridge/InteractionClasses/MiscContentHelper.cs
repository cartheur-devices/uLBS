//*************************************************************************
//
// $Archive: /sdt/product/fol/versions/head/src/java/web_module/src/com/teleca/fleetonline/web/bean/MiscContentHelper.java $
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
    // * Helper that outputs miscellaneous text content
    // * 
    // * @author $Author: hotten $
    // * @version $Revision: 1.1.1.1 $, $Date: 2004/09/27 11:00:42 $
    // 
    [System.Serializable]
    public class MiscContentHelper
    {
        private string content = "";

        public string Content
        {
            get { return content; }
            set { content = value; }
        }
        private string type = "";

        public string Type
        {
            get { return type; }
            set { type = value; }
        }
        public MiscContentHelper()
        {
        }
        //    *
        //	 * Create new help
        //	 * @param content to output
        //	 
        public MiscContentHelper(string content)
        {
            this.content = content;
        }

        public MiscContentHelper(string content, string type)
        {
            this.content = content;
            this.type = type;
        }

        //    *
        //	 * return the actual content
        //	 * @return String content
        //	 
        public virtual string getContent()
        {
            //content= "true";
            return Content;
        }

        public virtual string getType()
        {
            return Type;
        }
    }
}