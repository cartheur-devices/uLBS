<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Notifications.ascx.cs" Inherits="FindWhere.UserControls.UserControl_Notifications" %>
<%@ Register Src="~/UserControl/OkCancel.ascx" TagName="OkCancel" TagPrefix="ucl" %>
<%@ Register Src="~/UserControl/MembersMenuPanel.ascx" TagName="MembersPanel" TagPrefix="uc2" %>
<asp:Panel ID="setPanelMain" runat="server" Width="100%">
	<div class="setContent">
		<table class="boxedcontent">
			<tr>
				<td colspan="2">
					<asp:Label ID="Notifications_Label_Intro" runat="server" CssClass="labelremark" meta:resourcekey="Notifications_Label_Intro" />
				</td>
			</tr>
			<tr>
				<td style="width: 260px;">
					<asp:Label ID="Notifications_Label_SelectNotification" runat="server" CssClass="labelitem" meta:resourcekey="Notifications_Label_SelectNotification" />
				</td>
				<td style="width: 230px;">
					<asp:DropDownList ID="ddlEventsel" runat="server" Width="220px" DataValueField="id" DataTextField="name"
						OnSelectedIndexChanged="ddlEventsel_SelectedIndexChanged" AutoPostBack="True" />
				</td>
			</tr>
			<tr>
				<td>
					<asp:Label ID="Notifications_Label_SelectDevice" runat="server" CssClass="labelitem" meta:resourcekey="Notifications_Label_SelectDevice" />
				</td>
				<td>
					<div class="memberpanel" style="width: 220px; height: 170px;">
						<uc2:MembersPanel ID="MembersPanelNotifications" runat="server" />
					</div>
				</td>
			</tr>
			<tr>
				<td>
					<asp:Label ID="Notifications_Label_SelectContacts" runat="server" CssClass="labelitem" meta:resourcekey="Notifications_Label_SelectContacts" />
				</td>
				<td>
					<div class="memberpanel" style="width: 220px; height: 170px;">
						<asp:GridView ID="gvwContacts" runat="server" OnRowDataBound="gvwContacts_RowDataBound">
							<Columns>
								<asp:BoundField DataField="Name" meta:resourcekey="Notifications_Grid_Choose" ItemStyle-Width="150px">
									<HeaderStyle CssClass="gridfieldleft" />
								</asp:BoundField>
								<asp:TemplateField meta:resourcekey="Notifications_Grid_Text" ItemStyle-Width="50px">
									<ItemTemplate>
										<asp:CheckBox ID="chkSms" runat="server" CssClass="checkbox" />
									</ItemTemplate>
								</asp:TemplateField>
								<asp:TemplateField meta:resourcekey="Notifications_Grid_Email" ItemStyle-Width="50px">
									<ItemTemplate>
										<asp:CheckBox ID="chkEmail" runat="server" CssClass="checkbox" />
									</ItemTemplate>
								</asp:TemplateField>
							</Columns>
						</asp:GridView>
					</div>
				</td>
			</tr>
			<tr>
				<td colspan="2" style="padding-left: 52px; vertical-align: middle;">
					<asp:Label ID="lblInfo" runat="server" CssClass="lblInfo" Height="18px" />
				</td>
			</tr>
			<tr>
				<td colspan="2">
					<ucl:OkCancel ID="SubmitCancel1" runat="server" OnClickEvent="OkClicked" OnCancelEvent="CancelClicked"
						OnOKCloseClickEvent="OkCloseClicked" />
				</td>
			</tr>
		</table>
	</div>
</asp:Panel>
