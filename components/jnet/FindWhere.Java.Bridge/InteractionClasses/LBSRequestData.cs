namespace com.teleca.fleetonline.repository
{

//*************************************************************************
//
// $Source: D:/Data/cvs/FOL5/ejb_module/src/com/teleca/fleetonline/repository/LBSRequestData.java,v $
// $Revision: 1.1 $
// $Date: 2008/04/10 14:27:40 $
//
// Copyright (c) Teydo BV, Bilthoven, all rights reserved worldwide.
//
//************************************************************************* 

// *
// * This class holds LBS request data (FO, FM, transactionid, provider, timestamps, position & accuracy, FM status, town, postcode etc.)
// 



    //using TTMessageData = com.teleca.fleetonline.businessmanager.TTMessageData;
	using GlobalConstants = com.teleca.fleetonline.utils.GlobalConstants;
    using com.teleca.fleetonline.web.bean;
    using System;
    using com.teleca.fleetonline.businessmanager;
    [System.Serializable]
	public class LBSRequestData : RepositoryData
	{

//    *
//	 * 
//	 
        public const long serialVersionUID = -4372413889454048614L;
        private long lbsid; // no set function for this

        public long Lbsid
        {
            get { return lbsid; }
            set { lbsid = value; }
        }
        public bool lbsidSet = false; // no set function for this

        public string foUid = null; // must be defined
        public string fmUid = null; // must be defined
        public string transactionId = null; // must be defined
        public int providerId = GlobalConstants.PROVIDER_UNDEFINED;
        private int status = GlobalConstants.LBS_UNDEFINED;
        private string batteryLevel = "";


        private string member;

        public string Member
        {
            get { return member; }
            set { member = value; }
        }
        private string memberStatus;

        public string MemberStatus
        {
            get { return memberStatus; }
            set { memberStatus = value; }
        }
        private string proximity;

        public string Proximity
        {
            get { return proximity; }
            set { proximity = value; }
        }
        private string postcode;

        public string Postcode
        {
            get { return postcode; }
            set { postcode = value; }
        }
        private string speedDirection;

        public string SpeedDirection
        {
            get { return speedDirection; }
            set { speedDirection = value; }
        }
        private string dateTime;

        public string DateTime
        {
            get { return dateTime; }
            set { dateTime = value; }
        }


        private DateTime? timeSent = null; // must be defined

        public DateTime? TimeSent1
        {
            get { return timeSent; }
            set { timeSent = value; }
        }
        private DateTime? timeResponse = null;

        public DateTime? TimeResponse
        {
            get { return timeResponse; }
            set { timeResponse = value; }
        }
        private double accuracy = -1;

        public double Accuracy
        {
            get { return accuracy; }
            set { accuracy = value; }
        }
        //private string postcode = null;

        //public string Postcode
        //{
        //    get { return Postcode1; }
        //    set { Postcode1 = value; }
        //}
        private string town = null;

        public string Town
        {
            get { return town; }
            set { town = value; }
        }
        private int fmStatusId = 0;

        public int FmStatusId
        {
            get { return fmStatusId; }
            set { fmStatusId = value; }
        }
        private string fmStatusText = null;

        public string FmStatusText
        {
            get { return fmStatusText; }
            set { fmStatusText = value; }
        }

        private double locationX;

        public double LocationX
        {
            get { return locationX; }
            set { locationX = value; }
        }

        private double locationY;

        public double LocationY
        {
            get { return locationY; }
            set { locationY = value; }
        }
        private double locationnadX;

        public double LocationnadX
        {
            get { return locationnadX; }
            set { locationnadX = value; }
        }
        private double locationnadY;

        public double LocationnadY
        {
            get { return locationnadY; }
            set { locationnadY = value; }
        }
        private bool locationXSet = false;

        public bool LocationXSet
        {
            get { return locationXSet; }
            set { locationXSet = value; }
        }
        private bool locationYSet = false;

        public bool LocationYSet
        {
            get { return locationYSet; }
            set { locationYSet = value; }
        }

        private int? responseCode1 = null;

        public int? ResponseCode1
        {
            get { return responseCode1; }
            set { responseCode1 = value; }
        }
        private int? responseCode2 = null;

        public int? ResponseCode2
        {
            get { return responseCode2; }
            set { responseCode2 = value; }
        }
        private string response1 = null;

        public string Response1
        {
            get { return response1; }
            set { response1 = value; }
        }
        private string response2 = null;

        public string Response2
        {
            get { return response2; }
            set { response2 = value; }
        }

        private bool reCredit = false;

        public bool ReCredit
        {
            get { return reCredit; }
            set { reCredit = value; }
        }

        private string deviceType = null;

        public string DeviceType
        {
            get { return deviceType; }
            set { deviceType = value; }
        }

        private int fixStatus; // Status of the GPS fix

        public int FixStatus
        {
            get { return fixStatus; }
            set { fixStatus = value; }
        }
        private DateTime fixTime; // Timestamp of the GPS fix

        public DateTime FixTime
        {
            get { return fixTime; }
            set { fixTime = value; }
        }

        private int reportCause; // Cause identification of this lbs data

        public int ReportCause
        {
            get { return reportCause; }
            set { reportCause = value; }
        }

        private double speed;

        public double Speed
        {
            get { return speed; }
            set { speed = value; }
        }
        private double direction;

        public double Direction
        {
            get { return direction; }
            set { direction = value; }
        }

        private TTMessageData ttMessageData;

        public TTMessageData TtMessageData
        {
            get { return ttMessageData; }
            set { ttMessageData = value; }
        }

        private string alertInfo = "";

        public string AlertInfo
        {
            get { return alertInfo; }
            set { alertInfo = value; }
        }

        public string BatteryLevel
        {
            get { return batteryLevel; }
            set { batteryLevel = value; }
        }


//    *
//	 * Constructor
//	 
		public LBSRequestData() : base()
		{
			lbsidSet = false;
		}

//    *
//	 * Constructor that sets LBS request ID
//	 * @param setLbsid ID of the LBS request
//	 
		public LBSRequestData(long setLbsid) : base()
		{
			setLbsId(setLbsid);
		}

        public virtual void setLbsId(long setLbsid)
        {
            Lbsid = setLbsid;
            lbsidSet = true;
        }

//    *
//	 * Returns true if mandatory data is present
//	 * @return boolean
//	 
        public virtual bool hasRequiredData()
        {
            if (FoUid == null || FmUid == null || TransactionId == null || TimeSent == null)
                return false;

            return true;
        }

//    *
//	 * Returns the accuracy.
//	 * @return double
//	 
		public virtual double getAccuracy()
		{
			return Accuracy;
		}

//    *
//	 * Returns the fmStatusId.
//	 * @return int
//	 
		public virtual int getFmStatusId()
		{
			return FmStatusId;
		}

//    *
//	 * Returns the fmUid.
//	 * @return String
//	 
        public virtual string getFmUid()
        {
            return FmUid;
        }

//    *
//	 * Returns the foUid.
//	 * @return String
//	 
        public virtual string getFoUid()
        {
            return FoUid;
        }

//    *
//	 * Returns the lbsid.
//	 * @return long
//	 
        public virtual long getLbsid()
        {
            return Lbsid;
        }

//    *
//	 * Returns the lbsidSet.
//	 * @return boolean
//	 
		public virtual bool isLbsidSet()
		{
			return lbsidSet;
		}

//    *
//	 * Returns the postcode.
//	 * @return String
//	 
		public virtual string getPostcode()
		{
			if (Postcode == null)
			{
				Postcode = "";
			}

			return Postcode;
		}

//    *
//	 * Returns the providerId.
//	 * @return int
//	 
        public virtual int getProviderId()
        {
            return ProviderId;
        }

//    *
//	 * Returns the status.
//	 * @return int
//	 
        public virtual int getStatus()
        {
            return Status;
        }

//    *
//	 * Returns the timeResponse.
//	 * @return java.util.Date
//	 
		public virtual DateTime? getTimeResponse()
		{
			return TimeResponse;
		}

//    *
//	 * Returns the timeSent.
//	 * @return java.util.Date
//	 
        public virtual DateTime? getTimeSent()
        {
            return TimeSent;
        }

//    *
//	 * Returns the town.
//	 * @return String
//	 
		public virtual string getTown()
		{
			if (Town == null)
			{
				Town = "";
			}

			return Town;
		}

//    *
//	 * Returns the transactionId.
//	 * @return String
//	 
        public virtual string getTransactionId()
        {
            return TransactionId;
        }

//    *
//	 * Sets the accuracy.
//	 * @param accuracy The accuracy to set
//	 
		public virtual void setAccuracy(double accuracy)
		{
			this.Accuracy = accuracy;
		}

//    *
//	 * Sets the fmStatusId.
//	 * @param fmStatusId The fmStatusId to set
//	 
		public virtual void setFmStatusId(int fmStatusId)
		{
			this.FmStatusId = fmStatusId;
		}

//    *
//	 * Sets the fmUid.
//	 * @param fmUid The fmUid to set
//	 
        public virtual void setFmUid(string fmUid)
        {
            this.FmUid = fmUid;
        }

//    *
//	 * Sets the foUid.
//	 * @param foUid The foUid to set
//	 
        public virtual void setFoUid(string foUid)
        {
            this.FoUid = foUid;
        }

//    *
//	 * Sets the postcode.
//	 * @param postcode The postcode to set
//	 
		public virtual void setPostcode(string postcode)
		{
			this.Postcode = postcode;
		}

//    *
//	 * Sets the providerId.
//	 * @param providerId The providerId to set
//	 
        public virtual void setProviderId(int providerId)
        {
            this.ProviderId = providerId;
        }

//    *
//	 * Sets the status.
//	 * @param status The status to set
//	 
        public virtual void setStatus(int status)
        {
            this.Status = status;
        }

//    *
//	 * Sets the timeResponse.
//	 * @param timeResponse The timeResponse to set
//	 
        public virtual void setTimeResponse(DateTime timeResponse)
		{
			this.TimeResponse = timeResponse;
		}

//    *
//	 * Sets the timeSent.
//	 * @param timeSent The timeSent to set
//	 
        public virtual void setTimeSent(DateTime timeSent)
        {
            this.TimeSent = timeSent;
        }

//    *
//	 * Sets the town.
//	 * @param town The town to set
//	 
		public virtual void setTown(string town)
		{
			this.Town = town;
		}

//    *
//	 * Sets the transactionId.
//	 * @param transactionId The transactionId to set
//	 
        public virtual void setTransactionId(string transactionId)
        {
            this.TransactionId = transactionId;
        }

//    *
//	 * Returns the locationX.
//	 * @return double
//	 
		public virtual double getLocationX()
		{
			return LocationX;
		}

//    *
//	 * Returns the locationXSet.
//	 * @return boolean
//	 
		public virtual bool isLocationXSet()
		{
			LocationXSet = true;
			return LocationXSet;
		}

//    *
//	 * Returns the locationY.
//	 * @return double
//	 
		public virtual double getLocationY()
		{
			return LocationY;
		}

//    *
//	 * Returns the locationnadX.
//	 * @return double
//	 
		public virtual double getLocationnadX()
		{
			return LocationnadX;
		}

//    *
//	 * Returns the locationnadY.
//	 * @return double
//	 
		public virtual double getLocationnadY()
		{
			return LocationnadY;
		}

//    *
//	 * Returns the locationYSet.
//	 * @return boolean
//	 
		public virtual bool isLocationYSet()
		{
			this.LocationYSet = true;
			return LocationYSet;
		}

//    *
//	 * Sets the locationX.
//	 * @param locationX The locationX to set
//	 
		public virtual void setLocationX(double locationX)
		{
			this.LocationXSet = true;
			this.LocationX = locationX;
		}

//    *
//	 * Sets the locationY.
//	 * @param locationY The locationY to set
//	 
		public virtual void setLocationY(double locationY)
		{
			this.LocationY = locationY;
		}

//    *
//	 * Sets the locationnadX.
//	 * @param locationnadX The locationnadX to set
//	 
		public virtual void setLocationnadX(double locationnadX)
		{
			this.LocationnadX = locationnadX;
		}

//    *
//	 * Sets the locationnadY.
//	 * @param locationnadY The locationnadY to set
//	 
		public virtual void setLocationnadY(double locationnadY)
		{
			this.LocationnadY = locationnadY;
		}

//    *
//	 * Returns the fmStatusText.
//	 * @return String
//	 
		public virtual string getFmStatusText()
		{
			return FmStatusText;
		}

//    *
//	 * Sets the fmStatusText.
//	 * @param fmStatusText The fmStatusText to set
//	 
		public virtual void setFmStatusText(string fmStatusText)
		{
			this.FmStatusText = fmStatusText;
		}

//    *
//	 * @return Returns the response1.
//	 
		public virtual string getResponse1()
		{
			return Response1;
		}

//    *
//	 * @param response1 The response1 to set.
//	 
		public virtual void setResponse1(string response1)
		{
			this.Response1 = response1;
		}

//    *
//	 * @return Returns the response2.
//	 
		public virtual string getResponse2()
		{
			return Response2;
		}

//    *
//	 * @param response2 The response2 to set.
//	 
		public virtual void setResponse2(string response2)
		{
			this.Response2 = response2;
		}

//    *
//	 * @return Returns the responseCode1.
//	 
		public virtual int? getResponseCode1()
		{
			return ResponseCode1;
		}

//    *
//	 * @param responseCode1 The responseCode1 to set.
//	 
		public virtual void setResponseCode1(int responseCode1)
		{
			this.ResponseCode1 = responseCode1;
		}

//    *
//	 * @return Returns the responseCode2.
//	 
		public virtual int? getResponseCode2()
		{
			return ResponseCode2;
		}

//    *
//	 * @param responseCode2 The responseCode2 to set.
//	 
		public virtual void setResponseCode2(int responseCode2)
		{
			this.ResponseCode2 = responseCode2;
		}

//    *
//	 * @return Returns the reCredit.
//	 
		public virtual bool getReCredit()
		{
			return ReCredit;
		}

//    *
//	 * @param reCredit The reCredit to set.
//	 
		public virtual void setReCredit(bool reCredit)
		{
			this.ReCredit = reCredit;
		}

//    *
//	 * @return Returns the deviceType.
//	 
		public virtual string getDeviceType()
		{
			return DeviceType;
		}
//    *
//	 * @param deviceType The deviceType to set.
//	 
		public virtual void setDeviceType(string deviceType)
		{
			this.DeviceType = deviceType;
		}

//    *
//	 * @param messageData
//	 
		public virtual void setTrimtracData(TTMessageData messageData)
		{
			this.ttMessageData = messageData;
		}

		public virtual TTMessageData getTrimtracData()
		{
			return this.ttMessageData;
		}

//    *
//	 * @return Returns the fixStatus.
//	 
		public virtual int getFixStatus()
		{
			return FixStatus;
		}

//    *
//	 * @param fixStatus The fixStatus to set.
//	 
		public virtual void setFixStatus(int fixStatus)
		{
			this.FixStatus = fixStatus;
		}

//    *
//	 * @return Returns the fixTime.
//	 
        public virtual DateTime getFixTime()
		{
			return FixTime;
		}

//    *
//	 * @param fixTime The fixTime to set.
//	 
        public virtual void setFixTime(DateTime fixTime)
		{
			this.FixTime = fixTime;
		}

//    *
//	 * @return Returns the reportCause.
//	 
		public virtual int getReportCause()
		{
			return ReportCause;
		}

//    *
//	 * @param reportCause The reportCause to set.
//	 
		public virtual void setReportCause(int reportCause)
		{
			this.ReportCause = reportCause;
		}

		public virtual double getSpeed()
		{
			return Speed;
		}

		public virtual void setSpeed(double speed)
		{
			this.Speed = speed;
		}

//    *
//	 * @return the direction
//	 
		public virtual double getDirection()
		{
			return Direction;
		}

//    *
//	 * @param direction the direction to set
//	 
		public virtual void setDirection(double direction)
		{
			this.Direction = direction;
		}

		public virtual string getAlertInfo()
		{
			return alertInfo;
		}

		public virtual void setAlertInfo(string alertInfo)
		{
			this.alertInfo = alertInfo;
		}

        public virtual string getBatteryLevel()
        {
            return batteryLevel;
        }

        public virtual void setBatteryLevel(string battery)
        {
            this.batteryLevel = battery;
        }


//        public static Comparator townComparator = new Comparator()
        public string FmUid
        {
            get
            {
                return fmUid;
            }
            set
            {
                fmUid = value;
            }
        }
   
        public string FoUid
        {
            get
            {
                return foUid;
            }
            set
            {
                foUid = value;
            }
        }
      
        public int ProviderId
        {
            get
            {
                return providerId;
            }
            set
            {
                providerId = value;
            }
        }
        public int Status
        {
            get
            {
                return status ;
            }
            set
            {
                status = value;
            }
        }
        public DateTime? TimeSent
        {
            get
            {
                return TimeSent1;
            }
            set
            {
                TimeSent1 = value;
            }
        }
        public string TransactionId
        {
            get
            {
                return transactionId;
            }
            set
            {
                transactionId = value;
            }
        }
    }
}