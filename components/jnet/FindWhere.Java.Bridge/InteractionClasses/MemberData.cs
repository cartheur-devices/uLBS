using System;
using com.teleca.fleetonline.web.bean;
using System.Collections.Generic;
using System.Xml;
using JNetBridge.InteractionClasses;

namespace com.teleca.fleetonline.repository
{
    /// <summary> This class holds member data (user id, alias, status, msisdn, availability etc.)</summary>

    [Serializable]
    public class MemberData : RepositoryData
    {
        #region Properties
        virtual public int CurProfileNr
        {
            get
            {
                return curProfileNr;
            }

            set
            {
                this.curProfileNr = value;
            }

        }

        virtual public int NewProfileNr
        {
            get
            {
                return newProfileNr;
            }

            set
            {
                this.newProfileNr = value;
            }

        }
        virtual public System.String Alias
        {
            get
            {
                return alias;
            }

            set
            {
                this.alias = value;
            }

        }
        virtual public System.String Status
        {
            get
            {
                return status;
            }

            set
            {
                this.status = value;
            }

        }
        virtual public System.String Userid
        {
            get
            {
                return userid;
            }

            set
            {
                this.userid = value;
            }

        }
        virtual public bool AvailableWeekdays
        {
            get
            {
                return availableWeekdays;
            }

            set
            {
                this.availableWeekdays = value;
            }

        }
        virtual public bool AvailableWeekends
        {
            get
            {
                return availableWeekends;
            }

            set
            {
                this.availableWeekends = value;
            }

        }
        virtual public System.String Msisdn
        {
            get
            {
                return msisdn;
            }

            set
            {
                this.msisdn = value;
            }

        }
        virtual public int TimeEndH
        {
            get
            {
                return timeEndH;
            }

            set
            {
                this.timeEndH = value;
            }

        }
        virtual public int TimeEndM
        {
            get
            {
                return timeEndM;
            }

            set
            {
                this.timeEndM = value;
            }

        }
        virtual public int TimeStartH
        {
            get
            {
                return timeStartH;
            }

            set
            {
                this.timeStartH = value;
            }

        }
        virtual public int TimeStartM
        {
            get
            {
                return timeStartM;
            }

            set
            {
                this.timeStartM = value;
            }

        }
        virtual public bool Trackable
        {
            get
            {
                return trackable;
            }

            set
            {
                this.trackable = value;
            }

        }
        virtual public int OperatorId
        {
            get
            {
                return operatorId;
            }

            set
            {
                this.operatorId = value;
            }

        }
        virtual public int UserType
        {
            get
            {
                return userType;
            }

            set
            {
                this.userType = value;
            }

        }
        virtual public int OnDemand
        {
            get
            {
                return onDemand;
            }

            set
            {
                this.onDemand = value;
            }

        }
        virtual public int GetMisPos
        {
            get
            {
                return getMisPos;
            }

            set
            {
                this.getMisPos = value;
            }

        }
        virtual public int TimeUTCEndH
        {
            get
            {
                return timeUTCEndH;
            }

            set
            {
                this.timeUTCEndH = value;
            }

        }
        virtual public int TimeUTCEndM
        {
            get
            {
                return timeUTCEndM;
            }

            set
            {
                this.timeUTCEndM = value;
            }

        }
        virtual public int TimeUTCStartH
        {
            get
            {
                return timeUTCStartH;
            }

            set
            {
                this.timeUTCStartH = value;
            }

        }
        virtual public int TimeUTCStartM
        {
            get
            {
                return timeUTCStartM;
            }

            set
            {
                this.timeUTCStartM = value;
            }

        }
        virtual public System.String FoId
        {
            get
            {
                return foId;
            }

            set
            {
                this.foId = value;
            }

        }
        virtual public int IconId
        {
            get
            {
                return iconId;
            }

            set
            {
                this.iconId = value;
            }

        }
        virtual public bool GeocodingAvailable
        {
            get
            {
                return geocodingAvailable;
            }

            set
            {
                this.geocodingAvailable = value;
            }

        }
        virtual public bool Blocked
        {
            get
            {
                return blocked;
            }

            set
            {
                this.blocked = value;
            }

        }
        #endregion

        #region Fields
        private System.String foId;
        private System.String userid;
        private System.String alias;
        private System.String status;
        private bool blocked;

        private System.String msisdn;
        private bool availableWeekdays;
        private bool availableWeekends;
        private int timeStartH;
        private int timeStartM;
        private int timeEndH;
        private int timeEndM;
        private int timeUTCStartH;
        private int timeUTCStartM;
        private int timeUTCEndH;
        private int timeUTCEndM;
        private bool trackable;
        private int userType;
        private int operatorId = 9999;

        private int newProfileNr;
        private int curProfileNr;
        private int onDemand = 0;
        private int getMisPos = 1;
        private int iconId; // used for Gemini & Symmetry only
        private bool geocodingAvailable;

        private string txt__sto__on = null;

        public string Txt__sto__on
        {
            get { return txt__sto__on; }
            set { txt__sto__on = value; }
        }
        private string txt__srm__on = null;

        public string Txt__srm__on
        {
            get { return txt__srm__on; }
            set { txt__srm__on = value; }
        }
        private string txt__srt__on = null;

        public string Txt__srt__on
        {
            get { return txt__srt__on; }
            set { txt__srt__on = value; }
        }
        private string txt__mo__on = null;

        public string Txt__mo__on
        {
            get { return txt__mo__on; }
            set { txt__mo__on = value; }
        }
        private string txt__ap__on = null;

        public string Txt__ap__on
        {
            get { return txt__ap__on; }
            set { txt__ap__on = value; }
        }
        private string txt__mp__on = null;

        public string Txt__mp__on
        {
            get { return txt__mp__on; }
            set { txt__mp__on = value; }
        }
        private string txt__hpa__on = null;

        public string Txt__hpa__on
        {
            get { return txt__hpa__on; }
            set { txt__hpa__on = value; }
        }
        private string txt__mpa__on = null;

        public string Txt__mpa__on
        {
            get { return txt__mpa__on; }
            set { txt__mpa__on = value; }
        }
        private string txt__lpa__on = null;

        public string Txt__lpa__on
        {
            get { return txt__lpa__on; }
            set { txt__lpa__on = value; }
        }
        private string txt__lpag__on = null;

        public string Txt__lpag__on
        {
            get { return txt__lpag__on; }
            set { txt__lpag__on = value; }
        }
        private string txt__rtm__on = null;

        public string Txt__rtm__on
        {
            get { return txt__rtm__on; }
            set { txt__rtm__on = value; }
        }
        private string txt__motionrtm__on = null;

        public string Txt__motionrtm__on
        {
            get { return txt__motionrtm__on; }
            set { txt__motionrtm__on = value; }
        }
        private string txt__sv__on = null;

        public string Txt__sv__on
        {
            get { return txt__sv__on; }
            set { txt__sv__on = value; }
        }

        private string txt__sv__on__desc = null;

        public string Txt__sv__on__desc
        {
            get { return txt__sv__on__desc; }
            set { txt__sv__on__desc = value; }
        }

        private string txt__reqsrm__srt = null;

        public string Txt__reqsrm__srt
        {
            get { return txt__reqsrm__srt; }
            set { txt__reqsrm__srt = value; }
        }

        

        #endregion

        public static List<MemberData> DeSerialize(string sourceXML)
        {
            XmlDocument workingXmlDoc = new XmlDocument();
            workingXmlDoc.LoadXml(sourceXML);

            XmlNodeList nodeList_root = workingXmlDoc.DocumentElement.SelectNodes("com.teleca.fleetonline.repository.MemberData");

            List<MemberData> list = new List<MemberData>();

            foreach (XmlNode node in nodeList_root)
            {
                // Dit zijn de xml's met memberdata...
                MemberData md = new MemberData();
                Utils.FillProperties(md, node);
                list.Add(md);

            }


            return list;
        }
    }
}