using System;
using System.Collections.Generic;
using System.Text;
using JNetBridge.InteractionClasses;
using com.teleca.fleetonline.web.bean;
using com.teleca.fleetonline.repository;

namespace JNetBridge.ReplyClasses
{
    public class WIJavaCallReply : JavaCallReply
    {
        private int level;
        private string speed;
        private string timeZone;
        private LinkedList<MemberData> memberList;
        private ResponseTypeHelper fleetonline_request_type;
        private string intint;
        private string natint;
        private string locationExchange;

        private string error;
        private string confirm;


        public string Speed
        {
            get { return speed; }
            set { speed = value; }
        }
        

        public string TimeZone
        {
            get { return timeZone; }
            set { timeZone = value; }
        }
        
        
        

        public string Intint
        {
            get { return intint; }
            set { intint = value; }
        }
        

        public string Natint
        {
            get { return natint; }
            set { natint = value; }
        }

        public string LocationExchange
        {
            get { return locationExchange; }
            set { locationExchange = value; }
        }

       
        
        private WIConfigDBObject dbObject;
        //private ActionM m_MapDisplayHelper;



        public WIJavaCallReply()
        {
        }

        public LinkedList<MemberData> LinkedList { get { return memberList; } set { memberList = value; } }
        //public MapUrlHelper MapUrlHelper { get { return m_MapUrlHelper; } set { m_MapUrlHelper = value; } }
        public ResponseTypeHelper ResponseTypeHelper { get { return fleetonline_request_type; } set { fleetonline_request_type = value; } }
        public WIConfigDBObject WIConfigDBObject { get { return dbObject; } set { dbObject = value; } }
        public int Level
        {
            get { return level; }
            set { level = value; }
        }

        public String Error { get { return error; } set { error = value; } }
        public String Confirm { get { return confirm; } set { confirm = value; } }
    }
}
