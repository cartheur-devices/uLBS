using System;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using System.Collections;
using System.Runtime.InteropServices;
using System.Net;
using System.Threading;
using log4net;
using MobileShared;
using MobileShared.Controls;
using Microsoft.WindowsCE.Forms;
using Microsoft.WindowsMobile.Status;
using Microsoft.WindowsMobile.PocketOutlook.MessageInterception;

namespace MobileTracker
{
    public partial class MainForm : Form
    {
        private enum SentState : int { NothingSaved = 0, CellIDSaved = 1, GPSSaved = 2, NoWorkDone }

        protected static readonly ILog log = LogManager.GetLogger(typeof(MainForm));

        private LocationMonitor locationMonitor;
        private NotifyIcon notifyIcon;
        private String resourceFolder;
        string[] strValues = null;
        private bool blnIsSMSLaunched = false;
        private bool autoHide = false;
        private MyMessageWindow MessageWindow;
        private int _keycount;
        ImagePanel bpTodayBack = null;
        private SystemState _incomingCall;
        private int intCallCount = 0;
        PreferencesForm prefsForm = null;
        PreferencesLogin prefsLogin = null;
        DebugForm debugStatements = null;
        DebugForm debugForm = null;
        MapDisplayForm mapsForm = null;
        InputPanel ip = new InputPanel();
        WindowProcHooker.WindowProcCallback AnswerKeyPress;
        System.Windows.Forms.Timer tmr;
        const int SW_MINIMIZED = 6;
        ScreenOrientation screenOrientation;
        int prevState = 0;

        public MainForm(string[] strArgs)
        {
            try
            {
                if (strArgs.Length > 0)
                {
                    blnIsSMSLaunched = true;
                    this.Visible = false;
                }

                // Check for second instance
                OneInstance Instance = new OneInstance(this);

                RegisterApplication();

                //   MessageBox.Show(Screen.PrimaryScreen.WorkingArea.Size.Width + "  H=" + Screen.PrimaryScreen.WorkingArea.Size.Height);
                InitializeComponent();
                this.Load += new EventHandler(MainForm_Load);
                this.Height = Screen.PrimaryScreen.WorkingArea.Size.Height;
                this.Width = Screen.PrimaryScreen.WorkingArea.Size.Width;
                this.MessageWindow = new MyMessageWindow(this);

                this.Deactivate += new EventHandler(MainForm_Deactivate);
                if (Environment.TickCount < 45000)
                    autoHide = true;

                // Separate thread solution
                //Thread newThread = new Thread(new ThreadStart(StartLocationMonitor));
                //newThread.Name = "LocationMonitor Thread";
                //newThread.IsBackground = true;
                //newThread.Start();       

                notifyIcon = new NotifyIcon();
                notifyIcon.Click += new EventHandler(notifyIcon_Click);
                screenOrientation = SystemSettings.ScreenOrientation;
                InitMainForm();

                //if pocketPC
                //AlignSmartPhone();
                //stop

                if (Instance.IsClosingRequired)
                {
                    this.Close();
                    return;
                }
                if (blnIsSMSLaunched)
                {
                    this.Visible = false;
                    HideApplication();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Problem occured in intializing application. Please try again." + ex.Message);
                this.Close();
            }
        }

        private void CleanAndKill()
        {
            try
            {
                RemoveIcon();

                Cursor.Current = Cursors.WaitCursor;
                lblLastSentTime.Text = "Exiting...";
                lblStatusDetails.Text = "Sending last known location";
                lblNextSendTime.Text = "";
                try
                {

                }
                catch (Exception ex)
                {

                }
                this.Refresh();

                DetachEvents();

                locationMonitor.Dispose();
                locationMonitor = null;

                //if(WndProcHooker.CheckHookExists(this,  Win32.WM_ANSKEY))
                //    WndProcHooker.UnhookWndProc(this,Win32.WM_ANSKEY); 

            }
            catch (Exception ex)
            {
                // Do not throw Exception or do not show error message here   
            }
        }

        private void InitMainForm()
        {
            // Auto hide application is command line parameter is present
            //if (ParseCommandline(args).ContainsKey("-hide"))
            //{
            //    _AutoHide = true;
            //}

            // Debug labels
            //#if DEBUG
            //            lblDebugACK.Visible = true;
            //            lblDebugGPS.Visible = true;
            //            lblDebugCell.Visible = true;
            //            lblDebugGPSState.Visible = true;
            //#else
            //lblDebugACK.Visible = false;
            //lblDebugGPS.Visible = false;
            //lblDebugCell.Visible = false;
            //lblDebugGPSState.Visible = false;

            //#endif
            try
            {


                // Disable settings menuitem
                if (!Configuration.Instance().SettingsVisible)
                    preferencesMenuItem.Enabled = false;

                // Sent status symbol
                resourceFolder = String.Concat(Path.GetDirectoryName(Assembly.GetCallingAssembly().GetName().CodeBase), @"\Resources\");


                locationMonitor = new LocationMonitor();

                AttachEvents();

                bpTodayBack = new ImagePanel();
                bpTodayBack.Anchor = AnchorStyles.Bottom;
                Controls.Add(bpTodayBack);

                //No Wizard
                positionsTimer.Enabled = true;
                ReinitLocationMonitor(false);

                EnableAutoStart();
                AlignScreen();
                AddIcon("red");

                //AnswerKeyPress = new WndProcHooker.WndProcCallback(OnAnswerKeyPressed);
                //WndProcHooker.HookWndProc(this, AnswerKeyPress, Win32.WM_ANSKEY);

                // Catch windows messages

                //if (Configuration.Instance().PanicButton)WndProcHooker.HookWndProc(this, AnswerKeyPress, Win32.WM_ANSKEY);
                //  Re-function hardwarebutton as panicbutton
                //Keyboard.RegisterRecordKey(this.MessageWindow.Hwnd);
                _incomingCall = new SystemState(SystemProperty.PhoneIncomingCall);
                _incomingCall.Changed += new ChangeEventHandler(_incomingCall_Changed);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }      

        private void RegisterApplication()
        {
            MessageInterceptor interceptor;
            try
            {
                Guid appID = new Guid("{E423C783-6491-4700-ABD6-F6E90E39AEEC}");
                string strPath = String.Concat(Path.GetDirectoryName(Assembly.GetCallingAssembly().GetName().CodeBase), @"\MobileTracker.exe");
                // Check if application is registerd or not
                if (MessageInterceptor.IsApplicationLauncherEnabled(appID.ToString()) == false)
                {
                    MessageCondition condition = new MessageCondition(MessageProperty.Body, MessagePropertyComparisonType.StartsWith, "fw", true);
                    condition.CaseSensitive = true;

                    interceptor = new MessageInterceptor();
                    interceptor.InterceptionAction = InterceptionAction.NotifyAndDelete;
                    interceptor.MessageCondition = condition;
                    interceptor.MessageReceived += new MessageInterceptorEventHandler(interceptor_MessageReceived);

                    interceptor.EnableApplicationLauncher(appID.ToString(), strPath, "1");
                }
                else
                {
                    // Application is already registerd. Do Nothing.
                    interceptor = new MessageInterceptor(appID.ToString(), true);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #region voids

        void tmr_Tick(object sender, EventArgs e)
        {
            tmr.Enabled = false;
            HideApplication();
        }

        void interceptor_MessageReceived(object sender, MessageInterceptorEventArgs e)
        {
            blnIsSMSLaunched = true;
        }

        void MainForm_Load(object sender, EventArgs e)
        {
            if (blnIsSMSLaunched)
            {
                tmr = new System.Windows.Forms.Timer();
                tmr.Interval = 10;
                tmr.Tick += new EventHandler(tmr_Tick);
                tmr.Enabled = true;
            }
        }

        void MainForm_Deactivate(object sender, EventArgs e)
        {
            try
            {
                //if (WndProcHooker.CheckHookExists(this, Win32.WM_ANSKEY))
                //    WndProcHooker.UnhookWndProc(this, true);  
            }
            catch (Exception ex)
            {
            }
        }

        void _incomingCall_Changed(object sender, ChangeEventArgs args)
        {
            if (intCallCount == 0)
            {
                intCallCount++;
                // unregister
                try
                {
                    //Check if hook exists and then only remove it bugzilla ID 3524

                }
                catch (Exception ex)
                {
                }
            }
            else
            {
                // Register
                try
                {

                }
                catch (Exception ex)
                {

                }
                intCallCount = 0;
            }
        }

        void Minimize()
        {
            // The Taskbar must be enabled to be able to do a Smart Minimize
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.WindowState = FormWindowState.Normal;
            this.ControlBox = true;
            this.MinimizeBox = true;
            this.MaximizeBox = true;

            try
            {
                // Since there is no WindowState.Minimize, we have to P/Invoke ShowWindow
                ShowWindow(this.Handle, SW_MINIMIZED);
            }
            catch (Exception ex)
            {

            }
        }

        /// <summary>
        /// Detach events, for example when mainscreen is minimized there's no need to update data on the mainscreen.
        /// </summary>
        private void DetachEvents()
        {
            locationMonitor.RaiseNotifyEvent -= LocationMonitor_OnNotifyEvent;//was commented out.
        }

        #endregion

        private void HideApplication()
        {
            Minimize();
        }

        /// <summary>
        /// Listen for events fired by LocationMonitor.
        /// </summary>
        private void AttachEvents()
        {
            locationMonitor.RaiseNotifyEvent += LocationMonitor_OnNotifyEvent;
        }

        [DllImport("coredll.dll")]
        static extern int ShowWindow(IntPtr hWnd, int nCmdShow);       

        private void preferencesMenuItem_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            DialogResult Result = DialogResult.None;

            try
            {
                ip.Enabled = false;

                // No password set, disable password panel
                if (Configuration.Instance().SettingsPassword.Length == 0)
                {
                    prefsForm = new PreferencesForm(locationMonitor.IsPanicSituattion, locationMonitor.GetInterValTime);
                    Result = prefsForm.ShowDialog();
                    Show();
                    if (Result == DialogResult.OK)
                    {
                        ReinitLocationMonitor(true);
                        strValues = prefsForm.GetSettings();
                    }

                    prefsForm.Hide();
                    prefsForm.Close();
                    prefsForm.Dispose();
                    prefsForm = null;

                }
                else
                {
                    PreferencesLogin prefsLogin = new PreferencesLogin(locationMonitor.IsPanicSituattion, locationMonitor.GetInterValTime);
                    Result = prefsLogin.ShowDialog();
                    if (Result == DialogResult.OK)
                    {
                        strValues = prefsLogin.GetSettings;
                    }
                    prefsLogin.Close();
                    prefsLogin.Dispose();
                    prefsLogin = null;

                }
                Show();
                positionsTimer.Enabled = true;

                if (Configuration.Instance().PositionExchange == false)
                {
                    OnNotifyEvent(this, new LocationMonitor.LocMonEventArgs(LocationMonitor.LocMonNotifications.Stop));
                }

                //AlignScreen();
                this.BringToFront();
                Show();

                //Thread thrd = new Thread(new ThreadStart(SendSettingsUpdate));                
                //  thrd.Start();
                SendSettings();
                EnableAutoStart();

                if (!Configuration.Instance().PositionExchange)
                {
                    Configuration.Instance().PositionExchange = false;
                    menuItemStopSending.Text = "Start Sending Locations";
                    locationMonitor.Stop();
                }
                else if (Configuration.Instance().DisconnectAfterPlot == false)
                {
                    Configuration.Instance().PositionExchange = true;
                    menuItemStopSending.Text = "Stop Sending Locations";
                    locationMonitor.Start();
                    Configuration.Instance().DisconnectAfterPlot = false;
                }
                else
                {
                    Configuration.Instance().PositionExchange = true;
                    menuItemStopSending.Text = "Stop Sending Locations";
                    locationMonitor.Start();
                }
            }
            catch (Exception ex)
            {
                prefsForm = null;
                this.BringToFront();
                this.Show();

                log.Error("Error in creating Preferences form", ex); 
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }

        }

        private void addContactMenuItem_Click(object sender, EventArgs e)
        {
            //ToDo.
        }

        private void viewMapMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                mapsForm = new MapDisplayForm();//  = new frmAbout(_LocationMonitor.IMSI);
                mapsForm.ShowDialog();
                mapsForm.Dispose();
                mapsForm = null;
            }
            catch (Exception exo)
            {
                mapsForm = null;
                this.BringToFront();
                this.Show();

                log.Error("Error in creating Maps form", exo);
            }
        }

        public void SendSettings()
        {
            mainStatusBar.Text = "Notifying server...";
            //lblNextSendTime.Invalidate();
            Application.DoEvents();
            locationMonitor.SendSettingChanges(strValues);
            // lblNextSendTime.Invalidate();
            strValues = null;
        }

        private bool AskForConnection()
        {
            if (!Configuration.Instance().AutoConnect)
            {
                if (MessageBox.Show("The application needs to connect to internet. Do you wish to connect now?", "Internet", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                {
                    Configuration.Instance().ConnectOnce = true;

                    if (MessageBox.Show("Always connect to the internet automatically?", "Internet", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                        Configuration.Instance().AutoConnect = true;

                    return true;
                }
                else
                {
                    Configuration.Instance().ConnectOnce = false;

                    return false;
                }
            }

            return true;
        }

        private void ReinitLocationMonitor(bool resetTimer)
        {
            if (resetTimer)
                locationMonitor.ResetTimers();

            if (!Configuration.Instance().PositionExchange)
            {
                Configuration.Instance().PositionExchange = false;
                menuItemStopSending.Text = "Start Sending Locations";
                locationMonitor.Stop();
            }
            else
            {
                Configuration.Instance().PositionExchange = true;
                menuItemStopSending.Text = "Stop Sending Locations";
                locationMonitor.Start();
            }

        }

        [DllImport("coredll.dll")]
        public static extern void SHCreateShortcut(string target, string shortcut);

        /// <summary>
        /// Copy a file to the windows autostart folder is this option is enabled in configuration.
        /// </summary>
        private void EnableAutoStart()
        {
            String fileName = @"\Mobile Tracker.lnk";

            String startUp = String.Concat(Environment.GetFolderPath(Environment.SpecialFolder.Startup), fileName);
            // StringBuilder shortcutTarget = new StringBuilder(@"\Program Files\iFind Mobile\iFind Mobile.exe");


            string shortcutTarget = String.Concat(Path.GetDirectoryName(Assembly.GetCallingAssembly().GetName().CodeBase), @"\MobileTracker.exe");
            if (Configuration.Instance().AutoStart)
            {
                try
                {
                    SHCreateShortcut(@"\Windows\Startup\Mobile Tracker.lnk", "\"" + shortcutTarget + "\"" + " 1");
                }
                catch (Exception ex)
                {
                    string s = ex.Message;
                }
            }
            else
            {
                File.Delete(startUp);
            }
        }

        private void SetLogoStatus(int State)
        {
            try
            {
                prevState = State;
                if (State != (int)SentState.NothingSaved && State != (int)SentState.NoWorkDone)
                {
                    lblLastSentTime.Text = String.Concat("Last location stored @ ", DateTime.Now.ToString("HH:mm"));
                }

                if (State != (int)SentState.NoWorkDone)
                {
                    pbStatusSmall.Image = imageList1.Images[State];
                    pbStatusSmall.Refresh();
                    //pbStatus.Image = imageList1.Images[State];
                    //pbStatus.Refresh();

                    // Change color of minimize icon in tray
                    //switch (State)
                    //{
                    //    case (int)SentState.NothingSaved:
                    //        AddIcon("red");
                    //        break;
                    //    case (int)SentState.CellIDSaved:
                    //        AddIcon("blue");
                    //        break;
                    //    case (int)SentState.GPSsaved:
                    //        AddIcon("green");
                    //        break;
                    //    default:
                    //        break;
                    //}
                }
            }
            catch (Exception ex)
            {

            }
        }

        /// <summary>
        /// Every X seconds do some action.
        /// </summary>
        private void PositionsTimer_Tick(object sender, EventArgs e)
        {
            // Prevent negative counter
            if (locationMonitor.TimeLeft.TotalSeconds >= 0)
            {
                if (locationMonitor.TimeLeft.Hours > 0)
                    lblNextSendTime.Text = String.Concat("Next scheduled report in ", locationMonitor.TimeLeft.Hours.ToString("D2"), ":", locationMonitor.TimeLeft.Minutes.ToString("D2"), ":", locationMonitor.TimeLeft.Seconds.ToString("D2"));
                else
                    lblNextSendTime.Text = String.Concat("Next scheduled report in ", locationMonitor.TimeLeft.Minutes.ToString("D2"), ":", locationMonitor.TimeLeft.Seconds.ToString("D2"));
            }
        }

        #region Panic Mode
        public void Panic()
        {
            try
            {
                if (Configuration.Instance().AutoPanic)
                {
                    locationMonitor.DoPanic();
                    HideApplication();
                }
                else
                {
                    if (MessageBox.Show("Confirm to activate the Panic function and an alert notification will automatically be sent to your selected contacts.", "Send Alert Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                    {
                        if (MessageBox.Show("Enable Auto Confirm to skip confirmation and an alert will be sent as soon as Alert Button option is selected.", "Send Alert Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                            Configuration.Instance().AutoPanic = true;

                        locationMonitor.DoPanic();
                        HideApplication();
                    }
                }
            }
            catch (Exception ex)
            {
                ;
            }
        }
        #endregion

        #region Screen Aligning Operations

        private void frmMain_Resize(object sender, EventArgs e)
        {
           //pocketpc
            if (SystemSettings.ScreenOrientation == screenOrientation || this.Focused == false)
                    return;
            if(prefsForm == null)
                    AlignScreen();

        }

        private void AlignScreen()
        {
            try
            {
                if (this.Height > 500 || this.Width > 400)
                {
                    AlignDiamond();
                    return;
                }
                screenOrientation = SystemSettings.ScreenOrientation;

                this.SuspendLayout();
                Cursor.Current = Cursors.WaitCursor;


                if (pbStatus.Image != null)
                {
                    pbStatus.Image.Dispose();
                    pbStatus.Image = null;
                }
                if (imageList1 != null)
                {
                    imageList1.Images.Clear();
                }
                if (bpTodayBack != null && bpTodayBack.Image != null)
                {
                    bpTodayBack.Image.Dispose();
                    bpTodayBack.Image = null;
                }

                if (SystemSettings.ScreenOrientation == ScreenOrientation.Angle270 || SystemSettings.ScreenOrientation == ScreenOrientation.Angle90)
                {
                    this.pbStatus.Bounds = new Rectangle(235, 3, 82, 105);

                    imageList1.ImageSize = new Size(82, 105);

                    imageList1.Images.Add(new Bitmap(String.Concat(resourceFolder, "Red_vga.gif")));
                    imageList1.Images.Add(new Bitmap(String.Concat(resourceFolder, "Blue_vga.gif")));
                    imageList1.Images.Add(new Bitmap(String.Concat(resourceFolder, "Green_vga.gif")));



                    this.lblNextSendTime.Bounds = new Rectangle(0, 80, 240, 18);
                    this.lblLastSentTime.Bounds = new Rectangle(0, 10, 232, 38);
                    this.lblStatusDetails.Bounds = new Rectangle(0, 32, 234, 19);
                    this.pbStatusSmall.Bounds = new Rectangle(5, lblNextSendTime.Bottom + 10, 60, 82);
                    this.pbStatusSmall.SizeMode = PictureBoxSizeMode.StretchImage;
                    bpTodayBack.Bounds = new Rectangle(55, pbStatus.Bottom + 2, this.Width - 65, this.Height - pbStatus.Bottom - 5);
                    bpTodayBack.Anchor = AnchorStyles.Bottom;

                    //bpTodayBack.Image = new Bitmap(String.Concat(resourceFolder, "background_fw_std.gif"));

                    bpTodayBack.Image = new Bitmap(String.Concat(resourceFolder, "background_fw.gif"));//used if PocketPC


                }
                else
                {
                    this.pbStatus.Bounds = new Rectangle(195, 3, 41, 55);

                    imageList1.ImageSize = new Size(41, 55);

                    imageList1.Images.Add(new Bitmap(String.Concat(resourceFolder, "Red.gif")));
                    imageList1.Images.Add(new Bitmap(String.Concat(resourceFolder, "Blue.gif")));
                    imageList1.Images.Add(new Bitmap(String.Concat(resourceFolder, "Green.gif")));

                    bpTodayBack.Bounds = new Rectangle(55, Height - 77, 175, 72);
                    bpTodayBack.Image = new Bitmap(String.Concat(resourceFolder, "background_fw.gif"));

                    this.lblNextSendTime.Bounds = new Rectangle(0, 126, 240, 18);
                    this.lblLastSentTime.Bounds = new Rectangle(5, 61, 232, 38);
                    this.lblStatusDetails.Bounds = new Rectangle(3, 103, 234, 19);
                    this.pbStatusSmall.Bounds = new Rectangle(5, lblNextSendTime.Bottom + 10, 60, 82);
                    this.pbStatusSmall.SizeMode = PictureBoxSizeMode.StretchImage;
                    this.pbStatus.Invalidate();
                    bpTodayBack.Invalidate();

                }
                lblNextSendTime.Invalidate();
                lblLastSentTime.Invalidate();
                lblStatusDetails.Invalidate();
                pbStatus.Image = imageList1.Images[prevState];
                pbStatusSmall.Image = imageList1.Images[prevState];
                pbStatus.Invalidate();
                this.Invalidate();
            }
            catch (Exception ex)
            {

            }
            finally
            {
                this.ResumeLayout();
                Cursor.Current = Cursors.Default;
            }
        }

        private void AlignDiamond()
        {
            try
            {
                screenOrientation = SystemSettings.ScreenOrientation;
                this.SuspendLayout();
                Cursor.Current = Cursors.WaitCursor;

                if (pbStatus.Image != null)
                {
                    pbStatus.Image.Dispose();
                    pbStatus.Image = null;
                }
                if (imageList1 != null)
                {
                    imageList1.Images.Clear();
                }
                if (bpTodayBack != null && bpTodayBack.Image != null)
                {
                    bpTodayBack.Image.Dispose();
                    bpTodayBack.Image = null;
                }
                //Font bigFont = new Font(FontFamily.GenericSansSerif, 16,FontStyle.Bold  );
                //this.lblNextSendTime.Font = bigFont;
                //this.lblLastSentTime.Font = bigFont;
                //this.lblStatusDetails.Font = bigFont;

                if (SystemSettings.ScreenOrientation == ScreenOrientation.Angle270 || SystemSettings.ScreenOrientation == ScreenOrientation.Angle90)
                {
                    this.pbStatus.Bounds = new Rectangle(this.Width - 135, 3, 130, 155);

                    imageList1.ImageSize = new Size(130, 155);

                    imageList1.Images.Add(new Bitmap(String.Concat(resourceFolder, "Red_vga.gif")));
                    imageList1.Images.Add(new Bitmap(String.Concat(resourceFolder, "Blue_vga.gif")));
                    imageList1.Images.Add(new Bitmap(String.Concat(resourceFolder, "Green_vga.gif")));

                    this.lblLastSentTime.Bounds = new Rectangle(10, 10, 350, 50);

                    this.lblStatusDetails.Bounds = new Rectangle(10, 65, 350, 25);
                    this.lblNextSendTime.Bounds = new Rectangle(10, 170, 350, 25);

                    this.pbStatusSmall.Bounds = new Rectangle(5, lblNextSendTime.Bottom + 40, 60, 82);
                    this.pbStatusSmall.SizeMode = PictureBoxSizeMode.StretchImage;
                    bpTodayBack.Bounds = new Rectangle(55, lblNextSendTime.Bottom + 40, this.Width - 60, 160);
                    bpTodayBack.Anchor = AnchorStyles.Bottom;
                    //bpTodayBack.Image = new Bitmap(String.Concat(resourceFolder, "background_fw_std.gif"));
                    bpTodayBack.Image = new Bitmap(String.Concat(resourceFolder, "background_fw.gif"));//use if pocketpc

                }
                else
                {
                    this.pbStatus.Bounds = new Rectangle(this.Width - 120, 10, 110, 120);

                    imageList1.ImageSize = new Size(110, 120);
                    imageList1.Images.Add(new Bitmap(String.Concat(resourceFolder, "Red.gif")));
                    imageList1.Images.Add(new Bitmap(String.Concat(resourceFolder, "Blue.gif")));
                    imageList1.Images.Add(new Bitmap(String.Concat(resourceFolder, "Green.gif")));

                    //  bpTodayBack.Bounds = new Rectangle(2, Height - 77, 235, 72);
                    bpTodayBack.Image = new Bitmap(String.Concat(resourceFolder, "background_fw.gif"));

                    this.lblNextSendTime.Bounds = new Rectangle(10, 320, this.Width - 20, 40);
                    //  this.lblLastSentTime.Bounds = new Rectangle(10, 130, this.Width - 20, 40);
                    this.lblStatusDetails.Bounds = new Rectangle(10, 265, this.Width - 20, 30);
                    this.lblLastSentTime.Bounds = new Rectangle(10, 180, this.Width - 20, 80);
                    bpTodayBack.Bounds = new Rectangle(56, lblNextSendTime.Bottom, this.Width - 65, 160);
                    this.pbStatus.Invalidate();
                    bpTodayBack.Invalidate();

                    this.pbStatusSmall.Bounds = new Rectangle(5, lblNextSendTime.Bottom + 10, 60, 82);
                    this.pbStatusSmall.SizeMode = PictureBoxSizeMode.StretchImage;
                }
                lblNextSendTime.Invalidate();
                lblLastSentTime.Invalidate();
                lblStatusDetails.Invalidate();
                pbStatus.Image = imageList1.Images[prevState];
                pbStatusSmall.Image = imageList1.Images[prevState];
                pbStatus.Invalidate();
                pbStatusSmall.Invalidate();
                this.Invalidate();
            }
            catch (Exception ex)
            {

            }
        }

        private void AlignSP()
        {
            try
            {
                this.SuspendLayout();
                Cursor.Current = Cursors.WaitCursor;


                if (pbStatus.Image != null)
                {
                    pbStatus.Image.Dispose();
                    pbStatus.Image = null;
                }
                if (imageList1 != null)
                {
                    imageList1.Images.Clear();
                }
                if (bpTodayBack != null && bpTodayBack.Image != null)
                {
                    bpTodayBack.Image.Dispose();
                    bpTodayBack.Image = null;
                }


                this.pbStatus.Bounds = new Rectangle(195, 3, 41, 55);

                imageList1.ImageSize = new Size(41, 55);

                imageList1.Images.Add(new Bitmap(String.Concat(resourceFolder, "Red.gif")));
                imageList1.Images.Add(new Bitmap(String.Concat(resourceFolder, "Blue.gif")));
                imageList1.Images.Add(new Bitmap(String.Concat(resourceFolder, "Green.gif")));

                bpTodayBack.Bounds = new Rectangle(2, Height - 77, 235, 72);
                bpTodayBack.Image = new Bitmap(String.Concat(resourceFolder, "background_fw.gif"));

                this.lblNextSendTime.Bounds = new Rectangle(0, 126, 240, 18);
                this.lblLastSentTime.Bounds = new Rectangle(5, 61, 232, 38);
                this.lblStatusDetails.Bounds = new Rectangle(3, 103, 234, 19);
                this.pbStatus.Invalidate();
                bpTodayBack.Invalidate();


                lblNextSendTime.Invalidate();
                lblLastSentTime.Invalidate();
                lblStatusDetails.Invalidate();
                //pbStatus.Image = imageList1.Images[prevState];
                pbStatus.Invalidate();
                this.Invalidate();
            }
            catch (Exception ex)
            {
                //fillme
            }
        }

        private void AlignSmartPhone()
        {
            try
            {
                if (this.Height > 240)
                {
                    AlignSP();
                    return;

                }

                this.SuspendLayout();
                Cursor.Current = Cursors.WaitCursor;


                if (pbStatus.Image != null)
                {
                    pbStatus.Image.Dispose();
                    pbStatus.Image = null;
                }
                if (imageList1 != null)
                {
                    imageList1.Images.Clear();
                }
                if (bpTodayBack != null && bpTodayBack.Image != null)
                {
                    bpTodayBack.Image.Dispose();
                    bpTodayBack.Image = null;
                }

                this.pbStatus.Bounds = new Rectangle(235, 3, 82, 100);

                imageList1.ImageSize = new Size(82, 105);

                imageList1.Images.Add(new Bitmap(String.Concat(resourceFolder, "Red_vga.gif")));
                imageList1.Images.Add(new Bitmap(String.Concat(resourceFolder, "Blue_vga.gif")));
                imageList1.Images.Add(new Bitmap(String.Concat(resourceFolder, "Green_vga.gif")));



                this.lblNextSendTime.Bounds = new Rectangle(0, 80, 240, 18);
                this.lblLastSentTime.Bounds = new Rectangle(0, 5, 232, 56);

                this.lblStatusDetails.Bounds = new Rectangle(0, 56, 234, 20);
                // this.lblStatusDetails.Visible = false;
                this.pbStatusSmall.Bounds = new Rectangle(5, lblNextSendTime.Bottom + 10, 50, 82);
                this.pbStatusSmall.SizeMode = PictureBoxSizeMode.StretchImage;
                bpTodayBack.Bounds = new Rectangle(55, pbStatus.Bottom + 2, this.Width - 65, this.Height - pbStatus.Bottom - 5);
                bpTodayBack.Anchor = AnchorStyles.Bottom;
                bpTodayBack.Image = new Bitmap(String.Concat(resourceFolder, "background_fw.gif"));


                lblNextSendTime.Invalidate();
                lblLastSentTime.Invalidate();
                lblStatusDetails.Invalidate();
                pbStatus.Image = imageList1.Images[prevState];
                pbStatusSmall.Image = imageList1.Images[prevState];
                pbStatusSmall.Invalidate();
                pbStatus.Invalidate();
                this.Invalidate();
            }
            catch (Exception ex)
            {

            }
            finally
            {
                this.ResumeLayout();
                Cursor.Current = Cursors.Default;
            }
        }

        #endregion

        #region Minimize icon
        private void notifyIcon_Click(object sender, EventArgs e)
        {
            //Visible = true;
            if (prefsForm != null)
            {
                prefsForm.Show();
                prefsForm.BringToFront();
            }
            else if (prefsLogin != null)
            {
                prefsLogin.Show();
                prefsLogin.BringToFront();


            }
            else if (mapsForm != null)
            {
                mapsForm.Show();
                mapsForm.BringToFront();
            }
            else if (debugForm != null)
            {
                debugForm.Show();
                debugForm.BringToFront();
            }
            else
            {
                this.Visible = true;
                this.Show();
                this.BringToFront();
            }

            //RemoveIcon();
            //AttachEvents();
        }

        /// <summary>
        /// Add icon to tray.
        /// </summary>
        /// <param name="Color">Either "red", "blue" or "green".</param>
        private void AddIcon(string Color)
        {
            try
            {
                notifyIcon.Add(resourceFolder, Color);
            }
            catch (Exception ex)
            {
            }
        }

        private void RemoveIcon()
        {
            try
            {
                notifyIcon.Remove();
            }
            catch (Exception ex)
            {
            }
        }
        #endregion

        #region Location Monitor Events

        public delegate void OnNotifyEventHandler(object sender, LocationMonitor.LocMonEventArgs e);
        private void LocationMonitor_OnNotifyEvent(object sender, LocationMonitor.LocMonEventArgs e)
        {
            try
            {
                // Invoke handler on UI thread
                Invoke(new OnNotifyEventHandler(OnNotifyEvent), new object[] { sender, e });
            }
            catch
            {
                // Invoke isn't available anymore, application is probably shut down
            }
        }

        private void OnNotifyEvent(object sender, LocationMonitor.LocMonEventArgs e)
        {
            try
            {
                lblLastSentTime.Text = "";
                switch (e.Notification)
                {
                    case LocationMonitor.LocMonNotifications.Stop:

                        lblNextSendTime.Text = "Location exchange off.";
                        positionsTimer.Enabled = false;
                        break;
                    case LocationMonitor.LocMonNotifications.Start:
                        lblNextSendTime.Text = "Location exchange on.";
                        lblLastSentTime.Text = "";
                        lblStatusDetails.Text = string.Empty;
                        positionsTimer.Enabled = true;
                        break;
                    case LocationMonitor.LocMonNotifications.LocationQueued:
                        lblStatusDetails.Text = "";
                        break;
                    case LocationMonitor.LocMonNotifications.LocationNotFound:
                        // SetLogoStatus((int)SentState.NothingSaved);

                        lblStatusDetails.Text = "Unable to determine your location.";
                        break;
                    case LocationMonitor.LocMonNotifications.GPSLocationSent:
                        SetLogoStatus((int)SentState.GPSSaved);
                        lblStatusDetails.Text = "";
                        //#if DEBUG
                        //                    lblDebugACK.Text = String.Concat(DateTime.Now.ToShortTimeString(), " ", e.Data);
                        //#endif
                        break;
                    case LocationMonitor.LocMonNotifications.CellLocationSent:
                        SetLogoStatus((int)SentState.CellIDSaved);
                        lblStatusDetails.Text = "";
                        //#if DEBUG
                        //                    lblDebugACK.Text = String.Concat(DateTime.Now.ToShortTimeString(), " ", e.Data);
                        //#endif
                        break;
                    case LocationMonitor.LocMonNotifications.CellLocationUnknownSent:
                        SetLogoStatus((int)SentState.CellIDSaved);
                        lblStatusDetails.Text = "Unknown cell, use GPS.";
                        //#if DEBUG
                        //                    lblDebugACK.Text = String.Concat(DateTime.Now.ToShortTimeString(), " ", e.Data);
                        //#endif
                        break;
                    case LocationMonitor.LocMonNotifications.LocationNotChanged:
                        lblStatusDetails.Text = String.Concat("Not moved, skip plot = ", e.Data);
                        break;
                    case LocationMonitor.LocMonNotifications.LocationNotSent:
                        SetLogoStatus((int)SentState.NothingSaved);

                        lblLastSentTime.Text = "Location not sent.";
                        break;
                    case LocationMonitor.LocMonNotifications.LocationSentError:
                        SetLogoStatus((int)SentState.NothingSaved);

                        lblStatusDetails.Text = "";

                        //#if DEBUG
                        //                    if (e.Data == "-1")
                        //                        lblLastSentTime.Text = "Error sending(no data)";
                        //                    else if (e.Data == "-2")
                        //                        lblLastSentTime.Text = "Error sending(socket)";
                        //                    else if (e.Data == "-3")
                        //                        lblLastSentTime.Text = "Error sending(general)";
                        //                    else if (e.Data == "-4")
                        //                        lblLastSentTime.Text = "Error reading";
                        //#else
                        lblLastSentTime.Text = "Unable to connect to server.";
                        //#endif

                        break;
                    case LocationMonitor.LocMonNotifications.ConfigurationChange:
                        lblLastSentTime.Text = String.Concat("Setting changes applied @", DateTime.Now.ToString("HH:mm"));
                        lblStatusDetails.Text = "";
                        break;
                    case LocationMonitor.LocMonNotifications.ConfigReceived:
                        lblLastSentTime.Text = "Communicating with server...";
                        lblStatusDetails.Text = "";
                        break;
                    case LocationMonitor.LocMonNotifications.ConfigApplied:
                        lblLastSentTime.Text = String.Concat("Setting changes applied @", DateTime.Now.ToString("HH:mm"));
                        lblStatusDetails.Text = "";
                        break;
                    case LocationMonitor.LocMonNotifications.LocationBeingSent:
                        lblLastSentTime.Text = "Sending last report...";
                        lblStatusDetails.Text = "";
                        break;
                    case LocationMonitor.LocMonNotifications.ServerNAK:
                        SetLogoStatus((int)SentState.NothingSaved);

                        if (e.Data == "I")
                            lblStatusDetails.Text = "Invalid data.";
                        else if (e.Data == "B")
                            lblStatusDetails.Text = "Email or password unknown.";
                        else if (e.Data == "D")
                            lblStatusDetails.Text = "GPS not saved.";
                        else if (e.Data == "H")
                            lblStatusDetails.Text = "Cell not saved, use GPS.";
                        else if (e.Data == "A")
                            lblStatusDetails.Text = "An update of the iFind Mobile application is required. The application will stop sending data until you have downloaded the new application.";
                        else
                            lblStatusDetails.Text = "Not acknowledged.";

                        lblLastSentTime.Text = "Not acknowledged.";
                        break;
                    case LocationMonitor.LocMonNotifications.ConnectionNotAllowed:
                        if (AskForConnection())
                        {
                            locationMonitor.StartSendingUDPMessages();
                        }
                        break;
                    case LocationMonitor.LocMonNotifications.Connecting:
                        lblLastSentTime.Text = "Opening connection...";
                        lblStatusDetails.Text = "";

                        break;
                    case LocationMonitor.LocMonNotifications.ConnectFailed:
                        SetLogoStatus((int)SentState.NothingSaved);

                        lblLastSentTime.Text = "Connection failed!";
                        lblStatusDetails.Text = "";

                        break;
                    case LocationMonitor.LocMonNotifications.LVAReceived:
                        OnLVAReceived(e.Data);

                        break;
                    case LocationMonitor.LocMonNotifications.PowerNotification:
                        lblLastSentTime.Text = "Power change: " + e.Data;
                        break;
                    case LocationMonitor.LocMonNotifications.PhoneRadioStateChange:
                        if (e.Data == "off")
                            lblLastSentTime.Text = "Phone in flightmode.";
                        else
                            lblLastSentTime.Text = "";
                        break;
                    case LocationMonitor.LocMonNotifications.PhoneRadioOff:
                        lblLastSentTime.Text = "Phone in flightmode.";
                        SetLogoStatus((int)SentState.NothingSaved);
                        break;
                    case LocationMonitor.LocMonNotifications.ConfigChange:
                        lblLastSentTime.Text = "Notifying server...";
                        break;

                    // DEBUG Entry SP TESTING
                    case LocationMonitor.LocMonNotifications.NoGPSFix:
                        debugStatements.lblDebugGPS.Text = String.Concat(DateTime.Now.ToShortTimeString(), " ", "No GPS fix.");
                        break;
                    case LocationMonitor.LocMonNotifications.NoGPSChange:
                        debugStatements.lblDebugGPS.Text = String.Concat(DateTime.Now.ToShortTimeString(), " ", "GPS not changed.");
                        break;
                    case LocationMonitor.LocMonNotifications.GPSChange:
                        debugStatements.lblDebugGPS.Text = String.Concat(DateTime.Now.ToShortTimeString(), " ", e.Data);
                        break;
                    //case LocationMonitor.LocMonNotifications.CellChanged:
                    //    break;
                    case LocationMonitor.LocMonNotifications.CellFound:
                        debugStatements.lblDebugCell.Text = String.Concat(DateTime.Now.ToShortTimeString(), " ", e.Data);
                        break;
                    //case LocationMonitor.LocMonNotifications.LacChanged:
                    //    break;
                    case LocationMonitor.LocMonNotifications.GPSTurnedOff:
                        debugStatements.lblDebugGPSState.Text = String.Concat(DateTime.Now.ToShortTimeString(), " ", "GPS off.");
                        break;
                    case LocationMonitor.LocMonNotifications.GPSTurnedOn:
                        debugStatements.lblDebugGPSState.Text = String.Concat(DateTime.Now.ToShortTimeString(), " ", "GPS on.");
                        break;

                    //#if DEBUG
                    //                case LocationMonitor.LocMonNotifications.NoGPSFix:
                    //                    lblDebugGPS.Text = String.Concat(DateTime.Now.ToShortTimeString(), " ", "No GPS fix");
                    //                    break;
                    //                case LocationMonitor.LocMonNotifications.NoGPSChange:
                    //                    lblDebugGPS.Text = String.Concat(DateTime.Now.ToShortTimeString(), " ", "GPS not changed");
                    //                    break;
                    //                case LocationMonitor.LocMonNotifications.GPSChange:
                    //                    lblDebugGPS.Text = String.Concat(DateTime.Now.ToShortTimeString(), " ", e.Data);
                    //                    break;
                    //                //case LocationMonitor.LocMonNotifications.CellChanged:
                    //                //    break;
                    //                case LocationMonitor.LocMonNotifications.CellFound:
                    //                    lblDebugCell.Text = String.Concat(DateTime.Now.ToShortTimeString(), " ", e.Data);
                    //                    break;
                    //                //case LocationMonitor.LocMonNotifications.LacChanged:
                    //                //    break;
                    //                case LocationMonitor.LocMonNotifications.GPSTurnedOff:
                    //                    lblDebugGPSState.Text = String.Concat(DateTime.Now.ToShortTimeString(), " ", "GPS off");
                    //                    break;
                    //                case LocationMonitor.LocMonNotifications.GPSTurnedOn:
                    //                    lblDebugGPSState.Text = String.Concat(DateTime.Now.ToShortTimeString(), " ", "GPS on");
                    //                    break;
                    //                default:
                    //                    break;
                    //#endif
                }

                //lblNextSendTime.Text = "next repoet at 4.30";
                //lblNextSendTime.BackColor = Color.Yellow;
                //lblLastSentTime.Text = "report sent at 4.15";
                //lblLastSentTime.BackColor = Color.Yellow;
                //lblStatusDetails.Text = "notifying to server";
                //lblStatusDetails.BackColor = Color.Yellow;

                lblNextSendTime.Refresh();
                lblLastSentTime.Refresh();
                lblStatusDetails.Refresh();
            }
            catch (Exception ex)
            {
                // Event based function. Do not throw exception here                
            }
        }

        private void OnLVAReceived(string url)
        {
            Cursor.Current = Cursors.Default;

            if (url == "-2")
            {
                lblLastSentTime.Text = "You are currently running the latest version of Mobile Tracker.";
                return;
            }
            else if (url == "-1")
            {
                lblLastSentTime.Text = "Version check failed.";
                return;
            }

            DialogResult feedback = MessageBox.Show("An updated version of Mobile Tracker application is available. Download Now?", "New version", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

            if (feedback == DialogResult.Yes)
            {
                Cursor.Current = Cursors.WaitCursor;

                int downloadBlockSize = 4096;
                HttpWebRequest httpRequest = null;
                HttpWebResponse httpResponse = null;
                Stream responseStream = null;
                FileStream localFileStream = null;

                try
                {
                    String fileName = url.Substring(url.LastIndexOf("/") + 1);
                    httpRequest = (HttpWebRequest)WebRequest.Create(url);
                    httpRequest.Method = "GET";
                    httpResponse = (HttpWebResponse)httpRequest.GetResponse();
                    responseStream = httpResponse.GetResponseStream();
                    localFileStream = new FileStream(String.Concat(resourceFolder, @"\" + fileName), FileMode.Create);
                    Byte[] buffer = new byte[downloadBlockSize];

                    int bytesRead = responseStream.Read(buffer, 0, downloadBlockSize);
                    while (bytesRead > 0)
                    {
                        localFileStream.Write(buffer, 0, bytesRead);
                        bytesRead = responseStream.Read(buffer, 0, downloadBlockSize);
                    }

                    System.Diagnostics.Process p = System.Diagnostics.Process.Start(String.Concat(resourceFolder, @"\" + fileName), "");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Download failed.");
                }
                finally
                {
                    Cursor.Current = Cursors.Default;

                    if (httpResponse != null)
                        httpResponse.Close();
                    if (responseStream != null)
                        responseStream.Close();
                    if (localFileStream != null)
                        localFileStream.Close();
                }

            }
        }

        #endregion

        private void debugMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                debugForm = new DebugForm();//  = new frmAbout(_LocationMonitor.IMSI);
                debugForm.ShowDialog();
                debugForm.Dispose();
                debugForm = null;
            }
            catch (Exception deb)
            {
                debugForm = null;
                this.BringToFront();
                this.Show();

                log.Error("Error in creating Debug form", deb);
            }
        }

        private void exitMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void minimizeMenuItem_Click(object sender, EventArgs e)
        {
            HideApplication();
        }
    }
}
