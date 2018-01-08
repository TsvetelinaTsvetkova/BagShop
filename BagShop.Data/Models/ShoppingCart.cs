using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BagShop.Data.Models
{
    public class ShoppingCart
    {
        private readonly IList<CartItem> items;

        public ShoppingCart()
        {
            this.items = new List<CartItem>();
        }

        public IEnumerable<CartItem> Items => new List<CartItem>(this.items);


        public void AddToCart(int bagId)
        {
            var cartItem = this.items.FirstOrDefault(b => b.BagId == bagId);

            if (cartItem == null)
            {
                cartItem = new CartItem
                {
                    BagId = bagId,
                    Quantity = 1
                };
                this.items.Add(cartItem);
            }
            else
            {
                cartItem.Quantity++;
            }
          
        }

        public void Clear() => this.items.Clear();

      
        public void RemoveFromCart(int bagId)
        {
            var cartItem = this.items
                .FirstOrDefault(i => i.BagId == bagId);

            if (cartItem != null)
            {
                this.items.Remove(cartItem);
            }
        }

    }
}
