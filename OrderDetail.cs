using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1MileStone1
{
    public class OrderDetail
    {
        public int UserID { get; set; }
        public int OrderID { get; set; }
        public int ProductID { get; set; }
        public int Quantity { get; set; }
        public decimal Subtotal { get; set; }
        public DateTime OrderDate { get; set; }
        public string Username { get; set; }
    }

}