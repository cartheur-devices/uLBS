/*
    Copyright (c) 2009, FindWhere

    All rights reserved.

    This composite work is copyrighted in its entirety. Permission to use this material may be granted only after we receive a written request from you, and you receive written permission from us.
*/
using System;
using System.Text;
using System.Runtime.InteropServices;

namespace WIMP
{
    /// <summary>
    /// WIMP message superclass, all message classes must derive from this one.
    /// </summary>
    public abstract class Message
    {
        protected string _fieldseparator = ";";
        protected string _protocolversion = "1";

        protected string _userid;
        protected string _appid;

        protected string _imsi;
        protected string _password;

        protected int _messagesequencenumber;
        protected int _cellid;
        protected int _lacid;
        protected int _mncid;
        protected int _mccid;
        protected int _signal;
        protected int _networktype;
        protected int _spottype;
        protected int _batlevel;
        protected int _speedinkmhour;
        protected int _headingindegrees;

        protected DateTime _messagetime;
        protected DateTime _telemetrytime;

        protected MessageTypes _messagetype;
        protected MessageSubTypes _messagesubtype;
                
        protected double? _latitude;
        protected double? _longitude;

        protected string _servercommand;
        
        /// <summary>
        /// Constructor.
        /// </summary>
        public Message()
        {
        }

        /// <summary>
        /// Generate textual representation of this object.
        /// It's abstract so all subclasses must implemented their own ToString() method.
        /// </summary>
        /// <returns></returns>
        public override abstract string ToString();

        /// <summary>
        /// Set specific data for this object.
        /// It's abstract so all subclasses must implemented their own SetData() method.
        /// </summary>
        public abstract void SetData(String[] InData);

        /// <summary>
        /// Message type enumeration.
        /// </summary>
        public enum MessageTypes
        {
            /// <summary>
            /// Acknowledge message.
            /// </summary>
            ACK,
            /// <summary>
            /// Not acknowledged message.
            /// </summary>
            NAK,
            /// <summary>
            /// Panic message.
            /// </summary>
            PAN,
            /// <summary>
            /// Telemetry Message.
            /// </summary>
            TLM,
            /// <summary>
            /// Configuration Message.
            /// </summary>
            CON,
            /// <summary>
            /// Configuration Request.
            /// </summary>
            CRQ,
            /// <summary>
            /// Shadow message.
            /// </summary>
            SHA,
            /// <summary>
            /// Configuration Acknowledge Message.
            /// </summary>
            CAM,
            /// <summary>
            /// TLM Dump message.
            /// </summary>
            DMP, 
            /// <summary>
            /// Blog Message.
            /// </summary>
            BLG,
            /// <summary>
            /// Personal Status Message.
            /// </summary>
            PSM,
            /// <summary>
            /// Photo Message.
            /// </summary>
            PHO,
            /// <summary>
            /// Photo Init Message.
            /// </summary>
            PHI,
            /// <summary>
            /// Speeding Message alert.
            /// </summary>
            SPD,
            /// <summary>
            /// Battery critical alert.
            /// </summary>
            BAT,
            /// <summary>
            /// ShutDowN message.
            /// </summary>
            SDN,
            /// <summary>
            /// Latest Version Request
            /// </summary>
            LVR,
            /// <summary>
            /// Latest Version Answer
            /// </summary>
            LVA,
            /// <summary>
            /// Request for Config
            /// </summary>
            RFC
        }

        /// <summary>
        /// Configuration type enumeration.
        /// </summary>
        public enum CONTypes
        {
            /// <summary>
            /// Position exchange (1,0) [Only stops GPRS NOT GPS].
            /// </summary>
            A,
            /// <summary>
            /// Domestic interval (1-X in minutes).
            /// </summary>
            B, 
            /// <summary>
            /// International interval (1-X in minutes).
            /// </summary>
            C,
            /// <summary>
            /// Not moving delay (0-20) (0=off, 1-20 times interval, at least 1 position is send every 24 hours).
            /// </summary>
            D,
            /// <summary>
            /// Disconnect GPRS connection after plot (on/off; 1/0).
            /// </summary>
            E,
            /// <summary>
            /// Settings menu visible on mobile (on/off; 1/0).
            /// </summary>
            F, 
            /// <summary>
            /// password for unlocking settings menu on mobile (a-z, A-Z, 0-9).
            /// </summary>
            G,
            /// <summary>
            /// Virtual Mobile number for SMS gateway value [+XX 0123456789].
            /// </summary>
            H,
            /// <summary>
            /// Number of times trying to send message using GPRS before sending a message per SMS (1-X).
            /// </summary>
            I,
            /// <summary>
            /// Battery level Percentage at when an extra TLM message is send, Battery Low alert (1-99).
            /// </summary>
            J,
            /// <summary>
            /// GPS on/off (on/off; 1/0).
            /// </summary>
            K,
            /// <summary>
            /// GPS margin in meters for not moving calculation (1-999).
            /// </summary>
            L,
            /// <summary>
            /// Disconnect GPS connection after plot (0-X, 0=off, 1-X minutes defines how many minutes in advance GPS is started before next interval expires).
            /// </summary>
            M,
            /// <summary>
            /// Dump X records of the position table (1-X, number of records to dump).
            /// </summary>
            N,
            /// <summary>
            /// Position table interval (0-X, 0=off, 1-X in seconds, total table holds up to 1.000 location records including all relevant data as specified in TLM message) . Keep in mind this variable is overruled by M. If M=0 then the table will NOT be filled.
            /// </summary>
            O,
            /// <summary>
            /// Panic function (0=disabled, 1=YesButton, 2=NoButton).
            /// </summary>
            P,
            /// <summary>
            /// Fencing (ID % Latitude % Longitude % Radius % In/Out/Both).
            /// </summary>
            Q,
            /// <summary>
            /// Max SMS (0-X).
            /// </summary>
            R,
            /// <summary>
            /// Speeding limit (0=disabled, 1-X in km/u).
            /// </summary>
            S,
            /// <summary>
            /// Auto start application (on/off; 1/0).
            /// </summary>
            T,
            ///<summary>
            ///New URL
            ///</summary>
            U,
            /// <summary>
            /// New Personal Message.
            /// </summary>
            Y,
            /// <summary>
            /// E-mail.
            /// </summary>
            Z
            
            
        }

        /// <summary>
        /// ACK / NAK type enumeration.
        /// </summary>
        public enum MessageSubTypes
        {
            // Nak starts here

            /// <summary>
            /// New localizer available.
            /// </summary>
            A,
            /// <summary>
            /// Unknown user.
            /// </summary>
            B,
            /// <summary>
            /// GPS not saved.
            /// </summary>
            D,
            /// <summary>
            /// Cell not saved, use GPS.
            /// </summary>
            H,
            /// <summary>
            /// Invalid data.
            /// </summary>
            I,
            /// <summary>
            /// Personal message not saved.
            /// </summary>
            K,
            /// <summary>
            /// Photo not saved.
            /// </summary>
            M,
            /// <summary>
            /// Blog not saved.
            /// </summary>
            

            // Ack starts here
            O,
            /// <summary>
            /// Cell saved @ [time].
            /// </summary>
            C,
            /// <summary>
            /// GPS saved @ [time].
            /// </summary>
            G,
            /// <summary>
            /// Unknown cell, use GPS.
            /// </summary>
            F,
            /// <summary>
            /// Personal message saved @ [time].
            /// </summary>
            J,
            /// <summary>
            /// Photo saved @ [time].
            /// </summary>
            L,
            /// <summary>
            /// Blog saved @ [time].
            /// </summary>
            N,
            /// <summary>
            /// Change received
            /// </summary>
            E
        }

        #region Public Properties
        public string ServerCommand
        {
            get { return _servercommand; }
            set { _servercommand = value; }
        }

        public MessageSubTypes SubType
        {
            get { return _messagesubtype; }
            set { _messagesubtype = value; }
        }

        public int HeadingInDegrees
        {
            get { return _headingindegrees; }
            set { _headingindegrees = value; }
        }

        public int SpeedInKMHour
        {
            get { return _speedinkmhour; }
            set { _speedinkmhour = value; }
        }

        public struct SYSTEM_POWER_STATUS_EX2
        {
            public byte ACLineStatus;
            public byte BatteryFlag;
            public byte BatteryLifePercent;
            public byte Reserved1;
            public int BatteryLifeTime;
            public int BatteryFullLifeTime;
            public byte Reserved2;
            public byte BackupBatteryFlag;
            public byte BackupBatteryLifePercent;
            public byte Reserved3;
            public int BackupBatteryLifeTime;
            public int BackupBatteryFullLifeTime;
            public int BatteryVoltage;
            public int BatteryCurrent;
            public int BatteryAverageCurrent;
            public int BatteryAverageInterval;
            public int BatterymAHourConsumed;
            public int BatteryTemperature;
            public int BackupBatteryVoltage;
            public byte BatteryChemistry;
        };
        
        [DllImport("coredll.dll")]
        public static extern int GetSystemPowerStatusEx2(ref SYSTEM_POWER_STATUS_EX2 ex2, int len, bool update);

        public int BatteryLevel
        {
            get {
               SYSTEM_POWER_STATUS_EX2 ex2 = new SYSTEM_POWER_STATUS_EX2();
               if (GetSystemPowerStatusEx2(ref ex2, System.Runtime.InteropServices.Marshal.SizeOf(ex2), false) == 0)
               {
                    return _batlevel;
                }
                else
                {
                    return ex2.BatteryLifePercent;
                }
             
            }
            set { _batlevel = value; }
        }

        public int CELLID
        {
            get { return _cellid; }
            set { _cellid = value; }
        }

        public int LACID
        {
            get { return _lacid; }
            set { _lacid = value; }
        }

        public int MNCID
        {
            get { return _mncid; }
            set { _mncid = value; }
        }

        public int MCCID
        {
            get { return _mccid; }
            set { _mccid = value; }
        }

        public int Signal
        {
            get { return _signal; }
            set { _signal = value; }
        }

        /// <summary>
        /// Network type, possible values:
        /// 0,Network mode is unknown.
        /// 1,Mobile device is not registered.
        /// 2,GSM/GPRS or DCS1800 network.
        /// 3,AMPS network.
        /// 4,CDMA (IS-95) network.
        /// 5,CDMA (cdma2000) network.
        /// 6,WCDMA (UTRA Frequency Division Duplex (FDD)) network.
        /// 7,TD-CDMA (UTRA Time Division Duplex (TDD)) network.  
        /// </summary>
        public int NetworkType
        {
            get { return _networktype; }
            set { _networktype = value; }
        }

        /// <summary>
        /// Separator character, default character is ';'.
        /// </summary>
        public string FieldSeparator
        {
            get { return _fieldseparator; }
        }

        public string ProtocolVersion
        {
            get { return _protocolversion; }
        }

        public string Password
        {
            get { return _password; }
            set { _password = value; }
        }

        public string UserID
        {
            get { return _userid; }
            set { _userid = value; }
        }

        public string AppID
        {
            get { return _appid; }
            set { _appid = value; }
        }


       

        public int MessageSequenceNumber
        {
            get { return _messagesequencenumber; }
            set { _messagesequencenumber = value; }
        }

        public DateTime MessageTime
        {
            get { return _messagetime; }
            set { _messagetime = value; }
        }

        public MessageTypes MessageType
        {
            get { return _messagetype; }
            set { _messagetype = value; }
        }   
        
        public DateTime TelemetryTime
        {
            get { return _telemetrytime; }
            set { _telemetrytime = value; }
        }

        public double? Latitude
        {
            get { return _latitude; }
            set { _latitude = value; }
        }

        public double? Longitude
        {
            get { return _longitude; }
            set { _longitude = value; }
        }
        #endregion

        private static DateTime UnixEpoch
        {
            get { return new DateTime(1970, 1, 1); }
        }

        protected static double ToUnixTime(DateTime dateTime)
        {
            TimeSpan timeSpan = dateTime - UnixEpoch;
            return timeSpan.TotalSeconds;
        }
    }
}
