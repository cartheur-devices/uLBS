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
    public class TemplatesJavaCall : JavaCall
    {
        /// <summary>
        /// Call this static method to login. Use the data in the LoginJavaCallReply object in the GUI
        /// </summary>
        /// <returns></returns>
        public static TemplatesJavaCallReply Get(JNetBridge.Classes.JnetBridgeLoginUnit JSESSIONID)
        {
            return (TemplatesJavaCallReply)new TemplatesJavaCall(JSESSIONID).DoAction();
        }

        private TemplatesJavaCall(JNetBridge.Classes.JnetBridgeLoginUnit JSESSIONID)
            : base(JavaCall.baseURL + "getTemplateMessages.do",JSESSIONID,
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

            TemplatesJavaCallReply gr = new TemplatesJavaCallReply();

            Utils.FillProperties(gr, xdMain.SelectSingleNode("//main"));
            gr.Reply = reply;
            return gr;
        }



       
    }
}
