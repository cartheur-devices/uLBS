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
    /// The Parent Symmetry java call.
    /// </summary>
    public class SymmetryJavaCall : JavaCall
    {
        /// <summary>
        /// Gets the config.
        /// </summary>
        /// <param name="JSESSIONID">The JSESSIONID.</param>
        /// <returns></returns>
        public static SymmetryJavaCallReply GetConfig(JNetBridge.Classes.JnetBridgeLoginUnit JSESSIONID)
        {
            return (SymmetryJavaCallReply)new SymmetryJavaCall(JSESSIONID).DoAction();
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="SymmetryJavaCall"/> class.
        /// </summary>
        /// <param name="JSESSIONID">The JSESSIONID.</param>
        private SymmetryJavaCall(JNetBridge.Classes.JnetBridgeLoginUnit JSESSIONID)
            : base(JavaCall.baseURL + "deviceConfigAction.do",
            JSESSIONID,
                new string[] {},
                new string[] {}) { }
        /// <summary>
        /// Saves the config.
        /// </summary>
        /// <param name="JSESSIONID">The JSESSIONID.</param>
        /// <param name="fmid">The fmid.</param>
        /// <param name="rf">The rf.</param>
        /// <param name="mov">The mov.</param>
        /// <returns></returns>
        public static SymmetryJavaCallReply SaveConfig(JNetBridge.Classes.JnetBridgeLoginUnit JSESSIONID, string fmid, int rf, int mov)
        {
            return (SymmetryJavaCallReply)new SymmetryJavaCall(JSESSIONID, fmid, rf, mov ).DoAction();
        }



        private SymmetryJavaCall(JNetBridge.Classes.JnetBridgeLoginUnit JSESSIONID, string fmid, int rf, int mov  )
            : base(JavaCall.baseURL + "deviceConfigAction.do",
            JSESSIONID,
                new string[] { "cmd", "fmid", "rf", "mov" },
                new string[] { "set", fmid, rf.ToString(), mov.ToString()})  {  }
        /// <summary>
        /// Initializes a new instance of the <see cref="SymmetryJavaCall"/> class.
        /// </summary>
        /// <param name="JSESSIONID">The JSESSIONID.</param>
        /// <param name="fmid">The fmid.</param>
        /// <param name="intDaily">The int daily.</param>
        /// <param name="intMotion">The int motion.</param>
        /// <param name="intNoGPS">The int no GPS.</param>
        /// <param name="input1Name">Name of the input1.</param>
        /// <param name="input2Name">Name of the input2.</param>
        /// <param name="input3Name">Name of the input3.</param>
        /// <param name="input1OnOff">The input1 on off.</param>
        /// <param name="input2OnOff">The input2 on off.</param>
        /// <param name="input3OnOff">The input3 on off.</param>
        private SymmetryJavaCall(JNetBridge.Classes.JnetBridgeLoginUnit JSESSIONID, string fmid, int intDaily, int intMotion, int intNoGPS, string input1Name, string input2Name, string input3Name, string input1OnOff, string input2OnOff, string input3OnOff)
            : base(JavaCall.baseURL + "deviceConfigAction.do",
            JSESSIONID,
                new string[] { "cmd", "save", "fmid", "intDaily", "intMotion", "intNoGPS", "input1Name", "input2Name", "input3Name", "input1OnOff", "input2OnOff", "input3OnOff" },
                new string[] { "request", "true", fmid, intDaily.ToString(), intMotion.ToString(), intNoGPS.ToString(), input1Name, input2Name, input3Name, input1OnOff, input2OnOff, input3OnOff })  {  }
        /// <summary>
        /// Gets the config.
        /// </summary>
        /// <param name="JSESSIONID">The JSESSIONID.</param>
        /// <param name="fmid">The fmid.</param>
        /// <returns></returns>
        public static SymmetryJavaCallReply GetConfig(JNetBridge.Classes.JnetBridgeLoginUnit JSESSIONID, string fmid)
        {
            return (SymmetryJavaCallReply)new SymmetryJavaCall(JSESSIONID, fmid).DoAction();
        }

        private SymmetryJavaCall(JNetBridge.Classes.JnetBridgeLoginUnit JSESSIONID, string fmid)
            : base(JavaCall.baseURL + "deviceConfigAction.do",
            JSESSIONID,
                new string[] { "cmd", "fmid" },
                new string[] { "request", fmid })  {  }
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

                SymmetryJavaCallReply gmj = new SymmetryJavaCallReply();
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
