using System;
using System.Collections;
using System.Web.UI;
using System.Web.UI.WebControls;
using com.teleca.fleetonline.repository;
using com.teleca.fleetonline.utils;
using JNetBridge;
using JNetBridge.ReplyClasses;
using System.Collections.Generic;
using System.Globalization;
using System.Web;

namespace FindWhere.UserControls
{
	public partial class UserControl_Geofence : FindWhere.UserControls.UserControl_DefaultUserControl
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			lblInfo.Text = string.Empty;
            OkCancel0.HideOkCloseButton();
            OkCancel2.HideOkCloseButton();
            OkCancel0.Text_Ok = (String)GetLocalResourceObject("Geofence_ButtonDelete.Text");

			string[] deviceTypes = this.Attributes["DeviceTypes"].Split(',');
			bool clientSide = false;

			MembersPanelGeofence.BuildMenu(true, deviceTypes);
			addScript();			

			int userLevel = (int)(Session["UsrLevel"]);
			Boolean ttAdmin = (Boolean)Session["UsrTTAdmin"];
            
			if (!ttAdmin)
			{
				Geofence_TabArea_Button_EditStore.Visible = false;
				Geofence_TabArea_Button_GeofenceStoreClose.Visible = false;
				Geofence_TabArea_Button_DeleteGeofence.Visible = false;
				Geofence_TabRelation_Header.Visible = false;
				GeoFenceTab2.Visible = false;
				Geofence_TabArea_Button_NewGeofence.Visible = false;			
                
				OkCancel0.HideSubmitButton();
                OkCancel0.Text_Ok = (String)GetLocalResourceObject("Default_Button_Pop_ShowMessage_Delete");
				Geofence_TabViewDelete_Grid.Columns[0].Visible = false;
				ddlGeofences.Attributes.Add("onchange", "javascript:ShowGeoFence(false);");
			}
			else
			{
				ddlGeofences.Attributes.Add("onchange", "javascript:ShowGeoFence(true);");
			}
			
			OkCancel2.CancelClientScript = "CancelGeofence(); HideModalPopup();";
			OkCancel0.CancelClientScript = "CancelGeofence(); HideModalPopup();";
		
    		// Hide the period for devicetypes 10,11,50,51
			if (((IList)deviceTypes).Contains("10") ||
				((IList)deviceTypes).Contains("11") ||               
				((IList)deviceTypes).Contains("50") ||
				((IList)deviceTypes).Contains("51"))
			{
				clientSide = true;
				pnlPeriod.Visible = false;
				pnlActive.Visible = true;
				pnlNotificationOnce.Visible = false;				
			}
			else
			{
				pnlPeriod.Visible = true;
				pnlNotificationOnce.Visible = true;
				pnlActive.Visible = false;
			}

			if (!alreadyLoaded)
			{
				txtFromDate.Text = DateTime.Now.ToShortDateString();
                txtToDate.Text = DateTime.Now.AddYears(1).ToShortDateString();

				txtStartHour.Text = DateTime.Now.ToString("hh");
				txtStartMin.Text = DateTime.Now.ToString("mm");

				txtEndHour.Text = DateTime.Now.ToString("hh");
				txtEndMin.Text = DateTime.Now.ToString("mm");

				if (((IList)deviceTypes).Contains("50") || ((IList)deviceTypes).Contains("51"))				
					ddlActive.Items.Add(new ListItem("Always", "1"));				

				if (((IList)deviceTypes).Contains("10") || ((IList)deviceTypes).Contains("11"))
				{
					ddlActive.Items.Add(new ListItem((String)GetLocalResourceObject("Geofence_TabRelation_DDList_Always"), "1"));
					ddlActive.Items.Add(new ListItem((String)GetLocalResourceObject("Geofence_TabRelation_DDList_OutsideDefinedProfile"), "2"));
					ddlActive.Items.Add(new ListItem((String)GetLocalResourceObject("Geofence_TabRelation_DDList_DuringDefinedProfile"), "3"));
				}

				// hide some columns
				if (((IList)deviceTypes).Contains("40") || ((IList)deviceTypes).Contains("1") || ((IList)deviceTypes).Contains("4"))
				{
					// hide active
					Geofence_TabViewDelete_Grid.Columns[4].Visible = false;
					// hide status
					Geofence_TabViewDelete_Grid.Columns[6].Visible = false;
				}

				if (clientSide)
				{
					// hide from,to
					Geofence_TabViewDelete_Grid.Columns[2].Visible = false;
					Geofence_TabViewDelete_Grid.Columns[3].Visible = false;
					// hide alertonce
					Geofence_TabViewDelete_Grid.Columns[7].Visible = false;
				}

                System.Globalization.CultureInfo ci = new System.Globalization.CultureInfo(Session["UserCulture"].ToString());
                calFromDate.Format = ci.DateTimeFormat.ShortDatePattern;
                calToDate.Format = ci.DateTimeFormat.ShortDatePattern;

				updateScreen();
			}			
		}

		private void addScript()
		{
			string scriptText = @"  var GeofenceListID  = $get('" + ddlGeofences.ClientID + @"'); 
                                var txtGeoFenceID   = $get('" + txtGeoFenceName.ClientID + @"');
                                var txtGeocode   = $get('" + txtGeocode.ClientID + @"');
								var lblInfoGeofence = $get('" + lblInfo.ClientID + @"');";
			// geofencePopupOpen Enables Setting of FencePoints on the Map
			ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "PageScript", scriptText, true);
		}


		private void fillFenceTypeList()
		{
			string currentValue = ddlGeofenceOptions.SelectedValue;

			string[] deviceTypes = this.Attributes["DeviceTypes"].Split(',');
			foreach (string devicetype in deviceTypes)
			{
				ddlGeofenceOptions.Items.Clear();
				ddlGeofenceOptions.Items.Add(new ListItem((String)GetLocalResourceObject("Geofence_Common_SelectOption"), "0"));
				ddlGeofenceOptions.Items.Add(new ListItem((String)GetLocalResourceObject("Geofence_TabRelation_DDList_NotificationOnEntry"), "1"));
				ddlGeofenceOptions.Items.Add(new ListItem((String)GetLocalResourceObject("Geofence_TabRelation_DDList_NotificationOnExit"), "2"));
			}
			ddlGeofenceOptions.SelectedValue = currentValue;
		}

		private void fillGeofencesList(GeofenceActionJavaCallReply gmr)
		{
			string ddlgfValue = ddlGeofences.Value;
			string ddlsoValue = ddlSelectAnOption.SelectedValue;
			Boolean containsValue = false;

			ddlGeofences.Items.Clear();
			ddlGeofences.Items.Add(new ListItem((String)GetLocalResourceObject("Geofence_Common_SelectFence"), "-1"));
			ddlGeofenceOptions.Items.Add(new ListItem((String)GetLocalResourceObject("Geofence_Common_SelectFence"), "-1"));

			ddlSelectAnOption.Items.Clear();
			ddlSelectAnOption.Items.Add(new ListItem((String)GetLocalResourceObject("Geofence_Common_SelectOption"), "-1"));

			if (gmr.GeoFenceListDisplayHelper.DataList != null)
			{
				if (gmr.GeoFenceListDisplayHelper.DataList != null)
				{
					foreach (GeoFenceData gfd in gmr.GeoFenceListDisplayHelper.DataList)
					{
						ddlGeofences.Items.Add(new ListItem(HttpUtility.UrlDecode(gfd.Name), gfd.FenceId));
						ddlSelectAnOption.Items.Add(new ListItem(HttpUtility.UrlDecode(gfd.Name), gfd.FenceId));
						if (ddlsoValue == gfd.FenceId.ToString())
							containsValue = true;
					}
				}
			}

			ddlGeofences.Value = ddlgfValue;

			if (containsValue)
				ddlSelectAnOption.SelectedValue = ddlsoValue;
		}

		private ArrayList geofences;

		private void updateScreen()
		{
            GeofenceMembListJavaCallReply gmr = GeofenceMembListJavaCall.GetGeofenceMembList(GetJavaID());
            GeofenceActionJavaCallReply GeofenceActionJavaCallReply = JNetBridge.GeofenceActionJavaCall.GetGeofenceAction(GetJavaID(), "GETLIST");

			geofences = GeofenceActionJavaCallReply.GeoFenceListDisplayHelper.DataList;

			fillGeofencesList(GeofenceActionJavaCallReply);
			fillFenceTypeList();

			List<com.teleca.fleetonline.web.bean.GroupsAndMembersData> gmds = (List<com.teleca.fleetonline.web.bean.GroupsAndMembersData>)Session["UsrGroup"];

			// filter gmr.GeoFenceMembListDisplayHelper.DataList, only the following devicetypes: string[] deviceTypes = this.Attributes["DeviceTypes"].Split(',');
			ArrayList alNew = new ArrayList();

			string[] deviceTypes = this.Attributes["DeviceTypes"].Split(',');

			if (gmr.GeoFenceMembListDisplayHelper.DataList != null)
			{
				foreach (GeoFenceMembData gfmd in gmr.GeoFenceMembListDisplayHelper.DataList)
				{
					foreach (com.teleca.fleetonline.web.bean.GroupsAndMembersData gmd in gmds)
					{
						foreach (MemberData mdAll in gmd.AllMembers)
						{
							if (gfmd.FmId == mdAll.Userid)
							{
                                if (((IList)deviceTypes).Contains(mdAll.UserType.ToString()))
                                {
                                    gfmd.FmName = HttpUtility.UrlDecode(gfmd.FmName);                                    
                                    alNew.Add(gfmd);
                                }
							}
						}
					}
				}
			}
			Geofence_TabViewDelete_Grid.DataSource = alNew;
			Geofence_TabViewDelete_Grid.DataBind();
		}

		/// <summary>
		/// Delete geofence memberlist
		/// </summary>
		protected void DeleteClicked()
		{
			deleteRelation();
		}

		protected void DeleteCloseClicked()
		{
			if (deleteRelation())
				this.hideModalPopup();
		}

		protected Boolean deleteRelation()
		{
			ArrayList al = new ArrayList();

			for (int i = 0; i < Geofence_TabViewDelete_Grid.Rows.Count; i++)
			{
				CheckBox chkSelectFence = (CheckBox)Geofence_TabViewDelete_Grid.Rows[i].FindControl("chkSelectFence");
				if (chkSelectFence.Checked)
				{
					Label lblSselectedGeofenceId = (Label)Geofence_TabViewDelete_Grid.Rows[i].FindControl("lblGeofenceId");
					al.Add(lblSselectedGeofenceId.Text);
				}
			}
            
			string[] selectedFences = (string[])al.ToArray(typeof(string));

			bool clientSide = false;
			string[] deviceTypes = this.Attributes["DeviceTypes"].Split(',');
			if (((IList)deviceTypes).Contains("10") || 
                ((IList)deviceTypes).Contains("11") ||
				((IList)deviceTypes).Contains("50") ||
				((IList)deviceTypes).Contains("51"))
				clientSide = true;
            GeofenceMembListJavaCallReply gr = GeofenceMembListJavaCall.DeleteMember(GetJavaID(), selectedFences, clientSide);

			updateScreen();

			lblInfo.Text = (String)GetLocalResourceObject("Geofence_Message_RelationDeleted");
			return true;
		}

		protected Boolean StoreRelation()
		{
			string[] deviceTypes = this.Attributes["DeviceTypes"].Split(',');
			bool clientSide = false;

			if (ddlSelectAnOption.SelectedIndex == 0)
			{
				lblInfo.Text = (String)GetLocalResourceObject("Geofence_Message_SelectAreaName");
				return false;
			}

			if (ddlGeofenceOptions.SelectedIndex == 0)
			{
				lblInfo.Text = (String)GetLocalResourceObject("Geofence_Message_SelectGeoFenceType");
				return false;
			}

			if (((IList)deviceTypes).Contains("10") ||
				((IList)deviceTypes).Contains("11") ||
				((IList)deviceTypes).Contains("50") ||
				((IList)deviceTypes).Contains("51"))
			{
				clientSide = true;
				if (ddlActive.SelectedIndex == 0)
				{
					lblInfo.Text = (String)GetLocalResourceObject("Geofence_Message_SelectOption");
					return false;
				}
			}

			string[] fmidlst = MembersPanelGeofence.GetSelectedMembers();

			if (fmidlst.Length == 0)
			{
				lblInfo.Text = (String)GetLocalResourceObject("Geofence_Message_OneMoreMembers");
				return false;
			}

			string fenceid = ddlSelectAnOption.SelectedValue;
			int ftype = int.Parse(ddlGeofenceOptions.SelectedValue);
			string alertonce;
			if (Geofence_TabRelation_Check_Yes.Checked)
				alertonce = "1";
			else
				alertonce = "0";

			int iAlertOnce = int.Parse(alertonce);
            
            string startdate = txtFromDate.Text;
            string enddate = txtToDate.Text;

            DateTime dtStart = DateTime.Parse(startdate, new CultureInfo(Session["UserCulture"].ToString(), false));
            DateTime dtEnd = DateTime.Parse(enddate, new CultureInfo(Session["UserCulture"].ToString(), false));

            startdate = dtStart.ToString("dd-MM-yyyy");
            enddate = dtEnd.ToString("dd-MM-yyyy");        
            
			string starthr = txtStartHour.Text;
			string startmin = txtStartMin.Text;

			string endhr = txtEndHour.Text;
			string endmin = txtEndMin.Text;

			string enforcement = "1";
			string schedule = "0";
            
			// Check for only 1 geofence // 10,11,50,51
			if (clientSide)
			{
                GeofenceMembListJavaCallReply gmr = GeofenceMembListJavaCall.GetGeofenceMembList(GetJavaID());
				//Hashtable htExisting = new Hashtable();
				foreach (string fmid in fmidlst)
				{
                    if (gmr.GeoFenceMembListDisplayHelper.DataList != null)
                    {
					    foreach (GeoFenceMembData gfmd in gmr.GeoFenceMembListDisplayHelper.DataList)
					    {						
							if (fmid == gfmd.FmId)
							{								
								lblInfo.Text = FindWhere.Utils.Utils.GetUserAlias(Session, fmid) + (String)GetLocalResourceObject("Geofence_Error_MaxNumberOfOffences");
								return false;								
							}
						}
					}
				}

				schedule = (ddlActive.SelectedIndex - 1).ToString();

				//tt 15
				//addScript enforcement AND schedule
                GeofenceMembListJavaCallReply GeofenceActionJavaCallReply = GeofenceMembListJavaCall.ADDMember(GetJavaID(), fmidlst, fenceid, startdate, starthr, startmin, enddate, endhr, endmin, ftype, iAlertOnce, clientSide, enforcement, schedule);
			}
			else
			{
                GeofenceMembListJavaCallReply GeofenceActionJavaCallReply = GeofenceMembListJavaCall.ADDMember(GetJavaID(), fmidlst, fenceid, startdate, starthr, startmin, enddate, endhr, endmin, ftype, iAlertOnce, clientSide);
			}

			updateScreen();

			lblInfo.Text = (String)GetLocalResourceObject("Geofence_Message_RelationStored");

			return true;
		}

		protected void StoreRelationClicked()
		{
			StoreRelation();
		}

		protected void StoreCloseRelationClicked()
		{
			if (StoreRelation())
				this.hideModalPopup();
		}


		/// <summary>
		/// UPDATE!!! so all columns are correct
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void Geofence_TabViewDelete_Grid_RowDataBound(object sender, GridViewRowEventArgs e)
		{
			if (e.Row.RowType == DataControlRowType.DataRow)
			{
				string[] deviceTypes = this.Attributes["DeviceTypes"].Split(',');

				GeoFenceMembData gfmd = (GeoFenceMembData)e.Row.DataItem;

				foreach (GeoFenceData gd in geofences)
				{
                    Image imgStatus = (Image)e.Row.FindControl("imgStatus");

                    //client side
                    if (((IList)deviceTypes).Contains("10") ||
                        ((IList)deviceTypes).Contains("11") ||
                        ((IList)deviceTypes).Contains("50") ||
                        ((IList)deviceTypes).Contains("51"))
                    {                        
                        imgStatus.ImageUrl = getStatusImageUrl(gfmd.Status);
                        imgStatus.ToolTip = getStatusToolTip(gfmd.Status);

                        Label lblActive = (Label)e.Row.FindControl("lblActive");

                        switch (gfmd.Schedule)
                        {
                            case 0:
                                lblActive.Text = (String)GetLocalResourceObject("Geofence_TabRelation_DDList_Always");
                                break;
                            case 1:
                                lblActive.Text = (String)GetLocalResourceObject("Geofence_TabRelation_DDList_DuringDefinedProfile");
                                break;
                            case 2:
                                lblActive.Text = (String)GetLocalResourceObject("Geofence_TabRelation_DDList_OutsideDefinedProfile");
                                break;
                        }
                    }
                    else //server side
                    {                       
                        imgStatus.ImageUrl = getStatusImageUrl(2);
                        //imgStatus.ToolTip = getStatusToolTip(2);
                    }

					if (gd.FenceId == gfmd.FenceId)
					{
						Label Geofence_TabViewDelete_Grid_GeoFence = (Label)e.Row.FindControl("Geofence_TabViewDelete_Grid_GeoFence");
						Geofence_TabViewDelete_Grid_GeoFence.Text = HttpUtility.UrlDecode(gd.Name);

						Label lblInOutCrossing = (Label)e.Row.FindControl("lblOutInCrossing");
						switch (gfmd.Fmode)
						{
							case 1:
								lblInOutCrossing.Text = (String)GetGlobalResourceObject("Taal", "Common_Label_In");
								break;
							case 2:
								lblInOutCrossing.Text = (String)GetGlobalResourceObject("Taal", "Common_Label_Out");
								break;
							case 3:
								lblInOutCrossing.Text = (String)GetLocalResourceObject("Geofence_TabViewDelete_Grid_CSharp_Crossing");
								break;
						}
					}
				}

				Label lblAlertOnce = (Label)e.Row.FindControl("lblAlertOnce");
				if (gfmd.AlertOnce == 1)
					lblAlertOnce.Text = (String)GetGlobalResourceObject("Taal", "Common_Label_Yes");
				else
					lblAlertOnce.Text = (String)GetGlobalResourceObject("Taal", "Common_Label_No");

			}
		}

        private string getStatusToolTip(int status)
        {
            switch (status)
            {
                case 0:
                case 1:
                    return (String)GetLocalResourceObject("Geofence_Status_Scheduled");
                case 2:
                    return (String)GetLocalResourceObject("Geofence_Status_Sent");
                case 3:
                    return (String)GetLocalResourceObject("Geofence_Status_ToBeDeleted");
                default:
                    return (String)GetLocalResourceObject("Geofence_Status_Scheduled");
            }
        }

		private string getStatusImageUrl(int status)
		{
			string imgSrc = "";
			switch (status)
			{
				case 0:
				case 1:
					imgSrc = "/icn_scheduled.gif"; // will be sent to the device
					break;
				case 2:
					imgSrc = "/icn_sent.gif"; // sent to the device
					break;
				case 3:
					imgSrc = "/icn_failed.gif"; // to be deleted
					break;
				default:
                    imgSrc = "/icn_scheduled.gif"; // will be sent to the device
					break;
			}
			return String.Concat("../App_Themes/", Page.StyleSheetTheme, "/Images/", imgSrc);
		}

		protected void TabContainer1_ActiveTabChanged(object sender, EventArgs e)
		{
			// bug in de .net code; event fired ook als je nog in hetzelfde tab zit...
			// Joost mag weten waarom; maar nu update ik alleen als in javascript er fences zijn verwijderd
			// of toegevoegd

			if (hdnActiveTab.Value != TabContainer1.ActiveTabIndex.ToString())
			{
				hdnActiveTab.Value = TabContainer1.ActiveTabIndex.ToString();
				updateScreen();
                string geofencingString = Resources.Taal.Default_Menu_Devices_GeoFencing;
                FindWhere.Utils.Utils.SetModalPopupOutSideHeader(Page, geofencingString, "Help_Content_Geofence_" + hdnActiveTab.Value.ToString());
			}
		}
	}
}