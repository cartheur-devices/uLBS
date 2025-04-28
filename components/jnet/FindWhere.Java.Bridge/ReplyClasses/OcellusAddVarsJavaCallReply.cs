using System;
using System.Collections.Generic;
using System.Text;
using JNetBridge.InteractionClasses;
using com.teleca.fleetonline.web.bean;

namespace JNetBridge.ReplyClasses
{
    public class OcellusAddVarsJavaCallReply : JavaCallReply
    {
        private AnswerHelper fleetonline_request_status;
        private ResponseTypeHelper fleetonline_request_type;
        private string m_ActionMapping;
        private OcellusAdditionalVariablesDisplayHelper ocellus_addvars;

        
        public OcellusAddVarsJavaCallReply()
        {
        }


        public AnswerHelper AnswerHelper { get { return fleetonline_request_status; } set { fleetonline_request_status = value; } }
        public ResponseTypeHelper ResponseTypeHelper { get { return fleetonline_request_type; } set { fleetonline_request_type = value; } }
        public string ActionMapping { get { return m_ActionMapping; } set { m_ActionMapping = value; } }
        public OcellusAdditionalVariablesDisplayHelper OcellusAdditionalVariablesDisplayHelper { get { return ocellus_addvars; } set { ocellus_addvars = value; } }



       

       


               
    }
}
