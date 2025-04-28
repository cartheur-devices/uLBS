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
    public class OcellusProfilesJavaCall : JavaCall
    {
/// <summary>
/// 
/// </summary>
/// <param name="JSESSIONID"></param>
/// <param name="memberID"></param>
/// <param name="alias"></param>
/// <returns></returns>
        public static OcellusProfilesJavaCallReply Get(JNetBridge.Classes.JnetBridgeLoginUnit JSESSIONID)
        {
            return (OcellusProfilesJavaCallReply)new OcellusProfilesJavaCall(JSESSIONID).DoAction();
        }



        private OcellusProfilesJavaCall(JNetBridge.Classes.JnetBridgeLoginUnit JSESSIONID)
            : base(JavaCall.baseURL + "getOcellusProfiles.do",
            JSESSIONID,
                new string[] {},
                new string[] {})
        {

        }

        public static OcellusProfilesJavaCallReply Delete(JNetBridge.Classes.JnetBridgeLoginUnit JSESSIONID, int recid)
        {
            return (OcellusProfilesJavaCallReply)new OcellusProfilesJavaCall(JSESSIONID, OcellusProfilesCMD.deleteprofile, recid).DoAction();
        }



        private OcellusProfilesJavaCall(JNetBridge.Classes.JnetBridgeLoginUnit JSESSIONID, OcellusProfilesCMD cmd, int recid)
            : base(JavaCall.baseURL + "handleOcellusProfileAction.do",
            JSESSIONID,
                new string[] { "cmd", "name"},
                new string[] { cmd.ToString(), recid.ToString() })
        {

        }

        public static OcellusProfilesJavaCallReply Set(JNetBridge.Classes.JnetBridgeLoginUnit JSESSIONID, string name, string vars, int throttle, string actsens, string repints, string perrep, string repintm, string smsrep, string smscheck, string led, string rungps)
        {
            return (OcellusProfilesJavaCallReply)new OcellusProfilesJavaCall(JSESSIONID, OcellusProfilesCMD.setprofile  ,  name,  vars,  throttle,  actsens,  repints,  perrep,  repintm,  smsrep,  smscheck,  led,  rungps).DoAction();
        }

        private OcellusProfilesJavaCall(JNetBridge.Classes.JnetBridgeLoginUnit JSESSIONID, OcellusProfilesCMD cmd, string name, string vars, int throttle, string actsens, string repints, string perrep, string repintm, string smsrep, string smscheck, string led, string rungps)
            : base(JavaCall.baseURL + "handleOcellusProfileAction.do",
            JSESSIONID,
                new string[] {"cmd",  "name", "vars", "throttle", "actsens", "repints", "perrep", "repintm", "smsrep", "smscheck", "led", "rungps" },
                new string[] {  cmd.ToString(),   name,  vars,  throttle.ToString(),  actsens.ToString(),  repints.ToString(),  perrep,  repintm.ToString(),  smsrep.ToString(),  smscheck,  led,  rungps.ToString()})
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
                OcellusProfilesJavaCallReply gmj = new OcellusProfilesJavaCallReply();

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

        public enum OcellusProfilesCMD { setprofile, deleteprofile }


       
    }
}
