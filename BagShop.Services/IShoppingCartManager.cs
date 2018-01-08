using BagShop.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BagShop.Services
{
   public interface IShoppingCartManager
    {
        void AddToCart(string id,int bagId);

        void RemoveFromCart(string id, int bagId);

        IEnumerable<CartItem> GetItems(string id);

        void Clear(string id);
    }
}
