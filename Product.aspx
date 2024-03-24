<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Product.aspx.cs" Inherits="WebApplication1MileStone1.Product" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
   <main>
      <div class="container">
         <div class="products text-center">
            <div class="row">
               <asp:Repeater ID="rptProducts" runat="server">
                    <ItemTemplate>
                        <div class="col-3 text-center">
                            <a runat="server" href='<%# "~/ProductDetails.aspx?id=" + Eval("ProductId") %>'>
                                <img src='<%# Eval("ImageUrl") %>' alt='<%# Eval("ProductName") %>' class="img-fluid mt-3 mb-3" />
                            </a>
                            <a runat="server" href='<%# "~/ProductDetails.aspx?id=" + Eval("ProductId") %>' style="text-decoration: none; color: inherit;">
                                <h5><%# Eval("ProductName") %></h5>
                                <p><%# Eval("ProductName2") %>
                                    <br />$<%# Eval("Price") %></p>
                            </a>            
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
         </div>
      </div>
   </main>
</asp:Content>
