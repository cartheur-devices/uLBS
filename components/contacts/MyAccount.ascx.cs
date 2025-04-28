using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FindWhere.UserControls;
using System.Collections;
using com.teleca.fleetonline.repository;
using com.teleca.fleetonline.web.bean;
using JNetBridge.ReplyClasses;
using JNetBridge.InteractionClasses;
using com.teleca.fleetonline.utils;
using System.Globalization;
using JNetBridge;
using System.Text;
using log4net;

namespace FindWhere.UserControls
{
	public partial class UserControl_MyAccount : FindWhere.UserControls.UserControl_DefaultUserControl
	{
        protected static readonly ILog log = LogManager.GetLogger(typeof(Login));

		private string CurrentDistanceInUnits;
		private string CurrentTimezone;
		private Hashtable providers;
		private LinkedList<ContactData> contactDatas;

		protected void Page_Load(object sender, EventArgs e)
		{
			FindWhere.Utils.Utils.ValidateTimeoutFromWebpage(Session, Page, true);

			string strTemp1 = (String)this.GetGlobalResourceObject(Page.StyleSheetTheme, "MyAccount_IntroMyBalance");
			string strTemp2 = Resources.Properties.variable_maxRowCountBalance;
		
			lblMyAccountIntroMyBalance.Text = strTemp1.Replace("%1", strTemp2);

			//lblMyAccountIntroSMSAction.Text = (String)this.GetGlobalResourceObject(Page.StyleSheetTheme, "MyAccount_IntroSMSAction");
                       
            cmdTopUp.Visible = false;

			cmdCancelSubscription.PostBackUrl = (String)this.GetGlobalResourceObject(Page.StyleSheetTheme, "MyAccount_CancelSubscription");
            cmdCancelSubscription.OnClientClick = "window.open('" + (String)this.GetGlobalResourceObject(Page.StyleSheetTheme, "MyAccount_CancelSubscription") + "'); return false;";
			if (cmdCancelSubscription.PostBackUrl.Length < 4)
				cmdCancelSubscription.Visible = false;
			else
				cmdCancelSubscription.Text = (String)this.GetGlobalResourceObject(Page.StyleSheetTheme, "MyAccount_CancelSubscriptionText");
			cmdChangeSubscription.PostBackUrl = (String)this.GetGlobalResourceObject(Page.StyleSheetTheme, "MyAccount_ChangeSubscription");
            cmdChangeSubscription.OnClientClick = "window.open('" + (String)this.GetGlobalResourceObject(Page.StyleSheetTheme, "MyAccount_ChangeSubscription") + "'); return false;";
			if (cmdChangeSubscription.PostBackUrl.Length < 4)
				cmdChangeSubscription.Visible = false;
			else
				cmdChangeSubscription.Text = (String)this.GetGlobalResourceObject(Page.StyleSheetTheme, "MyAccount_ChangeSubscriptionText");
            
			//UserData from Session
			UserData userData = (UserData)Session["UsrData"];
			//get AccountData JNetBridge.Classes.JnetBridgeLoginUnit 
            AccountDetailsJavaCallReply accountData = JNetBridge.AccountDetailsJavaCall.Get(GetJavaID());
			contactDatas = accountData.LinkedList;

			if (!this.alreadyLoaded)
			{
				OkCancel_tab4.HideOkCloseButton();

				Page.Title = (String)this.GetGlobalResourceObject(Page.StyleSheetTheme, "brandName") + " My Account";
				int userLevel = (int)(Session["UsrLevel"]);
				Boolean ttAdmin = (Boolean)Session["UsrTTAdmin"];

				if (!ttAdmin)
				{
					MyAccount_Label_Header_Tab_Contacts.Visible = false;
					MyAccountTab2.Visible = false;

					MyAccount_TabBalance_Header.Visible = false;
					MyAccountTab3.Visible = false;
                    
					MyAccount_TabMobTrack_Header.Visible = false;
					MyAccountTab4.Visible = false;
				}

				if (userLevel < 2 || !ttAdmin)
				{
					MyAccount_TabMobTrack_Header.Visible = false;
					MyAccountTab4.Visible = false;
				}

				System.Globalization.CultureInfo ci = new System.Globalization.CultureInfo(Session["UserCulture"].ToString());
				calFromDate.Format = ci.DateTimeFormat.ShortDatePattern;
				calToDate.Format = ci.DateTimeFormat.ShortDatePattern;
                				
				// If Mobile user --> extra link visible
				if (!Session["UsrDevices"].ToString().Contains("|wi"))
					cmdChangeSubscription.Visible = false;

                // If iFind 3000 user --> extra tab visible
                if (Session["UsrDevices"].ToString().Contains("|ocellus"))
                    MyAccountTab5.Visible = true;
				
				int USER_LEVEL = (int)Session["UsrLevel"];
				if (USER_LEVEL > 1)
				{
					bool vam = false;
					bool showSmsActions = Boolean.Parse(Resources.Properties.variable_showSmsActions);
					if (showSmsActions)
					{
						//    int membersSize = members.size();
						//    for(int i=0; i<membersSize; i++) {
						//        MemberData data = (MemberData)members.get(i) ;
						//        if (data.getUserType() == GlobalConstants.USER_TYPE_ID_TTVAM || data.getUserType() == GlobalConstants.USER_TYPE_ID_TT_1_5VAM) {
						//            vam = true;
						//            break;
						//        }
					}
					vam = false;
					if (!vam)
					{
						//MyAccountTab4.Visible = false;
						//MyAccountTab4.lblMyAccountTab4. = string.Empty;
					}
                }
                #region Tabs
                //  TAB1:
				MyAccount_TabPersonal_Text_EMail.Text = accountData.AccountDetailsDisplayHelper.PersonalEmailAddress;
				MyAccount_Text_Tab_Personal_CompanyName.Text = HttpUtility.UrlDecode(accountData.AccountDetailsDisplayHelper.PersonalCompanyName);
				MyAccount_Text_Tab_Personal_PostCode.Text = accountData.AccountDetailsDisplayHelper.PersonalPostcode;

				ddlSelectTimeZone.DataSource = accountData.AccountDetailsDisplayHelper.TimeZoneIDS;
				ddlSelectTimeZone.SelectedValue = accountData.AccountDetailsDisplayHelper.TimeZoneId;
				ddlSelectTimeZone.DataBind();
				CurrentTimezone = accountData.AccountDetailsDisplayHelper.TimeZoneId;

				CurrentDistanceInUnits = accountData.AccountDetailsDisplayHelper.DistanceInUnits.ToString();

				ddlDistanceInUnits.SelectedValue = accountData.AccountDetailsDisplayHelper.DistanceInUnits.ToString();
				ddlDisplayMapLabels.SelectedValue = accountData.AccountDetailsDisplayHelper.PersonalDisplayMapLabels.ToString();
				ddlUserTimerSetting.SelectedValue = accountData.AccountDetailsDisplayHelper.UserTimeout.ToString();

				//TAB2: Contacts

				//TAB3: My Balance
				//this.Culture = Session["UserCulture"].ToString();  // set the culture of the page

				MyAccount_TabBalance_Text_FromDate.Text = DateTime.Now.AddDays(-7).Date.ToShortDateString();
				MyAccount_TabBalance_Text_ToDate.Text = DateTime.Now.Date.ToShortDateString();
				List<com.teleca.fleetonline.web.bean.GroupsAndMembersData> gmds = (List<com.teleca.fleetonline.web.bean.GroupsAndMembersData>)Session["UsrGroup"];
				ddlSelectMember.DataSource = gmds[0].AllMembers;
				ddlSelectMember.DataValueField = "Userid";
				ddlSelectMember.DataTextField = "Alias";
				ddlSelectMember.DataBind();

				BindGridContactdata();

				//TAB: SMS 
				foreach (string s in accountData.AccountDetailsDisplayHelper.SmsActionNameDatas)
				{
					ddlSelectAction.Items.Add(HttpUtility.UrlDecode(s));
				}

				fillSmsAccountData(accountData.AccountDetailsDisplayHelper);
                
				if (Resources.Properties.provider_useprovider.ToLower() != "true")
				{
					GridContacts.Columns[2].Visible = false;
				}

                // tab 4
                if ((Utils.Theme)Session["ThemeToUse"] == FindWhere.Utils.Theme.Omega)
                {
                    MyAccount_TabDeleteHistoryHeader.Visible = false;
                    MyAccountTab5.Visible = false;
                }
                else if (Session["UsrDevices"].ToString().Contains("|ocellus"))
                {
                    MyAccountTab5.Visible = true;
                    MyAccount_TabDeleteHistoryHeader.Visible = true;
                    MembersPanelHistory.LoadMembersWithDeviceTypes(new string[]{"4"});
                }

                // tab 6, popup
                fillMemberRepeater();
                                
                //OkCancel4.HideOkCloseButton();
                //OkCancel4.HideSubmitButton();
                #endregion
            }
		}

        private void fillMemberRepeater()
        {
            GroupsAndMembersData gmd = ((List<com.teleca.fleetonline.web.bean.GroupsAndMembersData>)Session["UsrGroup"])[0];
            if (gmd.AllMembers != null)
            {
                repTopUPMembersList.DataSource = gmd.AllMembers;
                repTopUPMembersList.DataBind();
                repChangePlanMembersList.DataSource = gmd.AllMembers;
                repChangePlanMembersList.DataBind();  
            }
        }

		public Hashtable GetProviders()
		{
			if (providers == null)
			{
				FOLProperties folProperties = (FOLProperties)Cache[string.Concat("folproperties", Page.StyleSheetTheme)];
				int nrOfProviders = int.Parse(Resources.Properties.provider_nrofproviders);
				providers = new Hashtable();

				for (int i = 0; i < nrOfProviders; i++)
				{

					string s = "provider" + i.ToString() + "_name";
					providers.Add(i, (String)GetGlobalResourceObject("Properties", s));
				}
			}
			return providers;
		}

		protected void ddlSelectAction_SelectedIndexChanged(object sender, EventArgs e)
		{
            AccountDetailsJavaCallReply accountData = JNetBridge.AccountDetailsJavaCall.Get(GetJavaID());
			fillSmsAccountData(accountData.AccountDetailsDisplayHelper);
		}

		private void fillSmsAccountData(AccountDetailsDisplayHelper accountDetailsDisplayHelper)
		{
			txtSmsActionKeywordId.Text = HttpUtility.UrlDecode(accountDetailsDisplayHelper.SmsActionKeyDatas[ddlSelectAction.SelectedIndex]);
			lblSmsactionUseID.Text = HttpUtility.UrlDecode(accountDetailsDisplayHelper.SmsActionUseDatas[ddlSelectAction.SelectedIndex]);
		}

		protected void AddContact()
		{
			FillContactDatasFromGrid(false);
			ContactData cd = new ContactData();
			cd.Name = "Contact_" + (contactDatas.Count+1).ToString();
			contactDatas.AddLast(cd);
			BindGridContactdata();
		}

		protected void SaveSmsActionClicked()
		{
			string url = "setSMSActions.do";
			// start filling params / paramvalues
			ArrayList alParamNames = new ArrayList();
			ArrayList alParamValues = new ArrayList();

			alParamNames.Add("cmd");
			alParamValues.Add("set");

			alParamNames.Add("action");
			int actionID = ddlSelectAction.SelectedIndex + 1;
			alParamValues.Add(actionID.ToString());

			alParamNames.Add("keyword");
			alParamValues.Add(txtSmsActionKeywordId.Text);

			string[] paramNames = (string[])alParamNames.ToArray(typeof(string));
			string[] paramValues = (string[])alParamValues.ToArray(typeof(string));

            JNetBridge.ReplyClasses.InterfaceJavaCallReply reply = JNetBridge.InterfaceJavaCall.getReply(GetJavaID(), url, paramNames, paramValues);

			lblInfo.Text = (String)GetLocalResourceObject("MyAccount_TabContacts_Message_Saved");

			//get AccountData
            AccountDetailsJavaCallReply accountData = JNetBridge.AccountDetailsJavaCall.Get(GetJavaID());
			fillSmsAccountData(accountData.AccountDetailsDisplayHelper);
			//TODO: make the reply working
            if (reply.Error != null)
            {
                lblInfo.Text = reply.Error;
                lblInfo = FindWhere.Utils.Utils.AddStyleForResultToLabel(lblInfo, false);
            }
            else
            {
                lblInfo.Text = reply.Confirm;
                lblInfo = FindWhere.Utils.Utils.AddStyleForResultToLabel(lblInfo, true);
            }

			lblInfo.Text = (String)GetLocalResourceObject("MyAccount_TabContacts_Message_ChangesToServer");
            lblInfo = FindWhere.Utils.Utils.AddStyleForResultToLabel(lblInfo, true);
		}

		private void BindGridContactdata()
		{
            if (contactDatas == null)
            {
                contactDatas = new LinkedList<ContactData>();
                //todo: make a better fix!
                //ContactData cd = new ContactData();
                //cd.IndexNr = -99999;
                //contactDatas.AddFirst(cd);
            }
            else
            {
                foreach (ContactData contact in contactDatas)
                    contact.Name = HttpUtility.UrlDecode(contact.Name);
            }

			GridContacts.DataSource = contactDatas;
			GridContacts.DataBind();

			//GridContacts.Rows[0].Visible = false;
		}

		protected void OkClicked()
		{
			savetab1();
		}

		protected void OkCloseClicked()
		{
			if (savetab1())
				this.hideModalPopup();
		}

		protected Boolean savetab1()
		{
			try
			{
				//Store the first Tab
				UserData userData = (UserData)Session["UsrData"];
				int operatorID = userData.Operator;

                if ((MyAccount_Text_Tab_Personal_PassWordOld != null && MyAccount_Text_Tab_Personal_PassWordOld.Text.Length > 8) ||
                   (MyAccount_Text_Tab_Personal_PassWordNew != null && MyAccount_Text_Tab_Personal_PassWordNew.Text.Length > 8)
                    )
                    throw new Exception("Max password length = 8.");

                JNetBridge.ReplyClasses.AccountDetailsJavaCallReply accountData = JNetBridge.AccountDetailsJavaCall.Set(GetJavaID(), MyAccount_TabPersonal_Text_EMail.Text, MyAccount_Text_Tab_Personal_PostCode.Text, "undefined", int.Parse(ddlDistanceInUnits.SelectedValue), int.Parse(ddlDisplayMapLabels.SelectedValue), MyAccount_Text_Tab_Personal_CompanyName.Text, MyAccount_Text_Tab_Personal_PassWordOld.Text, MyAccount_Text_Tab_Personal_PassWordNew.Text, int.Parse(ddlUserTimerSetting.SelectedValue), operatorID, ddlSelectTimeZone.SelectedValue.ToString());

				//refresh companel if timezone or distanceunits changed
				if (CurrentTimezone != ddlSelectTimeZone.SelectedValue | CurrentDistanceInUnits != ddlDistanceInUnits.SelectedValue)
					RefreshComPanel();

				CurrentTimezone = ddlSelectTimeZone.SelectedValue;
				CurrentDistanceInUnits = ddlDistanceInUnits.SelectedValue;

                if (CurrentDistanceInUnits == "1")
                    Session["DistanceUnit"] = Resources.Taal.Common_Value_MLHour; //  GetResource("Mph", session["ThemeToUse"].ToString());
                else
                    Session["DistanceUnit"] = Resources.Taal.Common_Value_KmHour;  //GetResource("Kph", session["ThemeToUse"].ToString());

				Session["UsrTimeout"] = int.Parse(ddlUserTimerSetting.SelectedValue);
				//settings stored!!!
				lblInfo.Text = (String)GetLocalResourceObject("MyAccount_TabContacts_Message_PersonalSettingsStored");
                lblInfo = FindWhere.Utils.Utils.AddStyleForResultToLabel(lblInfo, true);
         
                //refresh companel
                Utils.Utils.RefreshComPanel(this.Page);
                
                return true;
			}

			catch
			{
				lblInfo.Text = (String)GetLocalResourceObject("MyAccount_TabContacts_Message_PersonalSettingsNOTStored");
                lblInfo = FindWhere.Utils.Utils.AddStyleForResultToLabel(lblInfo, false);
				return false;
			}
		}

		protected void NewContactClicked()
		{
			ContactData cd = new ContactData();
			LinkedList<ContactData> ll = (LinkedList<ContactData>)GridContacts.DataSource;
		}

		protected void GridContacts_RowDeleting(object sender, GridViewDeleteEventArgs e)
		{
			//int categoryID = (int)GridContacts .DataKeys[e.RowIndex].Value;
            LinkedList<ContactData> contactDatasTemp = contactDatas;
			contactDatas = new LinkedList<ContactData>();
            ContactData[] cdArray = contactDatasTemp.ToArray();           
            for (int i = 0; i < GridContacts.Rows.Count; i++)
            {
                if(i != e.RowIndex)
                    contactDatas.AddLast(cdArray[i]);
            }

			BindGridContactdata();

            SaveContacts();
		}

		protected void GridContacts_RowDataBound(object sender, GridViewRowEventArgs e)
		{
			if (e.Row.RowType == DataControlRowType.DataRow)
			{
				ImageButton cmdDelete = (ImageButton)e.Row.FindControl("cmdDelete");
				cmdDelete.Attributes.Add("onclick", "javascript:return confirm('Are you sure you want to delete the contact: " +
					 DataBinder.Eval(e.Row.DataItem, "Name") + "')");
			}
		}

		protected void AddClicked(object sender, EventArgs e)
		{
        	FillContactDatasFromGrid(false);
			ContactData cd = new ContactData();
			try
			{
				cd.IndexNr = int.Parse(((TextBox)GridContacts.FooterRow.FindControl("lblIndexFooter")).Text);
			}
			catch { }

			cd.Name = ((TextBox)GridContacts.FooterRow.FindControl("txtNameFooter")).Text;
			cd.Phone = ((TextBox)GridContacts.FooterRow.FindControl("txtPhoneFooter")).Text;
			cd.Email = ((TextBox)GridContacts.FooterRow.FindControl("txtEmailFooter")).Text;
			try { cd.Provider = int.Parse(((DropDownList)GridContacts.FooterRow.FindControl("txtEmail")).SelectedValue); }
			catch { }
			contactDatas.AddLast(cd);
			BindGridContactdata();
		}

        private void FillContactDatasFromGrid(Boolean ForSave)
		{
			contactDatas = new LinkedList<ContactData>();
			for (int i = 0; i < GridContacts.Rows.Count; i++)
			{

				ContactData cd = new ContactData();

				string sIndex = ((Label)GridContacts.Rows[i].FindControl("lblIndex")).Text;
				int indexNr;
				int.TryParse(sIndex, out indexNr);

				//cd.IndexNr = int.Parse(((Label)GridContacts.Rows[i].FindControl("lblIndex")).Text);

				if (indexNr > 0)
					cd.IndexNr = indexNr;

				cd.Name = ((TextBox)GridContacts.Rows[i].FindControl("txtName")).Text;
				cd.Phone = ((TextBox)GridContacts.Rows[i].FindControl("txtPhone")).Text;
				cd.Email = ((TextBox)GridContacts.Rows[i].FindControl("txtEmail")).Text;
				try { cd.Provider = int.Parse(((DropDownList)GridContacts.Rows[i].FindControl("txtEmail")).SelectedValue); }
				catch { }
                if (cd.IndexNr != -99999)
                {
                    if (ForSave == false || GridContacts.Rows[i].Visible == true)
                    {
                        contactDatas.AddLast(cd);
                    }
                }
			}
		}

		protected void GridContacts_Inserting(Object sender, System.Web.UI.WebControls.SqlDataSourceCommandEventArgs e)
		{
			ContactData cd = new ContactData();
			try
			{
				cd.IndexNr = int.Parse(((Label)GridContacts.FooterRow.FindControl("lblIndex")).Text);
			}
			catch (Exception ex)
			{ }

			cd.Name = ((TextBox)GridContacts.FooterRow.FindControl("txtName")).Text;
			cd.Phone = ((TextBox)GridContacts.FooterRow.FindControl("txtPhone")).Text;
			cd.Email = ((TextBox)GridContacts.FooterRow.FindControl("txtEmail")).Text;
			try { cd.Provider = int.Parse(((DropDownList)GridContacts.FooterRow.FindControl("txtEmail")).SelectedValue); }
			catch { }
			contactDatas.AddLast(cd);
		}

		protected Boolean SaveContacts()
		{
			try
            {
                string valGrid = validateGrid();
                if (valGrid != null)
                {
                    lblInfo.Text = valGrid;
                    return false; ;
                }
               
                 FillContactDatasFromGrid(true);
                // JNetBridge.Classes.JnetBridgeLoginUnit javaID = (JNetBridge.Classes.JnetBridgeLoginUnit)Session["netCookieJavaSessionID"];
                JNetBridge.ReplyClasses.ContactsJavaCallReply cr = JNetBridge.ContactsJavaCall.Save(GetJavaID(), contactDatas);

                if (cr.Fleetonline_error_content == null)
                {
                    lblInfo.Text = (String)GetLocalResourceObject("MyAccount_TabContacts_Message_ContactsSaved");
                    lblInfo = FindWhere.Utils.Utils.AddStyleForResultToLabel(lblInfo, true);
                    return true;
                }
                else
                {
                    if (cr.Fleetonline_error_content.Content == "true")
                    {
                        lblInfo.Text = (String)GetLocalResourceObject("MyAccount_TabContacts_Message_ContactsSaved");
                        lblInfo = FindWhere.Utils.Utils.AddStyleForResultToLabel(lblInfo, true);
                        return true;
                    
                    }
                    else
                    {
                        //lblInfo.Text = cr.Contacts_error;
                        lblInfo.Text = (String)GetLocalResourceObject("MyAccount_TabContacts_Message_ContactsNotSaved");
                        lblInfo = FindWhere.Utils.Utils.AddStyleForResultToLabel(lblInfo, false);

                        // -->> reload the contacts grid.. onzin, maar waar....

                        AccountDetailsJavaCallReply accountData = JNetBridge.AccountDetailsJavaCall.Get(GetJavaID());
                        contactDatas = accountData.LinkedList;
                        BindGridContactdata();

                        return false;
                    }
                }

            }
			catch (Exception ex)
			{
				lblInfo.Text = (String)GetLocalResourceObject("MyAccount_TabContacts_Message_ContactsNotSaved");
                lblInfo = FindWhere.Utils.Utils.AddStyleForResultToLabel(lblInfo, false);

                AccountDetailsJavaCallReply accountData = JNetBridge.AccountDetailsJavaCall.Get(GetJavaID());
                contactDatas = accountData.LinkedList;
                BindGridContactdata();
				return false;
			}
		}

		protected void SaveContactsClicked()
		{
			SaveContacts();
		}

		protected void SaveCloseContactsClicked()
		{
			if (SaveContacts())
				this.hideModalPopup();
		}

		private string validateGrid()
		{
			string retString = null;
			for (int i = 0; i < GridContacts.Rows.Count; i++)
			{
				if (GridContacts.Rows[i].Visible == true)
				{
					string IndexNr = (((Label)GridContacts.Rows[i].FindControl("lblIndex")).Text);
					string eml = ((TextBox)GridContacts.Rows[i].FindControl("txtEmail")).Text;
					string txtPhone = ((TextBox)GridContacts.Rows[i].FindControl("txtPhone")).Text;
					string name = ((TextBox)GridContacts.Rows[i].FindControl("txtName")).Text;

					if (string.IsNullOrEmpty(eml) && string.IsNullOrEmpty(txtPhone))
					{
						retString = (String)GetLocalResourceObject("MyAccount_TabContacts_Message_InvalidPhoneEmail");
						return retString;
					}

					//todo: make.. -> clientside validation

					if (!FindWhere.Utils.Utils.ValidateEmail(eml) && !string.IsNullOrEmpty(eml))
					{
						retString = (String)GetLocalResourceObject("MyAccount_TabContacts_Message_InvalidEmail");
						return retString;
					}

					if (string.IsNullOrEmpty(name))
					{
						retString = (String)GetLocalResourceObject("MyAccount_TabContacts_Message_InvalidName");
						return retString;
					}
				}
			}
			return retString;
		}

		public void GridContacts_RowCommand(Object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
		{
			// e.commandArgument = empty for delele / add
			if (e.CommandArgument.ToString() != "")
			{
				GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
				TextBox txtEmail = (TextBox)row.FindControl("txtEmail");
				TextBox txtEmailFooter = (TextBox)row.FindControl("txtEmailFooter");

				DropDownList ddlProviders = (DropDownList)row.FindControl("ddlProviders");
				DropDownList ddlProvidersFooter = (DropDownList)row.FindControl("ddlProvidersFooter");

				TextBox txtPhone = (TextBox)row.FindControl("txtPhone");
				TextBox txtPhoneFooter = (TextBox)row.FindControl("txtPhoneFooter");

				FOLProperties folProperties = (FOLProperties)Cache[string.Concat("folproperties", Page.StyleSheetTheme)];

				String useProvider = (String)GetGlobalResourceObject("Properties", "provider_useprovider");

				Boolean useProv = false;
				if (((String)GetGlobalResourceObject("Properties", "provider_useprovider")).ToLower() == "true")
				{
					useProv = true;
				}

				string messageText = "Test Message"; //TODO: get from folprops (?)
				JNetBridge.ReplyClasses.SendEmailJavaCallReply sendMailReply;
				int operatoreid;
                bool error = false;
				switch (e.CommandArgument.ToString())
                {
                    case "Test_1":
                        int.TryParse(ddlProviders.SelectedValue, out operatoreid);

                        if (isValidPhoneNumber(txtPhone.Text))
                        {
                            if (useProv)
                            {
                                sendMailReply = JNetBridge.SendEmailJavaCall.SendMobileEmail(GetJavaID(), HttpUtility.UrlEncode(txtPhone.Text), operatoreid, messageText);
                            }
                            else
                            {
                                //todo: send sms
                                string temText = (String)GetLocalResourceObject("MyAccount_TabContacts_Message_SMSMessage");
                                temText = temText.Replace("%1", txtPhone.Text);
                                sendMailReply = JNetBridge.SendEmailJavaCall.SendSms(GetJavaID(), HttpUtility.UrlEncode(temText), HttpUtility.UrlEncode(txtPhone.Text));
                            }
                        }
                        else
                        {
                            lblInfo.Text = (String)GetLocalResourceObject("MyAccount_TabContacts_Message_InvalidPhone");
                            lblInfo = FindWhere.Utils.Utils.AddStyleForResultToLabel(lblInfo, false);
                            error = true;
                        }
                        break;
                    case "Test_1_Footer":
                        int.TryParse(ddlProvidersFooter.SelectedValue, out operatoreid);
                        if (isValidPhoneNumber(txtPhoneFooter.Text))
                        {
                            if (useProv)
                                sendMailReply = JNetBridge.SendEmailJavaCall.SendMobileEmail(GetJavaID(), HttpUtility.UrlEncode(txtPhoneFooter.Text), operatoreid, messageText);
                            else
                            {
                                //todo: send sms
                                //todo: send sms
                                string temText = (String)GetLocalResourceObject("MyAccount_TabContacts_Message_SMSMessage");
                                temText = temText.Replace("%1", txtPhoneFooter.Text);
                                sendMailReply = JNetBridge.SendEmailJavaCall.SendSms(GetJavaID(), HttpUtility.UrlEncode(temText), HttpUtility.UrlEncode(txtPhoneFooter.Text));
                            }
                        }
                        else
                        {
                            lblInfo.Text = (String)GetLocalResourceObject("MyAccount_TabContacts_Message_InvalidPhone");
                            lblInfo = FindWhere.Utils.Utils.AddStyleForResultToLabel(lblInfo, false);
                            error = true;
                        }
                        break;
                    case "Test_2":
                        if (FindWhere.Utils.Utils.ValidateEmail(txtEmail.Text) && !string.IsNullOrEmpty(txtEmail.Text))
                        {
                            if (txtEmail.Text.Trim().Length > 0)
                                sendMailReply = JNetBridge.SendEmailJavaCall.SendEmail(GetJavaID(), txtEmail.Text);
                        }
                        else
                        {
                            lblInfo.Text = (String)GetLocalResourceObject("MyAccount_TabContacts_Message_InvalidEmail");
                            lblInfo = FindWhere.Utils.Utils.AddStyleForResultToLabel(lblInfo, false);
                            error = true;
                        }
                        break;
                    case "Test_2_Footer":
                        if (!FindWhere.Utils.Utils.ValidateEmail(txtEmailFooter.Text) && !string.IsNullOrEmpty(txtEmailFooter.Text))
                        {
                            if (txtEmailFooter.Text.Trim().Length > 0)
                                sendMailReply = JNetBridge.SendEmailJavaCall.SendEmail(GetJavaID(), txtEmailFooter.Text);
                        }
                        else
                        {
                            lblInfo.Text = (String)GetLocalResourceObject("MyAccount_TabContacts_Message_InvalidEmail");
                            lblInfo = FindWhere.Utils.Utils.AddStyleForResultToLabel(lblInfo, false);
                            error = true;
                        }
                        break;
				}
                if (!error)
                {
                    lblInfo.Text = (String)GetGlobalResourceObject("Taal", "Common_Message_MessageSent");
                    lblInfo = FindWhere.Utils.Utils.AddStyleForResultToLabel(lblInfo, true);
                }

			}
		}

        private bool isValidPhoneNumber(String phone)
        {
            if (phone == null || phone.Length < 6)
                return false;
          
            try
            {
                long numeric = long.Parse(phone.Replace("+", ""));
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        private long to_print_totaltotal = 0;
        private long to_print_totalcost = 0;      
      
		protected void ShowBalanceClicked()
		{
			// maxRowCountBalance = 200  Bepaalt aantal te tonen regels

			string fromDate = MyAccount_TabBalance_Text_FromDate.Text;
			string toDate = MyAccount_TabBalance_Text_ToDate.Text;

			DateTime dtFrom = DateTime.Parse(fromDate, new CultureInfo(Session["UserCulture"].ToString(), false));
			DateTime dtTo = DateTime.Parse(toDate, new CultureInfo(Session["UserCulture"].ToString(), false));

			fromDate = dtFrom.ToString("dd-MM-yyyy");
			toDate = dtTo.ToString("dd-MM-yyyy");

			string memberId = ddlSelectMember.SelectedValue.ToString();
			int x = DateTime.Compare(dtFrom, dtTo);
			if (DateTime.Compare(dtFrom, dtTo) < 0)
			{
                JNetBridge.ReplyClasses.BalanceJavaCallReply reply = JNetBridge.BalanceJavaCall.Get(GetJavaID(), fromDate, toDate, int.Parse(memberId), "");

                to_print_totalcost = 0;
                to_print_totaltotal = 0;                

                if (reply.BalanceDisplayHelper != null && reply.BalanceDisplayHelper.BalanceDetailsList != null)
                {
                    foreach (BalanceHelper bh in reply.BalanceDisplayHelper.BalanceDetailsList)
                    {
                        try
                        {
                            to_print_totalcost += long.Parse(bh.Cost);
                            to_print_totaltotal += long.Parse(bh.Total);                            
                        }
                        catch (Exception e)
                        {
                            continue;
                        }
                    }
                    lblCurrentBalance.Visible = true;                               
                }

				GridViewBalance.DataSource = reply.BalanceDisplayHelper.BalanceDetailsList;				
				GridViewBalance.DataBind();
				PanelBalance.Visible = true;

                string to_print_alias = ddlSelectMember.SelectedItem.Text;
                string to_print_currentBalance = reply.BalanceDisplayHelper.Balance.ToString();
                string to_print_companyName = reply.BalanceDisplayHelper.UserInfoData.CompanyName; // ff in java checken of deze gevuld wordt!
                string to_print_memberType = reply.BalanceDisplayHelper.MemberType;

                string alias = (String)GetGlobalResourceObject("Taal", "Common_Alias");
                string memberType = (String)GetGlobalResourceObject("Taal", "Common_MemberType");
                string companyName = (String)GetGlobalResourceObject("Taal", "Common_CompanyName");
                string currentBalance = (String)GetGlobalResourceObject("Taal", "Common_CurrentBalance");

                //TODO: make nice
                StringBuilder sb = new StringBuilder();
                sb.Append("<table width=\"90%\">");
                sb.Append("<tr>");
                sb.Append("<td><b>" + alias + ":</b> " + to_print_alias + "</td>");
                sb.Append("<td><b>" + memberType + ":</b> " + to_print_memberType + "</td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td><b>" + companyName + ":</b> " + to_print_companyName + "</td>");
                sb.Append("<td><b>" + currentBalance + ":</b> " + to_print_currentBalance + "</td>");
                sb.Append("</tr>");
                sb.Append("</table>");

                OkCancel2.OkCloseClientScript = "printBalanceDiv('" + GridViewBalance.ClientID + "', '" + sb.ToString() + "');";

                lblCurrentBalance.Text = ((String)GetLocalResourceObject("MyAccount_AccountBalance.Text")).
                        Replace("%1", to_print_alias).
                        Replace("%2", to_print_currentBalance);         

			}
			else
				lblInfo.Text = (String)GetGlobalResourceObject("Taal", "Common_Message_DateFromBeforeDateTo");

		}

		protected void CloseClicked()
		{
			this.hideModalPopup();
		}

        protected void GridViewBalance_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Footer)
            {
             
                Label lblChargeTotal = (Label)e.Row.FindControl("lblChargeTotal");
                lblChargeTotal.Text = to_print_totalcost.ToString()  ;

                Label lblFooterTotalCredit = (Label)e.Row.FindControl("lblFooterTotalCredit");
                lblFooterTotalCredit.Text = to_print_totaltotal.ToString();

                //Label lblCurrentAccountBalance = (Label)e.Row.FindControl("lblCurrentAccountBalance");
                    //lblCurrentAccountBalance.Text = to_print_accountbalance.ToString();

                e.Row.BackColor = System.Drawing.Color.Gray;
            }
        }

        protected void repTopupMembersList_ItemDataBound(object sender, System.Web.UI.WebControls.RepeaterItemEventArgs e)
        {
            try
            {
                if (e.Item.ItemType == System.Web.UI.WebControls.ListItemType.Item || e.Item.ItemType == System.Web.UI.WebControls.ListItemType.AlternatingItem)
                {
                    MemberData md = (MemberData)e.Item.DataItem;
                    string topUpAdd = "?type=topup&MSISDN=#MSISDN&country=#country" .Replace("#MSISDN", md.Msisdn).Replace("#country",System.Configuration.ConfigurationManager.AppSettings["TopUpCountryCode"]);
                    //string topupUrl = System.Configuration.ConfigurationManager.AppSettings["MyAccount_TopUpUrl"];
                    string topupUrl = @"http://www.findwhere.com/sign-up-wizard/main.php";
                    string javaScriptToUpdateIframe = string.Concat("javascript:changetopUpIframe('", topupUrl + topUpAdd, "');");
                    ((HyperLink)e.Item.FindControl("lbAlias")).NavigateUrl = javaScriptToUpdateIframe;
                }
            }
            catch (Exception ex)
            {
                log.Error("MyAccountTopUp", ex);
            }
        }

        protected void repChangePlanMembersList_ItemDataBound(object sender, System.Web.UI.WebControls.RepeaterItemEventArgs e)
        {
            try
            {
                if (e.Item.ItemType == System.Web.UI.WebControls.ListItemType.Item || e.Item.ItemType == System.Web.UI.WebControls.ListItemType.AlternatingItem)
                {
                    MemberData md = (MemberData)e.Item.DataItem;
                    string changePlanAdd = "?type=cp&MSISDN=#MSISDN&country=#country" .Replace("#MSISDN", md.Msisdn).Replace("#country",System.Configuration.ConfigurationManager.AppSettings["ChangePlanCountryCode"]);
                    //string changePlanUrl = System.Configuration.ConfigurationManager.AppSettings["MyAccount_ChangePlanUrl"];
                    string changePlanUrl = @"http://www.findwhere.com/sign-up-wizard/main.php";
                    string javaScriptToUpdateIframe = string.Concat("javascript:changePlanIframe('", changePlanUrl + changePlanAdd, "');");
                    ((HyperLink)e.Item.FindControl("lbAlias")).NavigateUrl = javaScriptToUpdateIframe;
                }
            }
            catch (Exception ex)
            {
                log.Error("MyAccountChangePlan", ex);
            }
        }

        protected void DeleteHistoryClicked()
        {
            if (MembersPanelHistory.GetSelectedMembers().Length == 0)
            {
                MembersPanelHistory.LoadMembersWithDeviceTypes(new string[] { "4" });
                lblInfo.Text = Resources.Taal.Common_Device_SelectDevice;
                 lblInfo = FindWhere.Utils.Utils.AddStyleForResultToLabel(lblInfo, false);
                return;
            }

            //delete some history

            string selectedDevices = MembersPanelHistory.GetSelectedMembersCombined(",");

            string url = "deleteHistory.do";

            string[] paramNames = new string[]{"cmd", "fmid"};
            string[] paramValues = new string[]{"deleteHistory", selectedDevices};

            InterfaceJavaCallReply iReply = InterfaceJavaCall.getReply(this.GetJavaID(),url,paramNames, paramValues );          

            if (String.IsNullOrEmpty(iReply.Error))
            {
                lblInfo.Text = (String)GetLocalResourceObject("MyAccount_TabHistory_Message_HistoryDeleted");
                lblInfo = FindWhere.Utils.Utils.AddStyleForResultToLabel(lblInfo, true);
            }
            else
            {
                lblInfo.Text = Resources.Taal.Common_Error_UnknownError;
                lblInfo = FindWhere.Utils.Utils.AddStyleForResultToLabel(lblInfo, false);
            }

            //after delete refresh the treeview
            MembersPanelHistory.LoadMembersWithDeviceTypes(new string[] { "4" });
        }

        protected void activeTabChanged(object sender, EventArgs e)
        {
            FindWhere.Utils.Utils.SetModalPopupOutSideHeader(Page, Resources.Taal.Default_Menu_General_MyAccountLow, "Help_Content_MyAccount_" + TabContainer1.ActiveTabIndex);
            Session.Add("MyAccountOpenTab", "Help_Content_MyAccount_" + TabContainer1.ActiveTabIndex);
        }
	}
}
