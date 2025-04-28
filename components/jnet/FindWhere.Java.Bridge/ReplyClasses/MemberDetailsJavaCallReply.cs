﻿using System;
using System.Collections.Generic;
using System.Text;
using JNetBridge.InteractionClasses;
using com.teleca.fleetonline.web.bean;

namespace JNetBridge.ReplyClasses
{
    public class MemberDetailsJavaCallReply : JavaCallReply
    {
        private OcellusProfileDisplayHelper m_OcellusProfileDisplayHelper;

        private string error;
        private string confirm;

        //private ActionMapping m_ActionMapping;


        public MemberDetailsJavaCallReply()
        {
        }


        public OcellusProfileDisplayHelper OcellusProfileDisplayHelper { get { return m_OcellusProfileDisplayHelper; } set { m_OcellusProfileDisplayHelper = value; } }


        public String Error { get { return error; } set { error = value; } }
        public String Confirm { get { return confirm; } set { confirm = value; } }
        


        //public MapUrlHelper MapUrlHelper { get { return m_MapUrlHelper; } set { m_MapUrlHelper = value; } }
        //public ResponseTypeHelper ResponseTypeHelper { get { return m_ResponseTypeHelper; } set { m_ResponseTypeHelper = value; } }
        //public MapDisplayHelper SmListDisplayHelper { get { return m_MapDisplayHelper; } set { m_MapDisplayHelper = value; } }
        

    }
}
