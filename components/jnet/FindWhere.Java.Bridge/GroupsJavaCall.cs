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
    public class GroupsJavaCall : JavaCall
    {
/// <summary>
/// 
/// </summary>
/// <param name="JSESSIONID"></param>
/// <param name="groupName"></param>
/// <param name="members"></param>
/// <returns></returns>
        public static GroupsJavaCallReply AddGroup(JNetBridge.Classes.JnetBridgeLoginUnit JSESSIONID, string groupName, string[] members)
        {
            groupName = HttpUtility.UrlEncode(groupName);
            return (GroupsJavaCallReply)new GroupsJavaCall(JSESSIONID, action.add , groupName,0 , members).DoAction();
        }

        public static GroupsJavaCallReply EditGroup(JNetBridge.Classes.JnetBridgeLoginUnit JSESSIONID, int groupID, string[] members)
        {
            return (GroupsJavaCallReply)new GroupsJavaCall(JSESSIONID, action.edit, "", groupID , members).DoAction();
        }

        public static GroupsJavaCallReply DeleteGroup(JNetBridge.Classes.JnetBridgeLoginUnit JSESSIONID, int groupID)
        {
            return (GroupsJavaCallReply)new GroupsJavaCall(JSESSIONID, action.delete, "", groupID, null ).DoAction();
        }

        private GroupsJavaCall(JNetBridge.Classes.JnetBridgeLoginUnit JSESSIONID, action action, string groupName, int groupID, string[] members)
            : base(JavaCall.baseURL + "editTeam.do",
            JSESSIONID,
                new string[] { "type", "groupName", "member", "groupId" },
                new string[] { action.ToString(), groupName, Utils.SeperatedListFromString(members), groupID.ToString() })
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

                GroupsJavaCallReply gmj = new GroupsJavaCallReply();
                Utils.FillProperties(gmj, xdMain.SelectSingleNode("//main"));

                gmj.Reply = reply;
                return gmj;
            }
            catch (Exception ex)
            {
                throw new Exception("Failed parsing xml in GroupsJavaCall", ex);
            }

               
        }
        /// <summary>
        /// 
        /// </summary>
        public enum action { add, edit, delete }




       
    }
}
