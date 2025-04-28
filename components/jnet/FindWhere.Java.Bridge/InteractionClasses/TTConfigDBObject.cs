namespace com.teleca.fleetonline.repository
{
    [System.Serializable]
    public class TTConfigDBObject
    {
        private const int UNKNOWN = -1;

        public int UNKNOWN1
        {
            get { return UNKNOWN; }
        } 


        private string fmid, curr__sto, curr__srm, curr__srt, curr__mo, curr__hpa, curr__mpa, curr__lpa, curr__ap, curr__mp, curr__lpa__geofence, curr__lpag__radius, curr__rtm, curr__motionrtm, curr__sv, req__sto, req__srm, req__srt, req__mo, req__hpa, req__mpa, req__lpa, req__ap, req__mp, req__lpa__geofence, req__lpag__radius, req__rtm, req__motionrtm, req__sv, lpagFenceNr;

        public string Fmid
        {
            get { return fmid; }
            set { fmid = value; }
        }

        public string Curr__sto
        {
            get { return curr__sto; }
            set { curr__sto = value; }
        }

        public string Curr__srm
        {
            get { return curr__srm; }
            set { curr__srm = value; }
        }

        public string Curr__srt
        {
            get { return curr__srt; }
            set { curr__srt = value; }
        }

        public string Curr__mo
        {
            get { return curr__mo; }
            set { curr__mo = value; }
        }

        public string Curr__hpa
        {
            get { return curr__hpa; }
            set { curr__hpa = value; }
        }

        public string Curr__mpa
        {
            get { return curr__mpa; }
            set { curr__mpa = value; }
        }

        public string Curr__lpa
        {
            get { return curr__lpa; }
            set { curr__lpa = value; }
        }

        public string Curr__ap
        {
            get { return curr__ap; }
            set { curr__ap = value; }
        }

        public string Curr__mp
        {
            get { return curr__mp; }
            set { curr__mp = value; }
        }

        public string Curr__lpa__geofence
        {
            get { return curr__lpa__geofence; }
            set { curr__lpa__geofence = value; }
        }

        public string Curr__lpag__radius
        {
            get { return curr__lpag__radius; }
            set { curr__lpag__radius = value; }
        }

        public string Curr__rtm
        {
            get { return curr__rtm; }
            set { curr__rtm = value; }
        }

        public string Curr__motionrtm
        {
            get { return curr__motionrtm; }
            set { curr__motionrtm = value; }
        }

        public string Curr__sv
        {
            get { return curr__sv; }
            set { curr__sv = value; }
        }

        public string Req__sto
        {
            get { return req__sto; }
            set { req__sto = value; }
        }

        public string Req__srm
        {
            get { return req__srm; }
            set { req__srm = value; }
        }

        public string Req__srt
        {
            get { return req__srt; }
            set { req__srt = value; }
        }

        public string Req__mo
        {
            get { return req__mo; }
            set { req__mo = value; }
        }

        public string Req__hpa
        {
            get { return req__hpa; }
            set { req__hpa = value; }
        }

        public string Req__mpa
        {
            get { return req__mpa; }
            set { req__mpa = value; }
        }

        public string Req__lpa
        {
            get { return req__lpa; }
            set { req__lpa = value; }
        }

        public string Req__ap
        {
            get { return req__ap; }
            set { req__ap = value; }
        }

        public string Req__mp
        {
            get { return req__mp; }
            set { req__mp = value; }
        }

        public string Req__lpa__geofence
        {
            get { return req__lpa__geofence; }
            set { req__lpa__geofence = value; }
        }

        public string Req__lpag__radius
        {
            get { return req__lpag__radius; }
            set { req__lpag__radius = value; }
        }

        public string Req__rtm
        {
            get { return req__rtm; }
            set { req__rtm = value; }
        }

        public string Req__motionrtm
        {
            get { return req__motionrtm; }
            set { req__motionrtm = value; }
        }

        public string Req__sv
        {
            get { return req__sv; }
            set { req__sv = value; }
        }

        public string LpagFenceNr
        {
            get { return lpagFenceNr; }
            set { lpagFenceNr = value; }
        }
        private int motionDaily = UNKNOWN, lpaDaily = UNKNOWN, motionTime = UNKNOWN, lpaTime = UNKNOWN;

        public int MotionDaily
        {
            get { return motionDaily; }
            set { motionDaily = value; }
        }

        public int LpaDaily
        {
            get { return lpaDaily; }
            set { lpaDaily = value; }
        }

        public int MotionTime
        {
            get { return motionTime; }
            set { motionTime = value; }
        }

        public int LpaTime
        {
            get { return lpaTime; }
            set { lpaTime = value; }
        }

        public virtual string getCurr__ap()
        {
            return Curr__ap;
        }

        public virtual void setCurr__ap(string curr__ap)
        {
            this.Curr__ap = curr__ap;
        }

        public virtual string getCurr__hpa()
        {
            return Curr__hpa;
        }

        public virtual void setCurr__hpa(string curr__hpa)
        {
            this.Curr__hpa = curr__hpa;
        }

        public virtual string getCurr__lpa()
        {
            return Curr__lpa;
        }

        public virtual void setCurr__lpa(string curr__lpa)
        {
            this.Curr__lpa = curr__lpa;
        }

        public virtual string getCurr__mo()
        {
            return Curr__mo;
        }

        public virtual void setCurr__mo(string curr__mo)
        {
            this.Curr__mo = curr__mo;
        }

        public virtual string getCurr__mp()
        {
            return Curr__mp;
        }

        public virtual void setCurr__mp(string curr__mp)
        {
            this.Curr__mp = curr__mp;
        }

        public virtual string getCurr__mpa()
        {
            return Curr__mpa;
        }

        public virtual void setCurr__mpa(string curr__mpa)
        {
            this.Curr__mpa = curr__mpa;
        }

        public virtual string getCurr__srm()
        {
            return Curr__srm;
        }

        public virtual void setCurr__srm(string curr__srm)
        {
            this.Curr__srm = curr__srm;
        }

        public virtual string getCurr__srt()
        {
            return Curr__srt;
        }

        public virtual void setCurr__srt(string curr__srt)
        {
            this.Curr__srt = curr__srt;
        }

        public virtual string getCurr__sto()
        {
            return Curr__sto;
        }

        public virtual void setCurr__sto(string curr__sto)
        {
            this.Curr__sto = curr__sto;
        }

        public virtual string getFmid()
        {
            return Fmid;
        }

        public virtual void setFmid(string fmid)
        {
            this.Fmid = fmid;
        }

        public virtual string getReq__ap()
        {
            return Req__ap;
        }

        public virtual void setReq__ap(string req__ap)
        {
            this.Req__ap = req__ap;
        }

        public virtual string getReq__hpa()
        {
            return Req__hpa;
        }

        public virtual void setReq__hpa(string req__hpa)
        {
            this.Req__hpa = req__hpa;
        }

        public virtual string getReq__lpa()
        {
            return Req__lpa;
        }

        public virtual void setReq__lpa(string req__lpa)
        {
            this.Req__lpa = req__lpa;
        }

        public virtual string getReq__mo()
        {
            return Req__mo;
        }

        public virtual void setReq__mo(string req__mo)
        {
            this.Req__mo = req__mo;
        }

        public virtual string getReq__mp()
        {
            return Req__mp;
        }

        public virtual void setReq__mp(string req__mp)
        {
            this.Req__mp = req__mp;
        }

        public virtual string getReq__mpa()
        {
            return Req__mpa;
        }

        public virtual void setReq__mpa(string req__mpa)
        {
            this.Req__mpa = req__mpa;
        }

        public virtual string getReq__srm()
        {
            return Req__srm;
        }

        public virtual void setReq__srm(string req__srm)
        {
            this.Req__srm = req__srm;
        }

        public virtual string getReq__srt()
        {
            return Req__srt;
        }

        public virtual void setReq__srt(string req__srt)
        {
            this.Req__srt = req__srt;
        }

        public virtual string getReq__sto()
        {
            return Req__sto;
        }

        public virtual void setReq__sto(string req__sto)
        {
            this.Req__sto = req__sto;
        }

        public virtual string getCurr__lpa__geofence()
        {
            return Curr__lpa__geofence;
        }

        public virtual void setCurr__lpa__geofence(string curr__lpa__geofence)
        {
            this.Curr__lpa__geofence = curr__lpa__geofence;
        }

        public virtual string getReq__lpa__geofence()
        {
            return Req__lpa__geofence;
        }

        public virtual void setReq__lpa__geofence(string req__lpa__geofence)
        {
            this.Req__lpa__geofence = req__lpa__geofence;
        }

        public virtual string getCurr__lpag__radius()
        {
            return Curr__lpag__radius;
        }

        public virtual void setCurr__lpag__radius(string curr__lpag__radius)
        {
            this.Curr__lpag__radius = curr__lpag__radius;
        }

        public virtual string getReq__lpag__radius()
        {
            return Req__lpag__radius;
        }

        public virtual void setReq__lpag__radius(string req__lpag__radius)
        {
            this.Req__lpag__radius = req__lpag__radius;
        }

        public virtual string getLpagFenceNr()
        {
            return LpagFenceNr;
        }

        public virtual void setLpagFenceNr(string lpagFenceNr)
        {
            this.LpagFenceNr = lpagFenceNr;
        }

        public virtual string getCurr__rtm()
        {
            return Curr__rtm;
        }

        public virtual void setCurr__rtm(string curr__rtm)
        {
            this.Curr__rtm = curr__rtm;
        }

        public virtual string getCurr__motionrtm()
        {
            return Curr__motionrtm;
        }

        public virtual void setCurr__motionrtm(string curr__motionrtm)
        {
            this.Curr__motionrtm = curr__motionrtm;
        }

        public virtual string getReq__rtm()
        {
            return Req__rtm;
        }

        public virtual void setReq__rtm(string req__rtm)
        {
            this.Req__rtm = req__rtm;
        }

        public virtual string getReq__motionrtm()
        {
            return Req__motionrtm;
        }

        public virtual void setReq__motionrtm(string req__motionrtm)
        {
            this.Req__motionrtm = req__motionrtm;
        }

        public virtual string getCurr__sv()
        {
            return Curr__sv;
        }

        public virtual void setCurr__sv(string curr__sv)
        {
            this.Curr__sv = curr__sv;
        }

        public virtual string getReq__sv()
        {
            return Req__sv;
        }

        public virtual void setReq__sv(string req__sv)
        {
            this.Req__sv = req__sv;
        }

        public virtual int getLpaDaily()
        {
            return LpaDaily;
        }

        public virtual void setLpaDaily(int lpaDaily)
        {
            this.LpaDaily = lpaDaily;
        }

        public virtual int getMotionDaily()
        {
            return MotionDaily;
        }

        public virtual void setMotionDaily(int motionDaily)
        {
            this.MotionDaily = motionDaily;
        }

        public virtual int getLpaTime()
        {
            return LpaTime;
        }

        public virtual void setLpaTime(int lpaTime)
        {
            this.LpaTime = lpaTime;
        }

        public virtual int getMotionTime()
        {
            return MotionTime;
        }

        public virtual void setMotionTime(int motionTime)
        {
            this.MotionTime = motionTime;
        }


    }
}