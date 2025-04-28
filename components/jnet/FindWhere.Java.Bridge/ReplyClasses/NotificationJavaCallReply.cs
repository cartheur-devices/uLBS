using System;
using System.Collections.Generic;
using System.Text;
using JNetBridge.InteractionClasses;
using com.teleca.fleetonline.web.bean;

namespace JNetBridge.ReplyClasses
{
    public class NotificationJavaCallReply : JavaCallReply
    {

        private NotificationListDisplayHelper fleetonline_notificationlist;
        private AnswerHelper fleetonline_request_status;
        //private UserData  m_UserData;
        private string ActionMapping;
        
              

        public NotificationJavaCallReply()
        {
        }

        //public NotificationJavaCallReply(SmListDisplayHelper sdh, String reply)
        //{
        //    M_smListDisplayHelper = sdh;
        //    m_reply = reply;

        
        //}

        public NotificationListDisplayHelper NotificationListDisplayHelper { get { return fleetonline_notificationlist ; } set { fleetonline_notificationlist  = value; } }
        public AnswerHelper AnswerHelper { get { return fleetonline_request_status ; } set { fleetonline_request_status  = value; } }


        //public UserData UserData 
        //{
        //    get
        //    {
        //        return m_UserData; 
        //    }
        //    set 
        //    {
        //        m_UserData = value;
                
        //    }
        //}

       


               
    }
}
