using System;
using System.Collections.Generic;
using System.Text;
using JNetBridge.InteractionClasses;
using com.teleca.fleetonline.web.bean;

namespace JNetBridge.ReplyClasses
{
    public class PopupNotificationJavaCallReply : JavaCallReply
    {

        private NotificationListDisplayHelper M_NotificationListDisplayHelper;
        private AnswerHelper m_AnswerHelper;
        //private UserData  m_UserData;
        private string ActionMapping;



        public PopupNotificationJavaCallReply()
        {
        }

        //public NotificationJavaCallReply(SmListDisplayHelper sdh, String reply)
        //{
        //    M_smListDisplayHelper = sdh;
        //    m_reply = reply;

        
        //}

        public NotificationListDisplayHelper NotificationListDisplayHelper { get { return M_NotificationListDisplayHelper ; } set { M_NotificationListDisplayHelper  = value; } }
        public AnswerHelper AnswerHelper { get { return m_AnswerHelper ; } set { m_AnswerHelper  = value; } }


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
