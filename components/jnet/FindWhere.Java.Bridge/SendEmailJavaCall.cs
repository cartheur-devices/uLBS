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
    /// Used to get flat data from java
    /// </summary>
    public class SendEmailJavaCall : JavaCall
    {

        public static SendEmailJavaCallReply SendEmail(JNetBridge.Classes.JnetBridgeLoginUnit JSESSIONID, string recipients)
        {
            return (SendEmailJavaCallReply)new SendEmailJavaCall(JSESSIONID, recipients).DoAction();
        }

        private SendEmailJavaCall(JNetBridge.Classes.JnetBridgeLoginUnit JSESSIONID, string recipients)
            : base(JavaCall.baseURL + "sendEmail.do",
            JSESSIONID,
                new string[] { "cmd", "recipients" },
                new string[] { "sendemail", recipients })
        {

        }


        public static SendEmailJavaCallReply SendMobileEmail(JNetBridge.Classes.JnetBridgeLoginUnit JSESSIONID, string recipient, int Operator, string text)
        {
            return (SendEmailJavaCallReply)new SendEmailJavaCall(JSESSIONID, recipient, Operator, text).DoAction();
        }

        private SendEmailJavaCall(JNetBridge.Classes.JnetBridgeLoginUnit JSESSIONID, string recipient, int Operator, string text)
            : base(JavaCall.baseURL + "sendEmail.do",
            JSESSIONID,
                new string[] { "cmd", "recipient", "Operator", "text" },
                new string[] { "sendmobilemail", recipient, Operator.ToString(), text })
        {

        }


        public static SendEmailJavaCallReply SendSms(JNetBridge.Classes.JnetBridgeLoginUnit JSESSIONID, string text, string recipient)
        {
            return (SendEmailJavaCallReply)new SendEmailJavaCall(JSESSIONID, text, recipient ).DoAction();
        }

        private SendEmailJavaCall(JNetBridge.Classes.JnetBridgeLoginUnit JSESSIONID, string text, string recipient)
            : base(JavaCall.baseURL + "sendSm.do",
            JSESSIONID,
                new string[] { "member", "recipient", "text" },
                new string[] { "testsms", recipient, text  })
        {

        }


        public static SendEmailJavaCallReply SendSupportEmail(JNetBridge.Classes.JnetBridgeLoginUnit JSESSIONID, string text, string recipient, string subject, string originator)
        {
            return (SendEmailJavaCallReply)new SendEmailJavaCall(JSESSIONID, text, recipient, subject, originator).DoAction();
        }

        private SendEmailJavaCall(JNetBridge.Classes.JnetBridgeLoginUnit JSESSIONID, string text, string recipient, string subject, string originator)
            : base(JavaCall.baseURL + "sendEmail.do",
            JSESSIONID,
                new string[] {"cmd",  "member", "recipient", "text", "subject", "originator" },
                new string[] {"sendsupportemail", "testsms", recipient, text, subject, originator })
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
                SendEmailJavaCallReply gmj = new SendEmailJavaCallReply();

                gmj.Reply = reply;
                return gmj;
            }
            catch (Exception ex)
            {
                throw new Exception("Failed parsing xml in SendEmaiJavaCall", ex);
            }

        }


    }
}
