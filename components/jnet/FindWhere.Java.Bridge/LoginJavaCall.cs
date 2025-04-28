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
    /// Passes the login to the system.
    /// </summary>
    public class LoginJavaCall : JavaCall
    {
        /// <summary>
        /// Call this static method to login. Use the data in the LoginJavaCallReply object in the GUI.
        /// </summary>
        /// <returns></returns>
        public static LoginJavaCallReply DoLogin(Classes.JnetBridgeLoginUnit loginUnit, string userName, string passWord)
        {
            return (LoginJavaCallReply)new LoginJavaCall(loginUnit, userName, passWord).DoAction();
        }

        private LoginJavaCall(Classes.JnetBridgeLoginUnit loginUnit, string userName, string passWord)
            : base(JavaCall.baseURL + "logon.do",
                loginUnit,
                new string[] { "username", "password" },
                new string[] { userName, passWord }) {  }

        /// <summary>
        /// This method does the actual conversion from serialized data to the deserialized LoginJavaCallReply object.
        /// </summary>
        /// <param name="reply">The reply.</param>
        /// <param name="JSESSIONID">The JSESSIONID.</param>
        /// <returns>Java Call Reply</returns>
        protected override JavaCallReply ParseServerReply(string reply, Cookie JSESSIONID)
        {
            // Main XML Document
            XmlDocument xdMain = new XmlDocument();
            int pos1 = reply.IndexOf("<?xml");
            int pos2 = reply.IndexOf("</main>");

            xdMain.LoadXml(reply.Substring(pos1, (pos2 - pos1 + 7))); ;

            //if (System.Environment.MachineName == "TEYDO11")
            //{
                //xdMain.Save(@"c:\ReceivedXml.xml");
            //}

            if (xdMain.SelectSingleNode("error") != null)
            {
                throw new Exception(xdMain.SelectSingleNode("error").InnerText);
            }

            LoginJavaCallReply ljcr = new LoginJavaCallReply(JSESSIONID);
            Utils.FillProperties(ljcr, xdMain.SelectSingleNode("//main"));
            ljcr.Reply = reply;

            #region Commented Out
            //if (ljcr.Global_Error != null)
            //{
            //    if (ljcr.Global_Error != "")
            //    {
            //        throw new Exception(ljcr.Global_Error);
            //    }
            //}   

        //    XmlDocument workingXmlDoc;
        //    workingXmlDoc = new XmlDocument();


        //    #region GroupsAndMembersData

        //    List<GroupsAndMembersData> list = new List<GroupsAndMembersData>();

        //    XmlNode groupsAndMemberDataNode = xdMain.SelectSingleNode("//groupsAndMembers");

        //    list = GroupsAndMembersData.FromNode(groupsAndMemberDataNode);

        //    #region old
        //    //foreach (XmlNode groupsAndMemberDataNode in nodeList_root)
        //    //{

        //    //    GroupsAndMembersData gmd = new GroupsAndMembersData();
        //    //    gmd.Memberlists = new Dictionary<string, object>();
        //    //    XmlNodeList nodeListMemberData = groupsAndMemberDataNode.SelectNodes("//object[@class='com.teleca.fleetonline.repository.MemberData']");

        //    //    foreach (XmlNode book in nodeListMemberData)
        //    //    {
        //    //        // Dit zijn de xml's met memberdata...
        //    //        MemberData md = new MemberData();

        //    //        XmlNodeList nodeList2 = book.SelectNodes("void");
        //    //        foreach (XmlNode keysNode in nodeList2)
        //    //        {
        //    //            string propertyName = keysNode.Attributes["property"].Value.ToString();
        //    //            string propertyValue = keysNode.InnerText;

        //    //            propertyName = propertyName.Substring(0, 1).ToUpper() + propertyName.Substring(1);

        //    //            //PropertyInfo p = md.GetType().GetProperty(propertyName);
        //    //            //string ttype = md.GetType().GetProperty(propertyName).PropertyType.FullName.ToString();

        //    //            //md = setValueFunction(md, propertyName, propertyValue);

        //    //            switch (md.GetType().GetProperty(propertyName).PropertyType.FullName)
        //    //            {
        //    //                case "System.Boolean":
        //    //                    md.GetType().GetProperty(propertyName).SetValue(md, Boolean.Parse(propertyValue), null);
        //    //                    break;

        //    //                case "System.String":
        //    //                    md.GetType().GetProperty(propertyName).SetValue(md, propertyValue, null);
        //    //                    break;

        //    //                case "System.Int32":
        //    //                    md.GetType().GetProperty(propertyName).SetValue(md, int.Parse(propertyValue), null);
        //    //                    break;

        //    //                default:
        //    //                    throw new NotImplementedException("Property:" + propertyName + " unknown");

        //    //            }
        //    //        }

        //    //        gmd.Memberlists.Add(md.Alias, md);
        //    //    }
        //    //    list.Add(gmd);
        //    //}
        //    #endregion

        //    #endregion

        //    #region UserData
        //    UserData usrData = new UserData();
        //    XmlNode userNode = xdMain.SelectSingleNode("//fleetonline_user").FirstChild;

        //    Utils.FillProperties(usrData, userNode);


        //    #region old
        //    //foreach (XmlNode keysNode in nodeListUserData)
        //    //{
        //    //    string propertyName = keysNode.Attributes["property"].Value.ToString();
        //    //    string propertyValue = keysNode.InnerText;

        //    //    propertyName = propertyName.Substring(0, 1).ToUpper() + propertyName.Substring(1);

        //    //    switch (usrData.GetType().GetProperty(propertyName).PropertyType.FullName)
        //    //    {
        //    //        case "System.Boolean":
        //    //            usrData.GetType().GetProperty(propertyName).SetValue(usrData, Boolean.Parse(propertyValue), null);
        //    //            break;

        //    //        case "System.String":
        //    //            usrData.GetType().GetProperty(propertyName).SetValue(usrData, propertyValue, null);
        //    //            break;

        //    //        case "System.Int32":
        //    //            usrData.GetType().GetProperty(propertyName).SetValue(usrData, int.Parse(propertyValue), null);
        //    //            break;

        //    //        default:
        //    //            throw new NotImplementedException("Property:" + propertyName + " unknown");

        //    //    }
        //    //}
        //    #endregion

        //    #endregion

        //    #region mainProperties
        //    int level = 0;
        //    Boolean gsm = false;
        //    Boolean trimtrac = false;
        //    Boolean tt15 = false;
        //    Boolean ocellus = false;
        //    Boolean enfora = false;
        //    Boolean wi = false;
        //    Boolean TT_USER = false;
        //    string map_implementation = "";
        //    string REMOTE_ADDR_FOR_PORTA="";
        //    string OC4J_UNPARSED_URI = "";
        //    string initial_map_data ="";

        //    XmlNodeList nodeListProperties = xdMain.SelectNodes("main//property");
        //    foreach (XmlNode keysNode in nodeListProperties)
        //    {
        //        string propertyName = keysNode.Attributes["name"].Value.ToString();
        //        string propertyValue = keysNode.Attributes["value"].Value.ToString();

        //        switch (propertyName)
        //        {
        //            case "gsm":
        //                gsm = bool.Parse(propertyValue);
        //                break;
        //            case "trimtrac":
        //                trimtrac = bool.Parse(propertyValue);
        //                break;
        //            case "tt15":
        //                tt15 = bool.Parse(propertyValue);
        //                break;
        //            case "ocellus":
        //                ocellus = bool.Parse(propertyValue);
        //                break;
        //            case "enfora":
        //                enfora = bool.Parse(propertyValue);
        //                break;
        //            case "wi":
        //                wi = bool.Parse(propertyValue);
        //                break;
        //            case "map_implementation":
        //                map_implementation = propertyValue;
        //                break;
        //            case "USER_LEVEL":
        //                level = int.Parse(propertyValue);
        //                break;
        //            case "TT_USER":
        //                TT_USER = bool.Parse(propertyValue);
        //                break;
        //            case "REMOTE_ADDR_FOR_PORTAL":
        //                REMOTE_ADDR_FOR_PORTA = propertyValue;
        //                break;
        //            case "OC4J_UNPARSED_URI":
        //                OC4J_UNPARSED_URI = propertyValue;
        //                break;
        //            case "initial_map_data":
        //                initial_map_data = propertyValue;
        //                break;
        //            default:
        //                throw new NotImplementedException("Property:" + propertyName + " unknown");
        //        }
        //    }
        //    #endregion


        //    return new LoginJavaCallReply(JSESSIONID, TT_USER, level, list, usrData, reply, gsm, trimtrac, tt15, ocellus, enfora, wi, map_implementation, initial_map_data);
            //
            #endregion

            return ljcr;
        }      
    }
}
