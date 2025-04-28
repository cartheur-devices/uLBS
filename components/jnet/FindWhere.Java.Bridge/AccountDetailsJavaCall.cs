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
    /// <summary>
    /// 
    /// </summary>
    public class AccountDetailsJavaCall : JavaCall
    {
        /// <summary>
        /// Gets the specified JSESSIONID.
        /// </summary>
        /// <param name="JSESSIONID">The JSESSIONID.</param>
        /// <returns></returns>
        public static AccountDetailsJavaCallReply  Get(JNetBridge.Classes.JnetBridgeLoginUnit JSESSIONID)
        {
            return (AccountDetailsJavaCallReply)new AccountDetailsJavaCall(JSESSIONID).DoAction();
        }
        
        private AccountDetailsJavaCall(JNetBridge.Classes.JnetBridgeLoginUnit JSESSIONID)
            : base(JavaCall.baseURL + "getAccountDetails.do",
            JSESSIONID,
                new string[] { },
                new string[] { })  { }

        /// <summary>
        /// Sets the specified JSESSIONID.
        /// </summary>
        /// <param name="JSESSIONID">The JSESSIONID.</param>
        /// <param name="emailAddress">The email address.</param>
        /// <param name="companyPostcode">The company postcode.</param>
        /// <param name="language">The language.</param>
        /// <param name="distanceInUnits">The distance in units.</param>
        /// <param name="displayMapLabels">The display map labels.</param>
        /// <param name="companyName">Name of the company.</param>
        /// <param name="oldPassword">The old password.</param>
        /// <param name="newPassword">The new password.</param>
        /// <param name="userTimerSetting">The user timer setting.</param>
        /// <param name="operatorId">The operator id.</param>
        /// <param name="timeZoneId">The time zone id.</param>
        /// <returns></returns>
        public static AccountDetailsJavaCallReply Set(JNetBridge.Classes.JnetBridgeLoginUnit JSESSIONID, string emailAddress, string companyPostcode, string language, int distanceInUnits, int displayMapLabels, string companyName, string oldPassword, string newPassword, int userTimerSetting, int operatorId, string timeZoneId)
        {
            return (AccountDetailsJavaCallReply)new AccountDetailsJavaCall(JSESSIONID, emailAddress, companyPostcode, language, distanceInUnits, displayMapLabels, companyName, oldPassword, newPassword, userTimerSetting, operatorId, timeZoneId).DoAction();
        }

        private AccountDetailsJavaCall(JNetBridge.Classes.JnetBridgeLoginUnit JSESSIONID, string emailAddress, string companyPostcode, string language, int distanceInUnits, int displayMapLabels, string companyName, string oldPassword, string newPassword, int userTimerSetting, int operatorId, string timeZoneId)
            : base(JavaCall.baseURL + "setAccountDetails.do",
            JSESSIONID,
                new string[] { "emailAddress", "companyPostcode", "language", "distanceInUnits", "displayMapLabels", "companyName", "oldPassword", "newPassword", "userTimerSetting", "operatorId", "timeZoneId" },
                new string[] { emailAddress, companyPostcode, language, distanceInUnits.ToString(), displayMapLabels.ToString(), companyName, oldPassword, newPassword, userTimerSetting.ToString(), operatorId.ToString(), HttpUtility.UrlEncode(timeZoneId) })  { }

        /// <summary>
        /// This method does the actual conversion from serialized data to the deserialized LoginJavaCallReply object.
        /// </summary>
        /// <param name="reply">The reply.</param>
        /// <param name="JSESSIONID">The JSESSIONID.</param>
        /// <returns>The server reply.</returns>
        protected override JavaCallReply ParseServerReply(string reply, Cookie JSESSIONID)
        {
            try
            {
                // Main XML Document
                XmlDocument xdMain = new XmlDocument();
                int pos1 = reply.IndexOf("<?xml");
                int pos2 = reply.IndexOf("</main>");
                string xml = reply.Substring(pos1, (pos2 - pos1 + 7));
                xdMain.LoadXml(xml);

                //if (xdMain.SelectSingleNode("error") != null)
                //{
                //    throw new Exception(xdMain.SelectSingleNode("error").InnerText);
                //}

                //XmlDocument workingXmlDoc;
                //workingXmlDoc = new XmlDocument();


#if DEBUG
                if (System.Environment.MachineName == "TEYDO11")
                {
                    //xdMain.Save(@"c:\ReceivedXml.xml");
                }
#endif


                AccountDetailsJavaCallReply gmj = new AccountDetailsJavaCallReply ();
                Utils.FillProperties(gmj,  xdMain.SelectSingleNode("//main"));

                 gmj.Reply = xml;
                return gmj;
            }
            catch (Exception ex)
            {
                throw new Exception("Failed parsing xml in AccountDetailsJavaCall", ex);
            }
               
        }
    }
}
