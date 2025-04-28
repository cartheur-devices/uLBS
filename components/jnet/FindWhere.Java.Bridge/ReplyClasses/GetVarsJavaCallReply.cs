using System;
using System.Collections.Generic;
using System.Text;
using JNetBridge.InteractionClasses;
using com.teleca.fleetonline.web.bean;
using System.Net;
using com.teleca.fleetonline.utils;

namespace JNetBridge.ReplyClasses
{
    public class GetVarsJavaCallReply : JavaCallReply
    {
        private FOLProperties m_FOLProperties;
        
        public GetVarsJavaCallReply()
        { 
        }


        public FOLProperties FOLProperties { get { return m_FOLProperties; } set { m_FOLProperties = value; } }


               
    }
}
