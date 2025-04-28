using System;
using System.Threading;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Collections;
using System.Reflection;
using WIMP;
using MobileTracker.Codebase.TAPI;
using MobileTracker.GPS;
using Microsoft.WindowsMobile.Status;
using Microsoft.WindowsMobile.PocketOutlook.MessageInterception;
using OpenNETCF.WindowsCE;
using log4net;
using System.Net;


namespace MobileShared
{
    /// <summary>
    /// Summary description for LocationMonitor.
    /// </summary>
    public class LocationMonitor : IDisposable 
    {
        #region Private Declarations
        // Timer for disabling Panic Mope after 1 hour
        private LargeIntervalTimer _Lit_Panic;
        // Timer for collecting regular locations
        private LargeIntervalTimer  _Lit_Location;
        // Timer to wakeup GPS
        private LargeIntervalTimer  _Lit_GPSWakeUP;
        // Timer for collecting dump locations
        private LargeIntervalTimer  _Lit_Dump;

        private MessageFactory      _Factory;

        private MessageInterceptor  _interceptor;

        // Store DateTime at which location timer is ticking
        private DateTime            _Lit_LocationTickTime;
        private DateTime            _SpeedViolationTime = DateTime.MinValue;

        // Shared data between threads
        private Stack<String> _Tasks = new Stack<String>();
        private Stack<String> _DumpTasks = new Stack<String>();
        private CONMessage      _ConfigTask;
        private int             _ConfigTaskSequenceNumber;

        private bool            _DumpQueue = false;
        private bool            _Panic = false;
        private bool            _Roaming = false;
        private bool            _InitialLocationSent = false;
        
        private Gps             _Gps;
        private GpsPosition     _CurPos = null;
        private GpsPosition     _OldPos = null;
        private TimeSpan        _UTCDelta;
        
        private int             _OldCell              = 0;
        private int             _notmovingcount       = 0;
        private int             _senterrorcount       = 0;
        private int             _batterystrength      = 0;
        private int             _sentsmscount         = 0;
        private int             _ConnectionTimeout    = 20000;

        private String          _UDPResult      = String.Empty;
        private String          _IMSI           = null;
#if! LIVECONTACTS
        private String          _UserNamePassword = String.Empty;
#endif
        
        private SystemState     _RoamingState;
        private SystemState     _BatteryStrength;
        private SystemState     _PhoneRadioOffState;
       
//#if DEBUG
//        private ILog            _log;
//#endif
        private IntPtr          _PowerRequirement;
        //private PowerNotifications _pn;
        private bool isPanic = false;
        #endregion

        #region Public Properties       
        public int CurrentBatteryStrength
        {
            set { _batterystrength = value; }
            get { return _batterystrength; }
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

        public string IMSI
        {
            get
            {
                if (_IMSI == null)
                {
                    // TODO catch exception for BJ2
                    try
                    {
                        _IMSI = PhoneInfo.GetIMSI();
                    }
                    catch (Exception ex)
                    {
                        //#if DEBUG
                        //// Failes on some smartphone because of security settings / priveledged apis
                        //_log.Error("GetIMSI()", ex);                        
                        //#endif 
                    }
                }

                return _IMSI; 
            }
        }

        /// <summary>
        /// Returns number of seconds left before LocationMonitor wakes up.
        /// </summary>
        public TimeSpan TimeLeft
        {
            get
            {
                return _Lit_LocationTickTime.Subtract(DateTime.Now);
            }
        }
        #endregion

        #region Enumerations
        //public enum PowerMode
        //{
        //    ReevaluateStat = 0x0001,
        //    PowerChange = 0x0002,
        //    UnattendedMode = 0x0003,
        //    SuspendKeyOrPwrButtonPressed = 0x0004,
        //    SuspendKeyReleased = 0x0005,
        //    AppButtonPressed = 0x0006
        //}

        //public enum PowerState
        //{
        //    POWER_STATE_ON = 0x00010000,        // on state
        //    POWER_STATE_OFF = 0x00020000,       // no power, full off
        //    POWER_STATE_SUSPEND = 0x00200000,   // suspend state
        //    POWER_FORCE = 4096,
        //    POWER_STATE_RESET = 0x00800000,     // reset state
        //    POWER_STATE_IDLE = 0x00100000,      // idle state
        //    POWER_STATE_BACKLIGHTOFF = 0x04000000,
        //    POWER_STATE_UNATTENDED = 0x00400000,// Unattended state.
        //    POWER_STATE_USERIDLE = 0x01000000   // user idle state
        ////#define POWER_STATE_CRITICAL     (DWORD)(0x00040000)        // critical off
        ////#define POWER_STATE_BOOT         (DWORD)(0x00080000)        // boot state
        ////#define POWER_STATE_BACKLIGHTON  (DWORD)(0x02000000)        // device scree backlight on
        ////#define POWER_STATE_PASSWORD     (DWORD)(0x10000000)        // This state is password protected.
        //}

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

        public enum CEDEVICE_POWER_STATE : int
        {
            PwrDeviceUnspecified = -1,
            //Full On: full power,  full functionality
            D0 = 0,
            /// <summary>
            /// Low Power On: fully functional at low power/performance
            /// </summary>
            D1 = 1,
            /// <summary>
            /// Standby: partially powered with automatic wake
            /// </summary>
            D2 = 2,
            /// <summary>
            /// Sleep: partially powered with device initiated wake
            /// </summary>
            D3 = 3,
            /// <summary>
            /// Off: unpowered
            /// </summary>
            D4 = 4,
            PwrDeviceMaximum
        }

        [Flags()]
        public enum DevicePowerFlags
        {
            None = 0,
            /// <summary>
            /// Specifies the name of the device whose power should be maintained at or above the DeviceState level.
            /// </summary>
            POWER_NAME = 0x00000001,
            /// <summary>
            /// Indicates that the requirement should be enforced even during a system suspend.
            /// </summary>
            POWER_FORCE = 0x00001000,
            POWER_DUMPDW = 0x00002000
        }
        #endregion

        #region Platform Invokes
        //[DllImport("coredll.dll")]
        //public static extern int SystemIdleTimerReset();

        //[DllImport("coredll.dll")]
        //public static extern bool PowerPolicyNotify(PowerMode powermode, int flags);

        [DllImport("IPAQRil.dll")]
        extern private static int iPAQRilGetCellID(ref int hResult);

        [DllImport("IPAQRil.dll")]
        extern private static int iPAQRilGetLAC(ref int hResult);

        [DllImport("IPAQRil.dll")]
        extern private static int iPAQRilGetMCC_MNC(ref int hResult);

        [DllImport("CoreDLL", SetLastError = true)]
        public static extern IntPtr SetPowerRequirement
        (
            string pDevice,
            CEDEVICE_POWER_STATE DeviceState,
            DevicePowerFlags DeviceFlags,
            IntPtr pSystemState,
            uint StateFlagsZero
        );

        [DllImport("CoreDLL")]
        public static extern int ReleasePowerRequirement(IntPtr hPowerReq);

        //[ DllImport("IPAQRil.dll") ]
        //public extern static int iPAQRilGetSignalQuality(ref IPAQRILSIGNALQUALITY hResult);

        //[DllImport("coredll.dll", SetLastError = true)]
        //extern private static int SetSystemPowerState(string psState, int StateFlags, int Options);

        //[DllImport("coredll.dll", SetLastError = true)]
		//extern private static int GetSystemPowerState(string psState, UInt32 dwLength, out PowerState flags);
        #endregion

        #region Constructor
        public LocationMonitor()
        {
//#if DEBUG
//            log4net.Config.DOMConfigurator.Configure(new System.IO.FileInfo(Settings.GetFilePath()));

//            // Create a logger
//            _log = LogManager.GetLogger(typeof(LocationMonitor));
 
//            // Log an info level message
//            if (_log.IsInfoEnabled) _log.Info("Application [LocationMonitor] Start");
//#endif
            int ticks = Environment.TickCount;
//#if DEBUG
//            if (_log.IsInfoEnabled) _log.Info("Ticks: " + ticks.ToString());
//#endif
            //_pn = new PowerNotifications();
            //_pn.OnPowerNotifyEvent += new PowerNotifications.PowerNotifyEventHandler(OnPowerNotificationReceived);
            //_pn.Start();

            SetTimers();

            _Factory = new MessageFactory(Configuration.Instance().ApplicationID.ToString(), IMSI);

            // Set up SMS interception
            _interceptor = new MessageInterceptor();
            _interceptor.InterceptionAction = InterceptionAction.NotifyAndDelete;

            _interceptor.MessageCondition = new MessageCondition(MessageProperty.Body, MessagePropertyComparisonType.Contains, "findwhere", false);
            _interceptor.MessageReceived += new MessageInterceptorEventHandler(OnSmsReceived);

            _BatteryStrength = new SystemState(SystemProperty.PowerBatteryStrength);
            _BatteryStrength.Changed += new ChangeEventHandler(_BatteryStrength_Changed);
            CurrentBatteryStrength = (int)SystemState.PowerBatteryStrength;

            

             

            _RoamingState = new SystemState(SystemProperty.PhoneRoaming);
            _RoamingState.Changed += new ChangeEventHandler(_RoamingState_Changed);

            _PhoneRadioOffState = new SystemState(SystemProperty.PhoneRadioOff);
            _PhoneRadioOffState.Changed += new ChangeEventHandler(_PhoneRadioOffState_Changed);

            // Obtain lat/long from the GPS device
            _Gps = new Gps();
            _Gps.LocationChanged += new LocationChangedEventHandler(_Gps_LocationChanged);

            // Open GPS immediately, unless batterysafemode is on or interval is too small, or when application starts in 'wizard' mode
            if (_Lit_Location.Interval.TotalSeconds - Configuration.Instance().DisconnectGPSAfterPlot <= 0 || Configuration.Instance().DisconnectGPSAfterPlot == 0 || Configuration.Instance().WizardVisible || !_InitialLocationSent )
            {
                WakeUpGPS();
            }
        }

     
        #endregion

        /// <summary>
        /// Start LocationMonitor timer, enable unattended power mode, start UDP thread.
        /// </summary>
        public void Start()
        {
//#if DEBUG
//            if (_log.IsDebugEnabled) _log.Debug("Start()");
//#endif

            ResetTimers();

            if (!_Lit_Location.Enabled)
            {
                // Enable Location timer
                if (Configuration.Instance().PositionExchange)
                {
//#if DEBUG
//                    if (_log.IsDebugEnabled) _log.Debug("Start() - _Lit_Location enabled");
//#endif

                    _Lit_Location.Enabled = true;
                    _Lit_LocationTickTime = DateTime.Now.Add(_Lit_Location.Interval);

                    // Notify Location Monitor has started
                    OnNotifyEvent(new LocMonEventArgs(LocMonNotifications.Start));
                }

                // Enable panic timer
                if (Configuration.Instance().DumpQueueInterval > 0)
                {
//#if DEBUG
//                    if (_log.IsDebugEnabled) _log.Debug("Start() - _Lit_Dump enabled");
//#endif

                    _Lit_Dump.Enabled = true;
                }

                // Enable GPS wakeup timer
                if (Configuration.Instance().DisconnectGPSAfterPlot > 0 && Configuration.Instance().GPS)
                {
//#if DEBUG
//                    if (_log.IsDebugEnabled) _log.Debug("Start() - _Lit_GPSWakeUP enabled");
//#endif

                    _Lit_GPSWakeUP.Enabled = true;
                }

                // Enable 'unattended' power mode
                //bool result = PowerPolicyNotify(PowerMode.UnattendedMode, 1);
                //if (_log.IsDebugEnabled) _log.Debug("PowerPolicyNotify(UnattendedMode,1) : " + result.ToString());
            }
        }

        /// <summary>
        /// Stop locationmonitor. Disable unattended mode.
        /// </summary>
        public void Stop()
        {
//#if DEBUG
//            if (_log.IsDebugEnabled) _log.Debug("Stop()");
//#endif

            //_pn.Stop();

            // Stop timers
            _Lit_Location.Enabled = false;
            _Lit_Dump.Enabled = false;
            _Lit_GPSWakeUP.Enabled = false;

            //bool result = PowerPolicyNotify(PowerMode.UnattendedMode, 0);
            //if (_log.IsDebugEnabled) _log.Debug("PowerPolicyNotify(UnattendedMode,0) : " + result.ToString());

            OnNotifyEvent(new LocMonEventArgs(LocMonNotifications.Stop));
        }

        /// <summary>
        /// Dispose locationmonitor, clear UDP queue.
        /// </summary>
        public void Dispose()
        {
//#if DEBUG
//            if (_log.IsDebugEnabled) _log.Debug("Dispose()");
//             LogManager.Shutdown();

//#endif

            // It's not possible to use shutdown hooks in the .NET Compact Framework,
            // so you have manually shutdown log4net to free all resoures.
           
            CollectLocation(Message.MessageTypes.SDN);

            Stop();

            if (_PowerRequirement != null)
                ReleasePowerRequirement(_PowerRequirement);
            _Gps.Close();

            _DumpTasks.Clear();
            _Tasks.Clear();
        }

        /// <summary>
        /// Send latest version request to server.
        /// </summary>
        public void DoUpdate(bool Quiet)
        {
//#if DEBUG
//            if (_log.IsDebugEnabled) _log.Debug("DoUpdate()");
//#endif

            Message ClientMessage = _Factory.CreateMessage(Message.MessageTypes.LVR);

            Assembly ResLoader = Assembly.GetExecutingAssembly();
            Version AppVersion = ResLoader.GetName().Version;
            ClientMessage.SetData(new string[] { AppVersion.ToString() });

            String Result = null;

            // Don't use asynchronous StartSending() because we wan't to wait for it to finish
            try
            {
                UDP UDPHelper = new UDP();
                Result = UDPHelper.SendMessage(ClientMessage.ToString(), Configuration.Instance().Server, Configuration.Instance().Port, 10000);
            }
            catch (Exception ex)
            {
               // #if DEBUG
               //if (_log.IsDebugEnabled) _log.Error(ex.Message);
               // #endif 
            }

            if (Result != null)
            {
                if (Result.Length > 2)
                {
                    Message ServerMessage = null;

                    try
                    {
                        ServerMessage = _Factory.CreateMessage(Result);

                        if (ServerMessage.MessageType == Message.MessageTypes.LVA)
                        {
                            Version LatestVersion = new Version(((WIMP.LVAMessage)ServerMessage).VersionValue);

                            // New version available
                            if (AppVersion < LatestVersion)
                                OnNotifyEvent(new LocMonEventArgs(LocMonNotifications.LVAReceived, ((WIMP.LVAMessage)ServerMessage).URLValue));
                            else
                            {
                                // Only notify if not quiet mode is used (version check on startup application)
                                if (!Quiet)
                                    OnNotifyEvent(new LocMonEventArgs(LocMonNotifications.LVAReceived, "-2"));
                            }
                        }
                        else if (ServerMessage.MessageType == Message.MessageTypes.NAK)
                        {
                            // Blocked, upgrade needed
                            if (ServerMessage.SubType == Message.MessageSubTypes.A)
                            {
                                OnNotifyEvent(new LocMonEventArgs(LocMonNotifications.LVAReceived, ((WIMP.NAKMessage)ServerMessage).ServerCommand));
                            }
                        }
                        else
                        {
                            if (!Quiet)
                                OnNotifyEvent(new LocMonEventArgs(LocMonNotifications.LVAReceived, "-1"));
                        }
                    }
                    catch (Exception ex)
                    {
                        //#if DEBUG
                        //if (_log.IsDebugEnabled) _log.Error(ex.Message);
                        //#endif

                        if (!Quiet)
                            OnNotifyEvent(new LocMonEventArgs(LocMonNotifications.LVAReceived, "-1"));
                    }
                }
                else
                {
                    if (!Quiet)
                        OnNotifyEvent(new LocMonEventArgs(LocMonNotifications.LVAReceived, "-1"));
                }
            }
            else
            {
                if (!Quiet)
                    OnNotifyEvent(new LocMonEventArgs(LocMonNotifications.LVAReceived, "-1"));
            }
        }

        public bool IsPanicSituattion
        {
            get { return blnIsPanicForSettings; }
        }
        public int GetInterValTime
        {
            get { return _Lit_Dump.Interval.Minutes * 60; }            
        }
        public int GetNoMotionDelay
        {
            get { return _notmovingcount; }
        }
        //public bool IsStayConnectedOn
        //{
        //    get { }
        //}

        /// <summary>
        /// Set location monitor into 'panic mode'.
        /// </summary>
        public void DoPanic()
        {
//#if DEBUG
//            if (_log.IsDebugEnabled) _log.Debug("DoPanic()");
//#endif

            try
            {
                _Panic = true;
                _Lit_Panic = new LargeIntervalTimer();
                _Lit_Panic.OneShot = true;
                _Lit_Panic.Interval = new TimeSpan(1, 0, 0);
                _Lit_Panic.Tick += new EventHandler(_Lit_Panic_Tick);

                CollectLocation(Message.MessageTypes.PAN);

                StartSending();

                // Change interval to 60 seconds
                int Interval = 60;
                _Lit_Location.Interval = new TimeSpan(0, 0, Interval);
                _Lit_Location.Enabled = false;
                _Lit_Location.Enabled = true;
                _Lit_LocationTickTime = DateTime.Now.Add(_Lit_Location.Interval);


                _Lit_Dump.Interval = new TimeSpan(0, 0, Interval);
                _notmovingcount = 0;
                Configuration.Instance().DisconnectAfterPlot = false;

                // used to Change Disconnect after plot
                isPanic = true;
                blnIsPanicForSettings = true;
            }
            catch (Exception ex)
            {
                                
            }

        }
        bool blnIsPanicForSettings = false;

        /// <summary>
        /// Disable Panic Mode (ticks after 1 hour)
        /// </summary>
        void _Lit_Panic_Tick(object sender, EventArgs e)
        {
            ResetTimers();
        }

        /// <summary>
        /// Re-init timer values and restart timer.
        /// </summary>
        public void ResetTimers()
        {
//#if DEBUG
//            if (_log.IsDebugEnabled) _log.Debug("ResetTimers()");
//#endif

            SetInterval(SystemState.PhoneRoaming);
        }

        /// <summary>
        /// The MessageInterceptor event handler, which gets called when an SMS 
        /// message arrives.
        /// </summary>
        /// <param name="sender">The MessageInterceptor</param>
        /// <param name="e">Contains the SMS message</param>
        private void OnSmsReceived(object sender, MessageInterceptorEventArgs e)
        {
//#if DEBUG
//            if (_log.IsDebugEnabled) _log.Debug("OnSmsReceived()");
//#endif

            CollectLocation(Message.MessageTypes.TLM);
        }

        /// <summary>
        /// The MessageInterceptor event handler, which gets called when an SMS 
        /// message arrives.
        /// </summary>
        /// <param name="sender">The MessageInterceptor</param>
        /// <param name="e">Contains the SMS message</param>
        //private void OnPowerNotificationReceived(object sender, PowerNotifyEventArgs e)
        //{
        //    if (_log.IsDebugEnabled) _log.Debug("OnPowerNotificationReceived()");

        //    OnNotifyEvent(new LocMonEventArgs(LocMonNotifications.PowerNotification, e.DevicePowerState));
        //}

        /// <summary>
        /// Set interval according to roaming settings.
        /// </summary>
        private void _RoamingState_Changed(object sender, ChangeEventArgs args)
        {
//#if DEBUG
//            if (_log.IsDebugEnabled) _log.Debug("_RoamingState_Changed()");
//#endif

            RoamingStateChanged();
        }

        
        /// <summary>
        /// Disable communication.
        /// </summary>
        private void _PhoneRadioOffState_Changed(object sender, ChangeEventArgs args)
        {
//#if DEBUG
//            if (_log.IsDebugEnabled) _log.Debug("_PhoneRadioOffState_Changed()");
//#endif

            PhoneRadioOffStateChanged();
        }

        private void _BatteryStrength_Changed(object sender, ChangeEventArgs args)
        {
            try
            {
                CurrentBatteryStrength = Convert.ToInt32(args.NewValue);

                if (CurrentBatteryStrength < Configuration.Instance().MinBatLevel)
                    CollectLocation(Message.MessageTypes.BAT);
            }
            catch (Exception ex)
            {
                                
            }
        }

        /// <summary>
        /// Fires whene phone roaming state changes, (re)sets the interval.
        /// </summary>
        private void RoamingStateChanged()
        {
//#if DEBUG
//            if (_log.IsDebugEnabled) _log.Debug("RoamingStateChanged()");
//#endif

            if (Convert.ToBoolean(SystemState.PhoneRoaming) == false)
            {
                SetInterval(false);
            }
            else
            {
                SetInterval(true);
            }
        }

        /// <summary>
        /// Fires whene phone radio state changes.
        /// </summary>
        private void PhoneRadioOffStateChanged()
        {
//#if DEBUG
//            if (_log.IsDebugEnabled) _log.Debug("PhoneRadioOffStateChanged()");
//#endif

            if (Convert.ToBoolean(SystemState.PhoneRadioOff) == true)
            {
                Stop();
                OnNotifyEvent(new LocMonEventArgs(LocMonNotifications.PhoneRadioStateChange, "off"));
            }
            else
            {
                Start();
                OnNotifyEvent(new LocMonEventArgs(LocMonNotifications.PhoneRadioStateChange, "on"));
            }
        }

        /// <summary>
        /// Set all system (large interval) timers.
        /// </summary>
        private void SetTimers()
        {
//#if DEBUG
//            if (_log.IsDebugEnabled) _log.Debug("SetTimers()");
//#endif

            _Lit_Location = new LargeIntervalTimer();
            _Lit_GPSWakeUP = new LargeIntervalTimer();
            _Lit_Dump = new LargeIntervalTimer();

            SetInitialInterval();

            // We want it to be recurring
            _Lit_GPSWakeUP.OneShot = true;
            _Lit_Location.OneShot = false;
            _Lit_Dump.OneShot = false;

            // Wire up a handler
            _Lit_GPSWakeUP.Tick += new EventHandler(_Lit_GPSWakeUP_Tick);
            _Lit_Location.Tick += new EventHandler(m_Lit_Location_Tick);
            _Lit_Dump.Tick += new EventHandler(m_Lit_Dump_Tick);            
        }

        /// <summary>
        /// Turn GPS on.
        /// </summary>
        private void WakeUpGPS()
        {
            _PowerRequirement = SetPowerRequirement("GPD0:", CEDEVICE_POWER_STATE.D0, DevicePowerFlags.POWER_FORCE | DevicePowerFlags.POWER_NAME, IntPtr.Zero, 0);
//#if DEBUG
//            if (_log.IsDebugEnabled) _log.Debug("WakeUpGPS()");
//#endif

            if (Configuration.Instance().GPS)
            {
                if (!_Gps.Opened)
                {
                    _Gps.Open();
//#if DEBUG
//                    OnNotifyEvent(new LocMonEventArgs(LocMonNotifications.GPSTurnedOn));
//#endif
                }
            }
        }


        /// <summary>
        /// If enabled ticks X seconds before Location interval. X is defined in settings 'DisconnectGPSAfterPlot'.
        /// </summary>
        private void _Lit_GPSWakeUP_Tick(object sender, EventArgs e)
        {
//#if DEBUG
//            if (_log.IsDebugEnabled) _log.Debug("_Lit_GPSWakeUP_Tick()");
//#endif

            WakeUpGPS();
        }

        private void StartDoUpdate()
        {
            DoUpdate(true);
        }

        /// <summary>
        /// Regular location interval event.
        /// </summary>
        private void m_Lit_Location_Tick(object sender, EventArgs e)
        {
//#if DEBUG
//            if (_log.IsDebugEnabled) _log.Debug("m_Lit_Location_Tick()");
//#endif

            if (!_InitialLocationSent)
            {
                new Thread(new ThreadStart(StartDoUpdate)).Start();
                
                _InitialLocationSent = true;
                SetInterval(SystemState.PhoneRoaming);
            }

            CollectLocation(Message.MessageTypes.TLM);
            _Lit_LocationTickTime = DateTime.Now.Add(_Lit_Location.Interval);
//#if DEBUG
//            if (_log.IsDebugEnabled) _log.Debug("Set GPS WakeUP : " + DateTime.Now.Add(new TimeSpan(0, 0, Convert.ToInt32(_Lit_Location.Interval.TotalSeconds) - Configuration.Instance().DisconnectGPSAfterPlot)).ToShortTimeString());
//#endif

            _Lit_GPSWakeUP.FirstEventTime = DateTime.Now.Add(new TimeSpan(0,0,Convert.ToInt32(_Lit_Location.Interval.TotalSeconds) - Configuration.Instance().DisconnectGPSAfterPlot));
            _Lit_GPSWakeUP.Enabled = true;
        }

        /// <summary>
        /// If enabled ticks every X seconds and stores location to dump queue.
        /// </summary>
        private void m_Lit_Dump_Tick(object sender, EventArgs e)
        {
//#if DEBUG
//            if (_log.IsDebugEnabled) _log.Debug("m_Lit_Dump_Tick()");
//#endif

            CollectLocation(Message.MessageTypes.DMP);
        }
        
        public void TestLocationNow()
        {
            try
            {
                CollectLocation(Message.MessageTypes.TLM);
            }
            catch (Exception ex)
            {                
            }
        }

        /// <summary>
        /// If enabled ticks every X seconds and stores location to dump queue.
        /// </summary>
        private void CollectLocation(Message.MessageTypes MessageType)
        {
//#if DEBUG
//            if (_log.IsDebugEnabled) _log.Debug("CollectLocation() -" + MessageType.ToString());
//#endif

            bool SomeGPSToSend = false;
            bool SomeCellToSend = false;
            bool SkipSendingCell = false;
            bool SkipSendingGPS = false;

            // Let the factory decide which message to create
            Message CurrentMessage = _Factory.CreateMessage(MessageType);
            //if (SystemState.PhoneRadioOff)
            //{
            //    OnNotifyEvent(new LocMonEventArgs(LocMonNotifications.PhoneRadioStateChange));
            //    return;
            //}
            WakeUpGPS();

            CollectLocation(ref CurrentMessage, ref SomeGPSToSend, ref SomeCellToSend, ref SkipSendingCell, ref SkipSendingGPS);

            if (MessageType == Message.MessageTypes.TLM )
            {
              //  System.Windows.Forms.MessageBox.Show("SkipSendingGPS=" + SkipSendingGPS + "  AND SkipSendingCell=" + SkipSendingCell);  
   
                if (SkipSendingGPS && SkipSendingCell)
                {
                    _Factory.DecreaseMessageSequence();

                    //_Lit_LocationTickTime = DateTime.Now.Add(_Lit_Location.Interval);
                    OnNotifyEvent(new LocMonEventArgs(LocMonNotifications.LocationNotChanged, (++_notmovingcount).ToString()));
                }
                else if (SomeGPSToSend || SomeCellToSend)
                {
                    _notmovingcount = 0;

                    CurrentMessage.SetData(new string[] { SystemState.PhoneSignalStrength.ToString(), PhoneInfo.GetCellularConnectionType().ToString(), CurrentBatteryStrength.ToString() });
                    
                    EnqueueTask(CurrentMessage.ToString());

                    //_Lit_LocationTickTime = DateTime.Now.Add(_Lit_Location.Interval);
                    OnNotifyEvent(new LocMonEventArgs(LocMonNotifications.LocationQueued));

                    StartSending();
                }
                else
                {
                    _Factory.DecreaseMessageSequence();

                    _Lit_LocationTickTime = DateTime.Now.Add(_Lit_Location.Interval);
                    OnNotifyEvent(new LocMonEventArgs(LocMonNotifications.LocationNotFound));
                }
            }
            else if (MessageType == Message.MessageTypes.PAN || MessageType == Message.MessageTypes.DMP)
            {
                EnqueueDumpTask(CurrentMessage.ToString());             
            }
            else if (MessageType == Message.MessageTypes.SDN)
            {
                // Don't use asynchronous StartSending() because we wan't to wait for it to finish (application closes after this action) all message in queue
                // we just want to sent the shutdown message.
                UDP UDPHelper = new UDP();
                UDPHelper.SendMessage(CurrentMessage.ToString(), Configuration.Instance().Server, Configuration.Instance().Port, 5000);
            }
            else
            {
                EnqueueTask(CurrentMessage.ToString());

                StartSending();
            }
        }

        /// <summary>
        /// Fires when new GPS coordinates are received.
        /// </summary>
        private void _Gps_LocationChanged(object sender, LocationChangedEventArgs args)
        {
            try
            {
                if (args.Position != null)
                {
                    _CurPos = args.Position;
                    _UTCDelta = _CurPos.Time - DateTime.UtcNow;

                    // Check for speed violation.
                    if (Configuration.Instance().SpeedAlertInKM > 0)
                    {
                        if (Convert.ToInt32(_CurPos.Speed * 1.85200) > Configuration.Instance().SpeedAlertInKM)
                        {
                            // Only violate once per minute
                            if (DateTime.Now - _SpeedViolationTime > new TimeSpan(0, 0, 60))
                            {
                                CollectLocation(Message.MessageTypes.SPD);
                                _SpeedViolationTime = DateTime.Now;
                            }
                        }
                    }
                }
            }
            catch
            { }
        }

#if LIVECONTACTS
        public void SetUserPassword()
        {
            // Calculate the CRC checksum;
            Crc32 Crc = new Crc32();
            Crc.Reset();
            Crc.Update(System.Text.Encoding.UTF8.GetBytes(Configuration.Instance().Password)); // Message minus the checksum
            string Checksum = String.Format("{0:X8}", Crc.Value);

            _UserNamePassword = String.Concat(Configuration.Instance().Email, "%", Checksum);
        }
#endif

        /// <summary>
        /// Set Location Timer interval according to roaming state.
        /// </summary>
        private void SetInitialInterval()
        {
//#if DEBUG
//            if (_log.IsDebugEnabled) _log.Debug("SetInitialInterval()");
//#endif

            int Interval = 60;

            _Lit_Location.Interval = new TimeSpan(0, 0, Interval);

            // Reset all timers (if they are currently enabled)
            if (_Lit_Location.Enabled)
            {
                _Lit_Location.Enabled = false;
                _Lit_Location.Enabled = true;
                _Lit_LocationTickTime = DateTime.Now.Add(_Lit_Location.Interval);
            }
        }

        /// <summary>
        /// Set Location Timer interval according to roaming state.
        /// </summary>
        private void SetInterval(bool IsRoaming)
        {
//#if DEBUG
//            if (_log.IsDebugEnabled) _log.Debug("SetInterval()");
//#endif
            
            blnIsPanicForSettings = false;

            int Interval;
            _notmovingcount = Configuration.Instance().NotMovingDelay; 
            if (IsRoaming)
            {
                _Roaming = true;
                if (!_InitialLocationSent)
                {
                    Interval = 60;
                }
                else
                {
                    Interval = Configuration.Instance().InternationalInterval;
                }
            }
            else
            {
                _Roaming = false;
                if (!_InitialLocationSent)
                {
                    Interval = 60;
                }
                else
                {
                    Interval = Configuration.Instance().DomesticInterval;
                }
            }

            _Lit_Location.Interval = new TimeSpan(0, 0, Interval);
            if (Configuration.Instance().DumpQueueInterval > 0)
            {
                _Lit_Dump.Interval = new TimeSpan(0, 0, Configuration.Instance().DumpQueueInterval);
            }

            if (!_InitialLocationSent)
            {
                if (Configuration.Instance().DisconnectGPSAfterPlot > 0 && Configuration.Instance().GPS)
                {
                    if (Interval - Configuration.Instance().DisconnectGPSAfterPlot > 0)
                    {
//#if DEBUG
//                        if (_log.IsDebugEnabled) _log.Debug("Set GPS WakeUP : " + DateTime.Now.Add(new TimeSpan(0, 0, Convert.ToInt32(_Lit_Location.Interval.TotalSeconds) - Configuration.Instance().DisconnectGPSAfterPlot)).ToShortTimeString());
//#endif

                        _Lit_GPSWakeUP.FirstEventTime = DateTime.Now.Add(new TimeSpan(0, 0, Convert.ToInt32(_Lit_Location.Interval.TotalSeconds) - Configuration.Instance().DisconnectGPSAfterPlot));
                    }
                }
            }


            // Reset all timers (if they are currently enabled)
            Configuration.Instance().DisconnectAfterPlot = true ;
            if (_Lit_Location.Enabled)
            {
                _Lit_Location.Enabled = false;
                _Lit_Location.Enabled = true;
                _Lit_LocationTickTime = DateTime.Now.Add(_Lit_Location.Interval);
            }

            if (_Lit_Dump.Enabled)
            {
                _Lit_Dump.Enabled = false;
                _Lit_Dump.Enabled = true;
            }

            if (_Lit_GPSWakeUP.Enabled)
            {
                _Lit_GPSWakeUP.Enabled = false;
                _Lit_GPSWakeUP.Enabled = true;
            }
        }

        #region IPAQ specific cellid functions
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

        private void CollectLocation(ref Message _CurrentCollectedMessage, ref bool SomeGPSToSend, ref bool SomeCellToSend, ref bool SkipSendingCell, ref bool SkipSendingGPS)
        {
//#if DEBUG
//            if (_log.IsDebugEnabled) _log.Debug("CollectLocation()");
//#endif

            _CurrentCollectedMessage.SetData(new string[] { SystemState.PhoneSignalStrength.ToString(), PhoneInfo.GetCellularConnectionType().ToString(), CurrentBatteryStrength.ToString() });

            #region GPS
            if (_Gps != null)
            {
                // Close GPS immediately if settings say so
                if (Configuration.Instance().DisconnectGPSAfterPlot > 0 && _Lit_Location.Interval.TotalSeconds - Configuration.Instance().DisconnectGPSAfterPlot > 0 && !Configuration.Instance().WizardVisible)
                {
//#if DEBUG
//                    if (_log.IsDebugEnabled) _log.Debug("CollectLocation() -  _Gps.Close();");
//#endif

                    _Gps.Close();
                    if(_PowerRequirement!=null)
                        ReleasePowerRequirement(_PowerRequirement);
//#if DEBUG
//                    OnNotifyEvent(new LocMonEventArgs(LocMonNotifications.GPSTurnedOff));
//#endif
                }
            }

            // Only add GPS info when aquired within location interval
            if ((_CurPos != null) && (_CurPos.Time > DateTime.UtcNow - _Lit_Location.Interval))
            {
                SomeGPSToSend = true;

                _CurrentCollectedMessage.Latitude = _CurPos.Latitude;
                _CurrentCollectedMessage.Longitude = _CurPos.Longitude;

                _CurrentCollectedMessage.HeadingInDegrees = Convert.ToInt32(_CurPos.Heading);
                _CurrentCollectedMessage.SpeedInKMHour = Convert.ToInt32(_CurPos.Speed * 1.85200);
                _CurrentCollectedMessage.TelemetryTime = _CurPos.Time;

                // Do we have a previous location?
                if (_OldPos != null)
                {
                    // Check if GPS location if different from last one
                    if (_CurPos.DistLatLong(_OldPos.Latitude, _OldPos.Longitude, GpsPosition.DistanceUnits.Kilometers) * 1000 > Configuration.Instance().GPSMarginInMeters)
                    {
//#if DEBUG
//                        OnNotifyEvent(new LocMonEventArgs(LocMonNotifications.GPSChange, String.Concat(_CurPos.Latitude.ToString(), " - ", _CurPos.Longitude.ToString())));
//#endif

                        _OldPos = _CurPos;
                    }
                    else
                    {
                        if (Configuration.Instance().NotMovingDelay > 0)
                        {
                            if (_notmovingcount < Configuration.Instance().NotMovingDelay)
                            {
                                SkipSendingGPS = true;
//#if DEBUG
//                                OnNotifyEvent(new LocMonEventArgs(LocMonNotifications.NoGPSChange));
//#endif
                            }
                            else
                            {
//#if DEBUG
//                                OnNotifyEvent(new LocMonEventArgs(LocMonNotifications.GPSChange, String.Concat(_CurPos.Latitude.ToString(), " - ", _CurPos.Longitude.ToString())));
//#endif
                            }
                        }
                        else
                        {
//#if DEBUG
//                            // Moving delay is disabled
//                            OnNotifyEvent(new LocMonEventArgs(LocMonNotifications.GPSChange, String.Concat(_CurPos.Latitude.ToString(), " - ", _CurPos.Longitude.ToString())));
//#endif
                        }
                    }
                }
                else
                {

                    // first GPS location
//#if DEBUG
//                    OnNotifyEvent(new LocMonEventArgs(LocMonNotifications.GPSChange, String.Concat(_CurPos.Latitude.ToString(), " - ", _CurPos.Longitude.ToString())));
//#endif
                    _OldPos = _CurPos;
                }
            }
            else
            {
//#if DEBUG
//                OnNotifyEvent(new LocMonEventArgs(LocMonNotifications.NoGPSFix));
//#endif
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
                _CurrentCollectedMessage.CELLID = (int)tmpcellinfo.dwCellID;
                _CurrentCollectedMessage.LACID = (int)tmpcellinfo.dwLocationAreaCode;
                _CurrentCollectedMessage.MCCID = (int)tmpcellinfo.dwMobileCountryCode;
                _CurrentCollectedMessage.MNCID = (int)tmpcellinfo.dwMobileNetworkCode;

                SomeCellToSend = true;
                //System.Windows.Forms.MessageBox.Show("CELLID: " + _CurrentCollectedMessage.CELLID + "\n LocationAreaCode: " + _CurrentCollectedMessage.LACID + "\n CountryCode: " + _CurrentCollectedMessage.MCCID + "\n MobileNetworkCode:" + _CurrentCollectedMessage.MNCID); 
               

                // Do we have a previous location?
                if (_OldCell != 0)
                {
                    // Check if CellID is different from last one
                    if (_OldCell != (int) tmpcellinfo.dwCellID)
                    {
//#if DEBUG
//                        OnNotifyEvent(new LocMonEventArgs(LocMonNotifications.CellFound,String.Concat(tmpcellinfo.dwCellID.ToString(), ",",tmpcellinfo.dwLocationAreaCode.ToString())));
//#endif

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
//#if DEBUG
//                                OnNotifyEvent(
//                                    new LocMonEventArgs(LocMonNotifications.CellFound,
//                                                        String.Concat(tmpcellinfo.dwCellID.ToString(), ",",
//                                                                      tmpcellinfo.dwLocationAreaCode.ToString())));
//#endif
                            }
                        }
                        else
                        {
//#if DEBUG
//                            // Moving delay is disabled
//                            OnNotifyEvent(
//                                new LocMonEventArgs(LocMonNotifications.CellFound,
//                                                    String.Concat(tmpcellinfo.dwCellID.ToString(), ",",
//                                                                  tmpcellinfo.dwLocationAreaCode.ToString())));
//#endif
                        }
                    }
                }
                else
                {
//#if DEBUG
//                    OnNotifyEvent(
//                        new LocMonEventArgs(LocMonNotifications.CellFound,
//                                            String.Concat(tmpcellinfo.dwCellID.ToString(), ",",
//                                                          tmpcellinfo.dwLocationAreaCode.ToString())));
//#endif

                    _OldCell = (int) tmpcellinfo.dwCellID;
                }
            }
            else
            {
                _CurrentCollectedMessage.CELLID = 0;
                _CurrentCollectedMessage.LACID = 0;
                _CurrentCollectedMessage.MCCID = 0;
                _CurrentCollectedMessage.MNCID = 0;
                //System.Windows.Forms.MessageBox.Show("Cell Not available.");
            }
            #endregion
        }

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
            ServerNAK,
            ConnectionNotAllowed,
            ConfigReceived,
            ConfigApplied,
            Connecting,
            ConnectFailed,
            LVAReceived,
            PowerNotification,
            PhoneRadioStateChange,
            ConfigChange

             , NoGPSFix,
            NoGPSChange,
            GPSChange,
            CellChanged, //??
            CellFound,
            LacChanged,
            GPSTurnedOff,
            GPSTurnedOn,
            PhoneRadioOff
//#if DEBUG
//,
//            NoGPSFix,
//            NoGPSChange,
//            GPSChange,
//            CellChanged, //??
//            CellFound,
//            LacChanged,
//            GPSTurnedOff,
//            GPSTurnedOn
//#endif
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
        /// <summary>
        /// Add task to the dump queue.
        /// </summary>
        private void EnqueueDumpTask(String task)
        {
//#if DEBUG
//            if (_log.IsDebugEnabled) _log.Debug("EnqueueDumpTask()");
//#endif

            _DumpTasks.Push(task);

            // TODO : delete oldest instead of newest
            if (_DumpTasks.Count > Configuration.Instance().DumpQueueSize)
                _DumpTasks.Pop();
        }

        /// <summary>
        /// Add task to the location queue.
        /// </summary>
        private void EnqueueTask(String task)
        {
//#if DEBUG
//            if (_log.IsDebugEnabled) _log.Debug("EnqueueTask()");
//#endif

            _Tasks.Push(task);
        }

        private void EnqueueConfigTask(CONMessage task)
        {
//#if DEBUG
//            if (_log.IsDebugEnabled) _log.Debug("EnqueueConfigTask()");
//#endif

            _ConfigTask = task;
        }        
    
        /// <summary>
        /// Check if connection is allowed and start sending data.
        /// </summary>
        private void StartSending()
        {
//#if DEBUG
//            if (_log.IsDebugEnabled) _log.Debug("StartSending()");
//#endif

            if (Configuration.Instance().ConnectOnce || Configuration.Instance().AutoConnect)
            {
                new Thread(new ThreadStart(StartSendingUDPMessages)).Start();

                Configuration.Instance().ConnectOnce = false;
            }
            else
            {
                OnNotifyEvent(new LocMonEventArgs(LocMonNotifications.ConnectionNotAllowed));
            }
        }

        /// <summary>
        /// This will create RFC message & will send recent setting-changes
        /// </summary>
        /// <param name="strSettings"></param>
        public void SendSettingChanges(string[] strSettings)
        {
            Message ClientCommand = _Factory.CreateMessage(Message.MessageTypes.RFC);
            ClientCommand.SetData(strSettings);
            EnqueueTask(ClientCommand.ToString());
            StartSending();
            //Thread thrd = new Thread(new ThreadStart(StartSendingUDPMessages));
            //thrd.Start();
        }

       

        /// <summary>
        /// Determines which queue to send.
        /// </summary>
        public void StartSendingUDPMessages()
        {
//#if DEBUG
//            if (_log.IsDebugEnabled) _log.Debug("StartSendingUDPMessages()");
//#endif

            if (_DumpQueue)
            {
                SendUDPMessages(ref _DumpTasks);

                _DumpQueue = false;
            }
            else if(_Panic)
            {
                SendUDPMessages(ref _DumpTasks);

                _Panic = false;
            }
            else
            {
                SendUDPMessages(ref _Tasks);
            }
        }

        delegate string StartSendingDelegate(string DataToSend, string Server, int Port);

        private void SendUDPMessages(ref Stack<String> TasksToSent)
        {
//#if DEBUG
//            if (_log.IsDebugEnabled) _log.Debug("SendUDPMessages()");
//#endif
            int intConnAttemptcounts = 0;
            UDP UDPHelper = null;
            try
            {
                UDPHelper = new UDP();

                // String array for storing failed messages
                string[] TempTasks = new string[TasksToSent.Count + 30];

                int iTempTasksCount = 0;
                int iSentCount = 0;
                bool StopSending = false;
                if (SystemState.PhoneRadioOff)
                {
                    OnNotifyEvent(new LocMonEventArgs(LocMonNotifications.PhoneRadioOff));
                    return;
                }
                if (TasksToSent.Count > 0)
                {
                    OnNotifyEvent(new LocMonEventArgs(LocMonNotifications.Connecting));

                    try
                    {
                        UDPHelper.DoConnect(_ConnectionTimeout);
                    }
                    catch (Exception ex)
                    {
                        //#if DEBUG
                        //if (_log.IsDebugEnabled) _log.Debug("UDPHelper.DoConnect()");
                        //#endif

                        OnNotifyEvent(new LocMonEventArgs(LocMonNotifications.ConnectFailed));
                        //SendSMS(TasksToSent.Pop());
                        return;
                    }
                }

                while (TasksToSent.Count > 0 && iSentCount <= Configuration.Instance().PanicLocationsToSent && !StopSending)
                {
                    iSentCount++;
                    String task = null;
                   

                    task = TasksToSent.Pop();

                    if (task == null) return;


                    if (task != null)
                    {

                        if (task.IndexOf("RFC") > -1)
                            OnNotifyEvent(new LocMonEventArgs(LocMonNotifications.ConfigChange));
                        else
                            OnNotifyEvent(new LocMonEventArgs(LocMonNotifications.LocationBeingSent));
                        //#if DEBUG
                        //                    if (_log.IsDebugEnabled) _log.Debug("SendUDPMessages() - UDPHelper.Send");
                        //#endif


                        //try
                        {
                            _UDPResult = UDPHelper.SendMessage(task, Configuration.Instance().Server, Configuration.Instance().Port, 20000);

                            // Try sending TLM's twice
                            if (task.IndexOf(";TLM;") > 0 || task.IndexOf(";CRQ;") > 0)
                            {
                                // No answer
                                if (_UDPResult == null)
                                {

                                    _UDPResult = UDPHelper.SendMessage(task, Configuration.Instance().Server, Configuration.Instance().Port, 20000);
                                }
                                // Error code
                                else if (_UDPResult.Length < 3)
                                {
                                    _UDPResult = UDPHelper.SendMessage(task, Configuration.Instance().Server, Configuration.Instance().Port, 20000);
                                }
                            }
                        }
                        //catch (Exception ex)
                        //{
                        //  //  SendSMS(task); 
                        //  //  return;                            
                        //}
                        


                        switch (_UDPResult)
                        {


                            case "-1":
                            case "-2":
                            case "-3":
                                OnNotifyEvent(new LocMonEventArgs(LocMonNotifications.LocationSentError, _UDPResult));

                                // Send failed, queue task again
                                if (HandleLocationUDPError(task))
                                {
                                    TempTasks[iTempTasksCount] = task;
                                    iTempTasksCount++;
                                }

                                StopSending = true;
                                break;
                            case "":
                            case null:
                                if (task.IndexOf(";CRQ;") == -1)
                                {
                                    if (task.IndexOf("RFC") == -1)
                                        OnNotifyEvent(new LocMonEventArgs(LocMonNotifications.LocationNotSent));
                                    else
                                        OnNotifyEvent(new LocMonEventArgs(LocMonNotifications.LocationSentError));

                                    // Send failed, queue task again
                                    if (HandleLocationUDPError(task))
                                    {
                                        TempTasks[iTempTasksCount] = task;
                                        iTempTasksCount++;
                                    }
                                }
                                if (isPanic == false)
                                    StopSending = true;
                                break;
                            default:
                                // Let the factory decide which message to create
                                Message ServerResponse = null;
                                if (task.IndexOf("PAN") > -1)
                                    isPanic = false;
                                bool ResponseOK = false;

                                try
                                {
                                    _senterrorcount = 0;
                                    ServerResponse = _Factory.CreateMessage(_UDPResult);
                                    ResponseOK = true;
                                }
                                catch (Exception ex)
                                {
                                    //#if DEBUG
                                    //if (_log.IsDebugEnabled) _log.Error("CreateMessage", ex);
                                    //#endif 
                                    OnNotifyEvent(new LocMonEventArgs(LocMonNotifications.LocationSentError, "-4"));
                                }

                                if (ResponseOK)
                                {                                   
                                    if (ServerResponse.MessageType == Message.MessageTypes.NAK)
                                    {
                                        if (ServerResponse.SubType == Message.MessageSubTypes.I)
                                        {
                                            OnNotifyEvent(new LocMonEventArgs(LocMonNotifications.ServerNAK, "I"));
                                        }
                                        else if (ServerResponse.SubType == Message.MessageSubTypes.B)
                                        {
                                            OnNotifyEvent(new LocMonEventArgs(LocMonNotifications.ServerNAK, "B"));
                                        }
                                        else if (ServerResponse.SubType == Message.MessageSubTypes.D)
                                        {
                                            OnNotifyEvent(new LocMonEventArgs(LocMonNotifications.ServerNAK, "D"));
                                        }
                                        else if (ServerResponse.SubType == Message.MessageSubTypes.H)
                                        {
                                            OnNotifyEvent(new LocMonEventArgs(LocMonNotifications.ServerNAK, "H"));
                                        }
                                        else if (ServerResponse.SubType == Message.MessageSubTypes.A)
                                        {
                                            OnNotifyEvent(new LocMonEventArgs(LocMonNotifications.ServerNAK, "A"));
                                        }
                                    }
                                    else if (ServerResponse.MessageType == Message.MessageTypes.ACK)
                                    {                                       
                                        if (ServerResponse.SubType == Message.MessageSubTypes.G)
                                            OnNotifyEvent(new LocMonEventArgs(LocMonNotifications.GPSLocationSent, _UDPResult));
                                        else if (ServerResponse.SubType == Message.MessageSubTypes.C)
                                            OnNotifyEvent(new LocMonEventArgs(LocMonNotifications.CellLocationSent, _UDPResult));
                                        else if (ServerResponse.SubType == Message.MessageSubTypes.F)
                                            OnNotifyEvent(new LocMonEventArgs(LocMonNotifications.CellLocationUnknownSent, _UDPResult));
                                        else if (ServerResponse.SubType == Message.MessageSubTypes.E)
                                        {
                                            // Check if configs should be applied
                                            if (_ConfigTask != null)
                                            {
                                                // Do the sequencenumbers match?
                                                if (_ConfigTaskSequenceNumber == ServerResponse.MessageSequenceNumber)
                                                {
                                                    ApplyConfig(_ConfigTask);
                                                    OnNotifyEvent(new LocMonEventArgs(LocMonNotifications.ConfigApplied));
                                                }
                                            }
                                        }
                                        //else if (ServerResponse.SubType == Message.MessageSubTypes.U)
                                        //{
                                        //    Configuration.Instance().Server = ServerResponse.ToString(); 
                                        //}

                                        if (ServerResponse.ServerCommand == "1")
                                        {
                                            Message ClientCommand = _Factory.CreateMessage(Message.MessageTypes.CRQ);
                                            EnqueueTask(ClientCommand.ToString());
                                        }
                                    }
                                    else if (ServerResponse.MessageType == Message.MessageTypes.CON)
                                    {
                                        string CAM = StoreConfig((CONMessage)ServerResponse);

                                        if (CAM.Length > 0)
                                        {
                                            EnqueueTask(CAM);
                                            OnNotifyEvent(new LocMonEventArgs(LocMonNotifications.ConfigReceived));
                                        }
                                    }
                                    else
                                    {
                                        OnNotifyEvent(new LocMonEventArgs(LocMonNotifications.LocationNotSent));

                                        // Send failed, queue task again
                                        if (HandleLocationUDPError(task))
                                        {
                                            TempTasks[iTempTasksCount] = task;
                                            iTempTasksCount++;
                                        }
                                    }
                                }
                                break;
                        }
                    }
                }

                // Reschedule failed tasks
                for (int i = 0; i < iTempTasksCount; i++)
                {
                    EnqueueTask(TempTasks[i]);
                }
                TempTasks = null;
               
            }
            catch (Exception ex)
            {

            }
            finally
            {
                if (isPanic == false)
                    UDPHelper.Disconnect();
                UDPHelper = null;
               
            }
        }

        private void SendSMS(string strTask)
        {
            return;   
            //try
            //{
                
            //    Microsoft.WindowsMobile.PocketOutlook.SmsMessage msg = new Microsoft.WindowsMobile.PocketOutlook.SmsMessage(Configuration.Instance().VMN, strTask);    
            //    msg.RequestDeliveryReport = true;
            //    msg.Send();              
            //    msg = null;   
            //}               
            //catch (Microsoft.WindowsMobile.PocketOutlook.InvalidSmsRecipientException ex)
            //{
              
            //}
            //catch (Microsoft.WindowsMobile.PocketOutlook.SmsException ex)
            //{
               
            //}
            //catch (Exception ex)
            //{
              
            //}

            //try
            //{
            //    Microsoft.WindowsMobile.PocketOutlook.SmsMessage msg = new Microsoft.WindowsMobile.PocketOutlook.SmsMessage(Configuration.Instance().VMN2, strTask);
            //    msg.RequestDeliveryReport = true;
            //    msg.Send();              
            //}
            //catch (Microsoft.WindowsMobile.PocketOutlook.InvalidSmsRecipientException ex)
            //{
              
            //}
            //catch (Microsoft.WindowsMobile.PocketOutlook.SmsException ex)
            //{
               
            //}
            //catch (Exception ex)
            //{
              
            //}

            
        }

        /// <summary>
        /// Store configuration temporary and schedule CAM message.
        /// </summary>
        /// <param name="ConfigMessage">Config to store.</param>
        /// <returns></returns>
        private string StoreConfig(CONMessage ConfigMessage)
        {
//#if DEBUG
//            if (_log.IsDebugEnabled) _log.Debug("StoreConfig()");
//#endif

            Message ConfigAcceptMessage = _Factory.CreateMessage(Message.MessageTypes.CAM);
            ConfigAcceptMessage.SetData(new string[] { "1", ConfigMessage.MessageSequenceNumber.ToString()});

            _ConfigTaskSequenceNumber = ConfigMessage.MessageSequenceNumber;
            EnqueueConfigTask(ConfigMessage);

            return ConfigAcceptMessage.ToString();
        }

        /// <summary>
        /// Apply configuration messages from server to set client settings (after ACK message).
        /// </summary>
        private void ApplyConfig(CONMessage ConfigTask)
        {
//#if DEBUG
//            if (_log.IsDebugEnabled) _log.Debug("ApplyConfig()");
//#endif

            int NumberOfConfigs = ConfigTask.CONValue.Length;

            if (ConfigTask != null)
            {
                // Loop through configs
                for (int i = 0; i < NumberOfConfigs; i++)
                {

                    switch (ConfigTask.CONType[i])
                    {
                        case Message.CONTypes.A:
                            if (ConfigTask.CONValue[i] == "1")
                            {
                                Configuration.Instance().PositionExchange = true;
                                Start();
                                OnNotifyEvent(new LocMonEventArgs(LocMonNotifications.ConfigurationChange));
                            }
                            else if (ConfigTask.CONValue[i] == "0")
                            {
                                Configuration.Instance().PositionExchange = false;
                                _Lit_Location.Enabled = false;
                                OnNotifyEvent(new LocMonEventArgs(LocMonNotifications.ConfigurationChange));
                            }
                            break;
                        case Message.CONTypes.B:
                            int DomesticInterval = -1;

                            try
                            {
                                // Convert minutes to seconds
                                DomesticInterval = Convert.ToInt32(ConfigTask.CONValue[i]) * 60;
                            }
                            catch { }

                            if (DomesticInterval > 0)
                            {
                                Configuration.Instance().DomesticInterval = DomesticInterval;
                                SetInterval(_Roaming);

                                OnNotifyEvent(new LocMonEventArgs(LocMonNotifications.ConfigurationChange));
                            }

                            break;
                        case Message.CONTypes.C:
                            int InternationalInterval = -1;

                            try
                            {
                                // Convert minutes to seconds
                                InternationalInterval = Convert.ToInt32(ConfigTask.CONValue[i]) * 60;
                            }
                            catch { }

                            if (InternationalInterval > 0)
                            {
                                Configuration.Instance().InternationalInterval = InternationalInterval;
                                SetInterval(_Roaming);

                                OnNotifyEvent(new LocMonEventArgs(LocMonNotifications.ConfigurationChange));
                            }

                            break;
                        case Message.CONTypes.D:
                            int NotMovingDelay = -1;

                            // Not moving delay
                            try
                            {
                                NotMovingDelay = Convert.ToInt32(ConfigTask.CONValue[i]);
                            }
                            catch { }

                            if (NotMovingDelay >= 0)
                            {
                                Configuration.Instance().NotMovingDelay = NotMovingDelay;
                                OnNotifyEvent(new LocMonEventArgs(LocMonNotifications.ConfigurationChange));
                            }
                            break;
                        case Message.CONTypes.E:
                            // Disconnect GPRS

                            try
                            {
                                bool DisconnectPlot = Convert.ToBoolean(ConfigTask.CONValue[i]);
                                Configuration.Instance().DisconnectAfterPlot = DisconnectPlot;
                                OnNotifyEvent(new LocMonEventArgs(LocMonNotifications.ConfigurationChange));
                            }
                            catch { }
                            break;
                        case Message.CONTypes.F:
                            // Settings visible

                            try
                            {
                                bool SettingsVisible = Convert.ToBoolean(ConfigTask.CONValue[i]);
                                Configuration.Instance().SettingsVisible = SettingsVisible;
                                OnNotifyEvent(new LocMonEventArgs(LocMonNotifications.ConfigurationChange));
                            }
                            catch { }

                            break;
                        case Message.CONTypes.G:
                            // Settings password 
                            string SettingsPassword = "";

                            try
                            {
                                SettingsPassword = ConfigTask.CONValue[i];
                            }
                            catch { }

                            if (SettingsPassword != null)
                            {
                                Configuration.Instance().SettingsPassword = SettingsPassword;
                                OnNotifyEvent(new LocMonEventArgs(LocMonNotifications.ConfigurationChange));
                            }

                            break;
                        case Message.CONTypes.H:
                            // VMN 
                            string VMN = "";

                            try
                            {
                                VMN = ConfigTask.CONValue[i];
                            }
                            catch { }

                            if (VMN != null)
                            {
                                Configuration.Instance().VMN = VMN;
                                OnNotifyEvent(new LocMonEventArgs(LocMonNotifications.ConfigurationChange));
                            }

                            break;
                        case Message.CONTypes.I:
                            // SMS Fallback retry

                            int FailCountSMS = -1;

                            try
                            {
                                FailCountSMS = Convert.ToInt32(ConfigTask.CONValue);
                            }
                            catch { }

                            if (FailCountSMS >= 0)
                            {
                                Configuration.Instance().FailCountSMS = FailCountSMS;
                                OnNotifyEvent(new LocMonEventArgs(LocMonNotifications.ConfigurationChange));
                            }

                            break;
                        case Message.CONTypes.J:
                            // Battery level alert level

                            int MinBatLevel = -1;

                            try
                            {
                                MinBatLevel = Convert.ToInt32(ConfigTask.CONValue[i]);
                            }
                            catch { }

                            if (MinBatLevel >= 0)
                            {
                                Configuration.Instance().MinBatLevel = MinBatLevel;
                                OnNotifyEvent(new LocMonEventArgs(LocMonNotifications.ConfigurationChange));
                            }

                            break;
                        case Message.CONTypes.K:
                            // GPS on/off

                            try
                            {
                                bool GPS = Convert.ToBoolean(ConfigTask.CONValue[i]);
                                Configuration.Instance().GPS = GPS;
                                OnNotifyEvent(new LocMonEventArgs(LocMonNotifications.ConfigurationChange));
                            }
                            catch { }
                            break;
                        case Message.CONTypes.L:
                            // GPS margin

                            int GPSMarginInMeters = -1;

                            try
                            {
                                GPSMarginInMeters = Convert.ToInt32(ConfigTask.CONValue[i]);
                            }
                            catch { }

                            if (GPSMarginInMeters >= 0)
                            {
                                Configuration.Instance().GPSMarginInMeters = GPSMarginInMeters;
                                OnNotifyEvent(new LocMonEventArgs(LocMonNotifications.ConfigurationChange));
                            }
                            break;
                        case Message.CONTypes.M:
                            // Disconnect GPS after plot

                            try
                            {
                                int DisconnectGPSAfterPlot = Convert.ToInt32(ConfigTask.CONValue[i]);
                                Configuration.Instance().DisconnectGPSAfterPlot = DisconnectGPSAfterPlot;
                                OnNotifyEvent(new LocMonEventArgs(LocMonNotifications.ConfigurationChange));
                            }
                            catch { }
                            break;
                        case Message.CONTypes.N:
                            // Dump

                            _DumpQueue = true;
                            StartSendingUDPMessages();

                            break;
                        case Message.CONTypes.O:
                            // PanicQueue interval

                            int DumpQueueInterval = -1;

                            try
                            {
                                DumpQueueInterval = Convert.ToInt32(ConfigTask.CONValue[i]);
                            }
                            catch { }

                            if (DumpQueueInterval >= 0)
                            {
                                Configuration.Instance().DumpQueueInterval = DumpQueueInterval;
                                OnNotifyEvent(new LocMonEventArgs(LocMonNotifications.ConfigurationChange));
                            }

                            break;

                        case Message.CONTypes.Q:
                            // Geofencing

                            break;
                        case Message.CONTypes.R:
                            // Max SMS to send
                            int MaxSMS = 0;

                            try
                            {
                                MaxSMS = Convert.ToInt32(ConfigTask.CONValue[i]);
                            }
                            catch { }

                            if (MaxSMS >= 0)
                            {
                                Configuration.Instance().MaxSMS = MaxSMS;
                                OnNotifyEvent(new LocMonEventArgs(LocMonNotifications.ConfigurationChange));
                            }

                            break;
                        case Message.CONTypes.S:
                            // Speed limit

                            int SpeedAlertInKM = -1;

                            try
                            {
                                SpeedAlertInKM = Convert.ToInt32(ConfigTask.CONValue[i]);
                            }
                            catch { }

                            if (SpeedAlertInKM >= 0)
                            {
                                Configuration.Instance().SpeedAlertInKM = SpeedAlertInKM;
                                OnNotifyEvent(new LocMonEventArgs(LocMonNotifications.ConfigurationChange));
                            }

                            break;
                        case Message.CONTypes.T:
                            // Auto start

                            try
                            {
                                bool AutoStart = Convert.ToBoolean(ConfigTask.CONValue[i]);
                                Configuration.Instance().AutoStart = AutoStart;
                                OnNotifyEvent(new LocMonEventArgs(LocMonNotifications.ConfigurationChange));
                            }
                            catch { }


                            break;
                        case Message.CONTypes.U:
                            try
                            {                                 
                                Configuration.Instance().Server = ConfigTask.CONValue[i];
                            }
                            catch { }
                            break;
                        default:
                            break;
                    }
                }
            }
        }

        /// <summary>
        /// Notify system of UDP error.
        /// </summary>
        /// <returns>True if location needs to be re-queued. False is text message is sent in stead of UDP.</returns>
        private bool HandleLocationUDPError(string TLMMessage)
        {
//#if DEBUG
//            if (_log.IsDebugEnabled) _log.Debug("HandleLocationUDPError()");
//#endif

            _senterrorcount++;

            // SMS fallback
            if (_senterrorcount > Configuration.Instance().FailCountSMS)
            {
                if (_sentsmscount<Configuration.Instance().MaxSMS)
                {
                    // Only send real SMS in RELEASE mode

                   // MobileShared.TAPI.SMS.SendSMS(TLMMessage, Configuration.Instance().VMN);
                    try
                    {
                        Microsoft.WindowsMobile.PocketOutlook.SmsMessage msg = new Microsoft.WindowsMobile.PocketOutlook.SmsMessage(Configuration.Instance().VMN2, TLMMessage);
                        msg.RequestDeliveryReport = true;
                        msg.Send();

                        _sentsmscount++;
                    }
                    catch (Exception ex)
                    {

                        return true;
                    }

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