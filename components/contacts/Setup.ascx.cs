using System;
using System.Web.UI;
using AjaxControlToolkit;
using JNetBridge.ReplyClasses;

namespace FindWhere.UserControls
{
	public partial class UserControl_Setup : FindWhere.UserControls.UserControl_DefaultUserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SubmitCancel1.HideOkCloseButton();
            SubmitCancel1.HideCancelButton();

            lblInfo.Text = string.Empty;

            //if (!Page.IsPostBack)
            //{
                

                //fillTimeZoneDropDown();
            //}

            if (this.Visible && ddlSelectTimeZone.Items.Count == 0)
                fillTimeZoneDropDown();

        }

        public void fillTimeZoneDropDown()
        {
            AccountDetailsJavaCallReply accountData = JNetBridge.AccountDetailsJavaCall.Get(GetJavaID());

            ddlSelectTimeZone.DataSource = accountData.AccountDetailsDisplayHelper.TimeZoneIDS;
            ddlSelectTimeZone.SelectedValue = accountData.AccountDetailsDisplayHelper.TimeZoneId;
            ddlSelectTimeZone.DataBind();
        }


        protected void OkClicked()
        {
            if (MyAccount_Text_Tab_Personal_PassWordConfirm.Text.Length > 5 && MyAccount_Text_Tab_Personal_PassWordConfirm.Text == txtPassword.Text)
            {
                string timeZone = ddlSelectTimeZone.SelectedValue.ToString();

                ////Store the first Tab
                JNetBridge.ReplyClasses.StorePasswordAndTimezoneJavaCalReply rep = JNetBridge.StorePasswordAndTimezoneJavaCal.Store(GetJavaID(), MyAccount_Text_Tab_Personal_PassWordConfirm.Text, timeZone);


                if (string.IsNullOrEmpty(rep.Error))
                {
                    // now login withe the new password!
                    JNetBridge.ReplyClasses.LoginJavaCallReply loginAttempt = new LoginJavaCallReply();
                    try
                    {
                        string userName = (string)Session["UsrName"];
                        string userName2 = Session["UsrName"].ToString();
                        JNetBridge.Classes.JnetBridgeLoginUnit loginUnit = new JNetBridge.Classes.JnetBridgeLoginUnit(null, Page.StyleSheetTheme);
                        loginAttempt = JNetBridge.LoginJavaCall.DoLogin(loginUnit,(string)Session["UsrName"], MyAccount_Text_Tab_Personal_PassWordConfirm.Text);
                        if (loginAttempt == null) // Not Null => User has been Authenticated!
                            return;
                    }
                    catch (Exception ex)
                    {
                        lblInfo.Text = "Action Failed";
                        lblInfo = FindWhere.Utils.Utils.AddStyleForResultToLabel(lblInfo, false);

                    }
                    FindWhere.Utils.Utils.StoreLoginAttemptInSession(this.Session, this.Cache, loginAttempt, (string)Session["UsrName"], MyAccount_Text_Tab_Personal_PassWordConfirm.Text, Page.StyleSheetTheme,Request.QueryString["Language"]);


                    Session["ShowSetup"] = "false";

                    this.Visible = false;
                    ModalPopupExtender ModalPopupOutside = (ModalPopupExtender)Page.FindControl("ModalPopupOutside");
                    ModalPopupOutside.Hide();
                    FindWhere.Utils.Utils.ShowNextPopup(this.Session, this.Page);

                    //string controlToShow = FindWhere.Utils.Utils.NextPopupControlName(this.Session);

                    //if (controlToShow != null)
                    //{
                    //    Control ctrl = this.Page.FindControl(controlToShow);
                    //    ctrl.Visible = true;
                    //    Panel pnlOutside = (Panel)this.Page.FindControl("pnlOutside");
                    //    pnlOutside.Width = FindWhere.Utils.Utils.GetControlWidth(controlToShow);
                    //}
                }
                else
                {
                    lblInfo.Text = "Action Failed";
                    lblInfo = FindWhere.Utils.Utils.AddStyleForResultToLabel(lblInfo, false);
                }
            }
            else
            {
                lblInfo.Text = "Password error: Input is not same or too short (min 6 characters.)";
                lblInfo = FindWhere.Utils.Utils.AddStyleForResultToLabel(lblInfo, false);
            }
            ////settings stored!!!
            ////todo: show message
        }
    }
}