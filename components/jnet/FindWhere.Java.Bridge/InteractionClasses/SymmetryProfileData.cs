namespace com.teleca.fleetonline.repository
{

    //*************************************************************************
    //
    // $Source: D:/Data/cvs/FOL511/ejb_module/src/com/teleca/fleetonline/repository/SymmetryProfileData.java,v $
    // $Revision: 1.2 $
    // $Date: 2008/11/25 11:09:06 $
    //
    // Copyright(c) Teydo BV, Bilthoven, all rights reserved worldwide.
    //
    //************************************************************************* 

    [System.Serializable]
    public class SymmetryProfileData
    {

        //    *
        //	 * 
        //	 
        private const long serialVersionUID = 1L;

        public long SerialVersionUID
        {
            get { return serialVersionUID; }
        }

        private int recId;

        public int RecId
        {
            get { return recId; }
            set { recId = value; }
        }
        private string userId;

        public string UserId
        {
            get { return userId; }
            set { userId = value; }
        }

        private int isProfile;

        public int IsProfile
        {
            get { return isProfile; }
            set { isProfile = value; }
        }
        private string profileName;

        public string ProfileName
        {
            get { return profileName; }
            set { profileName = value; }
        }

        private int interval;

        public int Interval
        {
            get { return interval; }
            set { interval = value; }
        }

        private int startHour;

        public int StartHour
        {
            get { return startHour; }
            set { startHour = value; }
        }
        private int startMinute;

        public int StartMinute
        {
            get { return startMinute; }
            set { startMinute = value; }
        }
        private int endHour;

        public int EndHour
        {
            get { return endHour; }
            set { endHour = value; }
        }
        private int endMinute;

        public int EndMinute
        {
            get { return endMinute; }
            set { endMinute = value; }
        }

        private int MONDAY;

        public int MONDAY1
        {
            get { return MONDAY; }
            set { MONDAY = value; }
        }
        private int TUESDAY;

        public int TUESDAY1
        {
            get { return TUESDAY; }
            set { TUESDAY = value; }
        }
        private int WEDNESDAY;

        public int WEDNESDAY1
        {
            get { return WEDNESDAY; }
            set { WEDNESDAY = value; }
        }
        private int THURSDAY;

        public int THURSDAY1
        {
            get { return THURSDAY; }
            set { THURSDAY = value; }
        }
        private int FRIDAY;

        public int FRIDAY1
        {
            get { return FRIDAY; }
            set { FRIDAY = value; }
        }
        private int SATURDAY;

        public int SATURDAY1
        {
            get { return SATURDAY; }
            set { SATURDAY = value; }
        }
        private int SUNDAY;

        public int SUNDAY1
        {
            get { return SUNDAY; }
            set { SUNDAY = value; }
        }

        private int batterySaveMode;

        private int speed;

        public int Speed
        {
            get { return speed; }
            set { speed = value; }
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
        //	 * @return Returns the fRIDAY.
        //	 
        public virtual int getFRIDAY()
        {
            return FRIDAY1;
        }
        //    *
        //	 * @param friday The fRIDAY to set.
        //	 
        public virtual void setFRIDAY(int friday)
        {
            FRIDAY1 = friday;
        }
        //    *
        //	 * @return Returns the interval.
        //	 
        public virtual int getInterval()
        {
            return Interval;
        }
        //    *
        //	 * @param interval The interval to set.
        //	 
        public virtual void setInterval(int interval)
        {
            this.Interval = interval;
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
        //	 * @return Returns the mONDAY.
        //	 
        public virtual int getMONDAY()
        {
            return MONDAY1;
        }
        //    *
        //	 * @param monday The mONDAY to set.
        //	 
        public virtual void setMONDAY(int monday)
        {
            MONDAY1 = monday;
        }
        //    *
        //	 * @return Returns the profileName.
        //	 
        public virtual string getProfileName()
        {
            return ProfileName;
        }
        //    *
        //	 * @param profileName The profileName to set.
        //	 
        public virtual void setProfileName(string profileName)
        {
            this.ProfileName = profileName;
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
        //	 * @return Returns the sATURDAY.
        //	 
        public virtual int getSATURDAY()
        {
            return SATURDAY1;
        }
        //    *
        //	 * @param saturday The sATURDAY to set.
        //	 
        public virtual void setSATURDAY(int saturday)
        {
            SATURDAY1 = saturday;
        }
        //    *
        //	 * @return Returns the sUNDAY.
        //	 
        public virtual int getSUNDAY()
        {
            return SUNDAY1;
        }
        //    *
        //	 * @param sunday The sUNDAY to set.
        //	 
        public virtual void setSUNDAY(int sunday)
        {
            SUNDAY1 = sunday;
        }
        //    *
        //	 * @return Returns the tHURSDAY.
        //	 
        public virtual int getTHURSDAY()
        {
            return THURSDAY1;
        }
        //    *
        //	 * @param thursday The tHURSDAY to set.
        //	 
        public virtual void setTHURSDAY(int thursday)
        {
            THURSDAY1 = thursday;
        }
        //    *
        //	 * @return Returns the tUESDAY.
        //	 
        public virtual int getTUESDAY()
        {
            return TUESDAY1;
        }
        //    *
        //	 * @param tuesday The tUESDAY to set.
        //	 
        public virtual void setTUESDAY(int tuesday)
        {
            TUESDAY1 = tuesday;
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
        //	 * @return Returns the wEDNESDAY.
        //	 
        public virtual int getWEDNESDAY()
        {
            return WEDNESDAY1;
        }
        //    *
        //	 * @param wednesday The wEDNESDAY to set.
        //	 
        public virtual void setWEDNESDAY(int wednesday)
        {
            WEDNESDAY1 = wednesday;
        }
        //    *
        //	 * @return Returns the endHour.
        //	 
        public virtual int getEndHour()
        {
            return EndHour;
        }
        //    *
        //	 * @param endHour The endHour to set.
        //	 
        public virtual void setEndHour(int endHour)
        {
            this.EndHour = endHour;
        }
        //    *
        //	 * @return Returns the endMinute.
        //	 
        public virtual int getEndMinute()
        {
            return EndMinute;
        }
        //    *
        //	 * @param endMinute The endMinute to set.
        //	 
        public virtual void setEndMinute(int endMinute)
        {
            this.EndMinute = endMinute;
        }
        //    *
        //	 * @return Returns the startHour.
        //	 
        public virtual int getStartHour()
        {
            return StartHour;
        }
        //    *
        //	 * @param startHour The startHour to set.
        //	 
        public virtual void setStartHour(int startHour)
        {
            this.StartHour = startHour;
        }
        //    *
        //	 * @return Returns the startMinute.
        //	 
        public virtual int getStartMinute()
        {
            return StartMinute;
        }
        //    *
        //	 * @param startMinute The startMinute to set.
        //	 
        public virtual void setStartMinute(int startMinute)
        {
            this.StartMinute = startMinute;
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
            if (obj is SymmetryProfileData)
            {

                SymmetryProfileData other = (SymmetryProfileData)obj;

                if (this.Interval != other.getInterval())
                    isEqual = false;

                if (this.MONDAY1 != other.getMONDAY())
                    isEqual = false;
                if (this.TUESDAY1 != other.getTUESDAY())
                    isEqual = false;
                if (this.WEDNESDAY1 != other.getWEDNESDAY())
                    isEqual = false;
                if (this.THURSDAY1 != other.getTHURSDAY())
                    isEqual = false;
                if (this.FRIDAY1 != other.getFRIDAY())
                    isEqual = false;
                if (this.SATURDAY1 != other.getSATURDAY())
                    isEqual = false;
                if (this.SUNDAY1 != other.getSUNDAY())
                    isEqual = false;

                if (this.StartHour != other.getStartHour())
                    isEqual = false;
                if (this.StartMinute != other.getStartMinute())
                    isEqual = false;
                if (this.EndHour != other.getEndHour())
                    isEqual = false;
                if (this.EndMinute != other.getEndMinute())
                    isEqual = false;

                if (this.batterySaveMode != other.getBatterySaveMode())
                    isEqual = false;

                // do not add speed here !!!! it is not part of the device's profile, it is just a property for server-side speed violation notification handling

            }
            else
            {
                isEqual = false;
            }
            return isEqual;
        }
        //    *
        //	 * @return Returns the batterySaveMode.
        //	 
        public virtual int getBatterySaveMode()
        {
            return batterySaveMode;
        }
        //    *
        //	 * @param batterySaveMode The batterySaveMode to set.
        //	 
        public virtual void setBatterySaveMode(int batterySaveMode)
        {
            this.batterySaveMode = batterySaveMode;
        }

    }
}