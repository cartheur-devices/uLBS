//*************************************************************************
//
// $Archive: /sdt/product/fol/versions/head/src/java/ejb_module/src/com/teleca/fleetonline/repository/SMData.java $
// $Revision: 1.5 $
// $Date: 2007/07/24 12:17:48 $
// $Author: salih.canoz $
//
//*************************************************************************

/*
* @SMData
*
* Copyright (c) 2002 Teleca UK Ltd.
* All rights reserved.
*
* This software is the confidential and proprietary information of Teleca UK Ltd.
* ("Confidential Information").  You shall not
* disclose such Confidential Information and shall use it only in
* accordance with the terms of the license agreement you entered into
* with Teleca UK Ltd.
*/
using System;
using com.teleca.fleetonline.web.bean;
using System.Collections.Generic;
using System.Xml;
using JNetBridge.InteractionClasses;
using GlobalConstants = com.teleca.fleetonline.utils.GlobalConstants;
namespace com.teleca.fleetonline.repository
{
	
	/// <summary> This class holds SM data</summary>
	/// <author>   $Author: salih.canoz $
	/// </author>
	/// <version>  $Revision: 1.5 $, $Date: 2007/07/24 12:17:48 $
	/// </version>
	
	[Serializable]
	public class SMData:RepositoryData
	{
        //internal class AnonymousClassComparator : System.Collections.IComparer
        //{
        //    public virtual int Compare(System.Object o1, System.Object o2)
        //    {
        //        SMData l1 = (SMData) o1;
        //        SMData l2 = (SMData) o2;
        //        return l1.TimeSent.CompareTo(l2.TimeSent);
        //    }
        //}
		internal class AnonymousClassComparator1 : System.Collections.IComparer
		{
			public virtual int Compare(System.Object o1, System.Object o2)
			{
				SMData l1 = (SMData) o1;
				SMData l2 = (SMData) o2;
				return String.CompareOrdinal(l1.FmUid, l2.FmUid);
			}
		}
		internal class AnonymousClassComparator2 : System.Collections.IComparer
		{
			public virtual int Compare(System.Object o1, System.Object o2)
			{
				SMData l1 = (SMData) o1;
				SMData l2 = (SMData) o2;
				if (l1.Status > l2.Status)
					return 1;
				else if (l1.Status == l2.Status)
					return 0;
				else
					return - 1;
			}
		}
		internal class AnonymousClassComparator3 : System.Collections.IComparer
		{
			public virtual int Compare(System.Object o1, System.Object o2)
			{
				SMData l1 = (SMData) o1;
				SMData l2 = (SMData) o2;
				return String.CompareOrdinal(l1.MessageText, l2.MessageText);
			}
		}
        private void InitBlock()
        {
            status = GlobalConstants.SM_UNDEFINED;
            providerId = GlobalConstants.PROVIDER_UNDEFINED;
            direction = GlobalConstants.SM_DIRECTION_MT;
        }

		//UPGRADE_NOTE: Respective javadoc comments were merged.  It should be changed in order to comply with .NET documentation conventions. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1199'"
		/// <summary> Returns the device type.
		/// - Cell phone
		/// - Trimtrac
		/// </summary>
		/// <returns>
		/// </returns>
		/// <summary> Sets the device type.</summary>
		/// <param name="type">
		/// </param>
		virtual public System.String DeviceType
		{
			get
			{
				return this.deviveType;
			}
			
			set
			{
				this.deviveType = value;
			}
			
		}
		//UPGRADE_NOTE: Respective javadoc comments were merged.  It should be changed in order to comply with .NET documentation conventions. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1199'"
		/// <summary> Returns the messageText.</summary>
		/// <returns> String
		/// </returns>
		/// <summary> Sets the messageText.</summary>
		/// <param name="messageText">The messageText to set
		/// </param>
		virtual public System.String MessageText
		{
			get
			{
				return messageText;
			}
			
			set
			{
				this.messageText = value;
			}
			
		}
		//UPGRADE_NOTE: Respective javadoc comments were merged.  It should be changed in order to comply with .NET documentation conventions. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1199'"
		/// <summary> Returns the providerId.</summary>
		/// <returns> int
		/// </returns>
		/// <summary> Sets the providerId.</summary>
		/// <param name="providerId">The providerId to set
		/// </param>
		virtual public int ProviderId
		{
			get
			{
				return providerId;
			}
			
			set
			{
				this.providerId = value;
			}
			
		}
		//UPGRADE_NOTE: Respective javadoc comments were merged.  It should be changed in order to comply with .NET documentation conventions. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1199'"
		/// <summary> Returns the receiverUid.</summary>
		/// <returns> String
		/// </returns>
		/// <summary> Sets the fmUid.</summary>
		/// <param name="fmUid">The fmUid to set
		/// </param>
		virtual public System.String FmUid
		{
			get
			{
				return fmUid;
			}
			
			set
			{
				this.fmUid = value;
			}
			
		}
		//UPGRADE_NOTE: Respective javadoc comments were merged.  It should be changed in order to comply with .NET documentation conventions. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1199'"
		/// <summary> Returns the foUid.</summary>
		/// <returns> String
		/// </returns>
		/// <summary> Sets the foUid.</summary>
		/// <param name="foUid">The foUid to set
		/// </param>
		virtual public System.String FoUid
		{
			get
			{
				return foUid;
			}
			
			set
			{
				this.foUid = value;
			}
			
		}
		
		//UPGRADE_NOTE: Respective javadoc comments were merged.  It should be changed in order to comply with .NET documentation conventions. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1199'"
		/// <summary> Returns the status.</summary>
		/// <returns> int
		/// </returns>
		/// <summary> Sets the status.</summary>
		/// <param name="status">The status to set
		/// </param>
		virtual public int Status
		{
			get
			{
				return status;
			}
			
			set
			{
				this.status = value;
			}
			
		}
		//UPGRADE_NOTE: Respective javadoc comments were merged.  It should be changed in order to comply with .NET documentation conventions. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1199'"
		/// <summary> Returns the timeDeliveryReport.</summary>
		/// <returns> java.util.Date
		/// </returns>
		/// <summary> Sets the timeDeliveryReport.</summary>
		/// <param name="timeDeliveryReport">The timeDeliveryReport to set
		/// </param>
        //virtual public System.DateTime TimeDeliveryReport
        //{
        //    get
        //    {
        //        return timeDeliveryReport;
        //    }
			
        //    set
        //    {
        //        this.timeDeliveryReport = value;
        //    }
			
        //}
		//UPGRADE_NOTE: Respective javadoc comments were merged.  It should be changed in order to comply with .NET documentation conventions. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1199'"
		/// <summary> Returns the timeSent.</summary>
		/// <returns> java.util.Date
		/// </returns>
		/// <summary> Sets the timeSent.</summary>
		/// <param name="timeSent">The timeSent to set
		/// </param>
        //virtual public System.DateTime TimeSent
        //{
        //    get
        //    {
        //        return timeSent;
        //    }
			
        //    set
        //    {
        //        this.timeSent = value;
        //    }
			
        //}
		//UPGRADE_NOTE: Respective javadoc comments were merged.  It should be changed in order to comply with .NET documentation conventions. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1199'"
		/// <summary> Returns the transactionId.</summary>
		/// <returns> String
		/// </returns>
		/// <summary> Sets the transactionId.</summary>
		/// <param name="transactionId">The transactionId to set
		/// </param>
		virtual public System.String TransactionId
		{
			get
			{
				return transactionId;
			}
			
			set
			{
				this.transactionId = value;
			}
			
		}
		//UPGRADE_NOTE: Respective javadoc comments were merged.  It should be changed in order to comply with .NET documentation conventions. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1199'"
		/// <summary> Returns the direction.</summary>
		/// <returns> int
		/// </returns>
		/// <summary> Sets the direction.</summary>
		/// <param name="direction">The direction to set
		/// </param>
		virtual public int Direction
		{
			get
			{
				return direction;
			}
			
			set
			{
				this.direction = value;
			}
			
		}
		//UPGRADE_NOTE: Respective javadoc comments were merged.  It should be changed in order to comply with .NET documentation conventions. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1199'"
		/// <summary> Returns the read.</summary>
		/// <returns> boolean
		/// </returns>
		/// <summary> Sets the read.</summary>
		/// <param name="read">The read to set
		/// </param>
		virtual public bool Read
		{
			get
			{
				return read;
			}
			
			set
			{
				this.read = value;
			}
			
		}
		
		/// <summary> </summary>
		private const long serialVersionUID = 1L;
        private long smId; // no set function for smId

        public long SmId
        {
            get { return smId; }
            set { smId = value; }
        }
        private bool smIdSet; // no set function for smIdSet
        /// <summary>
        /// Returns the smIdSet.
        /// </summary>
        public bool SmIdSet
        {
            get { return smIdSet; }
            set { smIdSet = value; }
        }
		private System.String deviveType = null;
		private System.String foUid = null;
		private System.String fmUid = null;
		//UPGRADE_TODO: The 'System.DateTime' structure does not have an equivalent to NULL. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1291'"
		private System.DateTime? timeSent = null;
		//UPGRADE_TODO: The 'System.DateTime' structure does not have an equivalent to NULL. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1291'"
		private System.DateTime? timeDeliveryReport = null;
		private System.String messageText = null;
		private System.String transactionId = null;
		//UPGRADE_NOTE: The initialization of  'status' was moved to method 'InitBlock'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1005'"
		private int status;
		//UPGRADE_NOTE: The initialization of  'providerId' was moved to method 'InitBlock'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1005'"
		private int providerId;
		//UPGRADE_NOTE: The initialization of  'direction' was moved to method 'InitBlock'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1005'"
		private int direction;
		private bool read = false;

        private string fromTo;

        public string FromTo
        {
            get { return fromTo; }
            set { fromTo = value; }
        }
        private int messageStatus;

        public int MessageStatus
        {
            get { return messageStatus; }
            set { messageStatus = value; }
        }

        private string messageStatusText;

        public string MessageStatusText
        {
            get { return messageStatusText; }
            set { messageStatusText = value; }
        }

        private string content;

        public string Content
        {
            get { return content; }
            set { content = value; }
        }
        private DateTime? dateTimeM;

        public DateTime? DateTimeM
        {
            get { return dateTimeM; }
            set { dateTimeM = value; }
        }

        private string dateTime;

        public string DateTime
        {
            get { return dateTime; }
            set { dateTime = value; }
        }


		
        ///// <summary> Constructor</summary>
        //public SMData():base()
        //{
        //    InitBlock();
        //    smIdSet = false;
        //}
		
        ///// <summary> Constructor that sets SM ID</summary>
        ///// <param name="id">ID of the SM request
        ///// </param>
        //public SMData(long id):base()
        //{
        //    InitBlock();
			
        //    smId = id;
        //    smIdSet = true;
        //}
		
		/// <summary> Returns true if mandatory data is present</summary>
		/// <returns> boolean
		/// </returns>
		public virtual bool hasRequiredData()
		{
			
			//UPGRADE_TODO: The 'System.DateTime' structure does not have an equivalent to NULL. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1291'"
			if (foUid == null || fmUid == null || timeSent == null || messageText == null)
			{
				
				return false;
			}
			return true;
		}
		
        //public override System.String ToString()
        //{
        //    //UPGRADE_TODO: Method 'java.util.Date.toGMTString' was converted to 'System.DateTime.ToString' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javautilDatetoGMTString'"
        //    return timeSent.ToString("dd MMM yyy T 'GMT'") + "  -  " + messageText;
        //}
		
		//UPGRADE_NOTE: The initialization of  'timeSentComparator' was moved to static method 'com.teleca.fleetonline.repository.SMData'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1005'"
		public static System.Collections.IComparer timeSentComparator;
		
		//UPGRADE_NOTE: The initialization of  'fmComparator' was moved to static method 'com.teleca.fleetonline.repository.SMData'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1005'"
		public static System.Collections.IComparer fmComparator;
		
		//UPGRADE_NOTE: The initialization of  'statusComparator' was moved to static method 'com.teleca.fleetonline.repository.SMData'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1005'"
		public static System.Collections.IComparer statusComparator;
		
		//UPGRADE_NOTE: The initialization of  'messagetextComparator' was moved to static method 'com.teleca.fleetonline.repository.SMData'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1005'"
        //public static System.Collections.IComparer messagetextComparator;
        //static SMData()
        //{
        //    timeSentComparator = new AnonymousClassComparator();
        //    fmComparator = new AnonymousClassComparator1();
        //    statusComparator = new AnonymousClassComparator2();
        //    messagetextComparator = new AnonymousClassComparator3();
        //}

        public static List <SMData> FromNode(XmlNode xmlSourceNode)
        {
            XmlNodeList nodeListSMData = xmlSourceNode.SelectNodes("//com.teleca.fleetonline.repository.SMData");

            List<SMData> l = new List<SMData>();

            foreach (XmlNode node in nodeListSMData)
            {
                // Dit zijn de xml's met memberdata...
                SMData smd = new SMData();
                Utils.FillProperties(smd, node);
                l.Add(smd);
            }

            return l;


        }
    }
}