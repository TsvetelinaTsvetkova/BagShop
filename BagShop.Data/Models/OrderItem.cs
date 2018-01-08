using System;
using System.Collections.Generic;
using System.Text;

namespace BagShop.Data.Models
{
   public class OrderItem
    {
        public int Id { get; set; }

        public int BagId { get; set; }

        public double BagPrice { get; set; }

        public int Quantity { get; set; }

        public int OrderId { get; set; }

        public Order Order { get; set; }
    }
}
