//using System;
//using System.Collections.Specialized;
//using System.Text;
//using System.Configuration;

////using log4net; // Used for XML logging
////using log4net.Config; // Used for XML logging

//namespace WIMP
//{
//    class LogWriter
//    {
//        // Used for logging
//        //private static readonly ILog logger =
//        //   LogManager.GetLogger(typeof(LogWriter));

//        private string _application;

//        public LogWriter()
//        {
//            //XmlConfigurator.Configure();
//        }

//        public LogWriter(string Application)
//        {
//            //XmlConfigurator.Configure();

//            _application = Application;
//        }

//        /// <summary>
//        /// Write logging message to Trace Listeners.
//        /// </summary>
//        public void Write2Log(string Application, string Message, string Details, string ClientID, string RemoteEP)
//        {
//            try
//            {
//                //logger.Debug(Details + ", " + Message);
//                //_log.Application = Application;
//                //_log.Details = Details;
//                //_log.LogType = LogType.Information;
//                //_log.Status = LogStatus.Success;
//                //_log.Message = Message;
//                //_log.Identifier = ClientID + "***" + RemoteEP;
//                //_log.LogTime = DateTime.Now;
//                //_logger.Log(_log);

//            }
//            catch
//            {
//            }
//        }

//        /// <summary>
//        /// Write logging message to Trace Listeners.
//        /// </summary>
//        public void Write2Log(string Message, string Details, string ClientID, string RemoteEP)
//        {
//            Write2Log(_application, Message, Details, ClientID, RemoteEP);
//        }

//        /// <summary>
//        /// Write error logging message to Trace Listeners.
//        /// </summary>
//        public void WriteError2Log(string Application, string Message, Exception ex, string ClientID, string RemoteEP)
//        {
//            try
//            {
//                //logger.Debug(Message, ex);
//                //_log.Application = Application;
//                //_log.Details = ex.Message + Environment.NewLine + ex.StackTrace + Environment.NewLine + ex.Source;
//                //_log.LogType = LogType.Error;
//                //_log.Status = LogStatus.Failure;
//                //_log.Message = Message;
//                //_log.Identifier = ClientID + "***" + RemoteEP;
//                //_log.LogTime = DateTime.Now;
//                //_logger.Log(_log);
//            }
//            catch
//            {
//            }
//        }

//        /// <summary>
//        /// Write error logging message to Trace Listeners.
//        /// </summary>
//        public void WriteError2Log(string Message, Exception ex, string ClientID, string RemoteEP)
//        {
//            WriteError2Log(_application, Message, ex, ClientID, RemoteEP);
//        }
//    }
//}
