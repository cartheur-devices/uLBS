//*************************************************************************
//
// $Archive: /sdt/product/fol/versions/head/src/java/ejb_module/src/com/teleca/fleetonline/web/view/Definitions.java $
// $Revision: 1.1 $
// $Date: 2008/04/10 14:28:08 $
// $Author: salih.canoz $
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
namespace com.teleca.fleetonline.web.view
{
    ///
    // * 
    // * @author $Author: salih.canoz $
    // * @version $Revision: 1.1 $, $Date: 2008/04/10 14:28:08 $
    // 
    [System.Serializable]
    public class Definitions
    {
        private string[] namesarray;

        public string[] Namesarray
        {
            get { return namesarray; }
            set { namesarray = value; }
        }
        private string[] textsarray;

        public string[] Textsarray
        {
            get { return textsarray; }
            set { textsarray = value; }
        }
    }
}