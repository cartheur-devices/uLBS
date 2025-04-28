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
    /// Used to get flat data from java
    /// </summary>
    public class InterfaceJavaCall : JavaCall
    {

        public static InterfaceJavaCallReply Positioningdo(JNetBridge.Classes.JnetBridgeLoginUnit JSESSIONID, string type, int? positionId, int mapWidth, int mapHeight, string targetName, string member, int? numPositions, string startDate, string endDate)
        {
            return (InterfaceJavaCallReply)new InterfaceJavaCall(JSESSIONID, "positioning.do", type, positionId, mapWidth, mapHeight, targetName, member, numPositions, startDate, endDate).DoAction();
        }

        private InterfaceJavaCall(JNetBridge.Classes.JnetBridgeLoginUnit JSESSIONID, string url, string type, int? positionId, int mapWidth, int mapHeight, string targetName, string member, int? numPositions, string startDate, string endDate)
            : base(JavaCall.baseURL + url,
            JSESSIONID,
                new string[] { "type", "positionId", "mapWidth", "mapHeight", "targetName", "member", "numPositions", "startDate", "endDate" },
                new string[] { type, positionId.ToString(), mapWidth.ToString(), mapHeight.ToString(), targetName, member, numPositions.ToString(), startDate, endDate })
        {

        }



        public static InterfaceJavaCallReply getReply(JNetBridge.Classes.JnetBridgeLoginUnit JSESSIONID, string url)
        {
            string[] paramNames = new string[0];
            string[] paramValues = new string[0];

            return (InterfaceJavaCallReply)new InterfaceJavaCall(JSESSIONID, url, paramNames, paramValues).DoAction();
        }

        public static InterfaceJavaCallReply getReply(JNetBridge.Classes.JnetBridgeLoginUnit JSESSIONID, string url, string[] paramNames, string[] paramValues)
        {
            return (InterfaceJavaCallReply)new InterfaceJavaCall(JSESSIONID, url, paramNames, paramValues).DoAction();
        }

        private InterfaceJavaCall(JNetBridge.Classes.JnetBridgeLoginUnit JSESSIONID, string url, string[] paramNames, string[] paramValues)
            : base(JavaCall.baseURL + url,
            JSESSIONID, paramNames, paramValues)
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

                InterfaceJavaCallReply gmj = new InterfaceJavaCallReply();

                gmj.Reply = reply;
                return gmj;
            }
            catch (Exception ex)
            {
                throw new Exception("Failed parsing xml in InterfaceJavaCall", ex);
            }

        }


    }
}
