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
using System.Web;

namespace JNetBridge
{
    /// <summary>
    /// 
    /// </summary>
    public class ContactsJavaCall : JavaCall
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="JSESSIONID"></param>
        /// <param name="contacts"></param>
        /// <returns></returns>
        public static ContactsJavaCallReply Save(JNetBridge.Classes.JnetBridgeLoginUnit JSESSIONID, LinkedList<ContactData> contacts)
        {
            int highestIndex = 0;
            // 1e contacts doorlopen om hoogste index te bepalen
            foreach (ContactData cd in contacts)
            {
                if (cd.IndexNr > highestIndex)
                    highestIndex = cd.IndexNr;
            }

            // 2e contacts doorlopen en ontbrekende indexen vullen
            foreach (ContactData cd in contacts)
            {
                if (cd.IndexNr < 1)
                {
                    // set the index
                    highestIndex++;
                    cd.IndexNr = highestIndex;
                }
            }

            // start filling params / paramvalues
            ArrayList alParamNames = new ArrayList();
            ArrayList alParamValues = new ArrayList();

            alParamNames.Add("cmd");
            alParamValues.Add("set");

            int i = 1;
            foreach (ContactData cd in contacts)
            {
                alParamNames.Add("name" + i.ToString());
                if (cd.Name == null)
                    cd.Name = "";

                alParamValues.Add(cd.Name);

                alParamNames.Add("phone" + i.ToString());

                if (cd.Phone == null)
                    cd.Phone = "";
                alParamValues.Add(HttpUtility.UrlEncode(cd.Phone));

                alParamNames.Add("email" + i.ToString());
                if (cd.Email == null)
                    cd.Email = "";
                alParamValues.Add(cd.Email);

                alParamNames.Add("index" + i.ToString());
                alParamValues.Add(cd.IndexNr.ToString());

                alParamNames.Add("provider" + i.ToString());
                if (cd.Provider == null)
                    cd.Provider = -1;
                alParamValues.Add(cd.Provider.ToString());
                i++;
            }

            alParamNames.Add("nrOfContacts");
            alParamValues.Add(contacts.Count.ToString());

            alParamNames.Add("nextIndex");
            alParamValues.Add(highestIndex.ToString());

            string[] paramNames = (string[])alParamNames.ToArray(typeof(string));
            string[] paramValues = (string[])alParamValues.ToArray(typeof(string));




            return (ContactsJavaCallReply)new ContactsJavaCall(JSESSIONID, paramNames, paramValues).DoAction();
        }



        private ContactsJavaCall(JNetBridge.Classes.JnetBridgeLoginUnit JSESSIONID, string[] paramNames, string[] paramValues)
            : base(JavaCall.baseURL + "setContacts.do",
            JSESSIONID, paramNames, paramValues)
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
                if (System.Environment.MachineName == "TEYDO11")
                {
                    //xdMain.Save(@"c:\ReceivedXml.xml");
                }
#endif


                XmlDocument workingXmlDoc;
                workingXmlDoc = new XmlDocument();

                ContactsJavaCallReply gmj = new ContactsJavaCallReply();
                Utils.FillProperties(gmj, xdMain.SelectSingleNode("//main"));

                gmj.Reply = reply;
                return gmj;
            }
            catch (Exception ex)
            {
                throw new Exception("Failed parsing xml in ContactsJavaCallReply", ex);
            }
        }

    }
}
