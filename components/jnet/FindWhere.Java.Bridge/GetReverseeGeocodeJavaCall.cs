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
    /// Used to get flat data from java
    /// </summary>
    public class GetReverseeGeocodeJavaCall : JavaCall
    {

        public static GetReverseGeoceodeJavaCallReply getAddress(JNetBridge.Classes.JnetBridgeLoginUnit JSESSIONID,int lbsId)
        {
            return (GetReverseGeoceodeJavaCallReply)new GetReverseeGeocodeJavaCall(JSESSIONID,lbsId).DoAction();
        }

        private GetReverseeGeocodeJavaCall(JNetBridge.Classes.JnetBridgeLoginUnit JSESSIONID, int lbsId)
            : base(JavaCall.baseURL + "getReverseGeocode.do",
            JSESSIONID,
                new string[] { "cmd", "lbsId" },
                new string[] { "getAddress", lbsId.ToString() })
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

                GetReverseGeoceodeJavaCallReply gmj = new GetReverseGeoceodeJavaCallReply();
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