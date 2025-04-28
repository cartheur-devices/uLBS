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
    public class TTJavaCall : JavaCall
    {
        /// <summary>
        /// Gets the config.
        /// </summary>
        /// <param name="JSESSIONID">The JSESSIONID.</param>
        /// <param name="tt15">if set to <c>true</c> [TT15].</param>
        /// <returns></returns>
        public static TTJavaCallReply GetConfig(JNetBridge.Classes.JnetBridgeLoginUnit JSESSIONID, Boolean tt15)
        {
            return (TTJavaCallReply)new TTJavaCall(JSESSIONID, tt15).DoAction();
        }

        private TTJavaCall(JNetBridge.Classes.JnetBridgeLoginUnit JSESSIONID, Boolean tt15)
            : base(JavaCall.baseURL + "getTTConfig.do",
            JSESSIONID,
                new string[] { "tt15" },
                new string[] { tt15.ToString()})
        {

        }

        /// <summary>
        /// Gets the config.
        /// </summary>
        /// <param name="JSESSIONID">The JSESSIONID.</param>
        /// <param name="fmid">The fmid.</param>
        /// <returns></returns>
        public static TTJavaCallReply GetConfig(JNetBridge.Classes.JnetBridgeLoginUnit JSESSIONID, string fmid)
        {
            return (TTJavaCallReply)new TTJavaCall(JSESSIONID, fmid).DoAction();
        }



        /// <summary>
        /// Initializes a new instance of the <see cref="TTJavaCall"/> class.
        /// </summary>
        /// <param name="JSESSIONID">The JSESSIONID.</param>
        /// <param name="fmid">The fmid.</param>
        private TTJavaCall(JNetBridge.Classes.JnetBridgeLoginUnit JSESSIONID, string fmid)
             : base(JavaCall.baseURL + "getTTConfig.do",
            JSESSIONID,
                new string[] { "cmd", "fmid" },
                new string[] {"request", fmid })
        {

        }


        /// <summary>
        /// Resets the configuration.
        /// </summary>
        /// <param name="JSESSIONID">The JSESSIONID.</param>
        /// <param name="fmid">The fmid.</param>
        /// <returns></returns>
        public static TTJavaCallReply ResetConfig(JNetBridge.Classes.JnetBridgeLoginUnit JSESSIONID, int fmid)
        {
            return (TTJavaCallReply)new TTJavaCall(JSESSIONID, "true", fmid, 4, "true","").DoAction();
        }

        /// <summary>
        /// Gets the runtime.
        /// </summary>
        /// <param name="JSESSIONID">The JSESSIONID.</param>
        /// <param name="fmid">The fmid.</param>
        /// <returns></returns>
        public static TTJavaCallReply GetRuntime(JNetBridge.Classes.JnetBridgeLoginUnit JSESSIONID, int fmid)
        {
            return (TTJavaCallReply)new TTJavaCall(JSESSIONID, "true", fmid, 4, "","true").DoAction();
        }

        /// <summary>
        /// Gets the config.
        /// </summary>
        /// <param name="JSESSIONID">The JSESSIONID.</param>
        /// <param name="fmid">The fmid.</param>
        /// <param name="tabnr">The tabnr.</param>
        /// <returns></returns>
        public static TTJavaCallReply GetConfig(JNetBridge.Classes.JnetBridgeLoginUnit JSESSIONID, int fmid, int tabnr)
        {
            return (TTJavaCallReply)new TTJavaCall(JSESSIONID,"true", fmid, tabnr, "","").DoAction();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TTJavaCall"/> class.
        /// </summary>
        /// <param name="JSESSIONID">The JSESSIONID.</param>
        /// <param name="tt15">The TT15.</param>
        /// <param name="fmid">The fmid.</param>
        /// <param name="tabnr">The tabnr.</param>
        /// <param name="resetruntime">The resetruntime.</param>
        /// <param name="getruntime">The getruntime.</param>
        private TTJavaCall(JNetBridge.Classes.JnetBridgeLoginUnit JSESSIONID, string tt15, int fmid, int tabnr, string resetruntime, string getruntime)
            : base(JavaCall.baseURL + "getTTConfig.do",
            JSESSIONID,
                new string[] { "tt15", "cmd", "fmid", "tabnr", "resetruntime", "getruntime" },
                new string[] { tt15, "request", fmid.ToString(), tabnr.ToString(), resetruntime, getruntime })
        {

        }

        /// <summary>
        /// Sets the config.
        /// </summary>
        /// <param name="JSESSIONID">The JSESSIONID.</param>
        /// <param name="fmid">The fmid.</param>
        /// <param name="sto">The sto.</param>
        /// <param name="srm">The SRM.</param>
        /// <param name="srt">The SRT.</param>
        /// <param name="mo">The mo.</param>
        /// <param name="ap">The ap.</param>
        /// <param name="mp">The mp.</param>
        /// <param name="hpa">The hpa.</param>
        /// <param name="mpa">The mpa.</param>
        /// <param name="lpa">The lpa.</param>
        /// <param name="lpag">The lpag.</param>
        /// <param name="lpagr">The lpagr.</param>
        /// <param name="lpagswitch">The lpagswitch.</param>
        /// <param name="rtm">The RTM.</param>
        /// <param name="sv">The sv.</param>
        /// <param name="motionrtm">The motionrtm.</param>
        /// <param name="schedMotionTime">The sched motion time.</param>
        /// <param name="schedMotionDaily">The sched motion daily.</param>
        /// <param name="schedLpaTime">The sched lpa time.</param>
        /// <param name="schedLpaDaily">The sched lpa daily.</param>
        /// <returns></returns>
        public static TTJavaCallReply SetConfig(JNetBridge.Classes.JnetBridgeLoginUnit JSESSIONID, int fmid, string sto, string srm, string srt, string mo, string ap, string mp, string hpa, string mpa, string lpa, string lpag, string lpagr, string lpagswitch, string rtm, string sv, string motionrtm, string schedMotionTime, string schedMotionDaily, string schedLpaTime, string schedLpaDaily)
        {
            return (TTJavaCallReply)new TTJavaCall(JSESSIONID, fmid, sto, srm, srt, mo, ap, mp, hpa, mpa, lpa, lpag, lpagr, lpagswitch, rtm, sv, motionrtm, schedMotionTime, schedMotionDaily, schedLpaTime, schedLpaDaily).DoAction();
        }

        private TTJavaCall(JNetBridge.Classes.JnetBridgeLoginUnit JSESSIONID, int fmid, string sto, string srm, string srt, string mo, string ap, string mp, string hpa, string mpa, string lpa, string lpag, string lpagr, string lpagswitch, string rtm, string sv, string motionrtm,string schedMotionTime, string schedMotionDaily,string schedLpaTime,string schedLpaDaily)
            : base(JavaCall.baseURL + "getTTConfig.do",
            JSESSIONID,
                new string[] { "cmd", "fmid", "tabnr", "save",  "sto", "srm", "srt", "mo", "ap", "mp", "hpa", "mpa", "lpa", "lpag", "lpagr", "lpagswitch", "rtm", "sv", "motionrtm", "schedMotionTime", "schedMotionDaily", "schedLpaTime", "schedLpaDaily" },
                new string[] { "request", fmid.ToString(), "1", "true", sto, srm, srt, mo, ap, mp, hpa, mpa, lpa, lpag, lpagr, lpagswitch, rtm, sv, motionrtm, schedMotionTime, schedMotionDaily, schedLpaTime, schedLpaDaily }) { }

        /// <summary>
        /// This method does the actual conversion from serialized data to the deserialized LoginJavaCallReply object
        /// </summary>
        /// <param name="reply">The reply.</param>
        /// <param name="JSESSIONID">The JSESSIONID.</param>
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

                TTJavaCallReply gmj = new TTJavaCallReply();
                Utils.FillProperties(gmj, xdMain.SelectSingleNode("//main"));

                gmj.Reply = reply;
                return gmj;
            }
            catch (Exception ex)
            {
                throw new Exception("Failed parsing xml in TTJavaCallReply", ex);
            }    
        }      
    }
}
