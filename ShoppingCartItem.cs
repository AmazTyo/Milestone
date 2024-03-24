public class ShoppingCartItem
{
    public int ProductID { get; set; }
    public string ProductName { get; set; }
    public string Size { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    public decimal Subtotal { get; set; }
    public string ImageUrl { get; set; }

    public ShoppingCartItem(int productId, string productName, string size, decimal price, int quantity, decimal subtotal, string imageUrl)
    {
        ProductID = productId;
        ProductName = productName;
        Size = size;
        Price = price;
        Quantity = quantity;
        Subtotal = subtotal;
        ImageUrl = imageUrl;
    }
}
