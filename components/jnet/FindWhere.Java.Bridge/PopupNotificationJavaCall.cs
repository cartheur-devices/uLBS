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
    public class PopupNotificationJavaCall : JavaCall
    {
/// <summary>
/// 
/// </summary>
/// <param name="JSESSIONID"></param>
/// <param name="User"></param>
/// <returns></returns>
        public static PopupNotificationJavaCallReply GetNotifications(JNetBridge.Classes.JnetBridgeLoginUnit JSESSIONID, Boolean  notification, string deviceType)
        {
            return (PopupNotificationJavaCallReply)new PopupNotificationJavaCall(JSESSIONID, notification, deviceType ).DoAction();
        }

        private PopupNotificationJavaCall(JNetBridge.Classes.JnetBridgeLoginUnit JSESSIONID, Boolean notification, string deviceType)
            : base(JavaCall.baseURL + "getContacts.do",
            JSESSIONID,
                new string[] { "notification", "deviceType"},
                new string[] { notification.ToString(), deviceType })
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
                xdMain.LoadXml(reply.Substring(pos1, (pos2 - pos1+7)));

                if (xdMain.SelectSingleNode("error") != null)
                {
                    throw new Exception(xdMain.SelectSingleNode("error").InnerText);
                }

                XmlDocument workingXmlDoc;
                workingXmlDoc = new XmlDocument();

                PopupNotificationJavaCallReply gmj = new PopupNotificationJavaCallReply();
                Utils.FillProperties(gmj, xdMain.SelectSingleNode("//main"));

                gmj.Reply = reply;
                return gmj;
            }
            catch (Exception ex)
            {
                throw new Exception("Failed parsing xml in PopupNotificationJavaCall", ex);
            }









        }


      

    }
}
