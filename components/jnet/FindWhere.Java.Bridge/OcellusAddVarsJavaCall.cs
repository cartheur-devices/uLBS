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
    public class OcellusAddVarsJavaCall : JavaCall
    {
/// <summary>
/// iFind 3000 reports
/// </summary>
/// <param name="JSESSIONID"></param>
/// <param name="ListSize"></param>
/// <param name="OrderBY"></param>
/// <param name="sortDesc">True for descending</param>
/// <param name="User"></param>
/// <returns></returns>
        public static OcellusAddVarsJavaCallReply RequestOcellusAddVars(JNetBridge.Classes.JnetBridgeLoginUnit JSESSIONID,  int ListSize, string OrderBY, Boolean sortDesc, string[] User)
        {
            //tofo: fix the java code
            //sortDesc = !sortDesc;
            return (OcellusAddVarsJavaCallReply)new OcellusAddVarsJavaCall( JSESSIONID,  640,  ListSize,  OrderBY,sortDesc,  User).DoAction();
        }

        private OcellusAddVarsJavaCall(JNetBridge.Classes.JnetBridgeLoginUnit JSESSIONID, int ScreenWidth, int ListSize, string OrderBY, Boolean sortDesc, string[] User)
            : base(JavaCall.baseURL + "getOcellusAddVars.do",
            JSESSIONID,
                new string[] { "screenwidth", "listSize", "orderby", "desc", "user" },
                new string[] { ScreenWidth.ToString(), ListSize.ToString(), OrderBY,sortDesc.ToString(), Utils.SeperatedListFromString(User) })
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

                OcellusAddVarsJavaCallReply gmj = new OcellusAddVarsJavaCallReply();
                Utils.FillProperties(gmj, xdMain.SelectSingleNode("//main"));

                gmj.Reply = reply;

                return gmj;
            }
            catch (Exception ex)
            {
                throw new Exception("Failed parsing xml in OcellusAddVarsJavaCall", ex);
            }
         
        }




    }
}
