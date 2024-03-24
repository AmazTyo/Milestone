using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace WebApplication1MileStone1
{
    public partial class AdminManagement : System.Web.UI.Page
    {
        private static string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Populate the DropDownList for updating and deleting product details
                PopulateProductsDropDown();
            }
        }

        private void PopulateProductsDropDown()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT ProductID, ProductName FROM Products";
                SqlCommand command = new SqlCommand(query, connection);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    string productName = reader["ProductName"].ToString();
                    int productId = Convert.ToInt32(reader["ProductID"]);

                    // Add the product name and ID to the dropdown lists
                    ddlProductsToUpdate.Items.Add(new ListItem(productName, productId.ToString()));
                    ddlProductsToDelete.Items.Add(new ListItem(productName, productId.ToString()));
                }
            }
        }

        protected void btnAddProduct_Click(object sender, EventArgs e)
        {
            int productId = Convert.ToInt32(txtProductID.Text);
            string productName = txtProductName.Text;
            decimal price = Convert.ToDecimal(txtPrice.Text);
            string imageUrl = txtImageUrl.Text;

            // Insert the new product into the database
            InsertProduct(productId, productName, price, imageUrl);
        }

        private void InsertProduct(int productId, string productName, decimal price, string imageUrl)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO Products (ProductID, ProductName, Price, ImageUrl) VALUES (@ProductID, @ProductName, @Price, @ImageUrl)";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@ProductID", productId);
                command.Parameters.AddWithValue("@ProductName", productName);
                command.Parameters.AddWithValue("@Price", price);
                command.Parameters.AddWithValue("@ImageUrl", imageUrl);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        protected void btnUpdateProduct_Click(object sender, EventArgs e)
        {
            // Get the selected product ID, product name, and other details to update
            int selectedProductId = Convert.ToInt32(ddlProductsToUpdate.SelectedValue);
            string fieldToUpdate = ddlFieldToUpdate.SelectedValue;
            string newValue = txtNewValue.Text;

            // Update the selected field of the selected product in the database
            UpdateProductDetails(selectedProductId, fieldToUpdate, newValue);
        }

        private void UpdateProductDetails(int productId, string fieldToUpdate, string newValue)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = $"UPDATE Products SET {fieldToUpdate} = @NewValue WHERE ProductID = @ProductID";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@ProductID", productId);
                command.Parameters.AddWithValue("@NewValue", newValue);

                connection.Open();
                command.ExecuteNonQuery();
            }

            // Redirect to the same page to see the updated product details
            Response.Redirect("~/AdminManagement.aspx");
        }

        protected void btnDeleteProduct_Click(object sender, EventArgs e)
        {
            // Get the selected product ID
            int selectedProductId = Convert.ToInt32(ddlProductsToDelete.SelectedValue);

            // Delete the selected product from the database
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "DELETE FROM Products WHERE ProductID = @ProductID";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@ProductID", selectedProductId);

                connection.Open();
                command.ExecuteNonQuery();
            }

            // Redirect to the same page to see the updated product details
            Response.Redirect("~/AdminManagement.aspx");
        }
    }
}


