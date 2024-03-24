<%@ Page Title="Login" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="WebApplication1MileStone1.Login" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container py-5">
        <h2 class="text-center mb-4">LOGIN</h2>
        <p class="text-center mb-4">Please enter your e-mail and password:</p>
        <div class="row justify-content-center">
            <div class="col-md-6 offset-md-3">
                <asp:Label ID="lblLoginMessage" runat="server" Visible="false" CssClass="alert alert-danger"></asp:Label>
                <asp:Label ID="lblMessage" runat="server" CssClass="text-danger" EnableViewState="false" />
                <div class="form-group">
                    <asp:TextBox ID="txtUsername" runat="server" CssClass="form-control mb-3" placeholder="Email" />
                </div>
                <div class="form-group">
                    <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" CssClass="form-control mb-3" placeholder="Password" />
                </div>
                <div class="form-group">
                    <asp:Button ID="btnLogin" runat="server" Text="LOG IN" OnClick="btnLogin_Click" CssClass="btn text-white btn-block mt-3" style="background-color: #1b133c; width: 100%;" />
                </div>
                <div class="form-group mt-3">
                    <p class="text-muted">Don't have an account? <asp:HyperLink ID="lnkRegister" runat="server" NavigateUrl="~/Account/register" CssClass="text-muted text-decoration-none">Create one</asp:HyperLink></p>
                </div>
            </div>
        </div>
    </div>
</asp:Content>



