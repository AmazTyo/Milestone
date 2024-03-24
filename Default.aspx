<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebApplication1MileStone1._Default" %>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
   <main>
      <div class="row">
         <div class="jumbotron text-center">
            <h1>WELCOME TO KAWAII HOUSE ANIME MERCHANDISE STORE</h1>
            <p>EXPLORE OUR WIDE RANGE OF PRODUCTS!</p>
         </div>
         <div class="col">
            <img src="images/banner.jpg" alt="Image 1" class="img-fluid mt-3 mb-5" />
         </div>
         <div class="col">
            <img src="images/banner2.jpg" alt="Image 2" class="img-fluid mt-3 mb-5" />
         </div>
      </div>
      <div class="container">
         <div class="top-rated-fits text-center">
            <p>TOP RATED FITS</p>
         </div>
         <div class="best-sellers text-center">
            <h3>BEST SELLERS</h3>
            <div class="row">
               <asp:Repeater ID="rptBestSellers" runat="server">
                  <ItemTemplate>
                     <div class="col-3 text-center">
                        <a runat="server" href='<%# Eval("ProductPageUrl") %>'>
                           <img src='<%# Eval("ImageUrl") %>' alt="Best Seller" class="img-fluid mt-3 mb-3" />
                        </a>
                        <a runat="server" href='<%# Eval("ProductPageUrl") %>' style="text-decoration: none; color: inherit;">
                           <h5><%# Eval("ProductName") %></h5>
                           <p><%# Eval("ProductDescription") %><br />$<%# Eval("Price") %></p>
                        </a>
                     </div>
                  </ItemTemplate>
               </asp:Repeater>
            </div>
            <div class="mt-3 text-center">
               <a runat="server" href="~/Product" class="btn text-white" style="background-color: #1b133c; width: 150px;">SHOP ALL</a>
            </div>
         </div>
      </div>
      <div class="container mt-5">
         <div class="row">
            <div class="col-6 col-md-6 position-relative" style="background-color: black;">
               <br />
               <br />
               <br />
               <br />
               <a runat="server" href="~/Product" style="text-decoration: none;">
                  <h2 class="text-center" style="color: white;">ORIGINAL ANIME MERCH FOR ANIME LOVERS</h2>
                  <br />
                  <br />
                  <br />
                  <h4 class="text-center" style="color: white;">Kawaii House is here to help you live 
                     your best anime life. With weekly deals and crazy convenience 
                     you can count on us to keep you adorned 
                     in the best apparels.
                  </h4>
               </a>
               <a runat="server" href="~/Product" class="btn text-white position-absolute" style="background-color: #1b133c; 
                  bottom: 10%; left:100%; transform: translate(-50%, -50%); width: 200px;">SHOP ALL</a>
            </div>
            <div class="col-6 col-md-6" >
               <a runat="server" href="~/Product">
               <img src="images/pic.jpg" alt="Image" class="img-fluid">
               </a>
            </div>
         </div>
      </div>
      <div class="shop-by-series">
         <br />
         <br />
         <h3 class="text-center">SHOP BY SERIES</h3>
         <div class="merchandise-logos d-flex justify-content-center align-items-center">
            <a runat="server" href="~/ProductDetails.aspx?id=1002">
            <img src="images/jujutlogo.png" alt="Merchandise Logo 1" class="img-fluid mx-2">
            </a>
            <a runat="server" href="~/ProductDetails.aspx?id=1004">
            <img src="images/herologo.png" alt="Merchandise Logo 2" class="img-fluid mx-2">
            </a>
            <a runat="server" href="~/ProductDetails.aspx?id=1001">
            <img src="images/moblogo.png" alt="Merchandise Logo 3" class="img-fluid mx-2">
            </a>
            <a runat="server" href="~/ProductDetails.aspx?id=1003">
            <img src="images/narutologo.png" alt="Merchandise Logo 4" class="img-fluid mx-2">
            </a>
         </div>
      </div>
   </main>
</asp:Content>
