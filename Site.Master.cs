using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1MileStone1
{
    public partial class SiteMaster : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            UpdateCartCount();
        }
        public void UpdateCartCount()
        {
            List<ShoppingCartItem> cartItems;
            if (Session["UserID"] != null)
            {
                int userId = Convert.ToInt32(Session["UserID"]);
                cartItems = ShoppingCartHelper.GetCartItems(userId, Session); // Added userId and Session
            }
            else
            {
                cartItems = (List<ShoppingCartItem>)Session["Cart"];
                if (cartItems == null)
                {
                    cartItems = new List<ShoppingCartItem>();
                }
            }
            lblCartCount.Text = cartItems.Sum(item => item.Quantity).ToString();
        }
    }
}

