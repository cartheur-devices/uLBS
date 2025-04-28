<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Wiconfig.ascx.cs" Inherits="FindWhere.UserControls.UserControl_Wiconfig" %>
<%@ Register Src="~/UserControl/OkCancel.ascx" TagName="OkCancel" TagPrefix="ucl" %>
<%@ Register Src="~/UserControl/SaveInfo.ascx" TagName="SaveInfo" TagPrefix="uc1" %>
<asp:Panel ID="setPanelMain" runat="server" Width="100%">
	<div class="setContent">
		<table class="boxedcontent">
			<tr class="tablerowheader">
				<td>
					<asp:Label ID="lblWiconfigInfo" runat="server" meta:resourcekey="WiConfig_Label_Header" />
				</td>
			</tr>
			<tr>
				<td>
					<div style="height: 140px; overflow: auto;">
						<asp:GridView ID="gridViewWIDevices" runat="server" Width="97%" OnRowDataBound="gridViewWIDevices_RowDataBound"
							OnSelectedIndexChanged="gridViewWIDevices_SelectedIndexChanged" AutoEventWireup="true">
							<Columns>
								<asp:TemplateField meta:resourcekey="WiConfig_Grid_DeviceName">
									<ItemTemplate>
										<asp:LinkButton ID="lblAlias" runat="server" CommandName="select" Text='<%# Bind("Alias") %>' />
									</ItemTemplate>
									<HeaderStyle CssClass="gridfieldleft" />
									<ItemStyle CssClass="gridfieldleft" />
								</asp:TemplateField>
								<asp:TemplateField HeaderText="Member" Visible="False" meta:resourcekey="WiConfig_Grid_Device">
									<ItemTemplate>
										<asp:Label ID="lblUserId" runat="server" Text='<%# Bind("Userid") %>' Visible="False" />
									</ItemTemplate>
								</asp:TemplateField>
								<asp:BoundField meta:resourcekey="WiConfig_Grid_NetworkInterval" />
								<asp:BoundField meta:resourcekey="WiConfig_Grid_RoamingInterval" />
								<asp:BoundField meta:resourcekey="WiConfig_Grid_Skip" />
								<asp:BoundField meta:resourcekey="WiConfig_Grid_SpeedAlert">
									<HeaderStyle CssClass="center" />
									<ItemStyle CssClass="center" />
								</asp:BoundField>
								<asp:BoundField meta:resourcekey="WiConfig_Grid_SendSms" />
								<asp:BoundField meta:resourcekey="WiConfig_Grid_LocationExchange" />
							</Columns>
						</asp:GridView>
					</div>
				</td>
			</tr>
			<tr>
				<td>
					<asp:Label ID="lblWiconfigSelectUnitText" runat="server" meta:resourcekey="WiConfig_Label_ClickDeviceToChange" />
				</td>
			</tr>
		</table>
		<div style="width: 80%; margin-top: 10px;">
			<table class="boxedcontenttab">
				<tr>
					<td class="headeritem" style="width: 50%;">
						<asp:Label ID="Label1" runat="server" meta:resourcekey="WiConfig_Label_SettingsFor" /></span>
						<asp:Label ID="lblAlias" runat="server" Font-Bold="true" />
					</td>
					<td class="headeritem" style="width: 20%;">
						<asp:Label ID="Label3" runat="server" meta:resourcekey="WiConfig_Label_Header_Current" />
					</td>
					<td class="headeritem" style="width: 30%;">
						<asp:Label ID="Label4" runat="server" meta:resourcekey="WiConfig_Label_Header_Requested" />
					</td>
				</tr>
				<tr>
					<td>
						<asp:Label ID="Label5" runat="server" CssClass="labelitem" meta:resourcekey="WiConfig_Label_NetworkInterval" />
					</td>
					<td>
						<asp:Label ID="lblWiconfigCurrNatint" runat="server" />
					</td>
					<td>
						<asp:DropDownList ID="ddlPulldown_natint" runat="server" Width="160px">
							<asp:ListItem Value="-1" meta:resourcekey="WiConfig_DDList_SelectOption" />
						</asp:DropDownList>
					</td>
				</tr>
				<tr>
					<td>
						<asp:Label ID="Label6" runat="server" CssClass="labelitem" meta:resourcekey="WiConfig_Label_RoamingInterval" />
					</td>
					<td>
						<asp:Label ID="lblWiconfigCurrIntint" runat="server" />
					</td>
					<td>
						<asp:DropDownList ID="ddlPulldown_intint" runat="server" Width="160px">
							<asp:ListItem Value="-1" meta:resourcekey="WiConfig_DDList_SelectOption" />
						</asp:DropDownList>
					</td>
				</tr>
				<tr>
					<td>
						<asp:Label ID="Label7" runat="server" CssClass="labelitem" meta:resourcekey="WiConfig_Label_NumberSkippingMessagesNotMoving" />
					</td>
					<td>
						<asp:Label ID="lblWiconfigCurrNotmov" runat="server" />
					</td>
					<td>
						<asp:DropDownList ID="ddlPulldown_notmov" runat="server" Width="160px">
							<asp:ListItem Value="-1" meta:resourcekey="WiConfig_DDList_SelectOption" />
						</asp:DropDownList>
					</td>
				</tr>
				<tr>
					<td>
						<asp:Label ID="lblWiconfigHeaderSpeed" runat="server" CssClass="labelitem" meta:resourcekey="WiConfig_Label_km" />
					</td>
					<td>
						<asp:Label ID="lblWiconfigCurrSpeed" runat="server" />
					</td>
					<td>
						<asp:TextBox ID="txtInput_speed" runat="server" Width="137px" MaxLength="14" />
					</td>
				</tr>
				<tr>
					<td>
						<asp:Label ID="Label2" runat="server" CssClass="labelitem" meta:resourcekey="WiConfig_Label_SendingSmsNotices" />
					</td>
					<td>
						<asp:Label ID="WiConfig_Label_SendingSmsNotices_Value" runat="server" />
					</td>
					<td>
						<asp:DropDownList ID="WiConfig_DDList_SendingSmsNotices" runat="server" Width="160px" />
					</td>
				</tr>
			</table>
		</div>
		<div style="text-align: center;">
			<asp:Label ID="lblInfo" runat="server" CssClass="lblInfo" Height="10px" />
		</div>
		<div>
			<ucl:OkCancel ID="OkCancel" runat="server" OnClickEvent="SaveClicked" OnOKCloseClickEvent="OkCloseClicked"
				OnCancelEvent="CancelClicked" />
		</div>
		<div style="clear: both;">
		</div>
	</div>
</asp:Panel>
