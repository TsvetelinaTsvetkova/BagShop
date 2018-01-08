using BagShop.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BagShop.Models.Models.Orders
{
   public class OrderViewModel
    {
        public int Id { get; set; }

        public double TotalPrice { get; set; }

        public string UserName { get; set; }

        public int ItemsCount { get; set; }
    }
}
