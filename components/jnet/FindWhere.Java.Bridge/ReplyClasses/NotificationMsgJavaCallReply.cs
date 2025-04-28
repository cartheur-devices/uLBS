using System;
using System.Collections.Generic;
using System.Text;
using JNetBridge.InteractionClasses;
using com.teleca.fleetonline.web.bean;

namespace JNetBridge.ReplyClasses
{
    public class NotificationMsgJavaCallReply : JavaCallReply
    {
        private Boolean newMessages;

        public Boolean NewMessages
        {
            get { return newMessages; }
            set { newMessages = value; }
        }

        private NotifMsgListDisplayHelper fleetonline_notification_msg;
        private AnswerHelper fleetonline_request_status;
        private ResponseTypeHelper fleetonline_request_type;

        public ResponseTypeHelper Fleetonline_request_type
        {
            get { return fleetonline_request_type; }
            set { fleetonline_request_type = value; }
        }
        //private UserData  m_UserData;
        private string ActionMapping;



        public NotificationMsgJavaCallReply()
        {
        }

        //public NotificationJavaCallReply(SmListDisplayHelper sdh, String reply)
        //{
        //    M_smListDisplayHelper = sdh;
        //    m_reply = reply;

        
        //}

        public NotifMsgListDisplayHelper NotifMsgListDisplayHelper { get { return fleetonline_notification_msg; } set { fleetonline_notification_msg = value; } }
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
