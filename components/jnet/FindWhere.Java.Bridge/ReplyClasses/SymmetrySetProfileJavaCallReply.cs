﻿using System;
using System.Collections.Generic;
using System.Text;
using JNetBridge.InteractionClasses;
using com.teleca.fleetonline.web.bean;

namespace JNetBridge.ReplyClasses
{
    public class SymmetrySetProfileJavaCallReply : JavaCallReply
    {

        private SymmetryProfileDisplayHelper symmetry_profile;

        public SymmetryProfileDisplayHelper Symmetry_profile
        {
            get { return symmetry_profile; }
            set { symmetry_profile = value; }
        }

        private AnswerHelper fleetonline_request_status;

        public AnswerHelper Fleetonline_request_status
        {
            get { return fleetonline_request_status; }
            set { fleetonline_request_status = value; }
        }


        //private OcellusProfileDisplayHelper fleetonline_ocellusprofile;
        //private MiscContentHelper fleetonline_error_content;

        //public MiscContentHelper Fleetonline_error_content
        //{
        //    get { return fleetonline_error_content; }
        //    set { fleetonline_error_content = value; }
        //}
        private string error;
        private string confirm;

        //private ActionMapping m_ActionMapping;


        public SymmetrySetProfileJavaCallReply()
        {
        }


        //public OcellusProfileDisplayHelper OcellusProfileDisplayHelper { get { return fleetonline_ocellusprofile; } set { fleetonline_ocellusprofile = value; } }


        public String Error { get { return error; } set { error = value; } }
        public String Confirm { get { return confirm; } set { confirm = value; } }
        


        //public MapUrlHelper MapUrlHelper { get { return m_MapUrlHelper; } set { m_MapUrlHelper = value; } }
        //public ResponseTypeHelper ResponseTypeHelper { get { return m_ResponseTypeHelper; } set { m_ResponseTypeHelper = value; } }
        //public MapDisplayHelper SmListDisplayHelper { get { return m_MapDisplayHelper; } set { m_MapDisplayHelper = value; } }
        

    }
}
