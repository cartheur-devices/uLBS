using System;
using com.teleca.fleetonline.web.bean;
namespace com.teleca.fleetonline.repository
{


    //*************************************************************************
    //
    // $Source: D:/Data/cvs/FOL/src/java/ejb_module/src/com/teleca/fleetonline/repository/OcellusVarData.java,v $
    // $Revision: 1.9 $
    // $Date: 2007/01/24 15:02:57 $
    //
    // Copyright (c) Teydo BV, Bilthoven, all rights reserved worldwide.
    //
    //*************************************************************************

    ///
    /// This class holds Ocellus (Additional) Variables data
    //
    [System.Serializable]
    public class OcellusVarData : RepositoryData
    {

        // Only for display...
        private string member;

        public string Member
        {
          get { return member; }
          set { member = value; }
        }

        private string dateTime;

        public string DateTime
        {
            get { return dateTime; }
            set { dateTime = value; }
        }

        private DateTime dateTimeM;

        public DateTime DateTimeM
        {
            get { return dateTimeM; }
            set { dateTimeM = value; }
        }

        private const long serialVersionUID = 1L;

        private long recId;

        public long RecId
        {
            get { return recId; }
            set { recId = value; }
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
        private long lbsId;

        public long LbsId
        {
            get { return lbsId; }
            set { lbsId = value; }
        }
        private bool validPos = true; // default

        public bool ValidPos
        {
            get { return validPos; }
            set { validPos = value; }
        }

        private int fixStatus;

        public int FixStatus
        {
            get { return fixStatus; }
            set { fixStatus = value; }
        }

        private int altitude;

        public int Altitude
        {
            get { return altitude; }
            set { altitude = value; }
        }
        private int speed;

        public int Speed
        {
            get { return speed; }
            set { speed = value; }
        }
        private float horAcc;

        public float HorAcc
        {
            get { return horAcc; }
            set { horAcc = value; }
        }
        private float vertAcc;

        public float VertAcc
        {
            get { return vertAcc; }
            set { vertAcc = value; }
        }
        private int battery;

        public int Battery
        {
            get { return battery; }
            set { battery = value; }
        }
        private string inMotion;

        public string InMotion
        {
            get { return inMotion; }
            set { inMotion = value; }
        }
        private string reportInterval;

        public string ReportInterval
        {
            get { return reportInterval; }
            set { reportInterval = value; }
        }
        private string initCreditSms;

        public string InitCreditSms
        {
            get { return initCreditSms; }
            set { initCreditSms = value; }
        }
        private string remCreditSms;

        public string RemCreditSms
        {
            get { return remCreditSms; }
            set { remCreditSms = value; }
        }
        private string initCreditGprs;

        public string InitCreditGprs
        {
            get { return initCreditGprs; }
            set { initCreditGprs = value; }
        }
        private string remCreditGprs;

        public string RemCreditGprs
        {
            get { return remCreditGprs; }
            set { remCreditGprs = value; }
        }
        private int gpsTimeRunning;

        public int GpsTimeRunning
        {
            get { return gpsTimeRunning; }
            set { gpsTimeRunning = value; }
        }
        private DateTime msgTime;

        public DateTime MsgTime
        {
            get { return msgTime; }
            set { msgTime = value; }
        }
        private string alertInfo;

        public string AlertInfo
        {
            get { return alertInfo; }
            set { alertInfo = value; }
        }
        private int courseOverGround; // course over ground

        public int CourseOverGround
        {
            get { return courseOverGround; }
            set { courseOverGround = value; }
        }

        private string directionStr;

        public string DirectionStr
        {
            get { return directionStr; }
            set { directionStr = value; }
        }

        private string speedStr;

        public string SpeedStr
        {
            get { return speedStr; }
            set { speedStr = value; }
        }
        private string altitudeStr;

        public string AltitudeStr
        {
            get { return altitudeStr; }
            set { altitudeStr = value; }
        }

        private string batteryStr;

        public string BatteryStr
        {
            get { return batteryStr; }
            set { batteryStr = value; }
        }


        //    *
        //	 * @return Returns the altitude.
        //	 
        public virtual int getAltitude()
        {
            return Altitude;
        }
        //    *
        //	 * @param altitude The altitude to set.
        //	 
        public virtual void setAltitude(int altitude)
        {
            this.Altitude = altitude;
        }
        //    *
        //	 * @return Returns the battery.
        //	 
        public virtual int getBattery()
        {
            return Battery;
        }
        //    *
        //	 * @param battery The battery to set.
        //	 
        public virtual void setBattery(int battery)
        {
            this.Battery = battery;
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
        //	 * @return Returns the gpsTimeRunning.
        //	 
        public virtual int getGpsTimeRunning()
        {
            return GpsTimeRunning;
        }
        //    *
        //	 * @param gpsTimeRunning The gpsTimeRunning to set.
        //	 
        public virtual void setGpsTimeRunning(int gpsTimeRunning)
        {
            this.GpsTimeRunning = gpsTimeRunning;
        }
        //    *
        //	 * @return Returns the horAcc.
        //	 
        public virtual float getHorAcc()
        {
            return HorAcc;
        }
        //    *
        //	 * @param horAcc The horAcc to set.
        //	 
        public virtual void setHorAcc(float horAcc)
        {
            this.HorAcc = horAcc;
        }
        //    *
        //	 * @return Returns the initCreditGprs.
        //	 
        public virtual string getInitCreditGprs()
        {
            return InitCreditGprs;
        }
        //    *
        //	 * @param initCreditGprs The initCreditGprs to set.
        //	 
        public virtual void setInitCreditGprs(string initCreditGprs)
        {
            this.InitCreditGprs = initCreditGprs;
        }
        //    *
        //	 * @return Returns the initCreditSms.
        //	 
        public virtual string getInitCreditSms()
        {
            return InitCreditSms;
        }
        //    *
        //	 * @param initCreditSms The initCreditSms to set.
        //	 
        public virtual void setInitCreditSms(string initCreditSms)
        {
            this.InitCreditSms = initCreditSms;
        }
        //    *
        //	 * @return Returns the inMotion.
        //	 
        public virtual string getInMotion()
        {
            return InMotion;
        }
        //    *
        //	 * @param inMotion The inMotion to set.
        //	 
        public virtual void setInMotion(string inMotion)
        {
            this.InMotion = inMotion;
        }
        //    *
        //	 * @return Returns the lbsId.
        //	 
        public virtual long getLbsId()
        {
            return LbsId;
        }
        //    *
        //	 * @param lbsId The lbsId to set.
        //	 
        public virtual void setLbsId(long lbsId)
        {
            this.LbsId = lbsId;
        }
        //    *
        //	 * @return Returns the msgTime.
        //	 
        public virtual DateTime getMsgTime()
        {
            return MsgTime;
        }
        //    *
        //	 * @param msgTime The msgTime to set.
        //	 
        public virtual void setMsgTime(DateTime msgTime)
        {
            this.MsgTime = msgTime;
        }
        //    *
        //	 * @return Returns the recId.
        //	 
        public virtual long getRecId()
        {
            return RecId;
        }
        //    *
        //	 * @param recId The recId to set.
        //	 
        public virtual void setRecId(long recId)
        {
            this.RecId = recId;
        }
        //    *
        //	 * @return Returns the remCreditGprs.
        //	 
        public virtual string getRemCreditGprs()
        {
            return RemCreditGprs;
        }
        //    *
        //	 * @param remCreditGprs The remCreditGprs to set.
        //	 
        public virtual void setRemCreditGprs(string remCreditGprs)
        {
            this.RemCreditGprs = remCreditGprs;
        }
        //    *
        //	 * @return Returns the remCreditSms.
        //	 
        public virtual string getRemCreditSms()
        {
            return RemCreditSms;
        }
        //    *
        //	 * @param remCreditSms The remCreditSms to set.
        //	 
        public virtual void setRemCreditSms(string remCreditSms)
        {
            this.RemCreditSms = remCreditSms;
        }
        //    *
        //	 * @return Returns the reportInterval.
        //	 
        public virtual string getReportInterval()
        {
            return ReportInterval;
        }
        //    *
        //	 * @param reportInterval The reportInterval to set.
        //	 
        public virtual void setReportInterval(string reportInterval)
        {
            this.ReportInterval = reportInterval;
        }
        //    *
        //	 * @return Returns the speed.
        //	 
        public virtual int getSpeed()
        {
            return Speed;
        }
        //    *
        //	 * @param speed The speed to set.
        //	 
        public virtual void setSpeed(int speed)
        {
            this.Speed = speed;
        }
        //    *
        //	 * @return Returns the vertAcc.
        //	 
        public virtual float getVertAcc()
        {
            return VertAcc;
        }
        //    *
        //	 * @param vertAcc The vertAcc to set.
        //	 
        public virtual void setVertAcc(float vertAcc)
        {
            this.VertAcc = vertAcc;
        }
        public virtual string getAlertInfo()
        {
            return AlertInfo;
        }
        public virtual void setAlertInfo(string alertInfo)
        {
            this.AlertInfo = alertInfo;
        }
        //    *
        //	 * @return Returns the courseOverGround.
        //	 
        public virtual int getCourseOverGround()
        {
            return CourseOverGround;
        }
        //    *
        //	 * @param courseOverGround The courseOverGround to set.
        //	 
        public virtual void setCourseOverGround(int courseOverGround)
        {
            this.CourseOverGround = courseOverGround;
        }
        //    *
        //	 * @return Returns the validPos.
        //	 
        public virtual bool isValidPos()
        {
            return ValidPos;
        }
        //    *
        //	 * @param validPos The validPos to set.
        //	 
        public virtual void setValidPos(bool validPos)
        {
            this.ValidPos = validPos;
        }
    }
}