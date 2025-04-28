<%@ Control Language="C#" AutoEventWireup="true" CodeFile="LbsExport.ascx.cs" Inherits="FindWhere.UserControls.UserControl_LbsExport" %>
<%@ Register Src="~/UserControl/OkCancel.ascx" TagName="OkCancel" TagPrefix="ucl" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/UserControl/MembersMenuPanel.ascx" TagName="MembersPanel" TagPrefix="uc1" %>
<asp:Panel ID="setPanelMain" runat="server" Width="100%" meta:resourcekey="setPanelMainResource1">
	<div class="setContent">
		<table class="boxedcontent">
			<tr>
				<td colspan="3">
					<asp:Label ID="Export_Label_Intro" runat="server" CssClass="labelremark" meta:resourcekey="Export_Label_Intro" />
				</td>
			</tr>
			<tr>
				<td style="width: 200px;" rowspan="5">
					<asp:Label ID="Export_Label_SelectDeviceName" runat="server" CssClass="labelitem" meta:resourcekey="Export_Label_SelectDeviceName" />
					<div class="memberpanel" style="height: 300px; margin-top: 15px;">
						<uc1:MembersPanel ID="MembersPanelExport" runat="server" />
					</div>
				</td>
				<td style="width: 210px;">
					<asp:Label ID="Export_Label_SelectDataToExport" runat="server" CssClass="labelitem" meta:resourcekey="Export_Label_SelectDataToExport" />
				</td>
				<td style="width: 180px;">
					<asp:DropDownList ID="ddlDatatype" runat="server" Width="166px" AutoPostBack="True" OnSelectedIndexChanged="ddlDatatypeSelectedIndexChanged" />
				</td>
			</tr>
			<tr>
				<td>
					<asp:Label ID="lblLbsexportSelectPeriod" runat="server" CssClass="labelitem" meta:resourcekey="Export_Label_SelectDataRange" />
				</td>
				<td>
					<asp:TextBox ID="Export_Text_DateFrom" runat="server" Width="74px" MaxLength="10" Enabled="False" meta:resourcekey="Export_Text_DateFrom" />
					<asp:ImageButton ID="imgFromDate" runat="server" SkinID="ImageMyAccountCalendar" OnClientClick="return;" />&nbsp;&nbsp;
					<cc1:CalendarExtender ID="calFromDate" runat="server" TargetControlID="Export_Text_DateFrom" PopupButtonID="imgFromDate"
						FirstDayOfWeek="Monday" Animated="False" CssClass="ajax__calendar ajaxcalendar" Enabled="True" />
					<asp:DropDownList ID="ddlFromTime" runat="server" Width="60px" />
				</td>
			</tr>
			<tr>
				<td>
				</td>
				<td>
					<asp:TextBox ID="Export_Text_DateTo" runat="server" Width="74px" MaxLength="10" Enabled="False" meta:resourcekey="Export_Text_DateTo" />
					<asp:ImageButton ID="imgToDate" runat="server" SkinID="ImageMyAccountCalendar" OnClientClick="return;" />&nbsp;&nbsp;
					<cc1:CalendarExtender ID="calToDate" runat="server" TargetControlID="Export_Text_DateTo" PopupButtonID="imgToDate"
						FirstDayOfWeek="Monday" Animated="False" CssClass="ajax__calendar ajaxcalendar" Enabled="True">
					</cc1:CalendarExtender>
					<asp:DropDownList ID="ddlToTime" runat="server" Width="60px" />
				</td>
			</tr>
			<tr>
				<td>
					<asp:Label ID="lblLbsexportFormatHeading" runat="server" CssClass="labelitem" meta:resourcekey="Export_Label_SelectFormat" />
				</td>
				<td>
					<asp:DropDownList ID="ddlSelectFormat" runat="server" Width="166px" />
				</td>
			</tr>
			<tr>
				<td style="vertical-align: middle;">
					<asp:Label ID="Export_Label_DecimalPoint" runat="server" CssClass="labelitem" meta:resourcekey="Export_Label_DecimalPoint" />
				</td>
				<td style="vertical-align: middle;">
					<asp:RadioButtonList ID="RadioButtonList1" runat="server" RepeatDirection="Horizontal">
						<asp:ListItem Value="1" meta:resourcekey="Export_ButtonListItem_Period" />
						<asp:ListItem Value="0" meta:resourcekey="Export_ButtonListItem_Comma" />
					</asp:RadioButtonList>
				</td>
			</tr>
			<tr>
				<td colspan="3" style="padding-left: 160px; vertical-align: middle;">
					<asp:Label ID="lblInfo" runat="server" CssClass="lblInfo" Height="20px" />
				</td>
			</tr>
			<tr>
				<td colspan="3">
					<ucl:OkCancel ID="Export_Button_OkCancel" runat="server" OnClickEvent="OkClicked" OnCancelEvent="CancelClicked"
						OnOKCloseClickEvent="OkCloseClicked" Text_Ok="Export" meta:resourcekey="Export_Button_OkCancel" />
				</td>
			</tr>
		</table>
	</div>
</asp:Panel>
