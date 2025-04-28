using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using com.teleca.fleetonline.repository;

namespace FindWhere.UserControls
{
    public partial class UserControl_SendAction : FindWhere.UserControls.UserControl_DefaultUserControl
    {
        private enum ScreenMode { NitroLockDoor, NitroStarterInterrupt, NitroTriggerAlarm }

        private ScreenMode currentScreenmode;

        protected void Page_Load(object sender, EventArgs e)
        {
            lblInfo.Text = string.Empty;

            string[] deviceTypes = this.Attributes["DeviceTypes"].Split(',');
            if (((IList)deviceTypes).Contains("51"))
            {
                switch (this.Attributes["ExtraTypes"])
                {
                    case "1":
                        currentScreenmode = ScreenMode.NitroLockDoor;
                        break;
                    case "2":
                        currentScreenmode = ScreenMode.NitroTriggerAlarm;
                        break;
                    case "3":
                        currentScreenmode = ScreenMode.NitroStarterInterrupt;
                        break;

                }
            }

            if (!alreadyLoaded)
            {
                int userLevel = (int)(Session["UsrLevel"]);
                Boolean ttAdmin = (Boolean)Session["UsrTTAdmin"];

                if (!ttAdmin) SubmitCancel1.HideSubmitButton();

                MembersPanelSendActions.LoadMembersWithDeviceTypes(deviceTypes);

                switch (currentScreenmode)
                {
                    case ScreenMode.NitroLockDoor:
                        initScreenNitro();
                        break;
                    case ScreenMode.NitroStarterInterrupt:
                        initScreenNitro();
                        break;
                    case ScreenMode.NitroTriggerAlarm:
                        initScreenNitro();
                        break;
                }
                if (currentScreenmode == ScreenMode.NitroStarterInterrupt)
                {
                    pnlActionOneTwo.Visible = false;
                    pnlActionThree.Visible = true;
                }
                else
                {
                    pnlActionOneTwo.Visible = true;
                    pnlActionThree.Visible = false;
                }

                //this.SubmitCancel1.HideSubmitButton();

            }
        }

        private void initScreenNitro()
        {

            JNetBridge.ReplyClasses.EnforaJavaCallReply reply = JNetBridge.EnforaJavaCall.GetConfig(GetJavaID());

            LinkedList<MemberData> FilteredMemberList = new LinkedList<MemberData>();
            foreach (MemberData md in reply.LinkedList)
            {
                if (md.UserType == 51)
                    FilteredMemberList.AddLast(md);
            }

            ddlSelectDevice.DataSource = FilteredMemberList;
            ddlSelectDevice.DataBind();

            ddlSelectDeviceSelectedIndexChanged(null, null);

        }

        protected void OkClicked()
        {
            switch (currentScreenmode)
            {
                case ScreenMode.NitroLockDoor:
                    SendActions();
                    break;
                case ScreenMode.NitroTriggerAlarm:
                    SendActions();
                    break;
                case ScreenMode.NitroStarterInterrupt:
                    SaveTriggerInterrupt();
                    break;
            }

        }

        private Boolean SaveTriggerInterrupt()
        {
            string userId = ddlSelectDevice.SelectedValue;

            if (ddlIO3.SelectedIndex == 0)
            {
                lblInfo.Text = (String)GetLocalResourceObject("SendAction_NoStateSelected");
                return false;
            }

            Boolean Succes = false;
            string IOValue3 = ddlIO3.SelectedValue;
            JNetBridge.ReplyClasses.EnforaJavaCallReply repNitro = JNetBridge.EnforaJavaCall.SaveConfig(GetJavaID(), userId, -1, 0, -1, "", "", "", "", "", IOValue3);

            if (repNitro.Confirm != null && repNitro.Confirm.ToLower() == "ok")
            {
                lblInfo.Text = Resources.Taal.Common_Message_RequestSavedSettingsNextContactUnit;
                Succes = true;
                lblInfo = FindWhere.Utils.Utils.AddStyleForResultToLabel(lblInfo, true);
            }
            else
            {
                Succes = CommonErrorMessages(Succes, repNitro.Error);
            }
            return Succes;

        }

        protected void OkCloseClicked()
        {
            Boolean result = false;
            switch (currentScreenmode)
            {
                case ScreenMode.NitroLockDoor:
                    result = SendActions();
                    break;
                case ScreenMode.NitroTriggerAlarm:
                    result = SendActions();
                    break;
                case ScreenMode.NitroStarterInterrupt:
                    result = SaveTriggerInterrupt();
                    break;
            }

            if (result)
                this.hideModalPopup();
        }

        private bool SendActions()
        {
            //
            int IOID = int.Parse(this.Attributes["ExtraTypes"]);

            bool Succes = true;

            foreach (string userId in MembersPanelSendActions.GetSelectedMembers())
            {
                JNetBridge.ReplyClasses.EnforaJavaCallReply repNitro = JNetBridge.EnforaJavaCall.SaveConfig(GetJavaID(), userId, -1, 0, -1, "", "", "", "", "", "", IOID);


                if (repNitro.Confirm != null && repNitro.Confirm.ToLower() == "ok")
                {
                    lblInfo.Text = Resources.Taal.Common_Message_RequestSavedSettingsNextContactUnit;
                    Succes = true;
                    lblInfo = FindWhere.Utils.Utils.AddStyleForResultToLabel(lblInfo, true);
                }
                else
                {
                    Succes = CommonErrorMessages(Succes, repNitro.Error);
                }

            }

            return true;
        }

        protected void ddlSelectDeviceSelectedIndexChanged(object sender, EventArgs e)
        {
            switch (currentScreenmode)
            {

                case ScreenMode.NitroLockDoor:
                case ScreenMode.NitroTriggerAlarm:
                case ScreenMode.NitroStarterInterrupt:
                    #region nitro

                    string userId = ddlSelectDevice.SelectedValue;



                    // get the enforaconfig for the current member
                    JNetBridge.ReplyClasses.EnforaJavaCallReply reply = JNetBridge.EnforaJavaCall.GetConfig(GetJavaID(), userId);

                    string IntDailyValue = reply.EnforaConfigDBObject.Curr__IntDaily.ToString();
                    string IntDailyName = "Unknown";


                    string[] intDailyOptionNamesArray = (Resources.Taal.Nitroconfig_OptionNamesArray).Split(',');
                    string[] intDailyOptionValuesArray = (Resources.Properties.Nitroconfig_OptionValuesArray).Split(',');

                    // Find textual representation for int value
                    for (int teller = 0; teller < intDailyOptionNamesArray.Length; teller++)
                    {
                        if (IntDailyValue == intDailyOptionValuesArray[teller])
                        {
                            IntDailyName = intDailyOptionNamesArray[teller];
                            break;
                        }
                    }

                    string IntNoGPSValue = reply.EnforaConfigDBObject.Curr__IntNoGPS.ToString();
                    string IntNoGPSName = "Unknown";

                    // Find textual representation for int value
                    for (int teller = 0; teller < intDailyOptionNamesArray.Length; teller++)
                    {
                        if (IntNoGPSValue == intDailyOptionValuesArray[teller])
                        {
                            IntNoGPSName = intDailyOptionNamesArray[teller];
                            break;
                        }
                    }

                    // Fill labels with current value

                    LabelIO3.Text = getOnOffDexcription(reply.EnforaConfigDBObject.Curr__Input3);

                    ddlIO3.SelectedIndex = 0;


                    //reShowModalPopup();
                    #endregion
                    break;
            }
        }

        //private string getOnOffDexcription(int p)
        //{
        //    switch (p)
        //    {
        //        case 0:
        //            return "";
        //        case 1:
        //            return ddlIO3.Items[1].Text;
        //        case 2:
        //            return ddlIO3.Items[2].Text;
        //        default:
        //            return "";

        //    }
        //}

        private string getOnOffDexcription(int p)
        {
            switch (p)
            {
                case 0:
                    return ddlIO3.Items[2].Text;
                case 1:
                    return ddlIO3.Items[1].Text;
                default:
                    return "";

            }
        }

        private bool CommonErrorMessages(bool Succes, string replyError)
        {
            Succes = false;
            if (replyError == null) replyError = "";
            switch (replyError.ToLower())
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
                    lblInfo.Text = string.Concat(Resources.Taal.Common_Error_UnknownError, replyError.ToLower());
                    break;
            }
            lblInfo = FindWhere.Utils.Utils.AddStyleForResultToLabel(lblInfo, false);
            return Succes;
        }

    }
}
