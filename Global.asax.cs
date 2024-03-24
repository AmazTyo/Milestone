using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;

namespace WebApplication1MileStone1
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Session_Start(object sender, EventArgs e)
        {
            // Code that runs when a new session is started
            if (Session["Email"] != null)
            {
                int? userId = ShoppingCartHelper.GetUserId(Session["Email"].ToString());
                var cartItems = ShoppingCartHelper.GetCartItems(userId ?? 0, Session);
                Session["Cart"] = cartItems;
            }
            else
            {
                Session["Cart"] = new List<ShoppingCartItem>();
            }
        }
        protected void Session_End(object sender, EventArgs e)
        {
            // Code that runs when a session ends.
            // Save the shopping cart items to the database
            List<ShoppingCartItem> cartItems = (List<ShoppingCartItem>)Session["Cart"];
            if (cartItems != null && cartItems.Any())
            {
                if (Session["Email"] != null)
                {
                    int? userId = ShoppingCartHelper.GetUserId(Session["Email"].ToString());
                    foreach (var item in cartItems)
                    {
                        ShoppingCartHelper.AddToCart(userId ?? 0, item, Session);
                    }
                }
            }
        }
    }
}