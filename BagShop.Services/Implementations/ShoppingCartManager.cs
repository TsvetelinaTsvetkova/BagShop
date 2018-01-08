using System;
using System.Collections.Generic;
using System.Text;
using BagShop.Data.Models;
using System.Collections.Concurrent;

namespace BagShop.Services.Implementations
{
    public class ShoppingCartManager : IShoppingCartManager
    {
        private readonly ConcurrentDictionary<string, ShoppingCart> carts;

        public ShoppingCartManager()
        {
            this.carts = new ConcurrentDictionary<string, ShoppingCart>();
        }

        public void AddToCart(string id, int bagId)
        {
            var shoppingCart = this.GetShoppingCart(id);
            shoppingCart.AddToCart(bagId);
        }

        public IEnumerable<CartItem> GetItems(string id)
        {
            var shoppingCart = this.GetShoppingCart(id);
            return new List<CartItem>(shoppingCart.Items);
        }

        private ShoppingCart GetShoppingCart(string id)
        {
            return this.carts.GetOrAdd(id, new ShoppingCart());
        }

        public void RemoveFromCart(string id, int bagId)
        {
            var shoppingCart = this.GetShoppingCart(id);
            shoppingCart.RemoveFromCart(bagId);
        }

        public void Clear(string id) => this.GetShoppingCart(id).Clear();
    }
}
