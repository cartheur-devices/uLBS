using System;
using System.Collections;
using com.teleca.fleetonline.utils;
using com.teleca.fleetonline.repository;
using com.teydo.fleetonline.utils;

namespace com.teleca.fleetonline.web.bean
{

    //*************************************************************************
    //
    // $Source: D:/Data/cvs/FOL/src/java/web_module/src/com/teleca/fleetonline/web/bean/NotifMsgListDisplayHelper.java,v $
    // $Revision: 1.14 $
    // $Date: 2007/07/24 12:17:45 $
    //
    // Copyright : Teydo BV, Bilthoven, all rights reserved worldwide.
    //
    //*************************************************************************

    ///
    // * Outputs the notification messages table for the user
    // * 
    // 
    [System.Serializable]
    public class NotifMsgListDisplayHelper
    {

        //    *
        //	 * 
        //	 
        private const long serialVersionUID = 1L;

        private const int MAX_RECIPIENTS_LEN = 17;

        private static FOLProperties folProps = FOLProperties.getInstance();
       
        private static string[] eventTypeDesc = null;

        public static string[] EventTypeDesc
        {
            get { return NotifMsgListDisplayHelper.eventTypeDesc; }
            set { NotifMsgListDisplayHelper.eventTypeDesc = value; }
        }
        private static int[] eventTypeId = null;

        public static int[] EventTypeId
        {
            get { return NotifMsgListDisplayHelper.eventTypeId; }
            set { NotifMsgListDisplayHelper.eventTypeId = value; }
        }

        public ArrayList dataList = null;

        private ContactData[] contact = null;

        public ContactData[] Contact
        {
            get { return contact; }
            set { contact = value; }
        }
        private bool msgStatus; //default only

        public bool MsgStatus
        {
            get { return msgStatus; }
            set { msgStatus = value; }
        }
        private int failedMessages;

        public int FailedMessages
        {
            get { return failedMessages; }
            set { failedMessages = value; }
        }

        static NotifMsgListDisplayHelper()
        {
        }

        public NotifMsgListDisplayHelper()
        {
        }

        public NotifMsgListDisplayHelper(ArrayList all, GroupsAndMembersData fleet, bool msgStatus, string hqDisplayName, string unknownAliasDisplayName, int width, int listSize, int failedMessages)
        {
            dataList = all;
            this.msgStatus = msgStatus;
            this.failedMessages = failedMessages;
        }

        private static void buildDescrArray()
        {
            string propName = folProps.getProperty("notification.spec.nrOfItems");
            try
            {
                int itemCnt = Convert.ToInt32(propName);
                EventTypeDesc = new string[itemCnt];
                EventTypeId = new int[itemCnt];
                for (int i = 0; i < itemCnt; i++)
                {
                    EventTypeDesc[i] = null;
                    EventTypeId[i] = 0;
                }

                for (int i = 1; i <= itemCnt; i++)
                {
                    propName = folProps.getProperty("notification.spec." + i + ".eventtype");
                    EventTypeId[i - 1] = Convert.ToInt32(propName);
                    EventTypeDesc[i - 1] = folProps.getProperty("notification.spec." + i + ".txt.commcenter");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("NotifMsgListDisplayHelper.buildDescrArray:: ERROR parsing notification.spec.nrOfItems or notification.spec.<n>.eventtype");
                return;
            }
        }

        private static string getDesc(int id)
        {
            string retVal = null;
            if (eventTypeId != null)
            {
                for (int i = 0; i < eventTypeId.Length; i++)
                {
                    if (eventTypeId[i] == id)
                    {
                        retVal = eventTypeDesc[i];
                        break;
                    }
                }
            }
            return (retVal);
        }
    }
}