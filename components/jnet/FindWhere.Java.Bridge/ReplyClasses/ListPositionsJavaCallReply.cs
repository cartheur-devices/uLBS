using System;
using System.Collections.Generic;
using System.Text;
using JNetBridge.InteractionClasses;
using com.teleca.fleetonline.web.bean;

namespace JNetBridge.ReplyClasses
{
    public class ListPositionJavaCallReply : JavaCallReply
    {
        //TODO: remove times
        public DateTime StartTime;
        public DateTime EndTime;

        private MiscContentHelper m_MiscContentHelper;
        //private MapUrlHelper m_MapUrlHelper;
        private AnswerHelper fleetonline_request_status;
        private ResponseTypeHelper fleetonline_request_type;
        private string m_ActionMapping;

        private PositionsDisplayHelper fleetonline_positions;
        //private MapDisplayHelper m_MapDisplayHelper;
        //private ZoomLevelHelper m_ZoomLevelHelper;
        //private UserData m_UserData;


        public ListPositionJavaCallReply(){}

        //public NotificationJavaCallReply(SmListDisplayHelper sdh, String reply)
        //{
        //    M_smListDisplayHelper = sdh;
        //    m_reply = reply;

        
        //}

        public MiscContentHelper MiscContentHelper { get { return m_MiscContentHelper; } set { m_MiscContentHelper = value; } }
        //public MapUrlHelper MapUrlHelper { get { return m_MapUrlHelper; } set { m_MapUrlHelper = value; } }
        public AnswerHelper AnswerHelper { get { return fleetonline_request_status; } set { fleetonline_request_status = value; } }
        public ResponseTypeHelper ResponseTypeHelper { get { return fleetonline_request_type; } set { fleetonline_request_type = value; } }
        public String ActionMapping { get { return m_ActionMapping; } set { m_ActionMapping = value; } }
        //public MapDisplayHelper MapDisplayHelper { get { return m_MapDisplayHelper; } set { m_MapDisplayHelper = value; } }
        //public ZoomLevelHelper ZoomLevelHelper { get { return m_ZoomLevelHelper; } set { m_ZoomLevelHelper = value; } }
        public PositionsDisplayHelper PositionsDisplayHelper { get { return fleetonline_positions; } set { fleetonline_positions = value; } }


        
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
