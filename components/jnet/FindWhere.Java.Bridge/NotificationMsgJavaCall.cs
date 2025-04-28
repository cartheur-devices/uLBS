using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

using JNetBridge.ReplyClasses;
using JNetBridge.InteractionClasses;
using System.Xml.XPath;
using System.Xml.Serialization;
using System.Reflection;
using com.teleca.fleetonline.web.bean;
using System.Net;

namespace JNetBridge
{
    /// <summary>
    /// 
    /// </summary>
    public class NotificationMsgJavaCall : JavaCall
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="JSESSIONID"></param>
        /// <returns></returns>
        public static NotificationMsgJavaCallReply GetNotifications(JNetBridge.Classes.JnetBridgeLoginUnit JSESSIONID)
        {
            return (NotificationMsgJavaCallReply)new NotificationMsgJavaCall(JSESSIONID).DoAction();
        }

        private NotificationMsgJavaCall(JNetBridge.Classes.JnetBridgeLoginUnit JSESSIONID)
            : base(JavaCall.baseURL + "notificationAction.do",
            JSESSIONID,
                new string[] { },
                new string[] { })
        {

        }

        /// <summary>
        /// This method does the actual conversion from serialized data to the deserialized LoginJavaCallReply object
        /// </summary>
        /// <param name="reply"></param>
        /// <returns></returns>
        protected override JavaCallReply ParseServerReply(string reply, Cookie JSESSIONID)
        {
            try
            {
                // Main XML Document
                XmlDocument xdMain = new XmlDocument();
                int pos1 = reply.IndexOf("<?xml");
                int pos2 = reply.IndexOf("</main>");

                xdMain.LoadXml(reply.Substring(pos1, (pos2 - pos1 + 7)));

                if (xdMain.SelectSingleNode("error") != null)
                {
                    throw new Exception(xdMain.SelectSingleNode("error").InnerText);
                }

#if DEBUG
                if (System.Environment.MachineName == "TEYDO11")
                {
                    //xdMain.Save(@"c:\ReceivedXml.xml");
                }
#endif


                XmlDocument workingXmlDoc;
                workingXmlDoc = new XmlDocument();

                NotificationMsgJavaCallReply gmj = new NotificationMsgJavaCallReply();
                Utils.FillProperties(gmj, xdMain.SelectSingleNode("//main"));

                gmj.Reply = reply;
                return gmj;
            }
            catch (Exception ex)
            {
                throw new Exception("Failed parsing xml in NotificationMsgJavaCall", ex);
            }

        }
        // var getNotificationMsgUrl 	= "getNotifMsgList.do";
        /// <summary>
        /// Update one or morge notidication messsages ans set them to read
        /// </summary>
        /// <param name="JSESSIONID"></param>
        /// <param name="isUpdate"></param>
        /// <param name="msgids"></param>
        /// <param name="listSize"></param>
        /// <param name="orderby"></param>
        /// <param name="desc"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        public static NotificationMsgJavaCallReply SetNotificationMsgRead(JNetBridge.Classes.JnetBridgeLoginUnit JSESSIONID, Boolean isUpdate, int[] msgids, int listSize, string orderby, Boolean desc, string user)
        {
            return (NotificationMsgJavaCallReply)new NotificationMsgJavaCall(JSESSIONID, isUpdate, msgids, "1024", listSize, orderby, desc, user).DoAction();
        }

        private NotificationMsgJavaCall(JNetBridge.Classes.JnetBridgeLoginUnit JSESSIONID, Boolean isUpdate, int[] msgId, string screenwidth, int listSize, string orderby, Boolean desc, string user)
            : base(JavaCall.baseURL + "getNotifMsgList.do",
            JSESSIONID,
                new string[] { "isUpdate", "msgId", "screenwidth", "listSize", "orderby", "desc", "user" },
                new string[] { isUpdate.ToString(), Utils.SeperatedListFromInt(msgId), screenwidth, listSize.ToString(), orderby, desc.ToString(), user })
        {

        }

        public static NotificationMsgJavaCallReply RequestNotificationMsg(JNetBridge.Classes.JnetBridgeLoginUnit JSESSIONID, string isUpdate, int listSize, string orderby, Boolean desc, string user)
        {
            return (NotificationMsgJavaCallReply)new NotificationMsgJavaCall(JSESSIONID, isUpdate, listSize, orderby, desc, user).DoAction();
        }

        private NotificationMsgJavaCall(JNetBridge.Classes.JnetBridgeLoginUnit JSESSIONID, string isUpdate, int listSize, string orderby, Boolean desc, string user)
            : base(JavaCall.baseURL + "getNotifMsgList.do",
            JSESSIONID,
                new string[] { "isUpdate", "listSize", "orderby", "desc", "user" },
                new string[] { isUpdate, listSize.ToString(), orderby, desc.ToString(), user })
        {

        }

        public static NotificationMsgJavaCallReply Delete(JNetBridge.Classes.JnetBridgeLoginUnit JSESSIONID, int messageId)
        {
            return (NotificationMsgJavaCallReply)new NotificationMsgJavaCall(JSESSIONID, messageId).DoAction();
        }

        private NotificationMsgJavaCall(JNetBridge.Classes.JnetBridgeLoginUnit JSESSIONID, int messageId)
            : base(JavaCall.baseURL + "deleteMessage.do",
            JSESSIONID,
                new string[] { "messageId",  "notif" },
                new string[] { messageId.ToString(), "true" })
        {

        }



    }
}
