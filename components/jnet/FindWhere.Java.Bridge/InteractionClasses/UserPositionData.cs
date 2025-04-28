using com.teleca.fleetonline.web.bean;
using System;
using System.Xml;
using JNetBridge.InteractionClasses;
namespace com.teleca.fleetonline.repository
{


    //*************************************************************************
    //
    // $Source: D:/Data/cvs/FOL/src/java/ejb_module/src/com/teleca/fleetonline/repository/UserPositionData.java,v $
    // $Revision: 1.10 $
    // $Date: 2007/11/12 15:40:17 $
    //
    // Copyright(c) Teydo BV, Bilthoven, all rights reserved worldwide.
    //
    //************************************************************************* 

    // *
    // * This class holds User Position data (position in WGS84 and in NAD, timestamp and FM status)
    // 
    [System.Serializable]
    public class UserPositionData : RepositoryData
    {
        private const long serialVersionUID = 1L;

        public long SerialVersionUID
        {
            get { return serialVersionUID; }
        } 


        private double x;

        public double X
        {
            get { return x; }
            set { x = value; }
        }
        private double y;

        public double Y
        {
            get { return y; }
            set { y = value; }
        }
        private double nadX;

        public double NadX
        {
            get { return nadX; }
            set { nadX = value; }
        }
        private double nadY;

        public double NadY
        {
            get { return nadY; }
            set { nadY = value; }
        }

        private DateTime requestTime;

        public DateTime RequestTime
        {
            get { return requestTime; }
            set { requestTime = value; }
        }

        private string strRequestTime;

        public string StrRequestTime
        {
            get { return strRequestTime; }
            set { strRequestTime = value; }
        }

        private int fmStatusId;

        public int FmStatusId
        {
            get { return fmStatusId; }
            set { fmStatusId = value; }
        }
        private string uid;

        public string Uid
        {
            get { return uid; }
            set { uid = value; }
        }

        private string label;

        public string Label
        {
            get { return label; }
            set { label = value; }
        }
        private int accuracy; // Index for a accuracy icon (is NOT the real accuracy value)!

        public int Accuracy
        {
            get { return accuracy; }
            set { accuracy = value; }
        }
        private int age;

        public int Age
        {
            get { return age; }
            set { age = value; }
        }
        private int iconId;

        public int IconId
        {
            get { return iconId; }
            set { iconId = value; }
        }
        private bool livePos = false;

        public bool LivePos
        {
            get { return livePos; }
            set { livePos = value; }
        }
        private string postCode;

        public string PostCode
        {
            get { return postCode; }
            set { postCode = value; }
        }
        private string town;

        public string Town
        {
            get { return town; }
            set { town = value; }
        }
        private float deviation = -1; // This is the value from the attribute

        public float Deviation
        {
            get { return deviation; }
            set { deviation = value; }
        }
        // 'accuracy' (table LBSREQUEST)

        private int fixStatus = -1; // value representing status of the fix; e.g. historic vs current

        public int FixStatus
        {
            get { return fixStatus; }
            set { fixStatus = value; }
        }
        private DateTime fixTime; // Time of the GPS fix

        public DateTime FixTime
        {
            get { return fixTime; }
            set { fixTime = value; }
        }
        private int reportCause = -1; // constant that represent the cause of the location

        public int ReportCause
        {
            get { return reportCause; }
            set { reportCause = value; }
        }

        private double direction = -1;

        public double Direction
        {
            get { return direction; }
            set { direction = value; }
        }

        private string alertInfo = "";

        public string AlertInfo
        {
            get { return alertInfo; }
            set { alertInfo = value; }
        }
        private string requester;

        public string Requester
        {
            get { return requester; }
            set { requester = value; }
        }

        private double radius;

        public double Radius
        {
            get { return radius; }
            set { radius = value; }
        }

        private int lbsId;

        public int LbsId
        {
            get { return lbsId; }
            set { lbsId = value; }
        }

        private double  speed;

        public double Speed
        {
            get { return speed; }
            set { speed = value; }
        }


        //    *
        //	 * Returns the uid.
        //	 * @return String
        //	 
        public virtual string getUid()
        {
            return Uid;
        }

        //    *
        //	 * Sets the uid.
        //	 * @param uid The uid to set
        //	 
        public virtual void setUid(string uid)
        {
            this.Uid = uid;
        }

        //    *
        //	 * Returns the x.
        //	 * @return double
        //	 
        public virtual double getX()
        {
            return X;
        }

        //    *
        //	 * Returns the y.
        //	 * @return double
        //	 
        public virtual double getY()
        {
            return Y;
        }

        //    *
        //	 * Sets the x.
        //	 * @param x The x to set
        //	 
        public virtual void setX(double x)
        {
            this.X = x;
        }

        //    *
        //	 * Sets the y.
        //	 * @param y The y to set
        //	 
        public virtual void setY(double y)
        {
            this.Y = y;
        }

        //    *
        //	 * Returns the nadX.
        //	 * @return double
        //	 
        public virtual double getNadX()
        {
            return NadX;
        }

        //    *
        //	 * Returns the nadY.
        //	 * @return double
        //	 
        public virtual double getNadY()
        {
            return NadY;
        }

        //    *
        //	 * Sets the nadX.
        //	 * @param nadX The nadX to set
        //	 
        public virtual void setNadX(double nadX)
        {
            this.NadX = nadX;
        }

        //    *
        //	 * Sets the nadY.
        //	 * @param nadY The nadY to set
        //	 
        public virtual void setNadY(double nadY)
        {
            this.NadY = nadY;
        }

        //    *
        //	 * Returns the fmStatusId.
        //	 * @return int
        //	 
        public virtual int getFmStatusId()
        {
            return FmStatusId;
        }

        //    *
        //	 * Returns the requestTime.
        //	 * @return java.util.Date
        //	 
        public virtual DateTime getRequestTime()
        {
            return RequestTime;
        }

        //    *
        //	 * Sets the fmStatusId.
        //	 * @param fmStatusId The fmStatusId to set
        //	 
        public virtual void setFmStatusId(int fmStatusId)
        {
            this.FmStatusId = fmStatusId;
        }

        //    *
        //	 * Sets the requestTime.
        //	 * @param requestTime The requestTime to set
        //	 
        public virtual void setRequestTime(DateTime requestTime)
        {
            this.RequestTime = requestTime;
        }

        //    *
        //	 * @return Returns the accuracy.
        //	 
        public virtual int getAccuracy()
        {
            return Accuracy;
        }

        //    *
        //	 * @param accuracy The accuracy to set.
        //	 
        public virtual void setAccuracy(int accuracy)
        {
            this.Accuracy = accuracy;
        }

        //    *
        //	 * @return Returns the age.
        //	 
        public virtual int getAge()
        {
            return Age;
        }

        //    *
        //	 * @param age The age to set.
        //	 
        public virtual void setAge(int age)
        {
            this.Age = age;
        }

        //    *
        //	 * @return Returns the label.
        //	 
        public virtual string getLabel()
        {
            return Label;
        }

        //    *
        //	 * @param label The label to set.
        //	 
        public virtual void setLabel(string label)
        {
            this.Label = label;
        }

        //    *
        //	 * @return Returns the iconId.
        //	 
        public virtual int getIconId()
        {
            return IconId;
        }
        //    *
        //	 * @param iconId The iconId to set.
        //	 
        public virtual void setIconId(int iconId)
        {
            this.IconId = iconId;
        }
        //    *
        //	 * @return Returns the livePos.
        //	 
        public virtual bool isLivePos()
        {
            return LivePos;
        }
        //    *
        //	 * @param livePos The livePos to set.
        //	 
        public virtual void setLivePos(bool livePos)
        {
            this.LivePos = livePos;
        }
        //    *
        //	 * @return Returns the postCode.
        //	 
        public virtual string getPostCode()
        {
            return PostCode;
        }
        //    *
        //	 * @param postCode The postCode to set.
        //	 
        public virtual void setPostCode(string postCode)
        {
            this.PostCode = postCode;
        }
        //    *
        //	 * @return Returns the town.
        //	 
        public virtual string getTown()
        {
            return Town;
        }
        //    *
        //	 * @param town The town to set.
        //	 
        public virtual void setTown(string town)
        {
            this.Town = town;
        }
        //    *
        //	 * This is the value of the attribute 'accuracy' from the table 'lbsrequests'.
        //	 * The field 'accuracy' in this (UserPositionData) object is used to display
        //	 * an accuracy icon and does NOT contain the true accuracy value! 
        //	 * @return Returns the deviation.
        //	 
        public virtual float getDeviation()
        {
            return Deviation;
        }
        //    *
        //	 * @param deviation The deviation to set.
        //	 
        public virtual void setDeviation(float deviation)
        {
            this.Deviation = deviation;
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
        //	 * @return Returns the fixTime.
        //	 
        public virtual DateTime getFixTime()
        {
            return FixTime;
        }

        //    *
        //	 * @param fixTime The fixTime to set.
        //	 
        public virtual void setFixTime(DateTime fixTime)
        {
            this.FixTime = fixTime;
        }

        //    *
        //	 * @return Returns the reportCause.
        //	 
        public virtual int getReportCause()
        {
            return ReportCause;
        }

        //    *
        //	 * @param reportCause The reportCause to set.
        //	 
        public virtual void setReportCause(int reportCause)
        {
            this.ReportCause = reportCause;
        }

        public virtual int getLbsId()
        {
            return LbsId;
        }

        public virtual void setLbsId(int lbsId)
        {
            this.LbsId = lbsId;
        }

        //    *
        //	 * @return the direction
        //	 
        public virtual double getDirection()
        {
            return Direction;
        }

        //    *
        //	 * @param direction the direction to set
        //	 
        public virtual void setDirection(double direction)
        {
            this.Direction = direction;
        }

        public virtual string getAlertInfo()
        {
            return AlertInfo;
        }

        public virtual void setAlertInfo(string alertInfo)
        {
            this.AlertInfo = alertInfo;
        }

        public virtual string getRequester()
        {
            return Requester;
        }

        public virtual void setRequester(string requester)
        {
            this.Requester = requester;
        }

        public virtual double getRadius()
        {
            return Radius;
        }

        public virtual void setRadius(double radius)
        {
            this.Radius = radius;
        }

        public virtual double getSpeed()
        {
            return Speed;
        }

        public virtual void setSpeed(double  speed)
        {
            this.Speed = speed;
        }

        internal static UserPositionData[] ArrayFromNode(XmlNode value)
        {
            XmlNodeList userPositionNodes = value.SelectNodes("com.teleca.fleetonline.repository.UserPositionData");
            UserPositionData[] usd = new UserPositionData[userPositionNodes.Count];
            for (int i = 0; i < userPositionNodes.Count; i++)
            {
                usd[i] = new UserPositionData();
                Utils.FillProperties(usd[i], userPositionNodes[i]);
            }
            return usd;
        }
    }
}