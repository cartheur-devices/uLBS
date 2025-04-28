using System;
using System.Collections.Generic;
using System.Text;
using JNetBridge.InteractionClasses;
using com.teleca.fleetonline.web.bean;
using com.teleca.fleetonline.repository;

namespace JNetBridge.ReplyClasses
{
    public class EnforaJavaCallReply : JavaCallReply
    {
        private string level;

        public string Level
        {
            get { return level; }
            set { level = value; }
        }
        private string timeZone;

        public string TimeZone
        {
            get { return timeZone; }
            set { timeZone = value; }
        }
        private string intdaily;

        public string Intdaily
        {
            get { return intdaily; }
            set { intdaily = value; }
        }
        private string intmotion;

        public string Intmotion
        {
            get { return intmotion; }
            set { intmotion = value; }
        }
        private LinkedList<MemberData> memberList;
        private ResponseTypeHelper fleetonline_request_type;
        private string intnomotion;

        private EnforaIO enforaIO;

        public EnforaIO EnforaIO
        {
            get { return enforaIO; }
            set { enforaIO = value; }
        }

        public string Intnomotion
        {
            get { return intnomotion; }
            set { intnomotion = value; }
        }
        private EnforaConfigDBObject dbObject;
        private string intnogps;

        public string Intnogps
        {
            get { return intnogps; }
            set { intnogps = value; }
        }



        //private MiscContentHelper m_MiscContentHelper;
        //private MapUrlHelper m_MapUrlHelper;
        
        
        

        private string error;

      
        private string confirm;

        public string Confirm
        {
            get { return confirm; }
            set { confirm = value; }
        }
        public string Error
        {
            get { return error; }
            set { error = value; }
        }


        public EnforaJavaCallReply()
        {
        }

        public LinkedList<MemberData> LinkedList { get { return memberList; } set { memberList = value; } }
        //public MiscContentHelper MiscContentHelper { get { return m_MiscContentHelper; } set { m_MiscContentHelper = value; } }
        //public MapUrlHelper MapUrlHelper { get { return m_MapUrlHelper; } set { m_MapUrlHelper = value; } }
        public ResponseTypeHelper ResponseTypeHelper { get { return fleetonline_request_type; } set { fleetonline_request_type = value; } }
        public EnforaConfigDBObject EnforaConfigDBObject { get { return dbObject; } set { dbObject = value; } }


    }
}
