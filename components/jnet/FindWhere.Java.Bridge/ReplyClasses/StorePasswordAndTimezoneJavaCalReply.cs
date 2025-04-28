using System;
using System.Collections.Generic;
using System.Text;
using JNetBridge.InteractionClasses;
using com.teleca.fleetonline.web.bean;

namespace JNetBridge.ReplyClasses
{
    public class StorePasswordAndTimezoneJavaCalReply : JavaCallReply
    {

        private string changed;

        public string Changed
        {
            get { return changed; }
            set { changed = value; }
        }

        private string error;

        public string Error
        {
            get { return error; }
            set { error = value; }
        }



               
    }
}
