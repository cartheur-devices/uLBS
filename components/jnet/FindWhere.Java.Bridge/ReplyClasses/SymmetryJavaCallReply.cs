using System;
using System.Collections.Generic;
using System.Text;
using JNetBridge.InteractionClasses;
using com.teleca.fleetonline.web.bean;
using com.teleca.fleetonline.repository;

namespace JNetBridge.ReplyClasses
{
    public class SymmetryJavaCallReply : JavaCallReply
    {


        private SymConfigData config;
        private LinkedList<MemberData> memberList;

        public LinkedList<MemberData> MemberList
        {
            get { return memberList; }
            set { memberList = value; }
        }

        public SymConfigData Config
        {
            get { return config; }
            set { config = value; }
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


        public SymmetryJavaCallReply()
        {
        }

        //public LinkedList<MemberData> LinkedList { get { return memberList; } set { memberList = value; } }
        ////public MiscContentHelper MiscContentHelper { get { return m_MiscContentHelper; } set { m_MiscContentHelper = value; } }
        ////public MapUrlHelper MapUrlHelper { get { return m_MapUrlHelper; } set { m_MapUrlHelper = value; } }
        //public ResponseTypeHelper ResponseTypeHelper { get { return fleetonline_request_type; } set { fleetonline_request_type = value; } }
        //public EnforaConfigDBObject EnforaConfigDBObject { get { return dbObject; } set { dbObject = value; } }


    }
}
