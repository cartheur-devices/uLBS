namespace com.teleca.fleetonline.repository
{
    [System.Serializable]
    public class ProfileData
    {

        private int id, foId;

        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public int FoId
        {
            get { return foId; }
            set { foId = value; }
        }
        private string startDay, endDay, startTime, endTime, name;

        public string StartDay
        {
            get { return startDay; }
            set { startDay = value; }
        }

        public string EndDay
        {
            get { return endDay; }
            set { endDay = value; }
        }

        public string StartTime
        {
            get { return startTime; }
            set { startTime = value; }
        }

        public string EndTime
        {
            get { return endTime; }
            set { endTime = value; }
        }

        public virtual string getEndDay()
        {
            return EndDay;
        }
        public virtual void setEndDay(string endDay)
        {
            this.EndDay = endDay;
        }
        public virtual string getEndTime()
        {
            return EndTime;
        }
        public virtual void setEndTime(string endTime)
        {
            this.EndTime = endTime;
        }
        public virtual int getFoId()
        {
            return FoId;
        }
        public virtual void setFoId(int foId)
        {
            this.FoId = foId;
        }
        public virtual int getId()
        {
            return Id;
        }
        public virtual void setId(int id)
        {
            this.Id = id;
        }
        public virtual string Name
        {
            get
            {
                return name;
            }
            set
            {
                this.name = value;
            }
        }
        public virtual string getStartDay()
        {
            return StartDay;
        }
        public virtual void setStartDay(string startDay)
        {
            this.StartDay = startDay;
        }
        public virtual string getStartTime()
        {
            return StartTime;
        }
        public virtual void setStartTime(string startTime)
        {
            this.StartTime = startTime;
        }



    }
}