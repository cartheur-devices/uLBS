using System;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using MobileShared.TAPI;
using Microsoft.WindowsMobile.PocketOutlook;
    

namespace MobileShared
{
    /// <summary>
    /// Base class for Wizard forms.
    /// </summary>
    public partial class frmWizardBase : Form
    {
        #region Private declarations
        private int             _TimerTickCount = 0;
        private LocationMonitor _LocMonitor;
        private Panels          _CurrentPanel = Panels.None;
        #endregion

        [DllImport("coredll.dll")]
        public static extern int SystemIdleTimerReset();

        private enum Panels
        {
            None,
            Step1,
            Step2,
            Step2b,
            Step3,
            Step3b,
            Step4,
            Step5,
            Step6,
            Step7,
            Step8
        }

        public frmWizardBase(LocationMonitor inMonitor)
        {
            _LocMonitor = inMonitor;
           
            InitializeComponent();

#if LIVECONTACTS
            this.Text = "Livecontacts";
#endif

            InitStep1();
        }

        private void EnableLeftMenuItem(bool Enable)
        {
            if (Enable)
                menuItemLeft.Enabled = true;
            else
                menuItemLeft.Enabled = false;
        }

        private void EnableRightMenuItem(bool Enable)
        {
            if (Enable)
                menuItemRight.Enabled = true;
            else
                menuItemRight.Enabled = false;
        }

        /// <summary>
        /// Handles right menu item click.
        /// </summary>
        private void menuItemRight_Click(object sender, EventArgs e)
        {
            
            GetPreviousForm();
        }

        /// <summary>
        /// Handles left menu item click.
        /// </summary>
        private void menuItemLeft_Click(object sender, EventArgs e)
        {
            GetNextForm();
        }

        /// <summary>
        /// Action after 'ok' button.
        /// </summary>
        private void GetNextForm()
        {
            switch (_CurrentPanel)
            {
                case Panels.None:
                    InitStep1();
                    break;
                case Panels.Step1:
                    _LocMonitor.Start();
                    InitStep2();
                    break;
                case Panels.Step2:
#if LIVECONTACTS
                    Configuration.Instance().Email = txtUsername.Text;
                    Configuration.Instance().Password = txtPassword.Text;
                    _LocMonitor.GetIMSI();
#else
                    Cursor.Current = Cursors.WaitCursor;
                    SendInitialSMS();
                    Cursor.Current = Cursors.Default;
#endif
                    InitStep2b();
                    break;
                case Panels.Step2b:
                    // Ok pressed, enable auto start
                    Configuration.Instance().AutoStart = true;

                    InitStep3();
                    break;
                case Panels.Step3:
                    InitStep3b();
                    break;
                case Panels.Step3b:
                    InitStep3();
                    break;
                case Panels.Step4:
                    InitStep5();
                    break;
                case Panels.Step5:
                    // Ok pressed, enable auto connect
                    Configuration.Instance().AutoConnect = true;

                    InitStep7();
                    break;
                //case Panels.Step6:
                //    InitStep7();
                //    break;
                case Panels.Step7:
                    // Completed wizard, don't show it again
                    Configuration.Instance().WizardVisible = false;

                    this.Close();
                    break;
            }
        }

        /// <summary>
        /// Action after 'cancel' button.
        /// </summary>
        private void GetPreviousForm()
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor; 
                switch (_CurrentPanel)
                {
                    case Panels.Step1:
                        blnISShutingDown = true;
                        _LocMonitor.Dispose();
                        this.Close();
                        // Application.Exit();
                        break;
                    case Panels.Step2:
                        InitStep1();
                        break;
                    case Panels.Step2b:
                        // Cancel pressed, disable auto start
                        Configuration.Instance().AutoStart = false;
                        InitStep3();
                        break;
                    case Panels.Step3:
                        InitStep3b();
                        break;
                    case Panels.Step3b:
                        InitStep4();
                        break;
                    case Panels.Step4:
                        InitStep3();
                        break;
                    case Panels.Step5:
                        // Cancel pressed, disable auto connect
                        Configuration.Instance().AutoConnect = false;

                        InitStep7();
                        break;
                    //case Panels.Step6:
                    //    InitStep5();
                    //    break;
                    case Panels.Step7:
                        InitStep5();
                        break;
                    case Panels.Step8:
                        InitStep7();
                        break;
                }
            }
            catch (Exception ex)
            {
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

#region Init Steps
        private void InitStep1()
        {
            _CurrentPanel = Panels.Step1;
            lblWizard.Text = "Step 1/7";
            lblTop.Text = "Privacy statement";
#if LIVECONTACTS
            txtDescription.Text = "This application will connect to the Internet. This may result in (additional) costs charged to you by your operator for data usage. Your location will be determined via GPS and/or GSM Cellid and uploaded to the Livecontacts website. If you have any questions or concerns, please contact us at www.livecontacts.com";

            txtUsername.Visible = false;
            txtPassword.Visible = false;
            lblUsername.Visible = false;
            lblPassword.Visible = false;
#else
            txtDescription.Text = "This application needs to connect to the Internet to send your GPS location information, and may result in additional charges based on your data plan. Click OK to continue, and your GPS will be turned on.";
#endif
        }

        private void InitStep2()
        {
            EnableLeftMenuItem(true);

            _CurrentPanel = Panels.Step2;
            lblWizard.Text = "Step 2/7";

#if LIVECONTACTS
            lblTop.Text = "Account";
            txtDescription.Text = "Please register at www.livecontacts.com with your email to create an account.";

            txtUsername.Visible = true;
            txtUsername.Focus();
            txtPassword.Visible = true;
            lblUsername.Visible = true;
            lblPassword.Visible = true;
#else
            lblTop.Text = "Send text message";
            txtDescription.Text = "Your phone will now send a text message to activate your account, after which you can login at www.findwhere.com to track and manage your account.";
#endif
        }

        private void InitStep2b()
        {
            _CurrentPanel = Panels.Step2b;
            lblWizard.Text = "Step 3/7";
            lblTop.Text = "Auto startup";

#if LIVECONTACTS
            txtUsername.Visible = false;
            txtPassword.Visible = false;
            lblUsername.Visible = false;
            lblPassword.Visible = false;

            txtDescription.Text = "Livecontacts will automatically start when the phone is switched on. Press Ok to accept or Cancel to deny.";
#else
            txtDescription.Text = "This application will automatically start when the phone is switched on. Press OK to accept or Cancel to manually start.";
#endif
        }

        private void InitStep3()
        {
            EnableLeftMenuItem(false);

            Cursor.Current = Cursors.WaitCursor;
            GPSTimer.Enabled = true;
            
            _CurrentPanel = Panels.Step3;
            lblWizard.Text = "Step 4/7";
            lblTop.Text = "Seeking GPS location";
            txtDescription.Text = "For initial set-up, it may take up to 5 minutes to determine your current location. Please make sure your device is in clear view of the sky for optimal reception.";
        }

        private void InitStep3b()
        {
            EnableLeftMenuItem(true);

            Cursor.Current = Cursors.Default;
            GPSTimer.Enabled = false;

            _CurrentPanel = Panels.Step3b;
            lblWizard.Text = "Step 4/7";
            lblTop.Text = "No GPS location";
#if LIVECONTACTS
            txtDescription.Text = "GPS location cannot be calculated. Please make sure your phone is in clear view of the sky. To retry press OK, to abort and use your GSM Cellid locationposition press Cancel. If you are not able to retrieve a GPS location please contact support at www.livecontacts.com";
#else
            txtDescription.Text = "GPS location cannot be calculated. Please make sure your phone is in clear view of the sky. To retry press OK, to abort and use your GSM Cell locationposition press Cancel. If you are not able to retrieve a GPS location please contact support at www.findwhere.com";
#endif
        }

        private void InitStep4()
        {
            EnableLeftMenuItem(true);

            _CurrentPanel = Panels.Step4;
            lblWizard.Text = "Step 5/7";
            lblTop.Text = "Send location";
#if LIVECONTACTS
            txtDescription.Text = "Location calculated. To send your current location to the server press OK, to abort press Cancel. When in motion a location will be sent every 5 min. In case of not motion this interval will be once an hour.";
#else
            txtDescription.Text = "Location determined! To send your current location to your account press OK. Your location will be sent every 15 minutes, and when no movement is detected the location will be updated every two and one half hours. To change your settings login to your account at www.findwhere.com.";
#endif
        }

        private void InitStep5()
        {
            EnableRightMenuItem(true);

            _CurrentPanel = Panels.Step5;
            lblWizard.Text = "Step 6/7";
            lblTop.Text = "Connect to Internet";
            txtDescription.Text = "Always connect to the Internet automatically?";
        }

//        private void InitStep6()
//        {
//            EnableRightMenuItem(false);

//            _CurrentPanel = Panels.Step6;
//            lblWizard.Text = "Step 7/8";

//            // Show error message since no ACK has been received yet
//            if (_LocMonitor.UDPResult.IndexOf(";NAK;") > 0)
//            {
//                lblTop.Text = "Transmit not OK";
//#if LIVECONTACTS
//                txtDescription.Text = "Your location has not been sent succesfully, look for help at www.livecontacts.com";
//#else
//                txtDescription.Text = "Your location has not been sent succesfully, look for help at www.findwhere.com";
//#endif
//            }
//            else
//            {
//                lblTop.Text = "Transmit OK";
//                txtDescription.Text = "Your location has succesfully been sent to your account at www.findwhere.com";
//            }
            
//        }

        private void InitStep7()
        {
            //EnableRightMenuItem(false);
            RemoveRightMenuITem();
            _CurrentPanel = Panels.Step7;
            lblWizard.Text = "Step 7/7";
            lblTop.Text = "Info";
#if LIVECONTACTS
            txtDescription.Text = "Livecontacts is now operational. Check out www.findwhere.com to learn about the Livecontacts application features:\n- Alert button\n- Safe zones\n- Speed checking\n- Location notifications";
#else
            txtDescription.Text = "iFind Mobile is now activated. Please login to your account to track your phone and set-up your advanced features.\n- Alert Button\n- Email / Text Notifications\n- Safe Zones\n- Speed Alerts";
#endif
        }

        private void RemoveRightMenuITem()
        {
            menuItemRight.Text = string.Empty;
            menuItemRight.Enabled = false;
        }
//        private void InitStep8()
//        {
//            EnableRightMenuItem(false);

//            _CurrentPanel = Panels.Step8;
//            lblWizard.Text = "Step 9/9";
//            lblTop.Text = "Info";
//#if LIVECONTACTS
//            txtDescription.Text = "To track other mobile phones simply add members to your account online. If you have any issues or questions please consult the support section on the website. Thank you for choosing Livecontacts!";
//#else
//            txtDescription.Text = "To track other mobile phones simply add members to your account online. If you have any issues or questions please consult the support section on the website. Thank you for choosing FindWhere!";
//#endif
//        }
#endregion

        private void SendInitialSMS()
        {
            // Only send real SMS in RELEASE mode
#if !DEBUG
            SmsMessage msg = new SmsMessage(Configuration.Instance().VMN, String.Concat("IMSI: ", _LocMonitor.IMSI));
           
           
           //msg.To.Add(new Microsoft.WindowsMobile.PocketOutlook.Recipient(Configuration.Instance().VMN2));
            try
            {
                msg.RequestDeliveryReport = true;
                msg.Send();
               // MessageBox.Show("Message Sent to " + Configuration.Instance().VMN);
                msg = null;
               

            }               
            catch (InvalidSmsRecipientException ex)
            {
               // MessageBox.Show("InvalidSmsRecipientException =" + ex.Message);
            }
            catch (SmsException ex)
            {
               // MessageBox.Show("SmsException =" + ex.Message);
            }
            catch (Exception ex)
            {
               // MessageBox.Show("Exception =" + ex.Message);
            }

            try
            {
                msg = new SmsMessage(Configuration.Instance().VMN2, String.Concat("IMSI: ", _LocMonitor.IMSI));
                msg.RequestDeliveryReport = true;
                msg.Send();
              //  MessageBox.Show("Message Sent to " + Configuration.Instance().VMN2);
            }
            catch (InvalidSmsRecipientException ex)
            {
               // MessageBox.Show("InvalidSmsRecipientException =" + ex.Message);
            }
            catch (SmsException ex)
            {
               // MessageBox.Show("SmsException =" + ex.Message);
            }
            catch (Exception ex)
            {
               // MessageBox.Show("Exception =" + ex.Message);
            }

           // MobileShared.TAPI.SMS.SendSMS(String.Concat("IMSI: ", _LocMonitor.IMSI), Configuration.Instance().VMN);
           // MobileShared.TAPI.SMS.SendSMS(String.Concat("IMSI: ", _LocMonitor.IMSI), Configuration.Instance().VMN2);
#endif
        }

        private void GPSTimer_Tick(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                _TimerTickCount++;
                //if (_TimerTickCount % 50 == 0)
                //    System.Windows.Forms.MessageBox.Show(" =" + _TimerTickCount);
                SystemIdleTimerReset();

                if (_LocMonitor.CurrentLongitude != null)
                {
                    
                    if (_LocMonitor.CurrentLongitude != "0")
                    {
                        GPSTimer.Enabled = false;
                        Cursor.Current = Cursors.Default;

                        // Let application believe we are already at 3b, so this screen will be skipped.
                        _CurrentPanel = Panels.Step3b;

                        GetPreviousForm();
                    }
                }

                // Disable anyway after 5 minutes waiting
                if (_TimerTickCount > 300)
                {
                    GPSTimer.Enabled = false;
                    Cursor.Current = Cursors.Default;

                    GetNextForm();

                    _TimerTickCount = 0;
                }
            }
            catch (Exception ex)
            {              
                // Do not throw                
            }
            finally
            {
                
            }
        }
        bool blnISShutingDown = false;
        public bool ISShuttingDown()
        {
            return blnISShutingDown;
        }
    }
}