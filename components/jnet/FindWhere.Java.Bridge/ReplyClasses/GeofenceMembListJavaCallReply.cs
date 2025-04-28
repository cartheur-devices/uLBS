using System;
using System.Collections.Generic;
using System.Text;
using JNetBridge.InteractionClasses;
using com.teleca.fleetonline.web.bean;

namespace JNetBridge.ReplyClasses
{
    public class GeofenceMembListJavaCallReply : JavaCallReply
    {
        private GeoFenceMembListDisplayHelper fleetonline_geofencememblist;
        private AnswerHelper fleetonline_request_status;
        private MiscContentHelper m_MiscContentHelper;
        //private UserData m_UserData;

        public GeofenceMembListJavaCallReply()
        {
        }

        public GeofenceMembListJavaCallReply(GeoFenceMembListDisplayHelper gdh, String reply)
        {
            fleetonline_geofencememblist = gdh;
            this.Reply = reply;
        }

        public GeoFenceMembListDisplayHelper GeoFenceMembListDisplayHelper { get { return fleetonline_geofencememblist; } set { fleetonline_geofencememblist = value; } }
        public AnswerHelper AnswerHelper { get { return fleetonline_request_status; } set { fleetonline_request_status = value; } }
        public MiscContentHelper MiscContentHelper { get { return m_MiscContentHelper; } set { m_MiscContentHelper = value; } }


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
