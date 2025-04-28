using System;
using System.Globalization;

namespace com.teleca.fleetonline.businessmanager
{


    //using GpsTime = com.teydo.fleetonline.utils.GpsTime;


    [System.Serializable]
    public class TTMessageData
    {

        //    *
        //	 * 
        //	 
        private const long serialVersionUID = 1L;
        /// <summary>
        /// 
        /// </summary>
        public long SerialVersionUID
        {
            get { return serialVersionUID; }
        }


        private readonly int MIN_TT_MESSAGE_LENGTH = 26;
        /// <summary>
        /// 
        /// </summary>
        public int MIN_TT_MESSAGE_LENGTH1
        {
            get { return MIN_TT_MESSAGE_LENGTH; }
        }


        private char type;
        /// <summary>
        /// 
        /// </summary>
        public char Type
        {
            get { return type; }
            set { type = value; }
        }

        private int triggerType = -1;
        /// <summary>
        /// 
        /// </summary>
        public int TriggerType
        {
            get { return triggerType; }
            set { triggerType = value; }
        }

        private int batteryLevel;
        /// <summary>
        /// 
        /// </summary>
        public int BatteryLevel
        {
            get { return batteryLevel; }
            set { batteryLevel = value; }
        }
        private bool batteryStatusKnown = false;
        /// <summary>
        /// 
        /// </summary>
        public bool BatteryStatusKnown
        {
            get { return batteryStatusKnown; }
            set { batteryStatusKnown = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        private Calendar fixTime = null;
        /// <summary>
        /// 
        /// </summary>
        public Calendar FixTime
        {
            get { return fixTime; }
            set { fixTime = value; }
        }
        private int gpsStatus;
        /// <summary>
        /// 
        /// </summary>
        public int GpsStatus
        {
            get { return gpsStatus; }
            set { gpsStatus = value; }
        }
        private int gsmStatus;
        /// <summary>
        /// 
        /// </summary>
        public int GsmStatus
        {
            get { return gsmStatus; }
            set { gsmStatus = value; }
        }
        private int positionAge;
        /// <summary>
        /// 
        /// </summary>
        public int PositionAge
        {
            get { return positionAge; }
            set { positionAge = value; }
        }
        private int hpaStatus;
        /// <summary>
        /// 
        /// </summary>
        public int HpaStatus
        {
            get { return hpaStatus; }
            set { hpaStatus = value; }
        }
        private int mpaStatus;
        /// <summary>
        /// 
        /// </summary>
        public int MpaStatus
        {
            get { return mpaStatus; }
            set { mpaStatus = value; }
        }
        private int lpaStatus;
        /// <summary>
        /// 
        /// </summary>
        public int LpaStatus
        {
            get { return lpaStatus; }
            set { lpaStatus = value; }
        }
        private int externalPower;
        /// <summary>
        /// 
        /// </summary>
        public int ExternalPower
        {
            get { return externalPower; }
            set { externalPower = value; }
        }
        private char batchanged;
        /// <summary>
        /// 
        /// </summary>
        public char Batchanged
        {
            get { return batchanged; }
            set { batchanged = value; }
        }
        private int speed = -1;

        public int Speed
        {
            get { return speed; }
            set { speed = value; }
        }
        private int direction;
        /// <summary>
        /// 
        /// </summary>
        public int Direction
        {
            get { return direction; }
            set { direction = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        public TTMessageData()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <param name="triggerType"></param>
        /// <param name="batteryLevel"></param>
        /// <param name="fixTime"></param>
        /// <param name="gpsStatus"></param>
        /// <param name="gsmStatus"></param>
        /// <param name="positionAge"></param>
        /// <param name="hpaStatus"></param>
        /// <param name="mpaStatus"></param>
        /// <param name="lpaStatus"></param>
        /// <param name="externalPower"></param>
        /// <param name="batChanged"></param>
        /// <param name="speed"></param>
        /// <param name="direction"></param>
        public TTMessageData(char type, int triggerType, int batteryLevel, Calendar fixTime, int gpsStatus, int gsmStatus, int positionAge, int hpaStatus, int mpaStatus, int lpaStatus, int externalPower, char batChanged, int speed, int direction)
        {
            this.type = type;
            this.triggerType = triggerType;
            this.batteryLevel = batteryLevel;
            this.fixTime = fixTime;
            this.gpsStatus = gpsStatus;
            this.gsmStatus = gsmStatus;
            this.positionAge = positionAge;
            this.hpaStatus = hpaStatus;
            this.mpaStatus = mpaStatus;
            this.lpaStatus = lpaStatus;
            this.externalPower = externalPower;
            this.batchanged = batChanged;
            this.speed = speed;
            this.direction = direction;
        }

        ////JAVA TO VB & C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
        ////ORIGINAL LINE: public TTMessageData(String message) throws Exception
        //public TTMessageData(string message)
        //{

        //    if (message == null || message.Length <= MIN_TT_MESSAGE_LENGTH)
        //    {
        //        throw new Exception("No TrimTrac message suplied or too short.");
        //    }
        //    if (!message.StartsWith(">RTK") || !message.EndsWith("<"))
        //    {
        //        throw new Exception("Invalid TrimTrac message format. Should be >RTK...<");
        //    }

        //    this.type = message[4];
        //    try
        //    {
        //        this.triggerType = Convert.ToInt32("" + message[9]);
        //    }
        //    catch (NumberFormatException e)
        //    {
        //        throw new Exception("Error parsing message. Unknown triggertype : " + e);
        //    }

        //    string batteryLevel = message.Substring(10, 13);
        //    try
        //    {
        //        this.batteryLevel = new int(batteryLevel).intValue();
        //        this.batteryStatusKnown = true;
        //    }
        //    catch (NumberFormatException e1)
        //    {
        //        this.batteryStatusKnown = false;
        //    }
        //    this.batchanged = message[13];
        //    string val = message.Substring(14, 18);
        //    int weekNumber;
        //    try
        //    {
        //        weekNumber = Convert.ToInt32(val);
        //    }
        //    catch (Exception e)
        //    {
        //        throw new Exception("Error in TrimTrac message. Error parsing week number: " + val);
        //    }

        //    val = message.Substring(18, 24);
        //    int secondsIntoWeek;
        //    try
        //    {
        //        secondsIntoWeek = Convert.ToInt32(val);
        //    }
        //    catch (Exception e)
        //    {
        //        throw new Exception("Error in TrimTrac message. Error parsing seconds into week: " + val);
        //    }

        //    // // The time in the message is in UTC
        //    //		SimpleTimeZone timezone = new SimpleTimeZone(0, "UTC");
        //    //		Calendar fixTime = new GregorianCalendar(timezone);
        //    //		fixTime.set(1980, 0, 6, 0, 0, 0); // GPS start/offset date
        //    //		fixTime.add(Calendar.SECOND, (weekNumber * SECS_IN_WEEK) + secondsIntoWeek);
        //    //		
        //    //this.fixTime = fixTime;
        //    this.fixTime = GpsTime.toCalendar(weekNumber, secondsIntoWeek);
        //    this.gpsStatus = Convert.ToInt32("" + message[24]);
        //    this.gsmStatus = Convert.ToInt32("" + message[25]);

        //    this.positionAge = Convert.ToInt32("" + message[26]);
        //    this.hpaStatus = Convert.ToInt32("" + message[27]);
        //    this.mpaStatus = Convert.ToInt32("" + message[28]);
        //    this.lpaStatus = Convert.ToInt32("" + message[29]);
        //    this.externalPower = Convert.ToInt32("" + message[30]);

        //    if (type == 'P' || type == 'p')
        //    {
        //        // get the position information we want to know here
        //        try
        //        {
        //            this.speed = Convert.ToInt32(message.Substring(63, 66));
        //            this.direction = Convert.ToInt32(message.Substring(66, 69));
        //        }
        //        catch (NumberFormatException e)
        //        {
        //            throw new Exception("Error in TrimTrac message. Error parsing the speed / direction: " + e);
        //        }
        //    }

        //}

        //    *
        //	 * Gets the first index of a sign char (+/-) after startIndex. Returns -1 if not found.
        //	 * @param message
        //	 * @param startIndex
        //	 * @return
        //	 	
        internal static int getIndexOfSign(string message, int startIndex)
        {
            int p = message.IndexOf('+', startIndex);
            int m = message.IndexOf('-', startIndex);
            int result = p;
            if (p < 0)
            {
                result = m;
            }
            else if (m >= 0)
            {
                result = Math.Min(p, m);
            }
            return result;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public virtual int getBatteryLevel()
        {
            return BatteryLevel;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public virtual bool isBatteryStatusKnown()
        {
            return BatteryStatusKnown;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public virtual int getExternalPower()
        {
            return ExternalPower;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public virtual Calendar getFixTime()
        {
            return FixTime;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public virtual int getGpsStatus()
        {
            return GpsStatus;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public virtual int getGsmStatus()
        {
            return GsmStatus;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public virtual int getHpaStatus()
        {
            return HpaStatus;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public virtual int getLpaStatus()
        {
            return LpaStatus;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public virtual int getMpaStatus()
        {
            return MpaStatus;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public virtual int getPositionAge()
        {
            return PositionAge;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public virtual int getTriggerType()
        {
            return TriggerType;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public virtual char getType()
        {
            return Type;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public virtual char getBatChanged()
        {
            return this.Batchanged;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public virtual int getSpeed()
        {
            return Speed;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public virtual int getDirection()
        {
            return Direction;
        }
    }
}