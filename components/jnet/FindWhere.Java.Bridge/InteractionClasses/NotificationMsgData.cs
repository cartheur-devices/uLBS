using System;
using com.teleca.fleetonline.web.bean;
namespace com.teleca.fleetonline.repository
{


//*************************************************************************
//
//$Source: D:/Data/cvs/FOL/src/java/ejb_module/src/com/teleca/fleetonline/repository/NotificationMsgData.java,v $
//$Revision: 1.6 $
//$Date: 2007/04/10 07:15:19 $
//
//Copyright(c) Teydo BV, Bilthoven, all rights reserved worldwide.
//
//************************************************************************* 

// *
// * This class holds NotificationMsg data
// 
    [System.Serializable]
	public class NotificationMsgData : RepositoryData
	{

//    *
//	 * 
//	 
        private const long serialVersionUID = 1L;

        public long SerialVersionUID
        {
            get { return serialVersionUID; }
        }

        private long notifMsgId;

        public long NotifMsgId
        {
            get { return notifMsgId; }
            set { notifMsgId = value; }
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
        private int eventType;

        public int EventType
        {
            get { return eventType; }
            set { eventType = value; }
        }
        private long eventId;

        public long EventId
        {
            get { return eventId; }
            set { eventId = value; }
        }
        private string recipients;

        public string Recipients
        {
            get { return recipients; }
            set { recipients = value; }
        }
        private int carrier;

        public int Carrier
        {
            get { return carrier; }
            set { carrier = value; }
        }
        private string msgText;

        public string MsgText
        {
            get { return msgText; }
            set { msgText = value; }
        }
        private DateTime timeEvent; // timestamp of the eevent that triggered the notifiction msg

        public DateTime TimeEvent
        {
            get { return timeEvent; }
            set { timeEvent = value; }
        }
        private DateTime timeSent;

        public DateTime TimeSent
        {
            get { return timeSent; }
            set { timeSent = value; }
        }
        private int read = 0;

        public int Read
        {
            get { return read; }
            set { read = value; }
        }

		public NotificationMsgData() : base()
		{
		}

		public NotificationMsgData(long notifMsgId) : base()
		{
			this.notifMsgId = notifMsgId;
		}

        private String eventTypeLabel;

        public String EventTypeLabel
        {
            get { return eventTypeLabel; }
            set { eventTypeLabel = value; }
        }

        private string dateTime;

        public string DateTime
        {
            get { return dateTime; }
            set { dateTime = value; }
        }


//    *
//	 * @return Returns the carrier.
//	 
		public virtual int getCarrier()
		{
			return Carrier;
		}

//    *
//	 * @param carrier The carrier to set.
//	 
		public virtual void setCarrier(int carrier)
		{
			this.Carrier = carrier;
		}

//    *
//	 * @return Returns the eventType.
//	 
		public virtual int getEventType()
		{
			return EventType;
		}

//    *
//	 * @param eventType The eventType to set.
//	 
		public virtual void setEventType(int eventType)
		{
			this.EventType = eventType;
		}

//    *
//	 * @return Returns the eventId.
//	 
		public virtual long getEventId()
		{
			return EventId;
		}

//    *
//	 * @param eventId The eventId to set.
//	 
		public virtual void setEventId(long eventId)
		{
			this.EventId = eventId;
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
//	 * @return Returns the msgText.
//	 
		public virtual string getMsgText()
		{
			return MsgText;
		}

//    *
//	 * @param msgText The msgText to set.
//	 
		public virtual void setMsgText(string msgText)
		{
			this.MsgText = msgText;
		}

//    *
//	 * @return Returns the notifMsgId.
//	 
		public virtual long getNotifMsgId()
		{
			return NotifMsgId;
		}

//    *
//	 * @return Returns the read.
//	 
		public virtual int getRead()
		{
			return Read;
		}

//    *
//	 * @param read The read to set.
//	 
		public virtual void setRead(int read)
		{
			this.Read = read;
		}

//    *
//	 * @return Returns the recipients.
//	 
		public virtual string getRecipients()
		{
			return Recipients;
		}

//    *
//	 * @param recipients The recipients to set.
//	 
		public virtual void setRecipients(string recipients)
		{
			this.Recipients = recipients;
		}

//    *
//	 * @return Returns the timeEvent.
//	 
		public virtual DateTime getTimeEvent()
		{
			return TimeEvent;
		}

//    *
//	 * @param timeEvent The timeEvent to set.
//	 
        public virtual void setTimeEvent(DateTime timeEvent)
		{
			this.TimeEvent = timeEvent;
		}

//    *
//	 * @return Returns the timeSent.
//	 
        public virtual DateTime getTimeSent()
		{
			return TimeSent;
		}

//    *
//	 * @param timeSent The timeSent to set.
//	 
        public virtual void setTimeSent(DateTime timeSent)
		{
			this.TimeSent = timeSent;
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

//        public static Comparator timeSentComparator = new Comparator()
////JAVA TO VB & C# CONVERTER TODO TASK: Anonymous inner classes are not converted to .NET:
//        {
//            public int compare(object o1, object o2)
//            {
//                NotificationMsgData l1 = (NotificationMsgData)o1;
//                NotificationMsgData l2 = (NotificationMsgData)o2;
//                return l1.getTimeSent().CompareTo(l2.getTimeSent());
//            }
//        }

//        public static Comparator rcpComparator = new Comparator()
////JAVA TO VB & C# CONVERTER TODO TASK: Anonymous inner classes are not converted to .NET:
//        {
//            public int compare(object o1, object o2)
//            {
//                NotificationMsgData l1 = (NotificationMsgData)o1;
//                NotificationMsgData l2 = (NotificationMsgData)o2;
//                return l1.getRecipients().CompareTo(l2.getRecipients());
//            }
//        }

//        public static Comparator typeComparator = new Comparator()
////JAVA TO VB & C# CONVERTER TODO TASK: Anonymous inner classes are not converted to .NET:
//        {
//            public int compare(object o1, object o2)
//            {
//                NotificationMsgData l1 = (NotificationMsgData)o1;
//                NotificationMsgData l2 = (NotificationMsgData)o2;
//                if(l1.getEventType() > l2.getEventType())
//                    return 1;
//                else if(l1.getEventType() == l2.getEventType())
//                    return 0;
//                else
//                    return -1;
//            }
//        }

//        public static Comparator messagetextComparator = new Comparator()
////JAVA TO VB & C# CONVERTER TODO TASK: Anonymous inner classes are not converted to .NET:
//        {
//            public int compare(object o1, object o2)
//            {
//                NotificationMsgData l1 = (NotificationMsgData)o1;
//                NotificationMsgData l2 = (NotificationMsgData)o2;
//                return l1.getMsgText().CompareTo(l2.getMsgText());
//            }
//        }
	}
}