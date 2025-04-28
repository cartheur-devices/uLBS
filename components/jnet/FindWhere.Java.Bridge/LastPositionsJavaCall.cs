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
using System.Collections;

namespace JNetBridge
{
    /// <summary>
    /// 
    /// </summary>
    public class LastPositionJavaCall : JavaCall
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="JSESSIONID"></param>
        /// <param name="member"></param>
        /// <param name="targetPosition"></param>
        /// <param name="targetMember"></param>
        /// <param name="location"></param>
        /// <param name="numPositions"></param>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <param name="positionId"></param>
        /// <param name="type"></param>
        /// <param name="mapWidth"></param>
        /// <param name="mapHeight"></param>
        /// <param name="zoomLevel"></param>
        /// <param name="targetName"></param>
        /// <param name="positionType"></param>
        /// <param name="lbs"></param>
        /// <returns></returns>
        public static LastPositionJavaCallReply GetPositions(JNetBridge.Classes.JnetBridgeLoginUnit JSESSIONID, string[] member, string targetPosition, string targetMember, string location,
         int numPositions, DateTime fromDate, DateTime toDate, string positionId, string type, int mapWidth, int mapHeight, string zoomLevel, string targetName, int positionType, string lbs)
        {
            return (LastPositionJavaCallReply)new LastPositionJavaCall(JSESSIONID, member, targetPosition, targetMember, location,
          numPositions, fromDate, toDate, positionId, type, mapWidth, mapHeight, zoomLevel, targetName, positionType, lbs).DoAction();
        }

        private LastPositionJavaCall(JNetBridge.Classes.JnetBridgeLoginUnit JSESSIONID, string[] member, string targetPosition, string targetMember, string location,
        int numPositions, DateTime fromDate, DateTime toDate, string positionId, string type, int mapWidth, int mapHeight, string zoomLevel, string targetName, int positionType, string lbs)
            : base(JavaCall.baseURL + "positioning.do",
            JSESSIONID,
                new string[] { "member",  "targetPosition",  "targetMember",  "location",
          "numPositions",  "fromDate",  "toDate",  "fromHour",  "toHour",  "fromMinute",  "toMinute",
          "positionId",  "type",  "mapWidth",  "mapHeight",  "zoomLevel",  "targetName",  "positionType",  "lbs"},
                new string[] { Utils.SeperatedListFromString(member),  targetPosition,  targetMember,  location,
          numPositions.ToString(),  fromDate.ToString("dd-MM-yyyy"),  toDate.ToString("dd-MM-yyyy"),  fromDate.ToString("hh"),  toDate.ToString("hh"),  fromDate.ToString("mm"),  toDate.ToString("mm"),
          positionId,  type,  mapWidth.ToString(),  mapHeight.ToString(),  zoomLevel,  targetName,  positionType.ToString(),  lbs})
        {

        }


        public static LastPositionJavaCallReply GetPositions(JNetBridge.Classes.JnetBridgeLoginUnit JSESSIONID, string member, int numPositions, string startDate, string endDate)
        {
            return (LastPositionJavaCallReply)new LastPositionJavaCall(JSESSIONID,  member,   numPositions,  startDate,  endDate).DoAction();
        }

        private LastPositionJavaCall(JNetBridge.Classes.JnetBridgeLoginUnit JSESSIONID, string member, int numPositions, string startDate, string endDate)
            : base(JavaCall.baseURL + "positioning.do",
            JSESSIONID,
                new string[] {"type", "targetName","mapWidth","mapHeight","member","numPositions","startDate","endDate" },
                new string[] {postionType.traceHistory.ToString(),"TEST","640","480",member ,numPositions.ToString(),startDate, endDate  })
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

                LastPositionJavaCallReply gmj = new LastPositionJavaCallReply();
                Utils.FillProperties(gmj, xdMain.SelectSingleNode("//main"));

                gmj.Reply = reply;
                return gmj;
            }
            catch (Exception ex)
            {
                throw new Exception("Failed parsing xml in LastPositionJavaCall", ex);
            }

        }


        //private LastPositionJavaCall(Cookie JSESSIONID, string cmd, string eventType, int sendByEmail, int sendBySms, string emailList, String smsList, String memberList, string fmId, int alertOnceStr)
        //    : base(JavaCall.baseURL + "notificationAction.do",
        //    JSESSIONID,
        //        new string[] { "cmd", "type", "byemail", "bysms", "emaillist", "smslist", "memberlist", "member", "alertonce" },
        //        new string[] { cmd, eventType, sendByEmail.ToString(), sendBySms.ToString(), emailList, smsList, memberList, fmId, alertOnceStr.ToString() })
        //{

        //}


        public static LastPositionJavaCallReply GetLastKnownPositions(JNetBridge.Classes.JnetBridgeLoginUnit JSESSIONID, postionType type, string[] members,Boolean GSM)
        {
            ArrayList alParamNames = new ArrayList();
            ArrayList alParamValues = new ArrayList();

            alParamNames.Add("type");
            alParamValues.Add(type.ToString());

            //todo: remove width/height from ujava code
            alParamNames.Add("mapWidth");
            alParamValues.Add("640");

            alParamNames.Add("mapHeight");
            alParamValues.Add("480");

            ArrayList alMembers = new ArrayList();
            

            foreach (string member in members)
            {
                if (!alMembers.Contains(member)) alMembers.Add(member);
            }

            int i = 1;
            for (int m = 0; m < alMembers.Count; m++ )
            {
                alParamNames.Add("member");
                alParamValues.Add(alMembers[m]);
            }

            if (GSM == true)
            {
                alParamNames.Add("lbs");
                alParamValues.Add("1");
            }

            string[] paramNames = (string[])alParamNames.ToArray(typeof(string));
            string[] paramValues = (string[])alParamValues.ToArray(typeof(string));




            return (LastPositionJavaCallReply)new LastPositionJavaCall("positioning.do", JSESSIONID, paramNames, paramValues ).DoAction();
        }

        private LastPositionJavaCall(string functiondo, JNetBridge.Classes.JnetBridgeLoginUnit JSESSIONID, string[] paramNames, string[] paramValues)
            : base(JavaCall.baseURL + functiondo,
            JSESSIONID,paramNames, paramValues )
        {

        }


        private LastPositionJavaCall(string functiondo, JNetBridge.Classes.JnetBridgeLoginUnit JSESSIONID, postionType type, int mapWidth, int mapHeight, string[] member)
            : base(JavaCall.baseURL + functiondo,
            JSESSIONID,
                new string[] { "type", "mapWidth", "mapHeight", "member" },
                new string[] { type.ToString(), mapWidth.ToString(), mapHeight.ToString(), Utils.SeperatedListFromString(member) })
        {

        }



        public static LastPositionJavaCallReply RequestPosition(JNetBridge.Classes.JnetBridgeLoginUnit JSESSIONID, int positionID )
        {
            int mapWidth=1024;
            int mapHeight = 800;
            return (LastPositionJavaCallReply)new LastPositionJavaCall("positioning.do", JSESSIONID, positionID, mapWidth, mapHeight).DoAction();
        }



        private LastPositionJavaCall(string functiondo, JNetBridge.Classes.JnetBridgeLoginUnit JSESSIONID, int positionID, int mapWidth, int mapHeight)
            : base(JavaCall.baseURL + functiondo,
            JSESSIONID,
                new string[] { "type", "positionId", "mapWidth", "mapHeight" },
                new string[] { postionType.previous.ToString() , positionID.ToString(), mapWidth.ToString(), mapHeight.ToString() })
        {

        }

        /// <summary>
        /// 
        /// </summary>
        public enum postionType { historical, traceHistory, previous, position }

    }
}
