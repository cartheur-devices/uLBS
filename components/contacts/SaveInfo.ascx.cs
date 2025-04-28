using System;

namespace FindWhere.UserControls
{
    public partial class UserControl_SaveInfo : FindWhere.UserControls.UserControl_DefaultUserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!alreadyLoaded)
            {
                lblConfigSaveInfo.Text = String.Concat(@"<ol style='margin-left:3em;'><li>Select your desired settings and click Save.</li><li>Your changes will be saved in our system.</li><li>View the status of your changes under the messages tab.</li></ol><ul style='margin-left:5em;'><li><img src='",
                    String.Concat("App_Themes/", Page.StyleSheetTheme, "/Images/icn_scheduled.gif"),
                    "' align='absbottom' border='0' width='12' height='12' /> Requested  means: new settings saved.</li><li><img src='",
                    String.Concat("App_Themes/", Page.StyleSheetTheme, "/Images/icn_sent.gif"),
                    "' align='absbottom' border='0' width='12' height='12' /> Sent means: settings sent to the device.</li><li><img src='",
                    String.Concat("App_Themes/", Page.StyleSheetTheme, "/Images/icn_sms.gif"),
                    "' align='absbottom' border='0' width='12' height='12' /> Incoming  means: settings accepted by the device.</li></ul>");
            }
        }
    }
}
