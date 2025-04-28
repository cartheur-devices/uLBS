using System;
using System.Collections;
using System.Web.UI.WebControls;
using com.teleca.fleetonline.web.bean;
using System.Collections.Generic;
using com.teleca.fleetonline.repository;

namespace FindWhere.UserControls
{
	public partial class UserControl_Geocoding : FindWhere.UserControls.UserControl_DefaultUserControl
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			lblInfo.Text = string.Empty;

			if (!alreadyLoaded)
			{
				int userLevel = (int)(Session["UsrLevel"]);
				Boolean ttAdmin = (Boolean)Session["UsrTTAdmin"];

				if (!ttAdmin) SubmitCancel1.HideSubmitButton();

				fillMemberRepeater();
			}
		}

		public override void CancelClicked()
		{
			base.CancelClicked();
		}


		private void fillMemberRepeater()
		{
			GroupsAndMembersData gmd = ((List<com.teleca.fleetonline.web.bean.GroupsAndMembersData>)Session["UsrGroup"])[0];
			repPopupTTProfileMembersList.DataSource = gmd.AllMembers;
			repPopupTTProfileMembersList.DataBind();
		}

		protected void repPopupTTProfileMembersList_ItemDataBound(object sender, System.Web.UI.WebControls.RepeaterItemEventArgs e)
		{
			if (e.Item.ItemType == System.Web.UI.WebControls.ListItemType.Item || e.Item.ItemType == System.Web.UI.WebControls.ListItemType.AlternatingItem)
			{
				//TODO: add some translation
				((Label)e.Item.FindControl("Geocoding_Grid_On")).Visible = ((MemberData)e.Item.DataItem).GeocodingAvailable;
				((Label)e.Item.FindControl("Geocoding_Grid_Off")).Visible = !((MemberData)e.Item.DataItem).GeocodingAvailable;
			}
		}

		protected void OkClicked()
		{
			Store();
		}

		protected void OkCloseClicked()
		{

			if (Store())
				hideModalPopup();
		}

		private Boolean Store()
		{

			if (ddlActionSelect.SelectedIndex > 0)
			{
				string[] msisdn = getMsisdnFromRepeater();

				if (msisdn.Length == 0)
				{
					lblInfo.Text = Resources.Taal.Common_Device_SelectDevice;
					return false;
				}

				JNetBridge.GeoCodingJavaCall.GeoSwitch geoSwitch = JNetBridge.GeoCodingJavaCall.GeoSwitch.off;
				if (ddlActionSelect.SelectedIndex == 1) geoSwitch = JNetBridge.GeoCodingJavaCall.GeoSwitch.on;
				if (ddlActionSelect.SelectedIndex == 2) geoSwitch = JNetBridge.GeoCodingJavaCall.GeoSwitch.off;

                JNetBridge.ReplyClasses.GeoCodingJavaCallReply grpReply = JNetBridge.GeoCodingJavaCall.Change(GetJavaID(), geoSwitch, msisdn);

                GetTeamsFromJavaToSession();

				lblInfo.Text = Resources.Taal.Common_Device_SettingsStored;
				fillMemberRepeater();
				return true;
			}
			else
			{
				lblInfo.Text = Resources.Taal.Common_Device_SelectOptionFromList;
				return false;
			}
		}


		private string[] getMsisdnFromRepeater()
		{
			ArrayList arrayListMsisdn = new ArrayList();
			for (int i = 0; i < repPopupTTProfileMembersList.Items.Count; i++)
			{
				if (((CheckBox)repPopupTTProfileMembersList.Items[i].FindControl("chkMember")).Checked)
				{
					string userId = ((Label)repPopupTTProfileMembersList.Items[i].FindControl("lblMSISDN")).Text;
					arrayListMsisdn.Add(userId);
				}
			}
			string[] retStringArrray = new string[arrayListMsisdn.Count];
			arrayListMsisdn.CopyTo(retStringArrray);
			return retStringArrray;
		}


		protected void ddlActionSelect_SelectedIndexChanged(object sender, EventArgs e)
		{

			switch (ddlActionSelect.SelectedIndex)
			{
				case 0:
					lblInfo.Text = string.Empty;
					break;
				case 1:
					lblInfo.Text = Resources.Taal.Common_Device_GeoCodingSetToOn;
					break;
				case 2:
					lblInfo.Text = Resources.Taal.Common_Device_GeoCodingSetToOff;
					break;
			}
		}

	}
}