<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Checkout.aspx.cs" Inherits="WebApplication1MileStone1.Checkout" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container">
        <div class="row">
            <div class="col-md-12 text-center mb-4">
                <h1>Checkout</h1>
            </div>
        </div>
        <div class="row">
            <div class="col-md-6">
                <h5>Shipping Information</h5>
                <asp:Label ID="lblMessage" runat="server" Visible="false" CssClass="alert alert-danger"></asp:Label>
                <div class="form-group">
                    <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control mb-3" placeholder="Email" />
                </div>
                <div class="form-group">
                    <asp:TextBox ID="txtCountry" runat="server" CssClass="form-control mb-3" placeholder="Country" />
                </div>
                <div class="form-group">
                    <asp:TextBox ID="txtAddress" runat="server" CssClass="form-control mb-3" placeholder="Address" />
                </div>
                <div class="form-group">
                    <asp:TextBox ID="txtApartment" runat="server" CssClass="form-control mb-3" placeholder="Apartment, suite, etc. (optional)" />
                </div>
                <div class="form-group">
                    <asp:TextBox ID="txtCity" runat="server" CssClass="form-control mb-3" placeholder="City" />
                </div>
                <div class="form-group">
                    <asp:TextBox ID="txtState" runat="server" CssClass="form-control mb-3" placeholder="State" />
                </div>
                <div class="form-group">
                    <asp:TextBox ID="txtZipCode" runat="server" CssClass="form-control mb-3" placeholder="ZIP Code" />
                </div>
                <h5 class="mt-4">Payment Information</h5>
                <div class="form-group">
                    <asp:TextBox ID="txtCardNumber" runat="server" CssClass="form-control mb-3" placeholder="Card Number" />
                </div>
                <div class="form-group">
                    <asp:TextBox ID="txtExpirationDate" runat="server" CssClass="form-control mb-3" placeholder="Expiration Date" />
                </div>
                <div class="form-group">
                    <asp:TextBox ID="txtCVV" runat="server" CssClass="form-control mb-3" placeholder="CVV" />
                </div>
                <asp:Button ID="btnCompleteOrder" runat="server" Text="Complete Order" OnClick="btnCompleteOrder_Click" CssClass="btn btn-danger mt-3" />
            </div>
            <div class="col-md-6">
                <asp:GridView ID="gvCart" runat="server" AutoGenerateColumns="False" CssClass="table table-striped">
                    <Columns>
                        <asp:BoundField DataField="ProductID" HeaderText="Product ID" />
                        <asp:TemplateField HeaderText="Image">
                            <ItemTemplate>
                                <asp:Image ID="imgProduct" runat="server" ImageUrl='<%# Eval("ImageUrl") %>' Height="50" Width="50" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="ProductName" HeaderText="Product Name" />
                        <asp:BoundField DataField="Size" HeaderText="Size" />
                        <asp:BoundField DataField="Price" HeaderText="Price" DataFormatString="{0:C}" />
                        <asp:BoundField DataField="Quantity" HeaderText="Quantity" />
                        <asp:BoundField DataField="Subtotal" HeaderText="Subtotal" DataFormatString="{0:C}" />
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>
</asp:Content>




