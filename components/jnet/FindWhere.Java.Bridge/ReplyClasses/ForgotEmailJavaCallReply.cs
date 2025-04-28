using System;
using System.Collections.Generic;
using System.Text;
using JNetBridge.InteractionClasses;
using com.teleca.fleetonline.web.bean;

namespace JNetBridge.ReplyClasses
{
    public class ForgotEmailJavaCallReply : JavaCallReply
    {
       


        public ForgotEmailJavaCallReply()
        {
        }

        private string errors;

        private UserData user;

        public UserData User
        {
            get { return user; }
            set { user = value; }
        }

        public string Errors
        {
            get { return errors; }
            set { errors = value; }
        }



        

       


               
    }
}
