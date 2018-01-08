using BagShop.Data.Models;
using BagShop.Models.Models.Orders;
using System;
using System.Collections.Generic;
using System.Text;

namespace BagShop.Services
{
    public interface IOrderService
    {
        IEnumerable<OrderViewModel> Index();
    }
}
