<%@ Page Title="Error" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ErrorPage.aspx.cs" Inherits="WebApplication1MileStone1.ErrorPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2>An Error Occurred</h2>
    <asp:Label ID="FriendlyErrorMsg" runat="server" CssClass="text-danger"></asp:Label>

    <asp:Panel ID="DetailedErrorPanel" runat="server" Visible="false">
        <h3>Error Details (for local access only)</h3>
        <p><strong>Error Message:</strong> <asp:Label ID="ErrorDetailedMsg" runat="server"></asp:Label></p>
        <p><strong>Error Handler:</strong> <asp:Label ID="ErrorHandler" runat="server"></asp:Label></p>
        <p><strong>Inner Exception Message:</strong> <asp:Label ID="InnerMessage" runat="server"></asp:Label></p>
        <p><strong>Inner Exception Trace:</strong> <asp:Label ID="InnerTrace" runat="server"></asp:Label></p>
    </asp:Panel>
</asp:Content>


