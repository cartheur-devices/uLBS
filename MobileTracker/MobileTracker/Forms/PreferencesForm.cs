using System;
using System.Windows.Forms;
using MobileShared;
using System.Drawing;
using log4net;
using Microsoft.WindowsMobile.Status;

namespace MobileTracker
{
    public partial class PreferencesForm : Form
    {
        protected static readonly ILog log = LogManager.GetLogger(typeof(PreferencesForm));

        private void saveMenuItem_Click(object sender, EventArgs e)
        {
            preferencesStatusBar.Text = "Preferences Saved.";
        }

        private void cancelMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                this.DialogResult = DialogResult.Cancel;

                Close();
            }
            catch (Exception ex)
            {
                log.Error("Error in Canceling Preferences Window", ex);
            }
        }

        public  PreferencesForm(bool isPanic, int interval)
        {
            InitializeComponent();

            cbAutoStartup.SelectedIndexChanged += new EventHandler(cbAutoStartup_SelectedIndexChanged);
            cbLocExchange.SelectedIndexChanged += new EventHandler(cbLocExchange_SelectedIndexChanged);
            cbDomesticInt.SelectedIndexChanged += new EventHandler(cbDomesticInt_SelectedIndexChanged);

#if NOVOPORT
            Text = "Novoport";
            txtPassword.Text = Configuration.Instance().Password;
            txtUsername.Text = Configuration.Instance().Email;
#else
            lblPassword.Visible = false;
            txtPassword.Visible = false;
            lblUsername.Visible = false;
            txtUsername.Visible = false;
#endif
            this.panel2.Width = panel3.Width - 12;
            this.panel3.Height = this.Height;

            bindingSourceDomestic.Add(new ComboBoxItem("1 minute", 60));
            bindingSourceDomestic.Add(new ComboBoxItem("5 minutes", 300));
            bindingSourceDomestic.Add(new ComboBoxItem("10 minutes", 600));
            bindingSourceDomestic.Add(new ComboBoxItem("15 minutes", 900));
            bindingSourceDomestic.Add(new ComboBoxItem("30 minutes", 1800));
            bindingSourceDomestic.Add(new ComboBoxItem("1 hour", 3600));
            bindingSourceDomestic.Add(new ComboBoxItem("2 hours", 7200));
            bindingSourceDomestic.Add(new ComboBoxItem("4 hours", 14400));
            bindingSourceDomestic.Add(new ComboBoxItem("8 hours", 28800));
            bindingSourceDomestic.Add(new ComboBoxItem("Daily", 86400));


            if (isPanic)
            {
                cbDomesticInt.SelectedValue = interval;                        
            }
            else
            {
                cbDomesticInt.SelectedValue = Configuration.Instance().DomesticInterval;
            }
            if (Configuration.Instance().PositionExchange)
            {
                cbLocExchange.SelectedItem = "On";
            }
            else
            {
                cbLocExchange.SelectedItem = "Off";
            }

            if (Configuration.Instance().DisconnectAfterPlot)
            {
                cbDisconnectAfterPlot.SelectedItem = "Disconnect @ report";
            }
            else
            {
                cbDisconnectAfterPlot.SelectedItem = "Stay connected";
            }

            if (Configuration.Instance().AutoStart)
            {
                cbAutoStartup.SelectedItem = "On";
            }
            else
            {
                cbAutoStartup.SelectedItem = "Off";
            }

            cbDisconnectAfterPlot.SelectedIndexChanged += new EventHandler(cbDisconnectAfterPlot_SelectedIndexChanged);

            Cursor.Current = Cursors.Default;
        }

        void cbNotMovingDelay_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if ((int)cbNotMovingDelay.SelectedValue != Configuration.Instance().NotMovingDelay)
            //{
            //    changedSettings[3] = true;
            //}
            //else
            //{
            //    changedSettings[3] = false;
            //}
        }

        void cbDomesticInt_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                //if ((int)cbDomesticInt.SelectedValue != Configuration.Instance().DomesticInterval)
                //{ 
                //   // changedSettings[1] = true;
                //}
                //else
                //   // changedSettings[1] = false;
            }
            catch (Exception ex)
            {
                                
            }
        }

        void cbLocExchange_SelectedIndexChanged(object sender, EventArgs e)
        {
            //try
            //{
            //    if (cbLocExchange.Items[cbLocExchange.SelectedIndex].ToString() == "On" && Configuration.Instance().PositionExchange || cbLocExchange.Items[cbLocExchange.SelectedIndex].ToString() == "Off" && Configuration.Instance().PositionExchange == false)
            //    {
            //        changedSettings[0] = false;
            //    }
            //    else
            //        changedSettings[0] = true;

            //}
            //catch (Exception ex)
            //{
                                
            //}
        }
        
        int intH_OffSet = 5;
        int intV_OffSet = 5;
        int intLabelHeight = 16;
        int intComboHeight = 22;
        int intLabelWidth = 0;
        public void Align()
        {

            try
            {
                intLabelWidth = panel3.Width - 2 * intH_OffSet;
                lblSettings.Bounds = new Rectangle(intH_OffSet, 4, intLabelWidth, intLabelHeight);

                panel2.Bounds = new Rectangle(0, lblSettings.Bottom + 1, 217,1);


                lblLocExchange.Bounds = new Rectangle(intH_OffSet, panel2.Bottom + 1, intLabelWidth, intLabelHeight);

                cbLocExchange.Bounds = new Rectangle(intH_OffSet +1, lblSettings.Bottom+1, 147, intComboHeight);
                
                // Local Interval
                lblDomesticInt.Bounds = new Rectangle(intH_OffSet, cbLocExchange.Bottom + intV_OffSet, intLabelWidth, intLabelHeight);
                cbDomesticInt.Bounds = new Rectangle(intH_OffSet +1, lblDomesticInt.Bottom+1, 147,intComboHeight);
                


                // International interval
                //lblInternationalInt.Bounds = new Rectangle(intH_OffSet, cbDomesticInt.Bounds + intV_OffSet, intLabelWidth, intLabelHeight);
                //cbInternationalInt.Bounds = new Rectangle(intH_OffSet +1, lblInternationalInt.Bottom +1,147, intComboHeight);
                
                // No Motion Delay
                //lblNotMovingDelay.Bounds = new Rectangle(intH_OffSet, cbInternationalInt.Bottom + intV_OffSet, intLabelWidth, intLabelHeight);
                //cbNotMovingDelay.Bounds = new Rectangle(intH_OffSet+1, lblNotMovingDelay.Bottom +1, 147,intComboHeight);

                 
                // DisconnectAfterPlot                
                //lblDisconnectAfterPlot.Bounds  = new Rectangle(intH_OffSet, cbNotMovingDelay);
                //this.lblDisconnectAfterPlot.Size = new System.Drawing.Size(181, 18);



                //lblMaxSMS.Bounds = new Rectangle(5,269,181,18);
                
                //cbMaxSMS.Bounds = new Rectangle(6, 289, 147,22);
                
                txtPassword.Location = new System.Drawing.Point(6, 372);
                this.txtPassword.Size = new System.Drawing.Size(110, 21);

                lblAutoStartup.Location = new System.Drawing.Point(5, 226);
                this.lblAutoStartup.Size = new System.Drawing.Size(181, 18);
                
                // 
                // lblPassword
                // 
                lblPassword.Location = new System.Drawing.Point(5, 355);
                this.lblPassword.Size = new System.Drawing.Size(181, 18);
                
                // 
                // cbDisconnectAfterPlot
                // 
                this.cbDisconnectAfterPlot.Location = new System.Drawing.Point(6, 202);
                this.cbDisconnectAfterPlot.Size = new System.Drawing.Size(155, 22);
                
                // 
                // txtUsername
                // 
                txtUsername.Location = new System.Drawing.Point(6, 330);
                txtUsername.Size = new System.Drawing.Size(110, 21);

                cbAutoStartup.Location = new System.Drawing.Point(6, 244);
                cbAutoStartup.Size = new System.Drawing.Size(147, 22);
                
                lblUsername.Location = new System.Drawing.Point(5, 313);
                lblUsername.Size = new System.Drawing.Size(181, 18);
                
            }
            catch (Exception ex)
            {
                                
            }
        }

        void cbAutoStartup_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void menuItemRight_Click(object sender, EventArgs e)
        {
            try
            {
                this.DialogResult = DialogResult.OK;

                Save();
            }
            catch (Exception ex)
            {                                
            }
        }

        string[] strValues = new string[9] ;
        bool[] changedSettings = new bool[] { false, false, false, false, false, false, false, false, false };
       
        private void Save()
        {
            int intCount = 0;

            strValues[intCount++] = string.Empty;
            strValues[intCount++] = string.Empty;
            strValues[intCount++] = string.Empty;

            if (Configuration.Instance().DomesticInterval != (int)cbDomesticInt.SelectedValue)
                changedSettings[3] = true;
            Configuration.Instance().DomesticInterval = (int) cbDomesticInt.SelectedValue;

            strValues[intCount++] = "B" + Configuration.Instance().DomesticInterval/60 + string.Empty;

            if (cbLocExchange.SelectedItem.ToString() == "On" && Configuration.Instance().PositionExchange || cbLocExchange.SelectedItem.ToString() != "On" && Configuration.Instance().PositionExchange == false)
                changedSettings[6] = false;
            else
                changedSettings[6] = true;
            if (cbLocExchange.SelectedItem.ToString() == "On")
            {
                Configuration.Instance().PositionExchange = true;
                strValues[intCount++] = "A1" ;
            }
            else
            {
                Configuration.Instance().PositionExchange = false;
                strValues[intCount++] = "A0";
            }

            if (cbDisconnectAfterPlot.SelectedItem.ToString() == "Stay connected" && Configuration.Instance().DisconnectAfterPlot == false || cbDisconnectAfterPlot.SelectedItem.ToString() != "Stay connected" && Configuration.Instance().DisconnectAfterPlot)
                changedSettings[7] = false;
            else
                changedSettings[7] = true;

            if (cbDisconnectAfterPlot.SelectedItem.ToString() == "Stay connected")
            {
                Configuration.Instance().DisconnectAfterPlot = false;
            }
            else
            {
                Configuration.Instance().DisconnectAfterPlot = true;
            }

            if (cbAutoStartup.SelectedItem.ToString() == "On" && Configuration.Instance().AutoStart || cbAutoStartup.SelectedItem.ToString() != "On" && Configuration.Instance().AutoStart == false)
                changedSettings[8] = false;
            else
                changedSettings[8] = true;

            if (cbAutoStartup.SelectedItem.ToString() == "On")
            {
                Configuration.Instance().AutoStart = true;
                strValues[intCount++] = "T1";
            }
            else
            {
                Configuration.Instance().AutoStart = false;
                strValues[intCount++] = "T0";
            }

            strValues[intCount++] = "R" + Configuration.Instance().MaxSMS;


#if NOVOPORT
            Configuration.Instance().Password = txtPassword.Text;
            Configuration.Instance().Email = txtUsername.Text;
#endif
            
            Close();
        }
#if POCKETPC
        //private void inputPanel_EnabledChanged(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        if (inputPanel.Enabled)
        //        {
        //            this.panel3.Height -= inputPanel.Bounds.Height;
        //        }
        //        else
        //        {
        //            this.panel3.Height += inputPanel.Bounds.Height;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
                                
        //    }
        //}
#endif

        private void frmSettings_Resize(object sender, EventArgs e)
        {
            try
            {
                this.panel3.Height = this.Height;
            }
            catch (Exception ex)
            {
                                
            }
        }

        private void cbDisconnectAfterPlot_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbDisconnectAfterPlot.SelectedItem.ToString() == "Disconnect @ report")
            {
                MessageBox.Show("A disconnect may result in a unitisation (data traffic is rounded off) and (additional) costs charged by your operator.", "Important notice!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
            }
        }

        internal string[] GetSettings()
        {
            bool blnIsTrue = false;
            try
            {
                for (int count = 0; count < changedSettings.Length; count++)
                {
                    if (changedSettings[count] == false)
                    {
                        strValues[count] = string.Empty;                        
                    }
                    else
                        blnIsTrue = true;
                    changedSettings[count] = false;
                }
            }
            catch (Exception ex)
            {                
            }
            if (blnIsTrue)
                return strValues;
            else
                return null;
        }
    }
}
