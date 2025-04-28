using System;
using System.Collections.Generic;
using System.Text;

namespace JNetBridge.ReplyClasses
{
    public abstract class JavaCallReply
    {
        private string serializeError;

        public string SerializeError
        {
            get { return serializeError; }
            set { serializeError = value; }
        }

        private String m_reply;
        public String Reply
        {
            get
            {
                return m_reply;
            }
            set
            {
                #if DEBUG
                                m_reply = value;
                #else
                                m_reply = "";
                #endif
            }
        }


      
    }
}
