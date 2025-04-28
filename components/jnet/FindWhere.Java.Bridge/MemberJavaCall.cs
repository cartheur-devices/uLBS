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
    /// 
    /// </summary>
    public class MemberJavaCall : JavaCall
    {
/// <summary>
/// 
/// </summary>
/// <param name="JSESSIONID"></param>
/// <param name="memberID"></param>
/// <param name="alias"></param>
/// <returns></returns>
        public static MemberJavaCallReply SaveMember(JNetBridge.Classes.JnetBridgeLoginUnit JSESSIONID, int memberID, string alias, int iconId)
        {
            alias = HttpUtility.UrlEncode(alias);
            return (MemberJavaCallReply)new MemberJavaCall(JSESSIONID, action.edit, memberID, alias, iconId).DoAction();
        }



        private MemberJavaCall(JNetBridge.Classes.JnetBridgeLoginUnit JSESSIONID, action action, int memberID, string alias, int iconId)
            : base(JavaCall.baseURL + "editFm.do",
            JSESSIONID,
                new string[] { "type", "memberId", "alias", "iconId"},
                new string[] { action.ToString(), memberID.ToString(), alias, iconId.ToString() })
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

                MemberJavaCallReply gmj = new MemberJavaCallReply();
                Utils.FillProperties(gmj,  xdMain.SelectSingleNode("//main"));

                gmj.Reply = reply;
                return gmj;
            }
            catch (Exception ex)
            {
                throw new Exception("Failed parsing xml in MemberJavaCall", ex);
            }

               
        }
        /// <summary>
        /// 
        /// </summary>
        public enum action { add, edit, delete }




       
    }
}
