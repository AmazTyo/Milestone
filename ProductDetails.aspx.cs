using System;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;

namespace WebApplication1MileStone1
{
    public partial class ProductDetails : System.Web.UI.Page
    {
        private static string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int productId = Convert.ToInt32(Request.QueryString["id"]);
                PopulateProductDetails(productId);
            }
        }

        private void PopulateProductDetails(int productId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT ProductId, ProductName, Price, ImageUrl FROM Products WHERE ProductId = @ProductId";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@ProductId", productId);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    ltProductName.Text = reader["ProductName"].ToString();
                    ltPrice.Text = reader["Price"].ToString();
                    imgProduct.ImageUrl = reader["ImageUrl"].ToString();
                }
            }
        }
        protected void AddToCart_Click(object sender, EventArgs e)
        {
            // Get the product details
            int productId = Convert.ToInt32(Request.QueryString["id"]);
            string productName = ltProductName.Text;
            decimal price = Convert.ToDecimal(ltPrice.Text);
            string imageUrl = imgProduct.ImageUrl; // Make sure imgProduct is the ID of your Image control

            // Get the selected size and quantity
            string size = ddlSize.SelectedValue;
            int quantity = Convert.ToInt32(txtQuantity.Value); // Use the Value property for HtmlInputGenericControl

            // Create a new ShoppingCartItem
            ShoppingCartItem item = new ShoppingCartItem(productId, productName, size, price, quantity, price * quantity, imageUrl); // Pass imageUrl to ShoppingCartItem constructor

            // Get the user ID
            string userEmail = HttpContext.Current.User.Identity.Name; // Assuming you're using Forms Authentication
            int? userId = ShoppingCartHelper.GetUserId(userEmail);

            // Add the item to the cart
            ShoppingCartHelper.AddToCart(userId, item, Session);
        }

        protected void btnAddToCart_Click(object sender, EventArgs e)
        {
            // Get the product details
            int productId = Convert.ToInt32(Request.QueryString["id"]);
            string productName = ltProductName.Text;
            decimal price = Convert.ToDecimal(ltPrice.Text);
            string imageUrl = imgProduct.ImageUrl; // Make sure imgProduct is the ID of your Image control

            // Get the selected size and quantity
            string size = ddlSize.SelectedValue;
            int quantity = Convert.ToInt32(txtQuantity.Value); // Use the Value property for HtmlInputGenericControl

            // Create a new ShoppingCartItem
            ShoppingCartItem item = new ShoppingCartItem(productId, productName, size, price, quantity, price * quantity, imageUrl); // Pass imageUrl to ShoppingCartItem constructor

            // Get the user ID
            string userEmail = HttpContext.Current.User.Identity.Name; // Assuming you're using Forms Authentication
            int? userId = ShoppingCartHelper.GetUserId(userEmail);

            // Add the item to the cart
            ShoppingCartHelper.AddToCart(userId, item, Session);
        }
    }
}
