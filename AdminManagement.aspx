<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AdminManagement.aspx.cs" Inherits="WebApplication1MileStone1.AdminManagement" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container">
        <h3>Admin</h3>
        <div class="row">
            <div class="col-md-4">
                <h5>Add New Product</h5>
                
                <div class="form-group">
                     <label for="txtProductID">Product ID:</label>
                     <asp:TextBox ID="txtProductID" CssClass="form-control" runat="server"></asp:TextBox>
               </div>
                <div class="form-group">
                    <label for="txtProductName">Product Name:</label>
                    <asp:TextBox ID="txtProductName" CssClass="form-control" runat="server"></asp:TextBox>
                </div>

                <div class="form-group">
                    <label for="txtPrice">Price:</label>
                    <asp:TextBox ID="txtPrice" CssClass="form-control" runat="server"></asp:TextBox>
                </div>
                <div class="form-group">
                    <label for="txtImageUrl">Image URL:</label>
                    <asp:TextBox ID="txtImageUrl" CssClass="form-control" runat="server"></asp:TextBox>
                </div>
                <asp:Button ID="btnAddProduct" CssClass="btn btn-primary" runat="server" Text="Add Product" OnClick="btnAddProduct_Click" />
            </div>
            <div class="col-md-4">
                <h5>Update Product Details</h5>
                <div class="form-group">
                    
                    <label for="ddlProductsToUpdate">Select Product to Update:</label>
                    <asp:DropDownList ID="ddlProductsToUpdate" CssClass="form-control" runat="server" AutoPostBack="true">
                        <asp:ListItem Text=" " Value=""></asp:ListItem>                         
                    </asp:DropDownList>
                   
                </div>
                <div class="form-group">
                    <label for="ddlFieldToUpdate">Select Field to Update:</label>
                    <asp:DropDownList ID="ddlFieldToUpdate" CssClass="form-control" runat="server">
                        <asp:ListItem Text=" " Value=""></asp:ListItem>
                        <asp:ListItem Text="Product ID" Value="ProductID"></asp:ListItem>
                        <asp:ListItem Text="Product Name" Value="ProductName"></asp:ListItem>                  
                        <asp:ListItem Text="Price" Value="Price"></asp:ListItem>
                        <asp:ListItem Text="Image URL" Value="ImageUrl"></asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="form-group">
                    <label for="txtNewValue">New Value:</label>
                    <asp:TextBox ID="txtNewValue" CssClass="form-control" runat="server"></asp:TextBox>
                </div>
                <asp:Button ID="btnUpdateProduct" CssClass="btn btn-primary" runat="server" Text="Update Product" OnClick="btnUpdateProduct_Click" />
            </div>
            <div class="col-md-4">
                <h5>Delete Product</h5>
                <div class="form-group">
                    <label for="ddlProductsToDelete">Select Product to Delete:</label>
                    <asp:DropDownList ID="ddlProductsToDelete" CssClass="form-control" runat="server">  
                         <asp:ListItem Text=" " Value=""></asp:ListItem>
                    </asp:DropDownList>       
                </div>
                <div>
                <asp:Button ID="btnDeleteProduct" CssClass="btn btn-danger" runat="server" Text="Delete Product" OnClick="btnDeleteProduct_Click" />
           </div>
                    </div>
        </div>
    </div>
</asp:Content>


