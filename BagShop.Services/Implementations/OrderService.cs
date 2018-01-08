using BagShop.Data;
using BagShop.Models.Models.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BagShop.Data.Models;

namespace BagShop.Services.Implementations
{
    public class OrderService : IOrderService
    {
        private readonly BagShopDbContext db;

        public OrderService(BagShopDbContext db)
        {
            this.db = db;
        }

        public IEnumerable<OrderViewModel> Index()
        {
            return this.db
             .Orders
             .Select(b => new OrderViewModel
             {
                 Id=b.Id,
                 TotalPrice = b.TotalPrice,
                 UserName = b.User.UserName
             })
             .ToList();
        }
    }
}
