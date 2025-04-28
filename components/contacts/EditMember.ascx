<%@ Control Language="C#" AutoEventWireup="true" CodeFile="EditMember.ascx.cs" Inherits="FindWhere.UserControls.UserControl_EditMember" %>
<%@ Register Src="~/UserControl/OkCancel.ascx" TagName="OkCancel" TagPrefix="ucl" %>
<asp:Panel ID="setPanelMain" runat="server" Width="100%">
	<div class="setContent">
		<table class="boxedcontent">
			<tr>
				<td colspan="2">
					<asp:Label ID="EditMember_Label_Intro" runat="server" CssClass="labelremark"  />
				</td>
			</tr>
			<tr>
				<td>
					<asp:Label ID="EditMember_Label_SelectDevice" runat="server" CssClass="labelitem" meta:resourcekey="EditMember_Label_SelectDevice" />
				</td>
				<td>
					<asp:DropDownList ID="ddlMember" runat="server" Width="140px" DataTextField="Alias" AutoPostBack="True"
						DataValueField="UserId" AppendDataBoundItems="True" OnSelectedIndexChanged="ddlMember_SelectedIndexChanged">
						<asp:ListItem Value="-1" meta:resourcekey="EditMember_DDList_SelectedDevice" />
					</asp:DropDownList>
				</td>
			</tr>
			<asp:Panel ID="EditMember_EditDeviceName_Panel" runat="server">
				<tr>
					<td>
						<asp:Label ID="EditMember_Label_NewName" runat="server" CssClass="labelitem" meta:resourcekey="EditMember_Label_NewName" />
					</td>
					<td>
						<asp:TextBox ID="EditMember_Text_NewName" runat="server" Width="137px" MaxLength="8" meta:resourcekey="EditMember_Text_NewName" />
					</td>
				</tr>
				<tr>
					<td colspan="2" align="center">
						<asp:RequiredFieldValidator ID="reqNickname" runat="server" ControlToValidate="EditMember_Text_NewName"
							meta:resourcekey="EditMember_Validator_NameRequired" /><br />
					</td>
				</tr>
				<tr>
					<td>
						<asp:Label ID="EditMember_Label_SelectMarker" runat="server" CssClass="labelitem" meta:resourcekey="EditMember_Label_SelectMarker" />
					</td>
					<td>
						<asp:Panel ID="pnlMarkers" Width="138px" Height="134px" ScrollBars="Vertical" runat="server" BorderStyle="Solid"
							BorderColor="#CCCCCC" BorderWidth="1px">
							<div style="padding: 2px;">
								<asp:GridView ID="gvMarkers" ShowHeader="False" runat="server" meta:resourcekey="EditMember_Grid_SelectAMarker">
									<AlternatingRowStyle BackColor="White" />
									<Columns>
										<asp:TemplateField>
											<ItemTemplate>
												<div style="width: 21px;">
													<asp:ImageButton ID="ibMarkerVerySmall" runat="server" ImageAlign="AbsMiddle" ImageUrl='<%# Eval("MarkerVerySmall") %>'
														OnClick="Marker_Click" />
												</div>
											</ItemTemplate>
											<ItemStyle BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px" Width="21px" />
										</asp:TemplateField>
										<asp:TemplateField>
											<ItemTemplate>
												<div style="width: 29px;">
													<asp:ImageButton ID="ibMarkerSmall" runat="server" ImageAlign="AbsMiddle" ImageUrl='<%# Eval("MarkerSmall") %>'
														OnClick="Marker_Click" />
												</div>
											</ItemTemplate>
											<ItemStyle BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px" Width="29px" />
										</asp:TemplateField>
										<asp:TemplateField>
											<ItemTemplate>
												<div style="width: 33px;">
													<asp:ImageButton ID="ibMarkerNormal" runat="server" ImageAlign="AbsMiddle" ImageUrl='<%# Eval("MarkerNormal") %>'
														OnClick="Marker_Click" />
												</div>
											</ItemTemplate>
											<ItemStyle BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px" Width="33px" />
										</asp:TemplateField>
									</Columns>
									<RowStyle BackColor="White" />
									<SelectedRowStyle BackColor="White" />
								</asp:GridView>
							</div>
						</asp:Panel>
					</td>
				</tr>
				<tr>
					<td>
						<asp:Label ID="EditMember_Label_SelecedMarker" runat="server" CssClass="labelitem" meta:resourcekey="EditMember_Label_SelecedMarker" />
					</td>
					<td>
						<asp:Image ID="imgSelected" runat="server" />
					</td>
				</tr>
				<tr>
					<td colspan="2" style="padding-left: 60px; vertical-align: middle;">
						<asp:Label ID="lblInfo" runat="server" CssClass="lblInfo" Height="10px" />
					</td>
				</tr>
			</asp:Panel>
			<tr>
				<td colspan="2">
					<ucl:OkCancel ID="SubmitCancel1" runat="server" OnClickEvent="OkClicked" OnCancelEvent="CancelClicked" 
						OnOKCloseClickEvent="OkCloseClicked" meta:resourcekey="EditMember_Button_OkCancel"/>
				</td>
			</tr>
		</table>
		<div style="clear: both;">
		</div>
	</div>
</asp:Panel>
