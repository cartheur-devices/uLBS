using System;
using System.Threading;
using System.Collections.Generic;
using System.Runtime.InteropServices;

using WIMP;

using Findwhere.Codebase.WindowsMobile.TAPI;

using Microsoft.WindowsMobile.Samples.Location;
using Microsoft.WindowsMobile.Status;

namespace FindwhereMobileShared
{
    /// <summary>
    /// Summary description for LocationMonitor.
    /// </summary>
    public class LocationMonitor : IDisposable 
    {
        #region Private Declarations
        // Thread variables
        private bool            _LocationMonitorActive;
        private Thread          _LocationMonitorThread;
        private bool            _UDPThreadActive;
        private Thread          _UDPThread;

        // Shared data between threads
        private readonly AutoResetEvent _Wh = new AutoResetEvent(false);
        private readonly Queue<String>  _Tasks = new Queue<String>();
        private readonly Queue<String> _PanicTasks = new Queue<String>();
        
        private bool            _DumpQueue = false;
        private bool            _Roaming;
        
        private WIMPMessage     _CurrentMessage;
        private readonly Gps    _Gps;
        private GpsPosition     _CurPos = null;
        private GpsPosition     _OldPos = null;
        private TimeSpan        _UTCDelta;
        
        private int             _MessageSequence                = 0;
        private int             _PanicMessageSequence           = 0;
        private int             _LocationQueuePollingTime       = 20;
        private int             _PanicEnabledQueuePollingTime   = 0;
        private int             _InitGPSPollingTime             = 0;
        private int             _SecondsElapsed                 = 0;
        private int             _OldCell                        = 0;
        private int             _notmovingcount                 = 0;
        private int             _senterrorcount                 = 0;
        private int             _batterystrength                = 0;
        private int             _sentsmscount                   = 0;

        private String          _UDPResult      = String.Empty;
        private readonly String _AppID          = String.Empty;
        private String          _IMSI           = String.Empty;
#if! LIVECONTACTS
        private bool            _PanicEnabled = false;
#endif
        #endregion

        #region Public Properties
        public string UDPResult
        {
            get { return _UDPResult; }
        }
        
        public int CurrentBatteryStrength
        {
            set { _batterystrength = value; }
        }

        public string CurrentLongitude
        {
            get
            {
                if (_CurPos != null)
                    return _CurPos.Longitude.ToString();
                else 
                    return null;
            }
        }

        public string CurrentLatitude
        {
            get
            {
                if (_CurPos != null)
                    return _CurPos.Latitude.ToString();
                else
                    return null;
            }
        }

        public int PollingTime
        {
            set { _LocationQueuePollingTime = value; }
            get { return _LocationQueuePollingTime; }
        }

        public string IMSI
        {
            get { return _IMSI; }
        }

        public int SecondsLeft
        {
            get { return _LocationQueuePollingTime-_SecondsElapsed; }
        }
        #endregion

        #region Enumerations
        public enum PowerMode
        {
            ReevaluateStat = 0x0001,
            PowerChange = 0x0002,
            UnattendedMode = 0x0003,
            SuspendKeyOrPwrButtonPressed = 0x0004,
            SuspendKeyReleased = 0x0005,
            AppButtonPressed = 0x0006
        }
        #endregion

        #region Platform Invokes
        [DllImport("coredll.dll")]
        public static extern int SystemIdleTimerReset();

        [DllImport("coredll.dll")]
        public static extern void PowerPolicyNotify(PowerMode powermode, int flags);
        #endregion

        #region Constructor
        public LocationMonitor()
        {
            _CurrentMessage = new WIMPMessage();
            _SecondsElapsed = 0;

            SetInterval(SystemState.PhoneRoaming);

            _PanicEnabledQueuePollingTime = Configuration.Instance().PanicQueueInterval;
            _InitGPSPollingTime = Configuration.Instance().DisconnectGPSAfterPlot;

            // Obtain lat/long from the GPS device
            _Gps = new Gps();
            _Gps.LocationChanged += new LocationChangedEventHandler(_Gps_LocationChanged);

            // Open GPS immediately, unless batterysafemode is on
            if (Configuration.Instance().DisconnectGPSAfterPlot == 0 && Configuration.Instance().GPS)
            {
                _Gps.Open();
#if DEBUG
                OnNotifyEvent(new LocMonEventArgs(LocMonNotifications.GPSTurnedOn));
#endif
            }

            // initial message sequence
            _MessageSequence = 1;
            _AppID = Configuration.Instance().ApplicationID.ToString();
        }

        #endregion

        void _Gps_LocationChanged(object sender, LocationChangedEventArgs args)
        {
            if (args.Position.LongitudeValid)
            {
                _CurPos = args.Position;

                _UTCDelta = _CurPos.Time - DateTime.UtcNow;
            }
        }

        public void GetIMSI()
        {
#if LIVECONTACTS
            // Calculate the CRC checksum;
            Crc32 Crc = new Crc32();
            Crc.Reset();
            Crc.Update(System.Text.Encoding.UTF8.GetBytes(Configuration.Instance().Password)); // Message minus the checksum
            string Checksum = String.Format("{0:X8}", Crc.Value);

            _IMSI = String.Concat(Configuration.Instance().Email, "%", Checksum);
#else
            try
            {
                PhoneInfo.Get(out _IMSI);
            }
            catch (Exception ex)
            { }

            _CurrentMessage.UserID = _IMSI;
#endif
        }

        /// <summary>
        /// Start LocationMonitor thread, enable unattended power mode, start UDP thread.
        /// </summary>
        public void Start()
        {
            if (!_LocationMonitorActive)
            {
                _LocationMonitorThread = new Thread(new ThreadStart(RunLocationMonitor));
                _LocationMonitorThread.IsBackground = true;
                _LocationMonitorThread.Priority = ThreadPriority.Lowest;
                _LocationMonitorThread.Name = "LocationMonitor";
                _LocationMonitorThread.Start();

                OnNotifyEvent(new LocMonEventArgs(LocMonNotifications.Start));

                PowerPolicyNotify(PowerMode.UnattendedMode, 1);

                if (!_UDPThreadActive)
                {
                    _UDPThread = new Thread(RunUDPThread);
                    _UDPThread.IsBackground = true;
                    _UDPThread.Priority = ThreadPriority.Lowest;
                    _UDPThread.Name = "UDPThread";
                    _UDPThread.Start();
                }
            }
        }

        /// <summary>
        /// Stop locationmonitor. Disable unattended mode.
        /// </summary>
        public void Stop()
        {
            PowerPolicyNotify(PowerMode.UnattendedMode, 0);

            OnNotifyEvent(new LocMonEventArgs(LocMonNotifications.Stop));

            _LocationMonitorActive  = false;
            _UDPThreadActive        = false;
        }

        /// <summary>
        /// Dispose locationmonitor, clear UDP queue.
        /// </summary>
        public void Dispose()
        {
            Stop();

            _Gps.Close();
            
            EnqueueTask(null);      // Signal the consumer to exit.
            _UDPThread.Join();      // Wait for the consumer's thread to finish.
            _Wh.Close();            // Release any OS resources.

            _LocationMonitorThread  = null;
            _UDPThread              = null;
        }

        /// <summary>
        /// Set timer interval according to roaming state.
        /// </summary>
        public void SetInterval(bool IsRoaming)
        {
            if (IsRoaming)
            {
                _Roaming = true;
                PollingTime = Configuration.Instance().InternationalInterval;
            }
            else
            {
                _Roaming = false;
                PollingTime = Configuration.Instance().DomesticInterval;
            }
        }


#if !LIVECONTACTS
        public void DoPanic()
        {
            _PanicEnabled = true;
        }
#endif

        private void RunLocationMonitor()
        {
            _LocationMonitorActive = true;

            while (_LocationMonitorActive)
            {
                _SecondsElapsed++;

                // Disabled because of battery drainage

                // Reset standby every X seconds
                //if (_SecondsElapsed % 20 == 0)
                //{
                //    SystemIdleTimerReset();
                //}

#if !LIVECONTACTS
                if (_PanicEnabled)
                {
                    _PanicEnabled = false;

                    bool SomeGPSToSend = false;
                    bool SomeCellToSend = false;
                    bool SkipSendingCell = false;
                    bool SkipSendingGPS = false;
                    _CurrentMessage = new WIMPMessage();

                    CollectLocation(ref _CurrentMessage, ref SomeGPSToSend, ref SomeCellToSend, ref SkipSendingCell, ref SkipSendingGPS);

                    #region COMMON
                    if (SomeGPSToSend || SomeCellToSend)
                    {
                        _CurrentMessage.Signal = SystemState.PhoneSignalStrength;
                        _CurrentMessage.NetworkType = PhoneInfo.GetCellularConnectionType();
                        _CurrentMessage.BatteryLevel = _batterystrength;
                        _CurrentMessage.AppID = _AppID;
                        _CurrentMessage.MessageType = WIMPMessage.MessageTypes.PAN;
                        _CurrentMessage.UserID = _IMSI;
                        _CurrentMessage.MessageSequenceNumber = _PanicMessageSequence;

                        EnqueuePanicTask(_CurrentMessage.CreateUDPString());
                        _PanicMessageSequence++;
                    }
                    else
                    {
                    }

                    #endregion
                }
#endif

                // GPS Startup (in batterysafe mode only)
                if (_InitGPSPollingTime > 0)
                {
                    if (Configuration.Instance().GPS)
                    {
                        if (_SecondsElapsed >= (_LocationQueuePollingTime - _InitGPSPollingTime))
                        {
                            if (!_Gps.Opened)
                            {
                                _Gps.Open();
#if DEBUG
                                OnNotifyEvent(new LocMonEventArgs(LocMonNotifications.GPSTurnedOn));
#endif

                                // Disable opening GPS for the duration of this loop
                                _InitGPSPollingTime = 0;
                            }
                        }
                    }
                }

#if !LIVECONTACTS
                // Panic Queue
                if (_PanicEnabledQueuePollingTime > 0)
                {
                    if (_SecondsElapsed >= _PanicEnabledQueuePollingTime)
                    {
                        WIMPMessage PanicQueueMessage = new WIMPMessage();

                        bool SomeGPSToSend = false;
                        bool SomeCellToSend = false;
                        bool SkipSendingCell = false;
                        bool SkipSendingGPS = false;

                        CollectLocation(ref PanicQueueMessage, ref SomeGPSToSend, ref SomeCellToSend, ref SkipSendingCell, ref SkipSendingGPS);

                        if (SomeGPSToSend || SomeCellToSend)
                        {
                            PanicQueueMessage.Signal = SystemState.PhoneSignalStrength;
                            PanicQueueMessage.NetworkType = PhoneInfo.GetCellularConnectionType();
                            PanicQueueMessage.BatteryLevel = _batterystrength;
                            PanicQueueMessage.AppID = _AppID;
                            PanicQueueMessage.MessageType = WIMPMessage.MessageTypes.DMP;
                            PanicQueueMessage.UserID = _IMSI;
                            PanicQueueMessage.MessageSequenceNumber = _PanicMessageSequence;

                            EnqueuePanicTask(PanicQueueMessage.CreateUDPString());
                            _PanicMessageSequence++;
                        }

                        _PanicEnabledQueuePollingTime = 0;
                    }
                }
#endif

                // Location Queue
                if (_LocationQueuePollingTime > 0)
                {
                    if (_SecondsElapsed >= _LocationQueuePollingTime)
                    {
                        _SecondsElapsed = 0;
                        bool SomeGPSToSend = false;
                        bool SomeCellToSend = false;
                        bool SkipSendingCell = false;
                        bool SkipSendingGPS = false;
                        _CurrentMessage = new WIMPMessage();

                        CollectLocation(ref _CurrentMessage, ref SomeGPSToSend, ref SomeCellToSend, ref SkipSendingCell, ref SkipSendingGPS);

                        #region COMMON
                        if (SkipSendingGPS && SkipSendingCell)
                        {
                            OnNotifyEvent(new LocMonEventArgs(LocMonNotifications.LocationNotChanged, (++_notmovingcount).ToString()));
                        }
                        else if (SomeGPSToSend || SomeCellToSend)
                        {
                            _notmovingcount = 0;

                            _CurrentMessage.Signal = SystemState.PhoneSignalStrength;
                            _CurrentMessage.NetworkType = PhoneInfo.GetCellularConnectionType();
                            _CurrentMessage.BatteryLevel = _batterystrength;
                            _CurrentMessage.AppID = _AppID;

                            if (SomeGPSToSend && Configuration.Instance().SpeedAlertInKM>0&&_CurrentMessage.SpeedInKMHour > Configuration.Instance().SpeedAlertInKM)
                                _CurrentMessage.MessageType = WIMPMessage.MessageTypes.SPD;
                            else if (_CurrentMessage.BatteryLevel<Configuration.Instance().MinBatLevel)
                                _CurrentMessage.MessageType = WIMPMessage.MessageTypes.BAT;
                            else
                                _CurrentMessage.MessageType = WIMPMessage.MessageTypes.TLM;

                            _CurrentMessage.UserID = _IMSI;
                            _CurrentMessage.MessageSequenceNumber = _MessageSequence;

                            EnqueueTask(_CurrentMessage.CreateUDPString());
                            _MessageSequence++;

                            OnNotifyEvent(new LocMonEventArgs(LocMonNotifications.LocationQueued));

                            #region Kernel Prototype
                            //if(_CurrentMessage.LongitudeString!=null)
                            //    WriteJS(_CurrentMessage.CELLID, _CurrentMessage.LongitudeString, _CurrentMessage.LatitudeString);
                            //else
                            //    WriteJS(_CurrentMessage.CELLID, "-", "-");
                            #endregion
                        }
                        else
                        {
                            OnNotifyEvent(new LocMonEventArgs(LocMonNotifications.LocationNotFound));
                        }
                        #endregion
                    }
                }

                Thread.Sleep(1000);
            }
        }

        #region IPAQ specific cellid functions
		[DllImport("IPAQRil.dll")]
        public extern static int iPAQRilGetCellID(ref int hResult);

        [DllImport("IPAQRil.dll")]
        public extern static int iPAQRilGetLAC(ref int hResult);

        [ DllImport("IPAQRil.dll") ]
        public extern static int iPAQRilGetMCC_MNC(ref int hResult);

        //[ DllImport("IPAQRil.dll") ]
        //public extern static int iPAQRilGetSignalQuality(ref IPAQRILSIGNALQUALITY hResult);

        //// Signal strength quality
        //public struct IPAQRILSIGNALQUALITY
        //{
        //    public int iSignalStrength;
        //    public int iMinSignalStrength;
        //    public int iMaxSignalStrength;
        //    public int dwBitErrorRate;
        //    public int iLowSignalStrength;
        //    public int iHighSignalStrength;
        //};

        public RIL.RILCELLTOWERINFO GetIpaqCellInfo()
        {
            int reg_lac = -1;
            int reg_cellid = -1;
            int cimi = -1;
            int cimi_mcc = -1;
            int cimi_mnc = -1;
            //IPAQRILSIGNALQUALITY csq_rssi = new IPAQRILSIGNALQUALITY();
            RIL.RILCELLTOWERINFO oCellInfo = new RIL.RILCELLTOWERINFO();

            try
            {
                iPAQRilGetCellID(ref reg_cellid);
            }
            catch { reg_cellid = -1; }
            try
            {
                iPAQRilGetLAC(ref reg_lac);
            }
            catch { reg_lac = -1; }
            //try
            //{
            //    iPAQRilGetSignalQuality(ref csq_rssi);
            //}
            //catch { csq_rssi.iSignalStrength = 99; }

            try
            {
                iPAQRilGetMCC_MNC(ref cimi);
                cimi_mnc = Convert.ToInt32(Convert.ToString(cimi).Substring(3, 2));
                cimi_mcc = Convert.ToInt32(Convert.ToString(cimi).Substring(0, 3));
            }
            catch
            {
                cimi_mnc = -1;
                cimi_mcc = -1;
            }

            if (reg_cellid > 0)
                oCellInfo.dwCellID = (uint)reg_cellid;
            else
                oCellInfo.dwCellID = 0;

            if (reg_lac > 0)
                oCellInfo.dwLocationAreaCode = (uint)reg_lac;
            else
                oCellInfo.dwLocationAreaCode = 0;

            if (cimi_mcc > 0)
                oCellInfo.dwMobileCountryCode = (uint)cimi_mcc;
            else
                oCellInfo.dwMobileCountryCode = 01;

            if (cimi_mnc > 0)
                oCellInfo.dwMobileNetworkCode = (uint)cimi_mnc;
            else
                oCellInfo.dwMobileNetworkCode = 0;

            ////			<rssi>:
            ////			0: -113 dBm or less
            ////			1: -111 dBm
            ////			2 to 30: -109 to –53 dBm
            ////			31: -51dBm or greater
            ////			99: not known or not detectable
            //if (csq_rssi.iSignalStrength <= -113)
            //    oCellInfo.RSSI = 0;
            //else if (csq_rssi.iSignalStrength >= -51)
            //    oCellInfo.RSSI = 31;
            //else if (csq_rssi.iSignalStrength >= -112 && csq_rssi.iSignalStrength <= -52)
            //    oCellInfo.RSSI = (int)Math.Ceiling((csq_rssi.iSignalStrength + 113) * .5);
            //else
            //    oCellInfo.RSSI = 99;

            return oCellInfo;
        }
        #endregion

        private void CollectLocation(ref WIMPMessage _CurrentCollectedMessage, ref bool SomeGPSToSend, ref bool SomeCellToSend, ref bool SkipSendingCell, ref bool SkipSendingGPS)
        {
            #region GPS
            GpsPosition gpspos = null;

            try
            {
                if (_CurPos != null)
                {
                    if (DateTime.UtcNow.Add(_UTCDelta) - _CurPos.Time  < new TimeSpan(0, 2, 0))
                    {
                        // Get GPS Data (max age is _LocationQueuePollingTime(current interval setting) seconds from now)
                        //gpspos = _Gps.GetPosition(new TimeSpan(0, 2, 0)); // old value was _LocationQueuePollingTime instead of 5
                        gpspos = _CurPos;

                        // Open GPS immediately, unless batterysafemode is on
                        if (Configuration.Instance().DisconnectGPSAfterPlot > 0)
                        {
                            _Gps.Close();
                            _InitGPSPollingTime = Configuration.Instance().DisconnectGPSAfterPlot;
#if DEBUG
                            OnNotifyEvent(new LocMonEventArgs(LocMonNotifications.GPSTurnedOff));
#endif
                        }
                    }
                }
            }
            catch (Exception ex)
            { }

            if (gpspos != null)
            {
                if (gpspos.LongitudeValid)
                {
                    SomeGPSToSend = true;

                    _CurrentCollectedMessage.Latitude = gpspos.Latitude;
                    _CurrentCollectedMessage.Longitude = gpspos.Longitude;

                    _CurrentCollectedMessage.HeadingInDegrees = Convert.ToInt32(gpspos.Heading);
                    _CurrentCollectedMessage.SpeedInKMHour = Convert.ToInt32(gpspos.Speed * 1.85200);
                    _CurrentCollectedMessage.TelemetryTime = gpspos.Time;

                    // Do we have a previous location?
                    if (_OldPos != null)
                    {
                        // Check if GPS location if different from last one
                        if (gpspos.DistLatLong(_OldPos.Latitude, _OldPos.Longitude, GpsPosition.DistanceUnits.Kilometers) * 1000 > Configuration.Instance().GPSMarginInMeters)
                        {
#if DEBUG
                            OnNotifyEvent(new LocMonEventArgs(LocMonNotifications.GPSChange, String.Concat(gpspos.Latitude.ToString(), " - ", gpspos.Longitude.ToString())));
#endif

                            _OldPos = gpspos;
                        }
                        else
                        {
                            if (Configuration.Instance().NotMovingDelay > 0)
                            {
                                if (_notmovingcount < Configuration.Instance().NotMovingDelay)
                                {
                                    SkipSendingGPS = true;
#if DEBUG
                                    OnNotifyEvent(new LocMonEventArgs(LocMonNotifications.NoGPSChange));
#endif
                                }
                                else
                                {
#if DEBUG
                                    OnNotifyEvent(new LocMonEventArgs(LocMonNotifications.GPSChange, String.Concat(gpspos.Latitude.ToString(), " - ", gpspos.Longitude.ToString())));
#endif
                                }
                            }
                            else
                            {
#if DEBUG
                                // Moving delay is disabled
                                OnNotifyEvent(new LocMonEventArgs(LocMonNotifications.GPSChange, String.Concat(gpspos.Latitude.ToString(), " - ", gpspos.Longitude.ToString())));
#endif
                            }
                        }
                    }
                    else
                    {

                        // first GPS location
#if DEBUG
                        OnNotifyEvent(new LocMonEventArgs(LocMonNotifications.GPSChange, String.Concat(gpspos.Latitude.ToString(), " - ", gpspos.Longitude.ToString())));
#endif
                        _OldPos = gpspos;
                    }
                }
                else
                {
#if DEBUG
                    OnNotifyEvent(new LocMonEventArgs(LocMonNotifications.NoGPSFix));
#endif
                }
            }
            else
            {
#if DEBUG
                OnNotifyEvent(new LocMonEventArgs(LocMonNotifications.NoGPSDevice));
#endif
            }
            #endregion

            #region CELLID

            RIL.RILCELLTOWERINFO tmpcellinfo;

            if (OpenNETCF.WindowsCE.DeviceManagement.OemInfo.ToLower().IndexOf("ipaq") >= 0)
            {
                tmpcellinfo = GetIpaqCellInfo();
            }
            else
            {
                tmpcellinfo = RIL.GetCellTowerInfo();
            }


            if (tmpcellinfo != null)
            {
                _CurrentMessage.CELLID = (int) tmpcellinfo.dwCellID;
                _CurrentMessage.LACID = (int) tmpcellinfo.dwLocationAreaCode;
                _CurrentMessage.MCCID = (int) tmpcellinfo.dwMobileCountryCode;
                _CurrentMessage.MNCID = (int) tmpcellinfo.dwMobileNetworkCode;

                SomeCellToSend = true;

                // Do we have a previous location?
                if (_OldCell != 0)
                {
                    // Check if CellID is different from last one
                    if (_OldCell != (int) tmpcellinfo.dwCellID)
                    {
#if DEBUG
                        OnNotifyEvent(new LocMonEventArgs(LocMonNotifications.CellFound,String.Concat(tmpcellinfo.dwCellID.ToString(), ",",tmpcellinfo.dwLocationAreaCode.ToString())));
#endif

                        _OldCell = (int) tmpcellinfo.dwCellID;
                    }
                    else
                    {
                        if (Configuration.Instance().NotMovingDelay > 0)
                        {
                            if (_notmovingcount < Configuration.Instance().NotMovingDelay)
                            {
                                SkipSendingCell = true;
                            }
                            else
                            {
#if DEBUG
                                OnNotifyEvent(
                                    new LocMonEventArgs(LocMonNotifications.CellFound,
                                                        String.Concat(tmpcellinfo.dwCellID.ToString(), ",",
                                                                      tmpcellinfo.dwLocationAreaCode.ToString())));
#endif
                            }
                        }
                        else
                        {
#if DEBUG
                            // Moving delay is disabled
                            OnNotifyEvent(
                                new LocMonEventArgs(LocMonNotifications.CellFound,
                                                    String.Concat(tmpcellinfo.dwCellID.ToString(), ",",
                                                                  tmpcellinfo.dwLocationAreaCode.ToString())));
#endif
                        }
                    }
                }
                else
                {
#if DEBUG
                    OnNotifyEvent(
                        new LocMonEventArgs(LocMonNotifications.CellFound,
                                            String.Concat(tmpcellinfo.dwCellID.ToString(), ",",
                                                          tmpcellinfo.dwLocationAreaCode.ToString())));
#endif

                    _OldCell = (int) tmpcellinfo.dwCellID;
                }
            }
            else
            {
                _CurrentMessage.CELLID = 0;
                _CurrentMessage.LACID = 0;
                _CurrentMessage.MCCID = 0;
                _CurrentMessage.MNCID = 0;
            }

            #endregion
        }

        #region Kernel prototype
        ///// <summary>
        ///// Write javascript to file.
        ///// </summary>
        //public void WriteJS(int CellID, string Lon, string Lat)
        //{
        //    try
        //    {
        //        // header elements
        //        StreamWriter writer = File.CreateText(GetJSPath());

        //        writer.WriteLine("function LocateMe()");
        //        writer.WriteLine("\t{");
                    
        //        writer.WriteLine("\tloctime=\"{0}\";", DateTime.Now.ToShortTimeString());
        //        writer.WriteLine("\tcellvalue=\"{0}\";", CellID.ToString());
        //        writer.WriteLine("\tlonvalue=\"{0}\";", Lon);
        //        writer.WriteLine("\tlatvalue=\"{0}\";", Lat);

        //        // footer elements
        //        writer.WriteLine("\t}");

        //        writer.Close();
        //    }
        //    catch (Exception ex)
        //    {
        //        System.Diagnostics.Debug.WriteLine(ex.Message);
        //    }
        //}

        ///// <summary>
        ///// Return full path to settings file. Appends .config to the assembly name.
        ///// </summary>
        //private string GetJSPath()
        //{
        //    return Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetName().CodeBase) + @"\locateme.js";
        //}
        #endregion

        #region Events
        public enum LocMonNotifications
        {
            Stop = 1,
            Start,
            LocationQueued,
            GPSLocationSent,
            CellLocationSent,
            CellLocationUnknownSent,
            LocationNotChanged,
            LocationNotSent,
            LocationSentError,
            LocationNotFound,
            ConfigurationChange,
            LocationBeingSent,
            ServerNAK
#if DEBUG
            ,
            NoGPSDevice,
            NoGPSFix,
            NoGPSChange,
            GPSChange,
            CellChanged, //??
            CellFound,
            LacChanged,
            GPSTurnedOff,
            GPSTurnedOn
#endif
        }

        public class LocMonEventArgs : EventArgs
        {
            public LocMonEventArgs(LocMonNotifications n)
            {
                notification = n;
            }
            public LocMonEventArgs(LocMonNotifications n, string d)
            {
                notification = n;
                data = d;
            }
            private readonly LocMonNotifications notification;
            private readonly string data;
            public LocMonNotifications Notification
            {
                get { return notification; }
            }
            public string Data
            {
                get { return data; }
            }
        }

        public event NotifyEventHandler RaiseNotifyEvent;
        public delegate void NotifyEventHandler(object sender, LocMonEventArgs e);
        public void OnNotifyEvent(LocMonEventArgs e)
        {
            // Event will be null if there are no subscribers
            if (RaiseNotifyEvent != null)
            {
                RaiseNotifyEvent(this, e);
            }
        }
        #endregion

        #region UDP communication thread
        public void EnqueuePanicTask(String task)
        {
            _PanicTasks.Enqueue(task);

            if (_PanicTasks.Count > Configuration.Instance().PanicQueueSize)
                _PanicTasks.Dequeue();
        }

        public void EnqueueTask(String task)
        {
            _Tasks.Enqueue(task);
            _Wh.Set();
        }

        private void RunUDPThread() 
        {
            UDP UDPHelper = new UDP();

            _UDPThreadActive = true;

            while (_UDPThreadActive) 
            {
                String task = null;
                if (_DumpQueue)
                {
                    if (_PanicTasks.Count > 0)
                    {
                        task = _PanicTasks.Dequeue();

                        if (task == null) return;
                    }
                    else
                    {
                        _DumpQueue = false;
                    }
                }
                else
                {
                    if (_Tasks.Count > 0)
                    {
                        task = _Tasks.Dequeue();

                        if (task == null) return;
                    }
                }
                
                if (task != null)
                {
                    OnNotifyEvent(new LocMonEventArgs(LocMonNotifications.LocationBeingSent));

                    _UDPResult = UDPHelper.Send(task, Configuration.Instance().Server, Configuration.Instance().Port);

                    if (_UDPResult == "-1" )
                    {
                        OnNotifyEvent(new LocMonEventArgs(LocMonNotifications.LocationSentError, "-1"));
                        Thread.Sleep(5000);

                        // Send failed, queue task again
                        if (HandleLocationUDPError(task))
                        {
                            EnqueueTask(task);
                        }
                    }
                    if (_UDPResult == "-2")
                    {
                        OnNotifyEvent(new LocMonEventArgs(LocMonNotifications.LocationSentError, "-2"));
                        Thread.Sleep(5000);

                        // Send failed, queue task again
                        if (HandleLocationUDPError(task))
                        {
                            EnqueueTask(task);
                        }
                    }
                    if (_UDPResult == "-3")
                    {
                        OnNotifyEvent(new LocMonEventArgs(LocMonNotifications.LocationSentError, "-3"));
                        Thread.Sleep(5000);

                        // Send failed, queue task again
                        if (HandleLocationUDPError(task))
                        {
                            EnqueueTask(task);
                        }
                    }
                    else if (_UDPResult.Length == 0)
                    {
                        OnNotifyEvent(new LocMonEventArgs(LocMonNotifications.LocationNotSent));
                        Thread.Sleep(5000);

                        // Send failed, queue task again
                        if (HandleLocationUDPError(task))
                        {
                            EnqueueTask(task);
                        }
                    }
                    else
                    {
                        WIMPMessage ServerResponse = new WIMPMessage();
                        bool ResponseOK = false;

                        try
                        {
                            ServerResponse.ReadServerResponse(_UDPResult);
                            ResponseOK = true;
                        }
                        catch
                        {
                            OnNotifyEvent(new LocMonEventArgs(LocMonNotifications.LocationSentError, "-4"));
                        }

                        if (ResponseOK)
                        {
                            if (ServerResponse.MessageType == WIMPMessage.MessageTypes.NAK)
                            {
                                if (ServerResponse.NAKType == WIMPMessage.NAKTypes.I)
                                {
                                    OnNotifyEvent(new LocMonEventArgs(LocMonNotifications.ServerNAK, "I"));
                                }
                                else if (ServerResponse.NAKType == WIMPMessage.NAKTypes.B)
                                {
                                    OnNotifyEvent(new LocMonEventArgs(LocMonNotifications.ServerNAK, "B"));
                                }
                                else if (ServerResponse.NAKType == WIMPMessage.NAKTypes.D)
                                {
                                    OnNotifyEvent(new LocMonEventArgs(LocMonNotifications.ServerNAK, "D"));
                                }
                                else if (ServerResponse.NAKType == WIMPMessage.NAKTypes.H)
                                {
                                    OnNotifyEvent(new LocMonEventArgs(LocMonNotifications.ServerNAK, "H"));
                                }
                            }
                            else if (ServerResponse.MessageType == WIMPMessage.MessageTypes.ACK)
                            {
                                if (ServerResponse.ACKType == WIMPMessage.ACKTypes.G)
                                    OnNotifyEvent(new LocMonEventArgs(LocMonNotifications.GPSLocationSent));
                                else if (ServerResponse.ACKType == WIMPMessage.ACKTypes.C)
                                    OnNotifyEvent(new LocMonEventArgs(LocMonNotifications.CellLocationSent));
                                else if (ServerResponse.ACKType == WIMPMessage.ACKTypes.F)
                                    OnNotifyEvent(new LocMonEventArgs(LocMonNotifications.CellLocationUnknownSent));

                                if (ServerResponse.ServerCommand)
                                {
                                    WIMPMessage ClientCommand = new WIMPMessage();
                                    ClientCommand.MessageType = WIMPMessage.MessageTypes.CRQ;
                                    ClientCommand.AppID = Configuration.Instance().ApplicationID.ToString();
                                    ClientCommand.UserID = _IMSI;
                                    ClientCommand.MessageSequenceNumber = _MessageSequence;

                                    EnqueueTask(ClientCommand.CreateCRQ());
                                    _MessageSequence++;
                                }
                            }
                            else if (ServerResponse.MessageType == WIMPMessage.MessageTypes.CON)
                            {
                                HandleConfig(ServerResponse);
                            }
                            else
                            {
                                OnNotifyEvent(new LocMonEventArgs(LocMonNotifications.LocationNotSent));

                                // Send failed, queue task again
                                if (HandleLocationUDPError(task))
                                {
                                    EnqueueTask(task);
                                }
                            }
                        }
                    }
                }
                else
                {
                    _Wh.WaitOne(); // No more _Tasks - wait for a signal
                }
            }

            UDPHelper = null;
        }

        private void HandleConfig(WIMPMessage ConfigMessage)
        {
            switch (ConfigMessage.CONType)
            {
                case WIMPMessage.CONTypes.A:
                    if (ConfigMessage.CONValue == "1")
                    {
                        Configuration.Instance().PositionExchange = true;
                        Start();
                        OnNotifyEvent(new LocMonEventArgs(LocMonNotifications.ConfigurationChange));
                    }
                    else if (ConfigMessage.CONValue == "0")
                    {
                        Configuration.Instance().PositionExchange = false;
                        _LocationMonitorActive = false;
                        OnNotifyEvent(new LocMonEventArgs(LocMonNotifications.ConfigurationChange));
                    }
                    break;
                case WIMPMessage.CONTypes.B:
                    int DomesticInterval = Configuration.Instance().DomesticInterval;

                    try
                    {
                        // Convert minutes to seconds
                        DomesticInterval = Convert.ToInt32(ConfigMessage.CONValue) * 60;
                    }
                    catch { }

                    if (DomesticInterval > 0)
                    {
                        Configuration.Instance().DomesticInterval = DomesticInterval;
                        if(!_Roaming)
                            _LocationQueuePollingTime = DomesticInterval;
                        OnNotifyEvent(new LocMonEventArgs(LocMonNotifications.ConfigurationChange));
                    }
                    
                    break;
                case WIMPMessage.CONTypes.C:
                    int InternationalInterval = Configuration.Instance().InternationalInterval;

                    try
                    {
                        // Convert minutes to seconds
                        InternationalInterval = Convert.ToInt32(ConfigMessage.CONValue) * 60;
                    }
                    catch { }

                    if (InternationalInterval > 0)
                    {
                        Configuration.Instance().InternationalInterval = InternationalInterval;
                        if (_Roaming)
                            _LocationQueuePollingTime = InternationalInterval;
                        OnNotifyEvent(new LocMonEventArgs(LocMonNotifications.ConfigurationChange));
                    }

                    break;
                case WIMPMessage.CONTypes.D:
                    int NotMovingDelay = Configuration.Instance().NotMovingDelay;

                    // Not moving delay
                    try
                    {
                        NotMovingDelay = Convert.ToInt32(ConfigMessage.CONValue);
                    }
                    catch { }

                    if (NotMovingDelay >= 0)
                    {
                        Configuration.Instance().NotMovingDelay = NotMovingDelay;
                        OnNotifyEvent(new LocMonEventArgs(LocMonNotifications.ConfigurationChange));
                    }
                    break;
                case WIMPMessage.CONTypes.E:
                    // Disconnect GPRS
                    break;
                case WIMPMessage.CONTypes.F:
                    // Settings visible

                    try
                    {
                        bool SettingsVisible = Convert.ToBoolean(ConfigMessage.CONValue);
                        Configuration.Instance().SettingsVisible = SettingsVisible;
                        OnNotifyEvent(new LocMonEventArgs(LocMonNotifications.ConfigurationChange));
                    }
                    catch { }

                    break;
                case WIMPMessage.CONTypes.G:
                    // Settings password 
                    string SettingsPassword = Configuration.Instance().SettingsPassword;

                    try
                    {
                        SettingsPassword = ConfigMessage.CONValue;
                    }
                    catch { }

                    if (SettingsPassword != null)
                    {
                        Configuration.Instance().SettingsPassword = SettingsPassword;
                        OnNotifyEvent(new LocMonEventArgs(LocMonNotifications.ConfigurationChange));
                    }

                    break;
                case WIMPMessage.CONTypes.H:
                    // VMN 
                    string VMN = Configuration.Instance().VMN;

                    try
                    {
                        VMN = ConfigMessage.CONValue;
                    }
                    catch { }

                    if (VMN != null)
                    {
                        Configuration.Instance().VMN = VMN;
                        OnNotifyEvent(new LocMonEventArgs(LocMonNotifications.ConfigurationChange));
                    }

                    break;
                case WIMPMessage.CONTypes.I:
                    // SMS Fallback retry

                    int FailCountSMS = Configuration.Instance().FailCountSMS;

                    try
                    {
                        FailCountSMS = Convert.ToInt32(ConfigMessage.CONValue);
                    }
                    catch { }

                    if (FailCountSMS >= 0)
                    {
                        Configuration.Instance().FailCountSMS = FailCountSMS;
                        OnNotifyEvent(new LocMonEventArgs(LocMonNotifications.ConfigurationChange));
                    }

                    break;
                case WIMPMessage.CONTypes.J:
                    // Battery level alert level

                    int MinBatLevel = Configuration.Instance().MinBatLevel;

                    try
                    {
                        MinBatLevel = Convert.ToInt32(ConfigMessage.CONValue);
                    }
                    catch { }

                    if (MinBatLevel >= 0)
                    {
                        Configuration.Instance().MinBatLevel = MinBatLevel;
                        OnNotifyEvent(new LocMonEventArgs(LocMonNotifications.ConfigurationChange));
                    }

                    break;
                case WIMPMessage.CONTypes.K:
                    // GPS on/off

                    try
                    {
                        bool GPS = Convert.ToBoolean(ConfigMessage.CONValue);
                        Configuration.Instance().GPS = GPS;
                        OnNotifyEvent(new LocMonEventArgs(LocMonNotifications.ConfigurationChange));
                    }
                    catch { }
                    break;
                case WIMPMessage.CONTypes.L:
                    // GPS margin

                    int GPSMarginInMeters = Configuration.Instance().GPSMarginInMeters;

                    try
                    {
                        GPSMarginInMeters = Convert.ToInt32(ConfigMessage.CONValue);
                    }
                    catch { }

                    if (GPSMarginInMeters >= 0)
                    {
                        Configuration.Instance().GPSMarginInMeters = GPSMarginInMeters;
                        OnNotifyEvent(new LocMonEventArgs(LocMonNotifications.ConfigurationChange));
                    }
                    break;
                case WIMPMessage.CONTypes.M:
                    // Disconnect GPS after plot

                    try
                    {
                        int DisconnectGPSAfterPlot = Convert.ToInt32(ConfigMessage.CONValue);
                        Configuration.Instance().DisconnectGPSAfterPlot = DisconnectGPSAfterPlot;
                        OnNotifyEvent(new LocMonEventArgs(LocMonNotifications.ConfigurationChange));
                    }
                    catch { }
                    break;
                case WIMPMessage.CONTypes.N:
                    // Dump

                    _DumpQueue = true;
                    _Wh.Set();


                    break;
                case WIMPMessage.CONTypes.O:
                    // PanicQueue interval

                    int PanicQueueInterval = Configuration.Instance().PanicQueueInterval;

                    try
                    {
                        PanicQueueInterval = Convert.ToInt32(ConfigMessage.CONValue);
                    }
                    catch { }

                    if (PanicQueueInterval >= 0)
                    {
                        Configuration.Instance().PanicQueueInterval = PanicQueueInterval;
                        OnNotifyEvent(new LocMonEventArgs(LocMonNotifications.ConfigurationChange));
                    }

                    break;

                case WIMPMessage.CONTypes.Q:
                    // Geofencing

                    break;
                case WIMPMessage.CONTypes.R:
                    // Max SMS to send
                    int MaxSMS = Configuration.Instance().MaxSMS;

                    try
                    {
                        MaxSMS = Convert.ToInt32(ConfigMessage.CONValue);
                    }
                    catch { }

                    if (MaxSMS >= 0)
                    {
                        Configuration.Instance().MaxSMS = MaxSMS;
                        OnNotifyEvent(new LocMonEventArgs(LocMonNotifications.ConfigurationChange));
                    }

                    break;
                case WIMPMessage.CONTypes.S:
                    // Speed limit

                    int SpeedAlertInKM = Configuration.Instance().SpeedAlertInKM;

                    try
                    {
                        SpeedAlertInKM = Convert.ToInt32(ConfigMessage.CONValue);
                    }
                    catch { }

                    if (SpeedAlertInKM >= 0)
                    {
                        Configuration.Instance().SpeedAlertInKM = SpeedAlertInKM;
                        OnNotifyEvent(new LocMonEventArgs(LocMonNotifications.ConfigurationChange));
                    }

                    break;
                case WIMPMessage.CONTypes.T:
                    // Auto start

                    try
                    {
                        bool AutoStart = Convert.ToBoolean(ConfigMessage.CONValue);
                        Configuration.Instance().AutoStart = AutoStart;
                        OnNotifyEvent(new LocMonEventArgs(LocMonNotifications.ConfigurationChange));
                    }
                    catch { }

                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Notify system of UDP error.
        /// </summary>
        /// <returns>True if location needs to be re-queued. False is text message is sent in stead of UDP.</returns>
        private bool HandleLocationUDPError(string TLMMessage)
        {
            _senterrorcount++;

            // SMS fallback
            if (_senterrorcount > Configuration.Instance().FailCountSMS)
            {
                if (_sentsmscount<Configuration.Instance().MaxSMS)
                {
                    // Only send real SMS in RELEASE mode
#if !DEBUG
                    FindwhereMobileShared.TAPI.SMS.SendSMS(TLMMessage, Configuration.Instance().VMN);
#endif
                    _sentsmscount++;

                    return false;
                }
                else
                {
                    return true;
                }
            }

            return true;
        }
        #endregion
    }
}