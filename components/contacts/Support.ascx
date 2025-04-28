<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Support.ascx.cs" Inherits="FindWhere.UserControls.UserControl_Support" %>
<%@ Register Src="~/UserControl/OkCancel.ascx" TagName="OkCancel" TagPrefix="ucl" %>
<asp:Panel ID="setPanelMain" runat="server" Width="100%">
	<div class="setContent">
		<table class="boxedcontent">
			<tr>
				<td colspan="2">
					<asp:Label ID="Support_Label_Intro" runat="server" CssClass="labelremark"  />
				</td>
			</tr>
			<tr>
				<td>
					<asp:Label ID="Support_Label_Name" runat="server" CssClass="labelitem" meta:resourcekey="Support_Label_Name" />
				</td>
				<td>
					<asp:TextBox ID="txtSupport_YourName" runat="server" />
					<asp:RequiredFieldValidator ID="req1" runat="server" ControlToValidate="txtSupport_YourName" Text="!" />
				</td>
			</tr>
			<asp:Panel ID="Support_EditDeviceName_Panel" runat="server">
				<tr>
					<td>
						<asp:Label ID="Support_Label_NewName" runat="server" CssClass="labelitem" meta:resourcekey="Support_Label_Email" />
					</td>
					<td>
						<asp:TextBox ID="txtSupport_EmailAddress" runat="server" MaxLength="200"  />
						<asp:RequiredFieldValidator ID="req2" runat="server" ControlToValidate="txtSupport_EmailAddress" Text="!" />
					</td>
				</tr>
				<tr>
					<td>
						<asp:Label ID="Support_Label_DaytimePhone" runat="server" CssClass="labelitem" meta:resourcekey="Support_Label_DaytimePhone" />
					</td>
					<td>
						<asp:TextBox ID="txtDaytimePhone" runat="server" MaxLength="200"  />
						<asp:RequiredFieldValidator ID="req3" runat="server" ControlToValidate="txtDaytimePhone" Text="!" />
					</td>
				</tr>
				<tr>
					<td>
						<asp:Label ID="Support_Label_Subject" runat="server" CssClass="labelitem" meta:resourcekey="Support_Label_Vehicle" />
					</td>
					<td>
						<asp:TextBox ID="txtVehicle" runat="server" MaxLength="200"  />
					</td>
				</tr>
					<tr>
					<td>
						<asp:Label ID="Label1" runat="server" CssClass="labelitem" meta:resourcekey="Support_Label_Model" />
					</td>
					<td>
						<asp:TextBox ID="txtModel" runat="server" MaxLength="200"  />
					</td>
				</tr>
				<tr>
					<td>
						<asp:Label ID="Support_Label_Message" runat="server" CssClass="labelitem" meta:resourcekey="Support_Label_Message" />
					</td>
					<td>
						<asp:TextBox ID="txtMessage" runat="server" MaxLength="200"  TextMode="MultiLine" Height="100px" Width="400px" />
						<asp:RequiredFieldValidator ID="req4" runat="server" ControlToValidate="txtMessage" Text="!" />
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
				 		OnOKCloseClickEvent="OkCloseClicked"  meta:resourcekey="Support_Button_OkCancel" />
				</td>
			</tr>
		</table>
		<div style="clear: both;">
		</div>
	</div>
</asp:Panel>
