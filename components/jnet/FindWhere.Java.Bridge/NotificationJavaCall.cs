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
    public class NotificationJavaCall : JavaCall
    {
/// <summary>
/// 
/// </summary>
/// <param name="JSESSIONID"></param>
/// <param name="User"></param>
/// <returns></returns>
        public static NotificationJavaCallReply GetNotifications(JNetBridge.Classes.JnetBridgeLoginUnit JSESSIONID, int listSize, string orderby, Boolean desc, string user)
        {
            return (NotificationJavaCallReply)new NotificationJavaCall(JSESSIONID, user, listSize, orderby, desc).DoAction();
        }

        private NotificationJavaCall(JNetBridge.Classes.JnetBridgeLoginUnit JSESSIONID, string User, int listSize, string orderby, Boolean desc)
            : base(JavaCall.baseURL + "notificationAction.do",
            JSESSIONID,
                new string[] { "user", "listSize", "orderby", "desc" },
                new string[] { User, listSize.ToString(), orderby, desc.ToString() })
        {

        }

        /// <summary>
        /// This method does the actual conversion from serialized data to the deserialized LoginJavaCallReply object
        /// </summary>
        /// <param name="reply"></param>
        /// <returns></returns>
        protected override JavaCallReply ParseServerReply(string reply, System.Net.Cookie  JSESSIONID)
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

                NotificationJavaCallReply gmj = new NotificationJavaCallReply();
                Utils.FillProperties(gmj, xdMain.SelectSingleNode("//main"));

                gmj.Reply = reply;
                return gmj;
            }
            catch (Exception ex)
            {
                throw new Exception("Failed parsing xml in MessageJavaCall", ex);
            }









        }


        private NotificationJavaCall(JNetBridge.Classes.JnetBridgeLoginUnit JSESSIONID, string cmd, string eventType, int sendByEmail, int sendBySms, string emailList, string smsList, string memberList, string fmId, int alertOnceStr)
            : base(JavaCall.baseURL + "notificationAction.do",
            JSESSIONID,
                new string[] { "cmd", "type", "byemail", "bysms", "emaillist", "smslist", "memberlist", "member", "alertonce" },
                new string[] { cmd, eventType, sendByEmail.ToString(), sendBySms.ToString(), emailList, smsList, memberList, fmId, alertOnceStr.ToString() })
        {

        }




        public static NotificationJavaCallReply StoreNotification(JNetBridge.Classes.JnetBridgeLoginUnit JSESSIONID, string eventType, int[] email, int[] sms, string[] member, string fmId, int alertOnceStr)
        {
            int sendByEmail = 0;
            if (email != null && email.Length > 0) sendByEmail = 1;
            
            
            int sendBySms = 1;
            if (sms != null && sms.Length > 0) sendBySms = 1;

            string emailList = Utils.SemiColonSeperatedListFromInt(email);
            string smsList = Utils.SemiColonSeperatedListFromInt(sms);
            string memberList = Utils.SemiColonSeperatedListFromString(member);
            return (NotificationJavaCallReply)new NotificationJavaCall(JSESSIONID, "STORE" , eventType, sendByEmail, sendBySms, emailList, smsList, memberList, fmId, alertOnceStr).DoAction();
        }


    }
}
