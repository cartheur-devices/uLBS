<%@ Control Language="C#" AutoEventWireup="true" CodeFile="MyAccount.ascx.cs" Inherits="FindWhere.UserControls.UserControl_MyAccount" %>
<%@ Register Src="~/UserControl/OkCancel.ascx" TagName="OkCancel" TagPrefix="ucl" %>
<%@ Register Src="Ok.ascx" TagName="Ok" TagPrefix="ucl" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/UserControl/MembersMenuPanel.ascx" TagName="MembersPanel" TagPrefix="uc2" %>
<%@ Register Src="~/UserControl/MembersPanel.ascx" TagName="MembersPanel" TagPrefix="ucl" %>

<asp:Panel ID="setPanelMain" runat="server" Width="100%">
    <div class="setContent">
        <cc1:TabContainer ID="TabContainer1" runat="server" Width="100%" Height="370px" OnActiveTabChanged="activeTabChanged">
            <cc1:TabPanel ID="MyAccountTab1" runat="server">
                <HeaderTemplate>
                    <asp:Label ID="MyAccount_Label_Header_Tab_Personal" runat="server" Width="100px" CssClass="center" meta:resourcekey="MyAccount_Label_Header_Tab_Personal" />
                </HeaderTemplate>
                <ContentTemplate>
                    <div class="boxedcontentmin">
                        <div style="border: solid 1px #A0C0CF; padding: 0 5px 0 5px;">
                            <table>
                                <tr>
                                    <td colspan="4">
                                        <asp:Label ID="MyAccount_TabPersonal_Label_Intro" runat="server" meta:resourcekey="MyAccount_TabPersonal_Label_Intro" />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 15%;">
                                        <asp:Label ID="MyAccount_TabPersonal_Label_EMail" runat="server" CssClass="labelitem" meta:resourcekey="MyAccount_TabPersonal_Label_EMail" />
                                    </td>
                                    <td style="width: 35%;">
                                        <asp:TextBox ID="MyAccount_TabPersonal_Text_EMail" runat="server" Width="200px" MaxLength="50" meta:resourcekey="MyAccount_TabPersonal_Text_EMail" />
                                        <asp:RegularExpressionValidator ID="MyAccount_TabPersonal_Regex_EMail" meta:resourcekey="MyAccount_TabPersonal_Regex_EMail"
                                            runat="server" ControlToValidate="MyAccount_TabPersonal_Text_EMail" ErrorMessage="Enter a valid email address"
                                            ValidationExpression="^(?:[a-zA-Z0-9_'^&amp;/+-])+(?:\.(?:[a-zA-Z0-9_'^&amp;/+-])+)*@(?:(?:\[?(?:(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?))\.){3}(?:(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\]?)|(?:[a-zA-Z0-9-]+\.)+(?:[a-zA-Z]){2,}\.?)$" />
                                    </td>
                                    <td style="width: 15%;">
                                        <asp:Label ID="MyAccount_TabPersonal_Label_Distance" runat="server" CssClass="labelitem" meta:resourcekey="MyAccount_TabPersonal_Label_Distance" />
                                    </td>
                                    <td style="width: 35%;">
                                        <asp:DropDownList ID="ddlDistanceInUnits" runat="server" Width="100px">
                                            <asp:ListItem Value="0" meta:resourcekey="MyAccount_DDList_Tab_Personal_Item1" />
                                            <asp:ListItem Value="1" meta:resourcekey="MyAccount_DDList_Tab_Personal_Item2" />
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="MyAccount_TabPersonal_Label_PostCode" runat="server" CssClass="labelitem" meta:resourcekey="MyAccount_TabPersonal_Label_PostCode" />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="MyAccount_Text_Tab_Personal_PostCode" runat="server" Width="100px" MaxLength="8" meta:resourcekey="MyAccount_Text_Tab_Personal_PostCode" />
                                    </td>
                                    <td>
                                        <asp:Label ID="lblMyAccountCompanyName" runat="server" CssClass="labelitem" meta:resourcekey="MyAccount_TabPersonal_Label_CompanyName" />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="MyAccount_Text_Tab_Personal_CompanyName" runat="server" Width="200px" MaxLength="40"
                                            meta:resourcekey="MyAccount_Text_Tab_Personal_CompanyName" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div style="width: 48%; float: left; border: solid 1px #A0C0CF; margin-top: 10px; padding: 0.5em;">
                            <table style="width: 100%;">
                                <tr class="tablerowheader">
                                    <td colspan="2">
                                        <asp:Label ID="MyAccount_TabPersonal_Label_PassWordIntro" runat="server" ForeColor="White" meta:resourcekey="MyAccount_TabPersonal_Label_PassWordIntro" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="MyAccount_TabPersonal_Label_PassWordOld" runat="server" CssClass="labelitem" meta:resourcekey="MyAccount_TabPersonal_Label_PassWordOld" />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="MyAccount_Text_Tab_Personal_PassWordOld" runat="server" Width="80px" TextMode="Password" meta:resourcekey="MyAccount_Text_Tab_Personal_PassWordOld" />
                                        <br />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="MyAccount_TabPersonal_Label_PassWordNew" runat="server" CssClass="labelitem" meta:resourcekey="MyAccount_TabPersonal_Label_PassWordNew" />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="MyAccount_Text_Tab_Personal_PassWordNew" runat="server" Width="80px" TextMode="Password" meta:resourcekey="MyAccount_Text_Tab_Personal_PassWordNew" />
                                        <br />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="MyAccount_TabPersonal_Label_PassWordConfirm" runat="server" CssClass="labelitem" meta:resourcekey="MyAccount_TabPersonal_Label_PassWordConfirm" />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="MyAccount_Text_Tab_Personal_PassWordConfirm" runat="server" Width="80px" TextMode="Password" meta:resourcekey="MyAccount_Text_Tab_Personal_PassWordConfirm" />
                                        <br />
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div style="width: 48%; float: right; border: solid 1px #A0C0CF; margin-top: 10px; padding: 0.5em;">
                            <table style="width: 100%;">
                                <tr class="tablerowheader">
                                    <td colspan="2">
                                        <asp:Label ID="MyAccount_TabPersonal_Label_LogOutTimeZone" runat="server" ForeColor="White" meta:resourcekey="MyAccount_TabPersonal_Label_LogOutTimeZone" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="MyAccount_TabPersonal_Label_UserTimeOut" runat="server" CssClass="labelitem" meta:resourcekey="MyAccount_TabPersonal_Label_UserTimeOut" />
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlUserTimerSetting" runat="server" Width="160px">
                                            <asp:ListItem Value="20" meta:resourcekey="MyAccount_TabPersonal_DDList_TimeOut_Basic" />
                                            <asp:ListItem Value="40" meta:resourcekey="MyAccount_TabPersonal_DDList_TimeOut_Regular" />
                                            <asp:ListItem Value="90" meta:resourcekey="MyAccount_TabPersonal_DDList_TimeOut_Extended" />
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="MyAccount_TabPersonal_Label_TimeZone" runat="server" CssClass="labelitem" meta:resourcekey="MyAccount_TabPersonal_Label_TimeZone" />
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlSelectTimeZone" runat="server" Width="260px" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div style="clear: both; width: 48%; float: left; display: inline; border: solid 1px #A0C0CF; margin-top: 10px;
                            padding: 0.5em;">
                            <table style="width: 100%;">
                                <tr class="tablerowheader">
                                    <td colspan="2">
                                        <asp:Label ID="MyAccount_TabPersonal_Label_MapOptions" runat="server" CssClass="labelitem" ForeColor="White"
                                            meta:resourcekey="MyAccount_TabPersonal_Label_MapOptions" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="MyAccount_TabPersonal_Label_DisplayLocationLabels" runat="server" CssClass="labelitem"
                                            meta:resourcekey="MyAccount_TabPersonal_Label_DisplayLocationLabels" />
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlDisplayMapLabels" runat="server" Width="100px">
                                            <asp:ListItem Value="1" meta:resourcekey="MyAccount_TabPersonal_DDListLocationLabels_Show" />
                                            <asp:ListItem Value="0" meta:resourcekey="MyAccount_TabPersonal_DDListLocationLabels_Hide" />
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                    <div style="float: right; height: 26px; margin-top: 10px;">
                        <ucl:OkCancel ID="OkCancel3" runat="server" OnClickEvent="OkClicked" OnCancelEvent="CloseClicked" OnOKCloseClickEvent="OkCloseClicked" />
                    </div>
                </ContentTemplate>
            </cc1:TabPanel>
            <cc1:TabPanel ID="MyAccountTab2" runat="server">
                <HeaderTemplate>
                    <asp:Label ID="MyAccount_Label_Header_Tab_Contacts" runat="server" Width="90px" CssClass="center" meta:resourcekey="MyAccount_Label_Header_Tab_Contacts" />
                </HeaderTemplate>
                <ContentTemplate>
                    <div class="boxedcontentmin" style="margin-bottom: 8px;">
                        <asp:Label ID="MyAccount_TabContacts_Label_Intro" runat="server" meta:resourcekey="MyAccount_TabContacts_Label_Intro" />
                    </div>
                    <div class="boxedcontenttab" style="margin-bottom: 5px; overflow: auto;">
                        <div style="height: 300px; overflow-y: scroll;">
                            <asp:GridView ID="GridContacts" runat="server" AutoGenerateColumns="False" Width="97%" OnRowDeleting="GridContacts_RowDeleting"
                                OnRowDataBound="GridContacts_RowDataBound" OnInserting="GridContacts_Inserting" ShowFooter="True"
                                OnRowCommand="GridContacts_RowCommand" AutoEventWireup="true">
                                <Columns>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:Label ID="lblIndex" runat="server" Visible="False" Text='<%# Bind("IndexNr") %>' />
                                            <asp:TextBox ID="txtName" runat="server" Text='<%# Bind("Name") %>' />
                                            <asp:RequiredFieldValidator ID="reqName" runat="server" ControlToValidate="txtName" SetFocusOnError="True"
                                                meta:resourcekey="MyAccount_TabContacts_Grid_Validator_Message" />
                                        </ItemTemplate>
                                        <HeaderStyle CssClass="gridfieldleft" />
                                    </asp:TemplateField>
                                    <asp:TemplateField meta:resourcekey="MyAccount_TabContacts_Grid_Header_Phone">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtPhone" runat="server" Text='<%# Bind("Phone") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField meta:resourcekey="MyAccount_TabContacts_Grid_Header_Provider">
                                        <ItemTemplate>
                                            <asp:DropDownList ID="ddlProviders" DataSource='<%# GetProviders() %>' DataTextField="value" DataValueField="key"
                                                runat="server">
                                            </asp:DropDownList>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lbTest_1" runat="server" CommandName="Test_1" CommandArgument="Test_1" meta:resourcekey="MyAccount_TabContacts_Grid_LinkField_Test" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField meta:resourcekey="MyAccount_TabContacts_Grid_Header_Email">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtEmail" runat="server" Text='<%# Bind("Email") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lbTest_2" runat="server" CommandName="Test_2" CommandArgument="Test_2" meta:resourcekey="MyAccount_TabContacts_Grid_LinkField_Test" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:ImageButton runat="server" CommandName="Delete" SkinID="ImageMyAccountDelete" ID="cmdDelete" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                    <div style="float: right; height: 26px;">
                        <ucl:OkCancel ID="SubmitCancel1" runat="server" OnClickEvent="SaveContactsClicked" OnOKCloseClickEvent="SaveCloseContactsClicked"
                            OnCancelEvent="CloseClicked" />
                        <div style="float: right; display: inline;">
                            <ucl:Ok ID="OkAddAcontact" runat="server" OnClickEvent="AddContact" meta:resourcekey="MyAccount_TabContacts_Button_AddContact" />
                        </div>
                    </div>
                </ContentTemplate>
            </cc1:TabPanel>
            <cc1:TabPanel ID="MyAccountTab3" runat="server">
                <HeaderTemplate>
                    <asp:Label ID="MyAccount_TabBalance_Header" runat="server" CssClass="center" Width="100px" meta:resourcekey="MyAccount_TabBalance_Header" />
                </HeaderTemplate>
                <ContentTemplate>
                    <div class="boxedcontenttab" style="border-style: none">
                        <asp:Label ID="lblMyAccountIntroMyBalance" runat="server" />
                        <br />
                        <div style="margin: 5px;">
                            <asp:LinkButton ID="cmdTopUp" runat="server" ForeColor="Blue" />
                        </div>
                        <div>
                            <asp:Label ID="lblCurrentBalance" runat="server" Visible = "false" meta:resourcekey="MyAccount_AccountBalance" Font-Size="Small" Font-Bold="true" />
                            <asp:Label ID="showMember" runat="server" Visible = "false" Font-Size="Small" Font-Bold="true" />
                            <asp:Label ID="lblIs" runat="server" Visible = "false" meta:resourcekey="MyAccount_Is" Font-Size="Small" Font-Bold="true" />
                            <asp:Label ID="showAccountBalance" runat="server" Visible = "false" Font-Size="Small" Font-Bold="true" />
                            <asp:Label ID="lblBalanceTrailingTest" runat="server" Visible="false" meta:resourcekey="MyAccount_TrailingText" Font-Bold="true" Font-Size="Small" />
                            <!--<asp:LinkButton ID="cmdCancelSubscription" runat="server" ForeColor="Blue" />-->
                        </div>
                        <div style="margin: 5px;">
                            
                            <!--<asp:LinkButton ID="cmdChangeSubscription" runat="server" ForeColor="Blue" />-->
                            
                        </div>
                    </div>
                    <table class="boxedcontenttab" style="margin: 5px 0 10px 0; width: 757px;">
                        <tr>
                            <td style="width: 20%">
                                <asp:Label ID="MyAccount_TabBalance_Label_DeviceName" runat="server" CssClass="labelitem" meta:resourcekey="MyAccount_TabBalance_Label_DeviceName" />
                            </td>
                            <td style="width: 20%;">
                                <asp:DropDownList ID="ddlSelectMember" runat="server" Width="120px" />
                            </td>
                            <td style="width: 10%; text-align: right;">
                                <asp:Label ID="MyAccount_TabBalance_Label_From" runat="server" CssClass="labelitem" meta:resourcekey="MyAccount_TabBalance_Label_From" />
                            </td>
                            <td style="width: 18%;">
                                <asp:TextBox ID="MyAccount_TabBalance_Text_FromDate" runat="server" Width="74px" MaxLength="10" Enabled="False"
                                    CssClass="center" meta:resourcekey="MyAccount_TabBalance_Text_FromDate" />
                                <asp:ImageButton ID="imgFromDate" runat="server" SkinID="ImageMyAccountCalendar" OnClientClick="return;" />
                                &nbsp;&nbsp;
                                <cc1:CalendarExtender ID="calFromDate" runat="server" TargetControlID="MyAccount_TabBalance_Text_FromDate"
                                    PopupButtonID="imgFromDate" FirstDayOfWeek="Monday" Animated="False" CssClass="ajax__calendar ajaxcalendar"
                                    Enabled="True" EnableViewState="true" >
                                </cc1:CalendarExtender>
                                <span style="width: 50px; text-align: right;">
                            </td>
                            <td style="width: 10%; text-align: right;">
                                <asp:Label ID="MyAccount_TabBalance_Label_To" runat="server" CssClass="labelitem" meta:resourcekey="MyAccount_TabBalance_Label_To" />
                            </td>
                            <td style="width: 18%;">
                                <asp:TextBox ID="MyAccount_TabBalance_Text_ToDate" runat="server" Width="74px" MaxLength="10" Enabled="False"
                                    CssClass="center" meta:resourcekey="MyAccount_TabBalance_Text_ToDate" />
                                <asp:ImageButton ID="imgToDate" runat="server" OnClientClick="return;" SkinID="ImageMyAccountCalendar" />
                                &nbsp;&nbsp;
                                <cc1:CalendarExtender ID="calToDate" runat="server" TargetControlID="MyAccount_TabBalance_Text_ToDate"
                                    PopupButtonID="imgToDate" FirstDayOfWeek="Monday" Animated="False" CssClass="ajax__calendar ajaxcalendar"
                                    Enabled="True" >
                                </cc1:CalendarExtender>
                            </td>
                        </tr>
                    </table>
                    <div id="BalanceDiv">
                        <asp:Panel ID="PanelBalance" runat="server" CssClass="boxedcontent" ScrollBars="Vertical" Height="240px"
                            Width="757px" Visible="False">
                            <asp:GridView ID="GridViewBalance" runat="server" Width="737px"  OnRowDataBound="GridViewBalance_RowDataBound" show="true" ShowFooter="True">
                                <Columns>
                                <asp:TemplateField>
                                        <HeaderTemplate>
                                            <asp:Label ID="lblHeaderDateTime" runat="server" meta:resourcekey="MyAccount_GridBalance_DateTime" />
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblDateTime" runat="server" Text='<%# Eval("DateTime") %>' />
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="FooterTotalDateTime" runat="server" meta:resourcekey ="MyAccount_GridBalance_Note" Visible="false" />
                                        </FooterTemplate>
                                </asp:TemplateField>
                                 <asp:TemplateField>
                                        <HeaderTemplate>
                                            <asp:Label ID="lblHeaderQuantity" runat="server" meta:resourcekey="MyAccount_GridBalance_Quantity" />
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblQuantity" runat="server" Text='<%# Eval("Quantity") %>' />
                                        </ItemTemplate>
                                        <FooterTemplate>
                                        </FooterTemplate>
                                </asp:TemplateField>
                                 <asp:TemplateField>
                                        <HeaderTemplate>
                                            <asp:Label ID="lblHeaderDescription" runat="server" meta:resourcekey="MyAccount_GridBalance_Description" />
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblDescription" runat="server" Text='<%# Eval("Charge") %>' />
                                        </ItemTemplate>
                                        <FooterTemplate>
                                        <asp:Label ID="FooterTotalDescription" runat="server" meta:resourcekey ="MyAccount_GridBalance_DescriptionFooter" />
                                          </FooterTemplate>
                                </asp:TemplateField>
                                 <asp:TemplateField>
                                        <HeaderTemplate>
                                            <asp:Label ID="lblHeaderCharge" runat="server" meta:resourcekey="MyAccount_GridBalance_Charge" />
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblCharge" runat="server" Text='<%# Eval("Cost") %>' />
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lblChargeTotal" runat="server" Text='' />
                                         </FooterTemplate>
                                </asp:TemplateField>
                                   <asp:TemplateField>
                                        <HeaderTemplate>
                                            <asp:Label ID="lblHeaderCredit" runat="server" meta:resourcekey="MyAccount_GridBalance_Total" />
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblCredit" runat="server" Text='<%# Eval("Total") %>' />
                                        </ItemTemplate>
                                        <FooterTemplate> 
                                            <asp:Label ID="lblFooterTotalCredit" runat="server" Text='' />
                                        </FooterTemplate>
                                </asp:TemplateField>  
								</Columns>
								</asp:GridView>
                        </asp:Panel>
                    </div>
                    <div style="float: right; height: 26px; margin-top: 5px;">
                        <ucl:OkCancel ID="OkCancel2" runat="server" OnClickEvent="ShowBalanceClicked" OnCancelEvent="CloseClicked"
                            Text_OkClose="Print" meta:resourcekey="MyAccount_TabBalance_OkCancel" />
                    </div>
                </ContentTemplate>
            </cc1:TabPanel>
            <cc1:TabPanel ID="MyAccountTab4" runat="server">
                <HeaderTemplate>
                    <asp:Label ID="MyAccount_TabMobTrack_Header" runat="server" Width="100px" CssClass="center" meta:resourcekey="MyAccount_TabMobTrack_Header" />
                </HeaderTemplate>
                <ContentTemplate>
                    <div class="boxedcontenttab" style="padding: 10px; width: 737px;">
                        <table>
                            <tr>
                                <td colspan="2">
                                    <asp:Label ID="lblMyAccountIntroSMSAction" runat="server" meta:resourcekey="MyAccount_lblMyAccountIntroSMSAction" />
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 20%">
                                    <asp:Label ID="MyAccount_TabMobTrack_Label_Locate" runat="server" CssClass="labelitem" meta:resourcekey="MyAccount_TabMobTrack_Label_Locate" />
                                </td>
                                <td style="width: 80%" align="left">
                                    <asp:DropDownList ID="ddlSelectAction" runat="server" Width="140px" OnSelectedIndexChanged="ddlSelectAction_SelectedIndexChanged"
                                        AutoPostBack="True" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="MyAccount_TabMobTrack_Label_Keyword" runat="server" CssClass="labelitem" meta:resourcekey="MyAccount_TabMobTrack_Label_Keyword" />
                                </td>
                                <td>
                                    <asp:TextBox ID="txtSmsActionKeywordId" runat="server" Width="130px" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="MyAccount_TabMobTrack_Label_Use" runat="server" CssClass="labelitem" meta:resourcekey="MyAccount_TabMobTrack_Label_Use" />
                                </td>
                                <td>
                                    <asp:Label ID="lblSmsactionUseID" runat="server" meta:resourcekey="MyAccount_TabMobTrack_Label_SmsActionUseId" />
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div style="float: right; height: 26px; padding: 10px 0 4px 0">
                        <ucl:OkCancel ID="OkCancel_tab4" runat="server" OnClickEvent="SaveSmsActionClicked" />
                    </div>
                </ContentTemplate>
            </cc1:TabPanel>
            <cc1:TabPanel ID="MyAccountTab5" runat="server" Visible="false">
                <HeaderTemplate>
                    <asp:Label ID="MyAccount_TabDeleteHistoryHeader" runat="server" Width="100px" CssClass="center" meta:resourcekey="MyAccount_TabDeleteHistoryHeader" />
                </HeaderTemplate>
                <ContentTemplate>
                    <div class="boxedcontenttab" style="padding: 10px; width: 737px;">
                      <asp:Label ID="MyAccount_TabDeleteHistoryInfo" runat="server" meta:resourcekey="MyAccount_TabDeleteHistoryInfoMessage" />
                        <br />
                        <table>
                            <tr>
                                <td colspan="2">
                                    <asp:Label ID="Label2" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 20%">
                                    <!--<asp:Label ID="Label3" runat="server" CssClass="labelitem" meta:resourcekey="MyAccount_TabMobTrack_Label_Locate" />-->
                                </td>
                                <td style="width: 80%" align="left">
                                    
                                        <div style="width: 180px; height: 150px; overflow: auto; border: solid 1px #A0C0CF;">
								<uc2:MembersPanel ID="MembersPanelHistory" runat="server" />
							</div>
                                </td>
                            </tr>
                           
                          
                        </table>
                    </div>
                    <div style="float: right; height: 26px; padding: 10px 0 4px 0">
                        <ucl:OkCancel ID="OkCancel1" runat="server" OnClickEvent="DeleteHistoryClicked" Text_Ok="Delete" Text_OkClose="Delete and close" meta:resourcekey="MyAccount_TabDeleteHistory_OkCancel"  />
                    </div>
                </ContentTemplate>
            </cc1:TabPanel>
            <cc1:TabPanel ID="MyAccountTab6" runat="server">
                <HeaderTemplate>
                    <asp:Label ID="MyAccount_TabTopUpHeader" runat="server" Width="90px" CssClass="center" meta:resourcekey="MyAccount_TabTopUpHeader" />
                </HeaderTemplate>
                <ContentTemplate>
                    <div class="boxedcontenttab" style="padding: 10px; width: 737px;">
                      <asp:Label ID="Label4" runat="server" meta:resourcekey="MyAccount_TabTopUpSelectDeviceTopUp" />
                        <br />
                        <table width="100%">

                            <tr>
                                <td>
                                   <div class="memberpanel" style="width: 100px; height: 280px;">
							            <asp:Repeater ID="repTopUPMembersList" runat="server" OnItemDataBound="repTopupMembersList_ItemDataBound"  >
							                 <HeaderTemplate>
								            </HeaderTemplate>
								            <ItemTemplate>
									            <div style="margin-bottom: 10px; padding-left: 5px;">
										            <asp:Label ID="lblMSISDN" runat="server" Text='<%# Eval("MSISDN") %>' Visible="False" />
										            <asp:HyperLink ID="lbAlias"  runat="server" Text='<%# Eval("Alias") %>'  />
									            </div>
								            </ItemTemplate>
							            </asp:Repeater>
					                </div>
                                </td>
                                <td style="width: 98%;" align="left" >        
								    <iframe id='iframeTopup' width="100%" height="280px"></iframe>
                                </td>
                            </tr>                  
                        </table>
                    </div>
                    <div style="float: right; height: 26px; padding: 10px 0 4px 0">
                        <ucl:Ok ID="OkTopUp" runat="server" OnClickEvent="CloseClicked" meta:resourcekey="MyAccount_TabTopUp_Ok"  />
                    </div>
                </ContentTemplate>
            </cc1:TabPanel>
            <cc1:TabPanel ID="MyAccountTab7" runat="server" >
                <HeaderTemplate>
                    <asp:Label ID="MyAccount_TabChangePlanHeader" runat="server" Width="90px" CssClass="center" meta:resourcekey="MyAccount_TabChangePlanHeader" />
                </HeaderTemplate>
                <ContentTemplate>
                    <div class="boxedcontenttab" style="padding: 10px; width: 737px;">
                        <asp:Label ID="MyAccount_TabChangePlan" runat="server" meta:resourcekey="MyAccount_TabChangePlanSelectDeviceChangePlan" />
                        <br />
                        <table width="100%">
                            <tr>
                                <td>
                                   <div class="memberpanel" style="width: 100px; height: 280px;">
							            <asp:Repeater ID="repChangePlanMembersList" runat="server" OnItemDataBound="repChangePlanMembersList_ItemDataBound"  >
							                 <HeaderTemplate>
								            </HeaderTemplate>
								            <ItemTemplate>
									            <div style="margin-bottom: 10px; padding-left: 5px;">
										            <asp:Label ID="lblMSISDN" runat="server" Text='<%# Eval("MSISDN") %>' Visible="False" />
										            <asp:HyperLink ID="lbAlias"  runat="server" Text='<%# Eval("Alias") %>'  />
									            </div>
								            </ItemTemplate>
							            </asp:Repeater>
					                </div>
                                </td>
                                <td style="width: 98%;" align="left" >        
								<iframe id='iframeChangePlan' width="100%" height="280px"></iframe>
                                </td>
                            </tr>                          
                        </table>
                    </div>
                    <div style="float: right; height: 26px; padding: 10px 0 4px 0">
                        <ucl:Ok ID="OkChangePlan" runat="server" OnClickEvent="CloseClicked" meta:resourcekey="MyAccount_TabChangePlan_Ok"  />
                    </div>
                </ContentTemplate>
            </cc1:TabPanel>
        </cc1:TabContainer>
        <div style="clear: both; margin-top: 5px;">
            <asp:Label ID="lblInfo" runat="server" CssClass="lblInfo" />
        </div>
    </div>

    <script type="text/javascript">
        var pnlBalance = $get('<%= PanelBalance.ClientID %>');
    </script>

</asp:Panel>
