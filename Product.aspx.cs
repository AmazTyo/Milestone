using System;
using System.Data.SqlClient;
using System.Web.UI;

namespace WebApplication1MileStone1
{
    public partial class Product : System.Web.UI.Page
    {
        private static string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                PopulateProductsRepeater();
            }
        }

        private void PopulateProductsRepeater()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT ProductId, ProductName, ProductName2, Price, ImageUrl FROM Products";
                SqlCommand command = new SqlCommand(query, connection);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                rptProducts.DataSource = reader;
                rptProducts.DataBind();
            }
        }
    }
}
