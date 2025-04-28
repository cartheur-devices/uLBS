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
    ///  Reset the Device.
    /// </summary>
    public class SymmetryResetDeviceJavaCall : JavaCall
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SymmetryResetDeviceJavaCall"/> class.
        /// </summary>
        /// <param name="JSESSIONID">The JSESSIONID.</param>
        private SymmetryResetDeviceJavaCall(JNetBridge.Classes.JnetBridgeLoginUnit JSESSIONID)
            : base(JavaCall.baseURL + "resetUnit.do",
            JSESSIONID,
                new string[] { },
                new string[] { }) { }

        /// <summary>
        /// Resets the specified JSESSIONID.
        /// </summary>
        /// <param name="JSESSIONID">The JSESSIONID.</param>
        /// <param name="members">The members.</param>
        /// <returns></returns>
        public static SymmetryResetDeviceJavaCallReply Reset(JNetBridge.Classes.JnetBridgeLoginUnit JSESSIONID, string members)
        {
            if (members != null && members.Length > 0)
                return (SymmetryResetDeviceJavaCallReply)new SymmetryResetDeviceJavaCall(JSESSIONID, members).DoAction();
            else
            {
                throw new Exception("No member selected.");
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SymmetryResetDeviceJavaCall"/> class.
        /// </summary>
        /// <param name="JSESSIONID">The JSESSIONID.</param>
        /// <param name="members">The members.</param>
        private SymmetryResetDeviceJavaCall(JNetBridge.Classes.JnetBridgeLoginUnit JSESSIONID, string members)
            : base(JavaCall.baseURL + "resetUnit.do",
            JSESSIONID,
                new string[] { "cmd", "members" },
                new string[] { "resetUnit", members.ToString() })
        {

        }

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
                SymmetryResetDeviceJavaCallReply gmj = new SymmetryResetDeviceJavaCallReply();

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

                    Utils.FillProperties(gmj, xdMain.SelectSingleNode("//main"));

                }
                gmj.Reply = reply;
                return gmj;
            }
            catch (Exception ex)
            {
                throw new Exception("Failed parsing xml in MemberJavaCall", ex);
            }

        }

        /// <summary>
        /// 
        /// </summary>
        public enum SymmetryResetDeviceCMD { resetUnit }

    }
}
