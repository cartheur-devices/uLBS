using System;
using System.Collections.Generic;
using System.Text;
using JNetBridge.InteractionClasses;
using com.teleca.fleetonline.web.bean;

namespace JNetBridge.ReplyClasses
{
    public class SendEmailJavaCallReply : JavaCallReply
    {
       


        public SendEmailJavaCallReply()
        {
        }


        private string error;
        private string confirm;

        public String Error { get { return error; } set { error = value; } }
        public String Confirm { get { return confirm; } set { confirm = value; } }


        

       


               
    }
}
