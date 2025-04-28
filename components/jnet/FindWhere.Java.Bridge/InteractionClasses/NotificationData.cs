using com.teleca.fleetonline.web.bean;
namespace com.teleca.fleetonline.repository
{

    //*************************************************************************
    //
    //$Source: D:/Data/cvs/FOL/src/java/ejb_module/src/com/teleca/fleetonline/repository/NotificationData.java,v $
    //$Revision: 1.5 $
    //$Date: 2007/03/29 07:49:35 $
    //
    //Copyright(c) Teydo BV, Bilthoven, all rights reserved worldwide.
    //
    //************************************************************************* 

    // *
    // * This class holds data for notification data
    // 
    [System.Serializable]
    public class NotificationData : RepositoryData
    {
        private const long serialVersionUID = 1L;

        public long SerialVersionUID
        {
            get { return serialVersionUID; }
        } 


        private bool notifIdSet;

        public bool NotifIdSet
        {
            get { return notifIdSet; }
            set { notifIdSet = value; }
        }
        private string notifId;

        public string NotifId
        {
            get { return notifId; }
            set { notifId = value; }
        }
        private string foId;

        public string FoId
        {
            get { return foId; }
            set { foId = value; }
        }
        private string fmId;

        public string FmId
        {
            get { return fmId; }
            set { fmId = value; }
        }
        private string eventType;

        public string EventType
        {
            get { return eventType; }
            set { eventType = value; }
        }
        private int fmIdSet; // 0 if fmId == null;

        public int FmIdSet
        {
            get { return fmIdSet; }
            set { fmIdSet = value; }
        }
        private int sendByEmail;

        public int SendByEmail
        {
            get { return sendByEmail; }
            set { sendByEmail = value; }
        }
        private int sendBySms;

        public int SendBySms
        {
            get { return sendBySms; }
            set { sendBySms = value; }
        }
        private string emailRcpList;

        public string EmailRcpList
        {
            get { return emailRcpList; }
            set { emailRcpList = value; }
        }
        private string smsRcpList;

        public string SmsRcpList
        {
            get { return smsRcpList; }
            set { smsRcpList = value; }
        }
        private string memberList;

        public string MemberList
        {
            get { return memberList; }
            set { memberList = value; }
        }
        private int alertOnce;

        public int AlertOnce
        {
            get { return alertOnce; }
            set { alertOnce = value; }
        }


        public NotificationData()
            : base()
        {
            notifIdSet = false;
        }

        public NotificationData(string notifId)
            : base()
        {
            this.notifId = notifId;
            notifIdSet = true;
        }


        //    *
        //	 * @return Returns the emailRcpList.
        //	 
        public virtual string getEmailRcpList()
        {
            return EmailRcpList;
        }

        //    *
        //	 * @param emailRcpList The emailRcpList to set.
        //	 
        public virtual void setEmailRcpList(string emailRcpList)
        {
            this.EmailRcpList = emailRcpList;
        }

        //    *
        //	 * @return Returns the eventType.
        //	 
        public virtual string getEventType()
        {
            return EventType;
        }

        //    *
        //	 * @param eventType The eventType to set.
        //	 
        public virtual void setEventType(string eventType)
        {
            this.EventType = eventType;
        }

        //    *
        //	 * @return Returns the fmId.
        //	 
        public virtual string getFmId()
        {
            return FmId;
        }

        //    *
        //	 * @param fmId The fmId to set.
        //	 
        public virtual void setFmId(string fmId)
        {
            this.FmId = fmId;
        }

        //    *
        //	 * @return Returns the fmIdSet.
        //	 
        public virtual int getFmIdSet()
        {
            return FmIdSet;
        }

        //    *
        //	 * @param fmIdSet The fmIdSet to set.
        //	 
        public virtual void setFmIdSet(int fmIdSet)
        {
            this.FmIdSet = fmIdSet;
        }

        //    *
        //	 * @return Returns the foId.
        //	 
        public virtual string getFoId()
        {
            return FoId;
        }

        //    *
        //	 * @param foId The foId to set.
        //	 
        public virtual void setFoId(string foId)
        {
            this.FoId = foId;
        }

        //    *
        //	 * @return Returns the smsRcpList.
        //	 
        public virtual string getSmsRcpList()
        {
            return SmsRcpList;
        }

        //    *
        //	 * @param smsRcpList The smsRcpList to set.
        //	 
        public virtual void setSmsRcpList(string smsRcpList)
        {
            this.SmsRcpList = smsRcpList;
        }

        //    *
        //	 * @return Returns the memberList.
        //	 
        public virtual string getMemberList()
        {
            return MemberList;
        }

        //    *
        //	 * @param memberList The memberList to set.
        //	 
        public virtual void setMemberList(string memberList)
        {
            this.MemberList = memberList;
        }

        //    *
        //	 * @return Returns the notifId.
        //	 
        public virtual string getNotifId()
        {
            return NotifId;
        }

        //    *
        //	 * @param notifId The notifId to set.
        //	 
        public virtual void setNotifId(string notifId)
        {
            this.NotifId = notifId;
        }

        //    *
        //	 * @return Returns the sendByEmail.
        //	 
        public virtual int getSendByEmail()
        {
            return SendByEmail;
        }

        //    *
        //	 * @param sendByEmail The sendByEmail to set.
        //	 
        public virtual void setSendByEmail(int sendByEmail)
        {
            this.SendByEmail = sendByEmail;
        }

        //    *
        //	 * @return Returns the sendBySms.
        //	 
        public virtual int getSendBySms()
        {
            return SendBySms;
        }

        //    *
        //	 * @param sendBySms The sendBySms to set.
        //	 
        public virtual void setSendBySms(int sendBySms)
        {
            this.SendBySms = sendBySms;
        }

        //    *
        //	 * @return Returns the notifIdSet.
        //	 
        public virtual bool isNotifIdSet()
        {
            return NotifIdSet;
        }

        //    *
        //	 * @return Returns the alertOnce.
        //	 
        public virtual int getAlertOnce()
        {
            return AlertOnce;
        }

        //    *
        //	 * @param alertOnce The alertOnce to set.
        //	 
        public virtual void setAlertOnce(int alertOnce)
        {
            this.AlertOnce = alertOnce;
        }

    }
}