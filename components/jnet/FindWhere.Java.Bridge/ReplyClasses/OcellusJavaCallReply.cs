using System;
using System.Collections.Generic;
using System.Text;
using JNetBridge.InteractionClasses;
using com.teleca.fleetonline.web.bean;

namespace JNetBridge.ReplyClasses
{
    public class OcellusJavaCallReply : JavaCallReply
    {

        private OcellusProfileDisplayHelper fleetonline_ocellusprofile;
        //private AnswerHelper m_AnswerHelper;
        //private ResponseTypeHelper m_ResponseTypeHelper;
        //private MapDisplayHelper m_MapDisplayHelper;
        //private MiscContentHelper m_MiscContentHelper;
        
        //private string error;
        //private string confirm;



        #region needed for the set function (ocellusprofilevalidation.jsp)
        private MiscContentHelper fleetonline_error_content;

        public MiscContentHelper Fleetonline_error_content
        {
            get { return fleetonline_error_content; }
            set { fleetonline_error_content = value; }
        }
        private AnswerHelper fleetonline_request_status;

        public AnswerHelper Fleetonline_request_status
        {
            get { return fleetonline_request_status; }
            set { fleetonline_request_status = value; }
        }
        private ResponseTypeHelper ResponseTypeHelper;

        public ResponseTypeHelper ResponseTypeHelper1
        {
            get { return ResponseTypeHelper; }
            set { ResponseTypeHelper = value; }
        }
        private MiscContentHelper fleetonline_misc_content;

        public MiscContentHelper Fleetonline_misc_content
        {
            get { return fleetonline_misc_content; }
            set { fleetonline_misc_content = value; }
        }
        #endregion

        private string error;
        private string confirm;

        public String Error { get { return error; } set { error = value; } }
        public String Confirm { get { return confirm; } set { confirm = value; } }

        public OcellusJavaCallReply()
        {
        }

        public OcellusProfileDisplayHelper OcellusProfileDisplayHelper { get { return fleetonline_ocellusprofile; } set { fleetonline_ocellusprofile = value; } }
        //public AnswerHelper AnswerHelper { get { return m_AnswerHelper; } set { m_AnswerHelper = value; } }
        //public ResponseTypeHelper ResponseTypeHelper { get { return m_ResponseTypeHelper; } set { m_ResponseTypeHelper = value; } }
        //public MapDisplayHelper MapDisplayHelper { get { return m_MapDisplayHelper; } set { m_MapDisplayHelper = value; } }
        //public MiscContentHelper MiscContentHelper { get { return m_MiscContentHelper; } set { m_MiscContentHelper = value; } }

        //public String Error { get { return error; } set { error = value; } }
        //public String Confirm { get { return confirm; } set { confirm = value; } }


        //public MapUrlHelper MapUrlHelper { get { return m_MapUrlHelper; } set { m_MapUrlHelper = value; } }
        //public ResponseTypeHelper ResponseTypeHelper { get { return m_ResponseTypeHelper; } set { m_ResponseTypeHelper = value; } }
        //public MapDisplayHelper SmListDisplayHelper { get { return m_MapDisplayHelper; } set { m_MapDisplayHelper = value; } }
        

    }
}
