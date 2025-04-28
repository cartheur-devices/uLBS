using System;
using System.Windows.Forms;
using System.Reflection;
using OpenNETCF.WindowsCE;
using System.Diagnostics;

namespace MobileShared
{
    public partial class frmAbout : Form
    {
        public frmAbout(string IMSI)
        {
            InitializeComponent();

            #region #if 'LiveContacts' hidden
            //Text = "Livecontacts";
            //lblApplicationTitle.Text = "Livecontacts";
            //llFindwhereURL.Text = "www.livecontacts.com";
            //lblIMSI.Visible = false;
            //lblIMSINumber.Visible = false;
            //#else
            //lblIMSINumber.Text = IMSI;
            #endregion

            lblPlatformName.Text = OpenNETCF.WindowsCE.DeviceManagement.PlatformName;
            lblOSName.Text = System.Environment.OSVersion.Version.ToString();
            lblDeviceName.Text = OpenNETCF.WindowsCE.DeviceManagement.OemInfo;

            Assembly ResLoader = Assembly.GetExecutingAssembly();
            string AppVersion = Configuration.Instance().AppVersion;
                //ResLoader.GetName().Version.ToString();

            if(Configuration.Instance().Server.IndexOf("test")>-1)
                AppVersion += " T";
            else if(Configuration.Instance().Server.IndexOf("staging")>-1)
                AppVersion += " S";

            lblVersionNumber.Text = AppVersion;
        }

        void llFindwhereURL_Click(object sender, System.EventArgs e)
        {
            Process proc = null;
            try
            {
                proc = new Process();
                proc.StartInfo.FileName = @"\Windows\iexplore.exe";
                proc.StartInfo.Arguments = "http://www.findwhere.com";
                proc.Start();
                proc.WaitForExit(100);
            }
            catch (Exception)
            {
                // Do not throw exception                
            }
        }
        private void menuItem2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
