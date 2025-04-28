using System;

namespace MobileShared
{
    /// <summary>
    /// Summary description for Configuration.
    /// </summary>
    public sealed class Configuration
    {
        private static Configuration _instance = null;
        private static object _instanceLock = new object();

        static Settings _Settings = new Settings();

        public Configuration() { }

        /// <summary>
        /// Provides a reference to the singleton
        /// instance of the ConfigurationManager class.
        /// </summary>
        /// <returns>Singleton instance of the
        /// Configuration class.</returns>
        public static Configuration Instance()
        {
            if (_instance == null)
            {
                lock (_instanceLock)
                {
                    if (_instance == null)
                    {
                        _instance = new Configuration();
                    }
                }
            }
            return _instance;
        }

        /// <summary>
        /// Read / write application settings.
        /// </summary>
        public Settings Settings
        {
            get { return _Settings; }
        }

        #region Public properties
        public string Server
        {
            get { return _Settings.GetString("Server"); }
            set
            {
                _Settings.SetValue("Server", value);
            }
        }

        public string AppVersion
        {
            get { return _Settings.GetString("Version"); }
            set
            {
                _Settings.SetValue("Version", value);
            }
        }

        public string Email
        {
            get { return _Settings.GetString("Email"); }
            set
            {
                _Settings.SetValue("Email", value);
            }
        }

        public string Password
        {
            get { return SimpleEncrypt.Decrypt(_Settings.GetString("Password")); }
            set
            {
                _Settings.SetValue("Password", SimpleEncrypt.Encrypt(value));
            }
        }

        public int Port
        {
            get { return _Settings.GetInt("Port"); }
            set
            {
                _Settings.SetValue("Port", value);
            }
        }

        public bool AutoStart
        {
            get { return _Settings.GetBool("AutoStart"); }
            set
            {
                _Settings.SetValue("AutoStart", value);
            }
        }

        public bool AutoPanic
        {
            get { return _Settings.GetBool("AutoPanic"); }
            set
            {
                _Settings.SetValue("AutoPanic", value);
            }
        }

        public bool AutoConnect
        {
            get { return _Settings.GetBool("AutoConnect"); }
            set
            {
                _Settings.SetValue("AutoConnect", value);
            }
        }

        public bool ConnectOnce
        {
            get { return _Settings.GetBool("ConnectOnce"); }
            set
            {
                _Settings.SetValue("ConnectOnce", value);
            }
        }

        public int ApplicationID
        {
            get { return _Settings.GetInt("ApplicationID"); }
            set
            {
                _Settings.SetValue("ApplicationID", value);
            }
        }

        public int DomesticInterval
        {
            get { return _Settings.GetInt("DomesticInterval"); }
            set
            {
                _Settings.SetValue("DomesticInterval", value);
            }
        }

        public int InternationalInterval
        {
            get { return _Settings.GetInt("InternationalInterval"); }
            set
            {
                _Settings.SetValue("InternationalInterval", value);
            }
        }

        public int NotMovingDelay
        {
            get { return _Settings.GetInt("NotMovingDelay"); }
            set
            {
                _Settings.SetValue("NotMovingDelay", value);
            }
        }

        public bool DisconnectAfterPlot
        {
            get { return _Settings.GetBool("DisconnectAfterPlot"); }
            set
            {
                _Settings.SetValue("DisconnectAfterPlot", value);
            }
        }

        public bool SettingsVisible
        {
            get { return _Settings.GetBool("SettingsVisible"); }
            set
            {
                _Settings.SetValue("SettingsVisible", value);
            }
        }

        public bool WizardVisible
        {
            get { return _Settings.GetBool("WizardVisible"); }
            set
            {
                _Settings.SetValue("WizardVisible", value);
            }
        }

        public string SettingsPassword
        {
            get { return _Settings.GetString("SettingsPassword"); }
            set
            {
                _Settings.SetValue("SettingsPassword", value);
            }
        }

        public string VMN
        {
            get { return _Settings.GetString("VMN"); }
            set
            {
                _Settings.SetValue("VMN", value);
            }
        }

        public string VMN2
        {
            get { return _Settings.GetString("VMN2"); }
        }

        public int FailCountSMS
        {
            get { return _Settings.GetInt("FailCountSMS"); }
            set
            {
                _Settings.SetValue("FailCountSMS", value);
            }
        }

        public int MinBatLevel
        {
            get { return _Settings.GetInt("MinBatLevel"); }
            set
            {
                _Settings.SetValue("MinBatLevel", value);
            }
        }

        public bool GPS
        {
            get { return _Settings.GetBool("GPS"); }
            set
            {
                _Settings.SetValue("GPS", value);
            }
        }

        public int GPSMarginInMeters
        {
            get { return _Settings.GetInt("GPSMarginInMeters"); }
            set
            {
                _Settings.SetValue("GPSMarginInMeters", value);
            }
        }

        public int DisconnectGPSAfterPlot
        {
            get { return _Settings.GetInt("DisconnectGPSAfterPlot"); }
            set
            {
                _Settings.SetValue("DisconnectGPSAfterPlot", value);
            }
        }

        public int DumpQueueInterval
        {
            get { return _Settings.GetInt("DumpQueueInterval"); }
            set
            {
                _Settings.SetValue("DumpQueueInterval", value);
            }
        }

        public int DumpQueueSize
        {
            get { return _Settings.GetInt("DumpQueueSize"); }
            set
            {
                _Settings.SetValue("DumpQueueSize", value);
            }
        }

        public int PanicLocationsToSent
        {
            get { return _Settings.GetInt("PanicLocationsToSent"); }
            set
            {
                _Settings.SetValue("PanicLocationsToSent", value);
            }
        }        

        public bool PositionExchange
        {
            get { return _Settings.GetBool("PositionExchange"); }
            set
            {
                _Settings.SetValue("PositionExchange", value);
            }
        }

        public int SpeedAlertInKM
        {
            get { return _Settings.GetInt("SpeedAlertInKM"); }
            set
            {
                _Settings.SetValue("SpeedAlertInKM", value);
            }
        }

        public int MaxSMS
        {
            get { return _Settings.GetInt("MaxSMS"); }
            set
            {
                _Settings.SetValue("MaxSMS", value);
            }
        }    
        #endregion
    }
}
