﻿using BagShop.Common.Mapping;
using BagShop.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BagShop.Models.Models.Bags
{
   public class BagListingServiceModel:IMapFrom<Bag>
    {
        public int Id { get; set; }

        public string ImageUrl { get; set; }

        public string Title { get; set; }

        public string Color { get; set; }

        public double Price { get; set; }

        public string Description { get; set; }

        public int Quantity { get; set; }
    }
}
