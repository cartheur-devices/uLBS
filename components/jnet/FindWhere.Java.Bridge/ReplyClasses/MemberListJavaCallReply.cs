using System;
using System.Collections.Generic;
using System.Text;
using JNetBridge.InteractionClasses;
using com.teleca.fleetonline.web.bean;

namespace JNetBridge.ReplyClasses
{
    public class MemberListJavaCallReply : JavaCallReply
    {
        private AnswerHelper fleetonline_request_status;
        private MemberListDisplayHelper fleetonline_groups;
        private string m_ActionMapping;
        
        //TODO: ADD
        //private MemberForm m_MemberForm;



        public MemberListJavaCallReply()
        {
        }


        public AnswerHelper AnswerHelper { get { return fleetonline_request_status; } set { fleetonline_request_status = value; } }
        public MemberListDisplayHelper MemberListDisplayHelper { get { return fleetonline_groups; } set { fleetonline_groups = value; } }
        public string ActionMapping { get { return m_ActionMapping; } set { m_ActionMapping = value; } }

        
        //TODO: ADD
        //public MemberForm Reply { get { return m_MemberForm; } set { m_MemberForm = value; } }
       
    }
}
