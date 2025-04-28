using System;
using System.Collections.Generic;
using System.Text;
using JNetBridge.InteractionClasses;
using com.teleca.fleetonline.web.bean;

namespace JNetBridge.ReplyClasses
{
    public class LastPositionJavaCallReply : JavaCallReply
    {
        private AnswerHelper fleetonline_request_status;
        private ResponseTypeHelper fleetonline_request_type;
        private MapDisplayHelper fleetonline_trace_data;
        private MiscContentHelper fleetonline_misc_content;
        private MiscContentHelper fleetonline_error_content;

        public MiscContentHelper Fleetonline_error_content
        {
            get { return fleetonline_error_content; }
            set { fleetonline_error_content = value; }
        }
        private MapUrlHelper m_MapUrlHelper;
        
        
        private string m_ActionMapping;

        
        private ZoomLevelHelper m_ZoomLevelHelper;
        //private UserData m_UserData;


        public LastPositionJavaCallReply()
        {
        }

        //public NotificationJavaCallReply(SmListDisplayHelper sdh, String reply)
        //{
        //    M_smListDisplayHelper = sdh;
        //    m_reply = reply;

        
        //}

        public MiscContentHelper MiscContentHelper { get { return fleetonline_misc_content; } set { fleetonline_misc_content = value; } }
        public MapUrlHelper MapUrlHelper { get { return m_MapUrlHelper; } set { m_MapUrlHelper = value; } }
        public AnswerHelper AnswerHelper { get { return fleetonline_request_status; } set { fleetonline_request_status = value; } }
        public ResponseTypeHelper ResponseTypeHelper { get { return fleetonline_request_type; } set { fleetonline_request_type = value; } }
        public String ActionMapping { get { return m_ActionMapping; } set { m_ActionMapping = value; } }
        public MapDisplayHelper MapDisplayHelper { get { return fleetonline_trace_data; } set { fleetonline_trace_data = value; } }
        public ZoomLevelHelper ZoomLevelHelper { get { return m_ZoomLevelHelper; } set { m_ZoomLevelHelper = value; } }


        
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
