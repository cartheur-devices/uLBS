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
    public class EnforaJavaCall : JavaCall
    {
/// <summary>
/// 
/// </summary>
/// <param name="JSESSIONID"></param>
/// <param name="memberID"></param>
/// <param name="alias"></param>
/// <returns></returns>
        public static EnforaJavaCallReply GetConfig(JNetBridge.Classes.JnetBridgeLoginUnit JSESSIONID)
        {
            return (EnforaJavaCallReply)new EnforaJavaCall(JSESSIONID).DoAction();
        }



        private EnforaJavaCall(JNetBridge.Classes.JnetBridgeLoginUnit JSESSIONID)
            : base(JavaCall.baseURL + "getEnforaConfig.do",
            JSESSIONID,
                new string[] {},
                new string[] {})
        {

        }

        public static EnforaJavaCallReply SaveConfig(JNetBridge.Classes.JnetBridgeLoginUnit JSESSIONID, string fmid, int intDaily, int intMotion, int intNoGPS, string input1Name, string input2Name, string input3Name, string input1OnOff, string input2OnOff, string input3OnOff, int  sendIO)
        {
            return (EnforaJavaCallReply)new EnforaJavaCall(JSESSIONID, fmid, intDaily, intMotion, intNoGPS, input1Name, input2Name, input3Name, input1OnOff, input2OnOff, input3OnOff, sendIO).DoAction();
        }

        public static EnforaJavaCallReply SaveConfig(JNetBridge.Classes.JnetBridgeLoginUnit JSESSIONID, string fmid, int intDaily, int intMotion, int intNoGPS, string input1Name, string input2Name, string input3Name, string input1OnOff, string input2OnOff, string input3OnOff)
        {
            return (EnforaJavaCallReply)new EnforaJavaCall(JSESSIONID, fmid, intDaily, intMotion, intNoGPS, input1Name, input2Name, input3Name, input1OnOff, input2OnOff, input3OnOff, 0).DoAction();
        }

        private EnforaJavaCall(JNetBridge.Classes.JnetBridgeLoginUnit JSESSIONID, string fmid, int intDaily, int intMotion, int intNoGPS )
            : base(JavaCall.baseURL + "getEnforaConfig.do",
            JSESSIONID,
                new string[] { "cmd", "save", "fmid", "intDaily", "intMotion", "intNoGPS"},
                new string[] { "request", "true", fmid, intDaily.ToString(), intMotion.ToString(), intNoGPS.ToString()})
        {

        }

        private EnforaJavaCall(JNetBridge.Classes.JnetBridgeLoginUnit JSESSIONID, string fmid, int intDaily, int intMotion, int intNoGPS, string input1Name, string input2Name, string input3Name, string input1OnOff, string input2OnOff, string input3OnOff, int sendIO)
            : base(JavaCall.baseURL + "getEnforaConfig.do",
            JSESSIONID,
                new string[] { "cmd", "save", "fmid", "intDaily", "intMotion", "intNoGPS", "input1Name", "input2Name", "input3Name", "input1OnOff", "input2OnOff", "input3OnOff", "sendIO" },
                new string[] { "request", "true", fmid, intDaily.ToString(), intMotion.ToString(), intNoGPS.ToString(), input1Name, input2Name, input3Name, input1OnOff, input2OnOff, input3OnOff, sendIO.ToString() })
        {

        }

        public static EnforaJavaCallReply GetConfig(JNetBridge.Classes.JnetBridgeLoginUnit JSESSIONID, string fmid)
        {

            return (EnforaJavaCallReply)new EnforaJavaCall(JSESSIONID, fmid).DoAction();
        }

        private EnforaJavaCall(JNetBridge.Classes.JnetBridgeLoginUnit JSESSIONID, string fmid)
            : base(JavaCall.baseURL + "getEnforaConfig.do",
            JSESSIONID,
                new string[] {"cmd", "fmid" },
                new string[] {"request", fmid })
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

                EnforaJavaCallReply gmj = new EnforaJavaCallReply();
                Utils.FillProperties(gmj,  xdMain.SelectSingleNode("//main"));

                gmj.Reply = reply;
                return gmj;
            }
            catch (Exception ex)
            {
                throw new Exception("Failed parsing xml in MemberJavaCall", ex);
            }

               
        }

        




       
    }
}
