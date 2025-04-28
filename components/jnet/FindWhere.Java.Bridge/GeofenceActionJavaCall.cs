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
using System.Web;

namespace JNetBridge
{
    public class GeofenceActionJavaCall : JavaCall
    {
/// <summary>
/// 
/// </summary>
/// <param name="JSESSIONID"></param>
/// <param name="cmd"></param>
/// <returns></returns>
        public static GeofenceActionJavaCallReply GetGeofenceAction(JNetBridge.Classes.JnetBridgeLoginUnit JSESSIONID, string cmd)
        {
            return (GeofenceActionJavaCallReply)new GeofenceActionJavaCall(JSESSIONID, cmd).DoAction();
        }

        private GeofenceActionJavaCall(JNetBridge.Classes.JnetBridgeLoginUnit JSESSIONID, string cmd)
            : base(JavaCall.baseURL + "geofenceAction.do",
            JSESSIONID,
                new string[] {"cmd" },
                new string[] { cmd })
        {

        }

        public static GeofenceActionJavaCallReply GetGeofenceAction(JNetBridge.Classes.JnetBridgeLoginUnit JSESSIONID, GeofenceAction gfAction, string fenceid)
        {
            return (GeofenceActionJavaCallReply)new GeofenceActionJavaCall(JSESSIONID, gfAction, fenceid).DoAction();
        }


        private GeofenceActionJavaCall(JNetBridge.Classes.JnetBridgeLoginUnit JSESSIONID, GeofenceAction gfAction, string fenceid)
            : base(JavaCall.baseURL + "geofenceAction.do",
            JSESSIONID,
                new string[] { "cmd", "fenceid" },
                new string[] { gfAction.ToString(), fenceid })
        {

        }

        public static GeofenceActionJavaCallReply GetGeofenceAction(JNetBridge.Classes.JnetBridgeLoginUnit JSESSIONID, GeofenceAction cmd, int fenceid, string name, string postcode, double radius, int loctype, double locx, double locy)
        {
            return (GeofenceActionJavaCallReply)new GeofenceActionJavaCall(JSESSIONID,  cmd,  fenceid,  name,  postcode,  radius,  loctype,  locx,  locy).DoAction();
        }

        private GeofenceActionJavaCall(JNetBridge.Classes.JnetBridgeLoginUnit JSESSIONID, GeofenceAction cmd, int fenceid, string name, string postcode, double radius, int loctype, double locx, double locy)
            : base(JavaCall.baseURL + "geofenceAction.do",
            JSESSIONID,
                new string[] { "cmd", "fenceid", "name", "postcode", "radius", "loctype", "locx", "locy" },
                new string[] { cmd.ToString(), fenceid.ToString(), HttpUtility.UrlEncode(name), postcode, radius.ToString().Replace(",", "."), loctype.ToString(), locx.ToString().Replace(",", "."), locy.ToString().Replace(",", ".") })
        {

        }

        public static GeofenceActionJavaCallReply getMap(JNetBridge.Classes.JnetBridgeLoginUnit JSESSIONID, string postcode, double radius, int loctype, double locx, double locy, int keepzoom)
        {
            if (postcode == null) postcode = "null";
            return (GeofenceActionJavaCallReply)new GeofenceActionJavaCall(JSESSIONID, GeofenceAction.GETMAP , postcode, radius, loctype, locx, locy, keepzoom).DoAction();
        }

        private GeofenceActionJavaCall(JNetBridge.Classes.JnetBridgeLoginUnit JSESSIONID, GeofenceAction cmd, string postcode, double radius, int loctype, double locx, double locy, int keepzoom)
            : base(JavaCall.baseURL + "geofenceAction.do",
            JSESSIONID,
                new string[] { "cmd", "postcode", "width","height",  "radius", "loctype", "locx", "locy", "keepzoom" },
                new string[] { cmd.ToString(), postcode.ToString(), "800", "600", radius.ToString().Replace(",", "."), loctype.ToString().Replace(",", "."), locx.ToString().Replace(",", "."), locy.ToString().Replace(",", "."), keepzoom.ToString() })
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
                //    try {
                //        xdMain.Save(@"c:\Received.xml");
                //        }
                //    catch(Exception ex)
                //    {}

                    
                //}
#endif

                XmlDocument workingXmlDoc;
                workingXmlDoc = new XmlDocument();

                GeofenceActionJavaCallReply gmj = new GeofenceActionJavaCallReply();
                Utils.FillProperties(gmj, xdMain.SelectSingleNode("//main"));

                gmj.Reply = reply;

                return gmj;
            }
            catch (Exception ex)
            {
                throw new Exception("Failed parsing xml in GeofenceActionJavaCall", ex);
            }
         
        }


        public enum GeofenceAction { STORE, DEL, GETMAP }

    }
}
