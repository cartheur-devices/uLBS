using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.IO;
using JNetBridge.ReplyClasses;
using System.Diagnostics;
using JNetBridge.Classes;
using log4net;

namespace JNetBridge
{
    /// <summary>
    /// This base class abstracts the details of connecting and retreiving the response, leaving only calling the method with
    /// the correct parameters and parsing the server reply to derived classes.
    /// </summary>
    public class JavaCall
    {

        public static string baseURL = "";
        private string m_url;
        private string m_whiteLabelName;
        private System.Net.Cookie m_JSESSIONID;
        private string[] m_paramNames;
        private string[] m_paramValues;

        protected static readonly ILog log = LogManager.GetLogger(typeof(JavaCall));

        /// <summary>
        /// Initializes a new instance of the <see cref="JavaCall"/> class.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <param name="loginUnit">The login unit.</param>
        /// <param name="paramNames">The param names.</param>
        /// <param name="paramValues">The param values.</param>
        public JavaCall(string url, JnetBridgeLoginUnit loginUnit, string[] paramNames, string[] paramValues)
        {
            if (loginUnit == null)
            {
                log.Error("loginUnit is null");
                throw new Exception("loginUnit is null"); 
            }

            if (string.IsNullOrEmpty(loginUnit.WhiteLabelName))
            {
                log.Error("Whitelabel cannot be empty");
                throw new Exception("Whitelabel cannot be empty");
            }

            if (loginUnit.JSESSIONID != null)
                m_JSESSIONID = loginUnit.JSESSIONID;
            m_paramNames = paramNames;
            m_paramValues = paramValues;
            m_whiteLabelName = loginUnit.WhiteLabelName;
            string settingName = string.Empty;

#if DEBUG

             settingName = string.Concat("baseFleetOnlineURL", "_", System.Environment.MachineName);


            if (!String.IsNullOrEmpty(System.Configuration.ConfigurationSettings.AppSettings[settingName]))
            {
                m_url = System.Configuration.ConfigurationSettings.AppSettings[settingName];
            }
            else
            {
                settingName = string.Concat("baseFleetOnlineURL", "_", m_whiteLabelName);
                m_url = System.Configuration.ConfigurationSettings.AppSettings[settingName];
            }
#else
             settingName = string.Concat("baseFleetOnlineURL", "_", m_whiteLabelName);
             m_url = System.Configuration.ConfigurationSettings.AppSettings[settingName];
#endif
             log.Info("baseFleetOnlineURL Settingname: " + settingName);

            if (m_url == null)
            {
                log.Error(String.Concat("Cannot find connectionstring for label:", m_whiteLabelName));
                throw new Exception(String.Concat("Cannot find connectionstring for label:" , m_whiteLabelName)); }

            m_url = string.Concat(m_url, url);
        }

        /// <summary>
        /// Does the action.
        /// </summary>
        /// <returns></returns>
        protected JavaCallReply DoAction()
        {
            // An example request: http://l006.webintegration.local:8080/fleetonline/aspLogin.do?userName=Sjakie"
            StringBuilder requestString = new StringBuilder(m_url);
            if (m_paramNames.Length != m_paramValues.Length)
                throw new InvalidOperationException("JavaCall.Doaction: Invalid params");

            // first we build the string.
            // if no parameters, just send the URL
            // otherwise, append the paramaters.
            if (!(m_paramNames.Length == 0))
            {
                requestString.Append('?');
                for (int i = 0; i < m_paramNames.Length; ++i)
                {
                    requestString.Append(m_paramNames[i]);
                    requestString.Append('=');
                    requestString.Append(m_paramValues[i]);
                    requestString.Append('&');
                }
                // remove last '?'
                requestString.Remove(requestString.Length - 1, 1);
            }
            // next. do the request, and synchronously (!!) wait for the reply
            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(requestString.ToString());

            //extreme timeout; (debug time @ Java....)
            //httpWebRequest.Timeout = 6000000; 

            //httpWebRequest.SendChunked = true;
            //httpWebRequest.TransferEncoding = "gzip";
            httpWebRequest.Method = "POST";
            //TODO use a normal timout or -> app.config
            httpWebRequest.CookieContainer = new CookieContainer();
            
           
            if (m_JSESSIONID != null)
            {
                httpWebRequest.CookieContainer.Add(m_JSESSIONID);
            }


            HttpWebResponse httpWebResponse;
            Cookie JsessionID = null;
            StreamReader streamReader;
            string reply;

            DateTime startTime = DateTime.Now;

            //log.Info(String.Concat("Javacall:", requestString));
            Debug.WriteLine("Javacall|Before|" + requestString + "|time|" + DateTime.Now.ToLongTimeString());
            try
            {
                httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                streamReader = new StreamReader(httpWebResponse.GetResponseStream());
                reply = streamReader.ReadToEnd();

                JsessionID = httpWebResponse.Cookies["JSESSIONID"];
                //TODO: add!
//#if DEBUG
//                if (reply.Contains("Unknown_Type_in_Request_messages.jsp"))
//                    throw new Exception("Unknown_Type_in_Request_messages.jsp");
//#endif

            }
            catch (WebException e)
            {
                if (e.Response != null)
                {
                    streamReader = new StreamReader(e.Response.GetResponseStream());
                    reply = e.Message + "\r\nDetails:\r\n\r\n" + streamReader.ReadToEnd();
                }
                else
                {
                    reply = "";
                }
                TimeSpan duration1 = DateTime.Now - startTime;
                Debug.WriteLine("Javacall|On Parse|" + requestString + "|size|" + reply.Length + "|duration|" + duration1 + "|time|" + DateTime.Now.ToLongTimeString());
                log.Error("Javacall|On Parse|" + requestString + "|size|" + reply.Length + "|duration|" + duration1 + "|time|" , e);
                throw new Exception("webrequest: " + httpWebRequest.RequestUri.AbsoluteUri, e);
                
            }

            TimeSpan duration2 = DateTime.Now  - startTime;
            Debug.WriteLine("Javacall|On Parse|" + requestString + "|size|" + reply.Length + "|duration|" + duration2 + "|time|" + DateTime.Now.ToLongTimeString());
            log.Info("Javacall|On Parse|" + requestString + "|size|" + reply.Length + "|duration|" + duration2 );
            startTime = DateTime.Now;
            JavaCallReply returnObject = ParseServerReply(reply, JsessionID);

            duration2 = DateTime.Now - startTime;
            Debug.WriteLine("Javacall|End Parse|" + requestString + "|size|" + reply.Length + "|duration|" + duration2 + "|time|" + DateTime.Now.ToLongTimeString());
            log.Info("Javacall|End Parse|" + requestString + "|size|" + reply.Length + "|duration|" + duration2 );

            return returnObject;
            
        }

        // this call should always be delegated to derived classes
        /// <summary>
        /// Parses the server reply.
        /// </summary>
        /// <param name="reply">The reply.</param>
        /// <param name="JsessionID">The jsession ID.</param>
        /// <returns></returns>
        protected virtual JavaCallReply ParseServerReply(string reply, Cookie JsessionID)
        {
            throw new NotImplementedException();
        }

    }
}
