using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Reflection;
using System.Collections;
using System.Resources;
using System.Text;

public partial class Set_Livetrail : System.Web.UI.Page
{
	public override string StyleSheetTheme
	{
		get
		{
			if (Session["ThemeToUse"] != null)
				return Session["ThemeToUse"].ToString();
			else
				return string.Empty;
		}
	}

	protected void Page_Load(object sender, EventArgs e)
	{

        //lblTimerTester.Visible = true;
		lblTimerTester.Visible = false;

        FindWhere.Utils.Utils.SetSessionLanguage(Session, Request.QueryString["language"]);

		FindWhere.Utils.Utils.ValidateTimeoutFromWebpage(Session, Page, true);

		lblInfo.Text = string.Empty;
		Master.Cust_Width = Convert.ToInt32(setPanelMain.Width.Value);
		Master.Cust_HeaderHeight = Convert.ToInt32(this.GetGlobalResourceObject(Page.StyleSheetTheme, "LiveTrailHeaderHeight"));
		Master.Cust_ImageLeftUrl = Convert.ToString(this.GetGlobalResourceObject(Page.StyleSheetTheme, "LiveTrailImageLeftUrl"));
		Master.Cust_ImageRightUrl = Convert.ToString(this.GetGlobalResourceObject(Page.StyleSheetTheme, "LiveTrailImageRightUrl"));

		if (!IsPostBack)
		{
			OkCancel_1.HideOkCloseButton();

			hdnUserTimeoutMessage.Value = (String)GetGlobalResourceObject("Taal", "Common_Message_LoggedOffInMinutes");
			hdnUserLoggedoutMessage.Value = (String)GetGlobalResourceObject("Taal", "Common_Message_HaveBeenLoggedOff");

			Page.Title = (String)this.GetGlobalResourceObject(Page.StyleSheetTheme, "brandName") + " Live Trail";
            ((ImageButton)Master.FindControl("ImageButton3")).OnClientClick = "javascript:GetHelp('Help_Content_LiveTrail');return false;";
			Master.Cust_HeaderText = "Live Trail";
            lblVersion.Text = System.Configuration.ConfigurationSettings.AppSettings["ApplicationVersion"];

			string usrMap = Session["UsrMap"].ToString();

			FindWhere.Panel.Maps.SetMapScript(litBeforeScriptMaps, usrMap, Request.Url.ToString());  // Fill Literal in Header with Key.
			if (usrMap == "googlemaps")
			{
				ScriptReference scriptRefG = new ScriptReference("~/App_Scripts/Googlemaps.js");
				ScriptManagerProxy1.Scripts.Add(scriptRefG);
				string strScript = "<script type='text/javascript'>loadGoogleMaps();</script>";
				ClientScript.RegisterStartupScript(typeof(Page), "googleStart", strScript);
			}
			else if (usrMap == "virtualearth")
			{
				ScriptReference scriptRefV = new ScriptReference("~/App_Scripts/Virtualearth.js");
				ScriptManagerProxy1.Scripts.Add(scriptRefV);
				string strScript = "<script type='text/javascript'>function LoadVE(){loadVirtualEarth();};if (window.attachEvent){window.attachEvent('onload', LoadVE)}else{window.addEventListener('load', LoadVE, false);}</script>";
				ClientScript.RegisterStartupScript(typeof(Page), "virtualStart", strScript);

				string strScriptVars = @"var mapLabels = " + Session["MapLabels"].ToString().ToLower() + ";";
				ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "PageScript", strScriptVars, true);
			}

			MembersPanel1.BuildMenu(true);
			OkCancel_1.OkClientScript = "javascript:GetLiveTrail('" + MembersPanel1.GetHiddenFieldSelectedMembersClientID() + "');return false;";
			OkCancel_1.CancelClientScript = "javascript:self.close(); return false;";

			AddJavascriptResources();
		}
		FindWhere.Utils.Utils.SetLastUserActionTime(Session, Page);
	}

	private void GetLastPositions()
	{
		//TODO replace -> webservice @ Client?
		string[] members = MembersPanel1.GetSelectedMembers();
		JNetBridge.Classes.JnetBridgeLoginUnit javaID = (JNetBridge.Classes.JnetBridgeLoginUnit)Session["netCookieJavaSessionID"];

		JNetBridge.ReplyClasses.LastPositionJavaCallReply PositionsJavaCallReply = JNetBridge.LastPositionJavaCall.GetLastKnownPositions(javaID, JNetBridge.LastPositionJavaCall.postionType.historical, members,false);

		// posities in:  PositionsJavaCallReply.MapDisplayHelper.AllPos 
	}
	/// <summary>
	/// Used for translation of javascript messages
	/// </summary>
	private void AddJavascriptResources()
	{
		string scripttxt;
		if (Cache["javascript_vars"] != null)
		{
			scripttxt = (string)Cache["javascript_vars"];
		}
		else
		{

			Type type = typeof(System.Web.Compilation.BuildManager);

			PropertyInfo propertyInfo = type.GetProperty("AppResourcesAssembly",
			BindingFlags.Static |
			BindingFlags.GetField |
			BindingFlags.NonPublic);

			Assembly assembly = (Assembly)propertyInfo.GetValue(null, null);

			ResourceManager manager = new ResourceManager("resources.taal", assembly);

			ResourceSet resources = manager.GetResourceSet(
			System.Globalization.CultureInfo.CurrentCulture, true, true);

			IDictionaryEnumerator enumerator = resources.GetEnumerator();

			StringBuilder scriptText = new StringBuilder();
			scriptText.Append(Environment.NewLine);
			while (enumerator.MoveNext())
			{
				if (((string)enumerator.Key).StartsWith("Javascript_"))
				{
					scriptText.Append(string.Concat("var ", (string)enumerator.Key)); ;
					scriptText.Append(string.Concat("= '", (string)enumerator.Value, "'; ", Environment.NewLine));
				}
			}

			scripttxt = scriptText.ToString();
			Cache["javascript_vars"] = scripttxt;
		}

		string scriptKey = "JavascriptResources";
		ScriptManager.RegisterClientScriptBlock(this, typeof(Page), scriptKey, scripttxt.ToString(), true);
	}

    protected override void InitializeCulture()
    {
        UICulture = Session["UserCulture"].ToString();
        Culture = Session["UserCulture"].ToString();
        base.InitializeCulture();
    }
}