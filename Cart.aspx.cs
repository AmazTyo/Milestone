using System.Linq;
using System.Web.UI.WebControls;
using System;
using WebApplication1MileStone1;

namespace WebApplication1MileStone1
{
    public partial class Cart : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindCart();
            }
            var cartItems = ShoppingCartHelper.GetCartItems(Session["UserID"] as int?, Session);
            if (cartItems.Count == 0)
            {
                btnCheckout.Visible = false;
            }
            else
            {
                btnCheckout.Visible = true;
            }
        }
        private void BindCart()
        {
            int? userId = ShoppingCartHelper.GetUserId(Session["Email"]?.ToString());
            var cartItems = ShoppingCartHelper.GetCartItems(userId ?? 0, Session);

            // If the cart is empty, show the empty cart message and hide the checkout button
            lblEmptyCartMessage.Visible = !cartItems.Any();
            btnCheckout.Visible = cartItems.Any();

            // Calculate the grand total and show the label only if there are items in the cart
            if (cartItems.Any())
            {
                decimal grandTotal = cartItems.Sum(item => item.Subtotal);
                lblGrandTotal.Text = "Grand Total: " + grandTotal.ToString("C");
                lblGrandTotal.Visible = true;
            }
            else
            {
                lblGrandTotal.Visible = false;
            }

            // Bind the cart items to the GridView
            gvCart.DataSource = cartItems;
            gvCart.DataBind();
        }
        protected void gvCart_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Remove" || e.CommandName == "UpdateQuantity")
            {
                int rowIndex = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = gvCart.Rows[rowIndex];
                int productId = Convert.ToInt32(gvCart.DataKeys[rowIndex].Value);
                string size = gvCart.DataKeys[rowIndex].Values["Size"].ToString();

                int? userId = ShoppingCartHelper.GetUserId(Session["Email"]?.ToString());

                if (e.CommandName == "Remove")
                {
                    ShoppingCartHelper.RemoveFromCart(userId ?? 0, productId, size, Session);
                    BindCart();
                    ((SiteMaster)Master).UpdateCartCount();

                    if (!ShoppingCartHelper.GetCartItems(userId ?? 0, Session).Any())
                    {
                        lblGrandTotal.Visible = false;
                    }
                }
                else if (e.CommandName == "UpdateQuantity")
                {
                    TextBox txtQuantity = (TextBox)row.FindControl("txtQuantity");

                    if (txtQuantity != null)
                    {
                        if (int.TryParse(txtQuantity.Text, out int quantity) && quantity > 0)
                        {
                            ShoppingCartHelper.UpdateQuantity(userId ?? 0, productId, size, quantity, Session);
                            BindCart();
                            ((SiteMaster)Master).UpdateCartCount();
                        }
                        else
                        {
                            ShoppingCartHelper.RemoveFromCart(userId ?? 0, productId, size, Session);
                            BindCart();
                        }
                    }
                }
            }
        }
        protected void btnCheckout_Click(object sender, EventArgs e)
        {
            // Check if the user is logged in
            if (Session["UserID"] == null)
            {
                // If the user is not logged in, set a session variable with a message
                Session["LoginMessage"] = "Please log in to complete your purchase.";
                // If the user is not logged in, redirect them to the login page
                Response.Redirect("~/Account/Login.aspx");
            }
            else
            {
                // If the user is logged in, redirect them to the checkout page
                Response.Redirect("~/Checkout.aspx");
            }
        }
    }
}
    