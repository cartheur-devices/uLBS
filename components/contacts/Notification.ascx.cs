using System;
using System.Web;
using System.Web.UI.WebControls;

using com.teleca.fleetonline.web.bean;
using com.teleca.fleetonline.utils;
using AjaxControlToolkit;
using JNetBridge.ReplyClasses;
using System.Resources;
using System.Globalization;
using System.Reflection;
using System.Text;

namespace FindWhere.UserControls
{
	public partial class UserControl_Notification : FindWhere.UserControls.UserControl_DefaultUserControl
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			lblInfo.Text = string.Empty;

			if (!Page.IsPostBack)
			{
				SubmitCancel1.HideCancelButton();
				SubmitCancel1.HideOkCloseButton();

				SubmitCancel1.Text_Ok = (string)this.GetGlobalResourceObject("Taal", "OkCancel_Button_OkText_Continue");

				if (Session["Fleetonline_error_content"] != null)
				{
					lblNotification.Text = (Session["Fleetonline_error_content"].ToString()).Trim();
					lblNotificationMessage.Text = ((String)this.GetGlobalResourceObject(Page.StyleSheetTheme, "Notifier_Notification")).Trim();
					//cmdNotification.PostBackUrl = (String)this.GetGlobalResourceObject(Page.StyleSheetTheme, "Notifier_Notification_Url");
					cmdNotification.Text = (String)this.GetGlobalResourceObject(Page.StyleSheetTheme, "Notifier_Notification_Text");
                    //cmdNotification.OnClientClick = "window.open('" + (String)this.GetGlobalResourceObject(Page.StyleSheetTheme, "Notifier_Notification_Url") + "'); return false;";

                    string topupUrl = @"http://www.findwhere.com/sign-up-wizard/main.php";

                    cmdNotification.OnClientClick = "openShopWindow('" + topupUrl +"', '" + System.Configuration.ConfigurationManager.AppSettings["TopUpCountryCode"]+"')";                    
				}
			}
		}
        		
		protected void OkClicked()
		{
			HideMe();
		}

		/// <summary>
		/// Hides current popup.
		/// </summary>
		private void HideMe()
		{
			Session["Fleetonline_error_content"] = null;

			ModalPopupExtender ModalPopupOutside = (ModalPopupExtender)Page.FindControl("ModalPopupOutside");
			ModalPopupOutside.Hide();

			this.Visible = false;
			FindWhere.Utils.Utils.ShowNextPopup(this.Session, this.Page);

			lblNotification.Text = string.Empty;
			lblNotificationMessage.Text = string.Empty;
		}

		/// <summary>
		/// Override function from baseclass to hide Notification.
		/// </summary>
		protected override void CloseClicked(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			HideMe();
		}

		public string GetLabelNotificationClientiD()
		{
			return lblNotification.ClientID;
		}
	}
}