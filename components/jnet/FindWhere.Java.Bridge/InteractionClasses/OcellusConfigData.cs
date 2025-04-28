namespace com.teleca.fleetonline.repository
{

    //*************************************************************************
    //
    // $Source: D:/Data/cvs/FOL/src/java/ejb_module/src/com/teleca/fleetonline/repository/OcellusConfigData.java,v $
    // $Revision: 1.5 $
    // $Date: 2007/01/16 14:32:59 $
    //
    // Copyright (c) Teydo BV, Bilthoven, all rights reserved worldwide.
    //
    //*************************************************************************

    using GlobalConstants = com.teleca.fleetonline.utils.GlobalConstants;
    using com.teleca.fleetonline.web.bean;

    ///
    /// This class holds Ocellus Config data
    //
    [System.Serializable]
    public class OcellusConfigData : RepositoryData
    {
        private const long serialVersionUID = 1L;

        private int recId;
        /// <summary>
        /// 
        /// </summary>
        public int RecId
        {
            get { return recId; }
            set { recId = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        private string userId;
        /// <summary>
        /// 
        /// </summary>
        public string UserId
        {
            get { return userId; }
            set { userId = value; }
        }
        private int isProfile;
        /// <summary>
        /// 
        /// </summary>
        public int IsProfile
        {
            get { return isProfile; }
            set { isProfile = value; }
        }
        private string profName;
        /// <summary>
        /// 
        /// </summary>
        public string ProfName
        {
            get { return profName; }
            set { profName = value; }
        }
        private string reportVars;
        /// <summary>
        /// 
        /// </summary>
        public string ReportVars
        {
            get { return reportVars; }
            set { reportVars = value; }
        }
        private int gpsThrottle; // 0-9
        /// <summary>
        /// 
        /// </summary>
        public int GpsThrottle
        {
            get { return gpsThrottle; }
            set { gpsThrottle = value; }
        }
        private string LEDBrightness; // o, L, H (off, Low, High)
        /// <summary>
        /// 
        /// </summary>
        public string LEDBrightness1
        {
            get { return LEDBrightness; }
            set { LEDBrightness = value; }
        }
        private int motionSensorFilter; // in secs; Activity Sensor in panel 0-9
        /// <summary>
        /// 
        /// </summary>
        public int MotionSensorFilter
        {
            get { return motionSensorFilter; }
            set { motionSensorFilter = value; }
        }
        private int reportActive; // 0 or 1 on or off
        /// <summary>
        /// 
        /// </summary>
        public int ReportActive
        {
            get { return reportActive; }
            set { reportActive = value; }
        }
        private string reportIntervalWithoutMotion;
        /// <summary>
        /// /
        /// </summary>
        public string ReportIntervalWithoutMotion
        {
            get { return reportIntervalWithoutMotion; }
            set { reportIntervalWithoutMotion = value; }
        }
        private string reportIntervalWithMotion;
        /// <summary>
        /// 
        /// </summary>
        public string ReportIntervalWithMotion
        {
            get { return reportIntervalWithMotion; }
            set { reportIntervalWithMotion = value; }
        }
        private int runGpsAlways;
        /// <summary>
        /// 
        /// </summary>
        public int RunGpsAlways
        {
            get { return runGpsAlways; }
            set { runGpsAlways = value; }
        }


        // these are the additional variables; variables that can be requested to be sent with each position message 
        private int altitude; // 0 or 1 on or off
        /// <summary>
        /// 
        /// </summary>
        public int Altitude
        {
            get { return altitude; }
            set { altitude = value; }
        }
        private int speed; // 0 or 1 on or off
        /// <summary>
        /// 
        /// </summary>
        public int Speed
        {
            get { return speed; }
            set { speed = value; }
        }
        private int hae; // 0 or 1 on or off
        /// <summary>
        /// 
        /// </summary>
        public int Hae
        {
            get { return hae; }
            set { hae = value; }
        }
        private int vae; // 0 or 1 on or off
        /// <summary>
        /// 
        /// </summary>
        public int Vae
        {
            get { return vae; }
            set { vae = value; }
        }
        private int battery; // 0 or 1 on or off
        /// <summary>
        /// 
        /// </summary>
        public int Battery
        {
            get { return battery; }
            set { battery = value; }
        }
        private int secInMotion; // 0 or 1 on or off
        /// <summary>
        /// 
        /// </summary>
        public int SecInMotion
        {
            get { return secInMotion; }
            set { secInMotion = value; }
        }
        private int reportInterval; // 0 or 1 on or off
        /// <summary>
        /// 
        /// </summary>
        public int ReportInterval
        {
            get { return reportInterval; }
            set { reportInterval = value; }
        }
        private int initCreditSms; // 0 or 1 on or off
        /// <summary>
        /// 
        /// </summary>
        public int InitCreditSms
        {
            get { return initCreditSms; }
            set { initCreditSms = value; }
        }
        private int remCreditSms; // 0 or 1 on or off
        /// <summary>
        /// 
        /// </summary>
        public int RemCreditSms
        {
            get { return remCreditSms; }
            set { remCreditSms = value; }
        }
        private int initCreditGprs; // 0 or 1 on or off

        public int InitCreditGprs
        {
            get { return initCreditGprs; }
            set { initCreditGprs = value; }
        }
        private int remCreditGprs; // 0 or 1 on or off

        public int RemCreditGprs
        {
            get { return remCreditGprs; }
            set { remCreditGprs = value; }
        }
        private int gpsTimeRunning; // 0 or 1 on or off

        public int GpsTimeRunning
        {
            get { return gpsTimeRunning; }
            set { gpsTimeRunning = value; }
        }
        private int courseOverGround; // 0 or 1 on or off

        public int CourseOverGround
        {
            get { return courseOverGround; }
            set { courseOverGround = value; }
        }

        // new additional variables for 1.05
        private int diag1, diag2, diag3;

        public int Diag3
        {
            get { return diag3; }
            set { diag3 = value; }
        }

        public int Diag2
        {
            get { return diag2; }
            set { diag2 = value; }
        }

        public int Diag1
        {
            get { return diag1; }
            set { diag1 = value; }
        }
        private string smsRep;

        public string SmsRep
        {
            get { return smsRep; }
            set { smsRep = value; }
        }
        private string smsCheck;

        public string SmsCheck
        {
            get { return smsCheck; }
            set { smsCheck = value; }
        }


        //    *
        //	 * parses the additional variables string and sets the appropriate fields
        //	 * @param repVars The repVars to set.
        //	 
        //public virtual void setReportVars(string reportVars)
        //{
        //    this.reportVars = reportVars;

        //    // now parse the string if applicable
        //    if (reportVars != null && reportVars.Length > 0)
        //    {
        //        for (int i = 0; i < reportVars.Length; i++)
        //        {
        //            char c = reportVars[i];
        //            if (GlobalConstants.ALTITUDE_CODE.Equals(new string((new char(c)).ToString())))
        //            {
        //                altitude = 1;
        //            }
        //            if (GlobalConstants.SPEED_CODE.Equals(new string((new char(c)).ToString())))
        //            {
        //                speed = 1;
        //            }
        //            if (GlobalConstants.HORIZONTAL_ACCURACY_ESTIMATE_CODE.Equals(new string((new char(c)).ToString())))
        //            {
        //                hae = 1;
        //            }
        //            if (GlobalConstants.VERTICAL_ACCURACY_ESTIMATE_CODE.Equals(new string((new char(c)).ToString())))
        //            {
        //                vae = 1;
        //            }
        //            if (GlobalConstants.BATTERY_CAPACITY_CODE.Equals(new string((new char(c)).ToString())))
        //            {
        //                battery = 1;
        //            }
        //            if (GlobalConstants.SECONDS_IN_MOTION_CODE.Equals(new string((new char(c)).ToString())))
        //            {
        //                secInMotion = 1;
        //            }
        //            if (GlobalConstants.REPORT_INTERVAL_CODE.Equals(new string((new char(c)).ToString())))
        //            {
        //                reportInterval = 1;
        //            }
        //            if (GlobalConstants.INIT_CRED_SMS_CODE.Equals(new string((new char(c)).ToString())))
        //            {
        //                initCreditSms = 1;
        //            }
        //            if (GlobalConstants.REMAIMING_CRED_SMS_CODE.Equals(new string((new char(c)).ToString())))
        //            {
        //                remCreditSms = 1;
        //            }
        //            if (GlobalConstants.INIT_CRED_GPRS_CODE.Equals(new string((new char(c)).ToString())))
        //            {
        //                initCreditGprs = 1;
        //            }
        //            if (GlobalConstants.REMAINING_CRED_GPRS_CODE.Equals(new string((new char(c)).ToString())))
        //            {
        //                remCreditGprs = 1;
        //            }
        //            if (GlobalConstants.GPRS_TIME_RUNNING_CODE.Equals(new string((new char(c)).ToString())))
        //            {
        //                gpsTimeRunning = 1;
        //            }
        //            if (GlobalConstants.COURSE_OVER_GROUND_CODE.Equals(new string((new char(c)).ToString())))
        //            {
        //                courseOverGround = 1;
        //            }
        //            if (GlobalConstants.DIAGNOSE1.Equals(new string((new char(c)).ToString())))
        //            {
        //                diag1 = 1;
        //            }
        //            if (GlobalConstants.DIAGNOSE2.Equals(new string((new char(c)).ToString())))
        //            {
        //                diag2 = 1;
        //            }
        //            if (GlobalConstants.DIAGNOSE3.Equals(new string((new char(c)).ToString())))
        //            {
        //                diag3 = 1;
        //            }
        //        }
        //    }
        //}

        //    *
        //	 * @return Returns the reportVars.
        //	 
        public virtual string getReportVars()
        {
            return ReportVars;
        }
        //    *
        //	 * @return Returns the gpsThrottle.
        //	 
        public virtual int getGpsThrottle()
        {
            return GpsThrottle;
        }
        //    *
        //	 * @param gpsThrottle The gpsThrottle to set.
        //	 
        public virtual void setGpsThrottle(int gpsThrottle)
        {
            this.GpsThrottle = gpsThrottle;
        }
        //    *
        //	 * @return Returns the lEDBrightness.
        //	 
        public virtual string getLEDBrightness()
        {
            return LEDBrightness1;
        }
        //    *
        //	 * @param brightness The lEDBrightness to set.
        //	 
        public virtual void setLEDBrightness(string brightness)
        {
            LEDBrightness1 = brightness;
        }
        //    *
        //	 * @return Returns the isProfile.
        //	 
        public virtual int getIsProfile()
        {
            return IsProfile;
        }
        //    *
        //	 * @param isProfile The isProfile to set.
        //	 
        public virtual void setIsProfile(int isProfile)
        {
            this.IsProfile = isProfile;
        }
        //    *
        //	 * @return Returns the motionSensorFilter.
        //	 
        public virtual int getMotionSensorFilter()
        {
            return MotionSensorFilter;
        }
        //    *
        //	 * @param motionSensorFilter The motionSensorFilter to set.
        //	 
        public virtual void setMotionSensorFilter(int motionSensorFilter)
        {
            this.MotionSensorFilter = motionSensorFilter;
        }
        //    *
        //	 * @return Returns the profName.
        //	 
        public virtual string getProfName()
        {
            return ProfName;
        }
        //    *
        //	 * @param profName The profName to set.
        //	 
        public virtual void setProfName(string profName)
        {
            this.ProfName = profName;
        }
        //    *
        //	 * @return Returns the recId.
        //	 
        public virtual int getRecId()
        {
            return RecId;
        }
        //    *
        //	 * @param recId The recId to set.
        //	 
        public virtual void setRecId(int recId)
        {
            this.RecId = recId;
        }
        //    *
        //	 * @return Returns the reportActive.
        //	 
        public virtual int getReportActive()
        {
            return ReportActive;
        }
        //    *
        //	 * @param reportActive The reportActive to set.
        //	 
        public virtual void setReportActive(int reportActive)
        {
            this.ReportActive = reportActive;
        }
        //    *
        //	 * @return Returns the reportIntervalWithMotion.
        //	 
        public virtual string getReportIntervalWithMotion()
        {
            return ReportIntervalWithMotion;
        }
        //    *
        //	 * @param reportIntervalWithMotion The reportIntervalWithMotion to set.
        //	 
        public virtual void setReportIntervalWithMotion(string reportIntervalWithMotion)
        {
            this.ReportIntervalWithMotion = reportIntervalWithMotion;
        }
        //    *
        //	 * @return Returns the reportIntervalWithoutMotion.
        //	 
        public virtual string getReportIntervalWithoutMotion()
        {
            return ReportIntervalWithoutMotion;
        }
        //    *
        //	 * @param reportIntervalWithoutMotion The reportIntervalWithoutMotion to set.
        //	 
        public virtual void setReportIntervalWithoutMotion(string reportIntervalWithoutMotion)
        {
            this.ReportIntervalWithoutMotion = reportIntervalWithoutMotion;
        }
        //    *
        //	 * @return Returns the runGpsAlways.
        //	 
        public virtual int getRunGpsAlways()
        {
            return RunGpsAlways;
        }
        //    *
        //	 * @param runGpsAlways The runGpsAlways to set.
        //	 
        public virtual void setRunGpsAlways(int runGpsAlways)
        {
            this.RunGpsAlways = runGpsAlways;
        }
        //    *
        //	 * @return Returns the userId.
        //	 
        public virtual string getUserId()
        {
            return UserId;
        }
        //    *
        //	 * @param userId The userId to set.
        //	 
        public virtual void setUserId(string userId)
        {
            this.UserId = userId;
        }
        //    *
        //	 * @return Returns the altitude.
        //	 
        public virtual int getAltitude()
        {
            return Altitude;
        }
        //    *
        //	 * @return Returns the battery.
        //	 
        public virtual int getBattery()
        {
            return Battery;
        }
        //    *
        //	 * @return Returns the gpsTimeRunning.
        //	 
        public virtual int getGpsTimeRunning()
        {
            return GpsTimeRunning;
        }
        //    *
        //	 * @return Returns the hae.
        //	 
        public virtual int getHae()
        {
            return Hae;
        }
        //    *
        //	 * @return Returns the initCreditGprs.
        //	 
        public virtual int getInitCreditGprs()
        {
            return InitCreditGprs;
        }
        //    *
        //	 * @return Returns the initCreditSms.
        //	 
        public virtual int getInitCreditSms()
        {
            return InitCreditSms;
        }
        //    *
        //	 * @return Returns the remCreditGprs.
        //	 
        public virtual int getRemCreditGprs()
        {
            return RemCreditGprs;
        }
        //    *
        //	 * @return Returns the remCreditSms.
        //	 
        public virtual int getRemCreditSms()
        {
            return RemCreditSms;
        }
        //    *
        //	 * @return Returns the reportInterval.
        //	 
        public virtual int getReportInterval()
        {
            return ReportInterval;
        }
        //    *
        //	 * @return Returns the secInMotion.
        //	 
        public virtual int getSecInMotion()
        {
            return SecInMotion;
        }
        //    *
        //	 * @return Returns the speed.
        //	 
        public virtual int getSpeed()
        {
            return Speed;
        }
        //    *
        //	 * @return Returns the vae.
        //	 
        public virtual int getVae()
        {
            return Vae;
        }
        //    *
        //	 * @return Returns the courseOverGround.
        //	 
        public virtual int getCourseOverGround()
        {
            return CourseOverGround;
        }

        public virtual int getDiag1()
        {
            return Diag1;
        }

        public virtual void setDiag1(int diag1)
        {
            this.Diag1 = diag1;
        }

        public virtual int getDiag2()
        {
            return Diag2;
        }

        public virtual void setDiag2(int diag2)
        {
            this.Diag2 = diag2;
        }

        public virtual int getDiag3()
        {
            return Diag3;
        }

        public virtual void setDiag3(int diag3)
        {
            this.Diag3 = diag3;
        }

        public virtual string getSmsCheck()
        {
            return SmsCheck;
        }

        public virtual void setSmsCheck(string smsCheck)
        {
            this.SmsCheck = smsCheck;
        }

        public virtual string getSmsRep()
        {
            return SmsRep;
        }

        public virtual void setSmsRep(string smsRep)
        {
            this.SmsRep = smsRep;
        }

        public virtual void setCourseOverGround(int courseOverGround)
        {
            this.CourseOverGround = courseOverGround;
        }

        public override bool Equals(object obj)
        {
            bool isEqual = true;
            if (obj == this)
                return true;

            // is obj reference null 
            if (obj == null)
                return false;

            // make sure references are of same type 
            if (obj is OcellusConfigData)
            {
                OcellusConfigData other = (OcellusConfigData)obj;
                if (Altitude != other.getAltitude() || Battery != other.getBattery() || GpsTimeRunning != other.getGpsTimeRunning() || !GpsThrottle.Equals(other.getGpsThrottle()) || Hae != other.getHae() || InitCreditGprs != other.getInitCreditGprs() || InitCreditSms != other.getInitCreditSms() || IsProfile != other.getIsProfile() || !MotionSensorFilter.Equals(other.getMotionSensorFilter()) || RemCreditGprs != other.getRemCreditGprs() || RemCreditSms != other.getRemCreditSms() || !ReportActive.Equals(other.getReportActive()) || ReportInterval != other.getReportInterval() || !ReportIntervalWithMotion.Equals(other.getReportIntervalWithMotion()) || !ReportIntervalWithoutMotion.Equals(other.getReportIntervalWithoutMotion()) || !ReportVars.Equals(other.getReportVars()) || !RunGpsAlways.Equals(other.getRunGpsAlways()) || SecInMotion != other.getSecInMotion() || getSpeed() != other.getSpeed() || !UserId.Equals(other.getUserId()) || Vae != other.getVae() || LEDBrightness1 != other.getLEDBrightness() || SmsCheck != other.getSmsCheck() || SmsRep != other.getSmsRep() || CourseOverGround != other.getCourseOverGround())
                isEqual = false;
            }
            return isEqual;
        }
    }
}