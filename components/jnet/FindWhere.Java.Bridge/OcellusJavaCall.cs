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
using com.teleca.fleetonline.repository;
using System.Collections;

namespace JNetBridge
{
    /// <summary>
    /// 
    /// </summary>
    public class OcellusJavaCall : JavaCall
    {
/// <summary>
/// 
/// </summary>
/// <param name="JSESSIONID"></param>
/// <param name="memberID"></param>
/// <param name="alias"></param>
/// <returns></returns>
        public static OcellusJavaCallReply GetConfig(JNetBridge.Classes.JnetBridgeLoginUnit JSESSIONID)
        {

            return (OcellusJavaCallReply)new OcellusJavaCall(JSESSIONID).DoAction();
        }



        private OcellusJavaCall(JNetBridge.Classes.JnetBridgeLoginUnit JSESSIONID)
            : base(JavaCall.baseURL + "getOcellusViewSettings.do",
            JSESSIONID,
                new string[] {},
                new string[] {})
        {

        }

        public static OcellusJavaCallReply Set(JNetBridge.Classes.JnetBridgeLoginUnit JSESSIONID, int profid, string[] members)
        {
            ArrayList alParamNames = new ArrayList();
            ArrayList alParamValues = new ArrayList();

            alParamNames.Add("cmd");
            alParamValues.Add("sendprofile");

            alParamNames.Add("profid");
            alParamValues.Add(profid.ToString());

            foreach (string member in members)
            {
                alParamNames.Add("member");
                alParamValues.Add(member);
            }

            string[] paramNames = (string[])alParamNames.ToArray(typeof(string));
            string[] paramValues = (string[])alParamValues.ToArray(typeof(string));


            return (OcellusJavaCallReply)new OcellusJavaCall(JSESSIONID, paramNames, paramValues).DoAction();
        }

        private OcellusJavaCall(JNetBridge.Classes.JnetBridgeLoginUnit JSESSIONID, string[] paramNames, string[] paramValues)
            : base(JavaCall.baseURL + "handleOcellusProfileAction.do",  JSESSIONID,paramNames, paramValues)
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
                OcellusJavaCallReply gmj = new OcellusJavaCallReply();

                if (reply.Contains("<?xml"))
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
           
           
                Utils.FillProperties(gmj,  xdMain.SelectSingleNode("//main"));
                //if (gmj.OcellusProfileDisplayHelper != null && gmj.OcellusProfileDisplayHelper.Groups != null && gmj.OcellusProfileDisplayHelper.Groups.AllMembers != null)
                //{
                //    //TODO: in the xml: <allMembers reference="../groups/allMembers" /> 
                //    // so; do it manual!
                //    gmj.OcellusProfileDisplayHelper.AllMembers= new LinkedList<com.teleca.fleetonline.repository.MemberData>();
                //    foreach (MemberData md in gmj.OcellusProfileDisplayHelper.Groups.AllMembers)
                //    {
                //        gmj.OcellusProfileDisplayHelper.AllMembers.AddLast(md);
                //    }


                //}
                }
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
