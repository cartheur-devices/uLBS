using System;
using System.Collections.Generic;
using System.Text;
using JNetBridge.InteractionClasses;
using com.teleca.fleetonline.web.bean;

namespace JNetBridge.ReplyClasses
{
    public class MessagesJavaCallReply : JavaCallReply
    {
        //TODO: remove times
        public DateTime StartTime;
        public DateTime EndTime;
        private AnswerHelper fleetonline_request_status;

        public AnswerHelper Fleetonline_request_status
        {
            get { return fleetonline_request_status; }
            set { fleetonline_request_status = value; }
        }

        private Boolean newMessages;

        public Boolean NewMessages
        {
            get { return newMessages; }
            set { newMessages = value; }
        }

        private SmListDisplayHelper fleetonline_sm_messages;
        //private UserData  m_UserData;
        
              

        public MessagesJavaCallReply()
        {
        }

        public MessagesJavaCallReply(SmListDisplayHelper sdh, String reply)
        {
            fleetonline_sm_messages = sdh;
            Reply = reply;

        
        }

        public SmListDisplayHelper SmListDisplayHelper { get { return fleetonline_sm_messages; } set { fleetonline_sm_messages = value; } }


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
