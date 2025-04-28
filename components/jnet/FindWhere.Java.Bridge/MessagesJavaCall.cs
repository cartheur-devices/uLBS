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
    public class MessagesJavaCall : JavaCall
    {
/// <summary>
/// 
/// </summary>
/// <param name="JSESSIONID"></param>
/// <param name="User">All for All users</param>
/// <returns></returns>
        public static MessagesJavaCallReply getTextMessages(JNetBridge.Classes.JnetBridgeLoginUnit JSESSIONID, string User, int ListSize, string orderby, Boolean desc)
        {
            return (MessagesJavaCallReply)new MessagesJavaCall(JSESSIONID, User, ListSize, orderby, desc ).DoAction();
        }

        private MessagesJavaCall(JNetBridge.Classes.JnetBridgeLoginUnit JSESSIONID, string User, int ListSize, string orderby, Boolean desc)
            : base(JavaCall.baseURL + "getSmList.do",
            JSESSIONID,
                new string[] { "user", "listSize", "orderby", "desc" },
                new string[] { User, ListSize.ToString(), orderby, desc.ToString()  })
        {

        }

        /// <summary>
        /// Set message(s) as read
        /// </summary>
        /// <param name="JSESSIONID"></param>
        /// <param name="User"></param>
        /// <param name="msgId"></param>
        /// <param name="ListSize"></param>
        /// <param name="orderby"></param>
        /// <param name="desc"></param>
        /// <returns></returns>
        public static MessagesJavaCallReply getTextMessages(JNetBridge.Classes.JnetBridgeLoginUnit JSESSIONID, string User, int[] msgId, int ListSize, string orderby, Boolean desc)
        {
            return (MessagesJavaCallReply)new MessagesJavaCall(JSESSIONID, User, msgId, ListSize, orderby, desc).DoAction();
        }

        private MessagesJavaCall(JNetBridge.Classes.JnetBridgeLoginUnit JSESSIONID, string User, int[] msgId, int ListSize, string orderby, Boolean desc)
            : base(JavaCall.baseURL + "getSmList.do",
            JSESSIONID,
                new string[] { "user", "msgId", "listSize", "orderby", "desc" },
                new string[] { User, Utils.SeperatedListFromInt (msgId), ListSize.ToString(), orderby, desc.ToString() })
        {

        }

        public static MessagesJavaCallReply DeleteTextMessage(JNetBridge.Classes.JnetBridgeLoginUnit JSESSIONID,  int messageId)
        {
            return (MessagesJavaCallReply)new MessagesJavaCall(JSESSIONID, messageId).DoAction();
        }

        private MessagesJavaCall(JNetBridge.Classes.JnetBridgeLoginUnit JSESSIONID, int messageId)
            : base(JavaCall.baseURL + "deleteMessage.do",
            JSESSIONID,
                new string[] { "messageId" },
                new string[] { messageId.ToString() })
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

                MessagesJavaCallReply gmj = new MessagesJavaCallReply();
                gmj.StartTime = DateTime.Now;
                Utils.FillProperties(gmj, xdMain.SelectSingleNode("//main"));
                gmj.EndTime = DateTime.Now;
                gmj.Reply = reply;
                return gmj;
            }
            catch (Exception ex)
            {
                throw new Exception("Failed parsing xml in MessageJavaCall", ex);
            }

            
          
          
                     

          

     
        }



       
    }
}
