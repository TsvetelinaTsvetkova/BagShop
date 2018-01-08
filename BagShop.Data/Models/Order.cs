using System;
using System.Collections.Generic;
using System.Text;

namespace BagShop.Data.Models
{
   public class Order
    {
        public int Id { get; set; }

        public string UserId { get; set; }

        public User User { get; set; }

        public double TotalPrice { get; set; }

        public List<OrderItem> Items { get; set; } = new List<OrderItem>();
    }
}
