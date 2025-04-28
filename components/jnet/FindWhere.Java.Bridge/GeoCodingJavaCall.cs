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
    /// The call to Java for Geocoding.
    /// </summary>
    public class GeoCodingJavaCall : JavaCall
    {
        /// <summary>
        /// To use the Geoswitch or not.
        /// </summary>
        public enum GeoSwitch { on, off }
        /// <summary>
        /// Changes the specified JSESSIONID.
        /// </summary>
        /// <param name="JSESSIONID">The JSESSIONID.</param>
        /// <param name="swtch">The SWTCH.</param>
        /// <param name="msisdn">The msisdn.</param>
        /// <returns></returns>
        public static GeoCodingJavaCallReply Change(JNetBridge.Classes.JnetBridgeLoginUnit JSESSIONID, GeoSwitch  swtch, string[] msisdn)
        {
            return (GeoCodingJavaCallReply)new GeoCodingJavaCall(JSESSIONID, swtch, msisdn ).DoAction();
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="GeoCodingJavaCall"/> class.
        /// </summary>
        /// <param name="JSESSIONID">The JSESSIONID.</param>
        /// <param name="swtch">The SWTCH.</param>
        /// <param name="msisdn">The msisdn.</param>
        private GeoCodingJavaCall(JNetBridge.Classes.JnetBridgeLoginUnit JSESSIONID, GeoSwitch swtch, string[] msisdn)
            : base(JavaCall.baseURL + "handleTTCommandAction.do",
            JSESSIONID,
                new string[] { "cmd", "switch", "msisdn"},
                new string[] { "geocoding", swtch.ToString(), Utils.SeperatedListFromString(msisdn) })  { }

        /// <summary>
        /// This method does the actual conversion from serialized data to the deserialized LoginJavaCallReply object
        /// </summary>
        /// <param name="reply">The reply.</param>
        /// <param name="JSESSIONID">The JSESSIONID.</param>
        /// <returns>Xml Message Stream</returns>
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

                GeoCodingJavaCallReply gmj = new GeoCodingJavaCallReply();
                Utils.FillProperties(gmj, xdMain.SelectSingleNode("//main"));

                gmj.Reply = reply;
                return gmj;
            }
            catch (Exception ex)
            {
                throw new Exception("Failed parsing XML in GeoCodingJavaCall", ex);
            }      
        }
    }
}
