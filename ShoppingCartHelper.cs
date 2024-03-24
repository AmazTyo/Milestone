using System.Collections.Generic;
using System.Data.SqlClient;
using System.Web;
using System.Linq;
using System;
using System.Web.UI.WebControls;
using System.Web.SessionState;

namespace WebApplication1MileStone1
{
    public static class ShoppingCartHelper
    {
        private const string CartSessionKey = "Cart";
        private static string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;

        public static int? GetUserId(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                // For anonymous users, return null
                return null;
            }
            int userId = 0;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT UserID FROM Users WHERE Email = @Email";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Email", email);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    userId = Convert.ToInt32(reader["UserID"]);
                }
            }
            return userId;
        }
        public static List<ShoppingCartItem> GetCartItems(int? userId, HttpSessionState session)
        {
            if (userId.HasValue && userId != 0)
            {
                // Load the cart from the database for logged-in users
                return LoadCartFromDatabase(userId.Value);
            }
            else
            {
                // Load the cart from the session for guest users
                return session[CartSessionKey] as List<ShoppingCartItem> ?? new List<ShoppingCartItem>();
            }
        }
        public static void AddToCart(int? userId, ShoppingCartItem item, HttpSessionState session)
        {
            if (userId.HasValue && userId != 0)
            {
                // Add the item to the cart in the database for logged-in users
                AddToCartInDatabase(userId.Value, item);
            }
            else
            {
                // Add the item to the cart in the session for guest users
                AddToCartInSession(item, session);
            }
        }

        public static void ClearCart(int? userId, HttpSessionState session)
        {
            if (userId.HasValue && userId != 0)
            {
                // Clear the cart in the database for logged-in users
                ClearCartInDatabase(userId.Value);
            }
            else
            {
                // Clear the cart in the session for guest users
                session[CartSessionKey] = new List<ShoppingCartItem>();
            }
        }
        private static List<ShoppingCartItem> LoadCartFromDatabase(int userId)
        {
            List<ShoppingCartItem> cartItems = new List<ShoppingCartItem>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM CartItems WHERE UserID = @UserID";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@UserID", userId);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    ShoppingCartItem item = new ShoppingCartItem(
                        Convert.ToInt32(reader["ProductID"]),
                        Convert.ToString(reader["ProductName"]),
                        Convert.ToString(reader["Size"]),
                        Convert.ToDecimal(reader["Price"]),
                        Convert.ToInt32(reader["Quantity"]),
                        Convert.ToDecimal(reader["Subtotal"]),
                        Convert.ToString(reader["ImageUrl"])
                    );
                    cartItems.Add(item);
                }
            }
            return cartItems;
        }
        private static void AddToCartInDatabase(int userId, ShoppingCartItem item)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Check if the item already exists in the cart for the user with the same size
                string checkQuery = "SELECT COUNT(*) FROM CartItems WHERE UserID = @UserID AND ProductID = @ProductID AND Size = @Size";
                SqlCommand checkCommand = new SqlCommand(checkQuery, connection);
                checkCommand.Parameters.AddWithValue("@UserID", userId);
                checkCommand.Parameters.AddWithValue("@ProductID", item.ProductID);
                checkCommand.Parameters.AddWithValue("@Size", item.Size);

                connection.Open();
                int existingCount = (int)checkCommand.ExecuteScalar();

                if (existingCount > 0)
                {
                    // If the item already exists with the same size, update the quantity
                    string updateQuery = "UPDATE CartItems SET Quantity = Quantity + @Quantity WHERE UserID = @UserID AND ProductID = @ProductID AND Size = @Size";
                    SqlCommand updateCommand = new SqlCommand(updateQuery, connection);
                    updateCommand.Parameters.AddWithValue("@UserID", userId);
                    updateCommand.Parameters.AddWithValue("@ProductID", item.ProductID);
                    updateCommand.Parameters.AddWithValue("@Size", item.Size);
                    updateCommand.Parameters.AddWithValue("@Quantity", item.Quantity);

                    updateCommand.ExecuteNonQuery();
                }
                else
                {
                    // If the item does not exist with the same size, insert a new entry
                    string insertQuery = "INSERT INTO CartItems (UserID, ProductID, ProductName, Size, Price, Quantity, Subtotal, ImageUrl) " +
                                         "VALUES (@UserID, @ProductID, @ProductName, @Size, @Price, @Quantity, @Subtotal, @ImageUrl)";
                    SqlCommand insertCommand = new SqlCommand(insertQuery, connection);
                    insertCommand.Parameters.AddWithValue("@UserID", userId);
                    insertCommand.Parameters.AddWithValue("@ProductID", item.ProductID);
                    insertCommand.Parameters.AddWithValue("@ProductName", item.ProductName);
                    insertCommand.Parameters.AddWithValue("@Size", item.Size);
                    insertCommand.Parameters.AddWithValue("@Price", item.Price);
                    insertCommand.Parameters.AddWithValue("@Quantity", item.Quantity);
                    insertCommand.Parameters.AddWithValue("@Subtotal", item.Subtotal);
                    insertCommand.Parameters.AddWithValue("@ImageUrl", item.ImageUrl);

                    insertCommand.ExecuteNonQuery();
                }
            }
        }
        private static void AddToCartInSession(ShoppingCartItem item, HttpSessionState session)
        {
            List<ShoppingCartItem> cartItems = GetCartItems(null, session);
            var existingItem = cartItems.FirstOrDefault(i => i.ProductID == item.ProductID && i.Size == item.Size);

            if (existingItem != null)
            {
                // If the item already exists with the same size, update the quantity
                existingItem.Quantity += item.Quantity;
                existingItem.Subtotal = existingItem.Price * existingItem.Quantity;
            }
            else
            {
                // If the item does not exist with the same size, add a new entry
                cartItems.Add(item);
            }

            session[CartSessionKey] = cartItems;
        }
        public static void RemoveFromCart(int? userId, int productId, string size, HttpSessionState session)
        {
            if (userId.HasValue && userId != 0)
            {
                // The user is logged in, remove the item from the cart in the database
                RemoveFromCartInDatabase(userId.Value, productId, size);
            }
            else
            {
                // The user is not logged in, remove the item from the cart in the session
                RemoveFromCartInSession(productId, size, session);
            }
        }

        public static void UpdateQuantity(int? userId, int productId, string size, int quantity, HttpSessionState session)
        {
            if (userId.HasValue && userId != 0)
            {
                // The user is logged in, update the quantity in the database
                UpdateQuantityInDatabase(userId.Value, productId, size, quantity);
            }
            else
            {
                // The user is not logged in, update the quantity in the session
                UpdateQuantityInSession(productId, size, quantity, session);
            }
        }
        private static void RemoveFromCartInDatabase(int userId, int productId, string size)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "DELETE FROM CartItems WHERE UserID = @UserID AND ProductID = @ProductID AND Size = @Size";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@UserID", userId);
                command.Parameters.AddWithValue("@ProductID", productId);
                command.Parameters.AddWithValue("@Size", size);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }
        private static void RemoveFromCartInSession(int productId, string size, HttpSessionState session)
        {
            List<ShoppingCartItem> cartItems = GetCartItems(null, session);
            var itemToRemove = cartItems.FirstOrDefault(i => i.ProductID == productId && i.Size == size);
            if (itemToRemove != null)
            {
                cartItems.Remove(itemToRemove);
                session[CartSessionKey] = cartItems;
            }
        }
        private static void UpdateQuantityInDatabase(int userId, int productId, string size, int quantity)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // First, get the price from the database
                string priceQuery = "SELECT Price FROM CartItems WHERE UserID = @UserID AND ProductID = @ProductID AND Size = @Size";
                SqlCommand priceCommand = new SqlCommand(priceQuery, connection);
                priceCommand.Parameters.AddWithValue("@UserID", userId);
                priceCommand.Parameters.AddWithValue("@ProductID", productId);
                priceCommand.Parameters.AddWithValue("@Size", size);

                connection.Open();
                object result = priceCommand.ExecuteScalar();
                decimal price = result != null ? Convert.ToDecimal(result) : 0m;
                connection.Close();

                // Then, update the quantity and subtotal
                string query = "UPDATE CartItems SET Quantity = @Quantity, Subtotal = @Subtotal WHERE UserID = @UserID AND ProductID = @ProductID AND Size = @Size";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@UserID", userId);
                command.Parameters.AddWithValue("@ProductID", productId);
                command.Parameters.AddWithValue("@Size", size);
                command.Parameters.AddWithValue("@Quantity", quantity);
                command.Parameters.AddWithValue("@Subtotal", quantity * price);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }
        private static void UpdateQuantityInSession(int productId, string size, int quantity, HttpSessionState session)
        {
            List<ShoppingCartItem> cartItems = GetCartItems(null, session);
            var itemToUpdate = cartItems.FirstOrDefault(i => i.ProductID == productId && i.Size == size);
            if (itemToUpdate != null)
            {
                itemToUpdate.Quantity = quantity;
                itemToUpdate.Subtotal = itemToUpdate.Price * itemToUpdate.Quantity;
                session[CartSessionKey] = cartItems;
            }
        }
        private static void ClearCartInDatabase(int userId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "DELETE FROM CartItems WHERE UserID = @UserID";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@UserID", userId);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }
        public static int CompletePurchase(int userId, List<ShoppingCartItem> cartItems)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("INSERT INTO Orders (UserID, ProductID, OrderDate, Quantity, Subtotal) " +
                                                    "OUTPUT INSERTED.OrderID " +
                                                    "VALUES (@UserID, @ProductID, @OrderDate, @Quantity, @Subtotal)", connection);
                command.Parameters.AddWithValue("@UserID", userId);
                command.Parameters.AddWithValue("@ProductID", cartItems[0].ProductID); // Assuming there's at least one item in the cart
                command.Parameters.AddWithValue("@OrderDate", DateTime.Now);
                command.Parameters.AddWithValue("@Quantity", cartItems[0].Quantity); // Assuming there's at least one item in the cart
                command.Parameters.AddWithValue("@Subtotal", cartItems[0].Subtotal); // Assuming there's at least one item in the cart

                int newOrderId = (int)command.ExecuteScalar();

                for (int i = 1; i < cartItems.Count; i++)
                {
                    command = new SqlCommand("INSERT INTO Orders (OrderID, UserID, ProductID, OrderDate, Quantity, Subtotal) " +
                                             "VALUES (@OrderID, @UserID, @ProductID, @OrderDate, @Quantity, @Subtotal)", connection);
                    command.Parameters.AddWithValue("@OrderID", newOrderId);
                    command.Parameters.AddWithValue("@UserID", userId);
                    command.Parameters.AddWithValue("@ProductID", cartItems[i].ProductID);
                    command.Parameters.AddWithValue("@OrderDate", DateTime.Now);
                    command.Parameters.AddWithValue("@Quantity", cartItems[i].Quantity);
                    command.Parameters.AddWithValue("@Subtotal", cartItems[i].Subtotal);

                    command.ExecuteNonQuery();
                }

                return newOrderId;
            }
        }
    }
}

