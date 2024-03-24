<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="OrderConfirmation.aspx.cs" Inherits="WebApplication1MileStone1.OrderConfirmation" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container">
        <div class="row">
            <div class="col-lg-12">
                <h2>Thank you for your purchase!</h2>
                <p>Your order has been placed successfully.</p>
                <h3>Order Details:</h3>              
              <asp:GridView ID="gvOrderDetails" runat="server" CssClass="table table-striped" AutoGenerateColumns="False">
    <Columns>
        <asp:BoundField DataField="OrderID" HeaderText="Order ID" />
        <asp:BoundField DataField="ProductID" HeaderText="Product ID" />
        <asp:BoundField DataField="Quantity" HeaderText="Quantity" />
        <asp:BoundField DataField="Subtotal" HeaderText="Subtotal" DataFormatString="{0:C}" />
        <asp:BoundField DataField="OrderDate" HeaderText="Order Date" DataFormatString="{0:d}" />
        <asp:BoundField DataField="Username" HeaderText="Username" />
    </Columns>
</asp:GridView>
        </div>
    </div>
    </div>
</asp:Content>

