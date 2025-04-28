<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Setup.ascx.cs" Inherits="FindWhere.UserControls.UserControl_Setup" %>
<%@ Register Src="~/UserControl/OkCancel.ascx" TagName="OkCancel" TagPrefix="ucl" %>
<asp:Panel ID="setPanelMain" runat="server" Width="500" CssClass="setpanelmain">
	<div class="setContent">
		<table class="boxedcontent">
			<td style="width: 100%;" colspan="2">
				<table style="width: 100%;">
					<tr>
						<td>
							<asp:Label ID="Label1" runat="server" CssClass="labelitem" meta:resourcekey="Setup_Label_EnterNewPassword" />
						</td>
						<td>
							<asp:TextBox ID="txtPassword" runat="server" Width="80px" TextMode="Password" MaxLength="8" meta:resourcekey="Setup_Label_EnterNewPassword" />
							<br />
						</td>
					</tr>
					<tr>
						<td>
							<asp:Label ID="lblMyAccountConfirmNewPassword" runat="server" meta:resourcekey="Setup_Label_ConfirmNewPassword" />
						</td>
						<td>
							<asp:TextBox ID="MyAccount_Text_Tab_Personal_PassWordConfirm" runat="server" Width="80px" TextMode="Password" MaxLength="8"
								ToolTip="maxCharactersPassword - 8" />
							<br />
						</td>
					</tr>
					<tr>
						<td>
						</td>
						<td>
							<br />
						</td>
					</tr>
					<tr>
						<td>
							<asp:Label ID="Label3" runat="server" Text="Timezone :" meta:resourcekey="Setup_Label_Timezone"  />
						</td>
						<td>
							<asp:DropDownList ID="ddlSelectTimeZone" runat="server" Width="180px" />
							<br />
						</td>
					</tr>
				</table>
			</td>
			</tr>
		</table>
		<div style="margin: 15px 0 10px 0;">
			<ucl:OkCancel ID="SubmitCancel1" runat="server" OnClickEvent="OkClicked" OnCancelEvent="CancelClicked" meta:resourcekey="Setup_Button_OkCancel" />
		</div>
	</div>
</asp:Panel>
<asp:Label ID="lblInfo" runat="server" CssClass="lblInfo" />