using System;
using System.Collections.Generic;
using System.Text;
using JNetBridge.InteractionClasses;
using com.teleca.fleetonline.web.bean;

namespace JNetBridge.ReplyClasses
{
    public class InterfaceJavaCallReply : JavaCallReply
    {
       
        private string m_reply;

        private string error;
        private string confirm;
        private MiscContentHelper fleetonline_error_content;

        public MiscContentHelper MiscContentHelper
        {
            get { return fleetonline_error_content; }
            set { fleetonline_error_content = value; }
        }



        public InterfaceJavaCallReply()
        {
        }

      

        public String Reply { get { return m_reply; } set { m_reply = value; } }

        public String Error { get { return error; } set { error = value; } }
        public String Confirm { get { return confirm; } set { confirm = value; } }
       


               
    }
}
