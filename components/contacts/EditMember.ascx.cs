using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using FindWhere.UserControls;
using com.teleca.fleetonline.web.bean;
using com.teleca.fleetonline.repository;
using System.Collections;

namespace FindWhere.UserControls
{
	public partial class UserControl_EditMember : FindWhere.UserControls.UserControl_DefaultUserControl
	{
		private enum ScreenMode { EditDeviceName, MobileReloadMessage }
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
			string[] deviceTypes = this.Attributes["DeviceTypes"].Split(',');
			if (((IList)deviceTypes).Contains("2")) currentScreenmode = ScreenMode.EditDeviceName;
			if (((IList)deviceTypes).Contains("3")) currentScreenmode = ScreenMode.MobileReloadMessage;
			if (currentScreenmode == null) throw new Exception("Unknown screenmode for Nitroconfig.ascx!");

			lblInfo.Text = string.Empty;

			if (!alreadyLoaded)
			{
				FillDropDown();

				//FillMarkers();
				string scriptText = @"  var dropDownID  = $get('" + ddlMember.ClientID + @"');
										var EditMember_Text_NewName = $get('" + EditMember_Text_NewName.ClientID + @"');
										 function dropDownSelected(){EditMember_Text_NewName.value = dropDownID.options[dropDownID.selectedIndex].text;};";
				ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "PageScript", scriptText, true);


				switch (currentScreenmode)
				{
					case ScreenMode.EditDeviceName:
						EditMember_Label_Intro.Text = (String)GetLocalResourceObject("EditMember_Label_Intro");

						EditMember_EditDeviceName_Panel.Visible = true;

						List<Marker> markers = new List<Marker>();

						String[] imageSetNames = (Resources.Properties.direction_image_sets).Split(',');
						for (int i = 0; i < imageSetNames.Length; i++)
						{
							markers.Add(new Marker("~/App_Themes/Marker/" + imageSetNames[i] + "/"));
						}
						gvMarkers.DataSource = markers;
						gvMarkers.DataBind();


                        GroupsAndMembersData gmd = ((List<com.teleca.fleetonline.web.bean.GroupsAndMembersData>)Session["UsrGroup"])[0];
                        if (gmd.AllMembers == null | gmd.AllMembers.Count == 0)
                        {
                            imgSelected.Visible = false;
                        }


						break;

					case ScreenMode.MobileReloadMessage:
						EditMember_Label_Intro.Text = (String)GetLocalResourceObject("EditMember_Wiconfig_Label_ReloadMessage_Intro");
						SubmitCancel1.Text_Ok = (String)GetLocalResourceObject("EditMember_Button_OkCancel_Ok");
						SubmitCancel1.Text_OkClose = (String)GetLocalResourceObject("EditMember_Button_OkCancel_OkCancel");
						EditMember_EditDeviceName_Panel.Visible = false;


						break;
				}
			}
		}

		protected void ddlMember_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (ddlMember.SelectedIndex > -1)
			{
				EditMember_Text_NewName.Text = ddlMember.SelectedItem.Text;
				GroupsAndMembersData gmd = ((List<com.teleca.fleetonline.web.bean.GroupsAndMembersData>)Session["UsrGroup"])[0];
				foreach (MemberData md in gmd.AllMembers)
				{
					if (md.Userid == ddlMember.SelectedValue)
					{
						imgSelected.ImageUrl = convertIconId2ImageUrl(md.IconId);
						break;
					}
				}


			}
			else
				EditMember_Text_NewName.Text = "";

			reShowModalPopup();
		}

		private void FillDropDown()
		{
			ddlMember.Items.Clear();
			GroupsAndMembersData gmd = ((List<com.teleca.fleetonline.web.bean.GroupsAndMembersData>)Session["UsrGroup"])[0];
			ddlMember.DataSource = gmd.AllMembers;
			ddlMember.DataBind();

			if (gmd.AllMembers != null && gmd.AllMembers.Count > 0)
			{
				MemberData data = (MemberData)(gmd.AllMembers[0]);
				EditMember_Text_NewName.Text = data.Alias;
				imgSelected.ImageUrl = convertIconId2ImageUrl(data.IconId);
			}
			else
			{
				EditMember_Text_NewName.Text = String.Empty;
				imgSelected.ImageUrl = String.Empty;
			}

		}

		public string convertIconId2ImageUrl(int iconId)
		{
			string result = "~/App_Themes/Marker/set";
			int setNumber = 0; //default = set0
			int size = 1;

			if (iconId >= 0)
			{
				setNumber = int.Parse(iconId.ToString()[0].ToString());
			}

			if (iconId >= 10)
			{
				size = iconId % 10;
			}

			return result + setNumber + "/" + size + "/1.png";
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
				updateMemberMenuPanels();
                //RefreshChooseNameDropDown(); 
                RefreshComPanel();
                
				this.hideModalPopup();
			}
		}

		protected void OkClicked()
		{
			Save();
			updateMemberMenuPanels();
            //RefreshChooseNameDropDown(); 
            RefreshComPanel();
		}

        /// <summary>
        /// 
        /// </summary>
        protected void updateMemberMenuPanels()
        {
            FindWhere.UserControls.UserControl_MembersMenuPanel mmp = (FindWhere.UserControls.UserControl_MembersMenuPanel)this.Page.FindControl("MembersPanel");
            mmp.BuildMainMenu(true);


            mmp = (FindWhere.UserControls.UserControl_MembersMenuPanel)this.Page.FindControl("MembersPanel1");
            mmp.BuildMenu(false);

            mmp = (FindWhere.UserControls.UserControl_MembersMenuPanel)this.Page.FindControl("MembersPanel2");
            mmp.BuildMenu(false);

            UpdatePanel updMembersPanel = (UpdatePanel)this.Page.FindControl("updMembersPanel");
            updMembersPanel.Update();
        }

		private bool Save()
		{
			Boolean Success = false;

			switch (currentScreenmode)
			{
				case ScreenMode.EditDeviceName:
					#region EditDeviceName
					if (ddlMember.SelectedIndex > -1)
					{
						int memberId = int.Parse(ddlMember.SelectedValue);

						string selectedImg = imgSelected.ImageUrl;
						char[] splitter = { '/' };
						string[] strings = selectedImg.Split(splitter);
						string s1 = strings[strings.Length - 3].Replace("set", "");
						string s2 = strings[strings.Length - 2];
						string rawId = s1 + s2;
						int iconId = int.Parse(rawId);

                        JNetBridge.ReplyClasses.MemberJavaCallReply reply = JNetBridge.MemberJavaCall.SaveMember(GetJavaID(), memberId, EditMember_Text_NewName.Text, iconId);

                        GetTeamsFromJavaToSession();

						if (reply.Confirm != null && reply.Confirm.ToLower() == "ok")
						{
							lblInfo.Text = Resources.Taal.Common_Message_RequestSavedSettingsNextContactUnit;
							Success = true;
							lblInfo = FindWhere.Utils.Utils.AddStyleForResultToLabel(lblInfo, true);
						}
						else
						{
							Success = false;
							switch (reply.Error.ToLower())
							{
								case "editmember_delete_fo":
									lblInfo.Text = (String)GetLocalResourceObject("EditMember_Message_FleetownersCannotDelete");
									break;

								case "error.msisdn_not_the_same":
									lblInfo.Text = (String)GetLocalResourceObject("EditMember_Message_MsisdnNotTheSame");
									break;

								default:
									lblInfo.Text = string.Concat(Resources.Taal.Common_Error_UnknownError, reply.Error.ToLower());
									break;
							}
							lblInfo = FindWhere.Utils.Utils.AddStyleForResultToLabel(lblInfo, false);
						}
					}
					#endregion
					break;

				case ScreenMode.MobileReloadMessage:
					#region MobileReloadMessage

					#endregion
					break;
			}

			return Success;
		}      

		protected void Marker_Click(object sender, EventArgs e)
		{
			ImageButton ib = (ImageButton)sender;
			imgSelected.ImageUrl = ib.ImageUrl;
		}

		private class Marker
		{
			public string MarkerVerySmall { get; set; }
			public string MarkerSmall { get; set; }
			public string MarkerNormal { get; set; }

			public Marker(string name)
			{
				if (name.ToLower().Contains("set0"))
				{
					MarkerVerySmall = string.Concat(name + "1/0.png");
					MarkerSmall = string.Concat(name + "2/0.png");
					MarkerNormal = string.Concat(name + "3/0.png");
				}
				else
				{
					MarkerVerySmall = string.Concat(name + "1/1.png");
					MarkerSmall = string.Concat(name + "2/1.png");
					MarkerNormal = string.Concat(name + "3/1.png");
				}
			}
		}
	}
}
