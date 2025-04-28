using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using FindWhere.UserControls;

namespace FindWhere.UserControls
{
    public partial class UserControl_LbsExport : FindWhere.UserControls.UserControl_DefaultUserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            addScript();
            Export_Button_OkCancel.HideOkCloseButton();

            if (!this.alreadyLoaded)
            {
                lblInfo.Text = string.Empty;
                //lblLbsexportHeading.Visible = false;

                for (int i = 0; i < 25; i++)
                {
                    ddlFromTime.Items.Add(new ListItem(i.ToString() + ":00", i.ToString()));
                    ddlToTime.Items.Add(new ListItem(i.ToString() + ":00", i.ToString()));
                }
                int tijd = DateTime.Now.Hour;
                ddlFromTime.SelectedValue = tijd.ToString();
                ddlToTime.SelectedValue = (++tijd).ToString();
                                               
                Export_Text_DateFrom.Text = DateTime.Now.AddMonths(-1).ToShortDateString();
                Export_Text_DateTo.Text = DateTime.Now.ToShortDateString();              
                            
                char decimalPoint = '.';   // fol_properties.get1Property("label.lbsexport.decimalpnt.default" ;
                if (decimalPoint == '.')
                    RadioButtonList1.Items[0].Selected = true;
                else 
                    RadioButtonList1.Items[1].Selected = true;

                // NB! Verschil in basis 0 <> 1 in origineel tussen 2 volgende arrays
                int LabelLbsexportDatatypeNrOfItems = 3;  // fol_properties.get1Property("label.lbsexport.datatype.nrOfItems") Max 3

                if (ddlDatatype.Items.Count == 0)
                {
					ddlDatatype.Items.Add(new ListItem((String)GetLocalResourceObject("Export_DDList_DataType_RequestedLocations"), "LBS"));
                    if (LabelLbsexportDatatypeNrOfItems > 1)
						ddlDatatype.Items.Add(new ListItem((String)GetLocalResourceObject("Export_DDList_DataType_TextMessages"), "SMS"));
                    if (LabelLbsexportDatatypeNrOfItems > 2)
						ddlDatatype.Items.Add(new ListItem((String)GetLocalResourceObject("Export_DDList_DataType_AdditionalVariables"), "ADDVAR"));
                }
                int LabelLbsexportFormatNameNrOfItems = 2; // fol_properties.get1Property("label.lbsexport.format.name.nrOfItems")
                if (ddlSelectFormat.Items.Count == 0)
                {
					ddlSelectFormat.Items.Add(new ListItem((String)GetLocalResourceObject("Export_DDList_ExportFormatAutoRoute"), "0"));
                    if (LabelLbsexportFormatNameNrOfItems > 0)
						ddlSelectFormat.Items.Add(new ListItem((String)GetLocalResourceObject("Export_DDList_ExportFormatGoogle"), "1"));                   
                    ddlSelectFormat.SelectedIndex = 0;
                }

                MembersPanelExport.BuildMenu(true);
                
                System.Globalization.CultureInfo ci = new System.Globalization.CultureInfo(Session["UserCulture"].ToString());

                calFromDate.Format = ci.DateTimeFormat.ShortDatePattern;
                calToDate.Format = ci.DateTimeFormat.ShortDatePattern;
            }
        }

        private void addScript()
        {
            string script = @" 
                        function doExport() {
                               // Create an IFRAME.
                                  var iframe = document.createElement('iframe');
                             
                                  // Get the desired region from the dropdown.
                                  var members = $get('" + MembersPanelExport.GetHiddenFieldSelectedMembersClientID() + @"').value;

                                  if(members=='')
                                        {
                                        $get('" + lblInfo.ClientID + @"').text = 'First select a member';
                                        return;
                                        }

                                  var time1 = $get('" + ddlFromTime.ClientID + @"').value;
                                  var time2 = $get('" + ddlToTime.ClientID + @"').value;
                                  var date1 = $get('" + Export_Text_DateFrom.ClientID + @"').value;
                                  var date2 = $get('" + Export_Text_DateTo.ClientID + @"').value;
                                  
                                  var point = $get('" + RadioButtonList1.ClientID + @"').SelectedIndex;

                                  var format = 0;

                                    if ( $get('" + ddlSelectFormat.ClientID + @"') != null)
                                {
                                 format  = $get('" + ddlSelectFormat.ClientID + @"').value;
                                }
                                 
                                  var exportType  = $get('" + ddlDatatype.ClientID + @"').value;

                                  // Point the IFRAME to GenerateFile, with the
                                  //   desired region as a querystring argument.
                                  iframe.src = 'Set/GetExport.aspx?time1=' + time1 + '&time2=' + time2 + '&date1=' + escape(date1) + '&date2=' + escape(date2) + '&members=' + members + '&point=' + point + '&format=' + format + '&exportType=' + exportType ;
                            
                                  // This makes the IFRAME invisible to the user.
                                  iframe.style.display = 'none';
                             
                                  // Add the IFRAME to the page.  This will trigger
                                  //   a request to GenerateFile now.
                                  document.body.appendChild(iframe); 
                        }";
			
            ScriptManager.RegisterClientScriptBlock(Export_Button_OkCancel.CmdOk.LbtnSubmit, Export_Button_OkCancel.CmdOk.LbtnSubmit.GetType(), "ExportScript", script, true);
            Export_Button_OkCancel.CmdOk.LbtnSubmit.Attributes.Add("onclick", "doExport();");			
        }

        protected void ddlDatatypeSelectedIndexChanged(object sender, EventArgs e)
        {            
			lblLbsexportFormatHeading.Visible   = ddlDatatype.SelectedIndex == 0;
			ddlSelectFormat.Visible             = ddlDatatype.SelectedIndex == 0;
            string[] members = MembersPanelExport.GetSelectedMembers();
            MembersPanelExport.BuildMenu(true);
            MembersPanelExport.CheckMembers(members);
        }

        /// <summary>
        /// Save settings and close popup on success.
        /// </summary>
        protected void OkCloseClicked()
        {
            Boolean Succes = DoExport();

            // Only close popup on success
            if (Succes)
            {
                this.hideModalPopup();
            }
        }

        private bool DoExport()
        {
            Boolean Success = false;
			if (MembersPanelExport.GetSelectedMembers().Length == 0)
			{
				lblInfo.Text = (String)GetGlobalResourceObject("Taal", "Common_Device_SelectDevice");
			}
			else
			{
				Success = true;
				//lblInfo.Text = (String)GetLocalResourceObject("Export_Label_ExportSucces");
			}
            MembersPanelExport.BuildMenu(true);

            return Success;
        }

        protected void OkClicked()
        {
            DoExport();
        }
    }
}