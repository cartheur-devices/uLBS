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
    /// 
    /// </summary>
    public class ListPositionsJavaCall : JavaCall
    {
        /// <summary>
        /// Call this static method to login. Use the data in the LoginJavaCallReply object in the GUI
        /// </summary>
        /// <returns></returns>
        public static LastPositionJavaCallReply GetPositions(JNetBridge.Classes.JnetBridgeLoginUnit JSESSIONID, string[] member, string targetPosition, string targetMember, string location,
         int numPositions, DateTime fromDate, DateTime toDate, string positionId, string type, int mapWidth, int mapHeight, string zoomLevel, string targetName, int positionType, string lbs)
        {
            return (LastPositionJavaCallReply)new ListPositionsJavaCall(JSESSIONID, member, targetPosition, targetMember, location,
          numPositions, fromDate, toDate, positionId, type, mapWidth, mapHeight, zoomLevel, targetName, positionType, lbs).DoAction();
        }

        private ListPositionsJavaCall(JNetBridge.Classes.JnetBridgeLoginUnit JSESSIONID, string[] member, string targetPosition, string targetMember, string location,
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
        
        /// <summary>
        /// This method does the actual conversion from serialized data to the deserialized LoginJavaCallReply object.
        /// </summary>
        /// <param name="reply">Stream from the Server</param>
        /// <returns>A call reply.</returns>
        protected override JavaCallReply ParseServerReply(string reply, Cookie JSESSIONID)
        {
            try
            {
                //Serializer
                //object result = null;
                //result = reply.ToString();

                //if (result != null)
                //{
                //    XmlDocument document = new XmlDocument();
                //    document.LoadXml(result.ToString());
                //    document.Clone();
                //    //ParseXmlFields(document);
                //}

                XmlDocument document2 = new XmlDocument();
                int pos1 = reply.IndexOf("<?xml");
                int pos2 = reply.IndexOf("</main>");
                document2.LoadXml(reply.Substring(pos1, (pos2 - pos1+7)));

                if (document2.SelectSingleNode("error") != null)
                {
                    throw new Exception(document2.SelectSingleNode("error").InnerText);
                }

                //XmlDocument workingXmlDoc = new XmlDocument();

                ListPositionJavaCallReply gmj = new ListPositionJavaCallReply();
                gmj.StartTime = DateTime.Now;
                Utils.FillProperties(gmj, document2.SelectSingleNode("//main[position() = 1]"));
                gmj.EndTime = DateTime.Now;

                gmj.Reply = reply;
                return gmj;
            }
            catch (Exception ex)
            {
                throw new Exception("Failed parsing xml in ListPositionsJavaCall", ex);
            }

        }

        protected void ParseXmlFields(XmlDocument input)
        {
            XmlNodeList nodelist = input.SelectNodes("Candles/Candle");
        }


        private ListPositionsJavaCall(JNetBridge.Classes.JnetBridgeLoginUnit JSESSIONID, string cmd, string eventType, int sendByEmail, int sendBySms, string emailList, String smsList, String memberList, string fmId, int alertOnceStr)
            : base(JavaCall.baseURL + "notificationAction.do",
            JSESSIONID,
                new string[] { "cmd", "type", "byemail", "bysms", "emaillist", "smslist", "memberlist", "member", "alertonce" },
                new string[] { cmd, eventType, sendByEmail.ToString(), sendBySms.ToString(), emailList, smsList, memberList, fmId, alertOnceStr.ToString() })
        {

        }



        public static ListPositionJavaCallReply RequestPositionList(JNetBridge.Classes.JnetBridgeLoginUnit JSESSIONID, int listSize, string orderby, Boolean  desc, string user)
        {
            // Desc is the only list with stupid sorting :(
            //desc = !desc;

            return (ListPositionJavaCallReply)new ListPositionsJavaCall("getPositionsList.do", JSESSIONID, listSize, orderby, desc, user).DoAction();
        }

        private ListPositionsJavaCall(string functiondo, JNetBridge.Classes.JnetBridgeLoginUnit JSESSIONID, int listSize, string orderby, Boolean  desc, string user)
            : base(JavaCall.baseURL + functiondo,
            JSESSIONID,
                new string[] { "listSize", "orderby", "desc", "user" },
                new string[] { listSize.ToString(), orderby, desc.ToString(), user})
        {

        }


        
    }
}
