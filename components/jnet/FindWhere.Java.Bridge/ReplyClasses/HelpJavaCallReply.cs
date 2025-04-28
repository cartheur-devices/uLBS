using System;
using System.Collections.Generic;
using System.Text;
using JNetBridge.InteractionClasses;
using com.teleca.fleetonline.web.bean;

namespace JNetBridge.ReplyClasses
{
    public class HelpJavaCallReply : JavaCallReply
    {
        //TODO: remove times
        private HelpDisplayHelper fleetonline_help;

        public HelpDisplayHelper Fleetonline_help
        {
            get { return fleetonline_help; }
            set { fleetonline_help = value; }
        }
      

 
        
              

        public HelpJavaCallReply()
        {
        }

        



        //public UserData UserData 
        //{
        //    get
        //    {
        //        return m_UserData; 
        //    }
        //    set 
        //    {
        //        m_UserData = value;
                
        //    }
        //}

       


               
    }
}
