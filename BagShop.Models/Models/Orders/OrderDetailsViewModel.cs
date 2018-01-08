using System;
using System.Collections.Generic;
using System.Text;

namespace BagShop.Models.Models.Orders
{
   public class OrderDetailsViewModel
    {
        public string OrderId { get; set; }

        public string UserName { get; set; }

        public double FullPrice { get; set; }
  
        public List<OrderProductDetailsViewModel> Products { get; set; } = new List<OrderProductDetailsViewModel>();
    }
}
