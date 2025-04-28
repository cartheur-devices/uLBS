<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Geocoding.ascx.cs" Inherits="FindWhere.UserControls.UserControl_Geocoding" %>
<%@ Register Src="~/UserControl/OkCancel.ascx" TagName="OkCancel" TagPrefix="ucl" %>
<%@ Register Src="~/UserControl/MembersPanel.ascx" TagName="MembersPanel" TagPrefix="ucl" %>
<asp:Panel ID="setPanelMain" runat="server" Width="100%">
	<div class="setContent">
		<table class="boxedcontent">
			<tr>
				<td colspan="2">
					<asp:Label ID="Geocoding_Label_Intro" runat="server" Normal CssClass="labelremark" meta:resourcekey="Geocoding_Label_Intro" />
				</td>
			</tr>
			<tr>
				<td style="width: 180px;">
					<asp:Label ID="Geocoding_Label_SelectDevice" runat="server" CssClass="labelitem" meta:resourcekey="Geocoding_Label_SelectDevice" />
				</td>
				<td>
					<div class="memberpanel" style="width: 235px; height: 250px;">
						<div style="width: 200px;">
							<asp:Repeater ID="repPopupTTProfileMembersList" runat="server" OnItemDataBound="repPopupTTProfileMembersList_ItemDataBound">
								<HeaderTemplate>
									<div style="margin-bottom: 12px; padding-left: 10px; width: 100%; background-color: #D8D8D8;">
										<asp:Label ID="Geocoding_Grid_Device" runat="server" meta:resourcekey="Geocoding_Grid_Device" Width="100px"
											Font-Bold="true" />
										<asp:Label ID="Geocoding_Grid_Address" runat="server" meta:resourcekey="Geocoding_Grid_Address" Font-Bold="true" />
									</div>
								</HeaderTemplate>
								<ItemTemplate>
									<div style="width: 100%; margin-bottom: 10px; padding-left: 10px;">
										<asp:CheckBox ID="chkMember" runat="server" Text='<%# Eval("Alias") %>' Width="130px" TextAlign="Right" CssClass="checkbox" />
										<asp:Label ID="lblMSISDN" runat="server" Text='<%# Eval("MSISDN") %>' Visible="False" />
										<asp:Label ID="Geocoding_Grid_On" runat="server" meta:resourcekey="Geocoding_Grid_On" Width="40px" />
										<asp:Label ID="Geocoding_Grid_Off" runat="server" meta:resourcekey="Geocoding_Grid_Off" Width="40px" />
									</div>
								</ItemTemplate>
							</asp:Repeater>
						</div>
					</div>
				</td>
			</tr>
			<tr>
				<td>
					<asp:Label ID="Geocoding_Label_SelectOnOff" runat="server" CssClass="labelitem" meta:resourcekey="Geocoding_Label_SelectOnOff" />
				</td>
				<td>
					<asp:DropDownList ID="ddlActionSelect" runat="server" Width="238px" OnSelectedIndexChanged="ddlActionSelect_SelectedIndexChanged"
						AutoPostBack="True">
						<asp:ListItem meta:resourcekey="Geocoding_DDList_SelectOption" />
						<asp:ListItem Value="On" meta:resourcekey="Geocoding_DDList_On" />
						<asp:ListItem Value="Off" meta:resourcekey="Geocoding_DDList_Off" />
					</asp:DropDownList>
				</td>
			</tr>
			<tr>
				<td colspan="2" style="padding-left: 60px;">
					<asp:Label ID="lblInfo" runat="server" CssClass="lblInfo" Height="20px" />
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
