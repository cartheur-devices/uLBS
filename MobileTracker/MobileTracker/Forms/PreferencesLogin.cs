using System;
using System.Windows.Forms;
using MobileShared;

namespace MobileTracker
{
    public partial class PreferencesLogin : Form
    {
        private int _OriginalHeight;
        bool _panic = false;
        int _intInterval = 0;
        string strErrorMessage = "Incorrect password.";

        public PreferencesLogin(bool isPanic, int interval)
        {
            InitializeComponent();
            _intInterval = interval;
            _panic = isPanic;
            txtPassword.TextChanged += new EventHandler(txtPassword_TextChanged);
            this.Load += new EventHandler(PreferencesLogin_Load);
            this.lblErrorPassword.Text = string.Empty;
              
            Cursor.Current = Cursors.Default;
        }

        void PreferencesLogin_Load(object sender, EventArgs e)
        {
            lblErrorPassword.Text = string.Empty;
            lblErrorPassword.Visible = false;
            txtPassword.Text = " ";
            txtPassword.Text = "";
        }

        private void cancelMenuItem_Click(object sender, EventArgs e)
        {
            // In reaction to entering password

            InputPanel.Enabled = false;
            InputPanel.EnabledChanged -= new EventHandler(InputPanel_EnabledChanged); 

            Close();
        }

        private void nextMenuItem_Click(object sender, EventArgs e)
        {
            //InputPanel.Enabled = false;
            //InputPanel.EnabledChanged -= new EventHandler(InputPanel_EnabledChanged); 
            if (Configuration.Instance().SettingsPassword == txtPassword.Text)
                ShowSettings();
            else
            {
                lblErrorPassword.Text = strErrorMessage;
                lblErrorPassword.Visible = true;
            }
        }

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {
            lblErrorPassword.Text = string.Empty;
                lblErrorPassword.Visible = false;
        }

        string[] strValues = null;
        private void ShowSettings()
        {
            //if pocketpc
            InputPanel.Enabled = false;
            InputPanel.EnabledChanged -= new EventHandler(InputPanel_EnabledChanged);
            //stop

            Cursor.Current = Cursors.WaitCursor;
            PreferencesForm prefsForm = null;
            prefsForm = new PreferencesForm(_panic, _intInterval);
            DialogResult Result = prefsForm.ShowDialog();
            this.DialogResult = Result;

            if (DialogResult == DialogResult.OK)
            {

                strValues = prefsForm.GetSettings();

            }
            
           // InputPanel.Dispose();
            //SettingsForm.Close();
            //SettingsForm.Dispose();
            //SettingsForm = null;
                        
        }

        public string[] GetSettings
        {
            get { return strValues; }
        }
        //if pocketpc
        private void InputPanel_EnabledChanged(object sender, EventArgs e)
        {
            try
            {
                if (InputPanel.Enabled == false)
                {
                    // The SIP is disabled, so the height of the tab control
                    // is set to its original height with a variable
                    // which is determined during initialization of the form.
                    lblErrorPassword.Top += 80;
                    lblPassword.Top += 80;
                    txtPassword.Top += 80;
                }
                else
                {
                    // The SIP is enabled, so the height of the tab control
                    // is set to the height of the visible desktop area.
                    lblErrorPassword.Top -= 80;
                    lblPassword.Top -= 80;
                    txtPassword.Top -= 80;
                }
            }
            catch (Exception ex)
            {

                
            }
        }
        //stop
        private void txtPassword_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                nextMenuItem_Click(this, null);
            }
            else
            {
                lblErrorPassword.Text = string.Empty;
                lblErrorPassword.Visible = false;
            }
        }
    }
}
