using System;
using com.teleca.fleetonline.utils;
using System.Collections;
using com.teleca.fleetonline.repository;
using com.teydo.fleetonline.utils;
using System.Xml;
using JNetBridge.InteractionClasses;

//*************************************************************************
//
// $Archive: /sdt/product/fol/versions/head/src/java/web_module/src/com/teleca/fleetonline/web/bean/SmListDisplayHelper.java $
// $Revision: 1.19 $
// $Date: 2007/07/24 12:17:45 $
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

    ///
    // * Outputs the SM table for the user
    // * 
    // * @author $Author: salih.canoz $
    // * @version $Revision: 1.19 $, $Date: 2007/07/24 12:17:45 $
    // 
    [System.Serializable]
    public class SmListDisplayHelper
    {

        ///
        //	 * 
        //	 
        private const long serialVersionUID = 1L;
               
        private static FOLProperties props = FOLProperties.getInstance();
                
        private static string contactAlias;


        //    *
        //	 * Main SM data
        //	 
        private ArrayList allSm = null;

        public ArrayList AllMessages
        {
            get
            { return allSm; }
            set
            {
                allSm = value;
            }
        }

        //    *
        //	 * fleet infor
        //	 
        private GroupsAndMembersData fleet = null;

        //    *
        //	 * country code in use
        //	 
        private bool msgStatus; //default only

        //The displayname of HQ
        private string hqDisplayName = "HQ";

        //The displayname of unknownAlias
        private string unknownAliasDisplayName = "unknown";

        private int failedMessages;
              
        public SmListDisplayHelper()
        {
        }

        //    *
        //	 * Setup the helper
        //	 * @param all main SM data
        //	 * @param fleet fleet data
        //	 
        public SmListDisplayHelper(ArrayList all, GroupsAndMembersData fleet, bool msgStatus, string hqDisplayName, string unknownAliasDisplayName, int width, int listSize, int failedMessages)
        {
            allSm = all;
            this.fleet = fleet;
            this.msgStatus = msgStatus;
            this.hqDisplayName = hqDisplayName;
            this.unknownAliasDisplayName = unknownAliasDisplayName;
            this.failedMessages = failedMessages;
        }             

        internal void DeSerialize(string sourceXML)
        {
            XmlDocument workingXmlDoc = new XmlDocument();
            workingXmlDoc.LoadXml(sourceXML);

            //fleet = GroupsAndMembersData.DeSerializeSingle(workingXmlDoc.SelectSingleNode("//fleet"));

            #region UserData
            //UserData usrData = new UserData();
            //workingXmlDoc.LoadXml(xdMain.SelectSingleNode("//UserData").InnerXml);
            Utils.FillProperties(this, workingXmlDoc.FirstChild);
            #endregion
        }
    }
}