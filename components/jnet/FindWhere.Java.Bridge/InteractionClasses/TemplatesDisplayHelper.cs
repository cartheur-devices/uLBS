//*************************************************************************
//
// $Archive: /sdt/product/fol/versions/head/src/java/web_module/src/com/teleca/fleetonline/web/bean/TemplatesDisplayHelper.java $
// $Revision: 1.1 $
// $Date: 2008/04/10 14:28:21 $
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
using com.teleca.fleetonline.web.view;
namespace com.teleca.fleetonline.web.bean
{
    //using Definitions = com.teleca.fleetonline.web.view.Definitions;

    ///
    // * Handles outputting either of the template table: status defs and
    // * text templates
    // * 
    // * @author $Author: salih.canoz $
    // * @version $Revision: 1.1 $, $Date: 2008/04/10 14:28:21 $
    // 
    [System.Serializable]
    public class TemplatesDisplayHelper
    {

        //    *
        //	 * 
        //	 
        private const long serialVersionUID = 1L;
        private Definitions allSm = null;

        public Definitions AllSm
        {
            get { return allSm; }
            set { allSm = value; }
        }

        public TemplatesDisplayHelper()
        {
        }

        //    *
        //	 * Create the helper with the definitions
        //	 * @param all the main data
        //	 
        public TemplatesDisplayHelper(Definitions all)
        {
            allSm = all;
        }
    }
}