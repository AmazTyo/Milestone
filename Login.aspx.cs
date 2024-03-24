using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Remoting.Lifetime;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1MileStone1
{
    public partial class Login : Page
    {
        private static string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["LoginMessage"] != null)
            {
                lblLoginMessage.Text = Session["LoginMessage"].ToString();
                lblLoginMessage.Visible = true;
                Session.Remove("LoginMessage");
            }
        }
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string email = txtUsername.Text;
            string password = txtPassword.Text;

            // Check if the user is an admin
            bool isAdmin = CheckAdmin(email, password);

            if (isAdmin)
            {
                // Admin authentication successful
                // Redirect the admin to the admin management page
                Response.Redirect("~/AdminManagement.aspx");
            }
            else
            {
                // Check if the user exists and authenticate
                bool isAuthenticated = AuthenticateUser(email, password);

                if (isAuthenticated)
                {
                    // User authentication successful
                    // Set session variables and redirect to the default page
                    Session["Email"] = email;
                    Session["UserID"] = GetUserId(email);
                    Response.Redirect("~/Default.aspx");
                }
                else
                {
                    // Authentication failed
                    lblMessage.Text = "Invalid email or password.";
                }
            }
        }
        private bool CheckAdmin(string email, string password)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT COUNT(*) FROM Admin WHERE Email = @Email AND Password = @Password";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Email", email);
                command.Parameters.AddWithValue("@Password", password);

                connection.Open();
                int count = (int)command.ExecuteScalar();

                if (count > 0)
                {
                    // Admin authentication successful, set Session["AdminEmail"]
                    Session["AdminEmail"] = email;
                    return true;
                }
                else
                {
                    // Admin authentication failed
                    return false;
                }
            }
        }
        private bool AuthenticateUser(string email, string password)
        {
            // Retrieve the hashed password from the database
            string hashedPasswordFromDB = GetPasswordHash(email);

            if (string.IsNullOrEmpty(hashedPasswordFromDB))
            {
                // User does not exist
                return false;
            }
            // Hash the input password
            string hashedInputPassword = HashPassword(password);

            // Compare hashed passwords
            return hashedPasswordFromDB == hashedInputPassword;
        }
        private string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
        private string GetPasswordHash(string email)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT PasswordHash FROM Users WHERE Email = @Email";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Email", email);

                connection.Open();
                object result = command.ExecuteScalar();

                return result != null ? result.ToString() : null;
            }
        }
        private int GetUserId(string email)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT UserID FROM Users WHERE Email = @Email";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Email", email);

                connection.Open();
                object result = command.ExecuteScalar();

                return result != null ? (int)result : 0;
            }
        }
        private bool CheckUserExists(string email)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT COUNT(*) FROM Users WHERE Email = @Email";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Email", email);

                connection.Open();
                int count = (int)command.ExecuteScalar();

                return count > 0;
            }
        }
        private bool VerifyPassword(string password, string hashedPassword)
        {
            // Hash the password
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                string hashedInputPassword = builder.ToString();

                // Compare the hashed password from the database with the hashed input password
                return hashedInputPassword == hashedPassword;
            }
        }
    }
}
