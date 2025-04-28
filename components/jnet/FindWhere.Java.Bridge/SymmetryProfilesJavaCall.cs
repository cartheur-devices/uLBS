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
    public class SymmetryProfilesJavaCall : JavaCall
    {
/// <summary>
/// 
/// </summary>
/// <param name="JSESSIONID"></param>
/// <param name="memberID"></param>
/// <param name="alias"></param>
/// <returns></returns>
        public static SymmetryProfilesJavaCallReply Get(JNetBridge.Classes.JnetBridgeLoginUnit JSESSIONID)
        {
            return (SymmetryProfilesJavaCallReply)new SymmetryProfilesJavaCall(JSESSIONID).DoAction();
        }



        private SymmetryProfilesJavaCall(JNetBridge.Classes.JnetBridgeLoginUnit JSESSIONID)
            : base(JavaCall.baseURL + "defineSymmetryProfiles.do",
            JSESSIONID,
                new string[] {},
                new string[] {})
        {

        }

        public static SymmetryProfilesJavaCallReply Delete(JNetBridge.Classes.JnetBridgeLoginUnit JSESSIONID, int profileid)
        {
            return (SymmetryProfilesJavaCallReply)new SymmetryProfilesJavaCall(JSESSIONID, SymmetryProfilesCMD.delete, profileid).DoAction();
        }



        private SymmetryProfilesJavaCall(JNetBridge.Classes.JnetBridgeLoginUnit JSESSIONID, SymmetryProfilesCMD cmd, int profileid)
            : base(JavaCall.baseURL + "handleSymmetryProfile.do",
            JSESSIONID,
                new string[] { "cmd", "profileid" },
                new string[] { cmd.ToString(), profileid.ToString() })
        {

        }

        public static SymmetryProfilesJavaCallReply Set(JNetBridge.Classes.JnetBridgeLoginUnit JSESSIONID, string name, int interval, int starth, int startm, int endh, int endm, int monday, int tuesday, int wednesday, int thursday, int friday, int saturday, int sunday, int battsave, int? speed)
        {
            string spd = "";
            if (speed != null) spd = speed.ToString();
            return (SymmetryProfilesJavaCallReply)new SymmetryProfilesJavaCall(JSESSIONID, SymmetryProfilesCMD.setprofile, name, interval, starth, startm, endh, endm, monday, tuesday, wednesday, thursday, friday, saturday, sunday, battsave, spd).DoAction();
        }

        private SymmetryProfilesJavaCall(JNetBridge.Classes.JnetBridgeLoginUnit JSESSIONID, SymmetryProfilesCMD cmd, string name, int interval, int starth, int startm, int endh, int endm, int monday, int tuesday, int wednesday, int thursday, int friday, int saturday, int sunday, int battsave, string speed)
            : base(JavaCall.baseURL + "handleSymmetryProfile.do",
            JSESSIONID,
                new string[] { "cmd", "name", "interval", "starth", "startm", "endh", "endm", "monday", "tuesday", "wednesday", "thursday", "friday","saturday","sunday","battsave","speed" },
                new string[] { cmd.ToString(), name, interval.ToString(), starth.ToString(), startm.ToString(), endh.ToString(), endm.ToString(), monday.ToString(), tuesday.ToString(), wednesday.ToString(), thursday.ToString(), friday.ToString(), saturday.ToString(), sunday.ToString(), battsave.ToString(), speed })
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
                SymmetryProfilesJavaCallReply gmj = new SymmetryProfilesJavaCallReply();

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

        public enum SymmetryProfilesCMD { setprofile, delete }


       
    }
}
