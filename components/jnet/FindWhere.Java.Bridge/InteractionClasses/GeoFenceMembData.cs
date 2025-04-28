namespace com.teleca.fleetonline.repository
{

    //*************************************************************************
    //
    // $Archive: $
    // $Revision: 1.1 $
    // $Date: 2008/04/10 14:27:43 $
    //
    //*************************************************************************


    using GlobalConstants = com.teleca.fleetonline.utils.GlobalConstants;
    using com.teleca.fleetonline.web.bean;
    using System;
    using com.teleca.fleetonline.utils;

    // *
    // * This class holds GeofenceMemb data, i.e. the data that couples
    // * geofences to members.
    // 
    [System.Serializable]
    public class GeoFenceMembData : RepositoryData
    {

        //    *
        //	 * 
        //	 
        private const long serialVersionUID = 1L;

        public long SerialVersionUID
        {
            get { return serialVersionUID; }
        }

        private bool idSet;

        public bool IdSet
        {
            get { return idSet; }
            set { idSet = value; }
        }
        private string id;

        public string Id
        {
            get { return id; }
            set { id = value; }
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
        private string fenceId;

        public string FenceId
        {
            get { return fenceId; }
            set { fenceId = value; }
        }
        private DateTime timeStart; // time window start
        private string txt__timeStart; // time window start

        public string Txt__timeStart
        {
            get { return txt__timeStart; }
            set { txt__timeStart = value; }
        }

        public DateTime TimeStart
        {
            get { return timeStart; }
            set { timeStart = value; }
        }
        private DateTime timeEnd; // time window end
        private string txt__timeEnd; // time window start

        public string Txt__timeEnd
        {
            get { return txt__timeEnd; }
            set { txt__timeEnd = value; }
        }

        public DateTime TimeEnd
        {
            get { return timeEnd; }
            set { timeEnd = value; }
        }
        private int fmode; // 0 = alert when member leaves

        public int Fmode
        {
            get { return fmode; }
            set { fmode = value; }
        }
        private int alertOnce; // send alert only once

        public int AlertOnce
        {
            get { return alertOnce; }
            set { alertOnce = value; }
        }
        private int alertSent; // has an alert been sent

        public int AlertSent
        {
            get { return alertSent; }
            set { alertSent = value; }
        }
        private string fmName;

        public string FmName
        {
            get { return fmName; }
            set { fmName = value; }
        }
        private string fenceName;

        public string FenceName
        {
            get { return fenceName; }
            set { fenceName = value; }
        }

        private int status = GlobalConstants.CONFIG_STATUS_UNDEFINED; // for device-based fences, (NB. default = confirmed, this is default for server-based fences)

        public int Status
        {
            get { return status; }
            set { status = value; }
        }
        private int fenceNumber; // for device-based fences

        public int FenceNumber
        {
            get { return fenceNumber; }
            set { fenceNumber = value; }
        }
        private int enforcement; // for device-based fences

        public int Enforcement
        {
            get { return enforcement; }
            set { enforcement = value; }
        }
        private int schedule; // for device-based fences

        public int Schedule
        {
            get { return schedule; }
            set { schedule = value; }
        }

        public GeoFenceMembData()
            : base()
        {
            idSet = false;
        }

        public GeoFenceMembData(string id)
            : base()
        {
            this.id = id;
            idSet = true;
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

        //    *
        //	 * @return Returns the alertSent.
        //	 
        public virtual int getAlertSent()
        {
            return AlertSent;
        }

        //    *
        //	 * @param alertSent The alertSent to set.
        //	 
        public virtual void setAlertSent(int alertSent)
        {
            this.AlertSent = alertSent;
        }

        //    *
        //	 * @return Returns the fenceId.
        //	 
        public virtual string getFenceId()
        {
            return FenceId;
        }

        //    *
        //	 * @param fenceId The fenceId to set.
        //	 
        public virtual void setFenceId(string fenceId)
        {
            this.FenceId = fenceId;
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
        //	 * @return Returns the id.
        //	 
        public virtual string getId()
        {
            return Id;
        }

        //    *
        //	 * @param id The id to set.
        //	 
        public virtual void setId(string id)
        {
            this.Id = id;
        }

        //    *
        //	 * @return Returns the fmode.
        //	 
        public virtual int getFmode()
        {
            return Fmode;
        }

        //    *
        //	 * @param fmode The fmode to set.
        //	 
        public virtual void setFmode(int fmode)
        {
            this.Fmode = fmode;
        }

        //    *
        //	 * @return Returns the timeEnd.
        //	 
        public virtual DateTime getTimeEnd()
        {
            return TimeEnd;
        }

        //    *
        //	 * @param timeEnd The timeEnd to set.
        //	 
        public virtual void setTimeEnd(DateTime timeEnd)
        {
            this.TimeEnd = timeEnd;
        }

        //    *
        //	 * @return Returns the timeStart.
        //	 
        public virtual DateTime getTimeStart()
        {
            return TimeStart;
        }

        //    *
        //	 * @param timeStart The timeStart to set.
        //	 
        public virtual void setTimeStart(DateTime timeStart)
        {
            this.TimeStart = timeStart;
        }

        //    *
        //	 * @return Returns the idSet.
        //	 
        public virtual bool isIdSet()
        {
            return IdSet;
        }


        //    *
        //	 * @return Returns the fenceName.
        //	 
        public virtual string getFenceName()
        {
            return FenceName;
        }
        //    *
        //	 * @param fenceName The fenceName to set.
        //	 
        public virtual void setFenceName(string fenceName)
        {
            this.FenceName = fenceName;
        }
        //    *
        //	 * @return Returns the fmName.
        //	 
        public virtual string getFmName()
        {
            return FmName;
        }
        //    *
        //	 * @param fmName The fmName to set.
        //	 
        public virtual void setFmName(string fmName)
        {
            this.FmName = fmName;
        }

        //    *
        //	 * @return Returns the fenceNumber.
        //	 
        public virtual int getFenceNumber()
        {
            return FenceNumber;
        }

        //    *
        //	 * @param fenceNumber The fenceNumber to set.
        //	 
        public virtual void setFenceNumber(int fenceNumber)
        {
            this.FenceNumber = fenceNumber;
        }

        //    *
        //	 * @return Returns the status.
        //	 
        //public virtual int getStatus()
        //{
        //    return status;
        //}

        //    *
        //	 * @param status The status to set.
        //	 
        //public virtual void setStatus(int status)
        //{
        //    this.status = status;
        //}

        //    *
        //	 * @return Returns the enforcement.
        //	 
        public virtual int getEnforcement()
        {
            return Enforcement;
        }

        //    *
        //	 * @param enforcement The enforcement to set.
        //	 
        public virtual void setEnforcement(int enforcement)
        {
            this.Enforcement = enforcement;
        }

        //    *
        //	 * @return Returns the schedule.
        //	 
        public virtual int getSchedule()
        {
            return Schedule;
        }

        //    *
        //	 * @param schedule The schedule to set.
        //	 
        public virtual void setSchedule(int schedule)
        {
            this.Schedule = schedule;
        }


    }
}