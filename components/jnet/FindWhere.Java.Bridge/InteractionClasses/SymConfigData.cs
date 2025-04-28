namespace com.teleca.fleetonline.repository
{
    //*************************************************************************
    //
    // $Source: D:/Data/cvs/FOL511/ejb_module/src/com/teleca/fleetonline/repository/SymConfigData.java,v $
    // $Revision: 1.1 $
    // $Date: 2008/11/25 11:09:05 $
    //
    // Copyright(c) Teydo BV, Bilthoven, all rights reserved worldwide.
    //
    //*************************************************************************
    [System.Serializable]
    public class SymConfigData 
    {

        private string foId = null;

        public string FoId
        {
            get { return foId; }
            set { foId = value; }
        }
        private string fmId = null;

        public string FmId
        {
            get { return fmId; }
            set { fmId = value; }
        }

        // fields below hold config status of the Symmetry device 
        private int geofence; // default = 0 = undefined

        public int Geofence
        {
            get { return geofence; }
            set { geofence = value; }
        }
        private int autotrack; // default = 0 = undefined

        public int Autotrack
        {
            get { return autotrack; }
            set { autotrack = value; }
        }
        private int rf; // default = 0 = undefined

        public int Rf
        {
            get { return rf; }
            set { rf = value; }
        }
        private int gsm; // default = 0 = undefined

        public int Gsm
        {
            get { return gsm; }
            set { gsm = value; }
        }
        private int movement; // default = 0 = undefined

        public int Movement
        {
            get { return movement; }
            set { movement = value; }
        }
        private int alarmStatus;

        public int AlarmStatus
        {
            get { return alarmStatus; }
            set { alarmStatus = value; }
        }

        // 0 = undefined, 1 = requested, 2 = confirmed
        private int dialsSet; // status indicator of whether caller contacts are stored in device

        public int DialsSet
        {
            get { return dialsSet; }
            set { dialsSet = value; }
        }

        public virtual int getAutotrack()
        {
            return Autotrack;
        }
        public virtual void setAutotrack(int autotrack)
        {
            this.Autotrack = autotrack;
        }
        public virtual string getFmId()
        {
            return FmId;
        }
        public virtual void setFmId(string fmId)
        {
            this.FmId = fmId;
        }
        public virtual string getFoId()
        {
            return FoId;
        }
        public virtual void setFoId(string foId)
        {
            this.FoId = foId;
        }
        public virtual int getGeofence()
        {
            return Geofence;
        }
        public virtual void setGeofence(int geofence)
        {
            this.Geofence = geofence;
        }
        public virtual int getGsm()
        {
            return Gsm;
        }
        public virtual void setGsm(int gsm)
        {
            this.Gsm = gsm;
        }
        public virtual int getMovement()
        {
            return Movement;
        }
        public virtual void setMovement(int movement)
        {
            this.Movement = movement;
        }
        public virtual int getRf()
        {
            return Rf;
        }
        public virtual void setRf(int rf)
        {
            this.Rf = rf;
        }
        public virtual int getDialsSet()
        {
            return DialsSet;
        }
        public virtual void setDialsSet(int dialsSet)
        {
            this.DialsSet = dialsSet;
        }

        public virtual int getAlarmStatus()
        {
            return AlarmStatus;
        }
        public virtual void setAlarmStatus(int alarmStatus)
        {
            this.AlarmStatus = alarmStatus;
        }
    }
}