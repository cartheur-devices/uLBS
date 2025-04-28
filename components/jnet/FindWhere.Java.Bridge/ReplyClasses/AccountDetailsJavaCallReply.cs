using System;
using System.Collections.Generic;
using System.Text;
using JNetBridge.InteractionClasses;
using com.teleca.fleetonline.web.bean;
using System.Collections;
using com.teleca.fleetonline.repository;

namespace JNetBridge.ReplyClasses
{
    public class AccountDetailsJavaCallReply : JavaCallReply
    {
        

        /// <summary>
        /// The contacts
        /// </summary>
        private LinkedList<ContactData> contacts;


        private AnswerHelper fleetonline_request_status;
        private AccountDetailsDisplayHelper fleetonline_accountdetails;
        //private ContactsDisplayHelper contactlist;
        private TimewindowDisplayHelper fleetonline_timewindows;

        private string error;
        private string confirm;



        public AccountDetailsJavaCallReply()
        {
        }



        public LinkedList<ContactData> LinkedList { get { return contacts; } set { contacts = value; } }

        /// <summary>
        /// 
        /// </summary>
        public AnswerHelper AnswerHelper { get { return fleetonline_request_status; } set { fleetonline_request_status = value; } }

        /// <summary>
        /// 
        /// </summary>
        public AccountDetailsDisplayHelper AccountDetailsDisplayHelper { get { return fleetonline_accountdetails; } set { fleetonline_accountdetails = value; } }
        /// <summary>
        /// 
        /// </summary>
        //public ContactsDisplayHelper ContactsDisplayHelper { get { return contactlist; } set { contactlist = value; } }
        /// <summary>
        /// 
        /// </summary>
        public TimewindowDisplayHelper TimewindowDisplayHelper { get { return fleetonline_timewindows; } set { fleetonline_timewindows = value; } }

        public String Error { get { return error; } set { error = value; } }
        public String Confirm { get { return confirm; } set { confirm = value; } }

    }
}
