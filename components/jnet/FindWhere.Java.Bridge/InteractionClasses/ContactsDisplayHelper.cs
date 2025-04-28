//*************************************************************************
//
// $Archive: /gms/product/fol/versions/head/src/java/web_module/src/com/teleca/fleetonline/web/bean/BalanceDisplayHelper.java $
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


namespace com.teleca.fleetonline.web.bean
{


    using ContactData = com.teleca.fleetonline.repository.ContactData;
    //using Logging = com.teleca.fleetonline.utils.Logging;
    using System.Collections;

    ///
    // * 
    // * @author sander.smits
    // * helper class for displaying contact data via javascript
    // 
    [System.Serializable]
    public class ContactsDisplayHelper
    {

        private ArrayList dataList;

        public ArrayList DataList
        {
            get { return dataList; }
            set { dataList = value; }
        }

        public ContactsDisplayHelper(ArrayList dataList)
        {
            this.dataList = dataList;
        }

        public ContactsDisplayHelper()
        {

        }
    }
}