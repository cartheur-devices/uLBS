namespace com.teleca.fleetonline.repository
{
    [System.Serializable]
    public class WIConfigDBObject
    {
        public const int UNKNOWN = -1;

        private string fmid;

        private string txt__Curr__spd;

        private int curr__MaxSms;

        public int Curr__MaxSms
        {
            get { return curr__MaxSms; }
            set { curr__MaxSms = value; }
        }

        private int req__MaxSms;

        public int Req__MaxSms
        {
            get { return req__MaxSms; }
            set { req__MaxSms = value; }
        }

        public string Txt__Curr__spd
        {
            get { return txt__Curr__spd; }
            set { txt__Curr__spd = value; }
        }

        private string txt__Curr__spd__desc;

        public string Txt__Curr__spd__desc
        {
            get { return txt__Curr__spd__desc; }
            set { txt__Curr__spd__desc = value; }
        }


        private string txt__Req__spd;

        public string Txt__Req__spd
        {
            get { return txt__Req__spd; }
            set { txt__Req__spd = value; }
        }

        private string txt__Req__spd__desc;

        public string Txt__Req__spd__desc
        {
            get { return txt__Req__spd__desc; }
            set { txt__Req__spd__desc = value; }
        }
        
        private int curr__IntInt, curr__NatInt, curr__NotMov, curr__Speed, req__IntInt = -1, req__NatInt = -1, req__NotMov = -1, req__Speed = -1, locationExchange;

        public int Curr__NotMov
        {
            get { return curr__NotMov; }
            set { curr__NotMov = value; }
        }

        public int Curr__Speed
        {
            get { return curr__Speed; }
            set { curr__Speed = value; }
        }

        public int Req__IntInt
        {
            get { return req__IntInt; }
            set { req__IntInt = value; }
        }

        public int Req__NatInt
        {
            get { return req__NatInt; }
            set { req__NatInt = value; }
        }

        public int Req__NotMov
        {
            get { return req__NotMov; }
            set { req__NotMov = value; }
        }

        public int Req__Speed
        {
            get { return req__Speed; }
            set { req__Speed = value; }
        }

        public int Curr__NatInt
        {
            get { return curr__NatInt; }
            set { curr__NatInt = value; }
        }

        public int Curr__IntInt
        {
            get { return curr__IntInt; }
            set { curr__IntInt = value; }
        }

        public int LocationExchange
        {
            get { return locationExchange; }
            set { locationExchange = value; }
        }

        public virtual string getFmid()
        {
            return fmid;
        }

        public virtual void setFmid(string fmid)
        {
            this.fmid = fmid;
        }

        public virtual int getCurr_IntInt()
        {
            return Curr__IntInt;
        }

        public virtual void setCurr__IntInt(int curr__IntInt)
        {
            this.Curr__IntInt = curr__IntInt;
        }

        public virtual int getCurr__NatInt()
        {
            return curr__NatInt;
        }

        public virtual void setCurr_NatInt(int curr__NatInt)
        {
            this.curr__NatInt = curr__NatInt;
        }

        public virtual int getCurr__NotMov()
        {
            return Curr__NotMov;
        }

        public virtual void setCurr_NotMov(int curr__NotMov)
        {
            this.Curr__NotMov = curr__NotMov;
        }

        public virtual int getReq__IntInt()
        {
            return Req__IntInt;
        }

        public virtual void setReq__IntInt(int req__IntInt)
        {
            this.Req__IntInt = req__IntInt;
        }

        public virtual int getReq__NatInt()
        {
            return Req__NatInt;
        }

        public virtual void setReq__NatInt(int req__NatInt)
        {
            this.Req__NatInt = req__NatInt;
        }

        public virtual int getReq__NotMov()
        {
            return Req__NotMov;
        }

        public virtual void setReq__NotMov(int req__NotMov)
        {
            this.Req__NotMov = req__NotMov;
        }

        public virtual int getCurr__Speed()
        {
            return Curr__Speed;
        }

        public virtual void setCurr_Speed(int curr__Speed)
        {
            this.Curr__Speed = curr__Speed;
        }

        public virtual int getReq__Speed()
        {
            return req__Speed;
        }

        public virtual void setReq_Speed(int req__Speed)
        {
            this.req__Speed = req__Speed;
        }

        public virtual int getLocationExchange()
        {
            return locationExchange;
        }

        public virtual void setLocationExchange(int locationExchange)
        {
            this.locationExchange = locationExchange;
        }
    }
}