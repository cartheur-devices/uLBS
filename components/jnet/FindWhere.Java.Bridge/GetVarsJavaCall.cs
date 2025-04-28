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
    public class GetVarsJavaCall : JavaCall
    {
        /// <summary>
        /// Call this static method to login. Use the data in the LoginJavaCallReply object in the GUI
        /// </summary>
        /// <returns></returns>
        public static GetVarsJavaCallReply  DoLogin(JNetBridge.Classes.JnetBridgeLoginUnit JSESSIONID)
        {
            return (GetVarsJavaCallReply)new GetVarsJavaCall(JSESSIONID).DoAction();
        }

        private GetVarsJavaCall(JNetBridge.Classes.JnetBridgeLoginUnit JSESSIONID)
            : base(JavaCall.baseURL + "jsvars.jsp",
                JSESSIONID,
                new string[] {},
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

            GetVarsJavaCallReply gr = new GetVarsJavaCallReply();
            gr.FOLProperties = new com.teleca.fleetonline.utils.FOLProperties();
            gr.FOLProperties.properties = new System.Collections.Hashtable();
            Utils.FillProperties(gr.FOLProperties , xdMain.SelectSingleNode("//main/FOLProperties"));
            gr.Reply = reply;
            return gr;
        }



       
    }
}
