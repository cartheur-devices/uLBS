using System;
using System.Collections.Generic;
using System.Text;
using JNetBridge.InteractionClasses;
using com.teleca.fleetonline.web.bean;

namespace JNetBridge.ReplyClasses
{
    public class GetReverseGeoceodeJavaCallReply : JavaCallReply
    {
        private MiscContentHelper fleetonline_error_content;

        public MiscContentHelper Fleetonline_error_content
        {
            get { return fleetonline_error_content; }
            set { fleetonline_error_content = value; }
        }

        private AnswerHelper AnswerHelper;

        public AnswerHelper AnswerHelper1
        {
            get { return AnswerHelper; }
            set { AnswerHelper = value; }
        }

        private ResponseTypeHelper fleetonline_request_type;

        public ResponseTypeHelper Fleetonline_request_type
        {
            get { return fleetonline_request_type; }
            set { fleetonline_request_type = value; }
        }


        public GetReverseGeoceodeJavaCallReply()
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

        private string postcode;

        public string Postcode
        {
            get { return postcode; }
            set { 
                postcode = value; }
        }
        private string townName;

        public string TownName
        {
            get { return townName; }
            set
            {
                townName = value;

             }
        }               
    }
}
