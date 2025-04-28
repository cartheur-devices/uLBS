<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Livetrail.aspx.cs"
	Inherits="Set_Livetrail" Culture="auto" meta:resourcekey="PageResource1" UICulture="auto" %>

<%@ MasterType VirtualPath="~/MasterPage.master" %>
<%@ Register Src="~/UserControl/MembersMenuPanel.ascx" TagName="MembersPanel" TagPrefix="ucl" %>
<%@ Register Src="~/UserControl/OkCancel.ascx" TagName="OkCancel" TagPrefix="ucl" %>
<%@ Register Src="~/UserControl/ClientPopup.ascx" TagName="ClientPopup" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderHeader" runat="Server">
	<asp:Literal ID="litBeforeScriptMaps" runat="server"></asp:Literal>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="cph1" runat="Server">
    <link rel="SHORTCUT ICON" href="Misc/Graphics/findwhere_logo.ico" /
	<asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server">
		<Scripts>
			<asp:ScriptReference Path="~/App_Scripts/General.js" />
			<asp:ScriptReference Path="~/App_Scripts/DataProxy.js" />
		</Scripts>
		<Services>
			<asp:ServiceReference Path="~/Webservice/PlotService.asmx"></asp:ServiceReference>
		</Services>
	</asp:ScriptManagerProxy>
	<asp:Panel ID="setPanelMain" runat="server" Width="900px" CssClass="setpanelmain" BorderStyle="None">
		<div style="display: inline; padding: 5px 0 0 5px; float: left; width: 216px;">
			<asp:UpdatePanel ID="UpdatePanel1" runat="server">
				<ContentTemplate>
					<table>
						<tr>
							<td style="width: 200px;">
								<asp:Label ID="LiveTrail_Label_SelectDeviceName" runat="server" CssClass="labelitem" meta:resourcekey="LiveTrail_Label_SelectDeviceName" />
							</td>
						</tr>
						<tr>
							<td>
								<div class="memberpanel" style="height: 350px; width: 195px; overflow: auto; margin-bottom: 5px;">
									<ucl:MembersPanel ID="MembersPanel1" runat="server" />
								</div>
							</td>
						</tr>
						<tr>
							<td>
								<span style="margin-right: 10px;">
									<asp:Label ID="LiveTrail_Label_RefreshEvery" runat="server" CssClass="labelitem" meta:resourcekey="LiveTrail_Label_RefreshEvery" /></span>
								<asp:DropDownList ID="ddlRefreshInterval" runat="server" Width="100px" onchange="javascript:adjustInterval(this.options[this.selectedIndex].value);">
									<asp:ListItem Value="2" meta:resourcekey="LiveTrail_DDList_Seconds_2" />
									<asp:ListItem Value="5" meta:resourcekey="LiveTrail_DDList_Seconds_5" />
									<asp:ListItem Value="10" meta:resourcekey="LiveTrail_DDList_Seconds_10" />
									<asp:ListItem Value="30" meta:resourcekey="LiveTrail_DDList_Seconds_30" Selected="True" />
									<asp:ListItem Value="60" meta:resourcekey="LiveTrail_DDList_Minute_1" />
									<asp:ListItem Value="300" meta:resourcekey="LiveTrail_DDList_Minute_5" />
								</asp:DropDownList>
							</td>
						</tr>
						<tr>
							<td>
								<span style="margin-right: 25px;">
									<asp:Label ID="LiveTrail_Label_NextRefresh" runat="server" CssClass="labelitem" meta:resourcekey="LiveTrail_Label_NextRefreshResource1" /></span>
								<asp:Label ID="lblNextRefreshValue" runat="server" CssClass="labelitem" Text=". . ." />
							</td>
						</tr>
					</table>
				</ContentTemplate>
			</asp:UpdatePanel>
		</div>
		<div id="mappanel" class="mappanel" style="float: left; width: 674px; height: 418px; margin-top: 5px;">
			<div id="map" class="map" style="border: solid 1px #0066FF;">
			</div>
		</div>
		<div style="clear: both; display: inline; float: left; margin-top: 10px; width: 30%">
			<ucl:OkCancel ID="OkCancel_1" runat="server" meta:resourcekey="LiveTrail_Button_OkCancel" />
		</div>
		<div style="padding: 5px; display: inline; float: right; width: 65%">
			<asp:Label ID="LiveTrail_Label_Intro" runat="server" CssClass="labelremark" meta:resourcekey="LiveTrail_Label_Intro" />
		</div>
		<div style="clear: both; position: absolute; top: 130px; left: 750px;">
			<asp:Panel ID="pnlLoader" runat="server" CssClass="progressbar" HorizontalAlign="Center">
				<div id="Loader" style="visibility: hidden; display: inline;">
					<asp:Image ID="imgLoader" runat="server" SkinID="BarLoader" />
				</div>
			</asp:Panel>
		</div>
	</asp:Panel>
	<input id="cmdHelp3" type="button" runat="server" value="1" style="display: none;" />
	 <cc1:ModalPopupExtender ID="ModalPopupShowHelp" runat="server" PopupControlID="pnlShowHelp" CancelControlID="LiveTrail_Button_Pop_ShowHelp_Close"
                BehaviorID="ModalPopupShowHelpBehaviorID" PopupDragHandleControlID="dragShowHelp" DropShadow="True"
                TargetControlID="cmdHelp3" BackgroundCssClass="modalBackground" DynamicServicePath="" Enabled="True">
            </cc1:ModalPopupExtender>
            <div id="pnlShowHelp" style="height: 360px; width: 450px; background-color: #fbfefc; display: none;">
                <div id="dragShowHelp" class="masterwrappermenu">
                    <div class="masterwrappermenulabel">
                        <asp:Label ID="LiveTrail_Label_Pop_ShowHelp_Header" runat="server" meta:resourcekey="LiveTrail_Label_Pop_ShowHelp_Header" />
                    </div>
                    <div class="masterwrappermenuimages">
                        <asp:ImageButton ID="ImageButton8" SkinID="ImageButtonClose" runat="server" OnClientClick="HideHelp();return false;" />
                    </div>
                </div>
                <div style="margin: 5px; border: solid 1px blue; padding: 5px;">
                    <div id="pnlHelp" style="height: 260px; width: 100%;">
                        <div style="width: 95%;">
                            <asp:Label ID="lblHelp" runat="server" />
                        </div>
                    </div>
                    Version:<asp:Label ID="lblVersion" runat="server" />
                </div>
                <div style="margin: 6px 10px 6px 10px; float: right">
                    <asp:Button ID="LiveTrail_Button_Pop_ShowHelp_Close" runat="server" SkinID="LinkButtonCommon" meta:resourcekey="LiveTrail_Button_Pop_ShowHelp_Close" />
                </div>
            </div>
	
	<asp:Label ID="lblInfo" runat="server" CssClass="lblInfo" />
	<asp:HiddenField ID="hdnUserTimeoutInSeconds" runat="server" Value="99999" />
	<asp:HiddenField ID="hdnSecondsBeforeTimeout" runat="server" Value="99999" />
	<asp:HiddenField ID="hdnUserTimeoutMessage" runat="server" />
	<asp:HiddenField ID="hdnUserLoggedoutMessage" runat="server" />
	<asp:HiddenField ID="hdnDistanceUnit" runat="server" />
	<asp:Label ID="lblTimerTester" runat="server" BorderColor="#003300" BackColor="Red" ForeColor="White"
		Width="120px" />
	<cc1:AlwaysVisibleControlExtender ID="ace" runat="server" TargetControlID="lblTimerTester" VerticalSide="Bottom"
		VerticalOffset="10" HorizontalSide="Right" HorizontalOffset="30" Enabled="True" />

    <asp:UpdatePanel ID="UpdatePanelClientPoupup" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="False">
				    <ContentTemplate>
					    <input id="DummyBut2" type="button" runat="server" value="1" style="display: none;" />
					    <cc1:ModalPopupExtender ID="ModalPopupExtenderClientPopup" runat="server" PopupControlID="pnlClientPopup"
						    BehaviorID="ModalPopupClientPoupupBehaviorID" PopupDragHandleControlID="UpdatePanelClientPoupup"
						    DropShadow="True" TargetControlID="DummyBut2" BackgroundCssClass="modalBackground" DynamicServicePath=""
						    Enabled="True" />
					    <asp:Panel ID="pnlClientPopup" runat="server" Width="200px" BackColor="#FBFEFC" BorderStyle="None" CssClass="pnlOutside"
						    EnableViewState="False">
						    <uc1:ClientPopup ID="ClientPopup1" runat="server" EnableViewState="False" />
					    </asp:Panel>
				    </ContentTemplate>
			    </asp:UpdatePanel>
	    <script type="text/javascript">

		imgOutSetFolder = $get('<%= pnlLoader.ClientID %>');
		var LoaderID = $get('Loader');

		var intervalSecs = 30;
		function adjustInterval(seconds) {
			intervalSecs = seconds;
			if (intervalNo != null && intervalNo != -1)
				clearInterval(intervalNo);

			// ctl00_cph1_MembersPanel1_hdnSelected is temporary ID
			intervalNo = setInterval("GetLastKnownLocationForSelectedMembers('ctl00_cph1_MembersPanel1_hdnSelected');", intervalSecs * 1000);

			if (interval2No != null && interval2No != -1)
				clearInterval(interval2No);

			currCount = 0;
			interval2No = setInterval("refreshCount(" + intervalSecs + ")", 1000);
		}
		var UserTimeoutInSeconds = $get('<%= hdnUserTimeoutInSeconds.ClientID %>');
		var SecondsBeforeTimeout = $get('<%= hdnSecondsBeforeTimeout.ClientID %>').value;
		var UserTimeoutMessage = $get('<%= hdnUserTimeoutMessage.ClientID %>');
		var UserLoggedoutMessage = $get('<%= hdnUserLoggedoutMessage.ClientID %>');
		var ThemeName = '<%= Page.StyleSheetTheme %>';
		var lblTimerTester = $get('<%= lblTimerTester.ClientID %>');
		var hdnDistanceUnit = $get('<%= hdnDistanceUnit.ClientID %>');
		var lblNextRefreshValue = $get('<%= lblNextRefreshValue.ClientID %>');
		var NotificationLabel = $get('<%= ClientPopup1.GetLabelNotificationClientiD() %>');
		var lblHelp = $get('<%= lblHelp.ClientID %>');
		
	</script>

</asp:Content>
