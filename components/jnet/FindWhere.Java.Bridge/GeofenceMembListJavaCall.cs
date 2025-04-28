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
    public class GeofenceMembListJavaCall : JavaCall
    {
/// <summary>
/// 
/// </summary>
/// <param name="JSESSIONID"></param>
/// <returns></returns>
        public static GeofenceMembListJavaCallReply GetGeofenceMembList(JNetBridge.Classes.JnetBridgeLoginUnit JSESSIONID)
        {
            return (GeofenceMembListJavaCallReply)new GeofenceMembListJavaCall(JSESSIONID,GeofenceMemberListCMD.GETLIST ).DoAction();
        }

        private GeofenceMembListJavaCall(JNetBridge.Classes.JnetBridgeLoginUnit JSESSIONID, GeofenceMemberListCMD cmd)
            : base(JavaCall.baseURL + "getGeoFenceMembList.do",
            JSESSIONID,
                new string[] { "cmd" },
                new string[] { cmd.ToString() })
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
#if DEBUG
                //if (System.Environment.MachineName == "TEYDO11!")
                //{
                //    try
                //    {
                //        xdMain.Save(@"c:\Received.xml");
                //    }
                //    catch (Exception ex)
                //    { }
                //}
#endif

                XmlDocument workingXmlDoc;
                workingXmlDoc = new XmlDocument();

                GeofenceMembListJavaCallReply gmj = new GeofenceMembListJavaCallReply();
                Utils.FillProperties(gmj, xdMain.SelectSingleNode("//main"));

                gmj.Reply = reply;

                return gmj;

            }
            catch (Exception ex)
            {
                throw new Exception("Failed parsing xml in GeofenceMembListJavaCall", ex);
            }
        }





        public static GeofenceMembListJavaCallReply ADDMember(JNetBridge.Classes.JnetBridgeLoginUnit JSESSIONID, String[] fmIdLst, String fenceId, string startDate, string starthr, string startmin, string endDate, string endhr, string endmin, int fType, int alertOnce, Boolean deviceBased)
        {
            return (GeofenceMembListJavaCallReply)new GeofenceMembListJavaCall(JSESSIONID, GeofenceMemberListCMD.ADD , null, fenceId, fmIdLst, fenceId, startDate, endDate,startmin,starthr,endmin, endhr , fType, alertOnce, deviceBased, "", "").DoAction();
        }

        public static GeofenceMembListJavaCallReply ADDMember(JNetBridge.Classes.JnetBridgeLoginUnit JSESSIONID, String[] fmIdLst, String fenceId, string startDate, string starthr, string startmin, string endDate, string endhr, string endmin, int fType, int alertOnce, Boolean deviceBased, string enforcement, string schedule)
        {
            return (GeofenceMembListJavaCallReply)new GeofenceMembListJavaCall(JSESSIONID, GeofenceMemberListCMD.ADD, null, fenceId, fmIdLst, fenceId, startDate, endDate, startmin, starthr, endmin, endhr, fType, alertOnce, deviceBased, enforcement, schedule ).DoAction();
        }

        private GeofenceMembListJavaCall(JNetBridge.Classes.JnetBridgeLoginUnit JSESSIONID, GeofenceMemberListCMD cmd, string[] fenceidlst, string fenceid, String[] fmIdLst, String fenceId, string startDate, string endDate, string startmin, string starthr, string endmin, string endhr, int fType, int alertOnce, Boolean todevice, string enforcement, string schedule)
            : base(JavaCall.baseURL + "getGeoFenceMembList.do",
            JSESSIONID,
                new string[] { "cmd", "fenceidlst", "fenceid", "fmidlst", "fenceid", "startdate", "starthr", "startmin", "enddate", "endhr", "endmin", "ftype", "alertonce", "todevice", "enforcement", "schedule" },
                new string[] { cmd.ToString(), Utils.SeperatedListFromString(fenceidlst), fenceid, Utils.SeperatedListFromString(fmIdLst), fenceId, startDate , starthr, startmin , endDate , endhr , endmin, fType.ToString(), alertOnce.ToString(), todevice.ToString(), enforcement, schedule })
        {

        }

        public static GeofenceMembListJavaCallReply DeleteMember(JNetBridge.Classes.JnetBridgeLoginUnit JSESSIONID, string[] fenceidlst, bool deviceBased)
        {
            return (GeofenceMembListJavaCallReply)new GeofenceMembListJavaCall(JSESSIONID, GeofenceMemberListCMD.DEL , fenceidlst, "", null, null, null, null,null,null,null,null, 0, 0, deviceBased, "","").DoAction(); return null;
        }

        public enum GeofenceMemberListCMD { GETLIST, ADD, DEL }

    }
}

