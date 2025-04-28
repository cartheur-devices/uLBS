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
    public class TTProfileJavaCall : JavaCall
    {
/// <summary>
/// 
/// </summary>
/// <param name="JSESSIONID"></param>
/// <returns></returns>
        public static TTProfileJavaCallReply Get(JNetBridge.Classes.JnetBridgeLoginUnit JSESSIONID)
        {
            return (TTProfileJavaCallReply)new TTProfileJavaCall(JSESSIONID).DoAction();
        }

        private TTProfileJavaCall(JNetBridge.Classes.JnetBridgeLoginUnit JSESSIONID)
            : base(JavaCall.baseURL + "ttProfile.do",
            JSESSIONID,
                new string[] {  },
                new string[] { })
        {

        }

         public static TTProfileJavaCallReply Delete(JNetBridge.Classes.JnetBridgeLoginUnit JSESSIONID, int profileid)
        {
            return (TTProfileJavaCallReply)new TTProfileJavaCall(JSESSIONID, profileid, TTProfilesCMD.deleteprofile ).DoAction();
        }



         private TTProfileJavaCall(JNetBridge.Classes.JnetBridgeLoginUnit JSESSIONID, int profileid, TTProfilesCMD cmd)
             : base(JavaCall.baseURL + "ttProfile.do",
            JSESSIONID,
                new string[] { "cmd", "id", "tabnr" },
                new string[] {cmd.ToString(), profileid.ToString(), "1" })
        {

        }


         public static TTProfileJavaCallReply Save(JNetBridge.Classes.JnetBridgeLoginUnit JSESSIONID, string name, string startDay, string endDay, string startHour, string startMin, string endHour, string endMin)
         {
             return (TTProfileJavaCallReply)new TTProfileJavaCall(JSESSIONID, TTProfilesCMD.save, name, startDay, endDay, startHour, startMin, endHour, endMin).DoAction();
         }


        /// <summary>
        /// Used for save...
        /// </summary>
        /// <param name="JSESSIONID"></param>
        /// <param name="cmd"></param>
        /// <param name="name"></param>
        /// <param name="startDay"></param>
        /// <param name="endDay"></param>
        /// <param name="startHour"></param>
        /// <param name="startMin"></param>
        /// <param name="endHour"></param>
        /// <param name="endMin"></param>
         private TTProfileJavaCall(JNetBridge.Classes.JnetBridgeLoginUnit JSESSIONID, TTProfilesCMD cmd, string name, string startDay, string endDay, string startHour, string startMin, string endHour, string endMin)
             : base(JavaCall.baseURL + "ttProfile.do",
            JSESSIONID,
                new string[] {"cmd" ,  "name", "startDay", "endDay", "startHour","startMin", "endHour", "endMin", "tabnr" },
                new string[] { cmd.ToString(), name, startDay, endDay, startHour.ToString(), startMin.ToString(), endHour.ToString(), endMin.ToString(), "1" })
         {

         }





         public static TTProfileJavaCallReply SaveRelation(JNetBridge.Classes.JnetBridgeLoginUnit JSESSIONID, int id, string[] fmId)
         {
             return (TTProfileJavaCallReply)new TTProfileJavaCall(JSESSIONID, TTProfilesCMD.saveRelation, id, fmId, 2).DoAction();
         }



         private TTProfileJavaCall(JNetBridge.Classes.JnetBridgeLoginUnit JSESSIONID, TTProfilesCMD cmd, int id, string[] fmId, int tabnr)
             : base(JavaCall.baseURL + "ttProfile.do",
            JSESSIONID,
                new string[] { "cmd", "id", "fmId", "tabnr" },
                new string[] { cmd.ToString(), id.ToString(), Utils.SeperatedListFromString2(fmId), tabnr.ToString() })
         {

         }

         public static TTProfileJavaCallReply deleteRelation(JNetBridge.Classes.JnetBridgeLoginUnit JSESSIONID, string[] fmId)
         {
             return (TTProfileJavaCallReply)new TTProfileJavaCall(JSESSIONID, TTProfilesCMD.deleteRelation ,fmId, 3).DoAction();
         }


         private TTProfileJavaCall(JNetBridge.Classes.JnetBridgeLoginUnit JSESSIONID, TTProfilesCMD cmd, string[] fmId, int tabnr)
             : base(JavaCall.baseURL + "ttProfile.do",
            JSESSIONID,
                new string[] { "cmd",  "fmId", "tabnr" },
                new string[] { cmd.ToString(), Utils.SeperatedListFromString2(fmId), tabnr.ToString() })
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
                string xml = reply.Substring(pos1, (pos2 - pos1 + 7));
                xdMain.LoadXml(reply.Substring(pos1, (pos2 - pos1 + 7)));

                if (xdMain.SelectSingleNode("error") != null)
                {
                    throw new Exception(xdMain.SelectSingleNode("error").InnerText);
                }

                XmlDocument workingXmlDoc;
                workingXmlDoc = new XmlDocument();

                TTProfileJavaCallReply gmj = new TTProfileJavaCallReply();
                Utils.FillProperties(gmj,  xdMain.SelectSingleNode("//main"));

                gmj.Reply = reply;
                return gmj;
            }
            catch (Exception ex)
            {
                throw new Exception("Failed parsing xml in TTJavaCallReply", ex);
            }

               
        }

        public enum TTProfilesCMD { save, saveRelation, deleteprofile, deleteRelation }

     
    }
}
