using System;
using System.Collections.Generic;
using System.Text;
using JNetBridge.InteractionClasses;
using com.teleca.fleetonline.web.bean;
using System.Net;
using com.teleca.fleetonline.utils;

namespace JNetBridge.ReplyClasses
{
    public class TemplatesJavaCallReply : JavaCallReply
    {

        private AnswerHelper fleetonline_request_status;

        public AnswerHelper Fleetonline_request_status
        {
            get { return fleetonline_request_status; }
            set { fleetonline_request_status = value; }
        }
        private TemplatesDisplayHelper fleetonline_templates;

        public TemplatesDisplayHelper Fleetonline_templates
        {
            get { return fleetonline_templates; }
            set { fleetonline_templates = value; }
        }
 


        

        public TemplatesJavaCallReply()
        { 
        }


        



               
    }
}
