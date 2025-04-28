using System;
using System.Collections;
using System.Web.UI;
using System.Web.UI.WebControls;
using com.teleca.fleetonline.utils;
using com.teleca.fleetonline.repository;
using System.Resources;
using System.Reflection;
using System.Collections.Generic;
using System.Web;

namespace FindWhere.UserControls
{
	public partial class UserControl_Notifications : FindWhere.UserControls.UserControl_DefaultUserControl
	{
		private string emptyText = "--------------------------------";

		private static Hashtable itemEventParing = new Hashtable();

        protected void Page_Load(object sender, EventArgs e)
		{
			lblInfo.Text = string.Empty;

			if (!alreadyLoaded)
			{
				int userLevel = (int)(Session["UsrLevel"]);
				Boolean ttAdmin = (Boolean)Session["UsrTTAdmin"];

				if (!ttAdmin) SubmitCancel1.HideSubmitButton();

				if (Page.User.Identity.IsAuthenticated)
				{
				}
				initScreen();
			}
		}

		private void initScreen()
		{
			//TODO: in panel 4 zat dit voornamelijk in javascript. Vraag is of we dit volledig met Ajax aan de praat kunnen krijgen...

			//FOLProperties fp = FindWhere.Utils.Utils.GetCachedProperties(Page.StyleSheetTheme, Cache);

			ListItem tempOption = new ListItem((String)GetGlobalResourceObject("Taal", "Common_Option_SelectOption"), "-1");
			ddlEventsel.Items.Add(tempOption);
            ddlEventsel.Items.Add(new ListItem(emptyText, "-1"));

            int nrOfOrder = int.Parse((String)GetGlobalResourceObject("Properties", "Notifications_spec_nrOfOrder")); //4
            
			string[][] lbl = new string[nrOfOrder][]; // 4
            int[][] id = new int[nrOfOrder][];
            for (var i = 0; i < nrOfOrder; i++)
			{
                lbl[i] = new string[nrOfOrder];
				lbl[i][0] = "";
				id[i] = new int[1];
				id[i][0] = -1;
			}

			string usrDevices = (string)Session["UsrDevices"];

			int nrOfItems = int.Parse((String)GetGlobalResourceObject("Properties", "label_notification_item_nrOfItems"));  // gebruikte notificaties
			//17  aantal dropdownregels  Er zijn totaal aantal notificaties  en totaal aantal gebruikte notificaties

			// Groepen Doorlopen
			for (int grp = 0; grp < nrOfOrder + 1; grp++)   // nrOfOrder = aantal groepen
			{
                bool itemAdded = false;
				// Items doorlopen
				for (int itm = 1; itm < nrOfItems + 1; itm++)
				{
					string sreferenceName = (String)GetGlobalResourceObject("Properties", "label_notification_item_" + itm);
					string sgroup = (String)GetGlobalResourceObject("Properties", sreferenceName + "_order");
					                  
                    // only this one from local resources!!!!
                    string slbl = (String)GetLocalResourceObject( sreferenceName + "_txt_pulldown");					                                       
                    string sid = (String)GetGlobalResourceObject("Properties", sreferenceName + "_eventtype");
					string sdeviceTypes = (String)GetGlobalResourceObject("Properties", sreferenceName + "_devicetype");
					try
					{
						itemEventParing.Add(sid, itm);
					}
					catch (Exception ignore) {
                        //already inserted
                    }
					// Hier de zelf ingevulde waardes bij 1000 inlezen.
					if (grp == int.Parse(sgroup))
					{
                        EnforaIO EnforaIO = (EnforaIO)Session["EnforaIO"];
						if (EnforaIO != null)
						{
							if (slbl.Contains("%input1")) slbl = slbl.Replace("%input1", EnforaIO.Input1Name);
							if (slbl.Contains("%input2")) slbl = slbl.Replace("%input2", EnforaIO.Input2Name);
							if (slbl.Contains("%input3")) slbl = slbl.Replace("%input3", EnforaIO.Input3Name);
						}
                        if (availableForDevices(usrDevices, sdeviceTypes))
                        {
                            ddlEventsel.Items.Add(new ListItem(slbl, sid));
                            itemAdded = true;
                        }
					}
					else
					{ }
				}
				// add ---
                if(itemAdded)
				    ddlEventsel.Items.Add(new ListItem(emptyText, "-1"));
			}
		}

        protected bool availableForDevices(string usrDevices, string sdeviceTypes)
        {
            string temp = ";" + sdeviceTypes + ";";
            return (usrDevices.Contains("trimtrac")     && (temp.Contains(";1;")  || sdeviceTypes.Contains(";3;"))) ||
                      (usrDevices.Contains("tt15")      && (temp.Contains(";10;") || sdeviceTypes.Contains(";11;")))||
                      (usrDevices.Contains("ocellus")   && temp.Contains(";4;"))  ||
                      (usrDevices.Contains("wi")        && temp.Contains(";40;")) ||
                      (usrDevices.Contains("enfora")    && temp.Contains(";50;")) ||
                      (usrDevices.Contains("nitro")     && temp.Contains(";51;")) ||
                      (usrDevices.Contains("tm3000")    && temp.Contains(";60;")) ;
        }

		protected void ddlEventsel_SelectedIndexChanged(object sender, EventArgs e)
		{
			string[] memberList = MembersPanelNotifications.GetSelectedMembers();

			string eventType = ddlEventsel.SelectedValue;

			if (ddlEventsel.SelectedIndex > 0 && ddlEventsel.SelectedItem.ToString() != emptyText)
			{
				// fill the memberspanel

				string sreferenceName = (String)GetGlobalResourceObject("Properties", "label_notification_item_" + itemEventParing[ddlEventsel.SelectedValue]);
				// Bepaalt de volgorde. Dus kan vervangen worden door de juiste volgorde van de opgevraagde specs.
				string sgroup = (String)GetGlobalResourceObject("Properties", sreferenceName + "_order");
				// De order is een getal dat de groep aangeeft waartoe hij behoort 1 2 3 of 4 
				string slbl = (String)GetLocalResourceObject(sreferenceName + "_txt_pulldown");
				// Geeft het textveld voor de dropdown.
				string sid = (String)GetGlobalResourceObject("Properties", sreferenceName + "_eventtype");
				// Dit is een No. ID voor Java
				string sdeviceTypes = (String)GetGlobalResourceObject("Properties", sreferenceName + "_devicetype");
				// Het soort apparaat.

				// Fill the menu
				if (sreferenceName != null)
					MembersPanelNotifications.LoadMembersWithDeviceTypes(sdeviceTypes.Split(new char[] { ';' }));
				else
					MembersPanelNotifications.LoadMembersWithDeviceTypes(new string[] { });
                JNetBridge.ReplyClasses.NotificationJavaCallReply NotificationJavaCallReply = JNetBridge.NotificationJavaCall.GetNotifications(GetJavaID(), -1, "", false, "all");
				if (NotificationJavaCallReply.NotificationListDisplayHelper.DataList != null)
				{
                    allNotifications = (com.teleca.fleetonline.repository.NotificationData[])NotificationJavaCallReply.NotificationListDisplayHelper.DataList.ToArray(typeof(com.teleca.fleetonline.repository.NotificationData));
					for (int i = 0; i < allNotifications.Length; i++)
					{
						NotificationData nd = allNotifications[i];
						if (sid == nd.EventType)
						{
							//Current selected notification
							//nd.MemberList 
							MembersPanelNotifications.CheckMembers(nd.MemberList.Split(new char[] { ';' }));
						}
					}
				}
				//fill the gridview
                JNetBridge.ReplyClasses.AccountDetailsJavaCallReply reply = JNetBridge.AccountDetailsJavaCall.Get(GetJavaID());
                LinkedList<ContactData> contacts = reply.LinkedList;
                if(contacts != null)
                    foreach (ContactData contact in contacts)
                        contact.Name = HttpUtility.UrlDecode(contact.Name);

                gvwContacts.DataSource = reply.LinkedList;
				gvwContacts.DataBind();
			}
			else
			{
				MembersPanelNotifications.LoadMembersWithDeviceTypes(new string[] { "" });
				gvwContacts.DataSourceID = String.Empty;
				gvwContacts.DataSource = new int[0];
				gvwContacts.DataBind();
			}

			//reShowModalPopup();
		}

		private com.teleca.fleetonline.repository.NotificationData[] allNotifications;

		protected void gvwContacts_RowDataBound(object sender, GridViewRowEventArgs e)
		{
			string eventType = ddlEventsel.SelectedValue;
			if (e.Row.RowType == DataControlRowType.DataRow)
			{
				ContactData cd = (ContactData)e.Row.DataItem;
				if (allNotifications != null)
				{
					for (int i = 0; i < allNotifications.Length; i++)
					{
                        //TODO: change the state of the checkboxes here
                        if (String.IsNullOrEmpty(cd.Phone))
                        {
                            ((CheckBox)e.Row.FindControl("chkSms")).Visible = false;
                        }
                        // checkbox visible if email is filled
                        if (!FindWhere.Utils.Utils.ValidateEmail(cd.Email))
                        {
                            ((CheckBox)e.Row.FindControl("chkEmail")).Visible = false;
                        }

						NotificationData nd = allNotifications[i];
						if (eventType == nd.EventType)
						{
							if (nd.SmsRcpList != null)
							{
								string[] rps = nd.SmsRcpList.Split(new char[] { ';' });

								//Checked if the current contact must receive a sms
								if (((IList)(nd.SmsRcpList.Split(new char[] { ';' }))).Contains(cd.IndexNr.ToString()))
								{ ((CheckBox)e.Row.FindControl("chkSms")).Checked = true; }
							}
							//Checked if the current contact must receive a Email
							((CheckBox)e.Row.FindControl("chkEmail")).Checked = false;
							if (nd.EmailRcpList != null)
							{
								if (((IList)(nd.EmailRcpList.Split(new char[] { ';' }))).Contains(cd.IndexNr.ToString()))
								{ ((CheckBox)e.Row.FindControl("chkEmail")).Checked = true; }
							}
						}
					}
				}
			}
		}

		protected void OkClicked()
		{
			SaveNotifications();
		}

		protected void OkCloseClicked()
		{
			if (SaveNotifications())
			this.hideModalPopup();
		}

		protected Boolean SaveNotifications()
		{
			if (ddlEventsel.SelectedIndex < 1 || ddlEventsel.SelectedItem.ToString() == emptyText)
			{
				lblInfo.Text = (string)GetLocalResourceObject("Notifications_CS_OkClicked_Empty");
				return false;
			}
			string eventType = ddlEventsel.SelectedValue;
			//string[] members = MembersPanel.GetSelectedMembers();

			string[] memberList = MembersPanelNotifications.GetSelectedMembers();
            int alertOnceStr = 0;
            string fmId = "0";

            ArrayList alEmail = new ArrayList();
            ArrayList alSms = new ArrayList();

            int[] emailList ;
            int[] smsList ;

            JNetBridge.ReplyClasses.AccountDetailsJavaCallReply reply = JNetBridge.AccountDetailsJavaCall.Get(GetJavaID());

            LinkedList<ContactData>contacts = reply.LinkedList;

            ContactData[] contactArray = new ContactData[contacts.Count];
            LinkedListNode<ContactData> node;
            int counter =0;

            for (node = contacts.First; node != null; node = node.Next)
            {
                contactArray[counter++] = node.Value;
            }
                if (memberList.Length == 0)
                {
                    //// Clear receiptents
                    //lblInfo.Text = (string)GetLocalResourceObject("Notifications_CS_OkClicked_Select");
                    ////reload the screen
                    //ddlEventsel_SelectedIndexChanged(this, null);
                    //return false;
                    emailList = (int[])alEmail.ToArray(typeof(int));
                    smsList = (int[])alSms.ToArray(typeof(int));
                }
                else
                {
                    for (int i = 1; i < gvwContacts.Rows.Count + 1; i++)
                    {
                        GridViewRow gvr = gvwContacts.Rows[i - 1];
                        if (((CheckBox)gvr.FindControl("chkEmail")).Checked)
                        {
                            alEmail.Add(contactArray[i - 1].IndexNr);
                            //alEmail.Add(i);
                        }
                        if (((CheckBox)gvr.FindControl("chkSms")).Checked)
                        {
                            alSms.Add(contactArray[i - 1].IndexNr);
                            //alSms.Add(i);
                        }
                    }

                    emailList = (int[])alEmail.ToArray(typeof(int));
                    smsList = (int[])alSms.ToArray(typeof(int));

                    if (emailList.Length == 0 && smsList.Length == 0)
                    {
                        lblInfo.Text = (string)GetLocalResourceObject("Notifications_CS_OkClicked_SelectOne");
                        //reload the screen
                        ddlEventsel_SelectedIndexChanged(this, null);
                        return false;
                    }
                }

			//if (memberList.Length > 0)
            JNetBridge.ReplyClasses.NotificationJavaCallReply NotificationJavaCallReply = JNetBridge.NotificationJavaCall.StoreNotification(GetJavaID(), eventType, emailList, smsList, memberList, fmId, alertOnceStr);

			lblInfo.Text = (string)GetLocalResourceObject("Notifications_CS_OkClicked_Stored");
			//initScreen();
			ddlEventsel_SelectedIndexChanged(this, null);
			return true;
		}
	}
}
