<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="WebApplication1MileStone1.Register" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container py-5">
        <h2 class="text-center mb-4">REGISTER</h2>
        <p class="text-center mb-4">Please fill in the information below:</p>
        <div class="row justify-content-center">
            <div class="col-md-6 offset-md-3">
                <asp:Label ID="lblMessage" runat="server" CssClass="text-danger" EnableViewState="false" />
                <div class="form-group">
                    <asp:TextBox ID="txtFirstName" runat="server" CssClass="form-control mb-3" placeholder="First name" />
                </div>
                <div class="form-group">
                    <asp:TextBox ID="txtLastName" runat="server" CssClass="form-control mb-3" placeholder="Last name" />
                </div>
                <div class="form-group">
                    <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control mb-3" placeholder="Email" />
                </div>
                <div class="form-group">
                    <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" CssClass="form-control mb-3" placeholder="Password" />
                </div>
                <div class="form-group">
                    <asp:Button ID="btnRegister" runat="server" Text="CREATE MY ACCOUNT" OnClick="btnRegister_Click" CssClass="btn text-white btn-block mt-3" style="background-color: #1b133c; width: 100%;" />
                </div>
                <div class="form-group mt-3">
                    <p class="text-muted">Already have an account? <asp:HyperLink ID="lnkLogin" runat="server" NavigateUrl="~/Account/login" CssClass="text-muted text-decoration-none">Login</asp:HyperLink></p>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
