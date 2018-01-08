using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BagShop.Models.Bags
{
    public class BagListingModel
    {
        public int Id { get; set; }

        public string ImageUrl { get; set; }

        public string Title { get; set; }

        public string Color { get; set; }

        public double Price { get; set; }

        public string Description { get; set; }

    }
}
