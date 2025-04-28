using com.teleca.fleetonline.web.bean;
using System;
namespace com.teleca.fleetonline.repository
{


    //*************************************************************************
    //
    // $Source: D:/Data/cvs/FOL511/ejb_module/src/com/teleca/fleetonline/repository/SymPosData.java,v $
    // $Revision: 1.1 $
    // $Date: 2008/11/25 11:09:06 $
    //
    // Copyright (c) Teydo BV, Bilthoven, all rights reserved worldwide.
    //
    //*************************************************************************

    ///
    /// This class holds Symmetry Other Position data
    //

    public class SymPosData : RepositoryData
    {

        private long lbsId; // this is also the record id !
        private string foId;
        private string fmId;
        private DateTime timeStamp;

        private int battery;
        private double speed;
        private int cid; // cell identity code
        private int lac; // location area code

        private int fenceId;

        //    *
        //	 * @return Returns the battery.
        //	 
        public virtual int getBattery()
        {
            return battery;
        }
        //    *
        //	 * @param battery The battery to set.
        //	 
        public virtual void setBattery(int battery)
        {
            this.battery = battery;
        }
        //    *
        //	 * @return Returns the cid.
        //	 
        public virtual int getCid()
        {
            return cid;
        }
        //    *
        //	 * @param cid The cid to set.
        //	 
        public virtual void setCid(int cid)
        {
            this.cid = cid;
        }
        //    *
        //	 * @return Returns the fenceId.
        //	 
        public virtual int getFenceId()
        {
            return fenceId;
        }
        //    *
        //	 * @param fenceId The fenceId to set.
        //	 
        public virtual void setFenceId(int fenceId)
        {
            this.fenceId = fenceId;
        }
        //    *
        //	 * @return Returns the fmId.
        //	 
        public virtual string getFmId()
        {
            return fmId;
        }
        //    *
        //	 * @param fmId The fmId to set.
        //	 
        public virtual void setFmId(string fmId)
        {
            this.fmId = fmId;
        }
        //    *
        //	 * @return Returns the foId.
        //	 
        public virtual string getFoId()
        {
            return foId;
        }
        //    *
        //	 * @param foId The foId to set.
        //	 
        public virtual void setFoId(string foId)
        {
            this.foId = foId;
        }
        //    *
        //	 * @return Returns the lac.
        //	 
        public virtual int getLac()
        {
            return lac;
        }
        //    *
        //	 * @param lac The lac to set.
        //	 
        public virtual void setLac(int lac)
        {
            this.lac = lac;
        }
        //    *
        //	 * @return Returns the lbsId.
        //	 
        public virtual long getLbsId()
        {
            return lbsId;
        }
        //    *
        //	 * @param lbsId The lbsId to set.
        //	 
        public virtual void setLbsId(long lbsId)
        {
            this.lbsId = lbsId;
        }
        //    *
        //	 * @return Returns the speed.
        //	 
        public virtual double getSpeed()
        {
            return speed;
        }
        //    *
        //	 * @param speed The speed to set.
        //	 
        public virtual void setSpeed(double speed)
        {
            this.speed = speed;
        }
        //    *
        //	 * @return Returns the timeStamp.
        //	 
        public virtual DateTime getTimeStamp()
        {
            return timeStamp;
        }
        //    *
        //	 * @param timeStamp The timeStamp to set.
        //	 
        public virtual void setTimeStamp(DateTime timeStamp)
        {
            this.timeStamp = timeStamp;
        }
    }
}