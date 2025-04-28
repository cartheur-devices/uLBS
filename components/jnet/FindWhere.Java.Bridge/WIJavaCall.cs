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
    public class WIJavaCall : JavaCall
    {
/// <summary>
/// 
/// </summary>
/// <param name="JSESSIONID"></param>
/// <returns></returns>
        public static WIJavaCallReply GetConfig(JNetBridge.Classes.JnetBridgeLoginUnit JSESSIONID)
        {
            return (WIJavaCallReply)new WIJavaCall(JSESSIONID).DoAction();
        }



        private WIJavaCall(JNetBridge.Classes.JnetBridgeLoginUnit JSESSIONID)
            : base(JavaCall.baseURL + "getWIConfig.do",
            JSESSIONID,
                new string[] {},
                new string[] {})
        {

        }

           public static WIJavaCallReply GetConfig(JNetBridge.Classes.JnetBridgeLoginUnit JSESSIONID, string fmid)
        {
            return (WIJavaCallReply)new WIJavaCall(JSESSIONID, fmid).DoAction();
        }



        private WIJavaCall(JNetBridge.Classes.JnetBridgeLoginUnit JSESSIONID, string fmid)
             : base(JavaCall.baseURL + "getWIConfig.do",
            JSESSIONID,
                new string[] { "cmd", "fmid" },
                new string[] {"request", fmid })
        {

        }



        public static WIJavaCallReply SaveConfig(JNetBridge.Classes.JnetBridgeLoginUnit JSESSIONID, string fmid, int intInt, int natInt, int notMov, int speed, int maxSms)
        {
            return (WIJavaCallReply)new WIJavaCall(JSESSIONID, fmid, intInt, natInt, notMov, speed, maxSms).DoAction();
        }



        private WIJavaCall(JNetBridge.Classes.JnetBridgeLoginUnit JSESSIONID, string fmid, int intInt, int natInt, int notMov, int speed, int maxSms)
            : base(JavaCall.baseURL + "getWIConfig.do",
            JSESSIONID,
                new string[] { "cmd", "save", "fmid", "intInt", "natInt", "notMov", "speed", "maxSms" },
                new string[] { "request", "true", fmid, intInt.ToString(), natInt.ToString(), notMov.ToString(), speed.ToString(), maxSms.ToString()})
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
                string str = reply.Substring(pos1, (pos2 - pos1 + 7));
                xdMain.LoadXml(reply.Substring(pos1, (pos2 - pos1 + 7)));

                if (xdMain.SelectSingleNode("error") != null)
                {
                    throw new Exception(xdMain.SelectSingleNode("error").InnerText);
                }

                XmlDocument workingXmlDoc;
                workingXmlDoc = new XmlDocument();

                WIJavaCallReply gmj = new WIJavaCallReply();
                Utils.FillProperties(gmj,  xdMain.SelectSingleNode("//main"));

                gmj.Reply = reply;
                return gmj;
            }
            catch (Exception ex)
            {
                throw new Exception("Failed parsing xml in WIJavaCallReply", ex);
            }

               
        }
       


       
    }
}
