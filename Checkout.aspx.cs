using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebApplication1MileStone1;
using System.Data.SqlClient;

namespace WebApplication1MileStone1
{
    public partial class Checkout : Page
    {
        private static string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindCart();
            }
        }
        private void BindCart()
        {
            int? userId = ShoppingCartHelper.GetUserId(Session["Email"]?.ToString());
            var cartItems = ShoppingCartHelper.GetCartItems(userId ?? 0, Session);

            // Bind the cart items to the GridView
            gvCart.DataSource = cartItems;
            gvCart.DataBind();
        }

        protected void btnCompleteOrder_Click(object sender, EventArgs e)
        {
            // Validate the form
            if (!IsValidForm())
            {
                lblMessage.Text = "Please fill in all required fields.";
                lblMessage.Visible = true;
                return;
            }

            // If the form is valid, complete the purchase
            var userId = ShoppingCartHelper.GetUserId(txtEmail.Text);
            if (userId.HasValue)
            {
                var cartItems = ShoppingCartHelper.GetCartItems(userId.Value, Session);

                // Save the order details to the database
                foreach (var item in cartItems)
                {
                    var orderDetail = new OrderDetail
                    {
                        ProductID = item.ProductID,
                        OrderDate = DateTime.Now,
                        Quantity = item.Quantity,
                        Subtotal = item.Subtotal
                    };
                    SaveOrderDetail(orderDetail);
                }

                // Clear the cart
                ShoppingCartHelper.ClearCart(userId.Value, Session);

                // Redirect to a confirmation page (you'll need to create this page)
                Response.Redirect("~/OrderConfirmation.aspx");
            }
            else
            {
                // Handle the case when userId is null
                // For example, show an error message
                lblMessage.Text = "User ID could not be found.";
                lblMessage.Visible = true;
            }
        }
        private void SaveOrderDetail(OrderDetail orderDetail)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO Orders (UserID, ProductID, OrderDate, Quantity, Subtotal) " +
                               "VALUES (@UserID, @ProductID, @OrderDate, @Quantity, @Subtotal)";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@UserID", Session["UserID"]); 
                command.Parameters.AddWithValue("@ProductID", orderDetail.ProductID);
                command.Parameters.AddWithValue("@OrderDate", orderDetail.OrderDate);
                command.Parameters.AddWithValue("@Quantity", orderDetail.Quantity);
                command.Parameters.AddWithValue("@Subtotal", orderDetail.Subtotal);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }
        private bool IsValidForm()
        {
            return !string.IsNullOrWhiteSpace(txtEmail.Text)
                && !string.IsNullOrWhiteSpace(txtAddress.Text)
                && !string.IsNullOrWhiteSpace(txtCity.Text)
                && !string.IsNullOrWhiteSpace(txtState.Text)
                && !string.IsNullOrWhiteSpace(txtZipCode.Text)
                && IsValidEmail(txtEmail.Text);
        }
        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

    }
}
