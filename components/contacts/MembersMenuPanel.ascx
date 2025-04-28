<%@ Control Language="C#" AutoEventWireup="true" CodeFile="MembersMenuPanel.ascx.cs" Inherits="FindWhere.UserControls.UserControl_MembersMenuPanel" %>
<div>
	<asp:TreeView ID="tvMembers" runat="server" EnableClientScript="true" Target="_self" NodeIndent="0" EnableViewState="false" />
	<asp:HiddenField ID="hdnSelected" runat="server" Value="" EnableViewState="true" />
	<asp:HiddenField ID="hdnAllMembers" runat="server" Value="" EnableViewState="true" />
</div>
<%--autopostback="false"  uit treeview gehaald RPH  2008 - 11 - 10--%>