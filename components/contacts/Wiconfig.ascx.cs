using System;
using System.Web;
using System.Web.UI.WebControls;
using com.teleca.fleetonline.repository;
using AjaxControlToolkit;
using com.teleca.fleetonline.utils;

namespace FindWhere.UserControls
{
	public partial class UserControl_Wiconfig : FindWhere.UserControls.UserControl_DefaultUserControl
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			lblInfo.Text = string.Empty;

			if (alreadyLoaded == false)
			{
				int userLevel = (int)(Session["UsrLevel"]);
				Boolean ttAdmin = (Boolean)Session["UsrTTAdmin"];

				if (!ttAdmin) OkCancel.HideSubmitButton();

				HttpCookie fwCookie = Request.Cookies["fwUser"];

				bool int_on = false;
				bool nat_on = false;
				bool mov_on = false;
				bool speed_on = false;
                bool locationExchange_on = false;

				//FOLProperties fp = FindWhere.Utils.Utils.GetCachedProperties(Page.StyleSheetTheme, Cache);

				if (userLevel >= 0)
				{
					int_on = userLevel >= Convert.ToInt32(Resources.Properties.wiconfig_paramaterlevel_intint);
					nat_on = userLevel >= Convert.ToInt32(Resources.Properties.wiconfig_paramaterlevel_natint);
					mov_on = userLevel >= Convert.ToInt32(Resources.Properties.wiconfig_paramaterlevel_notmov);
					speed_on = userLevel >= Convert.ToInt32(Resources.Properties.wiconfig_paramaterlevel_speed);
                    locationExchange_on = userLevel >= Convert.ToInt32(Resources.Properties.wiconfig_paramaterlevel_locationExchange);
				}

				//Hide some columns
				gridViewWIDevices.Columns[2].Visible = int_on;
				gridViewWIDevices.Columns[3].Visible = nat_on;
				gridViewWIDevices.Columns[4].Visible = mov_on;
				gridViewWIDevices.Columns[5].Visible = speed_on;
                gridViewWIDevices.Columns[6].Visible = locationExchange_on;

				//TODO: fill dropdowns!
				string[] intDailyOptionNamesArray = (Resources.Taal.Wiconfig_DailyOptionNamesArray).Split(',');
				string[] intDailyOptionValuesArray = "0,1,5,10,15,30,60,120,240,480,1440".Split(',');
				int teller = 0;

				foreach (string item in intDailyOptionNamesArray)
				{
					ddlPulldown_natint.Items.Add(new ListItem(item, intDailyOptionValuesArray[teller]));
					teller++;
				}

				teller = 0;
				foreach (string item in intDailyOptionNamesArray)
				{
					ddlPulldown_intint.Items.Add(new ListItem(item, intDailyOptionValuesArray[teller]));
					teller++;
				}

				string[] intMotionOptionNamesArray = (Resources.Taal.Wiconfig_MotionOptionNamesArray).Split(',');
				string[] intMotionOptionValuesArray = "0,2,5,10,20".Split(',');
				teller = 0;
				foreach (string item in intMotionOptionNamesArray)
				{
					ddlPulldown_notmov.Items.Add(new ListItem(item, intMotionOptionValuesArray[teller]));
					teller++;
				}

				//SMS Notifications dropdownlist.
				string[] sendingSmsNoticesNamesArray = ((String)GetLocalResourceObject("WiConfig_DDList_SendingSmsNoticesNames")).Split(',');
				string[] sendingSmsNoticesValuesArray = "0,1,10,25,100,100000 ".Split(',');
				teller = 0;
				WiConfig_DDList_SendingSmsNotices.Items.Add(new ListItem(Resources.Taal.Common_Option_SelectOption, "-1"));

				foreach (string item in sendingSmsNoticesNamesArray)
				{
					WiConfig_DDList_SendingSmsNotices.Items.Add(new ListItem(item, sendingSmsNoticesValuesArray[teller]));
					teller++;
				}
				FillGridViewWI(null);
			}
		}

		/// <summary>
		/// Fill datagrid with data from JNetBridge.
		/// </summary>
		private void FillGridViewWI(string userid)
		{
            JNetBridge.ReplyClasses.WIJavaCallReply reply = JNetBridge.WIJavaCall.GetConfig(GetJavaID());

			gridViewWIDevices.DataSource = reply.LinkedList;
			gridViewWIDevices.DataBind();

			gridViewWIDevices.SelectedIndex = 0;
			gridViewWIDevices_SelectedIndexChanged(this, null);

			foreach (GridViewRow gr in gridViewWIDevices.Rows)
			{
				string s1 = ((Label)gridViewWIDevices.SelectedRow.FindControl("lblUserId")).Text;
				if (userid == ((Label)gr.FindControl("lblUserId")).Text)
					gridViewWIDevices.SelectedIndex = gr.RowIndex;
			}
		}

		protected void gridViewWIDevices_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (gridViewWIDevices.SelectedRow != null)
			{
				//fill the dropdowns with the selected member
				string userId = ((Label)gridViewWIDevices.SelectedRow.FindControl("lblUserId")).Text;
				// get the enforaconfig for the current member
                JNetBridge.ReplyClasses.WIJavaCallReply reply = JNetBridge.WIJavaCall.GetConfig(GetJavaID(), userId);

				ddlPulldown_natint.SelectedValue = reply.WIConfigDBObject.Req__NatInt.ToString();
				ddlPulldown_intint.SelectedValue = reply.WIConfigDBObject.Req__IntInt.ToString();
				ddlPulldown_notmov.SelectedValue = reply.WIConfigDBObject.Req__NotMov.ToString();
				txtInput_speed.Text = reply.WIConfigDBObject.Txt__Req__spd;

				lblWiconfigCurrNatint.Text = GetNetworkIntervalDescByValue(reply.WIConfigDBObject.Curr__NatInt);
				lblWiconfigCurrIntint.Text = GetRoamingIntervalDescDescByValue(reply.WIConfigDBObject.Curr__IntInt);
				lblWiconfigCurrNotmov.Text = GetSkipDescByValue(reply.WIConfigDBObject.Curr__NotMov);
				lblWiconfigCurrSpeed.Text = reply.WIConfigDBObject.Txt__Curr__spd;

                WiConfig_DDList_SendingSmsNotices.SelectedValue = reply.WIConfigDBObject.Req__MaxSms.ToString();
                WiConfig_Label_SendingSmsNotices_Value.Text = GetMaxSmsDescByValue(reply.WIConfigDBObject.Curr__MaxSms);
				//txtInput_speed.Text = reply.WIConfigDBObject.Curr__Speed.ToString();

				lblWiconfigHeaderSpeed.Text = (String)GetLocalResourceObject("WiConfig_Grid_SpeedAlert.HeaderText") + " (" + reply.WIConfigDBObject.Txt__Curr__spd__desc + ")";

				lblAlias.Text = ((LinkButton)gridViewWIDevices.SelectedRow.FindControl("lblAlias")).Text;
			}
		}

		protected void gridViewWIDevices_RowDataBound(object sender, GridViewRowEventArgs e)
		{
			// fill the intervalls
			if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.EmptyDataRow)
			{
				MemberData md = (MemberData)e.Row.DataItem;

				// get the enforaconfig for the current member
                JNetBridge.ReplyClasses.WIJavaCallReply reply = JNetBridge.WIJavaCall.GetConfig(GetJavaID(), md.Userid);

				e.Row.Cells[2].Text = GetRoamingIntervalDescDescByValue(reply.WIConfigDBObject.Curr__NatInt); // Roaming interval
				e.Row.Cells[3].Text = GetNetworkIntervalDescByValue(reply.WIConfigDBObject.Curr__IntInt); // Local interval
				e.Row.Cells[4].Text = GetSkipDescByValue(reply.WIConfigDBObject.Curr__NotMov); // Skip interval
				e.Row.Cells[5].Text = reply.WIConfigDBObject.Txt__Curr__spd.ToString(); // Speed violation
                e.Row.Cells[6].Text = GetMaxSmsDescByValue(reply.WIConfigDBObject.Curr__MaxSms); //Max SMS
                e.Row.Cells[7].Text = GetLocationExchangeDescByValue(reply.WIConfigDBObject.LocationExchange); //Location Exchange
			}
		}

        private string GetLocationExchangeDescByValue(int value)
        {
            try
            {
                return (Resources.Taal.Wiconfig_LocationExchangeOptionNamesArray).Split(',')[value];
            }
            catch (Exception e)
            {
                return String.Empty;
            }
        }

		private string GetSkipDescByValue(int p)
		{
			foreach (ListItem li in ddlPulldown_notmov.Items)
			{
				if (int.Parse(li.Value) == p)
					return li.Text;
			}
			return String.Empty;
		}

		private string GetRoamingIntervalDescDescByValue(int p)
		{
			foreach (ListItem li in ddlPulldown_natint.Items)
			{
				if (int.Parse(li.Value) == p)
					return li.Text;
			}
			return String.Empty;
		}

		private string GetNetworkIntervalDescByValue(int p)
		{
			foreach (ListItem li in ddlPulldown_intint.Items)
			{
				if (int.Parse(li.Value) == p)
					return li.Text;
			}
			return String.Empty;
		}

        private string GetMaxSmsDescByValue(int p)
        {
            foreach (ListItem li in WiConfig_DDList_SendingSmsNotices.Items)
            {
                if (int.Parse(li.Value) == p)
                    return li.Text;
            }
            return String.Empty;
        }

		/// <summary>
		/// Save settings and close popup on success.
		/// </summary>
		protected void OkCloseClicked()
		{
			Boolean Succes = Save();

			// Only close popup on success
			if (Succes)
			{
				this.hideModalPopup();
			}
		}

		/// <summary>
		/// Save settings & refresh datagrid.
		/// </summary>
		protected void SaveClicked()
		{
			string userId = null;
			if (gridViewWIDevices.SelectedRow != null)
				userId = ((Label)gridViewWIDevices.SelectedRow.FindControl("lblUserId")).Text;

			Save();

			// Refresh settings
			FillGridViewWI(userId);
		}

		/// <summary>
		/// Save WI settings.
		/// </summary>
		/// <returns>Succes boolean</returns>
		private bool Save()
		{
			Boolean _success = false;
			string userId = ((Label)gridViewWIDevices.SelectedRow.FindControl("lblUserId")).Text;

			int intInt = int.Parse(ddlPulldown_intint.SelectedValue);
			int natInt = int.Parse(ddlPulldown_natint.SelectedValue);
			int notMov = int.Parse(ddlPulldown_notmov.SelectedValue);
            int maxSms = int.Parse(WiConfig_DDList_SendingSmsNotices.SelectedValue);
			int speed = -1;

			try
			{
				speed = int.Parse(txtInput_speed.Text);
				// ALS MPH -> Speed *   1.6
			}
			catch (Exception e) { }

			// Do the actual save
            JNetBridge.ReplyClasses.WIJavaCallReply reply = JNetBridge.WIJavaCall.SaveConfig(GetJavaID(), userId, intInt, natInt, notMov, speed, maxSms);


			if (reply.Confirm != null && reply.Confirm.ToLower() == "ok")
			{
				lblInfo.Text = Resources.Taal.Common_Message_RequestSavedSettingsNextContactUnit;
				_success = true;
				lblInfo = FindWhere.Utils.Utils.AddStyleForResultToLabel(lblInfo, true);
			}
			else
			{
				_success = false;
                if (reply.Error == null) reply.Error = "";
				switch (reply.Error.ToLower())
				{
					case "notsaved":
						lblInfo.Text = Resources.Taal.Common_Error_RequestNotSaved;
						break;

					case "notsent":
						lblInfo.Text = Resources.Taal.Common_Error_RequestNotSent;
						break;

					case "notcharged":
						lblInfo.Text = Resources.Taal.Common_Error_NotCharged;
						break;

					default:
						lblInfo.Text = string.Concat(Resources.Taal.Common_Error_UnknownError, reply.Error.ToLower());
						break;
				}
				lblInfo = FindWhere.Utils.Utils.AddStyleForResultToLabel(lblInfo, false);
			}
			return _success;
		}
	}
}
