<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Notification.ascx.cs" Inherits="FindWhere.UserControls.UserControl_Notification" %>
<%@ Register Src="~/UserControl/OkCancel.ascx" TagName="OkCancel" TagPrefix="ucl" %>

<%--  This is the PopUp with Balance.  --%>
<asp:Panel ID="setPanelMain" runat="server" Width="100%">
	<div class="setContent">
		<table class="boxedcontent">
			<tr>
				<td colspan="2">
					<div style="height: 350px; overflow: auto">
						<asp:Label ID="lblNotification" runat="server" Width="540px" />
						<div style="padding: 10px 0 10px 0;">
							<asp:Label ID="lblNotificationMessage" runat="server" />
						</div>
						<div style="float: right;">
							<asp:Button ID="cmdNotification" runat="server" SkinID="LinkButtonCommon" />
						</div>
					</div>
				</td>
			</tr>
			<tr>
				<td>
					<asp:Label ID="lblInfo" runat="server" CssClass="lblInfo" />
				</td>
				<td>
					<ucl:OkCancel ID="SubmitCancel1" runat="server" OnCancelEvent="CancelClicked" OnClickEvent="OkClicked" />
				</td>
			</tr>
		</table>
	</div>
</asp:Panel>
