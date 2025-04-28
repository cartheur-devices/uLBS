<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SendAction.ascx.cs" Inherits="FindWhere.UserControls.UserControl_SendAction" %>
<%@ Register Src="~/UserControl/OkCancel.ascx" TagName="OkCancel" TagPrefix="ucl" %>
<%@ Register Src="~/UserControl/MembersMenuPanel.ascx" TagName="MembersPanel" TagPrefix="uc2" %>
<asp:Panel ID="setPanelMain" runat="server" Width="100%">
    <div class="setContent">
        <table class="boxedcontent">
            <asp:Panel runat="server" ID="pnlActionOneTwo">
                <tr>
                    <td colspan="3">
                        <asp:Label ID="SendAction_Label_Intro" runat="server" CssClass="labelremark" meta:resourcekey="SendAction_Label_Intro" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Notifications_Label_SelectDevice" runat="server" CssClass="labelitem" meta:resourcekey="Notifications_Label_SelectDevice" />
                    </td>
                    <td colspan="2">
                        <div class="memberpanel" style="width: 220px; height: 170px;">
                            <uc2:MembersPanel ID="MembersPanelSendActions" runat="server" />
                        </div>
                    </td>
                </tr>
            </asp:Panel>
            <asp:Panel runat="server" ID="pnlActionThree">
                <tr>
                    <td class="headeritem" style="width: 40%;">
                        <asp:Label ID="Label3" runat="server" meta:resourcekey="SendAction_TabStandard_Label_Header_Name" />
                    </td>
                    <td class="headeritem" style="width: 30%;">
                        <asp:Label ID="Label5" runat="server" meta:resourcekey="SendAction_TabStandard_Label_Header_Current" />
                    </td>
                    <td class="headeritem" style="width: 30%;">
                        <asp:Label ID="Label6" runat="server" meta:resourcekey="SendAction_TabStandard_Label_Header_Requested" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:DropDownList runat="server" ID="ddlSelectDevice" DataTextField="Alias" DataValueField="UserId" OnSelectedIndexChanged="ddlSelectDeviceSelectedIndexChanged"
                            AutoPostBack="true" />
                    </td>
                    <td>
                        <asp:Label ID="LabelIO3" runat="server" meta:resourcekey="SendAction_DDList_Off" />
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlIO3" runat="server" Width="200px">
                            <asp:ListItem Text="Select an option" Value="-1" meta:resourcekey="SendAction_DDList_SelectOption" />
                            <asp:ListItem Value="1" meta:resourcekey="SendAction_DDList_On" />
                            <asp:ListItem Value="0" meta:resourcekey="SendAction_DDList_Off" />
                        </asp:DropDownList>
                    </td>
                </tr>
            </asp:Panel>
            <tr>
                <td style="padding-left: 52px; vertical-align: middle;" colspan="3">
                    <asp:Label ID="lblInfo" runat="server" CssClass="lblInfo" Height="18px" />
                </td>
            </tr>
            <tr>
                <td colspan="3">
                    <ucl:OkCancel ID="SubmitCancel1" runat="server" OnClickEvent="OkClicked" OnCancelEvent="CancelClicked"
                        OnOKCloseClickEvent="OkCloseClicked" />
                </td>
            </tr>
        </table>
    </div>
</asp:Panel>
