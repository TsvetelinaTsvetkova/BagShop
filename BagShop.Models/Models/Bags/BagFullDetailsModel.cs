using System;
using System.Collections.Generic;
using System.Text;

namespace BagShop.Models.Models.Bags
{
  public class BagFullDetailsModel
    {
        public int BagId { get; set; }

        public string ImageUrl { get; set; }

        public string Title { get; set; }

        public string Color { get; set; }

        public double Price { get; set; }

        public string Description { get; set; }

        public int Quantity { get; set; }
    }
}
