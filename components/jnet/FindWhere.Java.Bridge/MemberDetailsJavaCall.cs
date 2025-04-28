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
    public class MemberDetailsJavaCall : JavaCall
    {
/// <summary>
/// UNUSED
/// </summary>
/// <param name="JSESSIONID"></param>
/// <returns></returns>
        public static MemberDetailsJavaCallReply Get(JNetBridge.Classes.JnetBridgeLoginUnit JSESSIONID)
        {
            return (MemberDetailsJavaCallReply)new MemberDetailsJavaCall(JSESSIONID).DoAction();
        }



        private MemberDetailsJavaCall(JNetBridge.Classes.JnetBridgeLoginUnit JSESSIONID)
            : base(JavaCall.baseURL + "popup_viewmemberdetails.jsp",
            JSESSIONID,
                new string[] {},
                new string[] {})
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
                MemberDetailsJavaCallReply gmj = new MemberDetailsJavaCallReply();

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



       
    }
}
