using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1MileStone1.Account
{
    public partial class Logout : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Get the user ID from the session
            int? userId = Session["UserID"] != null ? Convert.ToInt32(Session["UserID"]) : (int?)null;

            // If the user is logged in
            if (userId.HasValue)
            {
                // Get the shopping cart items from the session
                List<ShoppingCartItem> cartItems = (List<ShoppingCartItem>)Session["Cart"];

                // If there are any items in the cart
                if (cartItems != null && cartItems.Any())
                {
                    // Save each item to the database
                    foreach (var item in cartItems)
                    {
                        ShoppingCartHelper.AddToCart(userId.Value, item, Session);
                    }
                }
            }

            // Clear the shopping cart items from the session
            Session["Cart"] = null;

            // Clear the session
            Session.Clear();

            // Redirect the user to the login page
            Response.Redirect("Login.aspx");
        }
    }
}


