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
using System.Web;

namespace JNetBridge
{
    /// <summary>
    /// Used to get flat data from java
    /// </summary>
    public class StorePasswordAndTimezoneJavaCal : JavaCall
    {

        public static StorePasswordAndTimezoneJavaCalReply Store(JNetBridge.Classes.JnetBridgeLoginUnit JSESSIONID, string password, string timezone)
        {
            return (StorePasswordAndTimezoneJavaCalReply)new StorePasswordAndTimezoneJavaCal(JSESSIONID, password, timezone ).DoAction();
        }

        private StorePasswordAndTimezoneJavaCal(JNetBridge.Classes.JnetBridgeLoginUnit JSESSIONID, string password, string timezone)
            : base(JavaCall.baseURL + "setup.do",
            JSESSIONID,
                new string[] { "cmd", "password", "timezone" },
                new string[] { "save", password, HttpUtility.UrlEncode(timezone) })
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

                XmlDocument workingXmlDoc;
                workingXmlDoc = new XmlDocument();

                StorePasswordAndTimezoneJavaCalReply gmj = new StorePasswordAndTimezoneJavaCalReply();
                Utils.FillProperties(gmj, xdMain.SelectSingleNode("//main"));

                gmj.Reply = reply;

                return gmj;

            }
            catch (Exception ex)
            {
                throw new Exception("Failed parsing xml in GeofenceMembListJavaCall", ex);
            }
        }
    }
}