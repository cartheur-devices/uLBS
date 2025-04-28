using System.Text;
using System;
using System.Security.Cryptography;
using JNetBridge.ReplyClasses;
using System.Web.UI.WebControls;
using System.IO;
using System.Net.Mail;
using System.Web.Configuration;
using System.Text.RegularExpressions;
using JNetBridge.InteractionClasses;
using System.Collections;
using com.teleca.fleetonline.repository;
using System.Web.UI;
using AjaxControlToolkit;
using System.Web.Security;
using System.Collections.Generic;
using com.teleca.fleetonline.utils;
using System.Reflection;
using System.Resources;
using System.Globalization;
using System.Web;

namespace FindWhere.Utils
{
    public enum Theme { Fancy, Omega, Mogo, HLS, Farsi, Qinetiq }

    public class Utils
    {

        /// <summary>
        /// Calculate Password Hash SHA256
        /// </summary>
        public string GetSHA256Hash(string txtToHash)
        {
            string encString = string.Empty;
            ASCIIEncoding ascEncode = new ASCIIEncoding();
            byte[] HashValue, MessageBytes = ascEncode.GetBytes(txtToHash);
            SHA256Managed shaHash = new SHA256Managed();
            HashValue = shaHash.ComputeHash(MessageBytes);
            foreach (byte b in HashValue)
            {
                encString += String.Format("{0:x2}", b);
            }
            return encString;
        }

        public static Boolean ValidateEmail(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                return false;
            }
            return (!Regex.IsMatch(email, @"^[a-zA-Z]([.]?([[:alnum:]_-]+)*)?@([[:alnum:]\-_]+\.)+[a-zA-Z]{2,4}$"));


        }
        /// <summary>
        /// Store the LoginAttempt in the session
        /// </summary>
        /// <param name="loginAttempt"></param>
        public static void StoreLoginAttemptInSession(System.Web.SessionState.HttpSessionState session, System.Web.Caching.Cache cache, LoginJavaCallReply loginAttempt, string username, string password, string styleSheetThem, string querystringLanguage)
        {
            session["netCookieJavaSessionID"] = new JNetBridge.Classes.JnetBridgeLoginUnit(loginAttempt.JsessionID, styleSheetThem);
            
            string minBalanceMembers = "";
            bool memberFound = false;
            if (loginAttempt.MemberBalances != null)
            {               
                minBalanceMembers += "<div><table width=100% border='1'>";
                string labelMember = Resources.Taal.Common_Device; // "Member"; // folProp.getProperty("label.member");
                string labelBalance = Resources.Taal.Default_Balance; // "Balance"; // folProp.getProperty("label.balance");
                minBalanceMembers += "<tr><td><b>" + labelMember + "</b></td><td><b>" + labelBalance + "</b></td><td>&nbsp;</td></tr>";
                foreach (MemberBalanceData data in loginAttempt.MemberBalances)
                {                    
                    if (data.getBalance() <= loginAttempt.ImplementerDataBean.getMIN_CREDIT_VALUE())
                    {
                        minBalanceMembers += "<tr><td width=50%>" + data.getAlias() + "</td><td width=50%>" + String.Format("{0:N2}", data.getBalance() / 100) + "</td>" +
                            "<td><input type='radio' name='msisdn' id='msisdn' value='~" + data.getFmId() + "~' checked></td></tr>";
                        memberFound = true;
                    }
                }                
            }

            session["UsrPass"] = password;
            session["UsrName"] = username;

            UserData objUserData = loginAttempt.UsrData;

            session["UsrData"] = loginAttempt.UsrData;

            session["UsrID"] = objUserData.Uid;
            session["UsrLanguage"] = objUserData.Language;
            session["UsrCountry"] = objUserData.Country;
            session["UsrTimeZoneId"] = objUserData.TimeZoneId;
            session["UsrMsisdn"] = objUserData.Msisdn;
            session["UsrCurrentProfile"] = objUserData.CurrentProfile;
            session["UsrGeocodingAvailable"] = objUserData.GeocodingAvailable;
            session["UsrActive"] = objUserData.Active;
            session["UsrFo"] = objUserData.Fo;
            session["UsrGetMispos"] = objUserData.GetMispos;
            session["UsrLongUserTimeout"] = objUserData.LongUserTimeout;
            session["UsrMembertype"] = objUserData.Membertype;
            session["UsrNewProfile"] = objUserData.NewProfile;
            session["UsrOnDemand"] = objUserData.OnDemand;
            session["UsrOperator"] = objUserData.Operator;
            session["UsrUidSet"] = objUserData.UidSet;
            session["EnforaIO"] = loginAttempt.EnforaIO;

            if (loginAttempt.UserDistance == "1")
                session["DistanceUnit"] = Resources.Taal.Common_Value_MLHour; //Common_Value_MLHour
            else
                session["DistanceUnit"] = Resources.Taal.Common_Value_KmHour; //Common_Value_KmHour

            session["UsrLevel"] = loginAttempt.Level;                   // used

            if (loginAttempt.TT_USER)
                session["UsrTTAdmin"] = false;
            else
                session["UsrTTAdmin"] = true;
            session["UsrTimeout"] = objUserData.UserTimeout;
#if DEBUG
         if (System.Configuration.ConfigurationSettings.AppSettings["USRTIMEOUT"] != null)
                session["USRTIMEOUT"] = int.Parse( System.Configuration.ConfigurationSettings.AppSettings["USRTIMEOUT"].ToString());      
        if (System.Configuration.ConfigurationSettings.AppSettings["USERLEVEL"] != null)
                session["UsrLevel"] = int.Parse(System.Configuration.ConfigurationSettings.AppSettings["USERLEVEL"]);  
             if (System.Configuration.ConfigurationSettings.AppSettings["TTADMIN"] != null)
                session["UsrTTAdmin"] = Boolean.Parse(System.Configuration.ConfigurationSettings.AppSettings["TTADMIN"]);  
#else
#endif

            session["UsrType"] = objUserData.UserType;
            session["MapLabels"] = loginAttempt.Map_labels;


            SetSessionLanguage(session, querystringLanguage);

            // remove the owner @ UsrGroup

            foreach (com.teleca.fleetonline.web.bean.GroupsAndMembersData gmd in loginAttempt.Group)
            {
                ArrayList newList = new ArrayList();

                foreach (MemberData md in gmd.AllMembers)
                {
#if DEBUG
                    if (md.Userid == "1787")
                        md.UserType = 6;
                            
#endif

                    if (md.UserType != 0)
                        newList.Add(md);

                    //replace userid by msisdn in the topup window
                    if (minBalanceMembers.Length > 0)
                    {                        
                        string searchStr = "~" + md.Userid + "~";
                        if (minBalanceMembers.IndexOf(searchStr) > -1)                        
                            minBalanceMembers = minBalanceMembers.Replace(searchStr,md.Msisdn);                                                   
                    }
                }
                gmd.AllMembers = newList;
            }

            if (memberFound)            
                session["Fleetonline_error_content"] = string.Concat(Resources.Taal.Default_BalancePopup_NoCreditsWarning, minBalanceMembers.ToString(), "</table></div>");


            //decode alisas
            ArrayList members = loginAttempt.Group[0].AllMembers;
            if (members != null)
            {
                foreach(MemberData member in members) {
                    member.Alias = HttpUtility.UrlDecode(member.Alias);
                }
            }

            //decode group names

            Dictionary<int, string> temp = new Dictionary<int, string>();
            if (loginAttempt.Group[0].Groupnames != null)
            {
                foreach (KeyValuePair<int, string> kvp in (Dictionary<int, string>)loginAttempt.Group[0].Groupnames)
                    temp.Add(kvp.Key, HttpUtility.UrlDecode(kvp.Value));
                loginAttempt.Group[0].Groupnames = temp;
            }

            session["UsrGroup"] = loginAttempt.Group;                  // used
            session["UsrMap"] = loginAttempt.Map_implementation;        // used

            if (string.IsNullOrEmpty(loginAttempt.NotifierRead))
                session["NotifierRead"] = false;        // used
            else
                session["NotifierRead"] = true;        // used
            session["ShowSetup"] = loginAttempt.ShowSetup;              // used

            StringBuilder usrDevices = new StringBuilder();
            if (loginAttempt != null)
            {
                if (loginAttempt.Enfora)
                    usrDevices.Append("|enfora");
                if (loginAttempt.Gsm)
                    usrDevices.Append("|gsm");
                if (loginAttempt.Ocellus)
                    usrDevices.Append("|ocellus");
                if (loginAttempt.Trimtrac)
                    usrDevices.Append("|trimtrac");
                if (loginAttempt.Wi)
                    usrDevices.Append("|wi");
                if (loginAttempt.Tt15)
                    usrDevices.Append("|tt15");
                if (loginAttempt.Nitro)
                    usrDevices.Append("|nitro");
                if (loginAttempt.Tm3000)
                    usrDevices.Append("|tm3000");               
            }
            session["UsrDevices"] = usrDevices.ToString();    // used           
        }

        public static void SetSessionLanguage(System.Web.SessionState.HttpSessionState session, string querystringLanguage)
        {
            string cultureFromWebBrowser = string.Empty;

            if (WebConfigurationManager.AppSettings["UserCulture"] == null)	// Value from webconfig has to come from database !.
            {
                cultureFromWebBrowser = System.Threading.Thread.CurrentThread.CurrentUICulture.ToString();   // Value of the browser.
            }
            else
                cultureFromWebBrowser = WebConfigurationManager.AppSettings["UserCulture"];

            if (querystringLanguage == null)
                querystringLanguage = System.Configuration.ConfigurationSettings.AppSettings["UserCulture"];

            if (querystringLanguage == null)
            {
                //overrule webbrowser-language with querystring language            
                switch (querystringLanguage.ToLower())
                {
                    case "nl":
                        cultureFromWebBrowser = "nl-NL";
                        break;
                    case "es":
                        cultureFromWebBrowser = "es-ES";
                        break;
                    case "ar-eg":
                        cultureFromWebBrowser = "ar-eg";
                        break;
                    case "en-US":
                        cultureFromWebBrowser = "en-US";
                        break;
                    case "fr-FR":
                        cultureFromWebBrowser = "fr_FR";
                        break;
                    default:
                        cultureFromWebBrowser = "en-GB";
                        break;
                }
            }

            session["UserCulture"] = cultureFromWebBrowser;

            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo(session["UserCulture"].ToString());
            System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(session["UserCulture"].ToString());
        }

        public static string NextPopupControlName(System.Web.SessionState.HttpSessionState session)
        {
            if (session["ShowSetup"] != null)
            {
                string showSetup = (string)session["ShowSetup"];
                if (showSetup != "false")
                {
                    return "ucSetup";
                }
            }

            if (session["NotifierRead"] != null)
            {
                if ((Boolean)session["NotifierRead"] == false)
                {
                    return "ucNotifier";
                }
            }
            if (session["Fleetonline_error_content"] != null)
            {
                return "ucNotification";
            }

            return null;
        }

        public static Unit GetControlWidth(string controlName)
        {
            switch (controlName)
            {
                case "ucSetup":
                    return Unit.Pixel(600);

                case "ucNotifier":
                    return Unit.Pixel(750);

                case "ucNotification":
                    return Unit.Pixel(600);
            }
            return Unit.Pixel(500);
        }

        public static String GetControlHeaderText(System.Web.SessionState.HttpSessionState session, string controlName)
        {
            switch (controlName)
            {
                case "ucSetup":
                    return GetResource(session, "Default_Header_PopUpSetup", "taal");

                case "ucNotifier":
                    return GetResource(session, "Default_Header_PopUpNotifier", "taal");

                case "ucNotification":
                    return GetResource(session, "Default_Header_PopUpNotification", "taal");
            }
            return "";

        }

        public static String GetControlHelpText(string controlName)
        {
            switch (controlName)
            {
                case "ucSetup":
                    return "Help_Content_Setup";

                case "ucNotifier":
                    return "Help_Content_Notifier";

                case "ucNotification":
                    return "Help_Content_Notification";
            }
            return "";

        }

        public static void ShowNextPopup(System.Web.SessionState.HttpSessionState session, System.Web.UI.Page page)
        {
            string controlToShow = FindWhere.Utils.Utils.NextPopupControlName(session);

            if (controlToShow != null)
            {
                Control ctrl = page.FindControl(controlToShow);
                ctrl.Visible = true;
                System.Web.UI.WebControls.Panel pnlOutside = (System.Web.UI.WebControls.Panel)page.FindControl("pnlOutside");
                pnlOutside.Width = FindWhere.Utils.Utils.GetControlWidth(controlToShow);

                SetModalPopupOutSideHeader(page, GetControlHeaderText(session, controlToShow), GetControlHelpText(controlToShow));

                ShowHeaderImageButton(page, false);

                ModalPopupExtender ModalPopupOutside = (ModalPopupExtender)page.FindControl("ModalPopupOutside");
                ModalPopupOutside.Show();

                UpdatePanel UpdatePanelOutside = (UpdatePanel)page.FindControl("UpdatePanelOutside");
                UpdatePanelOutside.Update();
            }
            else
            {
                ShowHeaderImageButton(page, true);
            }
        }

        public static void SetModalPopupOutSideHeader(System.Web.UI.Page page, string headerText, string helptext)
        {
            ImageButton ImageButtonHeaderHelp = (ImageButton)page.FindControl("ImageButtonHeaderHelp");
            Label lblFromOutsideDrag = (Label)page.FindControl("lblFromOutsideDrag");

            lblFromOutsideDrag.Text = headerText;
            if (!string.IsNullOrEmpty(helptext))
                ImageButtonHeaderHelp.OnClientClick = string.Concat("GetHelp('", helptext, "');");
            else
                ImageButtonHeaderHelp.OnClientClick = "alert('Help needs to be developed.');";
        }

        public static void ValidateTimeoutFromWebpage(System.Web.SessionState.HttpSessionState session, System.Web.UI.Page page, Boolean UpdateTimer)
        {
            if (!validateSession(session))
            {
                // Clear session
                FormsAuthentication.SignOut();
                page.Response.Cookies[FormsAuthentication.FormsCookieName].Expires = DateTime.Now.AddDays(-999);
                session.Abandon();

                page.Response.Redirect("Login.aspx");
            }
            if (UpdateTimer)
            {
                SetLastUserActionTime(session, page);
            }
        }

        public static void ShowHeaderImageButton(System.Web.UI.Page page, Boolean visible)
        {
            ImageButton ImageButtonHeaderClose = (ImageButton)page.FindControl("ImageButtonHeaderClose");
            ImageButtonHeaderClose.Visible = visible;

            string script = " var ImageButtonHeaderCloseVisible = " + visible.ToString().ToLower() + ";";
            ScriptManager.RegisterClientScriptBlock(page, typeof(Page), "ImageButtonHeaderCloseVisible", script, true);
        }

        /// <summary>
        /// make the clientside get the right seconds to wait for
        /// </summary>
        public static void SetLastUserActionTime(System.Web.SessionState.HttpSessionState session, System.Web.UI.Page page)
        {
            session["LastUserActionTime"] = DateTime.Now;
            int seconds = (int)session["UsrTimeout"] * 60;
            string scriptText = @" SecondsBeforeTimeout = " + seconds.ToString() + " ; ";
            ScriptManager.RegisterClientScriptBlock(page, typeof(Page), "SecondsBeforeTimeoutScript", scriptText, true);
        }

        public static void RefreshComPanel(System.Web.UI.Page page)
        {
            string scriptText = @" Default_Button_Header_Refresh.click();";
            ScriptManager.RegisterClientScriptBlock(page, typeof(Page), "RefreshComPanel", scriptText, true);
        }


        public static Boolean ValidateTimeoutFromWebservice(System.Web.SessionState.HttpSessionState session)
        {
            // using the webservice we have to reset the timer clientside
            return validateSession(session);
        }

        private static Boolean validateSession(System.Web.SessionState.HttpSessionState Session)
        {
            DateTime lastAction = DateTime.MinValue; ;
            if (Session["LastUserActionTime"] != null)
                lastAction = (DateTime)Session["LastUserActionTime"];
            int x = DateTime.Compare(lastAction.AddMinutes((int)Session["UsrTimeout"]), DateTime.Now);
            //int x = DateTime.Compare(lastAction.AddMinutes(2), DateTime.Now);
            if (x < 0)
            {
                Session.Clear();
                return false;
            }

            Session["LastUserActionTime"] = DateTime.Now;
            return true;
        }

        public static string GetUserAlias(System.Web.SessionState.HttpSessionState Session, string fmid)
        {
            List<com.teleca.fleetonline.web.bean.GroupsAndMembersData> gmds = (List<com.teleca.fleetonline.web.bean.GroupsAndMembersData>)Session["UsrGroup"];

            foreach (com.teleca.fleetonline.web.bean.GroupsAndMembersData gmd in gmds)
            {
                foreach (MemberData mdAll in gmd.AllMembers)
                {
                    if (mdAll.Userid == fmid)
                        return mdAll.Alias;
                }
            }

            return "";
        }

        public static MemberData GetMemberByFmID(System.Web.SessionState.HttpSessionState Session, string fmid)
        {
            List<com.teleca.fleetonline.web.bean.GroupsAndMembersData> gmds = (List<com.teleca.fleetonline.web.bean.GroupsAndMembersData>)Session["UsrGroup"];

            foreach (com.teleca.fleetonline.web.bean.GroupsAndMembersData gmd in gmds)
            {
                foreach (MemberData mdAll in gmd.AllMembers)
                {
                    if (mdAll.Userid == fmid)
                        return mdAll;
                }
            }

            return null;
        }

        public static MemberData GetMemberDataFromSession(System.Web.SessionState.HttpSessionState Session, string fmId)
        {
            List<com.teleca.fleetonline.web.bean.GroupsAndMembersData> gmds = (List<com.teleca.fleetonline.web.bean.GroupsAndMembersData>)Session["UsrGroup"];

            foreach (com.teleca.fleetonline.web.bean.GroupsAndMembersData gmd in gmds)
            {
                foreach (MemberData mdAll in gmd.AllMembers)
                {
                    if (mdAll.Userid == fmId)
                        return mdAll;
                }
            }

            return null;

        }

        #region Commented out
        // /// <summary>
        // /// Do we have a device with one of the following types?
        // /// </summary>
        // /// <param name="Session"></param>
        // /// <param name="memberTypes"></param>
        // /// <returns></returns>
        // public static Boolean GetMemberCountForDeviceTypes(System.Web.SessionState.HttpSessionState Session, string[] deviceTypes)
        // {
        //     Boolean result = false;
        //     List<com.teleca.fleetonline.web.bean.GroupsAndMembersData> gmds = (List<com.teleca.fleetonline.web.bean.GroupsAndMembersData>)Session["UsrGroup"];

        //     foreach (com.teleca.fleetonline.web.bean.GroupsAndMembersData gmd in gmds)
        //     {
        //         foreach (MemberData mdAll in gmd.AllMembers)
        //         {
        //             foreach(string deviceType in deviceTypes)

        //             {
        //                 if (((IList)deviceTypes).Contains(deviceType))
        //                 {
        //                     result = true;
        //                 }
        //             }
        //         }
        //     }
        //     return result;
        //}
        #endregion

        public static FOLProperties GetCachedProperties(String styleSheetName, System.Web.Caching.Cache cache)
        {
            System.Diagnostics.Debug.WriteLine(string.Concat("GetCachedProperties Stylesheetname: ", styleSheetName));
            return (FOLProperties)cache[GetPropertiesCachName(styleSheetName)];

        }

        public static string GetPropertiesCachName(String styleSheetName)
        {
            string cacheKey = string.Concat("folproperties", styleSheetName);

            return cacheKey;
        }

        public static string GetSessionParams(System.Web.SessionState.HttpSessionState session)
        {
            StringBuilder ses = new StringBuilder();
            ses.Append("Session Params:" + Environment.NewLine);
            if (session != null)
            {
                for (int i = 0; i < session.Count; i++)
                {
                    ses.Append(session.Keys[i].ToString() + "=");
                    if (session[i] != null) ses.Append(session[i].ToString());
                    ses.Append(Environment.NewLine);
                }
            }
            return ses.ToString();

        }
        /// <summary>
        /// Get a resourcevalue from resourcefile
        /// </summary>
        /// <param name="resourceKey"></param>
        /// <param name="CurrentTheme"></param>
        /// <returns></returns>
        public static string GetResource(System.Web.SessionState.HttpSessionState session, string resourceKey, string resourceName)
        {
            Type type = typeof(System.Web.Compilation.BuildManager);

            PropertyInfo propertyInfo = type.GetProperty("AppResourcesAssembly",
            BindingFlags.Static |
            BindingFlags.GetField |
            BindingFlags.NonPublic);

            Assembly assembly = (Assembly)propertyInfo.GetValue(null, null);

            ResourceManager manager = new ResourceManager(string.Concat("resources.", resourceName.ToLower()), assembly);

            //ResourceSet resources = manager.GetResourceSet(System.Globalization.CultureInfo.CurrentUICulture, true, true);
            ResourceSet resources = manager.GetResourceSet(new System.Globalization.CultureInfo(session["UserCulture"].ToString()), true, true);

            return resources.GetString(resourceKey);
        }

        public static string getImgUrlByUserType(HttpRequest request, int iconId)
        {
            string imgUrl = string.Empty;

            //var set = iconId > 40 ? 21 : iconId > 30 ? 14 : iconId > 20 ? 10 : 8;

            string set = "0";
            if (iconId.ToString().Length == 2)
            {
                set = iconId.ToString()[0].ToString();
            }
            if (iconId.ToString().Length == 3)
            {
                set = string.Concat(iconId.ToString()[0].ToString(), iconId.ToString()[1].ToString());
            }


            // Determine Image url based on Theme
            imgUrl = string.Concat("App_Themes/Marker/set", set, "/1/0.png");

            string s = request.Url.AbsoluteUri;

            //TODO: menu rebuilden ;)
            if (s.Contains("/Set/")) imgUrl = string.Concat("../", imgUrl);
            return imgUrl;
        }

        public static string GetAlertImage(string alertInfo)
        {
            //wi msg contains "_x"
            if (alertInfo.IndexOf('_') > -1)
                alertInfo = alertInfo.Substring(0, alertInfo.IndexOf('_'));

            switch (alertInfo)
            {
                // Panic   
                case "WI1":
                    return @"Misc/Graphics/PNGs/Status/information.png";
                // Speed   
                case "WI2":
                case "TT2":
                    return @"Misc/Graphics/PNGs/Status/car_compact_orange.png";
                // Shutdown   
                case "WI3":
                    return @"Misc/Graphics/PNGs/Status/stop.png";
                // Battery message   
                case "WI4":
                    return @"Misc/Graphics/PNGs/Status/battery.png";
                // GO   
                case "GO":
                    return @"Misc/Graphics/PNGs/Status/car_compact_orange.png";
                // Stop   
                case "ST":
                    return @"Misc/Graphics/PNGs/Status/stop.png";
                // Timed   
                case "TI":
                    return @"Misc/Graphics/PNGs/Status/information.png";
                // Motion   
                case "MO":
                case "EN102":
                    return @"Misc/Graphics/PNGs/Status/gears_run.png";

                // Geofence violoation  
                case "TT1":
                    return @"Misc/Graphics/PNGs/Status/find_selection.png";
                // Scheduled hours violation  
                case "TT3":
                    return @"Misc/Graphics/PNGs/Status/information.png";
                // Runtime meter violation  
                case "TT4":
                    return @"Misc/Graphics/PNGs/Status/car_compact_orange.png";
                // Geofence violation lpa centered  
                case "TT5":
                    return @"Misc/Graphics/PNGs/Status/find_selection.png";
                // Geofence  
                case "EN21":
                case "EN22":
                    return @"Misc/Graphics/PNGs/Status/find_selection.png";
                default:
                    return @"Misc/Graphics/PNGs/Status/Empty.png"; ;
            }
        }

        public static Label AddStyleForResultToLabel(Label inputLabel, Boolean success)
        {
            if (success)
            {
                inputLabel.ForeColor = System.Drawing.Color.Green;
            }
            else
            {
                inputLabel.ForeColor = System.Drawing.Color.Red;
            }

            return inputLabel;
        }


        public static string GetThemeNameFromSession(System.Web.SessionState.HttpSessionState session)
        {
            if (session["ThemeToUse"] != null)
            {
                Theme currentTheme = (Theme)session["ThemeToUse"];
                return currentTheme.ToString();
            }
            else
                return null;


        }

    }

	public class Mail
	{
		public void SendMail(string emailTo, string emailFrom, string filePad, string fromName, string subject, StringBuilder bodyString)
		{
			string messageHeader = "127.0.0.1";    //Request.ServerVariables["REMOTE_ADDR"];
			object providerUserKey = Guid.NewGuid();
			string messageID = Convert.ToString(providerUserKey) + ".support@findwhere.weboffices.nl";

			if (!string.IsNullOrEmpty(filePad))
			{
				StreamReader reader = new StreamReader(filePad);
				bodyString.Append(reader.ReadToEnd());
				reader.Close();
			}

			MailAddress adresTo = new MailAddress(emailTo);
			MailAddress adresFrom = new MailAddress(emailFrom, fromName);

			MailMessage message = new MailMessage(adresFrom, adresTo);
			message.Body = bodyString.ToString();
			message.Subject = subject;

			message.Headers.Add("Originating-IP", messageHeader);
			message.Headers.Add("Message-ID", messageID);
			SmtpClient client = new SmtpClient(WebConfigurationManager.AppSettings["MailHost"]);
			client.UseDefaultCredentials = true;
			client.Send(message);
			message.Dispose();
		}
	}
    
	public class ThemeToUse
	{

        public static Theme ToUseTheme(string queryString, string httpHostString)
		{

            if (String.Compare(System.Configuration.ConfigurationSettings.AppSettings["UseThemeQueryString"], "true") == 0)
            {
                if (!String.IsNullOrEmpty(queryString))
                {
                    foreach (string item in Enum.GetNames(typeof(Theme)))
                    {
                        Theme current = (Theme)Enum.Parse(typeof(Theme), item);
                        if (ChecksumForTheme(current) == queryString)
                            return current;
                    }
                    // Missing theme? Or just a hacker
                    return Theme.Fancy;
                }
                else
                    return Theme.Fancy;
            }

            if (System.Configuration.ConfigurationSettings.AppSettings["ThemeToUse"] != null)
            {
                httpHostString = System.Configuration.ConfigurationSettings.AppSettings["ThemeToUse"];
            }


            httpHostString = httpHostString.ToLower();
			if (httpHostString != null)
			{
				if (httpHostString.StartsWith("omega"))
					return Theme.Omega;
				else if (httpHostString.StartsWith("mogo"))
					return Theme.Mogo;
				else if (httpHostString.StartsWith("hls"))
					return Theme.HLS;
				else if (httpHostString.StartsWith("findwhere"))
					return Theme.Fancy;
				else if (httpHostString.StartsWith("farsi"))
					return Theme.Farsi;
				else if (httpHostString.StartsWith("qinetiq"))
					return Theme.Qinetiq;
				else
					return Theme.Fancy;
			}
			else
				return Theme.Fancy;


		}


        public static string ChecksumForTheme(Theme currentTheme)
        {
            switch (currentTheme)
            {
                case Theme.Omega:
                    return "e1";
                case Theme.Mogo:
                    return "g6";
                case Theme.HLS:
                    return "r5";
                case Theme.Farsi:
                    return "x4";
                case Theme.Qinetiq:
                    return "c5";
                default:
                    return "";
            }
        }
	}

   
}
