using System;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1MileStone1
{
    public partial class OrderConfirmation : System.Web.UI.Page
    {
        private const string CartSessionKey = "Cart";
        private static string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindOrderDetails();
            }
        }
        private void BindOrderDetails()
        {
            // Get the order details from the database
            var orderDetails = GetOrderDetails();

            // Bind the order details to the GridView
            gvOrderDetails.DataSource = orderDetails;
            gvOrderDetails.DataBind();
        }
        private List<OrderDetail> GetOrderDetails()
        {
            List<OrderDetail> orderDetails = new List<OrderDetail>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT Orders.*, Users.FirstName, Users.LastName FROM Orders INNER JOIN Users ON Orders.UserID = Users.UserID WHERE Orders.UserID = @UserID";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@UserID", Session["UserID"]);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    OrderDetail detail = new OrderDetail
                    {
                        OrderID = Convert.ToInt32(reader["OrderID"]),
                        ProductID = Convert.ToInt32(reader["ProductID"]),
                        Quantity = Convert.ToInt32(reader["Quantity"]),
                        Subtotal = Convert.ToDecimal(reader["Subtotal"]),
                        OrderDate = Convert.ToDateTime(reader["OrderDate"]),
                        Username = reader["FirstName"].ToString() + " " + reader["LastName"].ToString() // Assuming the 'Username' is the user's full name
                    };
                    orderDetails.Add(detail);
                }
            }
            return orderDetails;
        }
    }
}



