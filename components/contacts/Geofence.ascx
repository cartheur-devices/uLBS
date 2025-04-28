<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Geofence.ascx.cs" Inherits="FindWhere.UserControls.UserControl_Geofence" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/UserControl/OkCancel.ascx" TagName="OkCancel" TagPrefix="ucl" %>
<%@ Register Src="~/UserControl/MembersMenuPanel.ascx" TagName="MembersPanel" TagPrefix="ucl" %>
<asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server">
	<Scripts>
		<asp:ScriptReference Path="~/App_Scripts/Geofence.js" />
	</Scripts>
</asp:ScriptManagerProxy>
<asp:Panel ID="Panel1" runat="server" Width="100%" Height="560px">
	<div class="setContent" runat="server">
		<asp:HiddenField ID="hdnActiveTab" Value="0" runat="server" />
		<cc1:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0" AutoPostBack="True" OnActiveTabChanged="TabContainer1_ActiveTabChanged"
			Height="440px">
			<cc1:TabPanel ID="GeoFenceTab1" runat="server" EnableViewState="False">
				<HeaderTemplate>
					<asp:Label ID="Geofence_TabArea_Header" runat="server" Width="100px" CssClass="center" meta:resourcekey="Geofence_TabArea_Header" />
				</HeaderTemplate>
				<ContentTemplate>
					<div id="selectDiv" class="boxedcontent" style="width: 98%;">
						<table>
							<tr>
								<td colspan="2">
									<asp:Label ID="Geofence_TabArea_Label_SelectAreaNameText" runat="server" meta:resourcekey="Geofence_TabArea_Label_SelectAreaNameText" />
								</td>
							</tr>
							<tr>
								<td>
									<asp:Label ID="Geofence_TabArea_Label_SelectGefenceName" runat="server" CssClass="labelitem" meta:resourcekey="Geofence_TabArea_Label_SelectGefenceName" />
								</td>
								<td>
									<select id="ddlGeofences" runat="server" style="width: 180px;" />
								&nbsp;&nbsp;</td>
							</tr>
						</table>
						<div id="divGeoFenceHide_1" style="display: none; padding: 10px; clear: both;">
							<div>
								<asp:Label ID="Geofence_TabArea_Label_MoveChangeGeofence" runat="server" CssClass="labelitem" meta:resourcekey="Geofence_TabArea_Label_MoveChangeGeofence" />
							</div>
							<div>
								<asp:Label ID="Geofence_TabArea_Label_ToMoveText" runat="server" meta:resourcekey="Geofence_TabArea_Label_ToMoveText" />
							</div>
						</div>
					</div>
					<div id="insertDiv" class="boxedcontent" style="display: none; width: 98%">
						<table class="boxedcontent" style="height: 150px;">
							<tr>
								<td colspan="2">
									<asp:Label ID="Geofence_TabArea_Label_CreateFence" runat="server" meta:resourcekey="Geofence_TabArea_Label_CreateFence" />
								</td>
							</tr>
							<tr>
								<td style="width: 60%;">
									<asp:Label ID="Geofence_TabArea_Label_EnterNewAreaName" runat="server" CssClass="labelitem" meta:resourcekey="Geofence_TabArea_Label_EnterNewAreaName" />:
								</td>
								<td>
									<asp:TextBox ID="txtGeoFenceName" runat="server" Width="200px" />
								</td>
							</tr>
						</table>
					</div>
					<div id="divGeoFenceHide_2" style="display: none; float: right; margin: 0 20px 10px 0; width: 98%">
						<div style="float: right;">
							<table style="width: 20%">
								<tr>
									<td>
									</td>
									<td style="margin: 0px; padding: 0px; vertical-align: bottom;">
										<asp:ImageButton ID="imgMoveGeoFenceN" SkinID="imgMoveGeoFenceN" runat="server" OnClientClick="MoveGeoFence('N');return false;" />
										<td colspan="4">
										</td>
								</tr>
								<tr>
									<td>
										<asp:ImageButton ID="imgMoveGeoFenceW" SkinID="imgMoveGeoFenceW" runat="server" OnClientClick="MoveGeoFence('W');return false;" />
									</td>
									<td>
									</td>
									<td>
										<asp:ImageButton ID="imgMoveGeoFenceE" SkinID="imgMoveGeoFenceE" runat="server" OnClientClick="MoveGeoFence('E');return false;" />
									</td>
									<td>
									</td>
									<td>
										<asp:ImageButton ID="imgChangeRadiusMin" SkinID="imgChangeRadiusMin" runat="server" OnClientClick="ChangeRadius('-');return false;" />
									</td>
									<td>
										<asp:ImageButton ID="imgChangeRadiusMax" SkinID="imgChangeRadiusMax" runat="server" OnClientClick="ChangeRadius('+');return false;" />
									</td>
								</tr>
								<tr>
									<td>
									</td>
									<td align="center" style="margin: 0px; padding: 0px; vertical-align: top;">
										<asp:ImageButton ID="imgMoveGeoFenceS" SkinID="imgMoveGeoFenceS" runat="server" OnClientClick="MoveGeoFence('S');return false;" />
									</td>
									<td colspan="4">
									</td>
								</tr>
							</table>
						</div>
					</div>
					<div style="float: right; margin: 10px 10px 0 0;">
						<div id="NewGeofenceButton" style="display: none">
							<asp:Button ID="Geofence_TabArea_Button_GeofenceStoreClose" runat="server" SkinID="LinkButtonCommon"
								OnClientClick="javascript:StoreNewGeofence();return false;" UseSubmitBehavior="False" meta:resourcekey="Geofence_TabArea_Button_GeofenceStoreClose" />
							<asp:Button ID="Geofence_TabArea_Button_InsertGeofence" runat="server" SkinID="LinkButtonCommon" OnClientClick="selectInsertGeofence('select');return false;"
								meta:resourcekey="Geofence_TabArea_Button_InsertGeofence" />
						</div>
						<div id="RegularButtons">
							<asp:Button ID="Geofence_TabArea_Button_NewGeofence" runat="server" SkinID="LinkButtonCommon" OnClientClick="selectInsertGeofence('insert');return false;"
								meta:resourcekey="Geofence_TabArea_Button_NewGeofence" />
							<asp:Button ID="Geofence_TabArea_Button_EditStore" runat="server" SkinID="LinkButtonCommon" OnClientClick="javascript:EditGeofence();return false;"
								UseSubmitBehavior="False" meta:resourcekey="Geofence_TabArea_Button_EditStore" />
							<asp:Button ID="Geofence_TabArea_Button_DeleteGeofence" runat="server" SkinID="LinkButtonCommon" OnClientClick="DeleteGeofence();return false;"
								UseSubmitBehavior="False" meta:resourcekey="Geofence_TabArea_Button_DeleteGeofence" />
							<asp:Button ID="Geofence_TabArea_Button_CancelGeofenceClose" runat="server" SkinID="LinkButtonCommon"
								OnClientClick="javascript:CancelGeofence(); HideModalPopup();return false;" meta:resourcekey="Geofence_TabArea_Button_CancelGeofenceClose" />
						</div>
						<div id="TestDiv">
						<asp:TextBox runat="server" ID="txtGeocode" Text="Bilthoven" Visible="false" />
						<asp:Button runat="server" ID="btnGeocode" OnClientClick="javascript:showAddress();return false;"  Text="Goto Address" Visible="false" />
						</div>
					</div>
				</ContentTemplate>
			</cc1:TabPanel>
			<cc1:TabPanel ID="GeoFenceTab2" runat="server">
				<HeaderTemplate>
					<asp:Label ID="Geofence_TabRelation_Header" runat="server" Width="100px" CssClass="center" meta:resourcekey="Geofence_TabRelation_Header" />
				</HeaderTemplate>
				<ContentTemplate>
					<div style="margin-bottom: 10px">
						<asp:Label ID="Geofence_TabRelation_Label_Intro" runat="server" meta:resourcekey="Geofence_TabRelation_Label_Intro" />
					</div>
					<div style="float: left; display: inline; width: 210px;">
						<asp:Label ID="Geofence_TabRelation_Label_AreaName" runat="server" CssClass="labelitem" meta:resourcekey="Geofence_TabRelation_Label_SelectDeviceName" />:
						<div class="memberpanel" style="height: 245px; margin-top: 15px;">
							<ucl:MembersPanel ID="MembersPanelGeofence" runat="server" />
						</div>
					</div>
					<div style="float: left; display: inline; width: 365px;">
						<table class="boxedcontent" style="border-style: none;">
							<tr>
								<td>
									<asp:Label ID="lblAreaName" runat="server" CssClass="labelitem" meta:resourcekey="Geofence_TabRelation_Label_AreaName" />:
								</td>
								<td>
									<asp:DropDownList ID="ddlSelectAnOption" runat="server" Width="176px" />
								</td>
							</tr>
							<tr>
								<td>
									<asp:Label ID="Geofence_TabRelation_Label_GeofenceType" runat="server" CssClass="labelitem" meta:resourcekey="Geofence_TabRelation_Label_GeofenceType" />:
								</td>
								<td>
									<asp:DropDownList ID="ddlGeofenceOptions" runat="server" Width="176px" />
								</td>
							</tr>
							<asp:Panel ID="pnlNotificationOnce" runat="server">
								<tr>
									<td>
										<asp:Label ID="Geofence_TabRelation_Label_SendNotificationOnce" runat="server" CssClass="labelitem" meta:resourcekey="Geofence_TabRelation_Label_SendNotificationOnce" />:
									</td>
									<td>
										<asp:CheckBox ID="Geofence_TabRelation_Check_Yes" runat="server" meta:resourcekey="Geofence_TabRelation_Check_Yes" CssClass="checkbox" />
									</td>
								</tr>
							</asp:Panel>
							<asp:Panel ID="pnlPeriod" runat="server">
								<tr>
									<td>
										<asp:Label ID="Geofence_TabViewDelete_Grid_InOut" runat="server" CssClass="labelitem" meta:resourcekey="Geofence_TabViewDelete_Grid_InOut" />:
									</td>
									<td>
									</td>
								</tr>
								<tr>
									<td style="text-align: center;">
										<asp:Label ID="Geofence_TabRelation_Label_From" runat="server" CssClass="labelitem" meta:resourcekey="Geofence_TabRelation_Label_From" />
									</td>
									<td>
										<asp:TextBox ID="txtFromDate" runat="server" Width="74px" MaxLength="10" Enabled="False" meta:resourcekey="Geofence_TabRelation_Text_FromDate" />
										<asp:ImageButton ID="imgFromDate" runat="server" OnClientClick="return;" SkinID="ImageMyAccountCalendar" />&nbsp;&nbsp;
										<cc1:CalendarExtender ID="calFromDate" runat="server" Format="dd-MM-yyyy" TargetControlID="txtFromDate"
											PopupButtonID="imgFromDate" FirstDayOfWeek="Monday" Animated="False" CssClass="ajax__calendar ajaxcalendar"
											Enabled="True" />
										<asp:TextBox ID="txtStartHour" runat="server" MaxLength="2" Width="20px" />&nbsp;:
										<asp:TextBox ID="txtStartMin" runat="server" MaxLength="2" Width="20px" />
									</td>
								</tr>
								<tr>
									<td style="text-align: center;">
										<asp:Label ID="Geofence_TabRelation_Label_To" runat="server" CssClass="labelitem" meta:resourcekey="Geofence_TabRelation_Label_To" />
									</td>
									<td>
										<asp:TextBox ID="txtToDate" runat="server" Width="74px" MaxLength="10" Enabled="False" meta:resourcekey="Geofence_TabRelation_Text_DateTo" />
										<asp:ImageButton ID="imgToDate" runat="server" OnClientClick="return;" SkinID="ImageMyAccountCalendar" />&nbsp;&nbsp;
										<cc1:CalendarExtender ID="calToDate" runat="server" Format="dd-MM-yyyy" TargetControlID="txtToDate" PopupButtonID="imgToDate"
											FirstDayOfWeek="Monday" Animated="False" CssClass="ajax__calendar ajaxcalendar" Enabled="True" />
										<asp:TextBox ID="txtEndHour" runat="server" MaxLength="2" Width="20px" />&nbsp;:
										<asp:TextBox ID="txtEndMin" runat="server" MaxLength="2" Width="20px" />
									</td>
								</tr>
							</asp:Panel>
							<asp:Panel ID="pnlActive" runat="server" Visible="False">
								<tr>
									<td>
										<asp:Label ID="Geofence_TabRelation_Label_Active" runat="server" CssClass="labelitem" meta:resourcekey="Geofence_TabRelation_Label_Active" />:
									</td>
									<td>
										<asp:DropDownList ID="ddlActive" runat="server" Width="166px">
											<asp:ListItem Value="-1" meta:resourcekey="Geofence_TabRelation_DDList_SelectOption" />
										</asp:DropDownList>
									</td>
								</tr>
							</asp:Panel>
						</table>
					</div>
					<div style="clear: both; padding: 10px;">
						<ucl:OkCancel ID="OkCancel2" runat="server" OnClickEvent="StoreRelationClicked" OnOKCloseClickEvent="StoreCloseRelationClicked"
							meta:resourcekey="Geofence_TabRelation_Button_OkCancel" />
					</div>
				</ContentTemplate>
			</cc1:TabPanel>
			<cc1:TabPanel ID="GeoFenceTab3" runat="server">
				<HeaderTemplate>
					<asp:Label ID="Geofence_TabViewDelete_Header" runat="server" Width="100px" CssClass="center" meta:resourcekey="Geofence_TabViewDelete_Header" /></HeaderTemplate>
				<ContentTemplate>
					<div style="padding: 0.5em; width: 97%; height: 350px; overflow: auto;">
						<asp:GridView ID="Geofence_TabViewDelete_Grid" runat="server" Width="100%" OnRowDataBound="Geofence_TabViewDelete_Grid_RowDataBound"
							meta:resourcekey="Geofence_TabViewDelete_Grid_NoDataAvailable">
							<Columns>
								<asp:TemplateField meta:resourcekey="Geofence_TabViewDelete_Grid_Device">
									<ItemTemplate>
										<asp:Label ID="lblGeofenceId" runat="server" Visible="False" Text='<%# Bind("Id") %>' />
										<asp:CheckBox ID="chkSelectFence" runat="server" Text='<%# Bind("FmName") %>' CssClass="checkbox" /></ItemTemplate>
									<HeaderStyle CssClass="gridfieldleft" />
								</asp:TemplateField>
								<asp:TemplateField meta:resourcekey="Geofence_TabViewDelete_Grid_GeoFenceHeader">
									<ItemTemplate>
										<asp:Label ID="Geofence_TabViewDelete_Grid_GeoFence" runat="server" meta:resourcekey="Geofence_TabViewDelete_Grid_GeoFence" /></ItemTemplate>
								</asp:TemplateField>
								<asp:TemplateField meta:resourcekey="Geofence_TabViewDelete_Grid_From">
									<ItemTemplate>
										<asp:Label ID="lblFrom" runat="server" Text='<%# Bind("txt__timeStart") %>' /></ItemTemplate>
								</asp:TemplateField>
								<asp:TemplateField meta:resourcekey="Geofence_TabViewDelete_Grid_To">
									<ItemTemplate>
										<asp:Label ID="lblTo" runat="server" Text='<%# Bind("txt__timeEnd") %>' /></ItemTemplate>
								</asp:TemplateField>
								<asp:TemplateField meta:resourcekey="Geofence_TabViewDelete_Grid_Active">
									<ItemTemplate>
										<asp:Label ID="lblActive" runat="server" /></ItemTemplate>
								</asp:TemplateField>
								<asp:TemplateField meta:resourcekey="Geofence_TabViewDelete_Grid_InOutHeader">
									<ItemTemplate>
										<asp:Label ID="lblOutInCrossing" runat="server" CommandName="Test_2" meta:resourcekey="Geofence_TabViewDelete_Grid_InOut" /></ItemTemplate>
								</asp:TemplateField>
								<asp:TemplateField meta:resourcekey="Geofence_TabViewDelete_Grid_Status">
									<ItemTemplate>
										<asp:Image ID="imgStatus" runat="server" /></ItemTemplate>
								</asp:TemplateField>
								<asp:TemplateField meta:resourcekey="Geofence_TabViewDelete_Grid_AlertOnce">
									<ItemTemplate>
										<asp:Label ID="lblAlertOnce" runat="server" />
									</ItemTemplate>
								</asp:TemplateField>
							</Columns>
						</asp:GridView>
					</div>
					<br />
					<ucl:OkCancel ID="OkCancel0" runat="server" OnClickEvent="DeleteClicked" OnOKCloseClickEvent="DeleteCloseClicked" meta:resourcekey="Geofence_ButtonDelete" />
					<br />
				</ContentTemplate>
			</cc1:TabPanel>
		</cc1:TabContainer>
		<div style="margin-top: 5px; display: inline;">
			<asp:Label ID="lblInfo" runat="server" CssClass="lblInfo" ForeColor="Blue" Height="20px" />
		</div>
	</div>
</asp:Panel>
