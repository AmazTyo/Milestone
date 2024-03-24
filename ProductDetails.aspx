<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ProductDetails.aspx.cs" Inherits="WebApplication1MileStone1.ProductDetails" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
   <main>
      <div class="container mt-5">
         <div class="row">
            <div class="col-lg-6">
               <asp:Image ID="imgProduct" runat="server" CssClass="img-fluid rounded mb-4" />
            </div>
            <div class="col-lg-6">
               <h5><asp:Literal ID="ltProductName" runat="server" /></h5>
               <p><asp:Literal ID="ltPrice" runat="server" /></p>
               <div class="form-group">
                  <label for="size">Size:</label>
                   <asp:DropDownList ID="ddlSize" runat="server" CssClass="form-control">
                       <asp:ListItem Text="Small" Value="Small"></asp:ListItem>
                       <asp:ListItem Text="Medium" Value="Medium"></asp:ListItem>
                       <asp:ListItem Text="Large" Value="Large"></asp:ListItem>
                   </asp:DropDownList>                                     
               </div>
               <div class="form-group">
                   <label for="quantity">Quantity:</label>
                   <input type="number" id="txtQuantity" min="1" value="1" class="form-control" runat="server">
               </div>
               <details class="form-group mt-3">
                  <summary class="form-control" style="cursor: pointer;">Product Details</summary>
                  <div style="margin-top: 10px; padding: 10px; border: 1px solid #ddd; border-radius: 4px;">
                     <p>Product ID: <%# Eval("ProductId") %></p>
                     <p>Category: Clothing</p>
                     <p>Short sleeve tee</p>
                     <p>Printed art on the front</p>
                     <p>Regular fit</p>
                     <p>100% fit</p>
                     <p>Officially licensed <%# Eval("ProductName") %> Merchandise</p>
                  </div>
               </details>
                <asp:Button ID="btnAddToCart" runat="server" Text="Add to Cart" CssClass="btn btn-danger add-to-cart mt-3" OnClick="AddToCart_Click" />
            </div>
         </div>
      </div>
   </main>
</asp:Content>
