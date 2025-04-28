using System;
using System.Collections.Generic;
using System.Text;
using JNetBridge.InteractionClasses;
using com.teleca.fleetonline.web.bean;

namespace JNetBridge.ReplyClasses
{
    public class ContactsJavaCallReply : JavaCallReply
    {
        private MiscContentHelper fleetonline_error_content;

        public MiscContentHelper Fleetonline_error_content
        {
            get { return fleetonline_error_content; }
            set { fleetonline_error_content = value; }
        }

        private MiscContentHelper miscContentHelper;

        public MiscContentHelper MiscContentHelper
        {
            get { return miscContentHelper; }
            set { miscContentHelper = value; }
        }
        //private MapUrlHelper m_MapUrlHelper;
        //private ResponseTypeHelper m_ResponseTypeHelper;
        //private MapDisplayHelper m_MapDisplayHelper;

        private string contacts_error;

        public string Contacts_error
        {
            get { return contacts_error; }
            set { contacts_error = value; }
        }

        private string error;

        public string Error
        {
            get { return error; }
            set { error = value; }
        }
        private string confirm;

        public string Confirm
        {
            get { return confirm; }
            set { confirm = value; }
        }

        public ContactsJavaCallReply()
        {
        }
      
        //public MiscContentHelper MiscContentHelper { get { return m_MiscContentHelper; } set { m_MiscContentHelper = value; } }
        //public MapUrlHelper MapUrlHelper { get { return m_MapUrlHelper; } set { m_MapUrlHelper = value; } }
        //public ResponseTypeHelper ResponseTypeHelper { get { return m_ResponseTypeHelper; } set { m_ResponseTypeHelper = value; } }
        //public MapDisplayHelper SmListDisplayHelper { get { return m_MapDisplayHelper; } set { m_MapDisplayHelper = value; } }


    }
}
