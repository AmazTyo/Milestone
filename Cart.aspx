<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Cart.aspx.cs" Inherits="WebApplication1MileStone1.Cart" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Shopping Cart</h2>
    <asp:Label ID="lblEmptyCartMessage" runat="server" Visible="false" Text="Shopping cart is empty." CssClass="text-center d-flex align-items-center justify-content-center" style="height: 200px;"></asp:Label>
    <asp:GridView ID="gvCart" runat="server" AutoGenerateColumns="False" OnRowCommand="gvCart_RowCommand" DataKeyNames="ProductID,Size" CssClass="table table-striped">
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
            <asp:TemplateField HeaderText="Quantity">
                <ItemTemplate>
                    <asp:TextBox ID="txtQuantity" runat="server" Text='<%# Eval("Quantity") %>' CssClass="form-control quantity-textbox" style="width: 50px; display: inline;" min="1" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="Subtotal" HeaderText="Subtotal" DataFormatString="{0:C}" />
            <asp:TemplateField HeaderText="Actions">
                <ItemTemplate>
                    <asp:Button ID="btnUpdate" runat="server" Text="Update" CommandName="UpdateQuantity" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>' CssClass="btn btn-primary update-button" style="margin-left: 10px;" />
                    <asp:Button ID="btnRemove" runat="server" Text="Remove" CommandName="Remove" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>' CssClass="btn btn-danger remove-button" style="margin-left: 10px;" />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    <h3>
        <asp:Label ID="lblGrandTotal" runat="server" CssClass="grand-total" />
    </h3>
    <asp:Button ID="btnCheckout" runat="server" Text="Checkout" OnClick="btnCheckout_Click" CssClass="btn btn-danger" style="margin-top: 20px;" />
</asp:Content>





