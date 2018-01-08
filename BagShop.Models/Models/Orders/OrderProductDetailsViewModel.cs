using System;
using System.Collections.Generic;
using System.Text;

namespace BagShop.Models.Models.Orders
{
    public class OrderProductDetailsViewModel
    {
        public string ProductName { get; set; }

        public double ProductPrice { get; set; }

        public double Quantity { get; set; }

        public double FullPrice
        {
            get
            {
                return this.ProductPrice * this.Quantity;
            }
        }
    }
}
