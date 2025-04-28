using System;
using System.Collections.Generic;
using System.Text;
using JNetBridge.InteractionClasses;
using com.teleca.fleetonline.web.bean;
using com.teleca.fleetonline.repository;

namespace JNetBridge.ReplyClasses
{
    public class TTProfileJavaCallReply : JavaCallReply
    {
        private MemberData[] members2; 
        private ResponseTypeHelper m_ResponseTypeHelper;
        private ProfileData[] profiles;

        private string error;
        private string confirm;


        public TTProfileJavaCallReply()
        {
        }

        public String Error { get { return error; } set { error = value; } }
        public String Confirm { get { return confirm; } set { confirm = value; } }
        public MemberData[] MemberData { get { return members2; } set { members2 = value; } }
        public ResponseTypeHelper ResponseTypeHelper { get { return m_ResponseTypeHelper; } set { m_ResponseTypeHelper = value; } }
        public ProfileData[] ProfileData { get { return profiles; } set { profiles = value; } }
        

    }
}
