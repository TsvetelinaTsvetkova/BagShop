using System;
using System.Collections.Generic;
using System.Text;

namespace BagShop.Models.Models.ShoppingCart
{
    public class CartItemViewModel
    {
        public int BagId { get; set; }

        public string Title { get; set; }

        public double Price { get; set; }

        public int Quantity { get; set; }

        public double FullPrice
        {
            get
            {
                return this.Price * this.Quantity;
            }
        }
    }
}
