using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using FindWhere.UserControls;
using com.teleca.fleetonline.web.bean;
using com.teleca.fleetonline.repository;
using System.Collections;
using JNetBridge;
using JNetBridge.ReplyClasses;

namespace FindWhere.UserControls
{
	public partial class UserControl_Support : FindWhere.UserControls.UserControl_DefaultUserControl
	{
		private enum ScreenMode { Omega}
		private ScreenMode currentScreenmode;

		//switch (currentScreenmode)
		//{
		//    case ScreenMode.EditDeviceName:
		//        #region EditDeviceName

		//        #endregion
		//        break;

		//    case ScreenMode.MobileReloadMessage:
		//        #region MobileReloadMessage

		//        #endregion
		//        break;
		//}


		string _StyleSheetTheme = string.Empty;
		public string StyleSheetTheme
		{
			get
			{
				if (string.IsNullOrEmpty(_StyleSheetTheme))
				{
					if (Session["ThemeToUse"] != null)
					{
						_StyleSheetTheme = Session["ThemeToUse"].ToString();
					}
				}
				return _StyleSheetTheme;
			}
		}

		protected void Page_Load(object sender, EventArgs e)
		{
            string inputScreenMode = this.Attributes["ExtraTypes"];

            switch (inputScreenMode)
            { 
                case "Omega":
                    currentScreenmode = ScreenMode.Omega;
                    break;
            
            }
            

			if (currentScreenmode == null) throw new Exception("Unknown screenmode for Nitroconfig.ascx!");

			lblInfo.Text = string.Empty;

			if (!alreadyLoaded)
			{

				switch (currentScreenmode)
				{
					case ScreenMode.Omega:
                        Support_Label_Intro.Text = (string)GetGlobalResourceObject(Page.StyleSheetTheme, "Support_Label_Intro");

                        //setPanelMain.BackColor = System.Drawing.Color.Black;
                        //lblInfo.ForeColor = System.Drawing.Color.White;
                        //Support_Label_Intro.ForeColor = System.Drawing.Color.White;
                        //Support_Label_Name.ForeColor = System.Drawing.Color.White;
                        //Support_Label_NewName.ForeColor = System.Drawing.Color.White;
                        //Support_Label_DaytimePhone.ForeColor = System.Drawing.Color.White;
                        //Support_Label_Subject.ForeColor = System.Drawing.Color.White;
                        //Support_Label_Message.ForeColor = System.Drawing.Color.White;
						break;

				
				}
			}
		}

	
		

		/// <summary>
		/// Save settings and close popup on success.
		/// </summary>
		protected void OkCloseClicked()
		{
            // clear Fields
            txtDaytimePhone.Text = "";
            txtSupport_YourName.Text = "";
            txtSupport_EmailAddress.Text = "";
            txtVehicle.Text = "";
            txtModel.Text = "";
            txtMessage.Text = "";
		}

		protected void OkClicked()
		{
			Save();
		}

     

		private bool Save()
		{
			Boolean Success = false;

			switch (currentScreenmode)
			{
				case ScreenMode.Omega:
					
                    // Send Omega Email
                    //string receiptent = (string)GetGlobalResourceObject(Page.StyleSheetTheme, "Support_Email_Receiptent");
                    //string subject = (string)GetGlobalResourceObject(Page.StyleSheetTheme, "Support_Email_Subject");
                    //string originator = (string)GetGlobalResourceObject(Page.StyleSheetTheme, "Support_Email_From");
                    string findwhereEmail = (string)GetGlobalResourceObject(Page.StyleSheetTheme, "Support_Form_Findwhere_CC");

                    string message =  string.Empty;
                    
                    //string messageSubject = txtSubject.Text;

                    //message = string.Concat(message, " Message Subject: ", txtSubject.Text, Environment.NewLine);
                    message = string.Concat(message, " Vehicle: ", txtVehicle.Text, Environment.NewLine);
                    message = string.Concat(message, " Model number: ", txtModel.Text, Environment.NewLine); 
                    message = string.Concat(message, " Message From: ", txtSupport_YourName.Text, Environment.NewLine);
                    message = string.Concat(message, " Email: ", txtSupport_EmailAddress.Text, Environment.NewLine);
                    message = string.Concat(message, " Daytimephone: ", txtDaytimePhone.Text, Environment.NewLine);
                    message = string.Concat(message, " DateTime: ", DateTime.Now.ToString("yyyyMMddHHmmss"), Environment.NewLine);
                    message = string.Concat(message, Environment.NewLine);
                    message = string.Concat(message, " Message Text: ", txtMessage.Text, Environment.NewLine);

                    // add some info to the messagetext
                    //string dayTimePhone = txtDaytimePhone.Text;
                    //string yourName = txtSupport_YourName.Text;
                    //string email = txtSupport_EmailAddress.Text;
                    //string  = txtSubject.Text;


                    SendEmailJavaCallReply rep = SendEmailJavaCall.SendSupportEmail(GetJavaID(), Server.UrlEncode(message),null,null,null);

                    if (string.IsNullOrEmpty(rep.Error))
                    {
                        FindWhere.Utils.Utils.AddStyleForResultToLabel(lblInfo, true);
                        lblInfo.Text = (string)GetLocalResourceObject("Support_Email_Message_Sent");
                        lblInfo = FindWhere.Utils.Utils.AddStyleForResultToLabel(lblInfo, true);
                    }
                    else
                    {
                        FindWhere.Utils.Utils.AddStyleForResultToLabel(lblInfo, false);
                        lblInfo.Text = (string)GetLocalResourceObject("Support_Email_Message_SentFailed");
                        lblInfo = FindWhere.Utils.Utils.AddStyleForResultToLabel(lblInfo, false);
                    }

                  	break;

			}

			return Success;
		}

      

		
	}
}